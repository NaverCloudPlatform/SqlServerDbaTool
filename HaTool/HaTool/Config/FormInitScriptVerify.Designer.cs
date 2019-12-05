namespace HaTool.Config
{
    partial class FormInitScriptVerify
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormInitScriptVerify));
            this.groupBoxInitScriptContentsVerify = new System.Windows.Forms.GroupBox();
            this.panelPowerShellParents = new System.Windows.Forms.Panel();
            this.splitContainerPowerShell = new System.Windows.Forms.SplitContainer();
            this.fastColoredTextBoxPowerShellScriptContents = new FastColoredTextBoxNS.FastColoredTextBox();
            this.documentMap = new FastColoredTextBoxNS.DocumentMap();
            this.panelUserData = new System.Windows.Forms.Panel();
            this.fastColoredTextBoxUserData = new FastColoredTextBoxNS.FastColoredTextBox();
            this.labelUploadedPowerShellRemoteScript = new System.Windows.Forms.Label();
            this.labelTextBoxUserData = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.groupBoxInitScriptContentsVerify.SuspendLayout();
            this.panelPowerShellParents.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerPowerShell)).BeginInit();
            this.splitContainerPowerShell.Panel1.SuspendLayout();
            this.splitContainerPowerShell.Panel2.SuspendLayout();
            this.splitContainerPowerShell.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fastColoredTextBoxPowerShellScriptContents)).BeginInit();
            this.panelUserData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fastColoredTextBoxUserData)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxInitScriptContentsVerify
            // 
            this.groupBoxInitScriptContentsVerify.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxInitScriptContentsVerify.Controls.Add(this.panelPowerShellParents);
            this.groupBoxInitScriptContentsVerify.Controls.Add(this.panelUserData);
            this.groupBoxInitScriptContentsVerify.Controls.Add(this.labelUploadedPowerShellRemoteScript);
            this.groupBoxInitScriptContentsVerify.Controls.Add(this.labelTextBoxUserData);
            this.groupBoxInitScriptContentsVerify.Location = new System.Drawing.Point(7, 7);
            this.groupBoxInitScriptContentsVerify.Name = "groupBoxInitScriptContentsVerify";
            this.groupBoxInitScriptContentsVerify.Size = new System.Drawing.Size(731, 455);
            this.groupBoxInitScriptContentsVerify.TabIndex = 0;
            this.groupBoxInitScriptContentsVerify.TabStop = false;
            this.groupBoxInitScriptContentsVerify.Text = "Config > Init Script Contents Verify";
            // 
            // panelPowerShellParents
            // 
            this.panelPowerShellParents.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelPowerShellParents.Controls.Add(this.splitContainerPowerShell);
            this.panelPowerShellParents.Location = new System.Drawing.Point(37, 133);
            this.panelPowerShellParents.Name = "panelPowerShellParents";
            this.panelPowerShellParents.Size = new System.Drawing.Size(675, 307);
            this.panelPowerShellParents.TabIndex = 11;
            // 
            // splitContainerPowerShell
            // 
            this.splitContainerPowerShell.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerPowerShell.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainerPowerShell.IsSplitterFixed = true;
            this.splitContainerPowerShell.Location = new System.Drawing.Point(0, 0);
            this.splitContainerPowerShell.Name = "splitContainerPowerShell";
            // 
            // splitContainerPowerShell.Panel1
            // 
            this.splitContainerPowerShell.Panel1.Controls.Add(this.fastColoredTextBoxPowerShellScriptContents);
            this.splitContainerPowerShell.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // splitContainerPowerShell.Panel2
            // 
            this.splitContainerPowerShell.Panel2.Controls.Add(this.documentMap);
            this.splitContainerPowerShell.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainerPowerShell.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainerPowerShell.Size = new System.Drawing.Size(675, 307);
            this.splitContainerPowerShell.SplitterDistance = 625;
            this.splitContainerPowerShell.TabIndex = 0;
            // 
            // fastColoredTextBoxPowerShellScriptContents
            // 
            this.fastColoredTextBoxPowerShellScriptContents.AutoCompleteBracketsList = new char[] {
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
            this.fastColoredTextBoxPowerShellScriptContents.AutoScrollMinSize = new System.Drawing.Size(179, 14);
            this.fastColoredTextBoxPowerShellScriptContents.BackBrush = null;
            this.fastColoredTextBoxPowerShellScriptContents.CharHeight = 14;
            this.fastColoredTextBoxPowerShellScriptContents.CharWidth = 8;
            this.fastColoredTextBoxPowerShellScriptContents.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.fastColoredTextBoxPowerShellScriptContents.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.fastColoredTextBoxPowerShellScriptContents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fastColoredTextBoxPowerShellScriptContents.Font = new System.Drawing.Font("Courier New", 9.75F);
            this.fastColoredTextBoxPowerShellScriptContents.IndentBackColor = System.Drawing.Color.White;
            this.fastColoredTextBoxPowerShellScriptContents.IsReplaceMode = false;
            this.fastColoredTextBoxPowerShellScriptContents.Location = new System.Drawing.Point(0, 0);
            this.fastColoredTextBoxPowerShellScriptContents.Name = "fastColoredTextBoxPowerShellScriptContents";
            this.fastColoredTextBoxPowerShellScriptContents.Paddings = new System.Windows.Forms.Padding(0);
            this.fastColoredTextBoxPowerShellScriptContents.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.fastColoredTextBoxPowerShellScriptContents.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("fastColoredTextBoxPowerShellScriptContents.ServiceColors")));
            this.fastColoredTextBoxPowerShellScriptContents.Size = new System.Drawing.Size(625, 307);
            this.fastColoredTextBoxPowerShellScriptContents.TabIndex = 0;
            this.fastColoredTextBoxPowerShellScriptContents.Text = "fastColoredTextBox1";
            this.fastColoredTextBoxPowerShellScriptContents.Zoom = 100;
            // 
            // documentMap
            // 
            this.documentMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.documentMap.ForeColor = System.Drawing.Color.Maroon;
            this.documentMap.Location = new System.Drawing.Point(0, 0);
            this.documentMap.Name = "documentMap";
            this.documentMap.Size = new System.Drawing.Size(46, 307);
            this.documentMap.TabIndex = 0;
            this.documentMap.Target = this.fastColoredTextBoxPowerShellScriptContents;
            this.documentMap.Text = "documentMap1";
            // 
            // panelUserData
            // 
            this.panelUserData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelUserData.Controls.Add(this.fastColoredTextBoxUserData);
            this.panelUserData.Location = new System.Drawing.Point(37, 57);
            this.panelUserData.Name = "panelUserData";
            this.panelUserData.Size = new System.Drawing.Size(675, 36);
            this.panelUserData.TabIndex = 10;
            // 
            // fastColoredTextBoxUserData
            // 
            this.fastColoredTextBoxUserData.AutoCompleteBracketsList = new char[] {
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
            this.fastColoredTextBoxUserData.AutoScrollMinSize = new System.Drawing.Size(179, 14);
            this.fastColoredTextBoxUserData.BackBrush = null;
            this.fastColoredTextBoxUserData.CharHeight = 14;
            this.fastColoredTextBoxUserData.CharWidth = 8;
            this.fastColoredTextBoxUserData.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.fastColoredTextBoxUserData.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.fastColoredTextBoxUserData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fastColoredTextBoxUserData.Font = new System.Drawing.Font("Courier New", 9.75F);
            this.fastColoredTextBoxUserData.IndentBackColor = System.Drawing.Color.White;
            this.fastColoredTextBoxUserData.IsReplaceMode = false;
            this.fastColoredTextBoxUserData.Location = new System.Drawing.Point(0, 0);
            this.fastColoredTextBoxUserData.Name = "fastColoredTextBoxUserData";
            this.fastColoredTextBoxUserData.Paddings = new System.Windows.Forms.Padding(0);
            this.fastColoredTextBoxUserData.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.fastColoredTextBoxUserData.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("fastColoredTextBoxUserData.ServiceColors")));
            this.fastColoredTextBoxUserData.Size = new System.Drawing.Size(675, 36);
            this.fastColoredTextBoxUserData.TabIndex = 0;
            this.fastColoredTextBoxUserData.Text = "fastColoredTextBox2";
            this.fastColoredTextBoxUserData.Zoom = 100;
            // 
            // labelUploadedPowerShellRemoteScript
            // 
            this.labelUploadedPowerShellRemoteScript.AutoSize = true;
            this.labelUploadedPowerShellRemoteScript.Location = new System.Drawing.Point(20, 108);
            this.labelUploadedPowerShellRemoteScript.Name = "labelUploadedPowerShellRemoteScript";
            this.labelUploadedPowerShellRemoteScript.Size = new System.Drawing.Size(238, 15);
            this.labelUploadedPowerShellRemoteScript.TabIndex = 9;
            this.labelUploadedPowerShellRemoteScript.Text = "Uploaded PowerShell Remote Script";
            // 
            // labelTextBoxUserData
            // 
            this.labelTextBoxUserData.AutoSize = true;
            this.labelTextBoxUserData.Location = new System.Drawing.Point(20, 27);
            this.labelTextBoxUserData.Name = "labelTextBoxUserData";
            this.labelTextBoxUserData.Size = new System.Drawing.Size(294, 15);
            this.labelTextBoxUserData.TabIndex = 8;
            this.labelTextBoxUserData.Text = "UserData (for windows server init script)";
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.Location = new System.Drawing.Point(596, 468);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(139, 23);
            this.buttonClose.TabIndex = 8;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // FormInitScriptVerify
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(746, 498);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.groupBoxInitScriptContentsVerify);
            this.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormInitScriptVerify";
            this.Text = "Init Script Verify";
            this.groupBoxInitScriptContentsVerify.ResumeLayout(false);
            this.groupBoxInitScriptContentsVerify.PerformLayout();
            this.panelPowerShellParents.ResumeLayout(false);
            this.splitContainerPowerShell.Panel1.ResumeLayout(false);
            this.splitContainerPowerShell.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerPowerShell)).EndInit();
            this.splitContainerPowerShell.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fastColoredTextBoxPowerShellScriptContents)).EndInit();
            this.panelUserData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fastColoredTextBoxUserData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxInitScriptContentsVerify;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Label labelUploadedPowerShellRemoteScript;
        private System.Windows.Forms.Label labelTextBoxUserData;
        private System.Windows.Forms.Panel panelPowerShellParents;
        private System.Windows.Forms.Panel panelUserData;
        private FastColoredTextBoxNS.FastColoredTextBox fastColoredTextBoxUserData;
        private System.Windows.Forms.SplitContainer splitContainerPowerShell;
        private FastColoredTextBoxNS.FastColoredTextBox fastColoredTextBoxPowerShellScriptContents;
        private FastColoredTextBoxNS.DocumentMap documentMap;
    }
}