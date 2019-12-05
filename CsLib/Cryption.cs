using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace CsLib
{
    
    public class Cryption
    {
        //https://github.com/Pakhee/Cross-platform-AES-encryption/blob/master/C-Sharp/CryptLib.cs
        UTF8Encoding _enc;
        RijndaelManaged _rcipher;
        byte[] _key, _pwd, _ivBytes, _iv;
        private string key;

        enum EncryptMode { ENCRYPT, DECRYPT };

        public Cryption(string key)
        {
            _enc = new UTF8Encoding();
            _rcipher = new RijndaelManaged();
            _rcipher.Mode = CipherMode.CBC;
            _rcipher.Padding = PaddingMode.PKCS7;
            _rcipher.KeySize = 128;
            _rcipher.BlockSize = 128;
            _key = new byte[16];
            _iv = new byte[_rcipher.BlockSize / 8]; //128 bit / 8 = 16 bytes
            _ivBytes = new byte[16];
            this.key = key;
        }

        public string Encrypt(string _plainText, string _iv)
        {
            string iv = ("0123456789012345" + _iv);
            iv = iv.Substring(iv.Length - 16);
            return EncryptDecrypt(_plainText, key, EncryptMode.ENCRYPT, iv);
        }

        public string Decrypt(string _encryptedText, string _iv)
        {
            string iv = ("0123456789012345" + _iv);
            iv = iv.Substring(iv.Length - 16);
            return EncryptDecrypt(_encryptedText, key, EncryptMode.DECRYPT, iv);
        }

        private string EncryptDecrypt(string _inputText, string _encryptionKey, EncryptMode _mode, string _initVector)
        {
            try
            {
                string _out = "";
                _pwd = Encoding.UTF8.GetBytes(_encryptionKey);
                _ivBytes = Encoding.UTF8.GetBytes(_initVector);

                int len = _pwd.Length;
                if (len > _key.Length)
                {
                    len = _key.Length;
                }
                int ivLenth = _ivBytes.Length;
                if (ivLenth > _iv.Length)
                {
                    ivLenth = _iv.Length;
                }

                Array.Copy(_pwd, _key, len);
                Array.Copy(_ivBytes, _iv, ivLenth);
                _rcipher.Key = _key;
                _rcipher.IV = _iv;

                if (_mode.Equals(EncryptMode.ENCRYPT))
                {
                    byte[] plainText = _rcipher.CreateEncryptor().TransformFinalBlock(_enc.GetBytes(_inputText), 0, _inputText.Length);
                    _out = Convert.ToBase64String(plainText);
                }
                if (_mode.Equals(EncryptMode.DECRYPT))
                {
                    byte[] plainText = _rcipher.CreateDecryptor().TransformFinalBlock(Convert.FromBase64String(_inputText), 0, Convert.FromBase64String(_inputText).Length);
                    _out = _enc.GetString(plainText);
                }
                _rcipher.Dispose();
                return _out;
            }
            catch (Exception ) { throw ; }
        }

    }
}
