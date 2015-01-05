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
    public partial class MainFrom : Form
    {
        ProxyDataModel proxyDataModel = new ProxyDataModel();

        TestProgressForm testProgressForm = null;

        public MainFrom()
        {
            InitializeComponent();

            this.ProxyDataGridView.DataSource = this.proxyDataModel.Table;
        }

        public void Log(string message)
        {
            this.textBoxLog.Text = message + "\n" + this.textBoxLog.Text;
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
            this.testProgressForm = new TestProgressForm();
            this.testProgressForm.Show();
            this.testProxyList();
        }

        private void testProxyList()
        {
            int proxyNum = this.proxyDataModel.ProxyList.Count;
            int proxyTested = 0;

            this.Enabled = false;
            Task.Factory.StartNew(() =>
            {
                Parallel.ForEach(this.proxyDataModel.ProxyList, new ParallelOptions() { MaxDegreeOfParallelism = 32 }, proxy =>
                {
                    proxy.PerformTest();
                    ++proxyTested;
                    this.BeginInvoke((MethodInvoker)delegate {
                        this.updateTestProgress(Convert.ToInt16(proxyTested * 100.0 / proxyNum));
                    });
                });

                this.BeginInvoke((MethodInvoker)delegate {
                    this.testProgressForm.Close();
                    this.testProgressForm = null;
                    this.Enabled = true;
                });                
            });
        }

        private void btnRemoveAll_Click(object sender, EventArgs e)
        {
            this.proxyDataModel.RemoveAll();
        }

        private void updateTestProgress(int progress)
        {
            this.Log(progress.ToString());
            this.testProgressForm.SetProgress(progress);
            this.proxyDataModel.UpdateTable();
        }
    }
}
