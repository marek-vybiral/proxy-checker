using System.Collections.ObjectModel;

namespace ProxyChecker;

public sealed class ProxyDataModel
{
    public ObservableCollection<Proxy> ProxyList { get; } = new();

    public void AddProxy(Proxy p) => ProxyList.Add(p);

    public void AddRange(IEnumerable<Proxy> list)
    {
        foreach (var p in list) ProxyList.Add(p);
    }

    public void RemoveAll() => ProxyList.Clear();
}
