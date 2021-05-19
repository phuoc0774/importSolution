using System;
using System.Collections.Generic;
using System.Text;

namespace ModelClassLibrary.unit.filter
{
    public class StoreData
    {
        public int storeid { get; set; }
        public int processid { get; set; }
        public string storename { get; set; }
        public int process { get; set; }
        public int position { get; set; }
        public Boolean status { get; set; }
        public string note { get; set; }
        public int groupstore { get; set; }

       // public List<VariableData> listvariable { get; set; }
       public string startday { get; set; }
        public string endday { get; set; }
    }
}
