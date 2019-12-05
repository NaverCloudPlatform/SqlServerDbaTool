using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HaTool.Server;
using HaTool.HighAvailability;
using HaTool.Monitoring;
using HaTool.Config;
using HaTool.Tools;

namespace HaTool
{
    public partial class FormMain : Form
    {
        
        public FormMain()
        {
            InitializeComponent();
            Bitmap icon = new Bitmap (Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "img", "___32.png"));
            this.Icon = Icon.FromHandle(icon.GetHicon());
        }



        private void encryptionKeyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormEncryptionKey formEncryptionKey = FormEncryptionKey.Instance;
            formEncryptionKey.StartPosition = FormStartPosition.CenterScreen;
            formEncryptionKey.ShowDialog();
        }

        private void objectStorageSettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormObjectStorage formObjectStorage = FormObjectStorage.Instance;
            formObjectStorage.StartPosition = FormStartPosition.CenterScreen;
            formObjectStorage.ShowDialog();
        }
        
        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveAllUc();
            panelMain.BringToFront();
            pictureBoxMain.Location = new Point(0, 0);
            pictureBoxMain.Image = Image.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "img", "main.PNG"));
            pictureBoxMain.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBoxMain.BringToFront();
        }

        private void publicIPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveAllUc();

            if (!panelMain.Controls.Contains(UcPublicIp.Instance))
            {
                panelMain.Controls.Add(UcPublicIp.Instance);
                UcPublicIp.Instance.Dock = DockStyle.Fill;
                UcPublicIp.Instance.BringToFront();
            }
            else
                UcPublicIp.Instance.BringToFront();

        }

        private void RemoveAllUc()
        {
            if (panelMain.Controls.Contains(UcCreateServer.Instance))
                panelMain.Controls.Remove(UcCreateServer.Instance);
            if (panelMain.Controls.Contains(UcPublicIp.Instance))
                panelMain.Controls.Remove(UcPublicIp.Instance);
            if (panelMain.Controls.Contains(UcSetAgentKey.Instance))
                panelMain.Controls.Remove(UcSetAgentKey.Instance);
            if (panelMain.Controls.Contains(UcSetSqlServer.Instance))
                panelMain.Controls.Remove(UcSetSqlServer.Instance);
            if (panelMain.Controls.Contains(UcSetServerDisk.Instance))
                panelMain.Controls.Remove(UcSetServerDisk.Instance);
            if (panelMain.Controls.Contains(UcLoadBalancer.Instance))
                panelMain.Controls.Remove(UcLoadBalancer.Instance);
            if (panelMain.Controls.Contains(UcExecuterSql.Instance))
                panelMain.Controls.Remove(UcExecuterSql.Instance);
            if (panelMain.Controls.Contains(UcMirroring.Instance))
                panelMain.Controls.Remove(UcMirroring.Instance);
            if (panelMain.Controls.Contains(UcEncoderDecoder.Instance))
                panelMain.Controls.Remove(UcEncoderDecoder.Instance);
            if (panelMain.Controls.Contains(UcExecuterAgent.Instance))
                panelMain.Controls.Remove(UcExecuterAgent.Instance);
            if (panelMain.Controls.Contains(UcExecuterNcpApi.Instance))
                panelMain.Controls.Remove(UcExecuterNcpApi.Instance);
            if (panelMain.Controls.Contains(UcFailoverPolicy.Instance))
                panelMain.Controls.Remove(UcFailoverPolicy.Instance);
            if (panelMain.Controls.Contains(UcBackupPolicy.Instance))
                panelMain.Controls.Remove(UcBackupPolicy.Instance);
            if (panelMain.Controls.Contains(UcPerfmonPolicy.Instance))
                panelMain.Controls.Remove(UcPerfmonPolicy.Instance);
            if (panelMain.Controls.Contains(UcSqlmonPolicy.Instance))
                panelMain.Controls.Remove(UcSqlmonPolicy.Instance);
            if (panelMain.Controls.Contains(UcExecuterMultiSql.Instance))
                panelMain.Controls.Remove(UcExecuterMultiSql.Instance);

        }

        private void loginKeyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormLoginKey formLoginKey = FormLoginKey.Instance;
            formLoginKey.StartPosition = FormStartPosition.CenterScreen;
            formLoginKey.ShowDialog();
        }

        private void initScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormInitScript formInitScript = FormInitScript.Instance;
            formInitScript.StartPosition = FormStartPosition.CenterScreen;
            formInitScript.ShowDialog();
        }

        private void createServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveAllUc();

            if (!panelMain.Controls.Contains(UcCreateServer.Instance))
            {
                panelMain.Controls.Add(UcCreateServer.Instance);
                UcCreateServer.Instance.Dock = DockStyle.Fill;
                UcCreateServer.Instance.BringToFront();
            }
            else
                UcCreateServer.Instance.BringToFront();
        }

        private void LoadData(object sender, EventArgs e)
        {
            pictureBoxMain.Location = new Point(0, 0);
            pictureBoxMain.Image = Image.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "img", "main.PNG"));
            pictureBoxMain.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBoxMain.BringToFront();
        }

        private void setAgentKeyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveAllUc();
            
            if (!panelMain.Controls.Contains(UcSetAgentKey.Instance))
            {
                panelMain.Controls.Add(UcSetAgentKey.Instance);
                UcSetAgentKey.Instance.Dock = DockStyle.Fill;
                UcSetAgentKey.Instance.BringToFront();
            }
            else
                UcSetAgentKey.Instance.BringToFront();
        }

        private void setSqlServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveAllUc();

            if (!panelMain.Controls.Contains(UcSetSqlServer.Instance))
            {
                panelMain.Controls.Add(UcSetSqlServer.Instance);
                UcSetSqlServer.Instance.Dock = DockStyle.Fill;
                UcSetSqlServer.Instance.BringToFront();
            }
            else
                UcSetSqlServer.Instance.BringToFront();
        }

        private void setServerDiskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveAllUc();

            if (!panelMain.Controls.Contains(UcSetServerDisk.Instance))
            {
                panelMain.Controls.Add(UcSetServerDisk.Instance);
                UcSetServerDisk.Instance.Dock = DockStyle.Fill;
                UcSetServerDisk.Instance.BringToFront();
            }
            else
                UcSetServerDisk.Instance.BringToFront();
        }

        private void loadBalancerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveAllUc();

            if (!panelMain.Controls.Contains(UcLoadBalancer.Instance))
            {
                panelMain.Controls.Add(UcLoadBalancer.Instance);
                UcLoadBalancer.Instance.Dock = DockStyle.Fill;
                UcLoadBalancer.Instance.BringToFront();
            }
            else
                UcLoadBalancer.Instance.BringToFront();
        }

        private void executerSqlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveAllUc();

            if (!panelMain.Controls.Contains(UcExecuterSql.Instance))
            {
                panelMain.Controls.Add(UcExecuterSql.Instance);
                UcExecuterSql.Instance.Dock = DockStyle.Fill;
                UcExecuterSql.Instance.BringToFront();
            }
            else
                UcExecuterSql.Instance.BringToFront();
        }

        private void mirroringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveAllUc();

            if (!panelMain.Controls.Contains(UcMirroring.Instance))
            {
                panelMain.Controls.Add(UcMirroring.Instance);
                UcMirroring.Instance.Dock = DockStyle.Fill;
                UcMirroring.Instance.BringToFront();
            }
            else
                UcMirroring.Instance.BringToFront();
        }

        private void encoderDecoderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveAllUc();

            if (!panelMain.Controls.Contains(UcEncoderDecoder.Instance))
            {
                panelMain.Controls.Add(UcEncoderDecoder.Instance);
                UcEncoderDecoder.Instance.Dock = DockStyle.Fill;
                UcEncoderDecoder.Instance.BringToFront();
            }
            else
                UcEncoderDecoder.Instance.BringToFront();
        }

        private void executerAgentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveAllUc();

            if (!panelMain.Controls.Contains(UcExecuterAgent.Instance))
            {
                panelMain.Controls.Add(UcExecuterAgent.Instance);
                UcExecuterAgent.Instance.Dock = DockStyle.Fill;
                UcExecuterAgent.Instance.BringToFront();
            }
            else
                UcExecuterAgent.Instance.BringToFront();
        }

        private void ncpApiExecuterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveAllUc();

            if (!panelMain.Controls.Contains(UcExecuterNcpApi.Instance))
            {
                panelMain.Controls.Add(UcExecuterNcpApi.Instance);
                UcExecuterNcpApi.Instance.Dock = DockStyle.Fill;
                UcExecuterNcpApi.Instance.BringToFront();
            }
            else
                UcExecuterNcpApi.Instance.BringToFront();
        }

        private void failoverPolicyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveAllUc();

            if (!panelMain.Controls.Contains(UcFailoverPolicy.Instance))
            {
                panelMain.Controls.Add(UcFailoverPolicy.Instance);
                UcFailoverPolicy.Instance.Dock = DockStyle.Fill;
                UcFailoverPolicy.Instance.BringToFront();
            }
            else
                UcFailoverPolicy.Instance.BringToFront();
        }

        private void databaseBackupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveAllUc();

            if (!panelMain.Controls.Contains(UcBackupPolicy.Instance))
            {
                panelMain.Controls.Add(UcBackupPolicy.Instance);
                UcBackupPolicy.Instance.Dock = DockStyle.Fill;
                UcBackupPolicy.Instance.BringToFront();
            }
            else
                UcBackupPolicy.Instance.BringToFront();
        }

        private void perfmonPolicyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveAllUc();

            if (!panelMain.Controls.Contains(UcPerfmonPolicy.Instance))
            {
                panelMain.Controls.Add(UcPerfmonPolicy.Instance);
                UcPerfmonPolicy.Instance.Dock = DockStyle.Fill;
                UcPerfmonPolicy.Instance.BringToFront();
            }
            else
                UcPerfmonPolicy.Instance.BringToFront();
        }

        private void sqlmonPolicyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveAllUc();

            if (!panelMain.Controls.Contains(UcSqlmonPolicy.Instance))
            {
                panelMain.Controls.Add(UcSqlmonPolicy.Instance);
                UcSqlmonPolicy.Instance.Dock = DockStyle.Fill;
                UcSqlmonPolicy.Instance.BringToFront();
            }
            else
                UcSqlmonPolicy.Instance.BringToFront();
        }

        private void executerMultiSqlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveAllUc();

            if (!panelMain.Controls.Contains(UcExecuterMultiSql.Instance))
            {
                panelMain.Controls.Add(UcExecuterMultiSql.Instance);
                UcExecuterMultiSql.Instance.Dock = DockStyle.Fill;
                UcExecuterMultiSql.Instance.BringToFront();
            }
            else
                UcExecuterMultiSql.Instance.BringToFront();
        }

        private void configurationCheckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveAllUc();

            FormConfigurationCheck formCheckConfiguration = FormConfigurationCheck.Instance;
            formCheckConfiguration.StartPosition = FormStartPosition.CenterScreen;
            formCheckConfiguration.ShowDialog();
        }
    }
}
