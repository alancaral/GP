namespace DeviceServer
{
    partial class WaitForm
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
            this.LB_Notice = new System.Windows.Forms.Label();
            this.LB_wait = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // LB_Notice
            // 
            this.LB_Notice.AutoSize = true;
            this.LB_Notice.Location = new System.Drawing.Point(95, 64);
            this.LB_Notice.Name = "LB_Notice";
            this.LB_Notice.Size = new System.Drawing.Size(245, 37);
            this.LB_Notice.TabIndex = 0;
            this.LB_Notice.Text = "Scan devices";
            // 
            // LB_wait
            // 
            this.LB_wait.AutoSize = true;
            this.LB_wait.Location = new System.Drawing.Point(97, 138);
            this.LB_wait.Name = "LB_wait";
            this.LB_wait.Size = new System.Drawing.Size(131, 37);
            this.LB_wait.TabIndex = 1;
            this.LB_wait.Text = "Wait..";
            // 
            // WaitForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(19F, 37F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(636, 249);
            this.ControlBox = false;
            this.Controls.Add(this.LB_wait);
            this.Controls.Add(this.LB_Notice);
            this.Font = new System.Drawing.Font("宋体", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(10, 9, 10, 9);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WaitForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LB_Notice;
        private System.Windows.Forms.Label LB_wait;

        public void setNotice(string str)
        {
            LB_Notice.Text = str; 
        }
    }
}