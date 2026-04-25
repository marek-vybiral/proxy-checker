using System.Net;
using System.Net.Http;

namespace ProxyChecker;

public sealed class Proxy
{
    public IPEndPoint Endpoint { get; }
    public string? Type { get; }

    private bool? _working;

    public string Status => _working switch
    {
        true => "online",
        false => "offline",
        null => "unknown",
    };

    public Proxy(IPEndPoint endpoint, string? type = null, bool? working = null)
    {
        Endpoint = endpoint;
        Type = type;
        _working = working;
    }

    public async Task PerformTestAsync(TimeSpan timeout, string userAgent, CancellationToken ct = default)
    {
        _working = await TestAsync(this, timeout, userAgent, ct);
    }

    public static Proxy? Parse(string raw)
    {
        var normalized = raw.Replace(';', ':').Replace(',', ':').Trim();
        var parts = normalized.Split(':', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length < 2) return null;
        if (!IPAddress.TryParse(parts[0], out var ip)) return null;
        if (!int.TryParse(parts[1], out var port)) return null;
        if (port < 1 || port > 65535) return null;
        return new Proxy(new IPEndPoint(ip, port));
    }

    public static async Task<bool> TestAsync(Proxy proxy, TimeSpan timeout, string userAgent, CancellationToken ct = default)
    {
        var handler = new HttpClientHandler
        {
            Proxy = new WebProxy(proxy.Endpoint.Address.ToString(), proxy.Endpoint.Port),
            UseProxy = true,
        };

        using var client = new HttpClient(handler) { Timeout = timeout };
        client.DefaultRequestHeaders.UserAgent.ParseAdd(userAgent);

        try
        {
            using var response = await client.GetAsync("http://example.com", HttpCompletionOption.ResponseHeadersRead, ct);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }
}
