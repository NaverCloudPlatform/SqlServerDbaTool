namespace HaTool
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.configToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.encryptionKeyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.objectStorageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loginKeyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.initScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkConfigurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serverToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.publicIPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setAgentKeyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setSqlServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setServerDiskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.highAvailabilityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadBalancerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mirroringToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.failoverPolicyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.databaseBackupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.monitoringToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.perfmonPolicyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sqlmonPolicyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sqlExecuterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.executerMultiSqlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.agentRestCallToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nCPAPIExecuterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.encoderDecoderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelMain = new System.Windows.Forms.Panel();
            this.pictureBoxMain = new System.Windows.Forms.PictureBox();
            this.menuStrip.SuspendLayout();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMain)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configToolStripMenuItem,
            this.serverToolStripMenuItem,
            this.highAvailabilityToolStripMenuItem,
            this.monitoringToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.infoToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1187, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // configToolStripMenuItem
            // 
            this.configToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.encryptionKeyToolStripMenuItem,
            this.objectStorageToolStripMenuItem,
            this.loginKeyToolStripMenuItem,
            this.initScriptToolStripMenuItem,
            this.checkConfigurationToolStripMenuItem});
            this.configToolStripMenuItem.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.configToolStripMenuItem.Name = "configToolStripMenuItem";
            this.configToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.configToolStripMenuItem.Text = "Config";
            // 
            // encryptionKeyToolStripMenuItem
            // 
            this.encryptionKeyToolStripMenuItem.Name = "encryptionKeyToolStripMenuItem";
            this.encryptionKeyToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.encryptionKeyToolStripMenuItem.Text = "Encryption Key";
            this.encryptionKeyToolStripMenuItem.Click += new System.EventHandler(this.encryptionKeyToolStripMenuItem_Click);
            // 
            // objectStorageToolStripMenuItem
            // 
            this.objectStorageToolStripMenuItem.Name = "objectStorageToolStripMenuItem";
            this.objectStorageToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.objectStorageToolStripMenuItem.Text = "Object Storage";
            this.objectStorageToolStripMenuItem.Click += new System.EventHandler(this.objectStorageSettingToolStripMenuItem_Click);
            // 
            // loginKeyToolStripMenuItem
            // 
            this.loginKeyToolStripMenuItem.Name = "loginKeyToolStripMenuItem";
            this.loginKeyToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.loginKeyToolStripMenuItem.Text = "Login Key";
            this.loginKeyToolStripMenuItem.Click += new System.EventHandler(this.loginKeyToolStripMenuItem_Click);
            // 
            // initScriptToolStripMenuItem
            // 
            this.initScriptToolStripMenuItem.Name = "initScriptToolStripMenuItem";
            this.initScriptToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.initScriptToolStripMenuItem.Text = "Init Script";
            this.initScriptToolStripMenuItem.Click += new System.EventHandler(this.initScriptToolStripMenuItem_Click);
            // 
            // checkConfigurationToolStripMenuItem
            // 
            this.checkConfigurationToolStripMenuItem.Name = "checkConfigurationToolStripMenuItem";
            this.checkConfigurationToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.checkConfigurationToolStripMenuItem.Text = "Configuration Check";
            this.checkConfigurationToolStripMenuItem.Click += new System.EventHandler(this.configurationCheckToolStripMenuItem_Click);
            // 
            // serverToolStripMenuItem
            // 
            this.serverToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createServerToolStripMenuItem,
            this.publicIPToolStripMenuItem,
            this.setAgentKeyToolStripMenuItem,
            this.setSqlServerToolStripMenuItem,
            this.setServerDiskToolStripMenuItem});
            this.serverToolStripMenuItem.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.serverToolStripMenuItem.Name = "serverToolStripMenuItem";
            this.serverToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.serverToolStripMenuItem.Text = "Server";
            // 
            // createServerToolStripMenuItem
            // 
            this.createServerToolStripMenuItem.Name = "createServerToolStripMenuItem";
            this.createServerToolStripMenuItem.Size = new System.Drawing.Size(291, 22);
            this.createServerToolStripMenuItem.Text = "Create Server";
            this.createServerToolStripMenuItem.Click += new System.EventHandler(this.createServerToolStripMenuItem_Click);
            // 
            // publicIPToolStripMenuItem
            // 
            this.publicIPToolStripMenuItem.Name = "publicIPToolStripMenuItem";
            this.publicIPToolStripMenuItem.Size = new System.Drawing.Size(291, 22);
            this.publicIPToolStripMenuItem.Text = "Create IP and Server Management";
            this.publicIPToolStripMenuItem.Click += new System.EventHandler(this.publicIPToolStripMenuItem_Click);
            // 
            // setAgentKeyToolStripMenuItem
            // 
            this.setAgentKeyToolStripMenuItem.Name = "setAgentKeyToolStripMenuItem";
            this.setAgentKeyToolStripMenuItem.Size = new System.Drawing.Size(291, 22);
            this.setAgentKeyToolStripMenuItem.Text = "Set Agent Key";
            this.setAgentKeyToolStripMenuItem.Click += new System.EventHandler(this.setAgentKeyToolStripMenuItem_Click);
            // 
            // setSqlServerToolStripMenuItem
            // 
            this.setSqlServerToolStripMenuItem.Name = "setSqlServerToolStripMenuItem";
            this.setSqlServerToolStripMenuItem.Size = new System.Drawing.Size(291, 22);
            this.setSqlServerToolStripMenuItem.Text = "Set Sql Server";
            this.setSqlServerToolStripMenuItem.Click += new System.EventHandler(this.setSqlServerToolStripMenuItem_Click);
            // 
            // setServerDiskToolStripMenuItem
            // 
            this.setServerDiskToolStripMenuItem.Name = "setServerDiskToolStripMenuItem";
            this.setServerDiskToolStripMenuItem.Size = new System.Drawing.Size(291, 22);
            this.setServerDiskToolStripMenuItem.Text = "Set Server Disk";
            this.setServerDiskToolStripMenuItem.Click += new System.EventHandler(this.setServerDiskToolStripMenuItem_Click);
            // 
            // highAvailabilityToolStripMenuItem
            // 
            this.highAvailabilityToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadBalancerToolStripMenuItem,
            this.mirroringToolStripMenuItem,
            this.failoverPolicyToolStripMenuItem,
            this.databaseBackupToolStripMenuItem});
            this.highAvailabilityToolStripMenuItem.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.highAvailabilityToolStripMenuItem.Name = "highAvailabilityToolStripMenuItem";
            this.highAvailabilityToolStripMenuItem.Size = new System.Drawing.Size(138, 20);
            this.highAvailabilityToolStripMenuItem.Text = "High Availability";
            // 
            // loadBalancerToolStripMenuItem
            // 
            this.loadBalancerToolStripMenuItem.Name = "loadBalancerToolStripMenuItem";
            this.loadBalancerToolStripMenuItem.Size = new System.Drawing.Size(333, 22);
            this.loadBalancerToolStripMenuItem.Text = "Create Load Balancer and Set HA Group";
            this.loadBalancerToolStripMenuItem.Click += new System.EventHandler(this.loadBalancerToolStripMenuItem_Click);
            // 
            // mirroringToolStripMenuItem
            // 
            this.mirroringToolStripMenuItem.Name = "mirroringToolStripMenuItem";
            this.mirroringToolStripMenuItem.Size = new System.Drawing.Size(333, 22);
            this.mirroringToolStripMenuItem.Text = "Database Mirroring";
            this.mirroringToolStripMenuItem.Click += new System.EventHandler(this.mirroringToolStripMenuItem_Click);
            // 
            // failoverPolicyToolStripMenuItem
            // 
            this.failoverPolicyToolStripMenuItem.Name = "failoverPolicyToolStripMenuItem";
            this.failoverPolicyToolStripMenuItem.Size = new System.Drawing.Size(333, 22);
            this.failoverPolicyToolStripMenuItem.Text = "Failover Policy";
            this.failoverPolicyToolStripMenuItem.Click += new System.EventHandler(this.failoverPolicyToolStripMenuItem_Click);
            // 
            // databaseBackupToolStripMenuItem
            // 
            this.databaseBackupToolStripMenuItem.Name = "databaseBackupToolStripMenuItem";
            this.databaseBackupToolStripMenuItem.Size = new System.Drawing.Size(333, 22);
            this.databaseBackupToolStripMenuItem.Text = "Backup Policy";
            this.databaseBackupToolStripMenuItem.Click += new System.EventHandler(this.databaseBackupToolStripMenuItem_Click);
            // 
            // monitoringToolStripMenuItem
            // 
            this.monitoringToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.perfmonPolicyToolStripMenuItem,
            this.sqlmonPolicyToolStripMenuItem});
            this.monitoringToolStripMenuItem.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.monitoringToolStripMenuItem.Name = "monitoringToolStripMenuItem";
            this.monitoringToolStripMenuItem.Size = new System.Drawing.Size(89, 20);
            this.monitoringToolStripMenuItem.Text = "Monitoring";
            // 
            // perfmonPolicyToolStripMenuItem
            // 
            this.perfmonPolicyToolStripMenuItem.Name = "perfmonPolicyToolStripMenuItem";
            this.perfmonPolicyToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.perfmonPolicyToolStripMenuItem.Text = "Perfmon Policy";
            this.perfmonPolicyToolStripMenuItem.Click += new System.EventHandler(this.perfmonPolicyToolStripMenuItem_Click);
            // 
            // sqlmonPolicyToolStripMenuItem
            // 
            this.sqlmonPolicyToolStripMenuItem.Name = "sqlmonPolicyToolStripMenuItem";
            this.sqlmonPolicyToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.sqlmonPolicyToolStripMenuItem.Text = "Sqlmon Policy";
            this.sqlmonPolicyToolStripMenuItem.Click += new System.EventHandler(this.sqlmonPolicyToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sqlExecuterToolStripMenuItem,
            this.executerMultiSqlToolStripMenuItem,
            this.agentRestCallToolStripMenuItem,
            this.nCPAPIExecuterToolStripMenuItem,
            this.encoderDecoderToolStripMenuItem});
            this.toolsToolStripMenuItem.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // sqlExecuterToolStripMenuItem
            // 
            this.sqlExecuterToolStripMenuItem.Name = "sqlExecuterToolStripMenuItem";
            this.sqlExecuterToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.sqlExecuterToolStripMenuItem.Text = "Executer Sql";
            this.sqlExecuterToolStripMenuItem.Click += new System.EventHandler(this.executerSqlToolStripMenuItem_Click);
            // 
            // executerMultiSqlToolStripMenuItem
            // 
            this.executerMultiSqlToolStripMenuItem.Name = "executerMultiSqlToolStripMenuItem";
            this.executerMultiSqlToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.executerMultiSqlToolStripMenuItem.Text = "Executer Multi Server Sql";
            this.executerMultiSqlToolStripMenuItem.Click += new System.EventHandler(this.executerMultiSqlToolStripMenuItem_Click);
            // 
            // agentRestCallToolStripMenuItem
            // 
            this.agentRestCallToolStripMenuItem.Name = "agentRestCallToolStripMenuItem";
            this.agentRestCallToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.agentRestCallToolStripMenuItem.Text = "Executer Agent";
            this.agentRestCallToolStripMenuItem.Click += new System.EventHandler(this.executerAgentToolStripMenuItem_Click);
            // 
            // nCPAPIExecuterToolStripMenuItem
            // 
            this.nCPAPIExecuterToolStripMenuItem.Name = "nCPAPIExecuterToolStripMenuItem";
            this.nCPAPIExecuterToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.nCPAPIExecuterToolStripMenuItem.Text = "Executer Ncp Api";
            this.nCPAPIExecuterToolStripMenuItem.Click += new System.EventHandler(this.ncpApiExecuterToolStripMenuItem_Click);
            // 
            // encoderDecoderToolStripMenuItem
            // 
            this.encoderDecoderToolStripMenuItem.Name = "encoderDecoderToolStripMenuItem";
            this.encoderDecoderToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.encoderDecoderToolStripMenuItem.Text = "Encoder Decoder";
            this.encoderDecoderToolStripMenuItem.Click += new System.EventHandler(this.encoderDecoderToolStripMenuItem_Click);
            // 
            // infoToolStripMenuItem
            // 
            this.infoToolStripMenuItem.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.infoToolStripMenuItem.Name = "infoToolStripMenuItem";
            this.infoToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.infoToolStripMenuItem.Text = "Info";
            this.infoToolStripMenuItem.Click += new System.EventHandler(this.infoToolStripMenuItem_Click);
            // 
            // panelMain
            // 
            this.panelMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(247)))), ((int)(((byte)(248)))));
            this.panelMain.Controls.Add(this.pictureBoxMain);
            this.panelMain.Location = new System.Drawing.Point(12, 37);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(1165, 651);
            this.panelMain.TabIndex = 1;
            // 
            // pictureBoxMain
            // 
            this.pictureBoxMain.Location = new System.Drawing.Point(3, 3);
            this.pictureBoxMain.Name = "pictureBoxMain";
            this.pictureBoxMain.Size = new System.Drawing.Size(1159, 644);
            this.pictureBoxMain.TabIndex = 0;
            this.pictureBoxMain.TabStop = false;
            // 
            // FormMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1187, 697);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.menuStrip);
            this.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "FormMain";
            this.Text = "SQL Server DBA Tool (NAVER Cloud Platform)";
            this.Load += new System.EventHandler(this.LoadData);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem configToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem objectStorageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem serverToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem publicIPToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setAgentKeyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setSqlServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setServerDiskToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem highAvailabilityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem encryptionKeyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadBalancerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mirroringToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem agentRestCallToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sqlExecuterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem infoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem encoderDecoderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loginKeyToolStripMenuItem;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.PictureBox pictureBoxMain;
        private System.Windows.Forms.ToolStripMenuItem initScriptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nCPAPIExecuterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem failoverPolicyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem databaseBackupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem monitoringToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem perfmonPolicyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sqlmonPolicyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem executerMultiSqlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkConfigurationToolStripMenuItem;
    }
}

