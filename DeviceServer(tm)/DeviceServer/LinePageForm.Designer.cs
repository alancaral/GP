namespace DeviceServer
{
    partial class LinePageForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LinePageForm));
            this.grpbox_main = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rTxtbox_Send = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_PrintONOFF = new System.Windows.Forms.Button();
            this.btn_RefreshMsgNames = new System.Windows.Forms.Button();
            this.cbobox_MsgList = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_Connect = new System.Windows.Forms.Button();
            this.LB_IPSN = new System.Windows.Forms.Label();
            this.tmr_CheckConn = new System.Windows.Forms.Timer(this.components);
            this.tmr_Reconn = new System.Windows.Forms.Timer(this.components);
            this.grpbox_main.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpbox_main
            // 
            this.grpbox_main.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpbox_main.Controls.Add(this.panel2);
            this.grpbox_main.Controls.Add(this.panel1);
            this.grpbox_main.Location = new System.Drawing.Point(16, 14);
            this.grpbox_main.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.grpbox_main.Name = "grpbox_main";
            this.grpbox_main.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.grpbox_main.Size = new System.Drawing.Size(800, 485);
            this.grpbox_main.TabIndex = 0;
            this.grpbox_main.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.rTxtbox_Send);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(4, 135);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(792, 347);
            this.panel2.TabIndex = 1;
            // 
            // rTxtbox_Send
            // 
            this.rTxtbox_Send.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rTxtbox_Send.Location = new System.Drawing.Point(0, 0);
            this.rTxtbox_Send.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.rTxtbox_Send.Name = "rTxtbox_Send";
            this.rTxtbox_Send.Size = new System.Drawing.Size(790, 345);
            this.rTxtbox_Send.TabIndex = 0;
            this.rTxtbox_Send.Text = "";
            this.rTxtbox_Send.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btn_PrintONOFF);
            this.panel1.Controls.Add(this.btn_RefreshMsgNames);
            this.panel1.Controls.Add(this.cbobox_MsgList);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btn_Connect);
            this.panel1.Controls.Add(this.LB_IPSN);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(4, 19);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(792, 116);
            this.panel1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(371, 31);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 28);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 14);
            this.label1.TabIndex = 6;
            this.label1.Text = "Printer:";
            // 
            // btn_PrintONOFF
            // 
            this.btn_PrintONOFF.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_PrintONOFF.Location = new System.Drawing.Point(673, 73);
            this.btn_PrintONOFF.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btn_PrintONOFF.Name = "btn_PrintONOFF";
            this.btn_PrintONOFF.Size = new System.Drawing.Size(100, 27);
            this.btn_PrintONOFF.TabIndex = 5;
            this.btn_PrintONOFF.Text = "Print";
            this.btn_PrintONOFF.UseVisualStyleBackColor = true;
            this.btn_PrintONOFF.Click += new System.EventHandler(this.btn_PrintONOFF_Click);
            // 
            // btn_RefreshMsgNames
            // 
            this.btn_RefreshMsgNames.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_RefreshMsgNames.Location = new System.Drawing.Point(524, 73);
            this.btn_RefreshMsgNames.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btn_RefreshMsgNames.Name = "btn_RefreshMsgNames";
            this.btn_RefreshMsgNames.Size = new System.Drawing.Size(141, 27);
            this.btn_RefreshMsgNames.TabIndex = 4;
            this.btn_RefreshMsgNames.Text = "Refresh Message";
            this.btn_RefreshMsgNames.UseVisualStyleBackColor = true;
            this.btn_RefreshMsgNames.Click += new System.EventHandler(this.btn_RefreshMsgNames_Click);
            // 
            // cbobox_MsgList
            // 
            this.cbobox_MsgList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbobox_MsgList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbobox_MsgList.FormattingEnabled = true;
            this.cbobox_MsgList.Location = new System.Drawing.Point(128, 76);
            this.cbobox_MsgList.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cbobox_MsgList.Name = "cbobox_MsgList";
            this.cbobox_MsgList.Size = new System.Drawing.Size(372, 22);
            this.cbobox_MsgList.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(45, 79);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "Message:";
            // 
            // btn_Connect
            // 
            this.btn_Connect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Connect.Location = new System.Drawing.Point(524, 17);
            this.btn_Connect.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btn_Connect.Name = "btn_Connect";
            this.btn_Connect.Size = new System.Drawing.Size(249, 37);
            this.btn_Connect.TabIndex = 1;
            this.btn_Connect.Text = "Connect";
            this.btn_Connect.UseVisualStyleBackColor = true;
            this.btn_Connect.Click += new System.EventHandler(this.btn_Connect_Click);
            // 
            // LB_IPSN
            // 
            this.LB_IPSN.AutoSize = true;
            this.LB_IPSN.Location = new System.Drawing.Point(125, 28);
            this.LB_IPSN.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LB_IPSN.Name = "LB_IPSN";
            this.LB_IPSN.Size = new System.Drawing.Size(0, 14);
            this.LB_IPSN.TabIndex = 0;
            // 
            // tmr_CheckConn
            // 
            this.tmr_CheckConn.Interval = 2000;
            this.tmr_CheckConn.Tag = "0";
            this.tmr_CheckConn.Tick += new System.EventHandler(this.tmr_CheckConn_Tick);
            // 
            // tmr_Reconn
            // 
            this.tmr_Reconn.Interval = 2000;
            this.tmr_Reconn.Tag = "0";
            this.tmr_Reconn.Tick += new System.EventHandler(this.tmr_Reconn_Tick);
            // 
            // LinePageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(832, 513);
            this.Controls.Add(this.grpbox_main);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "LinePageForm";
            this.Text = "LinePageForm";
            this.Load += new System.EventHandler(this.LinePageForm_Load);
            this.grpbox_main.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpbox_main;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_PrintONOFF;
        private System.Windows.Forms.Button btn_RefreshMsgNames;
        private System.Windows.Forms.ComboBox cbobox_MsgList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_Connect;
        private System.Windows.Forms.Label LB_IPSN;
        private System.Windows.Forms.RichTextBox rTxtbox_Send;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Timer tmr_CheckConn;
        private System.Windows.Forms.Timer tmr_Reconn;
    }
}