using System;
using System.Collections.Generic;
using System.Text;

namespace ModelClassLibrary.common
{
    public class DataRespond
    {
        public Boolean success { get; set; }
        public string controllername { get; set; }
        public dynamic data { get; set; }
        public string message { get; set; }
        public dynamic error { get; set; }
    }
}
