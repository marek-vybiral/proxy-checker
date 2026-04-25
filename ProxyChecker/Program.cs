using Eto.Forms;

namespace ProxyChecker;

internal static class Program
{
    [STAThread]
    private static int Main(string[] args)
    {
        if (args.Length > 0)
        {
            return Cli.RunAsync(args).GetAwaiter().GetResult();
        }

        new Application().Run(new MainForm());
        return 0;
    }
}
