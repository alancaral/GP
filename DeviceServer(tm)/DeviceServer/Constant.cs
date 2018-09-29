using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeviceServer
{
    public static class Constant
    {
        public static int ERR_ERROR = 0;
        public static int ERR_OK = 1;

        public static int ERR_EXCEPTION = 100; 
        public static int ERR_SOCKET_EXCEPTION = 101;
        public static int ERR_IO_EXCEPTION = 102;
        public static int ERR_OBJECT_DISPOSED_EXCEPTION = 103;

        public static int ERR_SEND_STRINGS = 200;
    }
}
