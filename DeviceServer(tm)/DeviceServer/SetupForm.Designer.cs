namespace DeviceServer
{
    partial class SetupForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetupForm));
            this.LB_IP = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_ServerIP = new System.Windows.Forms.TextBox();
            this.txt_PortIP = new System.Windows.Forms.TextBox();
            this.lst_BoradCastIP = new System.Windows.Forms.ListBox();
            this.txt_NeedBoradIP = new System.Windows.Forms.TextBox();
            this.btn_Add = new System.Windows.Forms.Button();
            this.btn_del = new System.Windows.Forms.Button();
            this.btn_OK = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chk_saveRecString = new System.Windows.Forms.CheckBox();
            this.chk_autoStartprint = new System.Windows.Forms.CheckBox();
            this.chk_autoConn = new System.Windows.Forms.CheckBox();
            this.chk_autoNewline = new System.Windows.Forms.CheckBox();
            this.chk_SaveLog = new System.Windows.Forms.CheckBox();
            this.LB_Ver = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // LB_IP
            // 
            this.LB_IP.AutoSize = true;
            this.LB_IP.Location = new System.Drawing.Point(21, 30);
            this.LB_IP.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LB_IP.Name = "LB_IP";
            this.LB_IP.Size = new System.Drawing.Size(87, 14);
            this.LB_IP.TabIndex = 1;
            this.LB_IP.Text = "Server IP:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 76);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "Listen Port:";
            // 
            // txt_ServerIP
            // 
            this.txt_ServerIP.Location = new System.Drawing.Point(133, 27);
            this.txt_ServerIP.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txt_ServerIP.Name = "txt_ServerIP";
            this.txt_ServerIP.Size = new System.Drawing.Size(132, 23);
            this.txt_ServerIP.TabIndex = 3;
            // 
            // txt_PortIP
            // 
            this.txt_PortIP.Location = new System.Drawing.Point(133, 72);
            this.txt_PortIP.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txt_PortIP.Name = "txt_PortIP";
            this.txt_PortIP.Size = new System.Drawing.Size(132, 23);
            this.txt_PortIP.TabIndex = 4;
            this.txt_PortIP.Text = "9000";
            this.txt_PortIP.TextChanged += new System.EventHandler(this.txt_PortIP_TextChanged);
            // 
            // lst_BoradCastIP
            // 
            this.lst_BoradCastIP.FormattingEnabled = true;
            this.lst_BoradCastIP.ItemHeight = 14;
            this.lst_BoradCastIP.Location = new System.Drawing.Point(23, 113);
            this.lst_BoradCastIP.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.lst_BoradCastIP.Name = "lst_BoradCastIP";
            this.lst_BoradCastIP.Size = new System.Drawing.Size(376, 186);
            this.lst_BoradCastIP.TabIndex = 5;
            // 
            // txt_NeedBoradIP
            // 
            this.txt_NeedBoradIP.Location = new System.Drawing.Point(23, 307);
            this.txt_NeedBoradIP.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txt_NeedBoradIP.Name = "txt_NeedBoradIP";
            this.txt_NeedBoradIP.Size = new System.Drawing.Size(193, 23);
            this.txt_NeedBoradIP.TabIndex = 6;
            // 
            // btn_Add
            // 
            this.btn_Add.Location = new System.Drawing.Point(249, 307);
            this.btn_Add.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(100, 27);
            this.btn_Add.TabIndex = 7;
            this.btn_Add.Text = "Add";
            this.btn_Add.UseVisualStyleBackColor = true;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // btn_del
            // 
            this.btn_del.Location = new System.Drawing.Point(249, 341);
            this.btn_del.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btn_del.Name = "btn_del";
            this.btn_del.Size = new System.Drawing.Size(100, 27);
            this.btn_del.TabIndex = 8;
            this.btn_del.Text = "Delete";
            this.btn_del.UseVisualStyleBackColor = true;
            this.btn_del.Click += new System.EventHandler(this.btn_del_Click);
            // 
            // btn_OK
            // 
            this.btn_OK.Location = new System.Drawing.Point(591, 445);
            this.btn_OK.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(100, 27);
            this.btn_OK.TabIndex = 9;
            this.btn_OK.Text = "OK";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(728, 445);
            this.btn_Cancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(100, 27);
            this.btn_Cancel.TabIndex = 10;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_del);
            this.groupBox1.Controls.Add(this.btn_Add);
            this.groupBox1.Controls.Add(this.txt_NeedBoradIP);
            this.groupBox1.Controls.Add(this.lst_BoradCastIP);
            this.groupBox1.Controls.Add(this.txt_PortIP);
            this.groupBox1.Controls.Add(this.txt_ServerIP);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.LB_IP);
            this.groupBox1.Location = new System.Drawing.Point(39, 30);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(459, 404);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chk_saveRecString);
            this.groupBox2.Controls.Add(this.chk_autoStartprint);
            this.groupBox2.Controls.Add(this.chk_autoConn);
            this.groupBox2.Controls.Add(this.chk_autoNewline);
            this.groupBox2.Controls.Add(this.chk_SaveLog);
            this.groupBox2.Location = new System.Drawing.Point(532, 30);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(342, 404);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            // 
            // chk_saveRecString
            // 
            this.chk_saveRecString.AutoSize = true;
            this.chk_saveRecString.Location = new System.Drawing.Point(59, 200);
            this.chk_saveRecString.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chk_saveRecString.Name = "chk_saveRecString";
            this.chk_saveRecString.Size = new System.Drawing.Size(194, 18);
            this.chk_saveRecString.TabIndex = 5;
            this.chk_saveRecString.Text = "Save received strings";
            this.chk_saveRecString.UseVisualStyleBackColor = true;
            // 
            // chk_autoStartprint
            // 
            this.chk_autoStartprint.AutoSize = true;
            this.chk_autoStartprint.Location = new System.Drawing.Point(61, 164);
            this.chk_autoStartprint.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chk_autoStartprint.Name = "chk_autoStartprint";
            this.chk_autoStartprint.Size = new System.Drawing.Size(154, 18);
            this.chk_autoStartprint.TabIndex = 4;
            this.chk_autoStartprint.Text = "Auto start print";
            this.chk_autoStartprint.UseVisualStyleBackColor = true;
            // 
            // chk_autoConn
            // 
            this.chk_autoConn.AutoSize = true;
            this.chk_autoConn.Location = new System.Drawing.Point(59, 125);
            this.chk_autoConn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chk_autoConn.Name = "chk_autoConn";
            this.chk_autoConn.Size = new System.Drawing.Size(210, 18);
            this.chk_autoConn.TabIndex = 3;
            this.chk_autoConn.Text = "Auto connect to printer";
            this.chk_autoConn.UseVisualStyleBackColor = true;
            // 
            // chk_autoNewline
            // 
            this.chk_autoNewline.AutoSize = true;
            this.chk_autoNewline.Location = new System.Drawing.Point(61, 86);
            this.chk_autoNewline.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chk_autoNewline.Name = "chk_autoNewline";
            this.chk_autoNewline.Size = new System.Drawing.Size(218, 18);
            this.chk_autoNewline.TabIndex = 2;
            this.chk_autoNewline.Text = "Auto create product line";
            this.chk_autoNewline.UseVisualStyleBackColor = true;
            // 
            // chk_SaveLog
            // 
            this.chk_SaveLog.AutoSize = true;
            this.chk_SaveLog.Location = new System.Drawing.Point(61, 45);
            this.chk_SaveLog.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chk_SaveLog.Name = "chk_SaveLog";
            this.chk_SaveLog.Size = new System.Drawing.Size(98, 18);
            this.chk_SaveLog.TabIndex = 1;
            this.chk_SaveLog.Text = "Save logs";
            this.chk_SaveLog.UseVisualStyleBackColor = true;
            // 
            // LB_Ver
            // 
            this.LB_Ver.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LB_Ver.AutoSize = true;
            this.LB_Ver.Location = new System.Drawing.Point(747, 9);
            this.LB_Ver.Name = "LB_Ver";
            this.LB_Ver.Size = new System.Drawing.Size(0, 14);
            this.LB_Ver.TabIndex = 13;
            // 
            // SetupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(894, 484);
            this.Controls.Add(this.LB_Ver);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_OK);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "SetupForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Setup";
            this.Load += new System.EventHandler(this.SetupForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LB_IP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_ServerIP;
        private System.Windows.Forms.TextBox txt_PortIP;
        private System.Windows.Forms.ListBox lst_BoradCastIP;
        private System.Windows.Forms.TextBox txt_NeedBoradIP;
        private System.Windows.Forms.Button btn_Add;
        private System.Windows.Forms.Button btn_del;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chk_SaveLog;
        private System.Windows.Forms.CheckBox chk_saveRecString;
        private System.Windows.Forms.CheckBox chk_autoStartprint;
        private System.Windows.Forms.CheckBox chk_autoConn;
        private System.Windows.Forms.CheckBox chk_autoNewline;
        private System.Windows.Forms.Label LB_Ver;
    }
}