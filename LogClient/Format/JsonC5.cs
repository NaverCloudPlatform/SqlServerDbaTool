using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Newtonsoft.Json;

namespace LogClient
{
    public class JsonC5
    {
        public static JsonC5 Instance { get { return lazy.Value; } }
        private static readonly Lazy<JsonC5> lazy =
            new Lazy<JsonC5>(() => new JsonC5(), LazyThreadSafetyMode.ExecutionAndPublication);

        public string c1 { get; set; }
        public string c2 { get; set; }
        public string c3 { get; set; }
        public string c4 { get; set; }
        public string c5 { get; set; }


        public string Format(string c1, string c2, string c3, string c4, string c5)
        {
            this.c1 = c1;
            this.c2 = c2;
            this.c3 = c3;
            this.c4 = c4;
            this.c5 = c5; 

            return JsonConvert.SerializeObject(this);
        }
    }
}
