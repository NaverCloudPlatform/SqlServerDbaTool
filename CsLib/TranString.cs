using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;
using CsLib;
using System.Web;
using System.Text.RegularExpressions;

namespace CsLib
{
    public static class TranString
    {

        public static SecureString convertToSecureString(string strPassword)
        {
            var secureStr = new SecureString();
            if (strPassword.Length > 0) foreach (var c in strPassword.ToCharArray()) secureStr.AppendChar(c);
            return secureStr;
        }

        public static string EncodeBase64Unicode(string value)
        {
            try
            {
                byte[] bytes = Encoding.Unicode.GetBytes(value);
                return Convert.ToBase64String(bytes);
            }
            catch (Exception )
            {
                throw ;
            }
        }

        public static string DecodeBase64Unicode(string value)
        {
            try
            {
                return Encoding.Unicode.GetString(Convert.FromBase64String(value));
            }
            catch (Exception )
            {
                throw ;
            }
        }

        public static string EncodeBase64(string value)
        {
            string returnValue = string.Empty;
            try
            {
                byte[] bytes = Encoding.ASCII.GetBytes(value);
                return Convert.ToBase64String(bytes);

            }
            catch (Exception )
            {
                throw ;
            }
        }

        public static string DecodeBase64(string value)
        {
            try
            {
                return Encoding.ASCII.GetString(Convert.FromBase64String(value));
            }
            catch (Exception )
            {
                throw ;
            }
        }

        public static string EncodeAES(string value, string key)
        {
            try
            {
                using (Cryptography.Generic Cryption = new Cryptography.Generic(Cryptography.CryptClass.AES, key))
                {
                    return Cryption.Crypt(Cryptography.CryptMethod.ENCRYPT, value);
                }
            }
            catch (Exception )
            {
                throw ;
            }
        }

        public static string DecodeAES(string value, string key)
        {
            try
            {
                using (Cryptography.Generic Cryption = new Cryptography.Generic(Cryptography.CryptClass.AES, key))
                {
                    return Cryption.Crypt(Cryptography.CryptMethod.DECRYPT, value);
                }
            }
            catch (Exception )
            {
                throw ;
            }

        }

        public static string EncodeRijndael(string value, string key, string vector = "")
        {
            try
            {
                Cryption c = new Cryption(key.Trim());
                return c.Encrypt(value.Trim(), vector.Trim());
            }
            catch (Exception )
            {
                throw ;
            }

        }

        public static string DecodeRijndael(string value, string key, string vector = "")
        {
            try
            {
                Cryption c = new Cryption(key.Trim());
                return c.Decrypt(value.Trim(), vector.Trim());
            }
            catch (Exception )
            {
                throw ;
            }
        }

        public static string EncodeUrlEncode(string value)
        {
            try
            {
                return HttpUtility.UrlEncode(value);
            }
            catch (Exception )
            {
                throw ;
            }
        }

        public static string DecodeUrlDecode(string value)
        {
            try
            {
                return HttpUtility.UrlDecode(value);
            }
            catch (Exception )
            {
                throw ;
            }
        }

        public static List<string> ReadQuery(string queryAll)
        {
            List<string> querys = new List<string>();
            try
            {

                string query = string.Empty;
                string[] lines = Regex.Split(queryAll, Environment.NewLine);
                foreach (string line in lines)
                {
                    if (line.Trim().Equals("go", StringComparison.InvariantCultureIgnoreCase))
                    {
                        if (query.Trim() != "")
                            querys.Add(query);

                        query = string.Empty;
                    }
                    else
                    {
                        if (line != "")
                            query = query + Environment.NewLine + line;
                    }
                }
                if (query.Trim() != "")
                    querys.Add(query);
            }
            catch (Exception)
            {
                throw;
            }
            return querys;
        }

    }
}
