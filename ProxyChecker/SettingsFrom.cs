using System;
using System.Windows.Forms;

namespace ProxyChecker
{
    public partial class SettingsFrom : Form
    {
        public SettingsFrom()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tb_con.Text != null || tb_con.Text != "")
                Proxy.Request.Timeout =Convert.ToInt32(tb_con.Text);
            if (tb_limit.Text != null || tb_limit.Text != "")
                MainFrom.MaxParallelism = Convert.ToInt32(tb_con.Text);
            if (tb_user_agent.Text != null || tb_user_agent.Text != "")
                Proxy.Request.UserAgent = tb_user_agent.Text;
            
        }
    }
}