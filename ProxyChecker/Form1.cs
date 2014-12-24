using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProxyChecker
{
    public partial class Form1 : Form
    {
        ProxyDataModel proxyDataModel = new ProxyDataModel();

        public Form1()
        {
            InitializeComponent();

            this.ProxyDataGridView.DataSource = this.proxyDataModel.Table;
            this.proxyDataModel.DataChanged += (this.UpdateTable);
        }

        public void UpdateTable()
        {
            this.Invoke((MethodInvoker)delegate { this.proxyDataModel.UpdateTable(); });
        }

        private void btnAddProxy_Click(object sender, EventArgs e)
        {
            AddProxyForm form = new AddProxyForm();
            form.ShowDialog();
            if (form.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                this.proxyDataModel.AddProxyList(form.GetData());
            }
        }

        private void btnStartTest_Click(object sender, EventArgs e)
        {
            IList<Proxy> proxyList = this.proxyDataModel.ProxyList;
            this.testProxyList(proxyList, ref this.proxyDataModel);
        }

        private void testProxyList(IList<Proxy> proxyList, ref ProxyDataModel proxyDataModel)
        {
            Parallel.ForEach(proxyList, new ParallelOptions() { MaxDegreeOfParallelism = 32 }, proxy =>
            {
                proxy.PerformTest();
            });

            proxyDataModel.UpdateTable();
        }
    }
}
