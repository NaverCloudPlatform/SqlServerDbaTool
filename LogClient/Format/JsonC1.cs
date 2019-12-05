using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Newtonsoft.Json;

namespace LogClient
{
    public class JsonC1
    {
        public static JsonC1 Instance { get { return lazy.Value; } }
        private static readonly Lazy<JsonC1> lazy =
            new Lazy<JsonC1>(() => new JsonC1(), LazyThreadSafetyMode.ExecutionAndPublication);

        public string c1 { get; set; }

        public string Format(string c1)
        {
            this.c1 = c1;

            return JsonConvert.SerializeObject(this);
        }
    }
}
