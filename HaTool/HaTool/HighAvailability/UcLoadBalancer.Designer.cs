namespace HaTool.HighAvailability
{
    partial class UcLoadBalancer
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
            this.groupBoxLoadBalancer = new System.Windows.Forms.GroupBox();
            this.groupBoxServer = new System.Windows.Forms.GroupBox();
            this.comboBoxMasterServer = new System.Windows.Forms.ComboBox();
            this.buttonDelHA = new System.Windows.Forms.Button();
            this.labelSlaveServer = new System.Windows.Forms.Label();
            this.buttonSetHA = new System.Windows.Forms.Button();
            this.labelMasterServer = new System.Windows.Forms.Label();
            this.comboBoxSlaveServer = new System.Windows.Forms.ComboBox();
            this.groupBoxSqlServerConfigurationTemplate = new System.Windows.Forms.GroupBox();
            this.buttonShowCheckedLBDetailInfo = new System.Windows.Forms.Button();
            this.buttonLoadBalancerNameCheck = new System.Windows.Forms.Button();
            this.buttonDbDelete = new System.Windows.Forms.Button();
            this.labelZone = new System.Windows.Forms.Label();
            this.comboBoxRegion = new System.Windows.Forms.ComboBox();
            this.comboBoxZone = new System.Windows.Forms.ComboBox();
            this.labelRegion = new System.Windows.Forms.Label();
            this.buttonDbSave = new System.Windows.Forms.Button();
            this.comboBoxProtocol = new System.Windows.Forms.ComboBox();
            this.buttonLoadTemplate = new System.Windows.Forms.Button();
            this.textBoxServerPort = new System.Windows.Forms.TextBox();
            this.buttonSaveTemplate = new System.Windows.Forms.Button();
            this.labelServerPort = new System.Windows.Forms.Label();
            this.labelProtocol = new System.Windows.Forms.Label();
            this.textBoxLoadBalancerName = new System.Windows.Forms.TextBox();
            this.textBoxLoadBalancerPort = new System.Windows.Forms.TextBox();
            this.labelCurrentAccessKey = new System.Windows.Forms.Label();
            this.labelLoadBalancerPort = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonCreateLoadBalancer = new System.Windows.Forms.Button();
            this.buttonServerListReload = new System.Windows.Forms.Button();
            this.buttonShowLBDetail = new System.Windows.Forms.Button();
            this.dgvloadBalancerList = new System.Windows.Forms.DataGridView();
            this.buttonLoadBalancerListReload = new System.Windows.Forms.Button();
            this.buttonDeleteLoadBalancer = new System.Windows.Forms.Button();
            this.groupBoxLoadBalancer.SuspendLayout();
            this.groupBoxServer.SuspendLayout();
            this.groupBoxSqlServerConfigurationTemplate.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvloadBalancerList)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxLoadBalancer
            // 
            this.groupBoxLoadBalancer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxLoadBalancer.Controls.Add(this.groupBoxServer);
            this.groupBoxLoadBalancer.Controls.Add(this.groupBoxSqlServerConfigurationTemplate);
            this.groupBoxLoadBalancer.Controls.Add(this.groupBox1);
            this.groupBoxLoadBalancer.Location = new System.Drawing.Point(1, 3);
            this.groupBoxLoadBalancer.Name = "groupBoxLoadBalancer";
            this.groupBoxLoadBalancer.Size = new System.Drawing.Size(894, 694);
            this.groupBoxLoadBalancer.TabIndex = 1;
            this.groupBoxLoadBalancer.TabStop = false;
            this.groupBoxLoadBalancer.Text = "High Availability > Create Load Balancer and Set HA Group";
            // 
            // groupBoxServer
            // 
            this.groupBoxServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxServer.Controls.Add(this.comboBoxMasterServer);
            this.groupBoxServer.Controls.Add(this.buttonDelHA);
            this.groupBoxServer.Controls.Add(this.labelSlaveServer);
            this.groupBoxServer.Controls.Add(this.buttonSetHA);
            this.groupBoxServer.Controls.Add(this.labelMasterServer);
            this.groupBoxServer.Controls.Add(this.comboBoxSlaveServer);
            this.groupBoxServer.Location = new System.Drawing.Point(22, 579);
            this.groupBoxServer.Name = "groupBoxServer";
            this.groupBoxServer.Size = new System.Drawing.Size(853, 109);
            this.groupBoxServer.TabIndex = 68;
            this.groupBoxServer.TabStop = false;
            this.groupBoxServer.Text = "HA Allocation Server";
            // 
            // comboBoxMasterServer
            // 
            this.comboBoxMasterServer.FormattingEnabled = true;
            this.comboBoxMasterServer.Location = new System.Drawing.Point(23, 41);
            this.comboBoxMasterServer.Name = "comboBoxMasterServer";
            this.comboBoxMasterServer.Size = new System.Drawing.Size(316, 23);
            this.comboBoxMasterServer.TabIndex = 61;
            // 
            // buttonDelHA
            // 
            this.buttonDelHA.Location = new System.Drawing.Point(142, 70);
            this.buttonDelHA.Name = "buttonDelHA";
            this.buttonDelHA.Size = new System.Drawing.Size(118, 23);
            this.buttonDelHA.TabIndex = 66;
            this.buttonDelHA.Text = "Del HA";
            this.buttonDelHA.UseVisualStyleBackColor = true;
            this.buttonDelHA.Click += new System.EventHandler(this.buttonDelHA_Click);
            // 
            // labelSlaveServer
            // 
            this.labelSlaveServer.AutoSize = true;
            this.labelSlaveServer.Location = new System.Drawing.Point(350, 23);
            this.labelSlaveServer.Name = "labelSlaveServer";
            this.labelSlaveServer.Size = new System.Drawing.Size(91, 15);
            this.labelSlaveServer.TabIndex = 64;
            this.labelSlaveServer.Text = "Slave Server";
            // 
            // buttonSetHA
            // 
            this.buttonSetHA.Location = new System.Drawing.Point(23, 70);
            this.buttonSetHA.Name = "buttonSetHA";
            this.buttonSetHA.Size = new System.Drawing.Size(118, 23);
            this.buttonSetHA.TabIndex = 65;
            this.buttonSetHA.Text = "Set HA";
            this.buttonSetHA.UseVisualStyleBackColor = true;
            this.buttonSetHA.Click += new System.EventHandler(this.buttonSetHA_Click);
            // 
            // labelMasterServer
            // 
            this.labelMasterServer.AutoSize = true;
            this.labelMasterServer.Location = new System.Drawing.Point(27, 23);
            this.labelMasterServer.Name = "labelMasterServer";
            this.labelMasterServer.Size = new System.Drawing.Size(98, 15);
            this.labelMasterServer.TabIndex = 63;
            this.labelMasterServer.Text = "Master Server";
            // 
            // comboBoxSlaveServer
            // 
            this.comboBoxSlaveServer.FormattingEnabled = true;
            this.comboBoxSlaveServer.Location = new System.Drawing.Point(344, 41);
            this.comboBoxSlaveServer.Name = "comboBoxSlaveServer";
            this.comboBoxSlaveServer.Size = new System.Drawing.Size(325, 23);
            this.comboBoxSlaveServer.TabIndex = 62;
            // 
            // groupBoxSqlServerConfigurationTemplate
            // 
            this.groupBoxSqlServerConfigurationTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxSqlServerConfigurationTemplate.Controls.Add(this.buttonShowCheckedLBDetailInfo);
            this.groupBoxSqlServerConfigurationTemplate.Controls.Add(this.buttonLoadBalancerNameCheck);
            this.groupBoxSqlServerConfigurationTemplate.Controls.Add(this.buttonDbDelete);
            this.groupBoxSqlServerConfigurationTemplate.Controls.Add(this.labelZone);
            this.groupBoxSqlServerConfigurationTemplate.Controls.Add(this.comboBoxRegion);
            this.groupBoxSqlServerConfigurationTemplate.Controls.Add(this.comboBoxZone);
            this.groupBoxSqlServerConfigurationTemplate.Controls.Add(this.labelRegion);
            this.groupBoxSqlServerConfigurationTemplate.Controls.Add(this.buttonDbSave);
            this.groupBoxSqlServerConfigurationTemplate.Controls.Add(this.comboBoxProtocol);
            this.groupBoxSqlServerConfigurationTemplate.Controls.Add(this.buttonLoadTemplate);
            this.groupBoxSqlServerConfigurationTemplate.Controls.Add(this.textBoxServerPort);
            this.groupBoxSqlServerConfigurationTemplate.Controls.Add(this.buttonSaveTemplate);
            this.groupBoxSqlServerConfigurationTemplate.Controls.Add(this.labelServerPort);
            this.groupBoxSqlServerConfigurationTemplate.Controls.Add(this.labelProtocol);
            this.groupBoxSqlServerConfigurationTemplate.Controls.Add(this.textBoxLoadBalancerName);
            this.groupBoxSqlServerConfigurationTemplate.Controls.Add(this.textBoxLoadBalancerPort);
            this.groupBoxSqlServerConfigurationTemplate.Controls.Add(this.labelCurrentAccessKey);
            this.groupBoxSqlServerConfigurationTemplate.Controls.Add(this.labelLoadBalancerPort);
            this.groupBoxSqlServerConfigurationTemplate.Location = new System.Drawing.Point(22, 458);
            this.groupBoxSqlServerConfigurationTemplate.Name = "groupBoxSqlServerConfigurationTemplate";
            this.groupBoxSqlServerConfigurationTemplate.Size = new System.Drawing.Size(853, 115);
            this.groupBoxSqlServerConfigurationTemplate.TabIndex = 54;
            this.groupBoxSqlServerConfigurationTemplate.TabStop = false;
            this.groupBoxSqlServerConfigurationTemplate.Text = "Load Balancer Configuration Template";
            // 
            // buttonShowCheckedLBDetailInfo
            // 
            this.buttonShowCheckedLBDetailInfo.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.buttonShowCheckedLBDetailInfo.Location = new System.Drawing.Point(384, 74);
            this.buttonShowCheckedLBDetailInfo.Name = "buttonShowCheckedLBDetailInfo";
            this.buttonShowCheckedLBDetailInfo.Size = new System.Drawing.Size(118, 23);
            this.buttonShowCheckedLBDetailInfo.TabIndex = 68;
            this.buttonShowCheckedLBDetailInfo.Text = "Show Detail";
            this.buttonShowCheckedLBDetailInfo.UseVisualStyleBackColor = true;
            this.buttonShowCheckedLBDetailInfo.Click += new System.EventHandler(this.buttonShowCheckedLBDetailInfo_Click);
            // 
            // buttonLoadBalancerNameCheck
            // 
            this.buttonLoadBalancerNameCheck.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.buttonLoadBalancerNameCheck.Location = new System.Drawing.Point(262, 74);
            this.buttonLoadBalancerNameCheck.Name = "buttonLoadBalancerNameCheck";
            this.buttonLoadBalancerNameCheck.Size = new System.Drawing.Size(118, 23);
            this.buttonLoadBalancerNameCheck.TabIndex = 67;
            this.buttonLoadBalancerNameCheck.Text = "Exists Check";
            this.buttonLoadBalancerNameCheck.UseVisualStyleBackColor = true;
            this.buttonLoadBalancerNameCheck.Click += new System.EventHandler(this.buttonNameCheck_Click);
            // 
            // buttonDbDelete
            // 
            this.buttonDbDelete.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.buttonDbDelete.Location = new System.Drawing.Point(593, 74);
            this.buttonDbDelete.Name = "buttonDbDelete";
            this.buttonDbDelete.Size = new System.Drawing.Size(84, 23);
            this.buttonDbDelete.TabIndex = 58;
            this.buttonDbDelete.Text = "db delete";
            this.buttonDbDelete.UseVisualStyleBackColor = true;
            this.buttonDbDelete.Click += new System.EventHandler(this.buttonDbDelete_Click);
            // 
            // labelZone
            // 
            this.labelZone.AutoSize = true;
            this.labelZone.Location = new System.Drawing.Point(287, 27);
            this.labelZone.Name = "labelZone";
            this.labelZone.Size = new System.Drawing.Size(35, 15);
            this.labelZone.TabIndex = 56;
            this.labelZone.Text = "Zone";
            // 
            // comboBoxRegion
            // 
            this.comboBoxRegion.FormattingEnabled = true;
            this.comboBoxRegion.Location = new System.Drawing.Point(152, 45);
            this.comboBoxRegion.Name = "comboBoxRegion";
            this.comboBoxRegion.Size = new System.Drawing.Size(123, 23);
            this.comboBoxRegion.TabIndex = 66;
            this.comboBoxRegion.SelectedIndexChanged += new System.EventHandler(this.ComboBoxRegionChanged);
            // 
            // comboBoxZone
            // 
            this.comboBoxZone.FormattingEnabled = true;
            this.comboBoxZone.Location = new System.Drawing.Point(283, 45);
            this.comboBoxZone.Name = "comboBoxZone";
            this.comboBoxZone.Size = new System.Drawing.Size(123, 23);
            this.comboBoxZone.TabIndex = 55;
            // 
            // labelRegion
            // 
            this.labelRegion.AutoSize = true;
            this.labelRegion.Location = new System.Drawing.Point(157, 27);
            this.labelRegion.Name = "labelRegion";
            this.labelRegion.Size = new System.Drawing.Size(49, 15);
            this.labelRegion.TabIndex = 65;
            this.labelRegion.Text = "Region";
            // 
            // buttonDbSave
            // 
            this.buttonDbSave.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.buttonDbSave.Location = new System.Drawing.Point(507, 74);
            this.buttonDbSave.Name = "buttonDbSave";
            this.buttonDbSave.Size = new System.Drawing.Size(84, 23);
            this.buttonDbSave.TabIndex = 57;
            this.buttonDbSave.Text = "db save";
            this.buttonDbSave.UseVisualStyleBackColor = true;
            this.buttonDbSave.Click += new System.EventHandler(this.buttonDbSave_Click);
            // 
            // comboBoxProtocol
            // 
            this.comboBoxProtocol.FormattingEnabled = true;
            this.comboBoxProtocol.Items.AddRange(new object[] {
            "TCP",
            "UDP"});
            this.comboBoxProtocol.Location = new System.Drawing.Point(413, 45);
            this.comboBoxProtocol.Name = "comboBoxProtocol";
            this.comboBoxProtocol.Size = new System.Drawing.Size(123, 23);
            this.comboBoxProtocol.TabIndex = 60;
            // 
            // buttonLoadTemplate
            // 
            this.buttonLoadTemplate.Location = new System.Drawing.Point(141, 74);
            this.buttonLoadTemplate.Name = "buttonLoadTemplate";
            this.buttonLoadTemplate.Size = new System.Drawing.Size(118, 23);
            this.buttonLoadTemplate.TabIndex = 54;
            this.buttonLoadTemplate.Text = "Load Template";
            this.buttonLoadTemplate.UseVisualStyleBackColor = true;
            this.buttonLoadTemplate.Click += new System.EventHandler(this.buttonLoadTemplate_Click);
            // 
            // textBoxServerPort
            // 
            this.textBoxServerPort.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBoxServerPort.Location = new System.Drawing.Point(674, 45);
            this.textBoxServerPort.Name = "textBoxServerPort";
            this.textBoxServerPort.Size = new System.Drawing.Size(123, 23);
            this.textBoxServerPort.TabIndex = 58;
            // 
            // buttonSaveTemplate
            // 
            this.buttonSaveTemplate.Location = new System.Drawing.Point(22, 74);
            this.buttonSaveTemplate.Name = "buttonSaveTemplate";
            this.buttonSaveTemplate.Size = new System.Drawing.Size(118, 23);
            this.buttonSaveTemplate.TabIndex = 53;
            this.buttonSaveTemplate.Text = "Save Template";
            this.buttonSaveTemplate.UseVisualStyleBackColor = true;
            this.buttonSaveTemplate.Click += new System.EventHandler(this.buttonSaveTemplate_Click);
            // 
            // labelServerPort
            // 
            this.labelServerPort.AutoSize = true;
            this.labelServerPort.Location = new System.Drawing.Point(677, 27);
            this.labelServerPort.Name = "labelServerPort";
            this.labelServerPort.Size = new System.Drawing.Size(84, 15);
            this.labelServerPort.TabIndex = 59;
            this.labelServerPort.Text = "Server Port";
            // 
            // labelProtocol
            // 
            this.labelProtocol.AutoSize = true;
            this.labelProtocol.Location = new System.Drawing.Point(416, 27);
            this.labelProtocol.Name = "labelProtocol";
            this.labelProtocol.Size = new System.Drawing.Size(63, 15);
            this.labelProtocol.TabIndex = 54;
            this.labelProtocol.Text = "Protocol";
            // 
            // textBoxLoadBalancerName
            // 
            this.textBoxLoadBalancerName.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBoxLoadBalancerName.Location = new System.Drawing.Point(23, 45);
            this.textBoxLoadBalancerName.Name = "textBoxLoadBalancerName";
            this.textBoxLoadBalancerName.Size = new System.Drawing.Size(123, 23);
            this.textBoxLoadBalancerName.TabIndex = 40;
            // 
            // textBoxLoadBalancerPort
            // 
            this.textBoxLoadBalancerPort.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBoxLoadBalancerPort.Location = new System.Drawing.Point(544, 45);
            this.textBoxLoadBalancerPort.Name = "textBoxLoadBalancerPort";
            this.textBoxLoadBalancerPort.Size = new System.Drawing.Size(123, 23);
            this.textBoxLoadBalancerPort.TabIndex = 41;
            // 
            // labelCurrentAccessKey
            // 
            this.labelCurrentAccessKey.AutoSize = true;
            this.labelCurrentAccessKey.Location = new System.Drawing.Point(26, 25);
            this.labelCurrentAccessKey.Name = "labelCurrentAccessKey";
            this.labelCurrentAccessKey.Size = new System.Drawing.Size(35, 15);
            this.labelCurrentAccessKey.TabIndex = 42;
            this.labelCurrentAccessKey.Text = "Name";
            // 
            // labelLoadBalancerPort
            // 
            this.labelLoadBalancerPort.AutoSize = true;
            this.labelLoadBalancerPort.Location = new System.Drawing.Point(544, 27);
            this.labelLoadBalancerPort.Name = "labelLoadBalancerPort";
            this.labelLoadBalancerPort.Size = new System.Drawing.Size(133, 15);
            this.labelLoadBalancerPort.TabIndex = 43;
            this.labelLoadBalancerPort.Text = "Load Balancer Port";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.buttonCreateLoadBalancer);
            this.groupBox1.Controls.Add(this.buttonServerListReload);
            this.groupBox1.Controls.Add(this.buttonShowLBDetail);
            this.groupBox1.Controls.Add(this.dgvloadBalancerList);
            this.groupBox1.Controls.Add(this.buttonLoadBalancerListReload);
            this.groupBox1.Controls.Add(this.buttonDeleteLoadBalancer);
            this.groupBox1.Location = new System.Drawing.Point(22, 22);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(853, 430);
            this.groupBox1.TabIndex = 53;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Load Balancer List";
            // 
            // buttonCreateLoadBalancer
            // 
            this.buttonCreateLoadBalancer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCreateLoadBalancer.Location = new System.Drawing.Point(130, 396);
            this.buttonCreateLoadBalancer.Name = "buttonCreateLoadBalancer";
            this.buttonCreateLoadBalancer.Size = new System.Drawing.Size(118, 23);
            this.buttonCreateLoadBalancer.TabIndex = 68;
            this.buttonCreateLoadBalancer.Text = "Create";
            this.buttonCreateLoadBalancer.UseVisualStyleBackColor = true;
            this.buttonCreateLoadBalancer.Click += new System.EventHandler(this.buttonCreateLoadBalancer_Click);
            // 
            // buttonServerListReload
            // 
            this.buttonServerListReload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonServerListReload.Location = new System.Drawing.Point(380, 396);
            this.buttonServerListReload.Name = "buttonServerListReload";
            this.buttonServerListReload.Size = new System.Drawing.Size(118, 23);
            this.buttonServerListReload.TabIndex = 67;
            this.buttonServerListReload.Text = "Load HA Info";
            this.buttonServerListReload.UseVisualStyleBackColor = true;
            this.buttonServerListReload.Click += new System.EventHandler(this.buttonServerListReload_Click);
            // 
            // buttonShowLBDetail
            // 
            this.buttonShowLBDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonShowLBDetail.Location = new System.Drawing.Point(253, 396);
            this.buttonShowLBDetail.Name = "buttonShowLBDetail";
            this.buttonShowLBDetail.Size = new System.Drawing.Size(123, 23);
            this.buttonShowLBDetail.TabIndex = 67;
            this.buttonShowLBDetail.Text = "Show Detail";
            this.buttonShowLBDetail.UseVisualStyleBackColor = true;
            this.buttonShowLBDetail.Click += new System.EventHandler(this.buttonShowLBDetail_Click);
            // 
            // dgvloadBalancerList
            // 
            this.dgvloadBalancerList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvloadBalancerList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvloadBalancerList.Location = new System.Drawing.Point(19, 22);
            this.dgvloadBalancerList.Name = "dgvloadBalancerList";
            this.dgvloadBalancerList.Size = new System.Drawing.Size(821, 368);
            this.dgvloadBalancerList.TabIndex = 1;
            
            // 
            // buttonLoadBalancerListReload
            // 
            this.buttonLoadBalancerListReload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonLoadBalancerListReload.Location = new System.Drawing.Point(19, 396);
            this.buttonLoadBalancerListReload.Name = "buttonLoadBalancerListReload";
            this.buttonLoadBalancerListReload.Size = new System.Drawing.Size(107, 23);
            this.buttonLoadBalancerListReload.TabIndex = 39;
            this.buttonLoadBalancerListReload.Text = "Reload";
            this.buttonLoadBalancerListReload.UseVisualStyleBackColor = true;
            this.buttonLoadBalancerListReload.Click += new System.EventHandler(this.buttonLoadBalancerListReload_Click);
            // 
            // buttonDeleteLoadBalancer
            // 
            this.buttonDeleteLoadBalancer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDeleteLoadBalancer.Location = new System.Drawing.Point(502, 396);
            this.buttonDeleteLoadBalancer.Name = "buttonDeleteLoadBalancer";
            this.buttonDeleteLoadBalancer.Size = new System.Drawing.Size(118, 23);
            this.buttonDeleteLoadBalancer.TabIndex = 59;
            this.buttonDeleteLoadBalancer.Text = "Delete";
            this.buttonDeleteLoadBalancer.UseVisualStyleBackColor = true;
            this.buttonDeleteLoadBalancer.Click += new System.EventHandler(this.buttonDeleteLoadBalancerInstance_Click);
            // 
            // UcLoadBalancer
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.groupBoxLoadBalancer);
            this.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.Name = "UcLoadBalancer";
            this.Size = new System.Drawing.Size(900, 700);
            this.Load += new System.EventHandler(this.LoadData);
            this.groupBoxLoadBalancer.ResumeLayout(false);
            this.groupBoxServer.ResumeLayout(false);
            this.groupBoxServer.PerformLayout();
            this.groupBoxSqlServerConfigurationTemplate.ResumeLayout(false);
            this.groupBoxSqlServerConfigurationTemplate.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvloadBalancerList)).EndInit();
            this.ResumeLayout(false);


            
        }

        #endregion
        private System.Windows.Forms.GroupBox groupBoxLoadBalancer;
        private System.Windows.Forms.DataGridView dgvloadBalancerList;
        private System.Windows.Forms.Button buttonLoadBalancerListReload;
        private System.Windows.Forms.Button buttonSaveTemplate;
        private System.Windows.Forms.Button buttonLoadTemplate;
        private System.Windows.Forms.Button buttonDbSave;
        private System.Windows.Forms.Button buttonDeleteLoadBalancer;
        private System.Windows.Forms.Button buttonDbDelete;
        private System.Windows.Forms.GroupBox groupBoxSqlServerConfigurationTemplate;
        private System.Windows.Forms.Button buttonShowLBDetail;
        private System.Windows.Forms.Label labelZone;
        private System.Windows.Forms.ComboBox comboBoxRegion;
        private System.Windows.Forms.ComboBox comboBoxZone;
        private System.Windows.Forms.Label labelRegion;
        private System.Windows.Forms.ComboBox comboBoxProtocol;
        private System.Windows.Forms.TextBox textBoxServerPort;
        private System.Windows.Forms.Label labelServerPort;
        private System.Windows.Forms.Label labelProtocol;
        private System.Windows.Forms.TextBox textBoxLoadBalancerName;
        private System.Windows.Forms.TextBox textBoxLoadBalancerPort;
        private System.Windows.Forms.Label labelCurrentAccessKey;
        private System.Windows.Forms.Label labelLoadBalancerPort;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBoxServer;
        private System.Windows.Forms.Button buttonServerListReload;
        private System.Windows.Forms.ComboBox comboBoxMasterServer;
        private System.Windows.Forms.Button buttonDelHA;
        private System.Windows.Forms.Label labelSlaveServer;
        private System.Windows.Forms.Button buttonSetHA;
        private System.Windows.Forms.Label labelMasterServer;
        private System.Windows.Forms.ComboBox comboBoxSlaveServer;
        private System.Windows.Forms.Button buttonLoadBalancerNameCheck;
        private System.Windows.Forms.Button buttonShowCheckedLBDetailInfo;
        private System.Windows.Forms.Button buttonCreateLoadBalancer;
    }
}
