using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

namespace CsLib
{
    public enum IpType { SocketConnet, LocalFirstIp, LocalAllIp }

    public static class Common
    {
        public static string GetLocalIpAddress(IpType ipType)
        {
            string ip = string.Empty;
            StringBuilder sb = new StringBuilder();
            IPHostEntry ipEntry = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress[] addr = addr = ipEntry.AddressList;
            switch (ipType)
            {
                case IpType.LocalAllIp:
                    for (int i = 0; i < addr.Length; i++)
                    {
                        sb.Append(addr[i].ToString());
                        sb.Append(",");
                    }
                    ip = sb.ToString().TrimEnd(',');
                    break;
                case IpType.LocalFirstIp:
                    for (int i = 0; i < addr.Length; i++)
                    {
                        if (
                            addr[i].ToString().Length > 4
                            && addr[i].ToString() != "127.0.0.1"
                            && addr[i].ToString().Substring(0, Math.Min(6, addr[i].ToString().Length)) != "0.0.0."
                            && addr[i].AddressFamily.ToString() != "InterNetworkV6"
                            )
                        {
                            ip = addr[i].ToString();
                            break;
                        }
                    }
                    break;
                case IpType.SocketConnet:
                    using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
                    {
                        socket.Connect("8.8.8.8", 65530);
                        IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                        ip = endPoint.Address.ToString();
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
                    
            }
            return ip;
        }

        public static void FileDeleteFromDate(string folder, string searchPattern, TimeSpan timeSpan)
        {
            DateTime targetDate = DateTime.Now.Subtract(timeSpan);

            string[] files = Directory.GetFiles(folder, searchPattern);
            foreach (var file in files)
            {
                try
                {
                    DateTime lastModified = File.GetLastWriteTime(file);
                    if (lastModified < targetDate)
                        File.Delete(file);
                }
                catch (Exception ) { throw ; }
            }
        }

        public static void FileLogWriteLine(string logFileFullname, string message)
        {
            try
            {
                File.AppendAllText(logFileFullname, "::: " + DateTime.Now.ToString("yyyyMMddHHmmss") + " ::: " + message + Environment.NewLine);
            }
            catch (Exception) { throw; }
        }


        public static List<string> ReadQuery(string listStringQuerys)
        {
            List<string> querys = new List<string>();
            try
            {
                string query = string.Empty;
                string[] lines = Regex.Split(listStringQuerys, "\r\n");
                foreach (string line in lines)
                {
                    if (line.Trim().Equals("go", StringComparison.OrdinalIgnoreCase))
                    {
                        if (query.Trim() != "")
                            querys.Add(query);

                        query = string.Empty;
                    }
                    else
                    {
                        if (line != "")
                            query = query + "\r\n" + line;
                    }
                }
                if (query.Trim() != "")
                    querys.Add(query);
            }
            catch (Exception) { throw; }

            return querys;
        }


        public static bool QueryExecuter(string connectionString, string listStringQuerys, int commandTimeout = 5)
        {
            bool bReturn = false;
            List<string> querys = ReadQuery(listStringQuerys);
            foreach (var query in querys)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = query;
                            cmd.CommandTimeout = commandTimeout;
                            cmd.ExecuteNonQuery();
                        }
                        conn.Close();
                        bReturn = true;
                    }
                }
                catch(Exception) { throw; }
            }
            return bReturn;
        }

        public static T DatabaseValue<T>(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return default(T);
            else
                return (T)obj;
        }
    }
}