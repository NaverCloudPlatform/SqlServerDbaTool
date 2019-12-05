namespace HaTool.Monitoring
{
    partial class UcPerfmonPolicy
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
            this.groupBoxFailoverPolicy = new System.Windows.Forms.GroupBox();
            this.lastMessage = new System.Windows.Forms.TextBox();
            this.comboBoxCloudInsightYN = new System.Windows.Forms.ComboBox();
            this.textBox02Comment = new System.Windows.Forms.TextBox();
            this.labelUsingCloudInsight = new System.Windows.Forms.Label();
            this.textBox01Comment = new System.Windows.Forms.TextBox();
            this.label3InMintues = new System.Windows.Forms.Label();
            this.textBoxRetentionPeriodMinutes = new System.Windows.Forms.TextBox();
            this.labelRetentionPeriod = new System.Windows.Forms.Label();
            this.label1InSeconds = new System.Windows.Forms.Label();
            this.textBox00Comment = new System.Windows.Forms.TextBox();
            this.label00 = new System.Windows.Forms.Label();
            this.textBoxPerfmonProbeIntervalSec = new System.Windows.Forms.TextBox();
            this.groupBoxSelectHaGroup = new System.Windows.Forms.GroupBox();
            this.buttonSavePolicy = new System.Windows.Forms.Button();
            this.buttonLoadPolicy = new System.Windows.Forms.Button();
            this.buttonServerListReload = new System.Windows.Forms.Button();
            this.dgvServerList = new System.Windows.Forms.DataGridView();
            this.groupBoxMirroring.SuspendLayout();
            this.groupBoxFailoverPolicy.SuspendLayout();
            this.groupBoxSelectHaGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvServerList)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxMirroring
            // 
            this.groupBoxMirroring.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxMirroring.Controls.Add(this.groupBoxFailoverPolicy);
            this.groupBoxMirroring.Controls.Add(this.groupBoxSelectHaGroup);
            this.groupBoxMirroring.Location = new System.Drawing.Point(3, 3);
            this.groupBoxMirroring.Name = "groupBoxMirroring";
            this.groupBoxMirroring.Size = new System.Drawing.Size(770, 728);
            this.groupBoxMirroring.TabIndex = 1;
            this.groupBoxMirroring.TabStop = false;
            this.groupBoxMirroring.Text = "Monitoring > Perfmon Policy";
            // 
            // groupBoxFailoverPolicy
            // 
            this.groupBoxFailoverPolicy.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxFailoverPolicy.Controls.Add(this.lastMessage);
            this.groupBoxFailoverPolicy.Controls.Add(this.comboBoxCloudInsightYN);
            this.groupBoxFailoverPolicy.Controls.Add(this.textBox02Comment);
            this.groupBoxFailoverPolicy.Controls.Add(this.labelUsingCloudInsight);
            this.groupBoxFailoverPolicy.Controls.Add(this.textBox01Comment);
            this.groupBoxFailoverPolicy.Controls.Add(this.label3InMintues);
            this.groupBoxFailoverPolicy.Controls.Add(this.textBoxRetentionPeriodMinutes);
            this.groupBoxFailoverPolicy.Controls.Add(this.labelRetentionPeriod);
            this.groupBoxFailoverPolicy.Controls.Add(this.label1InSeconds);
            this.groupBoxFailoverPolicy.Controls.Add(this.textBox00Comment);
            this.groupBoxFailoverPolicy.Controls.Add(this.label00);
            this.groupBoxFailoverPolicy.Controls.Add(this.textBoxPerfmonProbeIntervalSec);
            this.groupBoxFailoverPolicy.Location = new System.Drawing.Point(22, 297);
            this.groupBoxFailoverPolicy.Name = "groupBoxFailoverPolicy";
            this.groupBoxFailoverPolicy.Size = new System.Drawing.Size(742, 425);
            this.groupBoxFailoverPolicy.TabIndex = 162;
            this.groupBoxFailoverPolicy.TabStop = false;
            this.groupBoxFailoverPolicy.Text = "Perfmon Policy Setting";
            // 
            // lastMessage
            // 
            this.lastMessage.AcceptsReturn = true;
            this.lastMessage.BackColor = System.Drawing.SystemColors.Menu;
            this.lastMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lastMessage.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.lastMessage.Location = new System.Drawing.Point(20, 269);
            this.lastMessage.Multiline = true;
            this.lastMessage.Name = "lastMessage";
            this.lastMessage.Size = new System.Drawing.Size(572, 35);
            this.lastMessage.TabIndex = 81;
            this.lastMessage.Text = "comment";
            // 
            // comboBoxCloudInsightYN
            // 
            this.comboBoxCloudInsightYN.Enabled = false;
            this.comboBoxCloudInsightYN.FormattingEnabled = true;
            this.comboBoxCloudInsightYN.Items.AddRange(new object[] {
            "Y",
            "N"});
            this.comboBoxCloudInsightYN.Location = new System.Drawing.Point(610, 179);
            this.comboBoxCloudInsightYN.Name = "comboBoxCloudInsightYN";
            this.comboBoxCloudInsightYN.Size = new System.Drawing.Size(96, 23);
            this.comboBoxCloudInsightYN.TabIndex = 80;
            // 
            // textBox02Comment
            // 
            this.textBox02Comment.AcceptsReturn = true;
            this.textBox02Comment.BackColor = System.Drawing.SystemColors.Menu;
            this.textBox02Comment.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox02Comment.Enabled = false;
            this.textBox02Comment.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.textBox02Comment.Location = new System.Drawing.Point(20, 203);
            this.textBox02Comment.Multiline = true;
            this.textBox02Comment.Name = "textBox02Comment";
            this.textBox02Comment.Size = new System.Drawing.Size(572, 35);
            this.textBox02Comment.TabIndex = 68;
            this.textBox02Comment.Text = "comment";
            // 
            // labelUsingCloudInsight
            // 
            this.labelUsingCloudInsight.AutoSize = true;
            this.labelUsingCloudInsight.Enabled = false;
            this.labelUsingCloudInsight.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold);
            this.labelUsingCloudInsight.Location = new System.Drawing.Point(20, 182);
            this.labelUsingCloudInsight.Name = "labelUsingCloudInsight";
            this.labelUsingCloudInsight.Size = new System.Drawing.Size(147, 15);
            this.labelUsingCloudInsight.TabIndex = 65;
            this.labelUsingCloudInsight.Text = "Using Cloud Insight ";
            // 
            // textBox01Comment
            // 
            this.textBox01Comment.AcceptsReturn = true;
            this.textBox01Comment.BackColor = System.Drawing.SystemColors.Menu;
            this.textBox01Comment.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox01Comment.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.textBox01Comment.Location = new System.Drawing.Point(20, 129);
            this.textBox01Comment.Multiline = true;
            this.textBox01Comment.Name = "textBox01Comment";
            this.textBox01Comment.Size = new System.Drawing.Size(572, 35);
            this.textBox01Comment.TabIndex = 64;
            this.textBox01Comment.Text = "comment";
            // 
            // label3InMintues
            // 
            this.label3InMintues.AutoSize = true;
            this.label3InMintues.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold);
            this.label3InMintues.Location = new System.Drawing.Point(499, 108);
            this.label3InMintues.Name = "label3InMintues";
            this.label3InMintues.Size = new System.Drawing.Size(105, 15);
            this.label3InMintues.TabIndex = 63;
            this.label3InMintues.Text = "(in minutes) :";
            // 
            // textBoxRetentionPeriodMinutes
            // 
            this.textBoxRetentionPeriodMinutes.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBoxRetentionPeriodMinutes.Location = new System.Drawing.Point(610, 104);
            this.textBoxRetentionPeriodMinutes.Name = "textBoxRetentionPeriodMinutes";
            this.textBoxRetentionPeriodMinutes.Size = new System.Drawing.Size(96, 23);
            this.textBoxRetentionPeriodMinutes.TabIndex = 62;
            // 
            // labelRetentionPeriod
            // 
            this.labelRetentionPeriod.AutoSize = true;
            this.labelRetentionPeriod.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold);
            this.labelRetentionPeriod.Location = new System.Drawing.Point(20, 108);
            this.labelRetentionPeriod.Name = "labelRetentionPeriod";
            this.labelRetentionPeriod.Size = new System.Drawing.Size(119, 15);
            this.labelRetentionPeriod.TabIndex = 61;
            this.labelRetentionPeriod.Text = "Retention Period";
            // 
            // label1InSeconds
            // 
            this.label1InSeconds.AutoSize = true;
            this.label1InSeconds.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold);
            this.label1InSeconds.Location = new System.Drawing.Point(499, 34);
            this.label1InSeconds.Name = "label1InSeconds";
            this.label1InSeconds.Size = new System.Drawing.Size(105, 15);
            this.label1InSeconds.TabIndex = 60;
            this.label1InSeconds.Text = "(in seconds) :";
            // 
            // textBox00Comment
            // 
            this.textBox00Comment.AcceptsReturn = true;
            this.textBox00Comment.BackColor = System.Drawing.SystemColors.Menu;
            this.textBox00Comment.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox00Comment.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.textBox00Comment.Location = new System.Drawing.Point(22, 56);
            this.textBox00Comment.Multiline = true;
            this.textBox00Comment.Name = "textBox00Comment";
            this.textBox00Comment.Size = new System.Drawing.Size(572, 35);
            this.textBox00Comment.TabIndex = 56;
            this.textBox00Comment.Text = "comment";
            // 
            // label00
            // 
            this.label00.AutoSize = true;
            this.label00.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold);
            this.label00.Location = new System.Drawing.Point(20, 34);
            this.label00.Name = "label00";
            this.label00.Size = new System.Drawing.Size(161, 15);
            this.label00.TabIndex = 55;
            this.label00.Text = "Perfmon Probe Interval";
            // 
            // textBoxPerfmonProbeIntervalSec
            // 
            this.textBoxPerfmonProbeIntervalSec.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBoxPerfmonProbeIntervalSec.Location = new System.Drawing.Point(610, 30);
            this.textBoxPerfmonProbeIntervalSec.Name = "textBoxPerfmonProbeIntervalSec";
            this.textBoxPerfmonProbeIntervalSec.Size = new System.Drawing.Size(96, 23);
            this.textBoxPerfmonProbeIntervalSec.TabIndex = 54;
            // 
            // groupBoxSelectHaGroup
            // 
            this.groupBoxSelectHaGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxSelectHaGroup.Controls.Add(this.buttonSavePolicy);
            this.groupBoxSelectHaGroup.Controls.Add(this.buttonLoadPolicy);
            this.groupBoxSelectHaGroup.Controls.Add(this.buttonServerListReload);
            this.groupBoxSelectHaGroup.Controls.Add(this.dgvServerList);
            this.groupBoxSelectHaGroup.Location = new System.Drawing.Point(22, 22);
            this.groupBoxSelectHaGroup.Name = "groupBoxSelectHaGroup";
            this.groupBoxSelectHaGroup.Size = new System.Drawing.Size(742, 269);
            this.groupBoxSelectHaGroup.TabIndex = 159;
            this.groupBoxSelectHaGroup.TabStop = false;
            this.groupBoxSelectHaGroup.Text = "Select Server";
            // 
            // buttonSavePolicy
            // 
            this.buttonSavePolicy.Location = new System.Drawing.Point(246, 239);
            this.buttonSavePolicy.Name = "buttonSavePolicy";
            this.buttonSavePolicy.Size = new System.Drawing.Size(107, 23);
            this.buttonSavePolicy.TabIndex = 42;
            this.buttonSavePolicy.Text = "Save Policy";
            this.buttonSavePolicy.UseVisualStyleBackColor = true;
            this.buttonSavePolicy.Click += new System.EventHandler(this.buttonSavePolicy_Click);
            // 
            // buttonLoadPolicy
            // 
            this.buttonLoadPolicy.Location = new System.Drawing.Point(133, 239);
            this.buttonLoadPolicy.Name = "buttonLoadPolicy";
            this.buttonLoadPolicy.Size = new System.Drawing.Size(107, 23);
            this.buttonLoadPolicy.TabIndex = 41;
            this.buttonLoadPolicy.Text = "Load Policy";
            this.buttonLoadPolicy.UseVisualStyleBackColor = true;
            this.buttonLoadPolicy.Click += new System.EventHandler(this.buttonLoadPolicy_Click);
            // 
            // buttonServerListReload
            // 
            this.buttonServerListReload.Location = new System.Drawing.Point(20, 239);
            this.buttonServerListReload.Name = "buttonServerListReload";
            this.buttonServerListReload.Size = new System.Drawing.Size(107, 23);
            this.buttonServerListReload.TabIndex = 40;
            this.buttonServerListReload.Text = "Reload";
            this.buttonServerListReload.UseVisualStyleBackColor = true;
            this.buttonServerListReload.Click += new System.EventHandler(this.buttonServerListReload_Click);
            // 
            // dgvServerList
            // 
            this.dgvServerList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvServerList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvServerList.Location = new System.Drawing.Point(20, 22);
            this.dgvServerList.Name = "dgvServerList";
            this.dgvServerList.Size = new System.Drawing.Size(705, 212);
            this.dgvServerList.TabIndex = 2;
            // 
            // UcPerfmonPolicy
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.groupBoxMirroring);
            this.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.Name = "UcPerfmonPolicy";
            this.Size = new System.Drawing.Size(776, 734);
            this.Load += new System.EventHandler(this.LoadData);
            this.groupBoxMirroring.ResumeLayout(false);
            this.groupBoxFailoverPolicy.ResumeLayout(false);
            this.groupBoxFailoverPolicy.PerformLayout();
            this.groupBoxSelectHaGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvServerList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBoxMirroring;
        private System.Windows.Forms.GroupBox groupBoxSelectHaGroup;
        private System.Windows.Forms.GroupBox groupBoxFailoverPolicy;
        private System.Windows.Forms.DataGridView dgvServerList;
        private System.Windows.Forms.Button buttonServerListReload;
        private System.Windows.Forms.Button buttonLoadPolicy;
        private System.Windows.Forms.TextBox textBoxPerfmonProbeIntervalSec;
        private System.Windows.Forms.Label label00;
        private System.Windows.Forms.TextBox textBox00Comment;
        private System.Windows.Forms.Button buttonSavePolicy;
        private System.Windows.Forms.Label label1InSeconds;
        private System.Windows.Forms.Label label3InMintues;
        private System.Windows.Forms.TextBox textBoxRetentionPeriodMinutes;
        private System.Windows.Forms.Label labelRetentionPeriod;
        private System.Windows.Forms.TextBox textBox02Comment;
        private System.Windows.Forms.Label labelUsingCloudInsight;
        private System.Windows.Forms.TextBox textBox01Comment;
        private System.Windows.Forms.ComboBox comboBoxCloudInsightYN;
        private System.Windows.Forms.TextBox lastMessage;
    }
}
