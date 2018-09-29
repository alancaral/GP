using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace DeviceServer
{
    class CTCP
    {
        private TcpClient tc = null;
        private NetworkStream ns;
        public CTCP()
        {
            tc = new TcpClient();
            
        }
        private Int32 iSN;
        
        public Int32 Connect(string sRemoteIP ,Int32 iPrinterSN,Int32 iRemotePort=17888)
        {
            int iErrCode = 0;
            iSN = iPrinterSN;
            if (tc == null)
                tc = new TcpClient();
            byte[] inValue = new byte[] { 1, 0, 0, 0, 0x88, 0x13, 0, 0, 0xE8, 0x03, 0, 0 };// 首次探测时间5 秒, 间隔侦测时间1 秒
            tc.Client.IOControl(IOControlCode.KeepAliveValues, inValue, null);
            try
            {
                tc.Connect(sRemoteIP, iRemotePort);
                ns = tc.GetStream();
            }
            catch (SocketException se)
            {
                CLog.SaveException("---Tcp Connect---",se.Message);
                iErrCode = Constant.ERR_SOCKET_EXCEPTION;
            }
            catch (Exception e)
            {
                CLog.SaveException("---Tcp Connect---",e.Message);
                iErrCode = Constant.ERR_EXCEPTION ;
            }

            if (tc.Connected)
                return Constant.ERR_OK;
            else
                return Constant.ERR_ERROR;
        }

        public Int32 Disconnect()
        {
            int iErrCode = Constant.ERR_OK;
            try
            {
                if (tc != null)
                    tc.Close();
                tc = null;
            }
            catch (Exception e)
            {
                CLog.SaveException("---Tcp DisConnect---", e.Message);
                iErrCode = Constant.ERR_EXCEPTION;
            }

            return iErrCode;
        }

        public bool GetConnected()
        {
            if(tc == null)
                return false;
            if (tc.Connected)
                return true;
            else
                return false;
        }

        public Int32 Send(byte[] bData)
        {
            int iErrCode = Constant.ERR_OK;
            try 
            { 
                lock (ns)
                {
                    if (ns.CanWrite)
                        ns.Write(bData, 0, bData.Length);
                    else
                    {
                        iErrCode = Constant.ERR_OBJECT_DISPOSED_EXCEPTION;
                    }
                }
            }
            catch(System.IO.IOException ioe)
            {
                CLog.SaveException("---Tcp Send---", ioe.Message);
                iErrCode = Constant.ERR_IO_EXCEPTION;
            }
            catch(ObjectDisposedException ode)
            {
                CLog.SaveException("---Tcp Send---", ode.Message);
                iErrCode = Constant.ERR_OBJECT_DISPOSED_EXCEPTION;
            }
            catch (Exception e)
            {
                CLog.SaveException("---Tcp Send---", e.Message);
                iErrCode = Constant.ERR_EXCEPTION;
            }
            return iErrCode;
        }

        public Int32 Receive(byte[] bData,Int32 iDataLen)
        {
            Int32 iLen = 0;
            int iErrCode = Constant.ERR_OK;
            try
            {
                lock (ns)
                {
                    if (ns.CanRead)
                        iLen = ns.Read(bData, 0, iDataLen);
                    else { 
                        iErrCode = Constant.ERR_OBJECT_DISPOSED_EXCEPTION; 
                    }
                }
            }catch(System.IO.IOException ioe)
            {
                CLog.SaveException("---Tcp Receive---", ioe.Message);
                iErrCode = Constant.ERR_IO_EXCEPTION;
            }
            catch (ObjectDisposedException ode)
            {
                CLog.SaveException("---Tcp Receive---", ode.Message);
                iErrCode = Constant.ERR_OBJECT_DISPOSED_EXCEPTION;
            }
            catch (Exception e)
            {
                CLog.SaveException("---Tcp Receive---", e.Message);
                iErrCode = Constant.ERR_EXCEPTION;
            }
            return iLen;
        }

        public Int32 SendCMD(CData cCmd)
        {
            Send(cCmd.SendData);

            for (int i = 0; i < 3; i++)
            {
                System.Threading.Thread.Sleep(200);
                Int32 ilen = 0;
                ilen = Receive(cCmd.ReceiveData, cCmd.RecvDataSize);
                if (ilen > 0)
                {
                    cCmd.ReceiveDataLength = ilen;
                    return Constant.ERR_OK;
                }
            }
            return Constant.ERR_ERROR;
        }

        public Int32 CheckDisConn()
        {

            return 0;
        }
        public bool IsSocketConnected1()
        {
            #region remarks
            /********************************************************************************************
             * 当Socket.Conneted为false时， 如果您需要确定连接的当前状态，请进行非阻塞、零字节的 Send 调用。
             * 如果该调用成功返回或引发 WAEWOULDBLOCK 错误代码 (10035)，则该套接字仍然处于连接状态； 
             * 否则，该套接字不再处于连接状态。
             * Depending on http://msdn.microsoft.com/zh-cn/library/system.net.sockets.socket.connected.aspx?cs-save-lang=1&cs-lang=csharp#code-snippet-2
            ********************************************************************************************/
            #endregion

            #region 过程
            // This is how you can determine whether a socket is still connected.
            if (tc == null)
                return false;
            bool connectState = true;
            bool blockingState = tc.Client.Blocking;
            try
            {
                byte[] tmp = new byte[1];

                tc.Client.Blocking = false;
                tc.Client.Send(tmp, 0, 0);
                //Console.WriteLine("Connected!");
                connectState = true; //若Send错误会跳去执行catch体，而不会执行其try体里其之后的代码
            }
            catch (SocketException e)
            {
                // 10035 == WSAEWOULDBLOCK
                if (e.NativeErrorCode.Equals(10035))
                {
                    //Console.WriteLine("Still Connected, but the Send would block");
                    connectState = true;
                }

                else
                {
                    //Console.WriteLine("Disconnected: error code {0}!", e.NativeErrorCode);
                    connectState = false;
                }
            }
            finally
            {
                tc.Client.Blocking = blockingState;
            }

            //Console.WriteLine("Connected: {0}", client.Connected);
            return connectState;
            #endregion
        }

        private static bool IsSocketConnected(Socket s)
        {
            #region remarks
            /* As zendar wrote, it is nice to use the Socket.Poll and Socket.Available, but you need to take into consideration 
             * that the socket might not have been initialized in the first place. 
             * This is the last (I believe) piece of information and it is supplied by the Socket.Connected property. 
             * The revised version of the method would looks something like this: 
             * from：http://stackoverflow.com/questions/2661764/how-to-check-if-a-socket-is-connected-disconnected-in-c */
            #endregion

            #region 过程

            if (s == null)
                return false;
            //return !((s.Poll(1000, SelectMode.SelectRead) && (s.Available == 0)) || !s.Connected);

            /* The long, but simpler-to-understand version:
            */
                    bool part1 = s.Poll(1000, SelectMode.SelectRead);
                    bool part2 = (s.Available == 0);
                    if ((part1 && part2) || !s.Connected)
                        return false;
                    else
                    {
                        
                        return true;
                    }

            /**/
            #endregion
        }
        public bool IsSocketConneted2()
        {
            if (tc == null)
                return false;
            return IsSocketConnected(tc.Client);
        }

        public string GetMsgNameList()
        {
            //CData cdGetMsgCount = new CData(eCommand.CMD_GET_MSG_COUNT,iSN);
            CData_MsgCount cdGetMsgCount = new CData_MsgCount(iSN);
            cdGetMsgCount.PackData();
            SendCMD(cdGetMsgCount);
            Int32 iTemp = cdGetMsgCount.ReceiveData[8] + cdGetMsgCount.ReceiveData[9] * 256;
            if (iTemp == 1)
            {//命令成功
                iTemp = cdGetMsgCount.ReceiveData[14] + cdGetMsgCount.ReceiveData[15] * 256;
            }
            Int32 iMsgCount = iTemp;

            string strMsgList = "";
            if (iMsgCount < 0 || iMsgCount > 999)
                return strMsgList;
            for (short i = 0; i < iMsgCount; i++)
            {
                CData_MsgInfo cdGetMsgInfo = new CData_MsgInfo(iSN);
                cdGetMsgInfo.PackData(i);
                SendCMD(cdGetMsgInfo);
                Int32 iTemp1 = cdGetMsgInfo.ReceiveData[8] + cdGetMsgInfo.ReceiveData[9] * 256;
                if (iTemp1 == 1)
                {//命令成功 Name size
                    iTemp1 = cdGetMsgInfo.ReceiveData[26] + cdGetMsgInfo.ReceiveData[27] * 256;
                    byte[] bName = new byte[iTemp1-1];
                    for (int n = 0; n < iTemp1-1; n++)
                    {
                        bName[n] = cdGetMsgInfo.ReceiveData[63 + n];
                    }
                    string strTemp = Encoding.ASCII.GetString(bName);
                    strMsgList += strTemp;
                    strMsgList += ",";
                }
            }
            return strMsgList;
        }

        public bool GetIsPrinting()
        {
            CData_PrintingStatus cdGetPringStatus = new CData_PrintingStatus(iSN);
            cdGetPringStatus.PackData();
            SendCMD(cdGetPringStatus);
            Int32 iTemp = cdGetPringStatus.ReceiveData[8] + cdGetPringStatus.ReceiveData[9] * 256;
            if (iTemp == 1)
            {//命令成功
                iTemp = cdGetPringStatus.ReceiveData[18] + cdGetPringStatus.ReceiveData[19] * 256;
                if (iTemp > 0)
                    return true;
            }
            return false;
        }

        public Int32 StopPrintMsg()
        {
            CData_StopPrint cdStopPrint = new CData_StopPrint(iSN);
            cdStopPrint.PackData();
            SendCMD(cdStopPrint);
            Int32 iTemp = cdStopPrint.ReceiveData[8] + cdStopPrint.ReceiveData[9] * 256;
            if (iTemp == 1)
            {//命令成功
                return Constant.ERR_OK;
            }
            return Constant.ERR_ERROR;
        }

        public Int32 StartPrintMsg(string strName)
        {
            CData_StartPrint cdStartPrint = new CData_StartPrint(iSN);
            cdStartPrint.PackData(strName);
            SendCMD(cdStartPrint);
            Int32 iTemp = cdStartPrint.ReceiveData[8] + cdStartPrint.ReceiveData[9] * 256;
            if (iTemp == 1)
            {//命令成功
                return Constant.ERR_OK;
            }
            return Constant.ERR_ERROR;
        }

        public Int32 SendStrings(string strList)
        {
            CData_SendStrings cdSendString = new CData_SendStrings(iSN);
            cdSendString.PackData(strList);
            SendCMD(cdSendString);
            Int32 iTemp = cdSendString.ReceiveData[8] + cdSendString.ReceiveData[9] * 256;
            if (iTemp == 1)
            {//命令成功
                return Constant.ERR_OK;
            }
            return Constant.ERR_ERROR;
        }

    }
}
