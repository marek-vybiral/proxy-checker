using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            this.progressBar.Value = progress;
            this.progressLabel.Text = progress.ToString() + "%";
        }
    }
}
