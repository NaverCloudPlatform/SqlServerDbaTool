using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using CsLib; 
namespace LogClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Log log = Log.Instance;
            Config config = Config.Instance; 
            

            Console.WriteLine("GetCryptionKey Result : " + config.GetCryptionKey(
                    config.GetValue(Category.Encryption, Key.GetCryptionKey),
                    config.GetValue(Category.Encryption, Key.GetCryptionKeyUrl),
                    config.GetValue(Category.Api, Key.AccessKey),
                    config.GetValue(Category.Api, Key.SecretKey)
                ));

            HealthReporter.Instance.Start();

            Console.WriteLine("public IP Address : {0}", Common.GetLocalIpAddress(IpType.LocalFirstIp));

            for (int i = 0; i < 10000; i++)
            {
                log.Info(string.Format("logData hello {0} ", i));
                log.Warn(string.Format("logData hello {0} ", i));
                log.Error(string.Format("logData hello {0} ", i));
                //log.Error(JsonC1.Instance.Format(i.ToString()));
                Console.WriteLine(i);
                //log.Error(JsonC2.Instance.Format("a", i.ToString()));
                //log.Error(JsonC3.Instance.Format("a", "a", i.ToString()));
                //log.Error(JsonC4.Instance.Format("a", "a", "a", i.ToString()));
                //log.Error(JsonC5.Instance.Format("a", "a", "a", "a", i.ToString()));

                Thread.Sleep(1000);
            }
        }
    }

}
