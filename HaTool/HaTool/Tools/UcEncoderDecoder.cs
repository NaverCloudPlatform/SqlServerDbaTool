using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using HaTool.Config;
using HaTool.Global;
using CsLib;
using Newtonsoft.Json;
using HaTool.Model.NCloud;
using LogClient;
using Newtonsoft.Json.Linq;
using HaTool.Model;

namespace HaTool.Tools
{
    public partial class UcEncoderDecoder : UserControl
    {
        enum Algorithm { Base64Unicode, Base64, UrlEncode, AES, Rijndael }
        private Algorithm algorithm = Algorithm.Base64Unicode;
        private static readonly Lazy<UcEncoderDecoder> lazy =
            new Lazy<UcEncoderDecoder>(() => new UcEncoderDecoder(), LazyThreadSafetyMode.ExecutionAndPublication);
        public static UcEncoderDecoder Instance { get { return lazy.Value; } }

        public UcEncoderDecoder()
        {
            InitializeComponent();
            radioButtonBase64Unicode.Checked = true; 
            algorithm = Algorithm.Base64Unicode;
            textBoxKey.Enabled = false;
            textBoxRijndaelKey.Enabled = false;
            textBoxRijndaelVector.Enabled = false;
        }

        private void buttonEncode_Click(object sender, EventArgs e)
        {
            try
            {
                switch (algorithm)
                {
                    case Algorithm.Base64Unicode:
                        textBoxEncode.Text = TranString.EncodeBase64Unicode(textBoxDecode.Text);
                        break;
                    case Algorithm.Base64:
                        textBoxEncode.Text = TranString.EncodeBase64(textBoxDecode.Text);
                        break;
                    case Algorithm.UrlEncode:
                        textBoxEncode.Text = TranString.EncodeUrlEncode(textBoxDecode.Text);
                        break;
                    case Algorithm.AES:
                        textBoxEncode.Text = TranString.EncodeAES(textBoxDecode.Text, textBoxKey.Text);
                        break;
                    case Algorithm.Rijndael:
                        textBoxEncode.Text = TranString.EncodeRijndael(textBoxDecode.Text, textBoxRijndaelKey.Text, textBoxRijndaelVector.Text);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException($"algorithm : {algorithm}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonDecode_Click(object sender, EventArgs e)
        {
            try
            {
                switch (algorithm)
                {
                    case Algorithm.Base64Unicode:
                        textBoxDecode.Text = TranString.DecodeBase64Unicode(textBoxEncode.Text);
                        break;
                    case Algorithm.Base64:
                        textBoxDecode.Text = TranString.DecodeBase64(textBoxEncode.Text);
                        break;
                    case Algorithm.UrlEncode:
                        textBoxDecode.Text = TranString.DecodeUrlDecode(textBoxEncode.Text);
                        break;
                    case Algorithm.AES:
                        textBoxDecode.Text = TranString.DecodeAES(textBoxEncode.Text, textBoxKey.Text);
                        break;
                    case Algorithm.Rijndael:
                        textBoxDecode.Text = TranString.DecodeRijndael(textBoxEncode.Text, textBoxRijndaelKey.Text, textBoxRijndaelVector.Text);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException($"algorithm : {algorithm}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void radioButtonAlgorithm_Checked(object sender, EventArgs e)
        {
            RadioButton r = (RadioButton)sender;
            if (r.Name.Equals("radioButtonBase64Unicode"))
            {
                algorithm = Algorithm.Base64Unicode;
                textBoxKey.Enabled = false;
                textBoxRijndaelKey.Enabled = false;
                textBoxRijndaelVector.Enabled = false;
            }
            if (r.Name.Equals("radioButtonBase64Ascii"))
            {
                algorithm = Algorithm.Base64;
                textBoxKey.Enabled = false;
                textBoxRijndaelKey.Enabled = false;
                textBoxRijndaelVector.Enabled = false;
            }
            if (r.Name.Equals("radioButtonUrlEncode"))
            {
                algorithm = Algorithm.UrlEncode;
                textBoxKey.Enabled = false;
                textBoxRijndaelKey.Enabled = false;
                textBoxRijndaelVector.Enabled = false;
            }
            if (r.Name.Equals("radioButtonAes"))
            {
                algorithm = Algorithm.AES;
                textBoxKey.Enabled = true;
                textBoxRijndaelKey.Enabled = false;
                textBoxRijndaelVector.Enabled = false;
            }
            if (r.Name.Equals("radioButtonAesRijndael"))
            {
                algorithm = Algorithm.Rijndael;
                textBoxKey.Enabled = false;
                textBoxRijndaelKey.Enabled = true;
                textBoxRijndaelVector.Enabled = true;
            }

        }
    }
}
