using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;


namespace CsLib
{
    public class Cryptography
    {
        public enum CryptMethod { ENCRYPT, DECRYPT }
        public enum CryptClass { AES, RC2, RIJ, DES, TDES }
        public class Generic : IDisposable
        {
            ICryptoTransform cTransform = null;
            byte[] resultArray;
            SymmetricAlgorithm control;

            public Generic(CryptClass className, string key)
            {
                switch (className)
                {
                    case CryptClass.AES:
                        control = new AesManaged();
                        break;
                    case CryptClass.RC2:
                        control = new RC2CryptoServiceProvider();
                        break;
                    case CryptClass.RIJ:
                        control = new RijndaelManaged();
                        break;
                    case CryptClass.DES:
                        control = new DESCryptoServiceProvider();
                        break;
                    case CryptClass.TDES:
                        control = new TripleDESCryptoServiceProvider();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                control.Key = Encoding.UTF8.GetBytes(key);
                control.Padding = PaddingMode.PKCS7;
                control.Mode = CipherMode.ECB;
            }

            ~Generic()
            {
                Dispose(false);
            }

            public string Crypt(CryptMethod method, string input)
            {
                if (method == CryptMethod.ENCRYPT)
                {
                    var toEncryptArray = Encoding.UTF8.GetBytes(input as string);
                    resultArray = Crypt(method, toEncryptArray);
                    return Convert.ToBase64String(resultArray, 0, resultArray.Length);
                }
                else
                {
                    var toEncryptArray = Convert.FromBase64String(input.Replace(" ", "+"));
                    resultArray = Crypt(method, toEncryptArray);
                    return Encoding.UTF8.GetString(resultArray);
                }
            }

            public byte[] Crypt(CryptMethod method, byte[] input)
            {
                cTransform = method == CryptMethod.ENCRYPT ? control.CreateEncryptor() : control.CreateDecryptor();
                resultArray = cTransform.TransformFinalBlock(input, 0, input.Length);
                return resultArray;
            }

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(bool disposing)
            {
                if (_disposed == false)
                {
                    _disposed = true;
                    if (disposing == true)
                    {
                        control.Dispose();
                    }
                }
            }

            private bool _disposed = false;

        }
    }
    //using (Cryptography.Generic myCrypt = new Cryptography.Generic(Cryptography.CryptClass.AES, "KeyTextHere~~~16"))
    //{
    //    string data = "1234";
    //    string encryptedData = myCrypt.Crypt(Cryptography.CryptMethod.ENCRYPT, data);
    //    string decryptedData = myCrypt.Crypt(Cryptography.CryptMethod.DECRYPT, encryptedData);
    //    Console.WriteLine("{0}, {1}, {2}", data, encryptedData, decryptedData);
    //}
}
