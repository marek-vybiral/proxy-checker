namespace ProxyChecker
{
    partial class AddProxyForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnOk = new System.Windows.Forms.Button();
            this.btnStorno = new System.Windows.Forms.Button();
            this.proxyTextBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(399, 381);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "Add";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnStorno
            // 
            this.btnStorno.Location = new System.Drawing.Point(318, 381);
            this.btnStorno.Name = "btnStorno";
            this.btnStorno.Size = new System.Drawing.Size(75, 23);
            this.btnStorno.TabIndex = 2;
            this.btnStorno.Text = "Storno";
            this.btnStorno.UseVisualStyleBackColor = true;
            this.btnStorno.Click += new System.EventHandler(this.btnStorno_Click);
            // 
            // proxyTextBox
            // 
            this.proxyTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.proxyTextBox.Location = new System.Drawing.Point(12, 12);
            this.proxyTextBox.Name = "proxyTextBox";
            this.proxyTextBox.Size = new System.Drawing.Size(462, 363);
            this.proxyTextBox.TabIndex = 3;
            this.proxyTextBox.Text = "";
            // 
            // AddProxyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(486, 414);
            this.Controls.Add(this.proxyTextBox);
            this.Controls.Add(this.btnStorno);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "AddProxyForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Proxy";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnStorno;
        private System.Windows.Forms.RichTextBox proxyTextBox;
    }
}