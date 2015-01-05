using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Diagnostics;

namespace ProxyChecker
{
    public class Proxy
    {
        public IPEndPoint IPEndPoint { get; set; }
        public string Type { get; set; }
        public string Status
        {
            get
            {
                if (this._working == true)
                {
                    return "online";
                }
                else if (this._working == null)
                {
                    return "uknown";
                }
                return "offline";
            }
        }

        private Nullable<bool> _working = false;

        public Proxy(IPEndPoint endPoint, string type = null, Nullable<bool> working = null)
        {
            this.IPEndPoint = endPoint;
            this.Type = type;
            this._working = working;
        }

        public void PerformTest()
        {
            this._working = Proxy.TestProxy(this);
        }

        public static Proxy Parse(string str)
        {
            str = str.Replace(';', ':');
            str = str.Replace(',', ':');
            string[] parts = str.Split(':');

            try
            {
                string ipStr = parts[0];
                string portStr = parts[1];
                IPAddress ip;
                IPAddress.TryParse(ipStr, out ip);

                return new Proxy(new IPEndPoint(ip, int.Parse(portStr)));
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static bool TestProxy(Proxy proxy)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://google.com");
            request.Proxy = new WebProxy(proxy.IPEndPoint.Address.ToString(), proxy.IPEndPoint.Port);
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/39.0.2171.95 Safari/537.36";
            request.Timeout = 2000;

            try
            {
                WebResponse response = request.GetResponse();
            }
            catch (Exception)
            {
                return false;
            }
            return true;            
        }
    }
}
