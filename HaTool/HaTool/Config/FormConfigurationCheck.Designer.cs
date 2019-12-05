namespace HaTool.Config
{
    partial class FormConfigurationCheck
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormConfigurationCheck));
            this.buttonCancel = new System.Windows.Forms.Button();
            this.textBoxComment = new System.Windows.Forms.TextBox();
            this.buttonCreateBucket = new System.Windows.Forms.Button();
            this.groupBoxOuter = new System.Windows.Forms.GroupBox();
            this.textBoxLog = new FastColoredTextBoxNS.FastColoredTextBox();
            this.groupBoxOuter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxLog)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Location = new System.Drawing.Point(662, 362);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(139, 23);
            this.buttonCancel.TabIndex = 7;
            this.buttonCancel.Text = "Close";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // textBoxComment
            // 
            this.textBoxComment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxComment.Location = new System.Drawing.Point(24, 325);
            this.textBoxComment.Multiline = true;
            this.textBoxComment.Name = "textBoxComment";
            this.textBoxComment.Size = new System.Drawing.Size(777, 31);
            this.textBoxComment.TabIndex = 9;
            this.textBoxComment.Text = "Click the check button below to check that the SQL Server DBA Tool settings are c" +
    "orrect.";
            // 
            // buttonCreateBucket
            // 
            this.buttonCreateBucket.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCreateBucket.Location = new System.Drawing.Point(517, 362);
            this.buttonCreateBucket.Name = "buttonCreateBucket";
            this.buttonCreateBucket.Size = new System.Drawing.Size(139, 23);
            this.buttonCreateBucket.TabIndex = 9;
            this.buttonCreateBucket.Text = "Check";
            this.buttonCreateBucket.UseVisualStyleBackColor = true;
            this.buttonCreateBucket.Click += new System.EventHandler(this.buttonCreateBucket_Click);
            // 
            // groupBoxOuter
            // 
            this.groupBoxOuter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxOuter.Controls.Add(this.textBoxLog);
            this.groupBoxOuter.Location = new System.Drawing.Point(7, 7);
            this.groupBoxOuter.Name = "groupBoxOuter";
            this.groupBoxOuter.Size = new System.Drawing.Size(812, 312);
            this.groupBoxOuter.TabIndex = 0;
            this.groupBoxOuter.TabStop = false;
            this.groupBoxOuter.Text = "Config > Configuration Check";
            // 
            // textBoxLog
            // 
            this.textBoxLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxLog.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.textBoxLog.AutoScrollMinSize = new System.Drawing.Size(27, 14);
            this.textBoxLog.BackBrush = null;
            this.textBoxLog.CharHeight = 14;
            this.textBoxLog.CharWidth = 8;
            this.textBoxLog.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxLog.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.textBoxLog.IsReplaceMode = false;
            this.textBoxLog.Location = new System.Drawing.Point(11, 22);
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.Paddings = new System.Windows.Forms.Padding(0);
            this.textBoxLog.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.textBoxLog.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("textBoxLog.ServiceColors")));
            this.textBoxLog.Size = new System.Drawing.Size(795, 284);
            this.textBoxLog.TabIndex = 3;
            this.textBoxLog.Zoom = 100;
            // 
            // FormCheckConfiguration
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(826, 397);
            this.Controls.Add(this.buttonCreateBucket);
            this.Controls.Add(this.textBoxComment);
            this.Controls.Add(this.groupBoxOuter);
            this.Controls.Add(this.buttonCancel);
            this.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormCheckConfiguration";
            this.Text = "Configuration Check";
            this.Load += new System.EventHandler(this.LoadData);
            this.groupBoxOuter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textBoxLog)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TextBox textBoxComment;
        private System.Windows.Forms.Button buttonCreateBucket;
        private System.Windows.Forms.GroupBox groupBoxOuter;
        private FastColoredTextBoxNS.FastColoredTextBox textBoxLog;
    }
}