namespace HaTool.Monitoring
{
    partial class UcSqlmonPolicy
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
            this.textBox02Comment = new System.Windows.Forms.TextBox();
            this.textBox01Comment = new System.Windows.Forms.TextBox();
            this.textBox00Comment = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label02 = new System.Windows.Forms.Label();
            this.textBox_sp_lock2_intervalSec = new System.Windows.Forms.TextBox();
            this.label2InSeconds = new System.Windows.Forms.Label();
            this.label1InSeconds = new System.Windows.Forms.Label();
            this.label01 = new System.Windows.Forms.Label();
            this.textBox_dm_os_workers_IntervalSec = new System.Windows.Forms.TextBox();
            this.label00 = new System.Windows.Forms.Label();
            this.textBox_dm_exec_query_stats_IntervalSec = new System.Windows.Forms.TextBox();
            this.groupBoxSelectHaGroup = new System.Windows.Forms.GroupBox();
            this.buttonSavePolicy = new System.Windows.Forms.Button();
            this.buttonLoadPolicy = new System.Windows.Forms.Button();
            this.buttonServerListReload = new System.Windows.Forms.Button();
            this.dgvServerList = new System.Windows.Forms.DataGridView();
            this.textBox03Comment = new System.Windows.Forms.TextBox();
            this.label4InMinutes = new System.Windows.Forms.Label();
            this.textBoxRetentionPeriodMinutes = new System.Windows.Forms.TextBox();
            this.labelRetentionPeriod = new System.Windows.Forms.Label();
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
            this.groupBoxMirroring.Text = "Monitoring > Sqlmon Policy";
            // 
            // groupBoxFailoverPolicy
            // 
            this.groupBoxFailoverPolicy.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxFailoverPolicy.Controls.Add(this.textBox03Comment);
            this.groupBoxFailoverPolicy.Controls.Add(this.label4InMinutes);
            this.groupBoxFailoverPolicy.Controls.Add(this.textBoxRetentionPeriodMinutes);
            this.groupBoxFailoverPolicy.Controls.Add(this.labelRetentionPeriod);
            this.groupBoxFailoverPolicy.Controls.Add(this.lastMessage);
            this.groupBoxFailoverPolicy.Controls.Add(this.textBox02Comment);
            this.groupBoxFailoverPolicy.Controls.Add(this.textBox01Comment);
            this.groupBoxFailoverPolicy.Controls.Add(this.textBox00Comment);
            this.groupBoxFailoverPolicy.Controls.Add(this.label3);
            this.groupBoxFailoverPolicy.Controls.Add(this.label02);
            this.groupBoxFailoverPolicy.Controls.Add(this.textBox_sp_lock2_intervalSec);
            this.groupBoxFailoverPolicy.Controls.Add(this.label2InSeconds);
            this.groupBoxFailoverPolicy.Controls.Add(this.label1InSeconds);
            this.groupBoxFailoverPolicy.Controls.Add(this.label01);
            this.groupBoxFailoverPolicy.Controls.Add(this.textBox_dm_os_workers_IntervalSec);
            this.groupBoxFailoverPolicy.Controls.Add(this.label00);
            this.groupBoxFailoverPolicy.Controls.Add(this.textBox_dm_exec_query_stats_IntervalSec);
            this.groupBoxFailoverPolicy.Location = new System.Drawing.Point(22, 297);
            this.groupBoxFailoverPolicy.Name = "groupBoxFailoverPolicy";
            this.groupBoxFailoverPolicy.Size = new System.Drawing.Size(742, 425);
            this.groupBoxFailoverPolicy.TabIndex = 162;
            this.groupBoxFailoverPolicy.TabStop = false;
            this.groupBoxFailoverPolicy.Text = "Sqlmon Policy Setting";
            // 
            // lastMessage
            // 
            this.lastMessage.AcceptsReturn = true;
            this.lastMessage.BackColor = System.Drawing.SystemColors.Menu;
            this.lastMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lastMessage.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.lastMessage.Location = new System.Drawing.Point(23, 310);
            this.lastMessage.Multiline = true;
            this.lastMessage.Name = "lastMessage";
            this.lastMessage.Size = new System.Drawing.Size(572, 35);
            this.lastMessage.TabIndex = 86;
            this.lastMessage.Text = "comment";
            // 
            // textBox02Comment
            // 
            this.textBox02Comment.AcceptsReturn = true;
            this.textBox02Comment.BackColor = System.Drawing.SystemColors.Menu;
            this.textBox02Comment.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox02Comment.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.textBox02Comment.Location = new System.Drawing.Point(23, 178);
            this.textBox02Comment.Multiline = true;
            this.textBox02Comment.Name = "textBox02Comment";
            this.textBox02Comment.Size = new System.Drawing.Size(572, 35);
            this.textBox02Comment.TabIndex = 85;
            this.textBox02Comment.Text = "comment";
            // 
            // textBox01Comment
            // 
            this.textBox01Comment.AcceptsReturn = true;
            this.textBox01Comment.BackColor = System.Drawing.SystemColors.Menu;
            this.textBox01Comment.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox01Comment.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.textBox01Comment.Location = new System.Drawing.Point(23, 117);
            this.textBox01Comment.Multiline = true;
            this.textBox01Comment.Name = "textBox01Comment";
            this.textBox01Comment.Size = new System.Drawing.Size(572, 35);
            this.textBox01Comment.TabIndex = 84;
            this.textBox01Comment.Text = "comment";
            // 
            // textBox00Comment
            // 
            this.textBox00Comment.AcceptsReturn = true;
            this.textBox00Comment.BackColor = System.Drawing.SystemColors.Menu;
            this.textBox00Comment.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox00Comment.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.textBox00Comment.Location = new System.Drawing.Point(23, 56);
            this.textBox00Comment.Multiline = true;
            this.textBox00Comment.Name = "textBox00Comment";
            this.textBox00Comment.Size = new System.Drawing.Size(572, 35);
            this.textBox00Comment.TabIndex = 83;
            this.textBox00Comment.Text = "comment";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(499, 159);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 15);
            this.label3.TabIndex = 80;
            this.label3.Text = "(in seconds) :";
            // 
            // label02
            // 
            this.label02.AutoSize = true;
            this.label02.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold);
            this.label02.Location = new System.Drawing.Point(20, 159);
            this.label02.Name = "label02";
            this.label02.Size = new System.Drawing.Size(126, 15);
            this.label02.TabIndex = 64;
            this.label02.Text = "sp_lock2 Interval";
            // 
            // textBox_sp_lock2_intervalSec
            // 
            this.textBox_sp_lock2_intervalSec.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBox_sp_lock2_intervalSec.Location = new System.Drawing.Point(610, 154);
            this.textBox_sp_lock2_intervalSec.Name = "textBox_sp_lock2_intervalSec";
            this.textBox_sp_lock2_intervalSec.Size = new System.Drawing.Size(96, 23);
            this.textBox_sp_lock2_intervalSec.TabIndex = 63;
            // 
            // label2
            // 
            this.label2InSeconds.AutoSize = true;
            this.label2InSeconds.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold);
            this.label2InSeconds.Location = new System.Drawing.Point(499, 98);
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
            // label01
            // 
            this.label01.AutoSize = true;
            this.label01.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold);
            this.label01.Location = new System.Drawing.Point(20, 98);
            this.label01.Name = "label01";
            this.label01.Size = new System.Drawing.Size(161, 15);
            this.label01.TabIndex = 58;
            this.label01.Text = "dm_os_workers Interval";
            // 
            // textBox_dm_os_workers_IntervalSec
            // 
            this.textBox_dm_os_workers_IntervalSec.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBox_dm_os_workers_IntervalSec.Location = new System.Drawing.Point(610, 93);
            this.textBox_dm_os_workers_IntervalSec.Name = "textBox_dm_os_workers_IntervalSec";
            this.textBox_dm_os_workers_IntervalSec.Size = new System.Drawing.Size(96, 23);
            this.textBox_dm_os_workers_IntervalSec.TabIndex = 57;
            // 
            // label00
            // 
            this.label00.AutoSize = true;
            this.label00.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold);
            this.label00.Location = new System.Drawing.Point(20, 36);
            this.label00.Name = "label00";
            this.label00.Size = new System.Drawing.Size(203, 15);
            this.label00.TabIndex = 55;
            this.label00.Text = "dm_exec_query_stats Interval";
            // 
            // textBox_dm_exec_query_stats_IntervalSec
            // 
            this.textBox_dm_exec_query_stats_IntervalSec.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBox_dm_exec_query_stats_IntervalSec.Location = new System.Drawing.Point(610, 31);
            this.textBox_dm_exec_query_stats_IntervalSec.Name = "textBox_dm_exec_query_stats_IntervalSec";
            this.textBox_dm_exec_query_stats_IntervalSec.Size = new System.Drawing.Size(96, 23);
            this.textBox_dm_exec_query_stats_IntervalSec.TabIndex = 54;
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
            // textBox03Comment
            // 
            this.textBox03Comment.AcceptsReturn = true;
            this.textBox03Comment.BackColor = System.Drawing.SystemColors.Menu;
            this.textBox03Comment.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox03Comment.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.textBox03Comment.Location = new System.Drawing.Point(23, 245);
            this.textBox03Comment.Multiline = true;
            this.textBox03Comment.Name = "textBox03Comment";
            this.textBox03Comment.Size = new System.Drawing.Size(572, 35);
            this.textBox03Comment.TabIndex = 90;
            this.textBox03Comment.Text = "comment";
            // 
            // label4
            // 
            this.label4InMinutes.AutoSize = true;
            this.label4InMinutes.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold);
            this.label4InMinutes.Location = new System.Drawing.Point(499, 224);
            this.label4InMinutes.Name = "label4";
            this.label4InMinutes.Size = new System.Drawing.Size(105, 15);
            this.label4InMinutes.TabIndex = 89;
            this.label4InMinutes.Text = "(in minutes) :";
            // 
            // textBoxRetentionPeriodMinutes
            // 
            this.textBoxRetentionPeriodMinutes.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBoxRetentionPeriodMinutes.Location = new System.Drawing.Point(610, 220);
            this.textBoxRetentionPeriodMinutes.Name = "textBoxRetentionPeriodMinutes";
            this.textBoxRetentionPeriodMinutes.Size = new System.Drawing.Size(96, 23);
            this.textBoxRetentionPeriodMinutes.TabIndex = 88;
            // 
            // label5
            // 
            this.labelRetentionPeriod.AutoSize = true;
            this.labelRetentionPeriod.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold);
            this.labelRetentionPeriod.Location = new System.Drawing.Point(20, 224);
            this.labelRetentionPeriod.Name = "label5";
            this.labelRetentionPeriod.Size = new System.Drawing.Size(119, 15);
            this.labelRetentionPeriod.TabIndex = 87;
            this.labelRetentionPeriod.Text = "Retention Period";
            // 
            // UcSqlmonPolicy
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.groupBoxMirroring);
            this.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.Name = "UcSqlmonPolicy";
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
        private System.Windows.Forms.TextBox textBox_dm_exec_query_stats_IntervalSec;
        private System.Windows.Forms.Label label00;
        private System.Windows.Forms.Label label01;
        private System.Windows.Forms.TextBox textBox_dm_os_workers_IntervalSec;
        private System.Windows.Forms.Button buttonSavePolicy;
        private System.Windows.Forms.Label label2InSeconds;
        private System.Windows.Forms.Label label1InSeconds;
        private System.Windows.Forms.Label label02;
        private System.Windows.Forms.TextBox textBox_sp_lock2_intervalSec;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox02Comment;
        private System.Windows.Forms.TextBox textBox01Comment;
        private System.Windows.Forms.TextBox textBox00Comment;
        private System.Windows.Forms.TextBox lastMessage;
        private System.Windows.Forms.TextBox textBox03Comment;
        private System.Windows.Forms.Label label4InMinutes;
        private System.Windows.Forms.TextBox textBoxRetentionPeriodMinutes;
        private System.Windows.Forms.Label labelRetentionPeriod;
    }
}
