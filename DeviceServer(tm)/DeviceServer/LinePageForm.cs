using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace DeviceServer
{
    public partial class LinePageForm : Form
    {
        public string LineName = "";
        public string sIPSN = "";
        private string strIP = "";
        private string strSN = "";
        private uint iIP;
        private uint iSN;
        public frmMain mainForm;
        private int ipPrinter;
        private string lastPrintedMsgName = "";

        public bool bNeedAutoConnect=false;
        public bool bNeedAutoPrint = false;
        //-------------------------------
        //private CTCP tcp;
        private CPrinter m_Printer = new CPrinter();

        public LinePageForm()
        {
            InitializeComponent();
        }

        public void setIPstr(string str)
        {
            sIPSN = str;
            LB_IPSN.Text = str;

            splitIPSN(sIPSN);
        }
        public void setLastPrintedMsgName(string sName)
        {
            lastPrintedMsgName = sName;
        }

        public string getLastPrintedMsgName()
        {
            return lastPrintedMsgName;
        }

        public void setIP(string str)
        {
            strIP = str;
            if (strIP.Length > 0 && strSN.Length > 0)
            {
                setIPstr(mergeIPSN(strIP, strSN));
            }
        }
        public string getIP()
        {
            return strIP;
        }

        public string getSN()
        {
            return strSN;
        }

        public void setSN(string str)
        {
            strSN = str;
            if (strIP.Length > 0 && strSN.Length > 0)
            {
                setIPstr(mergeIPSN(strIP, strSN));
            }
        }
        

        private string mergeIPSN(string sIP, string sSN)
        {
            string strIPSN = sIP + "-" + sSN ;
            return strIPSN;
        }

        public void splitIPSN(string str)
        {
            string[] strArray = str.Split(new Char[] { '-' });
            strSN = strArray[1];//.Substring(1, strArray[0].Length - 2);
            strIP = strArray[0];//.Substring(1, strArray[1].Length - 2);

            iSN = uint.Parse(strSN);
            iIP = mainForm.IP2Int(strIP);

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_Connect_Click(object sender, EventArgs e)
        {
           // Application.DoEvents();
           // SetClient(ipPrinter, System.Text.Encoding.Default.GetBytes(mainForm.m_pIPClient), mainForm.m_iClientCount);

            bool bStatus = m_Printer.GetConnected();//GetConnected(ipPrinter);
            if (bStatus)
            {
                if (m_Printer.StopConnect() == 0)// StopConnect(ipPrinter) != 0)
                {
                    MessageBox.Show("Disconnect to printer failed!");
                    CLog.SaveLog("Line[" + LineName + "] disconnect error!");
                }
                else
                {
                    tmr_CheckConn.Enabled = false;
                    btn_Connect.Text = "Connect";
                    CLog.SaveLog("Line[" + LineName + "] disconnected.");

                }
            }
            else
            {
                
                m_Printer.StartConnect(strIP,(Int32)iSN);
                //StartConnect(ipPrinter, iSN, iIP) ;
                if (!m_Printer.GetConnected())//!GetConnected(ipPrinter))
                {
                    MessageBox.Show("Connect to printer failed!");
                    CLog.SaveLog("Line[" + LineName + "] connect error!");
                }
                else
                {
                    btn_Connect.Text = "Disconnect";
                    CLog.SaveLog("Line[" + LineName + "] connected.");

                    btn_RefreshMsgNames_Click(sender,e);

                    bool bPrintStatus = m_Printer.GetIsPrinting(); //GetPrinted(ipPrinter);
                    if (!bPrintStatus)
                    {
                        btn_RefreshMsgNames.Enabled = true;
                        btn_PrintONOFF.Text = "Print";
                    }
                    else
                    {
                        btn_RefreshMsgNames.Enabled = false;
                        btn_PrintONOFF.Text = "Stop";
                    }
                    tmr_CheckConn.Enabled = true;
                }
            }
                
            
        }

        private void LinePageForm_Load(object sender, EventArgs e)
        {
          
           // ipPrinter = CreatePrinter();
    
        }

        public void DoAuto()
        {
            if (bNeedAutoConnect)
            {
                if (iSN > 0 && iIP > 0)
                    btn_Connect.PerformClick();

            }
            if (bNeedAutoPrint)
            {
                if (lastPrintedMsgName.Length > 0)
                {
                    btn_PrintONOFF.PerformClick();
                }
            }
        }

        private void btn_RefreshMsgNames_Click(object sender, EventArgs e)
        {
            CLog.SaveLog("Line[" + LineName + "] Refresh Message list.");
            mainForm.startWait("Refresh message list");
            Application.DoEvents();
            try
            {


                //StringBuilder sbNames = new StringBuilder(255 * 100);
                //GetMsgNameList(ipPrinter, sbNames);

                string sNames = m_Printer.GetMsgNameList();

                String strNames = sNames;// sbNames.ToString();
                String[] strName = null;
                //for (int i = 0; i < count; i++)
                //{
                strName = strNames.Split(new Char[] { ',' });
                cbobox_MsgList.Items.Clear();
                int index = 0;
                while (true)
                {
                    if (strName[index].Length > 0)
                        cbobox_MsgList.Items.Add(strName[index]);
                    else
                        break;

                    index++;
                }
            }
            catch (Exception ex)
            {
                CLog.SaveException("---Get MessageList---", ex.Message);
                //iErrCode = Constant.ERR_OBJECT_DISPOSED_EXCEPTION;
            }
            if (cbobox_MsgList.Items.Count > 0)
                cbobox_MsgList.SelectedIndex = 0;

            mainForm.StopWait();
        }

        private void btn_PrintONOFF_Click(object sender, EventArgs e)
        {

            bool bStatus = m_Printer.GetIsPrinting(); //GetPrinted(ipPrinter);
            if (bStatus)
            {
                CLog.SaveLog("Line[" + LineName + "] Stop print");
                if (m_Printer.StopPrint() == 0)
                {
                    MessageBox.Show("Stop print message failed.");

                    CLog.SaveLog("Line[" + LineName + "] Stop print failed");

                    return;
                }
                btn_RefreshMsgNames.Enabled = true;
                btn_PrintONOFF.Text = "Print";
            }
            else
            {
                byte[] msgname = new byte[128];
                CLog.SaveLog("Line[" + LineName + "] Start print");
                string sName = cbobox_MsgList.Text.Substring(0,cbobox_MsgList.Text.Length-4);
                if (m_Printer.StartPrint(sName) == 0)
                {
                    MessageBox.Show("Start print message failed.");
                    CLog.SaveLog("Line[" + LineName + "] Start print failed");
                    return;
                }
                btn_RefreshMsgNames.Enabled = false;
                btn_PrintONOFF.Text = "Stop";
            }
        }
        private delegate void DispMSGDelegate(string MSG);
        private void DispMsg(string strMsg)
        {
            string sendMsg = strMsg + "\n";

            if (this.rTxtbox_Send.InvokeRequired == false)                      //如果调用该函数的线程和控件lstMain位于同一个线程内
            {
                rTxtbox_Send.AppendText(strMsg);
                rTxtbox_Send.AppendText("\n");
            }
            else                                                        //如果调用该函数的线程和控件lstMain不在同一个线程
            {
                 //通过使用Invoke的方法，让子线程告诉窗体线程来完成相应的控件操作
                 DispMSGDelegate DMSGD = new DispMSGDelegate(DispMsg);

                 //使用控件lstMain的Invoke方法执行DMSGD代理(其类型是DispMSGDelegate)
                 this.rTxtbox_Send.Invoke(DMSGD,  strMsg);

            }
        }

        public int SendStrings(string strStrings)
        {
            CLog.SaveLog("Line[" + LineName + "] Send Strings[" + strStrings + "]");
            //byte[] btsendStrings = System.Text.Encoding.ASCII.GetBytes(strStrings);
            int irlt = Constant.ERR_ERROR;
            try
            {
                irlt = m_Printer.SendDynamicString(strStrings);
                if (irlt == Constant.ERR_OK)
                {

                    DispMsg(strStrings);//rTxtbox_Send.AppendText(strStrings);// .Text = rTxtbox_Send.Text.  Insert(strStrings);
                }
            }catch(Exception ex)
            {
                CLog.SaveException("---Send Dynamic String Exception---", ex.Message);
                irlt = Constant.ERR_SEND_STRINGS;
            }
           
            return irlt; 
            
        }

        private void button1_Click(object sender, EventArgs e)
        {/*
            int hand = CreatePrinter();
            Application.DoEvents();
            SetClient(hand, System.Text.Encoding.Default.GetBytes("192.168.0 "), 1);
            ScanPrinter(hand);
            DelPrinter(hand);

          //   hand = CreatePrinter();
          //  uint isn = 260208;
          //  uint iip = 0xCA00A8C0;//3389040832;
          //  int rlt =  StartConnect(hand, isn, iip);

            */
            bool bc1 = false;// m_Printer.ISConnected1();
            bool bc2 = m_Printer.ISConnected2();
            MessageBox.Show(bc1.ToString() + " -- " + bc2.ToString());
        }

        private void tmr_CheckConn_Tick(object sender, EventArgs e)
        {
            bool bc1 = m_Printer.ISConnected1();
            bool bc2 = m_Printer.ISConnected2();
            if (bc1 == false || bc2 == false)
            {//断线
                //重连
                btn_Connect.Text = "ReConnect now...";
                btn_Connect.Enabled = false;
                Application.DoEvents();
                CLog.SaveLog("Line[" + LineName + "] disconnected [Client disconnected].");
                tmr_Reconn.Enabled = true;
                tmr_CheckConn.Enabled = false;
            }
        }

        private void tmr_Reconn_Tick(object sender, EventArgs e)
        {
            if (Convert.ToInt32(tmr_Reconn.Tag) == 1) return;
            tmr_Reconn.Tag = 1;
            CLog.SaveLog("Line[" + LineName + "] Reconnect Start!");
            m_Printer.StopConnect();
            Application.DoEvents();
            try
            {
                m_Printer.StartConnect(strIP, (Int32)iSN);
            }
            catch (Exception ecp)
            {
                //连不上
            }
            if (!m_Printer.GetConnected())//!GetConnected(ipPrinter))
            {
                //MessageBox.Show("Connect to printer failed!");
                CLog.SaveLog("Line[" + LineName + "] Reconnect failed!");
            }
            else
            {
                tmr_Reconn.Enabled = false;
                tmr_CheckConn.Enabled = true;
                btn_Connect.Text = "Disconnect";
                btn_Connect.Enabled = true;
                CLog.SaveLog("Line[" + LineName + "] Reconnect OK!");
            }
            tmr_Reconn.Tag = 0;
        }




    }
}
