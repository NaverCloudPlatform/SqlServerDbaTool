namespace HaTool.HighAvailability
{
    partial class UcBackupPolicy
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
            this.comboBoxcompressionYN = new System.Windows.Forms.ComboBox();
            this.textBox05Comment = new System.Windows.Forms.TextBox();
            this.label9InSeconds = new System.Windows.Forms.Label();
            this.label05 = new System.Windows.Forms.Label();
            this.textBoxPurgeObjectLimitSec = new System.Windows.Forms.TextBox();
            this.textBox04Comment = new System.Windows.Forms.TextBox();
            this.label7InSeconds = new System.Windows.Forms.Label();
            this.label04 = new System.Windows.Forms.Label();
            this.textBoxPurgeLocalLimitSec = new System.Windows.Forms.TextBox();
            this.textBox03Comment = new System.Windows.Forms.TextBox();
            this.label03 = new System.Windows.Forms.Label();
            this.textBox02Comment = new System.Windows.Forms.TextBox();
            this.label3Example = new System.Windows.Forms.Label();
            this.label02 = new System.Windows.Forms.Label();
            this.textBoxPath = new System.Windows.Forms.TextBox();
            this.textBox01Comment = new System.Windows.Forms.TextBox();
            this.label2InSeconds = new System.Windows.Forms.Label();
            this.label1InSeconds = new System.Windows.Forms.Label();
            this.label01 = new System.Windows.Forms.Label();
            this.textBoxLogBackupIntervalSec = new System.Windows.Forms.TextBox();
            this.textBox00Comment = new System.Windows.Forms.TextBox();
            this.label00 = new System.Windows.Forms.Label();
            this.textBoxFullBackupIntervalSec = new System.Windows.Forms.TextBox();
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
            this.groupBoxMirroring.Size = new System.Drawing.Size(770, 831);
            this.groupBoxMirroring.TabIndex = 1;
            this.groupBoxMirroring.TabStop = false;
            this.groupBoxMirroring.Text = "High Availability > Backup Policy";
            // 
            // groupBoxFailoverPolicy
            // 
            this.groupBoxFailoverPolicy.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxFailoverPolicy.Controls.Add(this.lastMessage);
            this.groupBoxFailoverPolicy.Controls.Add(this.comboBoxcompressionYN);
            this.groupBoxFailoverPolicy.Controls.Add(this.textBox05Comment);
            this.groupBoxFailoverPolicy.Controls.Add(this.label9InSeconds);
            this.groupBoxFailoverPolicy.Controls.Add(this.label05);
            this.groupBoxFailoverPolicy.Controls.Add(this.textBoxPurgeObjectLimitSec);
            this.groupBoxFailoverPolicy.Controls.Add(this.textBox04Comment);
            this.groupBoxFailoverPolicy.Controls.Add(this.label7InSeconds);
            this.groupBoxFailoverPolicy.Controls.Add(this.label04);
            this.groupBoxFailoverPolicy.Controls.Add(this.textBoxPurgeLocalLimitSec);
            this.groupBoxFailoverPolicy.Controls.Add(this.textBox03Comment);
            this.groupBoxFailoverPolicy.Controls.Add(this.label03);
            this.groupBoxFailoverPolicy.Controls.Add(this.textBox02Comment);
            this.groupBoxFailoverPolicy.Controls.Add(this.label3Example);
            this.groupBoxFailoverPolicy.Controls.Add(this.label02);
            this.groupBoxFailoverPolicy.Controls.Add(this.textBoxPath);
            this.groupBoxFailoverPolicy.Controls.Add(this.textBox01Comment);
            this.groupBoxFailoverPolicy.Controls.Add(this.label2InSeconds);
            this.groupBoxFailoverPolicy.Controls.Add(this.label1InSeconds);
            this.groupBoxFailoverPolicy.Controls.Add(this.label01);
            this.groupBoxFailoverPolicy.Controls.Add(this.textBoxLogBackupIntervalSec);
            this.groupBoxFailoverPolicy.Controls.Add(this.textBox00Comment);
            this.groupBoxFailoverPolicy.Controls.Add(this.label00);
            this.groupBoxFailoverPolicy.Controls.Add(this.textBoxFullBackupIntervalSec);
            this.groupBoxFailoverPolicy.Location = new System.Drawing.Point(22, 297);
            this.groupBoxFailoverPolicy.Name = "groupBoxFailoverPolicy";
            this.groupBoxFailoverPolicy.Size = new System.Drawing.Size(742, 528);
            this.groupBoxFailoverPolicy.TabIndex = 162;
            this.groupBoxFailoverPolicy.TabStop = false;
            this.groupBoxFailoverPolicy.Text = "Backup Policy Setting";
            // 
            // comboBoxcompressionYN
            // 
            this.comboBoxcompressionYN.FormattingEnabled = true;
            this.comboBoxcompressionYN.Items.AddRange(new object[] {
            "Y",
            "N"});
            this.comboBoxcompressionYN.Location = new System.Drawing.Point(610, 220);
            this.comboBoxcompressionYN.Name = "comboBoxcompressionYN";
            this.comboBoxcompressionYN.Size = new System.Drawing.Size(96, 23);
            this.comboBoxcompressionYN.TabIndex = 79;
            // 
            // textBox05Comment
            // 
            this.textBox05Comment.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox05Comment.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.textBox05Comment.Location = new System.Drawing.Point(22, 362);
            this.textBox05Comment.Multiline = true;
            this.textBox05Comment.Name = "textBox05Comment";
            this.textBox05Comment.Size = new System.Drawing.Size(572, 35);
            this.textBox05Comment.TabIndex = 78;
            // 
            // label9
            // 
            this.label9InSeconds.AutoSize = true;
            this.label9InSeconds.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold);
            this.label9InSeconds.Location = new System.Drawing.Point(499, 342);
            this.label9InSeconds.Name = "label9";
            this.label9InSeconds.Size = new System.Drawing.Size(105, 15);
            this.label9InSeconds.TabIndex = 77;
            this.label9InSeconds.Text = "(in seconds) :";
            // 
            // label05
            // 
            this.label05.AutoSize = true;
            this.label05.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold);
            this.label05.Location = new System.Drawing.Point(20, 342);
            this.label05.Name = "label05";
            this.label05.Size = new System.Drawing.Size(196, 15);
            this.label05.TabIndex = 76;
            this.label05.Text = "Delete Backup ObjectStorage";
            // 
            // textBoxPurgeObjectLimitSec
            // 
            this.textBoxPurgeObjectLimitSec.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBoxPurgeObjectLimitSec.Location = new System.Drawing.Point(610, 337);
            this.textBoxPurgeObjectLimitSec.Name = "textBoxPurgeObjectLimitSec";
            this.textBoxPurgeObjectLimitSec.Size = new System.Drawing.Size(96, 23);
            this.textBoxPurgeObjectLimitSec.TabIndex = 75;
            // 
            // textBox04Comment
            // 
            this.textBox04Comment.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox04Comment.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.textBox04Comment.Location = new System.Drawing.Point(22, 303);
            this.textBox04Comment.Multiline = true;
            this.textBox04Comment.Name = "textBox04Comment";
            this.textBox04Comment.Size = new System.Drawing.Size(572, 35);
            this.textBox04Comment.TabIndex = 74;
            // 
            // label7
            // 
            this.label7InSeconds.AutoSize = true;
            this.label7InSeconds.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold);
            this.label7InSeconds.Location = new System.Drawing.Point(499, 283);
            this.label7InSeconds.Name = "label7";
            this.label7InSeconds.Size = new System.Drawing.Size(105, 15);
            this.label7InSeconds.TabIndex = 73;
            this.label7InSeconds.Text = "(in seconds) :";
            // 
            // label04
            // 
            this.label04.AutoSize = true;
            this.label04.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold);
            this.label04.Location = new System.Drawing.Point(20, 283);
            this.label04.Name = "label04";
            this.label04.Size = new System.Drawing.Size(140, 15);
            this.label04.TabIndex = 72;
            this.label04.Text = "Delete Backup Local";
            // 
            // textBoxPurgeLocalLimitSec
            // 
            this.textBoxPurgeLocalLimitSec.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBoxPurgeLocalLimitSec.Location = new System.Drawing.Point(610, 278);
            this.textBoxPurgeLocalLimitSec.Name = "textBoxPurgeLocalLimitSec";
            this.textBoxPurgeLocalLimitSec.Size = new System.Drawing.Size(96, 23);
            this.textBoxPurgeLocalLimitSec.TabIndex = 71;
            // 
            // textBox03Comment
            // 
            this.textBox03Comment.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox03Comment.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.textBox03Comment.Location = new System.Drawing.Point(22, 240);
            this.textBox03Comment.Multiline = true;
            this.textBox03Comment.Name = "textBox03Comment";
            this.textBox03Comment.Size = new System.Drawing.Size(572, 35);
            this.textBox03Comment.TabIndex = 70;
            // 
            // label03
            // 
            this.label03.AutoSize = true;
            this.label03.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold);
            this.label03.Location = new System.Drawing.Point(20, 220);
            this.label03.Name = "label03";
            this.label03.Size = new System.Drawing.Size(133, 15);
            this.label03.TabIndex = 68;
            this.label03.Text = "Backup Compression";
            // 
            // textBox02Comment
            // 
            this.textBox02Comment.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox02Comment.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.textBox02Comment.Location = new System.Drawing.Point(22, 179);
            this.textBox02Comment.Multiline = true;
            this.textBox02Comment.Name = "textBox02Comment";
            this.textBox02Comment.Size = new System.Drawing.Size(572, 35);
            this.textBox02Comment.TabIndex = 66;
            // 
            // label3
            // 
            this.label3Example.AutoSize = true;
            this.label3Example.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold);
            this.label3Example.Location = new System.Drawing.Point(478, 159);
            this.label3Example.Name = "label3";
            this.label3Example.Size = new System.Drawing.Size(126, 15);
            this.label3Example.TabIndex = 65;
            this.label3Example.Text = "(ex :  c:\\temp) :";
            // 
            // label02
            // 
            this.label02.AutoSize = true;
            this.label02.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold);
            this.label02.Location = new System.Drawing.Point(20, 159);
            this.label02.Name = "label02";
            this.label02.Size = new System.Drawing.Size(84, 15);
            this.label02.TabIndex = 64;
            this.label02.Text = "Backup Path";
            // 
            // textBoxPath
            // 
            this.textBoxPath.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBoxPath.Location = new System.Drawing.Point(610, 154);
            this.textBoxPath.Name = "textBoxPath";
            this.textBoxPath.Size = new System.Drawing.Size(96, 23);
            this.textBoxPath.TabIndex = 63;
            // 
            // textBox01Comment
            // 
            this.textBox01Comment.AcceptsReturn = true;
            this.textBox01Comment.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox01Comment.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.textBox01Comment.Location = new System.Drawing.Point(22, 118);
            this.textBox01Comment.Multiline = true;
            this.textBox01Comment.Name = "textBox01Comment";
            this.textBox01Comment.Size = new System.Drawing.Size(572, 35);
            this.textBox01Comment.TabIndex = 62;
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
            this.label01.Size = new System.Drawing.Size(224, 15);
            this.label01.TabIndex = 58;
            this.label01.Text = "Transaction Log Backup Interval";
            // 
            // textBoxLogBackupIntervalSec
            // 
            this.textBoxLogBackupIntervalSec.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBoxLogBackupIntervalSec.Location = new System.Drawing.Point(610, 93);
            this.textBoxLogBackupIntervalSec.Name = "textBoxLogBackupIntervalSec";
            this.textBoxLogBackupIntervalSec.Size = new System.Drawing.Size(96, 23);
            this.textBoxLogBackupIntervalSec.TabIndex = 57;
            // 
            // textBox00Comment
            // 
            this.textBox00Comment.AcceptsReturn = true;
            this.textBox00Comment.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox00Comment.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.textBox00Comment.Location = new System.Drawing.Point(22, 56);
            this.textBox00Comment.Multiline = true;
            this.textBox00Comment.Name = "textBox00Comment";
            this.textBox00Comment.Size = new System.Drawing.Size(572, 35);
            this.textBox00Comment.TabIndex = 56;
            // 
            // label00
            // 
            this.label00.AutoSize = true;
            this.label00.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold);
            this.label00.Location = new System.Drawing.Point(20, 36);
            this.label00.Name = "label00";
            this.label00.Size = new System.Drawing.Size(147, 15);
            this.label00.TabIndex = 55;
            this.label00.Text = "Full Backup Interval";
            // 
            // textBoxFullBackupIntervalSec
            // 
            this.textBoxFullBackupIntervalSec.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBoxFullBackupIntervalSec.Location = new System.Drawing.Point(610, 31);
            this.textBoxFullBackupIntervalSec.Name = "textBoxFullBackupIntervalSec";
            this.textBoxFullBackupIntervalSec.Size = new System.Drawing.Size(96, 23);
            this.textBoxFullBackupIntervalSec.TabIndex = 54;
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
            this.lastMessage.Location = new System.Drawing.Point(20, 412);
            this.lastMessage.Multiline = true;
            this.lastMessage.Name = "lastMessage";
            this.lastMessage.Size = new System.Drawing.Size(572, 35);
            this.lastMessage.TabIndex = 87;
            this.lastMessage.Text = "comment";
            // 
            // UcBackupPolicy
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.groupBoxMirroring);
            this.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.Name = "UcBackupPolicy";
            this.Size = new System.Drawing.Size(776, 837);
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
        private System.Windows.Forms.TextBox textBoxFullBackupIntervalSec;
        private System.Windows.Forms.Label label00;
        private System.Windows.Forms.TextBox textBox00Comment;
        private System.Windows.Forms.Label label01;
        private System.Windows.Forms.TextBox textBoxLogBackupIntervalSec;
        private System.Windows.Forms.Button buttonSavePolicy;
        private System.Windows.Forms.Label label2InSeconds;
        private System.Windows.Forms.Label label1InSeconds;
        private System.Windows.Forms.TextBox textBox05Comment;
        private System.Windows.Forms.Label label9InSeconds;
        private System.Windows.Forms.Label label05;
        private System.Windows.Forms.TextBox textBoxPurgeObjectLimitSec;
        private System.Windows.Forms.TextBox textBox04Comment;
        private System.Windows.Forms.Label label7InSeconds;
        private System.Windows.Forms.Label label04;
        private System.Windows.Forms.TextBox textBoxPurgeLocalLimitSec;
        private System.Windows.Forms.TextBox textBox03Comment;
        private System.Windows.Forms.Label label03;
        private System.Windows.Forms.TextBox textBox02Comment;
        private System.Windows.Forms.Label label3Example;
        private System.Windows.Forms.Label label02;
        private System.Windows.Forms.TextBox textBoxPath;
        private System.Windows.Forms.TextBox textBox01Comment;
        private System.Windows.Forms.ComboBox comboBoxcompressionYN;
        private System.Windows.Forms.TextBox lastMessage;
    }
}
