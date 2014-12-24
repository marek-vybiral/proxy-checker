using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProxyChecker
{
    public partial class AddProxyForm : Form
    {
        public AddProxyForm()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnStorno_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        public IList<Proxy> GetData()
        {
            IList<Proxy> proxyList = new List<Proxy>();

            using (StringReader reader = new StringReader(this.proxyTextBox.Text))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    Proxy p = Proxy.Parse(line);
                    if (p != null)
                    {
                        proxyList.Add(p);
                    }                    
                }
            }

            return proxyList;
        }
    }
}
