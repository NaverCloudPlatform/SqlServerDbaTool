using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Newtonsoft.Json;

namespace LogClient
{
    public class JsonC2
    {
        public static JsonC2 Instance { get { return lazy.Value; } }
        private static readonly Lazy<JsonC2> lazy =
            new Lazy<JsonC2>(() => new JsonC2(), LazyThreadSafetyMode.ExecutionAndPublication);

        public string c1 { get; set; }
        public string c2 { get; set; }

        public string Format(string c1, string c2)
        {
            this.c1 = c1;
            this.c2 = c2;

            return JsonConvert.SerializeObject(this);
        }
    }
}
