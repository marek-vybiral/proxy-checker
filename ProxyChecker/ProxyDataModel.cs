using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyChecker
{
    public delegate void DataChangeEventHandler();
    public class ProxyDataModel
    {
        public DataTable Table { get; set; }
        public IList<Proxy> ProxyList { get; set; }

        public ProxyDataModel()
        {
            this.Table = new DataTable();
            this.Table.Columns.Add("IP", typeof(string));
            this.Table.Columns.Add("Port", typeof(int));
            this.Table.Columns.Add("Status", typeof(string));

            this.ProxyList = new List<Proxy>();
        }

        public void AddProxy(Proxy p)
        {
            this.Table.Rows.Add(p.IPEndPoint.Address, p.IPEndPoint.Port, p.Status);
            this.ProxyList.Add(p);
        }

        public void AddProxyList(IList<Proxy> list)
        {
            foreach (Proxy p in list)
            {
                this.AddProxy(p);
            }
        }

        public void UpdateTable()
        {
            this.Table.Clear();

            foreach (Proxy p in this.ProxyList)
            {
                this.Table.Rows.Add(p.IPEndPoint.Address, p.IPEndPoint.Port, p.Status);
            }
        }

        public void RemoveAll()
        {
            this.ProxyList.Clear();
            this.Table.Clear();
        }
    }
}
