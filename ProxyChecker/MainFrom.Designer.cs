namespace ProxyChecker
{
    partial class MainFrom
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
            this.button1 = new System.Windows.Forms.Button();
            this.butSettings = new System.Windows.Forms.Button();
            this.textBoxLog = new System.Windows.Forms.RichTextBox();
            this.btnRemoveAll = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ProxyDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // ProxyDataGridView
            // 
            this.ProxyDataGridView.AllowUserToAddRows = false;
            this.ProxyDataGridView.AllowUserToDeleteRows = false;
            this.ProxyDataGridView.AllowUserToResizeColumns = false;
            this.ProxyDataGridView.AllowUserToResizeRows = false;
            this.ProxyDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProxyDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.ProxyDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.ProxyDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ProxyDataGridView.Location = new System.Drawing.Point(12, 12);
            this.ProxyDataGridView.Name = "ProxyDataGridView";
            this.ProxyDataGridView.ReadOnly = true;
            this.ProxyDataGridView.Size = new System.Drawing.Size(377, 477);
            this.ProxyDataGridView.TabIndex = 0;
            // 
            // btnAddProxy
            // 
            this.btnAddProxy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddProxy.Location = new System.Drawing.Point(397, 12);
            this.btnAddProxy.Name = "btnAddProxy";
            this.btnAddProxy.Size = new System.Drawing.Size(75, 23);
            this.btnAddProxy.TabIndex = 1;
            this.btnAddProxy.Text = "Add";
            this.btnAddProxy.UseVisualStyleBackColor = true;
            this.btnAddProxy.Click += new System.EventHandler(this.btnAddProxy_Click);
            // 
            // btnStartTest
            // 
            this.btnStartTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStartTest.Location = new System.Drawing.Point(397, 545);
            this.btnStartTest.Name = "btnStartTest";
            this.btnStartTest.Size = new System.Drawing.Size(75, 23);
            this.btnStartTest.TabIndex = 2;
            this.btnStartTest.Text = "Start Test";
            this.btnStartTest.UseVisualStyleBackColor = true;
            this.btnStartTest.Click += new System.EventHandler(this.btnStartTest_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(397, 574);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Export";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // butSettings
            // 
            this.butSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butSettings.Location = new System.Drawing.Point(397, 70);
            this.butSettings.Name = "butSettings";
            this.butSettings.Size = new System.Drawing.Size(75, 23);
            this.butSettings.TabIndex = 4;
            this.butSettings.Text = "Settings";
            this.butSettings.UseVisualStyleBackColor = true;
            // 
            // textBoxLog
            // 
            this.textBoxLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxLog.Location = new System.Drawing.Point(12, 495);
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.Size = new System.Drawing.Size(377, 102);
            this.textBoxLog.TabIndex = 5;
            this.textBoxLog.Text = "";
            // 
            // btnRemoveAll
            // 
            this.btnRemoveAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveAll.Location = new System.Drawing.Point(397, 41);
            this.btnRemoveAll.Name = "btnRemoveAll";
            this.btnRemoveAll.Size = new System.Drawing.Size(75, 23);
            this.btnRemoveAll.TabIndex = 6;
            this.btnRemoveAll.Text = "Clear";
            this.btnRemoveAll.UseVisualStyleBackColor = true;
            this.btnRemoveAll.Click += new System.EventHandler(this.btnRemoveAll_Click);
            // 
            // MainFrom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 609);
            this.Controls.Add(this.btnRemoveAll);
            this.Controls.Add(this.textBoxLog);
            this.Controls.Add(this.butSettings);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnStartTest);
            this.Controls.Add(this.btnAddProxy);
            this.Controls.Add(this.ProxyDataGridView);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(500, 300);
            this.Name = "MainFrom";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ProxyCehck";
            ((System.ComponentModel.ISupportInitialize)(this.ProxyDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView ProxyDataGridView;
        private System.Windows.Forms.Button btnAddProxy;
        private System.Windows.Forms.Button btnStartTest;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button butSettings;
        private System.Windows.Forms.RichTextBox textBoxLog;
        private System.Windows.Forms.Button btnRemoveAll;
    }
}

