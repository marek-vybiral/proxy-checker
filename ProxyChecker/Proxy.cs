using System;
using System.Net;
using System.Windows.Forms;

namespace ProxyChecker
{
    public class Proxy
    {
        private bool? _working = false;

        private Proxy(IPEndPoint endPoint, string type = null, bool? working = null)
        {
            IpEndPoint = endPoint;
            Type = type;
            _working = working;
        }

        public static HttpWebRequest Request { get; set; }
        public IPEndPoint IpEndPoint { get; }
        public string Type { get; set; }

        public string Status
        {
            get
            {
                if (_working != true)
                {
                    if (_working == null)
                    {
                        return "uknown";
                    }
                }
                else
                {
                    return "online";
                }


                return "offline";
            }
        }

        public void PerformTest()
        {
            _working = TestProxy(this);
        }

        public static Proxy Parse(string str)
        {
            str = str.Replace(';', ':');
            str = str.Replace(',', ':');
            var parts = str.Split(':');

            try
            {
                var ipStr = parts[0];
                var portStr = parts[1];
                if (string.IsNullOrEmpty(portStr))
                    portStr = 80.ToString();
                IPAddress ip;
                IPAddress.TryParse(ipStr, out ip);

                return new Proxy(new IPEndPoint(ip, int.Parse(portStr)));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
        }

        private static bool TestProxy(Proxy proxy)
        {
            Request = (HttpWebRequest) WebRequest.Create("http://google.com");
            Request.Proxy = new WebProxy(proxy.IpEndPoint.Address.ToString(), proxy.IpEndPoint.Port);
            Request.UserAgent =
                "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/39.0.2171.95 Safari/537.36";
            Request.Timeout = 2000;

            try
            {
                Request.GetResponse();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}