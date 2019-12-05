using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lazylog
{
    public class PerfmonData
    {
        public string GUID { get; set; }
        public int CounterID { get; set; }
        public int RecordIndex { get; set; }
        public string CounterDateTime { get; set; }
        public float CounterValue { get; set; }
        public int FirstValueA { get; set; }
        public int FirstValueB { get; set; }
        public int SecondValueA { get; set; }
        public int SecondValueB { get; set; }
        public int MultiCount { get; set; }
    }

    class PerfmonTypeA
    {
        public string Ip { get; set; }
        public string MachineName { get; set; }
        public string ObjectName { get; set; }
        public string CounterName { get; set; }
        public string InstanceName { get; set; }
        public string CounterValue { get; set; }
        public string CounterDateTime { get; set; }
    }



}
