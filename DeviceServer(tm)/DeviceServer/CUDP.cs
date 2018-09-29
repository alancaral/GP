using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace DeviceServer
{
    class CUDP
    {
        //private UdpClient udpc = null;
        //private UdpClient udpReceive = null;
        private Socket UdpSkt;
        private byte[] strScan = {0x53,0x43,0x01,0x02,0,0,0,0,0x01,0x02,0,0,0x45,0x43 };
        //private IPEndPoint ipLocalep;
        public List<string> lstFind = new List<string>();

        //private bool bFind;

        public CUDP(string sLocalIP, Int32 iPort = 19000)
        {
            //ipLocalep = new IPEndPoint(IPAddress.Parse(sLocalIP), iPort);
            try
            {


                UdpSkt = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                UdpSkt.Bind(new IPEndPoint(IPAddress.Parse(sLocalIP), iPort));//绑定本地IP端口
                UdpSkt.ReceiveTimeout = 500;
                uint IOC_IN = 0x80000000;
                uint IOC_VENDOR = 0x18000000;
                uint SIO_UDP_CONNRESET = IOC_IN | IOC_VENDOR | 12;
                UdpSkt.IOControl((int)SIO_UDP_CONNRESET, new byte[] { Convert.ToByte(false) }, null);
            }
            catch (SocketException se)
            {
                CLog.SaveException("---UDP Init---", se.Message);
            }
            catch (Exception e)
            {
                CLog.SaveException("---UDP Init---", e.Message);
            }
            //setsockopt(m_sockClient, SOL_SOCKET, SO_RCVTIMEO, (char*)&iNetTimeout, sizeof(int));
        }

        ~CUDP()
        {
            UdpSkt.Close();
        }

        private Int32 Send(string sIP,int iPort)
        {
            IPEndPoint ipepnew = new IPEndPoint(IPAddress.Parse(sIP), iPort);
            Int32 irlt = 0;
            try
            {
                lock (UdpSkt)
                {
                    irlt = UdpSkt.SendTo(strScan, 14, SocketFlags.None, ipepnew);//将数据发送到指定的终结点
                }
            }
            catch (Exception e)
            {
                CLog.SaveException("---UDP Send---", e.Message);
            }
            if (irlt > 0)
                return Constant.ERR_OK;
            else
                return Constant.ERR_ERROR;
        }
        private void recv()
        {
            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
            EndPoint Remote = (EndPoint)sender;
            byte[] data = new byte[1024];
            int iRecv = 0;
            try { 
                UdpSkt.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 300);
                iRecv = UdpSkt.ReceiveFrom(data, ref Remote);
            }
            catch (Exception e)
            {
                CLog.SaveException("---UDP Recv---", e.Message);
            }
            if (iRecv > 0)
                return ;  
  
        }
        private bool receiveEnd = false;
        void ReciveMsg()
        {
            receiveEnd = false;
            while (true)
            {
                EndPoint point = new IPEndPoint(IPAddress.Any, 0);//用来保存发送方的ip和端口号
                byte[] buffer = new byte[1024];
 
                UdpSkt.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 13000);
                int length = 0;
                try
                {
                    lock(UdpSkt)
                    {
                        length = UdpSkt.ReceiveFrom(buffer, ref point);//接收数据报
                    }
                }
                catch (SocketException e)
                {
                    //超时
                    CLog.SaveException("---UDP Receive---", e.Message);
                    break;
                }
                if (length > 0)
                {
                    Int32 isn = buffer[4] + buffer[5] * 256 + buffer[6] * 256 * 256 + buffer[7] * 256 * 256 * 256;
                    string IPPort = (point as IPEndPoint).ToString();
                    string[] strArray = IPPort.Split(new Char[] { ':' });
                    string strIP = strArray[0];//.Substring(1, strArray[0].Length - 2);

                    lstFind.Add(strIP + "-" + isn.ToString());
                    //break;
                }
            }
            receiveEnd = true;
        }

        public bool TryDeviceIsExists(string localIP, string sIP, Int32 iPort = 18000)
        {
            //bFind = false;
            lstFind.Clear();
            Thread t = new Thread(ReciveMsg);//开启接收消息线程
            t.Start();
         
            for (int i = 1; i < 255; i++)
            {
                String strIP = string.Format("{0}.{1:D}", sIP, i);
                Send(strIP, iPort);
                
                Application.DoEvents();
                Thread.Sleep(50);
            }
            //for (int i = 0; i < 100;i++ )
            //    Thread.Sleep(30);

            
            while (true)
                {
                    ThreadState ts = t.ThreadState;
                    if (ts == ThreadState.Running)
                    {
                        Application.DoEvents();
                        Thread.Sleep(20);
                    }    //Thread.Sleep(30);
                    else if (ts == ThreadState.Stopped)
                        break;
                    //if (receiveEnd)
                    //    break;
                }
            Thread.Sleep(100);
            if (lstFind.Count > 0)
                return true;
            else 
                return false;
        }

        void receiveUdpMsg(object obj)
        {
            UdpClient client = obj as UdpClient;
            IPEndPoint endpoint = new IPEndPoint(IPAddress.Any, 0);
            while (true)
            {
                client.BeginReceive(delegate(IAsyncResult result) {
                       lstFind.Add(result.AsyncState.ToString()); //委托接收消息
                }, Encoding.UTF8.GetString(client.Receive(ref endpoint)));
            }
        }
        
        public void close()
        {          
            UdpSkt.Close();
        }
    }
}
