using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic;



namespace DeviceServer
{
    public struct SJ_PrinterInfo
    {
	    uint 	uiIPAddr;
	    uint 	uiSerialNumber;

    };

    public partial class frmMain : Form
    {
        [DllImport("Printer.dll", EntryPoint = "CreatePrinter", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int CreatePrinter();

        [DllImport("Printer.dll", EntryPoint = "DelPrinter", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        public static extern void DelPrinter(int pPrinter);

        [DllImport("Printer.dll", EntryPoint = "ScanPrinter", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int ScanPrinter(int pPrinter);

        [DllImport("Printer.dll", EntryPoint = "GetPrinterInfos", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        public static extern void GetPrinterInfos(int pPrinter, int iCount ,StringBuilder s);

        [DllImport("Printer.dll", EntryPoint = "SetIPSection", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        public static extern void SetClient(int pPrinter, byte[] ips,int iCount);

        //[DllImport("Printer.dll", EntryPoint = "SendDynStrings", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        //public static extern void SendDynStrings(int pPrinter, byte[] btStrings);

        public Thread StartSockst;
        public static Socket serverSocket;
        public string m_sServerIp = "192.168.0.206";
        static byte[] buffer = new byte[51200];
        public string m_pIPClient;
        public Int32 m_iClientCount = 0;
        public string m_sAllLineNames = "";
        public LinePageForm currLinePage;
        public WaitForm wf;

        public TcpListener listener = null;
        public NetworkStream clientStream = null;

        public int waitCount = 0;

        public bool m_bSaveLog = true;
        public bool m_bAutoNewline = true;
        public bool m_bAutoConnect = false;
        public bool m_bAutoPrint = false;
        public bool m_bSaveRecStrings = false;
        //--------------------------------------------------------------------------------
        Thread threadWatch = null; // 负责监听客户端连接请求的 线程；
        Socket socketWatch = null;
        Dictionary<string, Socket> dictSkt = new Dictionary<string, Socket>();  //IP Port 和socket的关系
        Dictionary<Socket, string> dictLine = new Dictionary<Socket, string>(); //保存LineName和Socket的关系
        /// <summary>
        /// 监听端口
        /// </summary>
        public int ServerPort = 9000;
        /// <summary>
        /// 是否已启动监听
        /// </summary>
        public bool IsStartListening = false;

        List<LinePageForm> LineForms;

        public frmMain()
        {
            InitializeComponent();
        }

        public bool bCloseListen = false;

        private void Form1_Load(object sender, EventArgs e)
        {
            if (IsStartListening)
                return;
            //启动线程打开监听
          //  StartSockst = new Thread(new ThreadStart(listensocket));
           // StartSockst.Start();
            ReadSetting();
            beginListen();
            
            LineForms = new List<LinePageForm>();

            btn_Refresh.PerformClick();
            
            if(m_bAutoNewline)
                ReadLineInfo();
            CLog.SaveLog("\r\n----------------------------------");

            LB_Ver.Text = "Version:" + Application.ProductVersion.ToString();
            //createlistenserver();
        }

        static string GetLocalIp()  
        {  
            string hostname = Dns.GetHostName();//得到本机名   
            IPHostEntry localhost = Dns.GetHostByName(hostname);//方法已过期，只得到IPv4的地址   
            //IPHostEntry localhost = Dns.GetHostEntry(hostname);  
            IPAddress localaddr = localhost.AddressList[0];  
            return localaddr.ToString();  
        }

        private void beginListen()
        {
            // 创建负责监听的套接字，注意其中的参数；
            socketWatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            // 获得文本框中的IP对象；
            
            IPAddress address = IPAddress.Parse(GetLocalIp());//IPAddress.Parse(txtIp.Text.Trim());
            // 创建包含ip和端口号的网络节点对象；
            IPEndPoint endPoint = new IPEndPoint(address, ServerPort);
            try
            {
                // 将负责监听的套接字绑定到唯一的ip和端口上；
                socketWatch.Bind(endPoint);
            }
            catch (SocketException se)
            {
                MessageBox.Show("监听绑定异常：" + se.Message);
                CLog.SaveLog("监听绑定异常：" + se.Message);
                return;
            }
            // 设置监听队列的长度；
            socketWatch.Listen(10);
            // 创建负责监听的线程；
            threadWatch = new Thread(listensocket);
            threadWatch.IsBackground = true;
            threadWatch.Start();
            CLog.SaveLog("服务器启动监听成功！");
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
            if (icount < 1)
            {
                m_iClientCount = 0;
                return;
            }

            m_iClientCount = icount;
            //m_pIPClient = new IntPtr[icount];
            String strAll = "";
            for (int i = 0; i < icount; i++)
            {
                strtemp = "ClientIP" + i.ToString();
                sval = cIniFile.ReadIniKeys("Base", strtemp, str);
              
                //while (sval.Length < 11)
                //    sval += ",";
                strAll += sval ;
                strAll += ",";
            }
            m_pIPClient = strAll;

            m_sServerIp = cIniFile.ReadIniKeys("Base", "ServerIP", str); 

            sval = cIniFile.ReadIniKeys("Base", "SaveLogs", str);
            if (sval.Length == 0) sval = "1";
            if (sval == "0")
                m_bSaveLog = false;
            else
                m_bSaveLog = true;

            sval = cIniFile.ReadIniKeys("Base", "AutoPL", str);
            if (sval.Length == 0) sval = "1";
            if (sval == "0")
                m_bAutoNewline = false;
            else
                m_bAutoNewline = true;

            sval = cIniFile.ReadIniKeys("Base", "AutoConnect", str);
            if (sval.Length == 0) sval = "0";
            if (sval == "0")
                m_bAutoConnect = false;
            else
                m_bAutoConnect = true;

            sval = cIniFile.ReadIniKeys("Base", "AutoPrint", str);
            if (sval.Length == 0) sval = "0";
            if (sval == "0")
                m_bAutoPrint = false;
            else
                m_bAutoPrint = true;

            sval = cIniFile.ReadIniKeys("Base", "SaveStrings", str);
            if (sval.Length == 0) sval = "1";
            if (sval == "0")
                m_bSaveRecStrings = false;
            else
                m_bSaveRecStrings = true;
        }

        public void DealCMD(string StrIN,Socket currSocket)
        {
            //如果客户端的命令以LIST开头，
            string request = StrIN.Replace("\n", "").Replace("\r", "");

            SaveRecStrings(StrIN);

            if (request.StartsWith("List:"))
            {
                byte[] responseBuffer = System.Text.Encoding.ASCII.GetBytes("List:" + m_sAllLineNames);
                currSocket.Send(responseBuffer);
                //clientStream.Write(responseBuffer, 0, responseBuffer.Length);
            }
            else if (request.StartsWith("Connect:"))
            {
                string[] requestMessage = request.Split(':');
                string reqLineName = requestMessage[1];

                string[] LineNames = m_sAllLineNames.Split(',');
                bool bFound = false;
                for (int i = 0; i < LineNames.Length; i++)
                {
                    if (reqLineName == LineNames[i])
                    {
                        bFound = true;
                        break;
                    }
                }
                byte[] data;
                if (bFound)
                {
                    string strReturn = "Connect:OK";
                    data = System.Text.Encoding.ASCII.GetBytes(strReturn);
                }
                else
                {
                    string strReturn = "Connect:ERROR [" + reqLineName + "] no found.";
                    data = System.Text.Encoding.ASCII.GetBytes(strReturn);
                }
                currSocket.Send(data);


                if (dictLine.ContainsKey(currSocket))
                    dictLine[currSocket] = reqLineName;
                else
                    dictLine.Add( currSocket,reqLineName);
                //clientStream.Write(data, 0, data.Length);
            }
            else if (request.StartsWith("Send:"))
            {
                string[] requestMessage = request.Split(':');
                string strStrings = requestMessage[1];
                byte[] data;
                string slinename = "";
                try
                {
                    slinename = dictLine[currSocket];
                }
                catch (KeyNotFoundException knfe)
                {
                    string strReturn = "Send:ERROR [ Please connect Line first ]! ";
                    data = System.Text.Encoding.ASCII.GetBytes(strReturn);
                }
                //find line page
                LinePageForm lpf = GetLineFormbyName(slinename);
                int irlt = 0;
                if(lpf!=null)
                    irlt = lpf.SendStrings(strStrings);
                else
                {
                    //找不到对应的，不显示
                }
                //int irlt = SendDynStrings(iprin)

                if (irlt == Constant.ERR_OK)
                {
                    string strReturn = "Send:OK";
                    data = System.Text.Encoding.ASCII.GetBytes(strReturn);
                }
                else
                {
                    string strError = irlt.ToString();

                    if (irlt == Constant.ERR_SEND_STRINGS)
                        strError = "Probably Message not correct.";
                    else if (irlt == Constant.ERR_ERROR)           
                        strError = "Probably Cache buffer is full or no dynamic text object.";
    
                    
                    string strReturn = "Send:ERROR [" + strError + "] ";
                    data = System.Text.Encoding.ASCII.GetBytes(strReturn);
                }
              
                currSocket.Send(data);
                //clientStream.Write(data, 0, data.Length);
            }
        }



        public void listensocket()
        {
            while (true)  // 持续不断的监听客户端的连接请求；  
            {
                // 开始监听客户端连接请求，Accept方法会阻断当前的线程；  
                Socket sokConnection;

                //IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse(m_sServerIp), ServerPort);

                //sokConnection.Bind(endpoint);
                //sokConnection.Listen(10);
                try
                {
                    sokConnection = socketWatch.Accept();
                }
                catch(SocketException se)
                {
                    CLog.SaveLog("客户端关闭！");
                    CLog.SaveException("---Listen---",se.Message);
                    CLog.SaveLog("-----------------------------");
                    return;
                }
                //Socket sokConnection = socketWatch.Accept(); // 一旦监听到一个客户端的请求，就返回一个与该客户端通信的 套接字；  
                
                // 将与客户端连接的 套接字 对象添加到集合中；  
                dictSkt.Add(sokConnection.RemoteEndPoint.ToString(), sokConnection);
                CLog.SaveLog("客户端连接成功！[" + sokConnection.RemoteEndPoint.ToString() + "]");
                Thread thr = new Thread(RecMsg);
                thr.IsBackground = true;
                thr.Start(sokConnection);
                
            }  
            
        }

        void RecMsg(object sokConnectionparn)
        {
            Socket sokClient = sokConnectionparn as Socket;
            while (true)
            {
                // 定义一个2M的缓存区；
                byte[] arrMsgRec = new byte[1024 * 1024 * 2];
                // 将接受到的数据存入到输入  arrMsgRec中；
                int length = -1;
                try
                {
                    length = sokClient.Receive(arrMsgRec); // 接收数据，并返回数据的长度；
                }
                catch (SocketException se)
                {
                    CLog.SaveLog("异常(Receive Message)：" + se.Message);
                    CLog.SaveException("---Receive Message---", se.Message);
                    // 从 通信套接字 集合中删除被中断连接的通信套接字；
                    dictSkt.Remove(sokClient.RemoteEndPoint.ToString());
                   
                    break;
                }
                catch (Exception e)
                {
                    CLog.SaveLog("异常(Receive Message)：" + e.Message);
                    CLog.SaveException("---Receive Message---", e.Message);
                    // 从 通信套接字 集合中删除被中断连接的通信套接字；
                    dictSkt.Remove(sokClient.RemoteEndPoint.ToString());
                 
                    break;
                }
                string strMsg = System.Text.Encoding.ASCII.GetString(arrMsgRec, 0, length);
                try
                {
                    DealCMD(strMsg, sokClient);
                }
                catch(SocketException se)
                {
                    CLog.SaveLog("异常(DealCMD)：" + se.Message);
                    CLog.SaveException("---DealCMD---", se.Message);
                }
                catch (Exception e)
                {
                    CLog.SaveLog("异常(DealCMD)：" + e.Message);
                    CLog.SaveException("---DealCMD---", e.Message);
                }
            }
        }
        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            CLog.SaveLog("Refresh, start scan printers");

            startWait("Scan devices");
            CPrinter cScanPrinter = new CPrinter();
            Application.DoEvents();
            cScanPrinter.SetIPSection(m_pIPClient);
  
            cScanPrinter.sUdpServerIP = m_sServerIp;
    
            int count = cScanPrinter.ScanPrinter();
 
            lstBox_Printers.Items.Clear();
            if (count > 0)
            {
                string[] sPrinters = cScanPrinter.GetPrinterInfos();
                Application.DoEvents();
                for (int i = 0; i < sPrinters.Length; i++)
                    lstBox_Printers.Items.Add(sPrinters[i]);
            }

            StopWait();
            CLog.SaveLog("scan end, find [" + count.ToString() + "] printers.");
        }
        /*    
        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            SaveLog("Refresh, start scan printers");

            startWait("Scan devices");
            
            int handle1 = CreatePrinter();
            Application.DoEvents();
            SetClient(handle1, System.Text.Encoding.ASCII.GetBytes( m_pIPClient), m_iClientCount);

            int count =  ScanPrinter(handle1);

            Application.DoEvents();
            StringBuilder pin = new StringBuilder(255 * 20);
            GetPrinterInfos(handle1, count ,pin);
            String strClient = pin.ToString();
            String[] strClients = null;
            Application.DoEvents();
            strClients = strClient.Split(new Char[] { ',' });
            //}

            lstBox_Printers.Items.Clear();
            //lstBox_Printers.DataSource = strClients;
            for(int i=0;i<strClients.Length;i++)
                lstBox_Printers.Items.Add(strClients[i]);

            DelPrinter(handle1);
            StopWait();
            SaveLog("scan end, find [" + count.ToString() + "] printers.");
        }
        */
        public LinePageForm newLinePage(string sLineName)
        {
            MainTables.TabPages.Add(sLineName);
            MainTables.SelectTab(MainTables.TabPages.Count - 1);

            LinePageForm lineform = new LinePageForm();

            lineform.FormBorderStyle = FormBorderStyle.None;
            lineform.Dock = DockStyle.Fill;
            lineform.TopLevel = false;
            
            lineform.Parent = MainTables.SelectedTab;
            lineform.LineName = sLineName;
            lineform.mainForm = this;
            lineform.bNeedAutoConnect = m_bAutoConnect;
            lineform.bNeedAutoPrint = m_bAutoPrint;
            lineform.Show();

            LineForms.Add(lineform);

            currLinePage = lineform;
            createAllLineName();

            CLog.SaveLog("Create Line[" + sLineName + "]");
            return lineform;
        }

        private bool checkLineName(string sLineName)
        {//true = can use
            for (int i = 0; i < LineForms.Count; i++)
            {
                LinePageForm lpf = LineForms[i];
                if (lpf.LineName == sLineName)
                {
                    return false;
                }
            }
            return true;
        }

        private LinePageForm GetLineFormbyName(string sLineName)
        {
            for (int i = 0; i < LineForms.Count; i++)
            {
                LinePageForm lpf = LineForms[i];
                if (lpf.LineName == sLineName)
                {
                    return lpf;
                }
            }
            return null;
        }

        private void btn_addLine_Click(object sender, EventArgs e)
        {
            string sName = Input("Please input the Product-Line Name:", "Line");

            if (sName.Length < 1)
                return;

            if (checkLineName(sName))
                newLinePage(sName);
            else
                MessageBox.Show("["+sName+"] is already exists!");
      
        }

        public string Input(string title, string defaultText)
        {
            InputBox ib = new InputBox();
            ib.setContext(title, defaultText);
            ib.ShowDialog();

            string returnval = ib.getInputValue();
            return returnval;
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveLineInfo();

            bCloseListen = true;
            Thread.Sleep(50);
            //listener.Stop();
            socketWatch.Close();
            //StartSockst.Abort();
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            //bCloseListen = true;
            CLog.SaveLog("-------------Close-----------");
        }

        private void btn_Assign_Click(object sender, EventArgs e)
        {
            if (MainTables.TabCount < 1)
            {
                MessageBox.Show("Please create product-line first.");
                return;
            }
            if (lstBox_Printers.Items.Count <1)
            {
                MessageBox.Show("Please refresh devices.");
                return;
            }
            if(lstBox_Printers.SelectedIndex < 0)
                lstBox_Printers.SelectedIndex = 0;

            String str;
            str="Are your sure add ";
            str += lstBox_Printers.Items[lstBox_Printers.SelectedIndex].ToString();
            str += " to [";
            str += MainTables.TabPages[MainTables.SelectedIndex].Text;
            str += "]?";

            DialogResult dr;
            dr=MessageBox.Show(str,"",MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,MessageBoxDefaultButton.Button1);
            if (dr == DialogResult.No)
                return;

            LinePageForm lPage = getPage(MainTables.TabPages[MainTables.SelectedIndex].Text);
            lPage.setIPstr(lstBox_Printers.Items[lstBox_Printers.SelectedIndex].ToString());

            CLog.SaveLog("Add [" + lstBox_Printers.Items[lstBox_Printers.SelectedIndex].ToString() +
                   "] to [" + MainTables.TabPages[MainTables.TabIndex].Text + "]");
            //MainTables.TabPages[MainTables.TabIndex].          
        }

        public LinePageForm getPage(string sLineName)
        {
            for (int i=0;i<LineForms.Count;i++)
            {
                if(sLineName == LineForms[i].LineName)
                    return LineForms[i];
            }
            
            return null;
        }

        public uint IP2Int(string strIPAddr)
        {
            char[] separator = new char[] { '.' };
            string[] items = strIPAddr.Split(separator);
            uint uiIP = uint.Parse(items[0])  
                    | uint.Parse(items[1]) << 8
                    | uint.Parse(items[2]) << 16
                    | uint.Parse(items[3]) << 24;
            return uiIP;
        }

        public string IP2Str(uint uiIPAddr)
        {
            string strIP="";
            strIP = (uiIPAddr & 0xff).ToString() + ".";
            strIP += ((uiIPAddr>>8) & 0xff).ToString() + ".";
            strIP += ((uiIPAddr>>16) & 0xff).ToString() + ".";
            strIP += ((uiIPAddr>>24) & 0xff).ToString() ;
            return strIP;
        }

        public void SaveRecStrings(string strLog)
        {
            if (!m_bSaveRecStrings) return;
            string str = System.Windows.Forms.Application.StartupPath;
            str += "\\Logs\\";

            if (!Directory.Exists(str))
                Directory.CreateDirectory(str);

            str += "Send" + DateTime.Now.ToString("yyyyMMdd");
            str += ".txt";

            if (!File.Exists(str))
            {
                FileStream fs = File.Create(str);
                fs.Close();
            }

            StreamWriter sw = new StreamWriter(str, true, System.Text.Encoding.ASCII);
            string strSave = DateTime.Now.ToString("hh:mm:ss")+"  "+strLog;
            sw.WriteLine(strSave);
            sw.Close();  
        }

        private bool bInLog = false;
        public void SaveLog(string strLog)
        {
            if (!m_bSaveLog) return;

            while (bInLog)
            {
                if (!bInLog) break;
                Application.DoEvents();
                Thread.Sleep(10);
            }
            bInLog = true;
            //DateTime DT = System.DateTime.Now; 
            string str = System.Windows.Forms.Application.StartupPath;
            str += "\\Logs\\";
           
            if (!Directory.Exists(str))
                Directory.CreateDirectory(str);  //如果文件夹已经存在，不进行任何操作，也不报错。

            str += "Log" + DateTime.Now.ToString("yyyyMMdd");
            str += ".txt";

            if (!File.Exists(str))
            {
                FileStream fs = File.Create(str);
                fs.Close();
            }

            StreamWriter sw = new StreamWriter(str, true, System.Text.Encoding.ASCII);
            string strSave = DateTime.Now.ToString("hh:mm:ss")+"  "+strLog;
            sw.WriteLine(strSave);
            sw.Close();

            bInLog = false;
            //File.AppendAllText

        }

        private void btn_DelLine_Click(object sender, EventArgs e)
        {
            if (MainTables.SelectedIndex < 0)
                return;

            string sLineName = MainTables.TabPages[MainTables.SelectedIndex].Text;
            DialogResult rlt = MessageBox.Show("Are you sure delete this product-line[" + sLineName + "]?"
                , "", MessageBoxButtons.YesNo);
            if (rlt == DialogResult.No)
                return;

            MainTables.TabPages.Remove(MainTables.TabPages[MainTables.SelectedIndex]);

            LinePageForm lpf = getPage(sLineName);
            LineForms.Remove(lpf);

            if (MainTables.TabPages.Count == 0)
            {
                btn_DelLine.Enabled = false;
                btn_RenameLine.Enabled = false;
            }
            else
            {
                btn_DelLine.Enabled = true;
                btn_RenameLine.Enabled = true; 

            }
            createAllLineName();

            CLog.SaveLog("Delete line [" + sLineName + "]");
        }

        private void btn_RenameLine_Click(object sender, EventArgs e)
        {
            if (MainTables.SelectedIndex < 0)
                return;
            string sLineName = MainTables.TabPages[MainTables.SelectedIndex].Text;

            string sName = Input("Please input the new Product-Line Name:", sLineName);

            if (sName.Length < 1)
                return;

            MainTables.TabPages[MainTables.SelectedIndex].Text = sName;

            LinePageForm lpf = getPage(sLineName);
            lpf.LineName = sName;

            createAllLineName();

            CLog.SaveLog("Rename line [" + sLineName + "] to [" + sName + "]");
        }

        private string GetAllLineName()
        {
            if (MainTables.SelectedIndex < 0)
                return "";
            string strAll="";
            for (int i = 0; i < MainTables.TabPages.Count; i++)
            {
                strAll += MainTables.TabPages[i].Text;
                strAll += ",";
            }
            return strAll;
        }

        private void createAllLineName()
        {
            m_sAllLineNames = GetAllLineName();
        
        }

        private void MainTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            //当当前活动页变化，当前活动Form也要改
            if (MainTables.SelectedIndex < 0) return;
            currLinePage = getPage(MainTables.TabPages[MainTables.SelectedIndex].Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        public void startWait(string sNotice)
        {
            wf = new WaitForm();
            wf.setNotice(sNotice);
            //wf.Owner = fParent;
            wf.Show();
        }

        public void StopWait()
        {
            wf.Visible = false;
            wf.Close();
        }

        private void btn_Setup_Click(object sender, EventArgs e)
        {
            SetupForm sf = new SetupForm();
            sf.ShowDialog();
            
        }

        public void ReadLineInfo()
        {
            
            string str = System.Windows.Forms.Application.StartupPath;
            str += "\\DeviceServer.ini";
            string sval = cIniFile.ReadIniKeys("ProductLines", "Count", str);
            int iCount = int.Parse(sval);
            LinePageForm lpf = null;
            LineForms.Clear();
            for (int i = 0; i < iCount; i++)
            {
                string strtemp = "ProductLine" + i.ToString();
                sval = cIniFile.ReadIniKeys(strtemp, "Name", str);
                lpf = newLinePage(sval);
                //LineForms.Add(lpf);
                sval = cIniFile.ReadIniKeys(strtemp, "IP", str);
                lpf.setIP(sval);// (IP2Str(uint.Parse(sval)));
                sval = cIniFile.ReadIniKeys(strtemp, "SN", str);
                lpf.setSN(sval);
                sval = cIniFile.ReadIniKeys(strtemp, "MsgName", str);
                lpf.setLastPrintedMsgName(sval);

                lpf.DoAuto();
            }
        }

        public void SaveLineInfo()
        {
            string str = System.Windows.Forms.Application.StartupPath;
            str += "\\DeviceServer.ini";
            cIniFile.WriteIniKeys("ProductLines", "Count", LineForms.Count.ToString(),str);

            for(int i=0;i<LineForms.Count;i++)
            {
                LinePageForm lpf = LineForms[i];
                
                string strtemp = "ProductLine" + i.ToString();
                cIniFile.WriteIniKeys(strtemp, "Name",lpf.LineName, str);
              
                cIniFile.WriteIniKeys(strtemp, "IP",lpf.getIP(), str);
                cIniFile.WriteIniKeys(strtemp, "SN",lpf.getSN(), str);
                
                cIniFile.WriteIniKeys(strtemp, "MsgName", lpf.getLastPrintedMsgName(),str);
                
            }
        }

    

    }


}
