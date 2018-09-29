using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeviceServer
{
    enum eCommand
    {
        CMD_GET_MSG_COUNT = 0x030B,
        CMD_GET_MSG_INFO = 0x030C
    }

    class CData
    {
        public short sSOC = 0x4353;
        public short sEOC = 0x4345;
        private Int32 isn = 0;
        public Int32 SN
        {
            get { return isn; }
            set { isn = value; }
        }

        private short sCMD = 0;
        public short CMD
        {
            get { return sCMD; }
            set { sCMD = value; }
        }

        private Int32 iSendDataLength = 0;
        public Int32 SendDataLength
        {
            get { return iSendDataLength; }
            set { iSendDataLength = value; }
        }
        private Int32 iRecvDataLength = 0;
        public Int32 ReceiveDataLength
        {
            get { return iRecvDataLength; }
            set { iRecvDataLength = value; }
        }
        private short sCheckSum = 0;
        public short CheckSum
        {
            get { return sCheckSum; }
            set { sCheckSum = value; }
        }

        public byte[] SendData;
        public byte[] ReceiveData;

        private Int32 iRecvSize;
        public Int32 RecvDataSize
        {
            get { return iRecvSize; }
            set { iRecvSize = value; }
        }

        /*public CData(eCommand eCMDType,Int32 iPrinterSN)
        {
            sCMD = (short)eCMDType;
            isn = iPrinterSN;
        }
        */

        public void AddShort(Int32 iPos, short sValue)
        {
            SendData[iPos] = (byte)(sValue % 256);
            short sTemp = (short)(sValue >> 8) ;
            byte btemp = (byte)(sTemp % 256);
            SendData[iPos + 1] = btemp;
        }

        public void AddInt(Int32 iPos, Int32 sValue)
        {
            SendData[iPos] = (byte)(sValue % 256);
            SendData[iPos + 1] = (byte)((sValue / 256)%256);
            SendData[iPos + 2] = (byte)((sValue / 256/256) % 256);
            SendData[iPos + 3] = (byte)((sValue / 256/256/256) % 256);
        }

        public virtual void PackData(){}
        /*
         * {switch (sCMD)
            {
                case 0x030B://eCommand.CMD_GET_MSG_COUNT：
                    {
                        //计算发送数据长度
                        int ilen = 21;
                        iSendDataLength = ilen;
                        SendData = new byte[ilen+1];
                        iRecvSize = 32;
                        ReceiveData = new byte[iRecvSize];
                        AddShort(0, sSOC);
                        AddShort(2, 0);
                        AddInt(4, isn);
                        AddShort(8, sCMD);
                        AddShort(10, 7);
                        AddShort(12, 5);
                        SendData[14] = 0x4D;
                        SendData[15] = 0x53;
                        SendData[16] = 0x47;
                        SendData[17] = 0x5C;
                        SendData[18] = 0x0;
                        AddShort(19, sEOC);
                    }
                    break;
                default:
                    {

                    }
                    break;
            }
            AddCheckSum();
        }*/
        /// <summary>
        /// 根据SendData里的数据计算出校验码，写入SendData 
        /// </summary>
        /// <param name="iLength">数据总长</param>
        /// <returns></returns>
        public void AddCheckSum()
        {
            Int32 iTotal=0;
            Int32 iTimes = (iSendDataLength - 6) / 2;
            Int32 iSupplyZero = (iSendDataLength - 6) % 2;
            for(int i = 0;i<iTimes;i++)
            {
                Int32 iUnit = SendData[4+i * 2] + SendData[4+i * 2 + 1] * 256;
                iTotal += iUnit;
            }

            if (iSupplyZero > 0)
            {
                iTotal += SendData[4+iTimes * 2];
            }

            short sCheckSum = (short)(iTotal & 0xFFFF);
            AddShort(2, sCheckSum);
        }

    }

    class CData_MsgCount : CData
    {
        public CData_MsgCount(Int32 iPrinterSN)
        {
            CMD = 0x030B;
            SN = iPrinterSN;
        }

        public void PackData() 
        {
            //计算发送数据长度
            int ilen = 21;
            SendDataLength = ilen;
            SendData = new byte[ilen+1];
            RecvDataSize = 32;
            ReceiveData = new byte[RecvDataSize];
            AddShort(0, sSOC);
            AddShort(2, 0);
            AddInt(4, SN);
            AddShort(8, CMD);
            AddShort(10, 7);
            AddShort(12, 5);
            SendData[14] = 0x4D;
            SendData[15] = 0x53;
            SendData[16] = 0x47;
            SendData[17] = 0x5C;
            SendData[18] = 0x0;
            AddShort(19, sEOC);
           
            AddCheckSum();
        }
    }

    class CData_MsgInfo : CData
    {
        public CData_MsgInfo(Int32 iPrinterSN)
        {
            CMD = 0x030C;
            SN = iPrinterSN;
        }

        public void PackData(short shMsgIndex)
        {
            //计算发送数据长度
            int ilen = 23;
            SendDataLength = ilen;
            SendData = new byte[ilen + 1];
            RecvDataSize = 80; 
            ReceiveData = new byte[RecvDataSize];
            AddShort(0, sSOC);
            AddShort(2, 0);
            AddInt(4, SN);
            AddShort(8, CMD);
            AddShort(10, 9);
            AddShort(12, shMsgIndex);
            AddShort(14, 5);
            SendData[16] = 0x4D;
            SendData[17] = 0x53;
            SendData[18] = 0x47;
            SendData[19] = 0x5C;
            SendData[20] = 0x0;
            AddShort(21, sEOC);

            AddCheckSum();
        }
    }

    class CData_PrintingStatus : CData
    {
        public CData_PrintingStatus(Int32 iPrinterSN)
        {
            CMD = 0x0202;
            SN = iPrinterSN;
        }

        public void PackData()
        {
            //计算发送数据长度
            int ilen = 14;
            SendDataLength = ilen;
            SendData = new byte[ilen + 1];
            RecvDataSize = 32;
            ReceiveData = new byte[RecvDataSize];
            AddShort(0, sSOC);
            AddShort(2, 0);
            AddInt(4, SN);
            AddShort(8, CMD);
            AddShort(10, 0);
            AddShort(12, sEOC);

            AddCheckSum();
        }
    }
    
    class CData_StopPrint : CData
    {
        public CData_StopPrint(Int32 iPrinterSN)
        {
            CMD = 0x0306;
            SN = iPrinterSN;
        }

        public void PackData()
        {
            //计算发送数据长度
            int ilen = 14;
            SendDataLength = ilen;
            SendData = new byte[ilen + 1];
            RecvDataSize = 32;
            ReceiveData = new byte[RecvDataSize];
            AddShort(0, sSOC);
            AddShort(2, 0);
            AddInt(4, SN);
            AddShort(8, CMD);
            AddShort(10, 0);
            AddShort(12, sEOC);

            AddCheckSum();
        }
    }

    class CData_StartPrint : CData
    {
        public CData_StartPrint(Int32 iPrinterSN)
        {
            CMD = 0x0305;
            SN = iPrinterSN;
        }

        public void PackData(string strName)
        {
            //计算资料名称
            short sNameLen = (short)strName.Length;
            byte[] bName = Encoding.ASCII.GetBytes(strName);
             

            //计算发送数据长度
            int ilen = 14 + sNameLen + 3;
            SendDataLength = ilen;
            SendData = new byte[ilen + 1];
            RecvDataSize = 32;
            ReceiveData = new byte[RecvDataSize];
            AddShort(0, sSOC);
            AddShort(2, 0);
            AddInt(4, SN);
            AddShort(8, CMD);
            AddShort(10, (short)(sNameLen + 1 + 2));//length
            AddShort(12, (short)(sNameLen + 1));//size
            for (int i = 0; i < sNameLen; i++)
            {
                SendData[14 + i] = bName[i];
            }
            SendData[14 + sNameLen] = 0;
            AddShort(15+sNameLen, sEOC);

            AddCheckSum();
        }
    }

    class CData_SendStrings : CData
    {
        public CData_SendStrings(Int32 iPrinterSN)
        {
            CMD = 0x0307;
            SN = iPrinterSN;
        }

        public void PackData(string strContent)
        {
            //计算资料名称
            //short sNameLen = (short)strContent.Length;
            //byte[] bName = Encoding.ASCII.GetBytes(strContent);
            //byte[] btsendStrings = System.Text.Encoding.ASCII.GetBytes(strStrings);
            string[] requestMessage = strContent.Split(',');
            int iCount = requestMessage.Length;
            
            //计算发送数据长度
            int ilen = 14 + 2;
            SendDataLength = ilen;
            SendData = new byte[512];
            RecvDataSize = 32;
            ReceiveData = new byte[RecvDataSize];
            AddShort(0, sSOC);
            AddShort(2, 0);
            AddInt(4, SN);
            AddShort(8, CMD);
            //AddShort(10, (short)(sNameLen + 1 + 2));//length
            AddShort(12, (short)iCount);
            int pos = 14;
            for (int n = 0; n < iCount; n++)
            {
                short sLen = (short)requestMessage[n].Length;
                if (sLen == 0) continue;
                AddShort(pos, (short)(sLen + 1)); pos += 2;//size
                byte[] bStr = Encoding.ASCII.GetBytes(requestMessage[n]);
                for (int i = 0; i < sLen; i++)
                {
                    SendData[pos++] = bStr[i];
                }
                SendData[pos++] = 0;
            }
            AddShort(pos, sEOC);
            pos += 2; //pos = total length
            AddShort(10, (short)(pos-12-2));
            SendDataLength = pos;

            AddCheckSum();
        }
    }
}
