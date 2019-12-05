using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Newtonsoft.Json;

namespace LogClient
{
    public class JsonC3
    {
        public static JsonC3 Instance { get { return lazy.Value; } }
        private static readonly Lazy<JsonC3> lazy =
            new Lazy<JsonC3>(() => new JsonC3(), LazyThreadSafetyMode.ExecutionAndPublication);

        public string c1 { get; set; }
        public string c2 { get; set; }
        public string c3 { get; set; }

        public string Format(string c1, string c2, string c3)
        {
            this.c1 = c1;
            this.c2 = c2;
            this.c3 = c3;

            return JsonConvert.SerializeObject(this);
        }
    }
}
