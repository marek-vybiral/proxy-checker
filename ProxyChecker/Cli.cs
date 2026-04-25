namespace ProxyChecker;

internal static class Cli
{
    public static async Task<int> RunAsync(string[] args)
    {
        var opts = ParseArgs(args);
        if (opts == null) return 2;
        if (opts.ShowHelp) { PrintHelp(Console.Out); return 0; }

        var input = await ReadInputAsync(opts.Input!);
        var proxies = input
            .Select(Proxy.Parse)
            .Where(p => p != null)
            .Cast<Proxy>()
            .ToArray();

        if (proxies.Length == 0)
        {
            Console.Error.WriteLine("No valid proxies parsed from input.");
            return 1;
        }

        Console.Error.WriteLine($"Testing {proxies.Length} proxies (parallelism={opts.Parallel}, timeout={opts.TimeoutMs}ms)...");

        var done = 0;
        var lastReport = 0;
        using var cts = new CancellationTokenSource();
        Console.CancelKeyPress += (_, e) => { e.Cancel = true; cts.Cancel(); };

        try
        {
            await Parallel.ForEachAsync(
                proxies,
                new ParallelOptions
                {
                    MaxDegreeOfParallelism = opts.Parallel,
                    CancellationToken = cts.Token,
                },
                async (proxy, ct) =>
                {
                    await proxy.PerformTestAsync(TimeSpan.FromMilliseconds(opts.TimeoutMs), opts.UserAgent, ct);
                    var n = Interlocked.Increment(ref done);
                    if (!opts.Quiet)
                    {
                        var pct = (int)(n * 100.0 / proxies.Length);
                        var prev = Interlocked.Exchange(ref lastReport, pct);
                        if (pct != prev && pct % 5 == 0)
                        {
                            Console.Error.Write($"\r{n}/{proxies.Length} ({pct}%)");
                        }
                    }
                });
        }
        catch (OperationCanceledException)
        {
            Console.Error.WriteLine("\nCancelled.");
            return 130;
        }

        if (!opts.Quiet) Console.Error.WriteLine();

        var output = opts.OnlineOnly
            ? proxies.Where(p => p.Status == "online")
            : proxies;

        var lines = output.Select(p => opts.OnlineOnly
            ? $"{p.Endpoint.Address}:{p.Endpoint.Port}"
            : $"{p.Endpoint.Address}:{p.Endpoint.Port}\t{p.Status}");

        await WriteOutputAsync(opts.Output, lines);

        var online = proxies.Count(p => p.Status == "online");
        Console.Error.WriteLine($"Done. {online}/{proxies.Length} online.");
        return 0;
    }

    private static async Task<IReadOnlyList<string>> ReadInputAsync(string source)
    {
        if (source == "-")
        {
            var lines = new List<string>();
            string? line;
            while ((line = await Console.In.ReadLineAsync()) != null) lines.Add(line);
            return lines;
        }
        return await File.ReadAllLinesAsync(source);
    }

    private static async Task WriteOutputAsync(string? destination, IEnumerable<string> lines)
    {
        if (string.IsNullOrEmpty(destination) || destination == "-")
        {
            foreach (var line in lines) Console.WriteLine(line);
            return;
        }
        await File.WriteAllLinesAsync(destination, lines);
    }

    private static Options? ParseArgs(string[] args)
    {
        var opts = new Options();
        for (var i = 0; i < args.Length; i++)
        {
            var a = args[i];
            switch (a)
            {
                case "-h" or "--help":
                    opts.ShowHelp = true;
                    return opts;
                case "-i" or "--input":
                    if (++i >= args.Length) return Fail($"Missing value for {a}");
                    opts.Input = args[i];
                    break;
                case "-o" or "--output":
                    if (++i >= args.Length) return Fail($"Missing value for {a}");
                    opts.Output = args[i];
                    break;
                case "-t" or "--timeout":
                    if (++i >= args.Length || !int.TryParse(args[i], out var ms) || ms <= 0)
                        return Fail($"Invalid value for {a}");
                    opts.TimeoutMs = ms;
                    break;
                case "-p" or "--parallel":
                    if (++i >= args.Length || !int.TryParse(args[i], out var par) || par <= 0)
                        return Fail($"Invalid value for {a}");
                    opts.Parallel = par;
                    break;
                case "-u" or "--user-agent":
                    if (++i >= args.Length) return Fail($"Missing value for {a}");
                    opts.UserAgent = args[i];
                    break;
                case "--online-only":
                    opts.OnlineOnly = true;
                    break;
                case "-q" or "--quiet":
                    opts.Quiet = true;
                    break;
                default:
                    if (opts.Input == null && !a.StartsWith('-'))
                    {
                        opts.Input = a;
                        break;
                    }
                    return Fail($"Unknown argument: {a}");
            }
        }

        if (opts.Input == null && !opts.ShowHelp)
            return Fail("Missing input. Pass a file path or '-' for stdin. See --help.");

        return opts;
    }

    private static Options? Fail(string message)
    {
        Console.Error.WriteLine($"error: {message}");
        return null;
    }

    private static void PrintHelp(TextWriter w)
    {
        w.WriteLine("Usage: ProxyChecker [options] <input>");
        w.WriteLine();
        w.WriteLine("Checks a list of proxies in parallel. With no arguments, launches the GUI.");
        w.WriteLine();
        w.WriteLine("Arguments:");
        w.WriteLine("  <input>                    File with one IP:port per line, or '-' for stdin");
        w.WriteLine();
        w.WriteLine("Options:");
        w.WriteLine("  -i, --input <file>         Same as positional <input>");
        w.WriteLine("  -o, --output <file>        Output file (default: stdout)");
        w.WriteLine("  -t, --timeout <ms>         Per-proxy timeout in ms (default: 2000)");
        w.WriteLine("  -p, --parallel <n>         Max concurrent checks (default: 32)");
        w.WriteLine("  -u, --user-agent <ua>      HTTP User-Agent header");
        w.WriteLine("      --online-only          Only emit working proxies");
        w.WriteLine("  -q, --quiet                Suppress progress output");
        w.WriteLine("  -h, --help                 Show this help");
    }

    private sealed class Options
    {
        public string? Input { get; set; }
        public string? Output { get; set; }
        public int TimeoutMs { get; set; } = 2000;
        public int Parallel { get; set; } = 32;
        public string UserAgent { get; set; } = new Settings().UserAgent;
        public bool OnlineOnly { get; set; }
        public bool Quiet { get; set; }
        public bool ShowHelp { get; set; }
    }
}
