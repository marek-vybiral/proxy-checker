using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace ProxyChecker
{
    public partial class AddProxyForm : Form
    {
		  //faasdf
		
        public AddProxyForm()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
      
        }

        private void btnStorno_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        public IEnumerable<Proxy> GetData()
        {
            IList<Proxy> proxyList = new List<Proxy>();

            using (var reader = new StringReader(proxyTextBox.Text))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var p = Proxy.Parse(line);
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