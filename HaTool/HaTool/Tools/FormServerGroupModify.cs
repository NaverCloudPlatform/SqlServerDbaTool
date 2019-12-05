using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using HaTool.Global;
using HaTool.Model;
using HaTool.Config;
using CsLib;
using Newtonsoft.Json;
using LogClient;
using Newtonsoft.Json.Linq;
using System.IO; 

namespace HaTool.Tools
{
    public partial class FormServerGroupModify : Form
    {
        private static readonly Lazy<FormServerGroupModify> lazy =
            new Lazy<FormServerGroupModify>(() => new FormServerGroupModify(), LazyThreadSafetyMode.ExecutionAndPublication);
        public static FormServerGroupModify Instance { get { return lazy.Value; } }

        LogClient.Config logClientConfig = LogClient.Config.Instance;
        DataManager dataManager = DataManager.Instance;
        public event EventHandler<ScriptArgs> ScriptModifyEvent;

        public string GroupBoxText
        {
            set
            {
                groupBox.Text = value;
            }
            get
            {
                return groupBox.Text; 
            }
        }

        public string Script
        {
            set
            {
                fastColoredTextBoxScript.Text = value;
            }
            get
            {
                return fastColoredTextBoxScript.Text;
            }
        }

        public string FileName { get; set; }


        public FormServerGroupModify()
        {
            InitializeComponent();
        }

        private void StatusChange(string value)
        {
            buttonExecute.InvokeIfRequired(async s => {
                s.Invalidate();
                s.Text = value.ToString();
                s.Update();
                var task = Task.Delay(200);
                await task;
                s.Hide();
                s.Refresh();
                s.Show();
                Application.DoEvents();
            });
        }

        private void buttonSaveAndClose_Click(object sender, EventArgs e)
        {
            

            if (File.Exists(FileName))
                File.Delete(FileName);

            File.WriteAllText(FileName, Script);
            ScriptModifyEvent?.Invoke(this, new ScriptArgs { Script = fastColoredTextBoxScript.Text });
            Close();
        }

    }
}
