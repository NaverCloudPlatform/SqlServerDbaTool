using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CsLib
{
    public class DicWrapper<K, V>
    {

        private object lockobj = new object();

        public Dictionary<K, V> Data { get; set; } = new Dictionary<K, V>();

        public DicWrapper() { }

        public void Insert(K key, V value)
        {
            lock (lockobj)
            {
                if (!Data.ContainsKey(key))
                    Data.Add(key, value);
                else
                    Update(key, value);
            }
        }

        public void Update(K key, V value)
        {
            lock (lockobj)
            {
                V oldValue;
                if (Data.TryGetValue(key, out oldValue))
                {
                    var oldProperties = oldValue.GetType().GetProperties();
                    var newProperies = value.GetType().GetProperties();
                    foreach (var o in oldProperties)
                    {
                        foreach (var n in newProperies)
                        {
                            if (o.Name == n.Name)
                            {
                                string newPropertyValue = value.GetType().GetProperty(n.Name).GetValue(value, null).ToString();
                                if (!newPropertyValue.Equals("NULL"))
                                    oldValue.GetType().GetProperty(o.Name).SetValue(oldValue, newPropertyValue, null);
                            }
                        }
                    }
                }
                Data[key] = oldValue;
            }
        }


        public void Truncate()
        {
            Data.Clear();
        }

        public void Delete(K key)
        {
            lock (lockobj)
            {
                if (Data.ContainsKey(key))
                {
                    Data.Remove(key);
                }
            }
        }

        public string GetJson()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.Formatting = Formatting.Indented;
            settings.ContractResolver = new DictionaryAsArrayResolver();
            return JsonConvert.SerializeObject(Data, settings);
        }

        class DictionaryAsArrayResolver : DefaultContractResolver
        {
            protected override JsonContract CreateContract(Type objectType)
            {
                if (objectType.GetInterfaces().Any(i => i == typeof(IDictionary) ||
                   (i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IDictionary<,>))))
                {
                    return base.CreateArrayContract(objectType);
                }
                return base.CreateContract(objectType);
            }
        }
    }
}




//public void Test()
//{
//    DataManager<ServerKey, ServerValue> dataManager = new DataManager<ServerKey, ServerValue>();

//    dataManager.Insert(
//            new ServerKey { ServerName = "ServerA" },
//            new ServerValue { ServerPrivateIp = "pri1", ServerPublicIp = "pub1" });
//    dataManager.Insert(
//        new ServerKey { ServerName = "ServerB" },
//        new ServerValue { ServerPrivateIp = "pri2", ServerPublicIp = "pub2" });
//    dataManager.Insert(
//        new ServerKey { ServerName = "ServerB" },
//        new ServerValue { ServerPrivateIp = "pri3", ServerPublicIp = "pub5" });
//    dataManager.Update(
//        new ServerKey { ServerName = "ServerB" },
//        new ServerValue { ServerPrivateIp = "pri3" });
//    dataManager.Insert(
//        new ServerKey { ServerName = "ServerC" },
//        new ServerValue { ServerPrivateIp = "pri3", ServerPublicIp = "pub3" });
//    dataManager.Delete(
//        new ServerKey { ServerName = "ServerA" });

//    // SerializeObject
//    string json = dataManager.GetJson();
//    Console.WriteLine(json);

//    // DeserializeObject
//    var tempobject = JsonConvert.DeserializeObject<List<KeyValuePair<ServerKey, ServerValue>>>(json);
//    dataManager.Data.Clear();

//    foreach (var a in tempobject)
//    {
//        dataManager.Insert(
//            new ServerKey { ServerName = a.Key.ServerName },
//            new ServerValue { ServerPrivateIp = a.Value.ServerPrivateIp, ServerPublicIp = a.Value.ServerPublicIp });
//    }

//    // data confirm 
//    foreach (var a in dataManager.Data)
//    {
//        Console.WriteLine($"{a.Key.ServerName}, {a.Value.ServerPrivateIp}, {a.Value.ServerPublicIp}");
//    }

//    ServerValue sv = dataManager.Data[new ServerKey { ServerName = "ServerB" }];
//    Console.WriteLine($"{sv.ServerPrivateIp}, {sv.ServerPublicIp}");
//    Console.ReadKey();
//}