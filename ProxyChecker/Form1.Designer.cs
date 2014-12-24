namespace ProxyChecker
{
    partial class Form1
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
            this.ProxyDataGridView = new System.Windows.Forms.DataGridView();
            this.btnAddProxy = new System.Windows.Forms.Button();
            this.btnStartTest = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ProxyDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // ProxyDataGridView
            // 
            this.ProxyDataGridView.AllowUserToAddRows = false;
            this.ProxyDataGridView.AllowUserToDeleteRows = false;
            this.ProxyDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ProxyDataGridView.Location = new System.Drawing.Point(12, 12);
            this.ProxyDataGridView.Name = "ProxyDataGridView";
            this.ProxyDataGridView.ReadOnly = true;
            this.ProxyDataGridView.Size = new System.Drawing.Size(377, 374);
            this.ProxyDataGridView.TabIndex = 0;
            // 
            // btnAddProxy
            // 
            this.btnAddProxy.Location = new System.Drawing.Point(233, 392);
            this.btnAddProxy.Name = "btnAddProxy";
            this.btnAddProxy.Size = new System.Drawing.Size(75, 23);
            this.btnAddProxy.TabIndex = 1;
            this.btnAddProxy.Text = "Add Proxy";
            this.btnAddProxy.UseVisualStyleBackColor = true;
            this.btnAddProxy.Click += new System.EventHandler(this.btnAddProxy_Click);
            // 
            // btnStartTest
            // 
            this.btnStartTest.Location = new System.Drawing.Point(314, 392);
            this.btnStartTest.Name = "btnStartTest";
            this.btnStartTest.Size = new System.Drawing.Size(75, 23);
            this.btnStartTest.TabIndex = 2;
            this.btnStartTest.Text = "Start Test";
            this.btnStartTest.UseVisualStyleBackColor = true;
            this.btnStartTest.Click += new System.EventHandler(this.btnStartTest_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 424);
            this.Controls.Add(this.btnStartTest);
            this.Controls.Add(this.btnAddProxy);
            this.Controls.Add(this.ProxyDataGridView);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ProxyCehck";
            ((System.ComponentModel.ISupportInitialize)(this.ProxyDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView ProxyDataGridView;
        private System.Windows.Forms.Button btnAddProxy;
        private System.Windows.Forms.Button btnStartTest;
    }
}

