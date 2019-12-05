namespace HaTool.HighAvailability
{
    partial class UcFailoverPolicy
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UcFailoverPolicy));
            this.groupBoxMirroring = new System.Windows.Forms.GroupBox();
            this.groupBoxFailoverPolicy = new System.Windows.Forms.GroupBox();
            this.label2InSeconds = new System.Windows.Forms.Label();
            this.label1InSeconds = new System.Windows.Forms.Label();
            this.textBox01Comment = new System.Windows.Forms.TextBox();
            this.label01 = new System.Windows.Forms.Label();
            this.textBoxHeartBeatTimeLimitSec = new System.Windows.Forms.TextBox();
            this.textBox00Comment = new System.Windows.Forms.TextBox();
            this.label00 = new System.Windows.Forms.Label();
            this.textBoxHeartBeatIntervalSec = new System.Windows.Forms.TextBox();
            this.groupBoxSelectHaGroup = new System.Windows.Forms.GroupBox();
            this.buttonSavePolicy = new System.Windows.Forms.Button();
            this.buttonLoadPolicy = new System.Windows.Forms.Button();
            this.buttonServerListReload = new System.Windows.Forms.Button();
            this.dgvServerList = new System.Windows.Forms.DataGridView();
            this.lastMessage = new System.Windows.Forms.TextBox();
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
            this.groupBoxMirroring.Size = new System.Drawing.Size(770, 694);
            this.groupBoxMirroring.TabIndex = 1;
            this.groupBoxMirroring.TabStop = false;
            this.groupBoxMirroring.Text = "High Availability > Failover Policy";
            // 
            // groupBoxFailoverPolicy
            // 
            this.groupBoxFailoverPolicy.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxFailoverPolicy.Controls.Add(this.lastMessage);
            this.groupBoxFailoverPolicy.Controls.Add(this.label2InSeconds);
            this.groupBoxFailoverPolicy.Controls.Add(this.label1InSeconds);
            this.groupBoxFailoverPolicy.Controls.Add(this.textBox01Comment);
            this.groupBoxFailoverPolicy.Controls.Add(this.label01);
            this.groupBoxFailoverPolicy.Controls.Add(this.textBoxHeartBeatTimeLimitSec);
            this.groupBoxFailoverPolicy.Controls.Add(this.textBox00Comment);
            this.groupBoxFailoverPolicy.Controls.Add(this.label00);
            this.groupBoxFailoverPolicy.Controls.Add(this.textBoxHeartBeatIntervalSec);
            this.groupBoxFailoverPolicy.Location = new System.Drawing.Point(22, 297);
            this.groupBoxFailoverPolicy.Name = "groupBoxFailoverPolicy";
            this.groupBoxFailoverPolicy.Size = new System.Drawing.Size(742, 391);
            this.groupBoxFailoverPolicy.TabIndex = 162;
            this.groupBoxFailoverPolicy.TabStop = false;
            this.groupBoxFailoverPolicy.Text = "Failover Policy Setting";
            // 
            // label2
            // 
            this.label2InSeconds.AutoSize = true;
            this.label2InSeconds.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold);
            this.label2InSeconds.Location = new System.Drawing.Point(499, 115);
            this.label2InSeconds.Name = "label2";
            this.label2InSeconds.Size = new System.Drawing.Size(105, 15);
            this.label2InSeconds.TabIndex = 61;
            this.label2InSeconds.Text = "(in seconds) :";
            // 
            // label1
            // 
            this.label1InSeconds.AutoSize = true;
            this.label1InSeconds.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold);
            this.label1InSeconds.Location = new System.Drawing.Point(499, 36);
            this.label1InSeconds.Name = "label1";
            this.label1InSeconds.Size = new System.Drawing.Size(105, 15);
            this.label1InSeconds.TabIndex = 60;
            this.label1InSeconds.Text = "(in seconds) :";
            // 
            // textBox01Comment
            // 
            this.textBox01Comment.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox01Comment.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.textBox01Comment.Location = new System.Drawing.Point(20, 135);
            this.textBox01Comment.Multiline = true;
            this.textBox01Comment.Name = "textBox01Comment";
            this.textBox01Comment.Size = new System.Drawing.Size(572, 80);
            this.textBox01Comment.TabIndex = 59;
            this.textBox01Comment.Text = resources.GetString("textBox01Comment.Text");
            // 
            // label01
            // 
            this.label01.AutoSize = true;
            this.label01.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold);
            this.label01.Location = new System.Drawing.Point(20, 115);
            this.label01.Name = "label01";
            this.label01.Size = new System.Drawing.Size(406, 15);
            this.label01.TabIndex = 58;
            this.label01.Text = "If there is no HeartBeat for the specified time, failover";
            // 
            // textBoxHeartBeatTimeLimitSec
            // 
            this.textBoxHeartBeatTimeLimitSec.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBoxHeartBeatTimeLimitSec.Location = new System.Drawing.Point(610, 110);
            this.textBoxHeartBeatTimeLimitSec.Name = "textBoxHeartBeatTimeLimitSec";
            this.textBoxHeartBeatTimeLimitSec.Size = new System.Drawing.Size(96, 23);
            this.textBoxHeartBeatTimeLimitSec.TabIndex = 57;
            // 
            // textBox00Comment
            // 
            this.textBox00Comment.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox00Comment.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.textBox00Comment.Location = new System.Drawing.Point(20, 56);
            this.textBox00Comment.Multiline = true;
            this.textBox00Comment.Name = "textBox00Comment";
            this.textBox00Comment.Size = new System.Drawing.Size(572, 46);
            this.textBox00Comment.TabIndex = 56;
            this.textBox00Comment.Text = "Writes data to the object storage quorum at delayed seconds intervals.  Recommend" +
    "ed value 5 seconds";
            // 
            // label00
            // 
            this.label00.AutoSize = true;
            this.label00.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold);
            this.label00.Location = new System.Drawing.Point(20, 36);
            this.label00.Name = "label00";
            this.label00.Size = new System.Drawing.Size(238, 15);
            this.label00.TabIndex = 55;
            this.label00.Text = "Failover HeartBeat Check Interval";
            // 
            // textBoxHeartBeatIntervalSec
            // 
            this.textBoxHeartBeatIntervalSec.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBoxHeartBeatIntervalSec.Location = new System.Drawing.Point(610, 31);
            this.textBoxHeartBeatIntervalSec.Name = "textBoxHeartBeatIntervalSec";
            this.textBoxHeartBeatIntervalSec.Size = new System.Drawing.Size(96, 23);
            this.textBoxHeartBeatIntervalSec.TabIndex = 54;
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
            // lastMessage
            // 
            this.lastMessage.AcceptsReturn = true;
            this.lastMessage.BackColor = System.Drawing.SystemColors.Menu;
            this.lastMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lastMessage.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.lastMessage.Location = new System.Drawing.Point(23, 210);
            this.lastMessage.Multiline = true;
            this.lastMessage.Name = "lastMessage";
            this.lastMessage.Size = new System.Drawing.Size(572, 35);
            this.lastMessage.TabIndex = 87;
            this.lastMessage.Text = "comment";
            // 
            // UcFailoverPolicy
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.groupBoxMirroring);
            this.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.Name = "UcFailoverPolicy";
            this.Size = new System.Drawing.Size(776, 700);
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
        private System.Windows.Forms.TextBox textBoxHeartBeatIntervalSec;
        private System.Windows.Forms.Label label00;
        private System.Windows.Forms.TextBox textBox00Comment;
        private System.Windows.Forms.TextBox textBox01Comment;
        private System.Windows.Forms.Label label01;
        private System.Windows.Forms.TextBox textBoxHeartBeatTimeLimitSec;
        private System.Windows.Forms.Button buttonSavePolicy;
        private System.Windows.Forms.Label label2InSeconds;
        private System.Windows.Forms.Label label1InSeconds;
        private System.Windows.Forms.TextBox lastMessage;
    }
}
