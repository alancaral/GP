namespace DeviceServer
{
    partial class frmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.lstBox_Printers = new System.Windows.Forms.ListBox();
            this.btn_Refresh = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_RenameLine = new System.Windows.Forms.Button();
            this.btn_DelLine = new System.Windows.Forms.Button();
            this.btn_addLine = new System.Windows.Forms.Button();
            this.MainTables = new System.Windows.Forms.TabControl();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_Setup = new System.Windows.Forms.Button();
            this.btn_Assign = new System.Windows.Forms.Button();
            this.pnl_wait = new System.Windows.Forms.Panel();
            this.LB_waitNotice = new System.Windows.Forms.Label();
            this.LB_wait = new System.Windows.Forms.Label();
            this.LB_Ver = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.pnl_wait.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstBox_Printers
            // 
            this.lstBox_Printers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstBox_Printers.FormattingEnabled = true;
            this.lstBox_Printers.ItemHeight = 14;
            this.lstBox_Printers.Location = new System.Drawing.Point(10, 91);
            this.lstBox_Printers.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.lstBox_Printers.Name = "lstBox_Printers";
            this.lstBox_Printers.Size = new System.Drawing.Size(261, 410);
            this.lstBox_Printers.TabIndex = 0;
            // 
            // btn_Refresh
            // 
            this.btn_Refresh.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Refresh.Location = new System.Drawing.Point(36, 23);
            this.btn_Refresh.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btn_Refresh.Name = "btn_Refresh";
            this.btn_Refresh.Size = new System.Drawing.Size(210, 49);
            this.btn_Refresh.TabIndex = 1;
            this.btn_Refresh.Text = "Refresh";
            this.btn_Refresh.UseVisualStyleBackColor = true;
            this.btn_Refresh.Click += new System.EventHandler(this.btn_Refresh_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.MainTables);
            this.groupBox1.Location = new System.Drawing.Point(298, 29);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Size = new System.Drawing.Size(955, 716);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_RenameLine);
            this.panel1.Controls.Add(this.btn_DelLine);
            this.panel1.Controls.Add(this.btn_addLine);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(4, 596);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(947, 117);
            this.panel1.TabIndex = 2;
            // 
            // btn_RenameLine
            // 
            this.btn_RenameLine.Location = new System.Drawing.Point(382, 35);
            this.btn_RenameLine.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btn_RenameLine.Name = "btn_RenameLine";
            this.btn_RenameLine.Size = new System.Drawing.Size(144, 49);
            this.btn_RenameLine.TabIndex = 3;
            this.btn_RenameLine.Text = "Rename";
            this.btn_RenameLine.UseVisualStyleBackColor = true;
            this.btn_RenameLine.Click += new System.EventHandler(this.btn_RenameLine_Click);
            // 
            // btn_DelLine
            // 
            this.btn_DelLine.Location = new System.Drawing.Point(208, 35);
            this.btn_DelLine.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btn_DelLine.Name = "btn_DelLine";
            this.btn_DelLine.Size = new System.Drawing.Size(144, 49);
            this.btn_DelLine.TabIndex = 2;
            this.btn_DelLine.Text = "Delete";
            this.btn_DelLine.UseVisualStyleBackColor = true;
            this.btn_DelLine.Click += new System.EventHandler(this.btn_DelLine_Click);
            // 
            // btn_addLine
            // 
            this.btn_addLine.Location = new System.Drawing.Point(40, 35);
            this.btn_addLine.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btn_addLine.Name = "btn_addLine";
            this.btn_addLine.Size = new System.Drawing.Size(144, 49);
            this.btn_addLine.TabIndex = 1;
            this.btn_addLine.Text = "New";
            this.btn_addLine.UseVisualStyleBackColor = true;
            this.btn_addLine.Click += new System.EventHandler(this.btn_addLine_Click);
            // 
            // MainTables
            // 
            this.MainTables.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.MainTables.Location = new System.Drawing.Point(19, 31);
            this.MainTables.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MainTables.Name = "MainTables";
            this.MainTables.SelectedIndex = 0;
            this.MainTables.Size = new System.Drawing.Size(916, 558);
            this.MainTables.TabIndex = 0;
            this.MainTables.SelectedIndexChanged += new System.EventHandler(this.MainTables_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btn_Setup);
            this.groupBox2.Controls.Add(this.btn_Assign);
            this.groupBox2.Controls.Add(this.btn_Refresh);
            this.groupBox2.Controls.Add(this.lstBox_Printers);
            this.groupBox2.Location = new System.Drawing.Point(16, 29);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox2.Size = new System.Drawing.Size(281, 713);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            // 
            // btn_Setup
            // 
            this.btn_Setup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Setup.Location = new System.Drawing.Point(36, 611);
            this.btn_Setup.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btn_Setup.Name = "btn_Setup";
            this.btn_Setup.Size = new System.Drawing.Size(202, 57);
            this.btn_Setup.TabIndex = 3;
            this.btn_Setup.Text = "Setup";
            this.btn_Setup.UseVisualStyleBackColor = true;
            this.btn_Setup.Click += new System.EventHandler(this.btn_Setup_Click);
            // 
            // btn_Assign
            // 
            this.btn_Assign.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Assign.Location = new System.Drawing.Point(36, 520);
            this.btn_Assign.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btn_Assign.Name = "btn_Assign";
            this.btn_Assign.Size = new System.Drawing.Size(202, 57);
            this.btn_Assign.TabIndex = 2;
            this.btn_Assign.Text = "Assign To";
            this.btn_Assign.UseVisualStyleBackColor = true;
            this.btn_Assign.Click += new System.EventHandler(this.btn_Assign_Click);
            // 
            // pnl_wait
            // 
            this.pnl_wait.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_wait.Controls.Add(this.LB_waitNotice);
            this.pnl_wait.Controls.Add(this.LB_wait);
            this.pnl_wait.Location = new System.Drawing.Point(239, 235);
            this.pnl_wait.Name = "pnl_wait";
            this.pnl_wait.Size = new System.Drawing.Size(601, 275);
            this.pnl_wait.TabIndex = 4;
            this.pnl_wait.Visible = false;
            // 
            // LB_waitNotice
            // 
            this.LB_waitNotice.AutoSize = true;
            this.LB_waitNotice.Font = new System.Drawing.Font("宋体", 38.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LB_waitNotice.Location = new System.Drawing.Point(83, 73);
            this.LB_waitNotice.Name = "LB_waitNotice";
            this.LB_waitNotice.Size = new System.Drawing.Size(346, 51);
            this.LB_waitNotice.TabIndex = 1;
            this.LB_waitNotice.Text = "Scan devices";
            // 
            // LB_wait
            // 
            this.LB_wait.AutoSize = true;
            this.LB_wait.Font = new System.Drawing.Font("宋体", 38F, System.Drawing.FontStyle.Bold);
            this.LB_wait.Location = new System.Drawing.Point(83, 154);
            this.LB_wait.Name = "LB_wait";
            this.LB_wait.Size = new System.Drawing.Size(157, 51);
            this.LB_wait.TabIndex = 0;
            this.LB_wait.Text = "Wait.";
            // 
            // LB_Ver
            // 
            this.LB_Ver.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LB_Ver.AutoSize = true;
            this.LB_Ver.Location = new System.Drawing.Point(1093, 12);
            this.LB_Ver.Name = "LB_Ver";
            this.LB_Ver.Size = new System.Drawing.Size(0, 14);
            this.LB_Ver.TabIndex = 5;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1276, 759);
            this.Controls.Add(this.LB_Ver);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pnl_wait);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DeviceServer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.pnl_wait.ResumeLayout(false);
            this.pnl_wait.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstBox_Printers;
        private System.Windows.Forms.Button btn_Refresh;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_addLine;
        private System.Windows.Forms.TabControl MainTables;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_Setup;
        private System.Windows.Forms.Button btn_Assign;
        private System.Windows.Forms.Button btn_RenameLine;
        private System.Windows.Forms.Button btn_DelLine;
        private System.Windows.Forms.Panel pnl_wait;
        private System.Windows.Forms.Label LB_waitNotice;
        private System.Windows.Forms.Label LB_wait;
        private System.Windows.Forms.Label LB_Ver;
    }
}

