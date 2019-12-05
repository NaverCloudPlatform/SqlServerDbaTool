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

namespace HaTool.Server
{
    public partial class FormPreview : Form
    {
        private static readonly Lazy<FormPreview> lazy =
            new Lazy<FormPreview>(() => new FormPreview(), LazyThreadSafetyMode.ExecutionAndPublication);
        public static FormPreview Instance { get { return lazy.Value; } }

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

        public FormPreview()
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

        private void buttonClose_Click(object sender, EventArgs e)
        {
            ScriptModifyEvent?.Invoke(this, new ScriptArgs { Script = fastColoredTextBoxScript.Text });
            Close();
        }


    }
}
