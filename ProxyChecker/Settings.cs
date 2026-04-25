namespace ProxyChecker;

public sealed class Settings
{
    public TimeSpan ConnectionTimeout { get; set; } = TimeSpan.FromSeconds(2);
    public string UserAgent { get; set; } =
        "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/17.0 Safari/605.1.15";
    public int ParallelLimit { get; set; } = 32;
}
