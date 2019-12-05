namespace HaTool.Config
{
    partial class FormEncryptionKey
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEncryptionKey));
            this.groupBoxOuter = new System.Windows.Forms.GroupBox();
            this.labelCiphertext = new System.Windows.Forms.Label();
            this.textBoxCiphertext = new System.Windows.Forms.TextBox();
            this.labelKeyTag = new System.Windows.Forms.Label();
            this.textBoxKeyTag = new System.Windows.Forms.TextBox();
            this.radioButtonNcpKms = new System.Windows.Forms.RadioButton();
            this.labelSecretKey = new System.Windows.Forms.Label();
            this.textBoxSecretKey = new System.Windows.Forms.TextBox();
            this.labelAccessKey = new System.Windows.Forms.Label();
            this.textBoxAccessKey = new System.Windows.Forms.TextBox();
            this.labelTextBoxLocalKey = new System.Windows.Forms.Label();
            this.textBoxKey = new System.Windows.Forms.TextBox();
            this.radioButtonLocalKey = new System.Windows.Forms.RadioButton();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.textBoxComment = new System.Windows.Forms.TextBox();
            this.buttonKeyTest = new System.Windows.Forms.Button();
            this.buttonGetCiphertext = new System.Windows.Forms.Button();
            this.groupBoxOuter.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxOuter
            // 
            this.groupBoxOuter.Controls.Add(this.labelCiphertext);
            this.groupBoxOuter.Controls.Add(this.textBoxCiphertext);
            this.groupBoxOuter.Controls.Add(this.labelKeyTag);
            this.groupBoxOuter.Controls.Add(this.textBoxKeyTag);
            this.groupBoxOuter.Controls.Add(this.radioButtonNcpKms);
            this.groupBoxOuter.Controls.Add(this.labelSecretKey);
            this.groupBoxOuter.Controls.Add(this.textBoxSecretKey);
            this.groupBoxOuter.Controls.Add(this.labelAccessKey);
            this.groupBoxOuter.Controls.Add(this.textBoxAccessKey);
            this.groupBoxOuter.Controls.Add(this.labelTextBoxLocalKey);
            this.groupBoxOuter.Controls.Add(this.textBoxKey);
            this.groupBoxOuter.Controls.Add(this.radioButtonLocalKey);
            this.groupBoxOuter.Location = new System.Drawing.Point(8, 7);
            this.groupBoxOuter.Name = "groupBoxOuter";
            this.groupBoxOuter.Size = new System.Drawing.Size(518, 353);
            this.groupBoxOuter.TabIndex = 0;
            this.groupBoxOuter.TabStop = false;
            this.groupBoxOuter.Text = "Config > Encryption Key Setting";
            // 
            // labelCiphertext
            // 
            this.labelCiphertext.AutoSize = true;
            this.labelCiphertext.Location = new System.Drawing.Point(20, 187);
            this.labelCiphertext.Name = "labelCiphertext";
            this.labelCiphertext.Size = new System.Drawing.Size(77, 15);
            this.labelCiphertext.TabIndex = 16;
            this.labelCiphertext.Text = "Ciphertext";
            // 
            // textBoxCiphertext
            // 
            this.textBoxCiphertext.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBoxCiphertext.Location = new System.Drawing.Point(17, 208);
            this.textBoxCiphertext.Name = "textBoxCiphertext";
            this.textBoxCiphertext.Size = new System.Drawing.Size(482, 23);
            this.textBoxCiphertext.TabIndex = 15;
            // 
            // labelKeyTag
            // 
            this.labelKeyTag.AutoSize = true;
            this.labelKeyTag.Location = new System.Drawing.Point(20, 133);
            this.labelKeyTag.Name = "labelKeyTag";
            this.labelKeyTag.Size = new System.Drawing.Size(56, 15);
            this.labelKeyTag.TabIndex = 14;
            this.labelKeyTag.Text = "Key Tag";
            // 
            // textBoxKeyTag
            // 
            this.textBoxKeyTag.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBoxKeyTag.Location = new System.Drawing.Point(17, 154);
            this.textBoxKeyTag.Name = "textBoxKeyTag";
            this.textBoxKeyTag.Size = new System.Drawing.Size(482, 23);
            this.textBoxKeyTag.TabIndex = 13;
            // 
            // radioButtonNcpKms
            // 
            this.radioButtonNcpKms.AutoSize = true;
            this.radioButtonNcpKms.Location = new System.Drawing.Point(278, 39);
            this.radioButtonNcpKms.Name = "radioButtonNcpKms";
            this.radioButtonNcpKms.Size = new System.Drawing.Size(74, 19);
            this.radioButtonNcpKms.TabIndex = 12;
            this.radioButtonNcpKms.TabStop = true;
            this.radioButtonNcpKms.Text = "Ncp KMS";
            this.radioButtonNcpKms.UseVisualStyleBackColor = true;
            this.radioButtonNcpKms.Click += new System.EventHandler(this.KeyServerTypeClicked);
            // 
            // labelSecretKey
            // 
            this.labelSecretKey.AutoSize = true;
            this.labelSecretKey.Location = new System.Drawing.Point(20, 295);
            this.labelSecretKey.Name = "labelSecretKey";
            this.labelSecretKey.Size = new System.Drawing.Size(77, 15);
            this.labelSecretKey.TabIndex = 11;
            this.labelSecretKey.Text = "Secret Key";
            // 
            // textBoxSecretKey
            // 
            this.textBoxSecretKey.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBoxSecretKey.Location = new System.Drawing.Point(17, 316);
            this.textBoxSecretKey.Name = "textBoxSecretKey";
            this.textBoxSecretKey.PasswordChar = '*';
            this.textBoxSecretKey.Size = new System.Drawing.Size(482, 23);
            this.textBoxSecretKey.TabIndex = 10;
            // 
            // labelAccessKey
            // 
            this.labelAccessKey.AutoSize = true;
            this.labelAccessKey.Location = new System.Drawing.Point(20, 241);
            this.labelAccessKey.Name = "labelAccessKey";
            this.labelAccessKey.Size = new System.Drawing.Size(77, 15);
            this.labelAccessKey.TabIndex = 9;
            this.labelAccessKey.Text = "Access Key";
            // 
            // textBoxAccessKey
            // 
            this.textBoxAccessKey.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBoxAccessKey.Location = new System.Drawing.Point(17, 262);
            this.textBoxAccessKey.Name = "textBoxAccessKey";
            this.textBoxAccessKey.Size = new System.Drawing.Size(482, 23);
            this.textBoxAccessKey.TabIndex = 8;
            // 
            // labelTextBoxLocalKey
            // 
            this.labelTextBoxLocalKey.AutoSize = true;
            this.labelTextBoxLocalKey.Location = new System.Drawing.Point(21, 79);
            this.labelTextBoxLocalKey.Name = "labelTextBoxLocalKey";
            this.labelTextBoxLocalKey.Size = new System.Drawing.Size(28, 15);
            this.labelTextBoxLocalKey.TabIndex = 4;
            this.labelTextBoxLocalKey.Text = "Key";
            // 
            // textBoxKey
            // 
            this.textBoxKey.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBoxKey.Location = new System.Drawing.Point(17, 100);
            this.textBoxKey.Name = "textBoxKey";
            this.textBoxKey.Size = new System.Drawing.Size(482, 23);
            this.textBoxKey.TabIndex = 2;
            // 
            // radioButtonLocalKey
            // 
            this.radioButtonLocalKey.AutoSize = true;
            this.radioButtonLocalKey.Location = new System.Drawing.Point(161, 39);
            this.radioButtonLocalKey.Name = "radioButtonLocalKey";
            this.radioButtonLocalKey.Size = new System.Drawing.Size(88, 19);
            this.radioButtonLocalKey.TabIndex = 0;
            this.radioButtonLocalKey.TabStop = true;
            this.radioButtonLocalKey.Text = "Local Key";
            this.radioButtonLocalKey.UseVisualStyleBackColor = true;
            this.radioButtonLocalKey.Click += new System.EventHandler(this.KeyServerTypeClicked);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(284, 456);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(107, 23);
            this.buttonSave.TabIndex = 8;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(398, 456);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(107, 23);
            this.buttonCancel.TabIndex = 7;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // textBoxComment
            // 
            this.textBoxComment.Location = new System.Drawing.Point(32, 372);
            this.textBoxComment.Multiline = true;
            this.textBoxComment.Name = "textBoxComment";
            this.textBoxComment.Size = new System.Drawing.Size(482, 73);
            this.textBoxComment.TabIndex = 10;
            this.textBoxComment.Text = resources.GetString("textBoxComment.Text");
            // 
            // buttonKeyTest
            // 
            this.buttonKeyTest.Location = new System.Drawing.Point(42, 456);
            this.buttonKeyTest.Name = "buttonKeyTest";
            this.buttonKeyTest.Size = new System.Drawing.Size(107, 23);
            this.buttonKeyTest.TabIndex = 11;
            this.buttonKeyTest.Text = "Key Test";
            this.buttonKeyTest.UseVisualStyleBackColor = true;
            this.buttonKeyTest.Click += new System.EventHandler(this.buttonKeyTest_Click);
            // 
            // buttonGetCiphertext
            // 
            this.buttonGetCiphertext.Location = new System.Drawing.Point(155, 456);
            this.buttonGetCiphertext.Name = "buttonGetCiphertext";
            this.buttonGetCiphertext.Size = new System.Drawing.Size(124, 23);
            this.buttonGetCiphertext.TabIndex = 12;
            this.buttonGetCiphertext.Text = "Get Ciphertext";
            this.buttonGetCiphertext.UseVisualStyleBackColor = true;
            this.buttonGetCiphertext.Click += new System.EventHandler(this.buttonGetCiphertext_Click);
            // 
            // FormEncryptionKey
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(537, 493);
            this.Controls.Add(this.buttonGetCiphertext);
            this.Controls.Add(this.buttonKeyTest);
            this.Controls.Add(this.textBoxComment);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.groupBoxOuter);
            this.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormEncryptionKey";
            this.Text = "Encryption Key";
            this.Load += new System.EventHandler(this.LoadData);
            this.Shown += new System.EventHandler(this.LoadData);
            this.groupBoxOuter.ResumeLayout(false);
            this.groupBoxOuter.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxOuter;
        private System.Windows.Forms.TextBox textBoxKey;
        private System.Windows.Forms.RadioButton radioButtonLocalKey;
        private System.Windows.Forms.RadioButton radioButtonNcpKms;
        private System.Windows.Forms.Label labelTextBoxLocalKey;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TextBox textBoxComment;
        private System.Windows.Forms.Label labelSecretKey;
        private System.Windows.Forms.TextBox textBoxSecretKey;
        private System.Windows.Forms.Label labelAccessKey;
        private System.Windows.Forms.TextBox textBoxAccessKey;
        private System.Windows.Forms.Button buttonKeyTest;
        private System.Windows.Forms.Label labelCiphertext;
        private System.Windows.Forms.TextBox textBoxCiphertext;
        private System.Windows.Forms.Label labelKeyTag;
        private System.Windows.Forms.TextBox textBoxKeyTag;
        private System.Windows.Forms.Button buttonGetCiphertext;
    }
}