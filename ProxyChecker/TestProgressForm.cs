using System;
using System.Windows.Forms;
using System.Threading;
using System.Windows.Forms.VisualStyles;

namespace ProxyChecker
{
    public partial class TestProgressForm : Form
    {
        public TestProgressForm()
        {
            InitializeComponent();
        }

        public void SetProgress(int progress)
        {
            Invoke(new Action(() =>
            {
                progressBar.Value = progress;
                progressLabel.Text = progress + @"%";
            }));
           
        }

        private void progressBar_Click(object sender, System.EventArgs e) {

        }
    }
}