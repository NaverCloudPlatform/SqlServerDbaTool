namespace HaTool.Server
{
    partial class UcSetSqlServer
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBoxStatus = new System.Windows.Forms.GroupBox();
            this.buttonClearLog = new System.Windows.Forms.Button();
            this.buttonShowDetailLog = new System.Windows.Forms.Button();
            this.pictureBoxProgressBar = new System.Windows.Forms.PictureBox();
            this.labelProgressBarPercent = new System.Windows.Forms.Label();
            this.labelProgressBarText = new System.Windows.Forms.Label();
            this.groupBoxServerList = new System.Windows.Forms.GroupBox();
            this.dgvServerList = new System.Windows.Forms.DataGridView();
            this.buttonServerListReload = new System.Windows.Forms.Button();
            this.buttonSetSqlServer = new System.Windows.Forms.Button();
            this.groupBoxSqlServerConfigurationTemplate = new System.Windows.Forms.GroupBox();
            this.comboBoxCollation = new System.Windows.Forms.ComboBox();
            this.buttonDbSave = new System.Windows.Forms.Button();
            this.buttonCommandPreview = new System.Windows.Forms.Button();
            this.labelPowserShellScript = new System.Windows.Forms.Label();
            this.buttonLoadTemplate = new System.Windows.Forms.Button();
            this.buttonModifyScript = new System.Windows.Forms.Button();
            this.labelSpConfiugure = new System.Windows.Forms.Label();
            this.buttonSaveTemplate = new System.Windows.Forms.Button();
            this.labelPort = new System.Windows.Forms.Label();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.labelCollation = new System.Windows.Forms.Label();
            this.textBoxId = new System.Windows.Forms.TextBox();
            this.textBoxTraceFlags = new System.Windows.Forms.TextBox();
            this.labelCurrentAccessKey = new System.Windows.Forms.Label();
            this.labelNewAccessKey = new System.Windows.Forms.Label();
            this.labelCurrentSecretKey = new System.Windows.Forms.Label();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBoxStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxProgressBar)).BeginInit();
            this.groupBoxServerList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvServerList)).BeginInit();
            this.groupBoxSqlServerConfigurationTemplate.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.groupBoxStatus);
            this.groupBox1.Controls.Add(this.groupBoxServerList);
            this.groupBox1.Controls.Add(this.groupBoxSqlServerConfigurationTemplate);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(763, 741);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Server > Set Sql Server";
            // 
            // groupBoxStatus
            // 
            this.groupBoxStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxStatus.Controls.Add(this.buttonClearLog);
            this.groupBoxStatus.Controls.Add(this.buttonShowDetailLog);
            this.groupBoxStatus.Controls.Add(this.pictureBoxProgressBar);
            this.groupBoxStatus.Controls.Add(this.labelProgressBarPercent);
            this.groupBoxStatus.Controls.Add(this.labelProgressBarText);
            this.groupBoxStatus.Location = new System.Drawing.Point(22, 633);
            this.groupBoxStatus.Name = "groupBoxStatus";
            this.groupBoxStatus.Size = new System.Drawing.Size(729, 100);
            this.groupBoxStatus.TabIndex = 58;
            this.groupBoxStatus.TabStop = false;
            this.groupBoxStatus.Text = "Status";
            // 
            // buttonClearLog
            // 
            this.buttonClearLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonClearLog.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.buttonClearLog.Location = new System.Drawing.Point(284, 65);
            this.buttonClearLog.Name = "buttonClearLog";
            this.buttonClearLog.Size = new System.Drawing.Size(266, 25);
            this.buttonClearLog.TabIndex = 166;
            this.buttonClearLog.Text = "Clear Log";
            this.buttonClearLog.UseVisualStyleBackColor = true;
            this.buttonClearLog.Click += new System.EventHandler(this.buttonClearLog_Click);
            // 
            // buttonShowDetailLog
            // 
            this.buttonShowDetailLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonShowDetailLog.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.buttonShowDetailLog.Location = new System.Drawing.Point(14, 65);
            this.buttonShowDetailLog.Name = "buttonShowDetailLog";
            this.buttonShowDetailLog.Size = new System.Drawing.Size(266, 25);
            this.buttonShowDetailLog.TabIndex = 165;
            this.buttonShowDetailLog.Text = "Show Detail Log";
            this.buttonShowDetailLog.UseVisualStyleBackColor = true;
            this.buttonShowDetailLog.Click += new System.EventHandler(this.buttonShowDetailLog_Click);
            // 
            // pictureBoxProgressBar
            // 
            this.pictureBoxProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxProgressBar.Location = new System.Drawing.Point(14, 56);
            this.pictureBoxProgressBar.Name = "pictureBoxProgressBar";
            this.pictureBoxProgressBar.Size = new System.Drawing.Size(707, 3);
            this.pictureBoxProgressBar.TabIndex = 54;
            this.pictureBoxProgressBar.TabStop = false;
            // 
            // labelProgressBarPercent
            // 
            this.labelProgressBarPercent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelProgressBarPercent.Location = new System.Drawing.Point(500, 34);
            this.labelProgressBarPercent.Name = "labelProgressBarPercent";
            this.labelProgressBarPercent.Size = new System.Drawing.Size(223, 13);
            this.labelProgressBarPercent.TabIndex = 55;
            this.labelProgressBarPercent.Text = "0% Completed";
            this.labelProgressBarPercent.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelProgressBarText
            // 
            this.labelProgressBarText.Location = new System.Drawing.Point(13, 32);
            this.labelProgressBarText.Name = "labelProgressBarText";
            this.labelProgressBarText.Size = new System.Drawing.Size(451, 13);
            this.labelProgressBarText.TabIndex = 56;
            this.labelProgressBarText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBoxServerList
            // 
            this.groupBoxServerList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxServerList.Controls.Add(this.dgvServerList);
            this.groupBoxServerList.Controls.Add(this.buttonServerListReload);
            this.groupBoxServerList.Controls.Add(this.buttonSetSqlServer);
            this.groupBoxServerList.Location = new System.Drawing.Point(22, 199);
            this.groupBoxServerList.Name = "groupBoxServerList";
            this.groupBoxServerList.Size = new System.Drawing.Size(729, 428);
            this.groupBoxServerList.TabIndex = 57;
            this.groupBoxServerList.TabStop = false;
            this.groupBoxServerList.Text = "Server List";
            // 
            // dgvServerList
            // 
            this.dgvServerList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvServerList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvServerList.Location = new System.Drawing.Point(14, 22);
            this.dgvServerList.Name = "dgvServerList";
            this.dgvServerList.Size = new System.Drawing.Size(709, 367);
            this.dgvServerList.TabIndex = 1;
            // 
            // buttonServerListReload
            // 
            this.buttonServerListReload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonServerListReload.Location = new System.Drawing.Point(14, 395);
            this.buttonServerListReload.Name = "buttonServerListReload";
            this.buttonServerListReload.Size = new System.Drawing.Size(107, 23);
            this.buttonServerListReload.TabIndex = 39;
            this.buttonServerListReload.Text = "Reload";
            this.buttonServerListReload.UseVisualStyleBackColor = true;
            this.buttonServerListReload.Click += new System.EventHandler(this.buttonServerListReload_Click);
            // 
            // buttonSetSqlServer
            // 
            this.buttonSetSqlServer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSetSqlServer.Location = new System.Drawing.Point(127, 395);
            this.buttonSetSqlServer.Name = "buttonSetSqlServer";
            this.buttonSetSqlServer.Size = new System.Drawing.Size(214, 23);
            this.buttonSetSqlServer.TabIndex = 53;
            this.buttonSetSqlServer.Text = "Set Sql Server";
            this.buttonSetSqlServer.UseVisualStyleBackColor = true;
            this.buttonSetSqlServer.Click += new System.EventHandler(this.buttonSetSqlServer_Click);
            // 
            // groupBoxSqlServerConfigurationTemplate
            // 
            this.groupBoxSqlServerConfigurationTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxSqlServerConfigurationTemplate.Controls.Add(this.comboBoxCollation);
            this.groupBoxSqlServerConfigurationTemplate.Controls.Add(this.buttonDbSave);
            this.groupBoxSqlServerConfigurationTemplate.Controls.Add(this.buttonCommandPreview);
            this.groupBoxSqlServerConfigurationTemplate.Controls.Add(this.labelPowserShellScript);
            this.groupBoxSqlServerConfigurationTemplate.Controls.Add(this.buttonLoadTemplate);
            this.groupBoxSqlServerConfigurationTemplate.Controls.Add(this.buttonModifyScript);
            this.groupBoxSqlServerConfigurationTemplate.Controls.Add(this.labelSpConfiugure);
            this.groupBoxSqlServerConfigurationTemplate.Controls.Add(this.buttonSaveTemplate);
            this.groupBoxSqlServerConfigurationTemplate.Controls.Add(this.labelPort);
            this.groupBoxSqlServerConfigurationTemplate.Controls.Add(this.textBoxPort);
            this.groupBoxSqlServerConfigurationTemplate.Controls.Add(this.labelCollation);
            this.groupBoxSqlServerConfigurationTemplate.Controls.Add(this.textBoxId);
            this.groupBoxSqlServerConfigurationTemplate.Controls.Add(this.textBoxTraceFlags);
            this.groupBoxSqlServerConfigurationTemplate.Controls.Add(this.labelCurrentAccessKey);
            this.groupBoxSqlServerConfigurationTemplate.Controls.Add(this.labelNewAccessKey);
            this.groupBoxSqlServerConfigurationTemplate.Controls.Add(this.labelCurrentSecretKey);
            this.groupBoxSqlServerConfigurationTemplate.Controls.Add(this.textBoxPassword);
            this.groupBoxSqlServerConfigurationTemplate.Location = new System.Drawing.Point(22, 22);
            this.groupBoxSqlServerConfigurationTemplate.Name = "groupBoxSqlServerConfigurationTemplate";
            this.groupBoxSqlServerConfigurationTemplate.Size = new System.Drawing.Size(728, 171);
            this.groupBoxSqlServerConfigurationTemplate.TabIndex = 0;
            this.groupBoxSqlServerConfigurationTemplate.TabStop = false;
            this.groupBoxSqlServerConfigurationTemplate.Text = "Sql Server Configuration Template";
            // 
            // comboBoxCollation
            // 
            this.comboBoxCollation.FormattingEnabled = true;
            this.comboBoxCollation.Items.AddRange(new object[] {
            "Korean_Wansung_CI_AS",
            "SQL_Latin1_General_CP1_CI_AS",
            "Chinese_PRC_CI_AS",
            "Chinese_PRC_Stroke_CI_AS",
            "Japanese_CI_AS",
            "Latin1_General_CI_AI"});
            this.comboBoxCollation.Location = new System.Drawing.Point(346, 44);
            this.comboBoxCollation.Name = "comboBoxCollation";
            this.comboBoxCollation.Size = new System.Drawing.Size(248, 23);
            this.comboBoxCollation.TabIndex = 58;
            this.comboBoxCollation.SelectedIndexChanged += new System.EventHandler(this.PsTemplateChange);
            // 
            // buttonDbSave
            // 
            this.buttonDbSave.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.buttonDbSave.Location = new System.Drawing.Point(238, 126);
            this.buttonDbSave.Name = "buttonDbSave";
            this.buttonDbSave.Size = new System.Drawing.Size(84, 23);
            this.buttonDbSave.TabIndex = 57;
            this.buttonDbSave.Text = "db save";
            this.buttonDbSave.UseVisualStyleBackColor = true;
            this.buttonDbSave.Click += new System.EventHandler(this.buttonDbSave_Click);
            // 
            // buttonCommandPreview
            // 
            this.buttonCommandPreview.Location = new System.Drawing.Point(511, 97);
            this.buttonCommandPreview.Name = "buttonCommandPreview";
            this.buttonCommandPreview.Size = new System.Drawing.Size(160, 23);
            this.buttonCommandPreview.TabIndex = 56;
            this.buttonCommandPreview.Text = "Command Preview";
            this.buttonCommandPreview.UseVisualStyleBackColor = true;
            this.buttonCommandPreview.Click += new System.EventHandler(this.buttonPowerShellScriptPreview_Click);
            // 
            // labelPowserShellScript
            // 
            this.labelPowserShellScript.AutoSize = true;
            this.labelPowserShellScript.Location = new System.Drawing.Point(512, 78);
            this.labelPowserShellScript.Name = "labelPowserShellScript";
            this.labelPowserShellScript.Size = new System.Drawing.Size(126, 15);
            this.labelPowserShellScript.TabIndex = 57;
            this.labelPowserShellScript.Text = "PowerShell Script";
            // 
            // buttonLoadTemplate
            // 
            this.buttonLoadTemplate.Location = new System.Drawing.Point(128, 126);
            this.buttonLoadTemplate.Name = "buttonLoadTemplate";
            this.buttonLoadTemplate.Size = new System.Drawing.Size(107, 23);
            this.buttonLoadTemplate.TabIndex = 54;
            this.buttonLoadTemplate.Text = "Load Template";
            this.buttonLoadTemplate.UseVisualStyleBackColor = true;
            this.buttonLoadTemplate.Click += new System.EventHandler(this.buttonLoadTemplate_Click);
            // 
            // buttonModifyScript
            // 
            this.buttonModifyScript.Location = new System.Drawing.Point(346, 97);
            this.buttonModifyScript.Name = "buttonModifyScript";
            this.buttonModifyScript.Size = new System.Drawing.Size(160, 23);
            this.buttonModifyScript.TabIndex = 53;
            this.buttonModifyScript.Text = "Modify Script";
            this.buttonModifyScript.UseVisualStyleBackColor = true;
            this.buttonModifyScript.Click += new System.EventHandler(this.buttonModifyScript_Click);
            // 
            // labelSpConfiugure
            // 
            this.labelSpConfiugure.AutoSize = true;
            this.labelSpConfiugure.Location = new System.Drawing.Point(350, 78);
            this.labelSpConfiugure.Name = "labelSpConfiugure";
            this.labelSpConfiugure.Size = new System.Drawing.Size(91, 15);
            this.labelSpConfiugure.TabIndex = 55;
            this.labelSpConfiugure.Text = "Sp_configure";
            // 
            // buttonSaveTemplate
            // 
            this.buttonSaveTemplate.Location = new System.Drawing.Point(16, 126);
            this.buttonSaveTemplate.Name = "buttonSaveTemplate";
            this.buttonSaveTemplate.Size = new System.Drawing.Size(107, 23);
            this.buttonSaveTemplate.TabIndex = 53;
            this.buttonSaveTemplate.Text = "Save Template";
            this.buttonSaveTemplate.UseVisualStyleBackColor = true;
            this.buttonSaveTemplate.Click += new System.EventHandler(this.buttonSaveTemplate_Click);
            // 
            // labelPort
            // 
            this.labelPort.AutoSize = true;
            this.labelPort.Location = new System.Drawing.Point(606, 25);
            this.labelPort.Name = "labelPort";
            this.labelPort.Size = new System.Drawing.Size(35, 15);
            this.labelPort.TabIndex = 54;
            this.labelPort.Text = "Port";
            // 
            // textBoxPort
            // 
            this.textBoxPort.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBoxPort.Location = new System.Drawing.Point(599, 44);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(71, 23);
            this.textBoxPort.TabIndex = 53;
            this.textBoxPort.TextChanged += new System.EventHandler(this.PsTemplateChange);
            // 
            // labelCollation
            // 
            this.labelCollation.AutoSize = true;
            this.labelCollation.Location = new System.Drawing.Point(349, 25);
            this.labelCollation.Name = "labelCollation";
            this.labelCollation.Size = new System.Drawing.Size(70, 15);
            this.labelCollation.TabIndex = 52;
            this.labelCollation.Text = "Collation";
            // 
            // textBoxId
            // 
            this.textBoxId.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBoxId.Location = new System.Drawing.Point(16, 44);
            this.textBoxId.Name = "textBoxId";
            this.textBoxId.Size = new System.Drawing.Size(160, 23);
            this.textBoxId.TabIndex = 40;
            this.textBoxId.TextChanged += new System.EventHandler(this.PsTemplateChange);
            // 
            // textBoxTraceFlags
            // 
            this.textBoxTraceFlags.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBoxTraceFlags.Location = new System.Drawing.Point(16, 97);
            this.textBoxTraceFlags.Name = "textBoxTraceFlags";
            this.textBoxTraceFlags.Size = new System.Drawing.Size(325, 23);
            this.textBoxTraceFlags.TabIndex = 41;
            this.textBoxTraceFlags.TextChanged += new System.EventHandler(this.PsTemplateChange);
            // 
            // labelCurrentAccessKey
            // 
            this.labelCurrentAccessKey.AutoSize = true;
            this.labelCurrentAccessKey.Location = new System.Drawing.Point(19, 25);
            this.labelCurrentAccessKey.Name = "labelCurrentAccessKey";
            this.labelCurrentAccessKey.Size = new System.Drawing.Size(21, 15);
            this.labelCurrentAccessKey.TabIndex = 42;
            this.labelCurrentAccessKey.Text = "Id";
            // 
            // labelNewAccessKey
            // 
            this.labelNewAccessKey.AutoSize = true;
            this.labelNewAccessKey.Location = new System.Drawing.Point(184, 25);
            this.labelNewAccessKey.Name = "labelNewAccessKey";
            this.labelNewAccessKey.Size = new System.Drawing.Size(63, 15);
            this.labelNewAccessKey.TabIndex = 49;
            this.labelNewAccessKey.Text = "Password";
            // 
            // labelCurrentSecretKey
            // 
            this.labelCurrentSecretKey.AutoSize = true;
            this.labelCurrentSecretKey.Location = new System.Drawing.Point(19, 78);
            this.labelCurrentSecretKey.Name = "labelCurrentSecretKey";
            this.labelCurrentSecretKey.Size = new System.Drawing.Size(77, 15);
            this.labelCurrentSecretKey.TabIndex = 43;
            this.labelCurrentSecretKey.Text = "TraceFlags";
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBoxPassword.Location = new System.Drawing.Point(181, 44);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(160, 23);
            this.textBoxPassword.TabIndex = 47;
            this.textBoxPassword.TextChanged += new System.EventHandler(this.PsTemplateChange);
            // 
            // UcSetSqlServer
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.Name = "UcSetSqlServer";
            this.Size = new System.Drawing.Size(773, 752);
            this.Load += new System.EventHandler(this.LoadData);
            this.Resize += new System.EventHandler(this.progressBarInit);
            this.groupBox1.ResumeLayout(false);
            this.groupBoxStatus.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxProgressBar)).EndInit();
            this.groupBoxServerList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvServerList)).EndInit();
            this.groupBoxSqlServerConfigurationTemplate.ResumeLayout(false);
            this.groupBoxSqlServerConfigurationTemplate.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvServerList;
        private System.Windows.Forms.Button buttonServerListReload;
        private System.Windows.Forms.Label labelCurrentSecretKey;
        private System.Windows.Forms.Label labelCurrentAccessKey;
        private System.Windows.Forms.TextBox textBoxTraceFlags;
        private System.Windows.Forms.TextBox textBoxId;
        private System.Windows.Forms.Label labelNewAccessKey;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.GroupBox groupBoxSqlServerConfigurationTemplate;
        private System.Windows.Forms.Button buttonSaveTemplate;
        private System.Windows.Forms.Button buttonModifyScript;
        private System.Windows.Forms.Label labelSpConfiugure;
        private System.Windows.Forms.Label labelPort;
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.Label labelCollation;
        private System.Windows.Forms.Button buttonSetSqlServer;
        private System.Windows.Forms.Label labelProgressBarPercent;
        private System.Windows.Forms.PictureBox pictureBoxProgressBar;
        private System.Windows.Forms.Label labelProgressBarText;
        private System.Windows.Forms.Button buttonCommandPreview;
        private System.Windows.Forms.Label labelPowserShellScript;
        private System.Windows.Forms.Button buttonLoadTemplate;
        private System.Windows.Forms.Button buttonDbSave;
        private System.Windows.Forms.GroupBox groupBoxServerList;
        private System.Windows.Forms.GroupBox groupBoxStatus;
        private System.Windows.Forms.Button buttonShowDetailLog;
        private System.Windows.Forms.Button buttonClearLog;
        private System.Windows.Forms.ComboBox comboBoxCollation;
    }
}
