namespace HaTool.Server
{
    partial class FormNcpRestPreview
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormNcpRestPreview));
            this.buttonExecute = new System.Windows.Forms.Button();
            this.textBoxAction = new System.Windows.Forms.TextBox();
            this.labelAction = new System.Windows.Forms.Label();
            this.splitContainerCommand = new System.Windows.Forms.SplitContainer();
            this.groupBoxCommand = new System.Windows.Forms.GroupBox();
            this.fastColoredTextBoxCommand = new FastColoredTextBoxNS.FastColoredTextBox();
            this.groupBoxResult = new System.Windows.Forms.GroupBox();
            this.fastColoredTextBoxResult = new FastColoredTextBoxNS.FastColoredTextBox();
            this.buttonLoadPemFile = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerCommand)).BeginInit();
            this.splitContainerCommand.Panel1.SuspendLayout();
            this.splitContainerCommand.Panel2.SuspendLayout();
            this.splitContainerCommand.SuspendLayout();
            this.groupBoxCommand.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fastColoredTextBoxCommand)).BeginInit();
            this.groupBoxResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fastColoredTextBoxResult)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonExecute
            // 
            this.buttonExecute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExecute.Location = new System.Drawing.Point(320, 41);
            this.buttonExecute.Name = "buttonExecute";
            this.buttonExecute.Size = new System.Drawing.Size(225, 25);
            this.buttonExecute.TabIndex = 90;
            this.buttonExecute.Text = "Execute";
            this.buttonExecute.UseVisualStyleBackColor = true;
            this.buttonExecute.Click += new System.EventHandler(this.buttonExecute_Click);
            // 
            // textBoxAction
            // 
            this.textBoxAction.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxAction.Location = new System.Drawing.Point(75, 12);
            this.textBoxAction.Name = "textBoxAction";
            this.textBoxAction.Size = new System.Drawing.Size(470, 23);
            this.textBoxAction.TabIndex = 89;
            // 
            // labelAction
            // 
            this.labelAction.AutoSize = true;
            this.labelAction.Location = new System.Drawing.Point(14, 15);
            this.labelAction.Name = "labelAction";
            this.labelAction.Size = new System.Drawing.Size(49, 15);
            this.labelAction.TabIndex = 88;
            this.labelAction.Text = "Action";
            // 
            // splitContainerCommand
            // 
            this.splitContainerCommand.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerCommand.Location = new System.Drawing.Point(12, 72);
            this.splitContainerCommand.Name = "splitContainerCommand";
            this.splitContainerCommand.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerCommand.Panel1
            // 
            this.splitContainerCommand.Panel1.Controls.Add(this.groupBoxCommand);
            // 
            // splitContainerCommand.Panel2
            // 
            this.splitContainerCommand.Panel2.Controls.Add(this.groupBoxResult);
            this.splitContainerCommand.Size = new System.Drawing.Size(533, 374);
            this.splitContainerCommand.SplitterDistance = 129;
            this.splitContainerCommand.TabIndex = 91;
            // 
            // groupBoxCommand
            // 
            this.groupBoxCommand.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxCommand.Controls.Add(this.fastColoredTextBoxCommand);
            this.groupBoxCommand.Location = new System.Drawing.Point(5, 3);
            this.groupBoxCommand.Name = "groupBoxCommand";
            this.groupBoxCommand.Size = new System.Drawing.Size(525, 123);
            this.groupBoxCommand.TabIndex = 0;
            this.groupBoxCommand.TabStop = false;
            this.groupBoxCommand.Text = "Command";
            // 
            // fastColoredTextBoxCommand
            // 
            this.fastColoredTextBoxCommand.AutoCompleteBracketsList = new char[] {
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
            this.fastColoredTextBoxCommand.AutoScrollMinSize = new System.Drawing.Size(27, 14);
            this.fastColoredTextBoxCommand.BackBrush = null;
            this.fastColoredTextBoxCommand.CharHeight = 14;
            this.fastColoredTextBoxCommand.CharWidth = 8;
            this.fastColoredTextBoxCommand.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.fastColoredTextBoxCommand.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.fastColoredTextBoxCommand.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fastColoredTextBoxCommand.Font = new System.Drawing.Font("Courier New", 9.75F);
            this.fastColoredTextBoxCommand.IsReplaceMode = false;
            this.fastColoredTextBoxCommand.Location = new System.Drawing.Point(3, 19);
            this.fastColoredTextBoxCommand.Name = "fastColoredTextBoxCommand";
            this.fastColoredTextBoxCommand.Paddings = new System.Windows.Forms.Padding(0);
            this.fastColoredTextBoxCommand.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.fastColoredTextBoxCommand.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("fastColoredTextBoxCommand.ServiceColors")));
            this.fastColoredTextBoxCommand.Size = new System.Drawing.Size(519, 101);
            this.fastColoredTextBoxCommand.TabIndex = 63;
            this.fastColoredTextBoxCommand.Zoom = 100;
            // 
            // groupBoxResult
            // 
            this.groupBoxResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxResult.Controls.Add(this.fastColoredTextBoxResult);
            this.groupBoxResult.Location = new System.Drawing.Point(5, 3);
            this.groupBoxResult.Name = "groupBoxResult";
            this.groupBoxResult.Size = new System.Drawing.Size(525, 235);
            this.groupBoxResult.TabIndex = 1;
            this.groupBoxResult.TabStop = false;
            this.groupBoxResult.Text = "Result";
            // 
            // fastColoredTextBoxResult
            // 
            this.fastColoredTextBoxResult.AutoCompleteBracketsList = new char[] {
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
            this.fastColoredTextBoxResult.AutoScrollMinSize = new System.Drawing.Size(27, 14);
            this.fastColoredTextBoxResult.BackBrush = null;
            this.fastColoredTextBoxResult.CharHeight = 14;
            this.fastColoredTextBoxResult.CharWidth = 8;
            this.fastColoredTextBoxResult.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.fastColoredTextBoxResult.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.fastColoredTextBoxResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fastColoredTextBoxResult.Font = new System.Drawing.Font("Courier New", 9.75F);
            this.fastColoredTextBoxResult.IsReplaceMode = false;
            this.fastColoredTextBoxResult.Location = new System.Drawing.Point(3, 19);
            this.fastColoredTextBoxResult.Name = "fastColoredTextBoxResult";
            this.fastColoredTextBoxResult.Paddings = new System.Windows.Forms.Padding(0);
            this.fastColoredTextBoxResult.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.fastColoredTextBoxResult.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("fastColoredTextBoxResult.ServiceColors")));
            this.fastColoredTextBoxResult.Size = new System.Drawing.Size(519, 213);
            this.fastColoredTextBoxResult.TabIndex = 63;
            this.fastColoredTextBoxResult.Zoom = 100;
            // 
            // buttonLoadPemFile
            // 
            this.buttonLoadPemFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonLoadPemFile.Location = new System.Drawing.Point(89, 41);
            this.buttonLoadPemFile.Name = "buttonLoadPemFile";
            this.buttonLoadPemFile.Size = new System.Drawing.Size(225, 25);
            this.buttonLoadPemFile.TabIndex = 92;
            this.buttonLoadPemFile.Text = "Load Pem File";
            this.buttonLoadPemFile.UseVisualStyleBackColor = true;
            this.buttonLoadPemFile.Click += new System.EventHandler(this.buttonLoadPemFile_Click);
            // 
            // FormNcpRestPreview
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(557, 458);
            this.Controls.Add(this.buttonLoadPemFile);
            this.Controls.Add(this.splitContainerCommand);
            this.Controls.Add(this.buttonExecute);
            this.Controls.Add(this.textBoxAction);
            this.Controls.Add(this.labelAction);
            this.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormNcpRestPreview";
            this.Text = "Command Preview";
            this.Load += new System.EventHandler(this.LoadPreview);
            this.splitContainerCommand.Panel1.ResumeLayout(false);
            this.splitContainerCommand.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerCommand)).EndInit();
            this.splitContainerCommand.ResumeLayout(false);
            this.groupBoxCommand.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fastColoredTextBoxCommand)).EndInit();
            this.groupBoxResult.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fastColoredTextBoxResult)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonExecute;
        private System.Windows.Forms.TextBox textBoxAction;
        private System.Windows.Forms.Label labelAction;
        private System.Windows.Forms.SplitContainer splitContainerCommand;
        private System.Windows.Forms.GroupBox groupBoxCommand;
        private FastColoredTextBoxNS.FastColoredTextBox fastColoredTextBoxCommand;
        private System.Windows.Forms.GroupBox groupBoxResult;
        private FastColoredTextBoxNS.FastColoredTextBox fastColoredTextBoxResult;
        private System.Windows.Forms.Button buttonLoadPemFile;
    }
}