using System.Collections.Generic;
using System.Data;

namespace ProxyChecker
{
    public delegate void DataChangeEventHandler();

    public class ProxyDataModel
    {
        public ProxyDataModel()
        {
            Table = new DataTable();
            Table.Columns.Add("IP", typeof (string));
            Table.Columns.Add("Port", typeof (int));
            Table.Columns.Add("Status", typeof (string));

            ProxyList = new List<Proxy>();
        }

        public DataTable Table { get; }
        public IList<Proxy> ProxyList { get; }

        private void AddProxy(Proxy p)
        {
            Table.Rows.Add(p.IpEndPoint.Address, p.IpEndPoint.Port, p.Status);
            ProxyList.Add(p);
        }

        public void AddProxyList(IEnumerable<Proxy> list)
        {
            foreach (var p in list)
            {
                AddProxy(p);
            }
        }

        public void UpdateTable()
        {
            Table.Clear();

            foreach (var p in ProxyList)
            {
                Table.Rows.Add(p.IpEndPoint.Address, p.IpEndPoint.Port, p.Status);
            }
        }

        public void RemoveAll()
        {
            ProxyList.Clear();
            Table.Clear();
        }
    }
}