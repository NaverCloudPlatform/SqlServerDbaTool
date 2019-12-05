namespace HaTool.Tools
{
    partial class UcEncoderDecoder
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UcEncoderDecoder));
            this.groupBoxSetAgentKey = new System.Windows.Forms.GroupBox();
            this.groupBoxSelectAlgorithm = new System.Windows.Forms.GroupBox();
            this.labelKeyVector = new System.Windows.Forms.Label();
            this.label16Key = new System.Windows.Forms.Label();
            this.buttonDecode = new System.Windows.Forms.Button();
            this.textBoxRijndaelVector = new System.Windows.Forms.TextBox();
            this.textBoxRijndaelKey = new System.Windows.Forms.TextBox();
            this.radioButtonAesRijndael = new System.Windows.Forms.RadioButton();
            this.textBoxKey = new System.Windows.Forms.TextBox();
            this.radioButtonAes = new System.Windows.Forms.RadioButton();
            this.radioButtonBase64Ascii = new System.Windows.Forms.RadioButton();
            this.radioButtonBase64Unicode = new System.Windows.Forms.RadioButton();
            this.radioButtonUrlEncode = new System.Windows.Forms.RadioButton();
            this.buttonEncode = new System.Windows.Forms.Button();
            this.groupBoxResult = new System.Windows.Forms.GroupBox();
            this.splitContainerEncodeDecode = new System.Windows.Forms.SplitContainer();
            this.textBoxDecode = new FastColoredTextBoxNS.FastColoredTextBox();
            this.labelDecode = new System.Windows.Forms.Label();
            this.textBoxEncode = new FastColoredTextBoxNS.FastColoredTextBox();
            this.labelEncode = new System.Windows.Forms.Label();
            this.groupBoxSetAgentKey.SuspendLayout();
            this.groupBoxSelectAlgorithm.SuspendLayout();
            this.groupBoxResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerEncodeDecode)).BeginInit();
            this.splitContainerEncodeDecode.Panel1.SuspendLayout();
            this.splitContainerEncodeDecode.Panel2.SuspendLayout();
            this.splitContainerEncodeDecode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxDecode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxEncode)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxSetAgentKey
            // 
            this.groupBoxSetAgentKey.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxSetAgentKey.Controls.Add(this.groupBoxSelectAlgorithm);
            this.groupBoxSetAgentKey.Controls.Add(this.groupBoxResult);
            this.groupBoxSetAgentKey.Location = new System.Drawing.Point(3, 3);
            this.groupBoxSetAgentKey.Name = "groupBoxSetAgentKey";
            this.groupBoxSetAgentKey.Size = new System.Drawing.Size(969, 679);
            this.groupBoxSetAgentKey.TabIndex = 1;
            this.groupBoxSetAgentKey.TabStop = false;
            this.groupBoxSetAgentKey.Text = "Server > Encoder Decoder";
            // 
            // groupBoxSelectAlgorithm
            // 
            this.groupBoxSelectAlgorithm.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxSelectAlgorithm.Controls.Add(this.labelKeyVector);
            this.groupBoxSelectAlgorithm.Controls.Add(this.label16Key);
            this.groupBoxSelectAlgorithm.Controls.Add(this.buttonDecode);
            this.groupBoxSelectAlgorithm.Controls.Add(this.textBoxRijndaelVector);
            this.groupBoxSelectAlgorithm.Controls.Add(this.textBoxRijndaelKey);
            this.groupBoxSelectAlgorithm.Controls.Add(this.radioButtonAesRijndael);
            this.groupBoxSelectAlgorithm.Controls.Add(this.textBoxKey);
            this.groupBoxSelectAlgorithm.Controls.Add(this.radioButtonAes);
            this.groupBoxSelectAlgorithm.Controls.Add(this.radioButtonBase64Ascii);
            this.groupBoxSelectAlgorithm.Controls.Add(this.radioButtonBase64Unicode);
            this.groupBoxSelectAlgorithm.Controls.Add(this.radioButtonUrlEncode);
            this.groupBoxSelectAlgorithm.Controls.Add(this.buttonEncode);
            this.groupBoxSelectAlgorithm.Location = new System.Drawing.Point(22, 22);
            this.groupBoxSelectAlgorithm.Name = "groupBoxSelectAlgorithm";
            this.groupBoxSelectAlgorithm.Size = new System.Drawing.Size(940, 193);
            this.groupBoxSelectAlgorithm.TabIndex = 54;
            this.groupBoxSelectAlgorithm.TabStop = false;
            this.groupBoxSelectAlgorithm.Text = "Select Algorithm";
            // 
            // labelKeyVector
            // 
            this.labelKeyVector.AutoSize = true;
            this.labelKeyVector.Location = new System.Drawing.Point(482, 131);
            this.labelKeyVector.Name = "labelKeyVector";
            this.labelKeyVector.Size = new System.Drawing.Size(98, 15);
            this.labelKeyVector.TabIndex = 49;
            this.labelKeyVector.Text = "(key, vector)";
            // 
            // label16Key
            // 
            this.label16Key.AutoSize = true;
            this.label16Key.Location = new System.Drawing.Point(314, 101);
            this.label16Key.Name = "label16Key";
            this.label16Key.Size = new System.Drawing.Size(98, 15);
            this.label16Key.TabIndex = 48;
            this.label16Key.Text = "(16 char key)";
            // 
            // buttonDecode
            // 
            this.buttonDecode.Location = new System.Drawing.Point(135, 162);
            this.buttonDecode.Name = "buttonDecode";
            this.buttonDecode.Size = new System.Drawing.Size(107, 25);
            this.buttonDecode.TabIndex = 47;
            this.buttonDecode.Text = "Decode";
            this.buttonDecode.UseVisualStyleBackColor = true;
            this.buttonDecode.Click += new System.EventHandler(this.buttonDecode_Click);
            // 
            // textBoxRijndaelVector
            // 
            this.textBoxRijndaelVector.Location = new System.Drawing.Point(313, 127);
            this.textBoxRijndaelVector.Name = "textBoxRijndaelVector";
            this.textBoxRijndaelVector.Size = new System.Drawing.Size(163, 23);
            this.textBoxRijndaelVector.TabIndex = 46;
            // 
            // textBoxRijndaelKey
            // 
            this.textBoxRijndaelKey.Location = new System.Drawing.Point(145, 127);
            this.textBoxRijndaelKey.Name = "textBoxRijndaelKey";
            this.textBoxRijndaelKey.Size = new System.Drawing.Size(163, 23);
            this.textBoxRijndaelKey.TabIndex = 44;
            // 
            // radioButtonAesRijndael
            // 
            this.radioButtonAesRijndael.AutoSize = true;
            this.radioButtonAesRijndael.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.radioButtonAesRijndael.Location = new System.Drawing.Point(27, 127);
            this.radioButtonAesRijndael.Name = "radioButtonAesRijndael";
            this.radioButtonAesRijndael.Size = new System.Drawing.Size(109, 19);
            this.radioButtonAesRijndael.TabIndex = 45;
            this.radioButtonAesRijndael.TabStop = true;
            this.radioButtonAesRijndael.Text = "AES Rijndael";
            this.radioButtonAesRijndael.UseVisualStyleBackColor = true;
            this.radioButtonAesRijndael.Click += new System.EventHandler(this.radioButtonAlgorithm_Checked);
            // 
            // textBoxKey
            // 
            this.textBoxKey.Location = new System.Drawing.Point(145, 98);
            this.textBoxKey.Name = "textBoxKey";
            this.textBoxKey.Size = new System.Drawing.Size(163, 23);
            this.textBoxKey.TabIndex = 42;
            // 
            // radioButtonAes
            // 
            this.radioButtonAes.AutoSize = true;
            this.radioButtonAes.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.radioButtonAes.Location = new System.Drawing.Point(27, 103);
            this.radioButtonAes.Name = "radioButtonAes";
            this.radioButtonAes.Size = new System.Drawing.Size(46, 19);
            this.radioButtonAes.TabIndex = 43;
            this.radioButtonAes.TabStop = true;
            this.radioButtonAes.Text = "AES";
            this.radioButtonAes.UseVisualStyleBackColor = true;
            this.radioButtonAes.Click += new System.EventHandler(this.radioButtonAlgorithm_Checked);
            // 
            // radioButtonBase64Ascii
            // 
            this.radioButtonBase64Ascii.AutoSize = true;
            this.radioButtonBase64Ascii.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.radioButtonBase64Ascii.Location = new System.Drawing.Point(27, 55);
            this.radioButtonBase64Ascii.Name = "radioButtonBase64Ascii";
            this.radioButtonBase64Ascii.Size = new System.Drawing.Size(102, 19);
            this.radioButtonBase64Ascii.TabIndex = 41;
            this.radioButtonBase64Ascii.TabStop = true;
            this.radioButtonBase64Ascii.Text = "Base64Ascii";
            this.radioButtonBase64Ascii.UseVisualStyleBackColor = true;
            this.radioButtonBase64Ascii.Click += new System.EventHandler(this.radioButtonAlgorithm_Checked);
            // 
            // radioButtonBase64Unicode
            // 
            this.radioButtonBase64Unicode.AutoSize = true;
            this.radioButtonBase64Unicode.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.radioButtonBase64Unicode.Location = new System.Drawing.Point(27, 31);
            this.radioButtonBase64Unicode.Name = "radioButtonBase64Unicode";
            this.radioButtonBase64Unicode.Size = new System.Drawing.Size(116, 19);
            this.radioButtonBase64Unicode.TabIndex = 39;
            this.radioButtonBase64Unicode.TabStop = true;
            this.radioButtonBase64Unicode.Text = "Base64Unicode";
            this.radioButtonBase64Unicode.UseVisualStyleBackColor = true;
            this.radioButtonBase64Unicode.Click += new System.EventHandler(this.radioButtonAlgorithm_Checked);
            // 
            // radioButtonUrlEncode
            // 
            this.radioButtonUrlEncode.AutoSize = true;
            this.radioButtonUrlEncode.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.radioButtonUrlEncode.Location = new System.Drawing.Point(27, 79);
            this.radioButtonUrlEncode.Name = "radioButtonUrlEncode";
            this.radioButtonUrlEncode.Size = new System.Drawing.Size(88, 19);
            this.radioButtonUrlEncode.TabIndex = 40;
            this.radioButtonUrlEncode.TabStop = true;
            this.radioButtonUrlEncode.Text = "UrlEncode";
            this.radioButtonUrlEncode.UseVisualStyleBackColor = true;
            this.radioButtonUrlEncode.Click += new System.EventHandler(this.radioButtonAlgorithm_Checked);
            // 
            // buttonEncode
            // 
            this.buttonEncode.Location = new System.Drawing.Point(22, 162);
            this.buttonEncode.Name = "buttonEncode";
            this.buttonEncode.Size = new System.Drawing.Size(107, 25);
            this.buttonEncode.TabIndex = 38;
            this.buttonEncode.Text = "Encode";
            this.buttonEncode.UseVisualStyleBackColor = true;
            this.buttonEncode.Click += new System.EventHandler(this.buttonEncode_Click);
            // 
            // groupBoxResult
            // 
            this.groupBoxResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxResult.Controls.Add(this.splitContainerEncodeDecode);
            this.groupBoxResult.Location = new System.Drawing.Point(22, 221);
            this.groupBoxResult.Name = "groupBoxResult";
            this.groupBoxResult.Size = new System.Drawing.Size(940, 449);
            this.groupBoxResult.TabIndex = 53;
            this.groupBoxResult.TabStop = false;
            this.groupBoxResult.Text = "Result";
            // 
            // splitContainerEncodeDecode
            // 
            this.splitContainerEncodeDecode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerEncodeDecode.Location = new System.Drawing.Point(3, 19);
            this.splitContainerEncodeDecode.Name = "splitContainerEncodeDecode";
            // 
            // splitContainerEncodeDecode.Panel1
            // 
            this.splitContainerEncodeDecode.Panel1.Controls.Add(this.textBoxDecode);
            this.splitContainerEncodeDecode.Panel1.Controls.Add(this.labelDecode);
            // 
            // splitContainerEncodeDecode.Panel2
            // 
            this.splitContainerEncodeDecode.Panel2.Controls.Add(this.textBoxEncode);
            this.splitContainerEncodeDecode.Panel2.Controls.Add(this.labelEncode);
            this.splitContainerEncodeDecode.Size = new System.Drawing.Size(934, 427);
            this.splitContainerEncodeDecode.SplitterDistance = 458;
            this.splitContainerEncodeDecode.TabIndex = 0;
            // 
            // textBoxDecode
            // 
            this.textBoxDecode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDecode.AutoCompleteBracketsList = new char[] {
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
            this.textBoxDecode.AutoScrollMinSize = new System.Drawing.Size(27, 14);
            this.textBoxDecode.BackBrush = null;
            this.textBoxDecode.CharHeight = 14;
            this.textBoxDecode.CharWidth = 8;
            this.textBoxDecode.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxDecode.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.textBoxDecode.IsReplaceMode = false;
            this.textBoxDecode.Location = new System.Drawing.Point(6, 28);
            this.textBoxDecode.Name = "textBoxDecode";
            this.textBoxDecode.Paddings = new System.Windows.Forms.Padding(0);
            this.textBoxDecode.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.textBoxDecode.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("textBoxDecode.ServiceColors")));
            this.textBoxDecode.Size = new System.Drawing.Size(449, 399);
            this.textBoxDecode.TabIndex = 2;
            this.textBoxDecode.Zoom = 100;
            // 
            // labelDecode
            // 
            this.labelDecode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelDecode.AutoSize = true;
            this.labelDecode.Location = new System.Drawing.Point(3, 3);
            this.labelDecode.Name = "labelDecode";
            this.labelDecode.Size = new System.Drawing.Size(70, 15);
            this.labelDecode.TabIndex = 1;
            this.labelDecode.Text = "[Decoded]";
            // 
            // textBoxEncode
            // 
            this.textBoxEncode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxEncode.AutoCompleteBracketsList = new char[] {
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
            this.textBoxEncode.AutoScrollMinSize = new System.Drawing.Size(27, 14);
            this.textBoxEncode.BackBrush = null;
            this.textBoxEncode.CharHeight = 14;
            this.textBoxEncode.CharWidth = 8;
            this.textBoxEncode.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxEncode.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.textBoxEncode.Font = new System.Drawing.Font("Courier New", 9.75F);
            this.textBoxEncode.IsReplaceMode = false;
            this.textBoxEncode.Location = new System.Drawing.Point(3, 27);
            this.textBoxEncode.Name = "textBoxEncode";
            this.textBoxEncode.Paddings = new System.Windows.Forms.Padding(0);
            this.textBoxEncode.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.textBoxEncode.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("textBoxEncode.ServiceColors")));
            this.textBoxEncode.Size = new System.Drawing.Size(466, 397);
            this.textBoxEncode.TabIndex = 3;
            this.textBoxEncode.Zoom = 100;
            // 
            // labelEncode
            // 
            this.labelEncode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelEncode.AutoSize = true;
            this.labelEncode.Location = new System.Drawing.Point(3, 3);
            this.labelEncode.Name = "labelEncode";
            this.labelEncode.Size = new System.Drawing.Size(70, 15);
            this.labelEncode.TabIndex = 2;
            this.labelEncode.Text = "[Encoded]";
            // 
            // UcEncoderDecoder
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.groupBoxSetAgentKey);
            this.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.Name = "UcEncoderDecoder";
            this.Size = new System.Drawing.Size(981, 690);
            this.groupBoxSetAgentKey.ResumeLayout(false);
            this.groupBoxSelectAlgorithm.ResumeLayout(false);
            this.groupBoxSelectAlgorithm.PerformLayout();
            this.groupBoxResult.ResumeLayout(false);
            this.splitContainerEncodeDecode.Panel1.ResumeLayout(false);
            this.splitContainerEncodeDecode.Panel1.PerformLayout();
            this.splitContainerEncodeDecode.Panel2.ResumeLayout(false);
            this.splitContainerEncodeDecode.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerEncodeDecode)).EndInit();
            this.splitContainerEncodeDecode.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textBoxDecode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxEncode)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBoxSetAgentKey;
        private System.Windows.Forms.Button buttonEncode;
        private System.Windows.Forms.GroupBox groupBoxSelectAlgorithm;
        private System.Windows.Forms.GroupBox groupBoxResult;
        private System.Windows.Forms.TextBox textBoxRijndaelVector;
        private System.Windows.Forms.TextBox textBoxRijndaelKey;
        private System.Windows.Forms.RadioButton radioButtonAesRijndael;
        private System.Windows.Forms.TextBox textBoxKey;
        private System.Windows.Forms.RadioButton radioButtonAes;
        private System.Windows.Forms.RadioButton radioButtonBase64Ascii;
        private System.Windows.Forms.RadioButton radioButtonBase64Unicode;
        private System.Windows.Forms.RadioButton radioButtonUrlEncode;
        private System.Windows.Forms.Button buttonDecode;
        private System.Windows.Forms.SplitContainer splitContainerEncodeDecode;
        private FastColoredTextBoxNS.FastColoredTextBox textBoxDecode;
        private System.Windows.Forms.Label labelDecode;
        private FastColoredTextBoxNS.FastColoredTextBox textBoxEncode;
        private System.Windows.Forms.Label labelEncode;
        private System.Windows.Forms.Label labelKeyVector;
        private System.Windows.Forms.Label label16Key;
    }
}
