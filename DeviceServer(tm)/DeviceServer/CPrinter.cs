using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DeviceServer
{
    class CPrinter
    {
        private List<string> lScanPrinters = new List<string>();
        private List<string> lScanIPSection = new List<string>();

        public string sUdpServerIP;

        /// <summary>
        /// 扫描喷码机设备（根据设定好的IP端搜索）
        /// </summary>
        /// <returns>返回搜索到的数量</returns>
        public Int32 ScanPrinter()
        {
            lScanPrinters.Clear();

            if (sUdpServerIP.Length < 1)
                sUdpServerIP = "192.168.0.206";
            CUDP udpClient = new CUDP(sUdpServerIP);

            //udpClient.CreateReceive();
            for (Int32 Sec = 0; Sec < lScanIPSection.Count; Sec++)
            {
                String strSec = lScanIPSection[Sec];
                if (strSec.Length == 0) continue;
                //for (Int32 i = 1; i < 255; i++)
                {
                    //String strIP = string.Format("{0}.{1:D}",strSec,i);
                    if (udpClient.TryDeviceIsExists(sUdpServerIP, strSec))//传入IP段
                    {
                        for (Int32 n = 0; n < udpClient.lstFind.Count; n++)
                        {
                            lScanPrinters.Add(udpClient.lstFind[n]);
                        }
                        //lScanPrinters.Add(strIP);
                        
                    }
                }
            }

            udpClient.close();

            return lScanPrinters.Count;//lScanPrinters.ToArray();
        }
        /// <summary>
        /// 获取扫描到的设备IP和SN
        /// </summary>
        /// <returns>string[] </returns>
        public string[] GetPrinterInfos()
        {
            return lScanPrinters.ToArray();
        }
        /// <summary>
        /// 设置需要扫描的IP段
        /// </summary>
        /// <param name="strSection"></param>
        /// <returns></returns>
        public Int32 SetIPSection(string strSection)
        {
            lScanIPSection.Clear();
            string[] IPArray = strSection.Split(',');
            lScanIPSection = new List<string>(IPArray);
            return Constant.ERR_OK;
        }

        private CTCP ct = null;
        private string strIP;
        public string IP
        {
            get { return strIP; }
            //set { strIP = value; }
        }
        private Int32 iSerialNumber;
        public Int32 SN
        {
            get { return iSerialNumber; }
            //set { iSN = value; }
        }

        public bool GetConnected()
        {
            if (ct == null) return false ;
            return ct.GetConnected();
        }

        public Int32 StartConnect(string sIP,Int32 iSN)
        {
            strIP = sIP;
            iSerialNumber = iSN;
            if(ct == null)
                ct = new CTCP();
            return ct.Connect(strIP, iSerialNumber);
        }

        public Int32 StopConnect()
        {
            return ct.Disconnect();
        }

        public string GetMsgNameList()
        {
            return ct.GetMsgNameList();
        }

        public bool GetIsPrinting()
        {
            if (ct == null) return false;
            return ct.GetIsPrinting();
        }

        public Int32 StartPrint(string strMsgName)
        {
            return ct.StartPrintMsg(strMsgName);
        }

        public Int32 StopPrint()
        {
            return ct.StopPrintMsg();
        }

        public Int32 SendDynamicString(string strStrings)
        {
            return ct.SendStrings(strStrings);
        }

        public bool ISConnected1()
        {
            return ct.IsSocketConnected1();
        }
        public bool ISConnected2()
        {
            return ct.IsSocketConneted2();
        }
    }
}
