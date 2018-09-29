using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DeviceServer
{
    public partial class InputBox : Form
    {
        public InputBox()
        {
            InitializeComponent();
        }

        private bool bOK = false;

        public void setContext(string title,string defaultText)
        {
            this.LB_Notice.Text = title;
            this.textBox1.Text = defaultText;
            
        }

        public string getInputValue()
        {
            if (bOK)
                return this.textBox1.Text;
            else
                return "";
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            bOK = true;
            this.Close();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            bOK = false;
            this.Close();
        }
    }
}
