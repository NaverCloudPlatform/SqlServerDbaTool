namespace HaTool.HighAvailability
{
    partial class UcMirroring
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
            this.groupBoxMirroring = new System.Windows.Forms.GroupBox();
            this.groupBoxStatus = new System.Windows.Forms.GroupBox();
            this.buttonClearLog = new System.Windows.Forms.Button();
            this.buttonShowDetailLog = new System.Windows.Forms.Button();
            this.labelProgressBarText = new System.Windows.Forms.Label();
            this.labelProgressBarPercent = new System.Windows.Forms.Label();
            this.pictureBoxProgressBar = new System.Windows.Forms.PictureBox();
            this.groupBoxMasterServerDatabaseList = new System.Windows.Forms.GroupBox();
            this.buttonDropDatabase = new System.Windows.Forms.Button();
            this.buttonRemoveMirror = new System.Windows.Forms.Button();
            this.buttonStartAutomaticMirroring = new System.Windows.Forms.Button();
            this.buttonMirrorStatusReload = new System.Windows.Forms.Button();
            this.dgvMirrorStatus = new System.Windows.Forms.DataGridView();
            this.groupBoxBackupRestorePathCheck = new System.Windows.Forms.GroupBox();
            this.textBoxBackupRestorePath = new System.Windows.Forms.TextBox();
            this.labelSlaveServerCheckStatusValue = new System.Windows.Forms.Label();
            this.labelMasterServerCheckStatusValue = new System.Windows.Forms.Label();
            this.textBoxBackupRestorePathCheckDescription = new System.Windows.Forms.TextBox();
            this.labelMasterServerCheckStatus = new System.Windows.Forms.Label();
            this.labelSlaveServerCheckStatus = new System.Windows.Forms.Label();
            this.buttonBackupRestorePathCheck = new System.Windows.Forms.Button();
            this.groupBoxSelectHaGroup = new System.Windows.Forms.GroupBox();
            this.textBoxDomain = new System.Windows.Forms.TextBox();
            this.label7MasterServer = new System.Windows.Forms.Label();
            this.comboBoxloadBalancerName = new System.Windows.Forms.ComboBox();
            this.label8SlaveServer = new System.Windows.Forms.Label();
            this.buttonLoadBalancerReload = new System.Windows.Forms.Button();
            this.textBoxSlaveServerName = new System.Windows.Forms.TextBox();
            this.textBoxMasterServerName = new System.Windows.Forms.TextBox();
            this.groupBoxMirroring.SuspendLayout();
            this.groupBoxStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxProgressBar)).BeginInit();
            this.groupBoxMasterServerDatabaseList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMirrorStatus)).BeginInit();
            this.groupBoxBackupRestorePathCheck.SuspendLayout();
            this.groupBoxSelectHaGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxMirroring
            // 
            this.groupBoxMirroring.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxMirroring.Controls.Add(this.groupBoxStatus);
            this.groupBoxMirroring.Controls.Add(this.groupBoxMasterServerDatabaseList);
            this.groupBoxMirroring.Controls.Add(this.groupBoxBackupRestorePathCheck);
            this.groupBoxMirroring.Controls.Add(this.groupBoxSelectHaGroup);
            this.groupBoxMirroring.Location = new System.Drawing.Point(3, 3);
            this.groupBoxMirroring.Name = "groupBoxMirroring";
            this.groupBoxMirroring.Size = new System.Drawing.Size(770, 694);
            this.groupBoxMirroring.TabIndex = 1;
            this.groupBoxMirroring.TabStop = false;
            this.groupBoxMirroring.Text = "High Availability > Database Mirroring";
            // 
            // groupBoxStatus
            // 
            this.groupBoxStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxStatus.Controls.Add(this.buttonClearLog);
            this.groupBoxStatus.Controls.Add(this.buttonShowDetailLog);
            this.groupBoxStatus.Controls.Add(this.labelProgressBarText);
            this.groupBoxStatus.Controls.Add(this.labelProgressBarPercent);
            this.groupBoxStatus.Controls.Add(this.pictureBoxProgressBar);
            this.groupBoxStatus.Location = new System.Drawing.Point(22, 591);
            this.groupBoxStatus.Name = "groupBoxStatus";
            this.groupBoxStatus.Size = new System.Drawing.Size(742, 92);
            this.groupBoxStatus.TabIndex = 164;
            this.groupBoxStatus.TabStop = false;
            this.groupBoxStatus.Text = "Status (Total 12 Steps)";
            // 
            // buttonClearLog
            // 
            this.buttonClearLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonClearLog.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.buttonClearLog.Location = new System.Drawing.Point(288, 54);
            this.buttonClearLog.Name = "buttonClearLog";
            this.buttonClearLog.Size = new System.Drawing.Size(266, 25);
            this.buttonClearLog.TabIndex = 167;
            this.buttonClearLog.Text = "Clear Log";
            this.buttonClearLog.UseVisualStyleBackColor = true;
            this.buttonClearLog.Click += new System.EventHandler(this.buttonClearLog_Click);
            // 
            // buttonShowDetailLog
            // 
            this.buttonShowDetailLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonShowDetailLog.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.buttonShowDetailLog.Location = new System.Drawing.Point(16, 54);
            this.buttonShowDetailLog.Name = "buttonShowDetailLog";
            this.buttonShowDetailLog.Size = new System.Drawing.Size(266, 25);
            this.buttonShowDetailLog.TabIndex = 164;
            this.buttonShowDetailLog.Text = "Show Detail Log";
            this.buttonShowDetailLog.UseVisualStyleBackColor = true;
            this.buttonShowDetailLog.Click += new System.EventHandler(this.buttonShowDetailLog_Click);
            // 
            // labelProgressBarText
            // 
            this.labelProgressBarText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelProgressBarText.Location = new System.Drawing.Point(14, 19);
            this.labelProgressBarText.Name = "labelProgressBarText";
            this.labelProgressBarText.Size = new System.Drawing.Size(208, 21);
            this.labelProgressBarText.TabIndex = 59;
            this.labelProgressBarText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelProgressBarPercent
            // 
            this.labelProgressBarPercent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelProgressBarPercent.Location = new System.Drawing.Point(550, 19);
            this.labelProgressBarPercent.Name = "labelProgressBarPercent";
            this.labelProgressBarPercent.Size = new System.Drawing.Size(173, 21);
            this.labelProgressBarPercent.TabIndex = 58;
            this.labelProgressBarPercent.Text = "0% Completed";
            this.labelProgressBarPercent.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pictureBoxProgressBar
            // 
            this.pictureBoxProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxProgressBar.Location = new System.Drawing.Point(16, 45);
            this.pictureBoxProgressBar.Name = "pictureBoxProgressBar";
            this.pictureBoxProgressBar.Size = new System.Drawing.Size(710, 3);
            this.pictureBoxProgressBar.TabIndex = 57;
            this.pictureBoxProgressBar.TabStop = false;
            // 
            // groupBoxMasterServerDatabaseList
            // 
            this.groupBoxMasterServerDatabaseList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxMasterServerDatabaseList.Controls.Add(this.buttonDropDatabase);
            this.groupBoxMasterServerDatabaseList.Controls.Add(this.buttonRemoveMirror);
            this.groupBoxMasterServerDatabaseList.Controls.Add(this.buttonStartAutomaticMirroring);
            this.groupBoxMasterServerDatabaseList.Controls.Add(this.buttonMirrorStatusReload);
            this.groupBoxMasterServerDatabaseList.Controls.Add(this.dgvMirrorStatus);
            this.groupBoxMasterServerDatabaseList.Location = new System.Drawing.Point(22, 220);
            this.groupBoxMasterServerDatabaseList.Name = "groupBoxMasterServerDatabaseList";
            this.groupBoxMasterServerDatabaseList.Size = new System.Drawing.Size(742, 365);
            this.groupBoxMasterServerDatabaseList.TabIndex = 163;
            this.groupBoxMasterServerDatabaseList.TabStop = false;
            this.groupBoxMasterServerDatabaseList.Text = "Master Server Database List";
            // 
            // button2
            // 
            this.buttonDropDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDropDatabase.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.buttonDropDatabase.Location = new System.Drawing.Point(553, 328);
            this.buttonDropDatabase.Name = "button2";
            this.buttonDropDatabase.Size = new System.Drawing.Size(128, 25);
            this.buttonDropDatabase.TabIndex = 165;
            this.buttonDropDatabase.Text = "Drop Database";
            this.buttonDropDatabase.UseVisualStyleBackColor = true;
            this.buttonDropDatabase.Click += new System.EventHandler(this.buttonDropDatabase_Click);
            // 
            // button1
            // 
            this.buttonRemoveMirror.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonRemoveMirror.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.buttonRemoveMirror.Location = new System.Drawing.Point(421, 328);
            this.buttonRemoveMirror.Name = "button1";
            this.buttonRemoveMirror.Size = new System.Drawing.Size(128, 25);
            this.buttonRemoveMirror.TabIndex = 164;
            this.buttonRemoveMirror.Text = "Remove Mirror";
            this.buttonRemoveMirror.UseVisualStyleBackColor = true;
            this.buttonRemoveMirror.Click += new System.EventHandler(this.buttonRemoveMirroring_Click);
            // 
            // buttonStartAutomaticMirroring
            // 
            this.buttonStartAutomaticMirroring.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonStartAutomaticMirroring.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.buttonStartAutomaticMirroring.Location = new System.Drawing.Point(149, 328);
            this.buttonStartAutomaticMirroring.Name = "buttonStartAutomaticMirroring";
            this.buttonStartAutomaticMirroring.Size = new System.Drawing.Size(266, 25);
            this.buttonStartAutomaticMirroring.TabIndex = 163;
            this.buttonStartAutomaticMirroring.Text = "Start Automatic Mirroring";
            this.buttonStartAutomaticMirroring.UseVisualStyleBackColor = true;
            this.buttonStartAutomaticMirroring.Click += new System.EventHandler(this.buttonStartAutomaticMirroring_Click);
            // 
            // buttonMirrorStatusReload
            // 
            this.buttonMirrorStatusReload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonMirrorStatusReload.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.buttonMirrorStatusReload.Location = new System.Drawing.Point(15, 328);
            this.buttonMirrorStatusReload.Name = "buttonMirrorStatusReload";
            this.buttonMirrorStatusReload.Size = new System.Drawing.Size(128, 25);
            this.buttonMirrorStatusReload.TabIndex = 162;
            this.buttonMirrorStatusReload.Text = "Reload";
            this.buttonMirrorStatusReload.UseVisualStyleBackColor = true;
            this.buttonMirrorStatusReload.Click += new System.EventHandler(this.buttonMirrorStatusReload_Click);
            // 
            // dgvMirrorStatus
            // 
            this.dgvMirrorStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMirrorStatus.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMirrorStatus.Location = new System.Drawing.Point(16, 22);
            this.dgvMirrorStatus.Name = "dgvMirrorStatus";
            this.dgvMirrorStatus.Size = new System.Drawing.Size(707, 300);
            this.dgvMirrorStatus.TabIndex = 14;
            // 
            // groupBoxBackupRestorePathCheck
            // 
            this.groupBoxBackupRestorePathCheck.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxBackupRestorePathCheck.Controls.Add(this.textBoxBackupRestorePath);
            this.groupBoxBackupRestorePathCheck.Controls.Add(this.labelSlaveServerCheckStatusValue);
            this.groupBoxBackupRestorePathCheck.Controls.Add(this.labelMasterServerCheckStatusValue);
            this.groupBoxBackupRestorePathCheck.Controls.Add(this.textBoxBackupRestorePathCheckDescription);
            this.groupBoxBackupRestorePathCheck.Controls.Add(this.labelMasterServerCheckStatus);
            this.groupBoxBackupRestorePathCheck.Controls.Add(this.labelSlaveServerCheckStatus);
            this.groupBoxBackupRestorePathCheck.Controls.Add(this.buttonBackupRestorePathCheck);
            this.groupBoxBackupRestorePathCheck.Location = new System.Drawing.Point(22, 121);
            this.groupBoxBackupRestorePathCheck.Name = "groupBoxBackupRestorePathCheck";
            this.groupBoxBackupRestorePathCheck.Size = new System.Drawing.Size(742, 93);
            this.groupBoxBackupRestorePathCheck.TabIndex = 162;
            this.groupBoxBackupRestorePathCheck.TabStop = false;
            this.groupBoxBackupRestorePathCheck.Text = "Initial backup path for mirroring (Created automatically if drive exists)";
            // 
            // textBoxBackupRestorePath
            // 
            this.textBoxBackupRestorePath.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.textBoxBackupRestorePath.Location = new System.Drawing.Point(16, 30);
            this.textBoxBackupRestorePath.Name = "textBoxBackupRestorePath";
            this.textBoxBackupRestorePath.Size = new System.Drawing.Size(264, 23);
            this.textBoxBackupRestorePath.TabIndex = 164;
            // 
            // labelSlaveServerCheckStatusValue
            // 
            this.labelSlaveServerCheckStatusValue.AutoSize = true;
            this.labelSlaveServerCheckStatusValue.Location = new System.Drawing.Point(570, 35);
            this.labelSlaveServerCheckStatusValue.Name = "labelSlaveServerCheckStatusValue";
            this.labelSlaveServerCheckStatusValue.Size = new System.Drawing.Size(56, 15);
            this.labelSlaveServerCheckStatusValue.TabIndex = 163;
            this.labelSlaveServerCheckStatusValue.Text = "unknown";
            // 
            // labelMasterServerCheckStatusValue
            // 
            this.labelMasterServerCheckStatusValue.AutoSize = true;
            this.labelMasterServerCheckStatusValue.Location = new System.Drawing.Point(428, 35);
            this.labelMasterServerCheckStatusValue.Name = "labelMasterServerCheckStatusValue";
            this.labelMasterServerCheckStatusValue.Size = new System.Drawing.Size(56, 15);
            this.labelMasterServerCheckStatusValue.TabIndex = 162;
            this.labelMasterServerCheckStatusValue.Text = "unknown";
            // 
            // textBoxBackupRestorePathCheckDescription
            // 
            this.textBoxBackupRestorePathCheckDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxBackupRestorePathCheckDescription.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.textBoxBackupRestorePathCheckDescription.Location = new System.Drawing.Point(16, 59);
            this.textBoxBackupRestorePathCheckDescription.Multiline = true;
            this.textBoxBackupRestorePathCheckDescription.Name = "textBoxBackupRestorePathCheckDescription";
            this.textBoxBackupRestorePathCheckDescription.Size = new System.Drawing.Size(493, 25);
            this.textBoxBackupRestorePathCheckDescription.TabIndex = 161;
            this.textBoxBackupRestorePathCheckDescription.Text = "type your server\'s local backup restore path and press check button.";
            // 
            // labelMasterServerCheckStatus
            // 
            this.labelMasterServerCheckStatus.AutoSize = true;
            this.labelMasterServerCheckStatus.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.labelMasterServerCheckStatus.Location = new System.Drawing.Point(428, 17);
            this.labelMasterServerCheckStatus.Name = "labelMasterServerCheckStatus";
            this.labelMasterServerCheckStatus.Size = new System.Drawing.Size(98, 15);
            this.labelMasterServerCheckStatus.TabIndex = 157;
            this.labelMasterServerCheckStatus.Text = "Master Server";
            // 
            // labelSlaveServerCheckStatus
            // 
            this.labelSlaveServerCheckStatus.AutoSize = true;
            this.labelSlaveServerCheckStatus.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.labelSlaveServerCheckStatus.Location = new System.Drawing.Point(566, 17);
            this.labelSlaveServerCheckStatus.Name = "labelSlaveServerCheckStatus";
            this.labelSlaveServerCheckStatus.Size = new System.Drawing.Size(91, 15);
            this.labelSlaveServerCheckStatus.TabIndex = 158;
            this.labelSlaveServerCheckStatus.Text = "Slave Server";
            // 
            // buttonBackupRestorePathCheck
            // 
            this.buttonBackupRestorePathCheck.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.buttonBackupRestorePathCheck.Location = new System.Drawing.Point(288, 29);
            this.buttonBackupRestorePathCheck.Name = "buttonBackupRestorePathCheck";
            this.buttonBackupRestorePathCheck.Size = new System.Drawing.Size(128, 25);
            this.buttonBackupRestorePathCheck.TabIndex = 156;
            this.buttonBackupRestorePathCheck.Text = "Check";
            this.buttonBackupRestorePathCheck.UseVisualStyleBackColor = true;
            this.buttonBackupRestorePathCheck.Click += new System.EventHandler(this.buttonBackupRestorePathCheck_Click);
            // 
            // groupBoxSelectHaGroup
            // 
            this.groupBoxSelectHaGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxSelectHaGroup.Controls.Add(this.textBoxDomain);
            this.groupBoxSelectHaGroup.Controls.Add(this.label7MasterServer);
            this.groupBoxSelectHaGroup.Controls.Add(this.comboBoxloadBalancerName);
            this.groupBoxSelectHaGroup.Controls.Add(this.label8SlaveServer);
            this.groupBoxSelectHaGroup.Controls.Add(this.buttonLoadBalancerReload);
            this.groupBoxSelectHaGroup.Controls.Add(this.textBoxSlaveServerName);
            this.groupBoxSelectHaGroup.Controls.Add(this.textBoxMasterServerName);
            this.groupBoxSelectHaGroup.Location = new System.Drawing.Point(22, 22);
            this.groupBoxSelectHaGroup.Name = "groupBoxSelectHaGroup";
            this.groupBoxSelectHaGroup.Size = new System.Drawing.Size(742, 93);
            this.groupBoxSelectHaGroup.TabIndex = 159;
            this.groupBoxSelectHaGroup.TabStop = false;
            this.groupBoxSelectHaGroup.Text = "Select HA Group";
            // 
            // textBoxDomain
            // 
            this.textBoxDomain.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxDomain.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.textBoxDomain.Location = new System.Drawing.Point(16, 59);
            this.textBoxDomain.Multiline = true;
            this.textBoxDomain.Name = "textBoxDomain";
            this.textBoxDomain.Size = new System.Drawing.Size(354, 25);
            this.textBoxDomain.TabIndex = 161;
            this.textBoxDomain.Text = "domain : ";
            // 
            // label7
            // 
            this.label7MasterServer.AutoSize = true;
            this.label7MasterServer.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.label7MasterServer.Location = new System.Drawing.Point(428, 12);
            this.label7MasterServer.Name = "label7";
            this.label7MasterServer.Size = new System.Drawing.Size(98, 15);
            this.label7MasterServer.TabIndex = 157;
            this.label7MasterServer.Text = "Master Server";
            // 
            // comboBoxloadBalancerName
            // 
            this.comboBoxloadBalancerName.FormattingEnabled = true;
            this.comboBoxloadBalancerName.Location = new System.Drawing.Point(16, 30);
            this.comboBoxloadBalancerName.Name = "comboBoxloadBalancerName";
            this.comboBoxloadBalancerName.Size = new System.Drawing.Size(264, 23);
            this.comboBoxloadBalancerName.TabIndex = 45;
            this.comboBoxloadBalancerName.SelectedIndexChanged += new System.EventHandler(this.HaGroup_Changed);
            // 
            // label8
            // 
            this.label8SlaveServer.AutoSize = true;
            this.label8SlaveServer.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.label8SlaveServer.Location = new System.Drawing.Point(566, 12);
            this.label8SlaveServer.Name = "label8";
            this.label8SlaveServer.Size = new System.Drawing.Size(91, 15);
            this.label8SlaveServer.TabIndex = 158;
            this.label8SlaveServer.Text = "Slave Server";
            // 
            // buttonLoadBalancerReload
            // 
            this.buttonLoadBalancerReload.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.buttonLoadBalancerReload.Location = new System.Drawing.Point(288, 29);
            this.buttonLoadBalancerReload.Name = "buttonLoadBalancerReload";
            this.buttonLoadBalancerReload.Size = new System.Drawing.Size(128, 25);
            this.buttonLoadBalancerReload.TabIndex = 156;
            this.buttonLoadBalancerReload.Text = "Reload";
            this.buttonLoadBalancerReload.UseVisualStyleBackColor = true;
            this.buttonLoadBalancerReload.Click += new System.EventHandler(this.buttonLoadServerList_Click);
            // 
            // textBoxSlaveServerName
            // 
            this.textBoxSlaveServerName.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.textBoxSlaveServerName.Location = new System.Drawing.Point(562, 30);
            this.textBoxSlaveServerName.Name = "textBoxSlaveServerName";
            this.textBoxSlaveServerName.Size = new System.Drawing.Size(128, 23);
            this.textBoxSlaveServerName.TabIndex = 160;
            // 
            // textBoxMasterServerName
            // 
            this.textBoxMasterServerName.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.textBoxMasterServerName.Location = new System.Drawing.Point(422, 30);
            this.textBoxMasterServerName.Name = "textBoxMasterServerName";
            this.textBoxMasterServerName.Size = new System.Drawing.Size(132, 23);
            this.textBoxMasterServerName.TabIndex = 159;
            // 
            // UcMirroring
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.groupBoxMirroring);
            this.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.Name = "UcMirroring";
            this.Size = new System.Drawing.Size(776, 700);
            this.Load += new System.EventHandler(this.LoadData);
            this.Resize += new System.EventHandler(this.progressBarInit);
            this.groupBoxMirroring.ResumeLayout(false);
            this.groupBoxStatus.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxProgressBar)).EndInit();
            this.groupBoxMasterServerDatabaseList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMirrorStatus)).EndInit();
            this.groupBoxBackupRestorePathCheck.ResumeLayout(false);
            this.groupBoxBackupRestorePathCheck.PerformLayout();
            this.groupBoxSelectHaGroup.ResumeLayout(false);
            this.groupBoxSelectHaGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBoxMirroring;
        private System.Windows.Forms.GroupBox groupBoxSelectHaGroup;
        private System.Windows.Forms.ComboBox comboBoxloadBalancerName;
        private System.Windows.Forms.Button buttonLoadBalancerReload;
        private System.Windows.Forms.Label label7MasterServer;
        private System.Windows.Forms.Label label8SlaveServer;
        private System.Windows.Forms.TextBox textBoxSlaveServerName;
        private System.Windows.Forms.TextBox textBoxMasterServerName;
        private System.Windows.Forms.TextBox textBoxDomain;
        private System.Windows.Forms.GroupBox groupBoxStatus;
        private System.Windows.Forms.GroupBox groupBoxMasterServerDatabaseList;
        private System.Windows.Forms.Button buttonStartAutomaticMirroring;
        private System.Windows.Forms.Button buttonMirrorStatusReload;
        private System.Windows.Forms.DataGridView dgvMirrorStatus;
        private System.Windows.Forms.GroupBox groupBoxBackupRestorePathCheck;
        private System.Windows.Forms.TextBox textBoxBackupRestorePathCheckDescription;
        private System.Windows.Forms.Label labelMasterServerCheckStatus;
        private System.Windows.Forms.Label labelSlaveServerCheckStatus;
        private System.Windows.Forms.Button buttonBackupRestorePathCheck;
        private System.Windows.Forms.Button buttonShowDetailLog;
        private System.Windows.Forms.Label labelProgressBarText;
        private System.Windows.Forms.Label labelProgressBarPercent;
        private System.Windows.Forms.PictureBox pictureBoxProgressBar;
        private System.Windows.Forms.Label labelSlaveServerCheckStatusValue;
        private System.Windows.Forms.Label labelMasterServerCheckStatusValue;
        private System.Windows.Forms.TextBox textBoxBackupRestorePath;
        private System.Windows.Forms.Button buttonRemoveMirror;
        private System.Windows.Forms.Button buttonDropDatabase;
        private System.Windows.Forms.Button buttonClearLog;
    }
}
