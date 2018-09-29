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
    public partial class SetupForm : Form
    {
        public SetupForm()
        {
            InitializeComponent();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {

            SaveSetting();
            this.DialogResult = DialogResult.OK;
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void txt_PortIP_TextChanged(object sender, EventArgs e)
        {

        }

        public void SaveSetting()
        {
            string str = System.Windows.Forms.Application.StartupPath;
            str += "\\DeviceServer.ini";

            string strtemp;
            cIniFile.WriteIniKeys("Base", "ClinetCount", lst_BoradCastIP.Items.Count.ToString(),str);
            for (int i = 0; i < lst_BoradCastIP.Items.Count; i++)
            {
                strtemp = "ClientIP" + i.ToString();
                cIniFile.WriteIniKeys("Base", strtemp, lst_BoradCastIP.Items[i].ToString(), str);
            }

            cIniFile.WriteIniKeys("Base", "ServerIP", txt_ServerIP.Text ,str);
            cIniFile.WriteIniKeys("Base", "ListenPort", txt_PortIP.Text, str);
            string sval = "";
            if (chk_SaveLog.Checked)
                sval = "1";
            else 
                sval = "0";
            cIniFile.WriteIniKeys("Base", "SaveLogs", sval,str);

            if (chk_autoNewline.Checked)
                sval = "1";
            else
                sval = "0";
            cIniFile.WriteIniKeys("Base", "AutoPL", sval, str);

            if (chk_autoConn.Checked)
                sval = "1";
            else
                sval = "0";
            cIniFile.WriteIniKeys("Base", "AutoConnect", sval, str);

            if (chk_autoStartprint.Checked)
                sval = "1";
            else
                sval = "0";
            cIniFile.WriteIniKeys("Base", "AutoPrint", sval, str);

            if (chk_saveRecString.Checked)
                sval = "1";
            else
                sval = "0";
            cIniFile.WriteIniKeys("Base", "SaveStrings", sval, str);
        }
        public void ReadSetting()
        {
            string str = System.Windows.Forms.Application.StartupPath;
            str += "\\DeviceServer.ini";
            String sval = cIniFile.ReadIniKeys("Base", "ClinetCount", str);
            int icount = 0;
            if (sval.Length > 0)
            {
                icount = int.Parse(sval);
            }
            String strtemp;
            int ClientCount = 0;
            if (icount < 1)
            {
                ClientCount = 0; 
            }

            ClientCount = icount;
            if (ClientCount > 0)
            {
                lst_BoradCastIP.Items.Clear();
                for (int i = 0; i < icount; i++)
                {
                    strtemp = "ClientIP" + i.ToString();
                    sval = cIniFile.ReadIniKeys("Base", strtemp, str);
                    //m_pIPClient[i] = Marshal.StringToHGlobalAnsi(sval);
                    lst_BoradCastIP.Items.Add( sval);
                }
            }
           

            txt_ServerIP.Text = cIniFile.ReadIniKeys("Base", "ServerIP", str);
            txt_PortIP.Text = cIniFile.ReadIniKeys("Base", "ListenPort", str);
            string val = cIniFile.ReadIniKeys("Base","SaveLogs",str);
            if (val.Length == 0) val = "1";
            Int32 ival = int.Parse(val);
            if (ival == 0)
                chk_SaveLog.Checked = false;
            else
                chk_SaveLog.Checked = true;

            val = cIniFile.ReadIniKeys("Base","AutoPL",str);
            if (val.Length == 0) val = "1";
            if (val == "1")
                chk_autoNewline.Checked = true;
            else
                chk_autoNewline.Checked = false;

            val = cIniFile.ReadIniKeys("Base", "AutoConnect", str);
            if (val.Length == 0) val = "0";
            if (val == "1")
                chk_autoConn.Checked = true;
            else
                chk_autoConn.Checked = false;

            val = cIniFile.ReadIniKeys("Base", "AutoPrint", str);
            if (val.Length == 0) val = "0";
            if (val == "1")
                chk_autoStartprint.Checked = true;
            else
                chk_autoStartprint.Checked = false;

            val = cIniFile.ReadIniKeys("Base", "SaveStrings", str);
            if (val.Length == 0) val = "1";
            if (val == "1")
                chk_saveRecString.Checked = true;
            else
                chk_saveRecString.Checked = false;
        }

        private void SetupForm_Load(object sender, EventArgs e)
        {
            ReadSetting();
            //LB_Ver.Text = "Version:" + Application.ProductVersion.ToString() ;
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            lst_BoradCastIP.Items.Add(txt_NeedBoradIP.Text);
        }

        private void btn_del_Click(object sender, EventArgs e)
        {
            lst_BoradCastIP.Items.RemoveAt(lst_BoradCastIP.SelectedIndex);
        }


    }
}
