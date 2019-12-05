namespace HaTool.Server
{
    partial class UcPublicIp
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBoxOuter = new System.Windows.Forms.GroupBox();
            this.groupBoxServerLocation = new System.Windows.Forms.GroupBox();
            this.comboBoxRegion = new System.Windows.Forms.ComboBox();
            this.labelRegion = new System.Windows.Forms.Label();
            this.comboBoxZone = new System.Windows.Forms.ComboBox();
            this.labelZone = new System.Windows.Forms.Label();
            this.groupBoxPublicIpCreateOrAssociation = new System.Windows.Forms.GroupBox();
            this.dgvPublicIpList = new System.Windows.Forms.DataGridView();
            this.buttonPublicIpListReload = new System.Windows.Forms.Button();
            this.buttonAssociatePublicIpAndServer = new System.Windows.Forms.Button();
            this.buttonDeleteIp = new System.Windows.Forms.Button();
            this.buttonDisassociateIp = new System.Windows.Forms.Button();
            this.buttonCreatePublicIp = new System.Windows.Forms.Button();
            this.groupBoxServerStatus = new System.Windows.Forms.GroupBox();
            this.dgvServerList = new System.Windows.Forms.DataGridView();
            this.buttonGetServerInfo = new System.Windows.Forms.Button();
            this.buttonStartServer = new System.Windows.Forms.Button();
            this.buttonGetPassword = new System.Windows.Forms.Button();
            this.buttonStopServer = new System.Windows.Forms.Button();
            this.buttonTerminateServer = new System.Windows.Forms.Button();
            this.buttonServerListReload = new System.Windows.Forms.Button();
            this.panelIpAndServerList = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBoxOuter.SuspendLayout();
            this.groupBoxServerLocation.SuspendLayout();
            this.groupBoxPublicIpCreateOrAssociation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPublicIpList)).BeginInit();
            this.groupBoxServerStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvServerList)).BeginInit();
            this.panelIpAndServerList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxOuter
            // 
            this.groupBoxOuter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxOuter.Controls.Add(this.panelIpAndServerList);
            this.groupBoxOuter.Controls.Add(this.groupBoxServerLocation);
            this.groupBoxOuter.Location = new System.Drawing.Point(3, 3);
            this.groupBoxOuter.Name = "groupBoxOuter";
            this.groupBoxOuter.Size = new System.Drawing.Size(1307, 849);
            this.groupBoxOuter.TabIndex = 1;
            this.groupBoxOuter.TabStop = false;
            this.groupBoxOuter.Text = "Server > Create IP and Server Management";
            // 
            // groupBoxServerLocation
            // 
            this.groupBoxServerLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxServerLocation.Controls.Add(this.comboBoxRegion);
            this.groupBoxServerLocation.Controls.Add(this.labelRegion);
            this.groupBoxServerLocation.Controls.Add(this.comboBoxZone);
            this.groupBoxServerLocation.Controls.Add(this.labelZone);
            this.groupBoxServerLocation.Location = new System.Drawing.Point(22, 22);
            this.groupBoxServerLocation.Name = "groupBoxServerLocation";
            this.groupBoxServerLocation.Size = new System.Drawing.Size(1276, 82);
            this.groupBoxServerLocation.TabIndex = 161;
            this.groupBoxServerLocation.TabStop = false;
            this.groupBoxServerLocation.Text = "Server Locaion";
            // 
            // comboBoxRegion
            // 
            this.comboBoxRegion.FormattingEnabled = true;
            this.comboBoxRegion.Location = new System.Drawing.Point(16, 46);
            this.comboBoxRegion.Name = "comboBoxRegion";
            this.comboBoxRegion.Size = new System.Drawing.Size(259, 23);
            this.comboBoxRegion.TabIndex = 17;
            this.comboBoxRegion.SelectedIndexChanged += new System.EventHandler(this.ComboBoxRegionChanged);
            // 
            // labelRegion
            // 
            this.labelRegion.AutoSize = true;
            this.labelRegion.Location = new System.Drawing.Point(19, 28);
            this.labelRegion.Name = "labelRegion";
            this.labelRegion.Size = new System.Drawing.Size(49, 15);
            this.labelRegion.TabIndex = 0;
            this.labelRegion.Text = "Region";
            // 
            // comboBoxZone
            // 
            this.comboBoxZone.FormattingEnabled = true;
            this.comboBoxZone.Location = new System.Drawing.Point(282, 46);
            this.comboBoxZone.Name = "comboBoxZone";
            this.comboBoxZone.Size = new System.Drawing.Size(259, 23);
            this.comboBoxZone.TabIndex = 18;
            this.comboBoxZone.SelectedIndexChanged += new System.EventHandler(this.ComboBoxZoneChanged);
            // 
            // labelZone
            // 
            this.labelZone.AutoSize = true;
            this.labelZone.Location = new System.Drawing.Point(285, 28);
            this.labelZone.Name = "labelZone";
            this.labelZone.Size = new System.Drawing.Size(35, 15);
            this.labelZone.TabIndex = 19;
            this.labelZone.Text = "Zone";
            // 
            // groupBoxPublicIpCreateOrAssociation
            // 
            this.groupBoxPublicIpCreateOrAssociation.Controls.Add(this.dgvPublicIpList);
            this.groupBoxPublicIpCreateOrAssociation.Controls.Add(this.buttonPublicIpListReload);
            this.groupBoxPublicIpCreateOrAssociation.Controls.Add(this.buttonAssociatePublicIpAndServer);
            this.groupBoxPublicIpCreateOrAssociation.Controls.Add(this.buttonDeleteIp);
            this.groupBoxPublicIpCreateOrAssociation.Controls.Add(this.buttonDisassociateIp);
            this.groupBoxPublicIpCreateOrAssociation.Controls.Add(this.buttonCreatePublicIp);
            this.groupBoxPublicIpCreateOrAssociation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxPublicIpCreateOrAssociation.Location = new System.Drawing.Point(0, 0);
            this.groupBoxPublicIpCreateOrAssociation.Name = "groupBoxPublicIpCreateOrAssociation";
            this.groupBoxPublicIpCreateOrAssociation.Size = new System.Drawing.Size(1276, 357);
            this.groupBoxPublicIpCreateOrAssociation.TabIndex = 52;
            this.groupBoxPublicIpCreateOrAssociation.TabStop = false;
            this.groupBoxPublicIpCreateOrAssociation.Text = "Public Ip Create Or Association";
            // 
            // dgvPublicIpList
            // 
            this.dgvPublicIpList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPublicIpList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPublicIpList.Location = new System.Drawing.Point(17, 22);
            this.dgvPublicIpList.Name = "dgvPublicIpList";
            this.dgvPublicIpList.Size = new System.Drawing.Size(1244, 295);
            this.dgvPublicIpList.TabIndex = 43;
            // 
            // buttonPublicIpListReload
            // 
            this.buttonPublicIpListReload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonPublicIpListReload.Location = new System.Drawing.Point(17, 323);
            this.buttonPublicIpListReload.Name = "buttonPublicIpListReload";
            this.buttonPublicIpListReload.Size = new System.Drawing.Size(107, 23);
            this.buttonPublicIpListReload.TabIndex = 45;
            this.buttonPublicIpListReload.Text = "Reload";
            this.buttonPublicIpListReload.UseVisualStyleBackColor = true;
            this.buttonPublicIpListReload.Click += new System.EventHandler(this.buttonPublicIpListReload_Click);
            // 
            // buttonAssociatePublicIpAndServer
            // 
            this.buttonAssociatePublicIpAndServer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAssociatePublicIpAndServer.Location = new System.Drawing.Point(469, 323);
            this.buttonAssociatePublicIpAndServer.Name = "buttonAssociatePublicIpAndServer";
            this.buttonAssociatePublicIpAndServer.Size = new System.Drawing.Size(107, 23);
            this.buttonAssociatePublicIpAndServer.TabIndex = 48;
            this.buttonAssociatePublicIpAndServer.Text = "Associate";
            this.buttonAssociatePublicIpAndServer.UseVisualStyleBackColor = true;
            this.buttonAssociatePublicIpAndServer.Click += new System.EventHandler(this.buttonAssociatePublicIpAndServer_Click);
            // 
            // buttonDeleteIp
            // 
            this.buttonDeleteIp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDeleteIp.Location = new System.Drawing.Point(356, 323);
            this.buttonDeleteIp.Name = "buttonDeleteIp";
            this.buttonDeleteIp.Size = new System.Drawing.Size(107, 23);
            this.buttonDeleteIp.TabIndex = 46;
            this.buttonDeleteIp.Text = "Delete";
            this.buttonDeleteIp.UseVisualStyleBackColor = true;
            this.buttonDeleteIp.Click += new System.EventHandler(this.buttonDeleteIp_Click);
            // 
            // buttonDisassociateIp
            // 
            this.buttonDisassociateIp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDisassociateIp.Location = new System.Drawing.Point(243, 323);
            this.buttonDisassociateIp.Name = "buttonDisassociateIp";
            this.buttonDisassociateIp.Size = new System.Drawing.Size(107, 23);
            this.buttonDisassociateIp.TabIndex = 47;
            this.buttonDisassociateIp.Text = "Disassociate";
            this.buttonDisassociateIp.UseVisualStyleBackColor = true;
            this.buttonDisassociateIp.Click += new System.EventHandler(this.buttonDisassociateIp_Click);
            // 
            // buttonCreatePublicIp
            // 
            this.buttonCreatePublicIp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCreatePublicIp.Location = new System.Drawing.Point(130, 323);
            this.buttonCreatePublicIp.Name = "buttonCreatePublicIp";
            this.buttonCreatePublicIp.Size = new System.Drawing.Size(107, 23);
            this.buttonCreatePublicIp.TabIndex = 38;
            this.buttonCreatePublicIp.Text = "Create";
            this.buttonCreatePublicIp.UseVisualStyleBackColor = true;
            this.buttonCreatePublicIp.Click += new System.EventHandler(this.buttonCreatePublicIp_Click);
            // 
            // groupBoxServerStatus
            // 
            this.groupBoxServerStatus.Controls.Add(this.dgvServerList);
            this.groupBoxServerStatus.Controls.Add(this.buttonGetServerInfo);
            this.groupBoxServerStatus.Controls.Add(this.buttonStartServer);
            this.groupBoxServerStatus.Controls.Add(this.buttonGetPassword);
            this.groupBoxServerStatus.Controls.Add(this.buttonStopServer);
            this.groupBoxServerStatus.Controls.Add(this.buttonTerminateServer);
            this.groupBoxServerStatus.Controls.Add(this.buttonServerListReload);
            this.groupBoxServerStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxServerStatus.Location = new System.Drawing.Point(0, 0);
            this.groupBoxServerStatus.Name = "groupBoxServerStatus";
            this.groupBoxServerStatus.Size = new System.Drawing.Size(1276, 372);
            this.groupBoxServerStatus.TabIndex = 51;
            this.groupBoxServerStatus.TabStop = false;
            this.groupBoxServerStatus.Text = "Server List";
            // 
            // dgvServerList
            // 
            this.dgvServerList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvServerList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvServerList.Location = new System.Drawing.Point(17, 25);
            this.dgvServerList.Name = "dgvServerList";
            this.dgvServerList.Size = new System.Drawing.Size(1244, 312);
            this.dgvServerList.TabIndex = 1;
            // 
            // buttonGetServerInfo
            // 
            this.buttonGetServerInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonGetServerInfo.Location = new System.Drawing.Point(469, 343);
            this.buttonGetServerInfo.Name = "buttonGetServerInfo";
            this.buttonGetServerInfo.Size = new System.Drawing.Size(107, 23);
            this.buttonGetServerInfo.TabIndex = 50;
            this.buttonGetServerInfo.Text = "Get Info";
            this.buttonGetServerInfo.UseVisualStyleBackColor = true;
            this.buttonGetServerInfo.Click += new System.EventHandler(this.buttonGetServerInfo_Click);
            // 
            // buttonStartServer
            // 
            this.buttonStartServer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonStartServer.Location = new System.Drawing.Point(243, 343);
            this.buttonStartServer.Name = "buttonStartServer";
            this.buttonStartServer.Size = new System.Drawing.Size(107, 23);
            this.buttonStartServer.TabIndex = 42;
            this.buttonStartServer.Text = "Start";
            this.buttonStartServer.UseVisualStyleBackColor = true;
            this.buttonStartServer.Click += new System.EventHandler(this.buttonStartServer_Click);
            // 
            // buttonGetPassword
            // 
            this.buttonGetPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonGetPassword.Location = new System.Drawing.Point(582, 343);
            this.buttonGetPassword.Name = "buttonGetPassword";
            this.buttonGetPassword.Size = new System.Drawing.Size(107, 23);
            this.buttonGetPassword.TabIndex = 49;
            this.buttonGetPassword.Text = "Get Password";
            this.buttonGetPassword.UseVisualStyleBackColor = true;
            this.buttonGetPassword.Click += new System.EventHandler(this.buttonGetPassword_Click);
            // 
            // buttonStopServer
            // 
            this.buttonStopServer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonStopServer.Location = new System.Drawing.Point(130, 343);
            this.buttonStopServer.Name = "buttonStopServer";
            this.buttonStopServer.Size = new System.Drawing.Size(107, 23);
            this.buttonStopServer.TabIndex = 40;
            this.buttonStopServer.Text = "Stop";
            this.buttonStopServer.UseVisualStyleBackColor = true;
            this.buttonStopServer.Click += new System.EventHandler(this.buttonStopServer_Click);
            // 
            // buttonTerminateServer
            // 
            this.buttonTerminateServer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonTerminateServer.Location = new System.Drawing.Point(356, 343);
            this.buttonTerminateServer.Name = "buttonTerminateServer";
            this.buttonTerminateServer.Size = new System.Drawing.Size(107, 23);
            this.buttonTerminateServer.TabIndex = 41;
            this.buttonTerminateServer.Text = "Terminate";
            this.buttonTerminateServer.UseVisualStyleBackColor = true;
            this.buttonTerminateServer.Click += new System.EventHandler(this.buttonTerminateServer_Click);
            // 
            // buttonServerListReload
            // 
            this.buttonServerListReload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonServerListReload.Location = new System.Drawing.Point(17, 343);
            this.buttonServerListReload.Name = "buttonServerListReload";
            this.buttonServerListReload.Size = new System.Drawing.Size(107, 23);
            this.buttonServerListReload.TabIndex = 39;
            this.buttonServerListReload.Text = "Reload";
            this.buttonServerListReload.UseVisualStyleBackColor = true;
            this.buttonServerListReload.Click += new System.EventHandler(this.buttonServerListReload_Click);
            // 
            // panelIpAndServerList
            // 
            this.panelIpAndServerList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelIpAndServerList.Controls.Add(this.splitContainer1);
            this.panelIpAndServerList.Location = new System.Drawing.Point(22, 110);
            this.panelIpAndServerList.Name = "panelIpAndServerList";
            this.panelIpAndServerList.Size = new System.Drawing.Size(1276, 733);
            this.panelIpAndServerList.TabIndex = 162;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBoxPublicIpCreateOrAssociation);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBoxServerStatus);
            this.splitContainer1.Size = new System.Drawing.Size(1276, 733);
            this.splitContainer1.SplitterDistance = 357;
            this.splitContainer1.TabIndex = 0;
            // 
            // UcPublicIp
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.groupBoxOuter);
            this.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.Name = "UcPublicIp";
            this.Size = new System.Drawing.Size(1320, 859);
            this.Load += new System.EventHandler(this.LoadData);
            this.groupBoxOuter.ResumeLayout(false);
            this.groupBoxServerLocation.ResumeLayout(false);
            this.groupBoxServerLocation.PerformLayout();
            this.groupBoxPublicIpCreateOrAssociation.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPublicIpList)).EndInit();
            this.groupBoxServerStatus.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvServerList)).EndInit();
            this.panelIpAndServerList.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBoxOuter;
        private System.Windows.Forms.DataGridView dgvServerList;
        private System.Windows.Forms.Button buttonCreatePublicIp;
        private System.Windows.Forms.Button buttonServerListReload;
        private System.Windows.Forms.Button buttonStopServer;
        private System.Windows.Forms.Button buttonTerminateServer;
        private System.Windows.Forms.Button buttonStartServer;
        private System.Windows.Forms.Button buttonPublicIpListReload;
        private System.Windows.Forms.DataGridView dgvPublicIpList;
        private System.Windows.Forms.Button buttonDeleteIp;
        private System.Windows.Forms.Button buttonDisassociateIp;
        private System.Windows.Forms.Button buttonAssociatePublicIpAndServer;
        private System.Windows.Forms.Button buttonGetServerInfo;
        private System.Windows.Forms.Button buttonGetPassword;
        private System.Windows.Forms.GroupBox groupBoxServerStatus;
        private System.Windows.Forms.GroupBox groupBoxPublicIpCreateOrAssociation;
        private System.Windows.Forms.GroupBox groupBoxServerLocation;
        private System.Windows.Forms.ComboBox comboBoxRegion;
        private System.Windows.Forms.Label labelRegion;
        private System.Windows.Forms.ComboBox comboBoxZone;
        private System.Windows.Forms.Label labelZone;
        private System.Windows.Forms.Panel panelIpAndServerList;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}
