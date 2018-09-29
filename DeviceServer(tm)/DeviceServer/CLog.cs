using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace DeviceServer
{
    public static class CLog
    {
        private static bool bInLog = false;
        public static void  SaveLog(string strText)
        {
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
            try
            {


                if (!File.Exists(str))
                {
                    FileStream fs = File.Create(str);
                    fs.Close();
                }

                string strSave = DateTime.Now.ToString("hh:mm:ss") + "  " + strText;

                FileStream fstream = new FileStream(str, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                using (StreamWriter sw = new StreamWriter(fstream))
                {
                    sw.WriteLine(strSave);
                    sw.Close();
                    fstream.Close();
                }
            }
            catch (Exception ie)
            {
                //应该没办法记录了吧。
            }
            finally
            {
                bInLog = false;
            }
            /*StreamWriter sw = new StreamWriter(str, true, System.Text.Encoding.UTF8);
            string strSave = DateTime.Now.ToString("hh:mm:ss") + "  " + strText;
            sw.WriteLine(strSave);
            sw.Close();*/


            
        }

        public static void SaveException(string strTitle,string strText)
        {
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

            str += "Exception" + DateTime.Now.ToString("yyyyMMdd");
            str += ".txt";

            if (!File.Exists(str))
            {
                FileStream fs = File.Create(str);
                fs.Close();
            }

            StreamWriter sw = new StreamWriter(str, true, System.Text.Encoding.UTF8);
            string strSave = DateTime.Now.ToString("hh:mm:ss") + "  " + strText;
            sw.WriteLine(strTitle);
            sw.WriteLine(strSave);
            sw.WriteLine();
            sw.Close();

            bInLog = false;
        }

    }
}
