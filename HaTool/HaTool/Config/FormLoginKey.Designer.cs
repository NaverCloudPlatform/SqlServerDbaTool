namespace HaTool.Config
{
    partial class FormLoginKey
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLoginKey));
            this.groupBoxOuter = new System.Windows.Forms.GroupBox();
            this.buttonReload = new System.Windows.Forms.Button();
            this.buttonChangeKeySavePath = new System.Windows.Forms.Button();
            this.textBoxKeySavePath = new System.Windows.Forms.TextBox();
            this.labelKeySavePath = new System.Windows.Forms.Label();
            this.comboBoxSelectKey = new System.Windows.Forms.ComboBox();
            this.labelTextBoxRemoteKeyServerUrl = new System.Windows.Forms.Label();
            this.labelTextBoxLocalKey = new System.Windows.Forms.Label();
            this.textBoxNewKeyName = new System.Windows.Forms.TextBox();
            this.radioButtonNewKey = new System.Windows.Forms.RadioButton();
            this.radioButtonOwnKey = new System.Windows.Forms.RadioButton();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.textBoxComment = new System.Windows.Forms.TextBox();
            this.groupBoxOuter.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxOuter
            // 
            this.groupBoxOuter.Controls.Add(this.buttonReload);
            this.groupBoxOuter.Controls.Add(this.buttonChangeKeySavePath);
            this.groupBoxOuter.Controls.Add(this.textBoxKeySavePath);
            this.groupBoxOuter.Controls.Add(this.labelKeySavePath);
            this.groupBoxOuter.Controls.Add(this.comboBoxSelectKey);
            this.groupBoxOuter.Controls.Add(this.labelTextBoxRemoteKeyServerUrl);
            this.groupBoxOuter.Controls.Add(this.labelTextBoxLocalKey);
            this.groupBoxOuter.Controls.Add(this.textBoxNewKeyName);
            this.groupBoxOuter.Controls.Add(this.radioButtonNewKey);
            this.groupBoxOuter.Controls.Add(this.radioButtonOwnKey);
            this.groupBoxOuter.Location = new System.Drawing.Point(7, 7);
            this.groupBoxOuter.Name = "groupBoxOuter";
            this.groupBoxOuter.Size = new System.Drawing.Size(470, 262);
            this.groupBoxOuter.TabIndex = 0;
            this.groupBoxOuter.TabStop = false;
            this.groupBoxOuter.Text = "Config > Login Key Setting";
            // 
            // buttonReload
            // 
            this.buttonReload.Location = new System.Drawing.Point(381, 118);
            this.buttonReload.Name = "buttonReload";
            this.buttonReload.Size = new System.Drawing.Size(70, 23);
            this.buttonReload.TabIndex = 18;
            this.buttonReload.Text = "Reload";
            this.buttonReload.UseVisualStyleBackColor = true;
            this.buttonReload.Click += new System.EventHandler(this.buttonAuthenticationKeyReload_Click);
            // 
            // buttonChangeKeySavePath
            // 
            this.buttonChangeKeySavePath.Location = new System.Drawing.Point(381, 229);
            this.buttonChangeKeySavePath.Name = "buttonChangeKeySavePath";
            this.buttonChangeKeySavePath.Size = new System.Drawing.Size(70, 23);
            this.buttonChangeKeySavePath.TabIndex = 10;
            this.buttonChangeKeySavePath.Text = "Path";
            this.buttonChangeKeySavePath.UseVisualStyleBackColor = true;
            this.buttonChangeKeySavePath.Click += new System.EventHandler(this.buttonChangeKeySavePath_Click);
            // 
            // textBoxKeySavePath
            // 
            this.textBoxKeySavePath.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBoxKeySavePath.Location = new System.Drawing.Point(20, 229);
            this.textBoxKeySavePath.Name = "textBoxKeySavePath";
            this.textBoxKeySavePath.Size = new System.Drawing.Size(355, 23);
            this.textBoxKeySavePath.TabIndex = 17;
            // 
            // labelKeySavePath
            // 
            this.labelKeySavePath.AutoSize = true;
            this.labelKeySavePath.Location = new System.Drawing.Point(22, 208);
            this.labelKeySavePath.Name = "labelKeySavePath";
            this.labelKeySavePath.Size = new System.Drawing.Size(231, 15);
            this.labelKeySavePath.TabIndex = 16;
            this.labelKeySavePath.Text = "New authentication key save path";
            // 
            // comboBoxSelectKey
            // 
            this.comboBoxSelectKey.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxSelectKey.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxSelectKey.FormattingEnabled = true;
            this.comboBoxSelectKey.Location = new System.Drawing.Point(18, 118);
            this.comboBoxSelectKey.Name = "comboBoxSelectKey";
            this.comboBoxSelectKey.Size = new System.Drawing.Size(357, 23);
            this.comboBoxSelectKey.TabIndex = 15;
            // 
            // labelTextBoxRemoteKeyServerUrl
            // 
            this.labelTextBoxRemoteKeyServerUrl.AutoSize = true;
            this.labelTextBoxRemoteKeyServerUrl.Location = new System.Drawing.Point(21, 153);
            this.labelTextBoxRemoteKeyServerUrl.Name = "labelTextBoxRemoteKeyServerUrl";
            this.labelTextBoxRemoteKeyServerUrl.Size = new System.Drawing.Size(196, 15);
            this.labelTextBoxRemoteKeyServerUrl.TabIndex = 13;
            this.labelTextBoxRemoteKeyServerUrl.Text = "New authentication key name";
            // 
            // labelTextBoxLocalKey
            // 
            this.labelTextBoxLocalKey.AutoSize = true;
            this.labelTextBoxLocalKey.Location = new System.Drawing.Point(22, 97);
            this.labelTextBoxLocalKey.Name = "labelTextBoxLocalKey";
            this.labelTextBoxLocalKey.Size = new System.Drawing.Size(182, 15);
            this.labelTextBoxLocalKey.TabIndex = 12;
            this.labelTextBoxLocalKey.Text = "Select authentication key";
            // 
            // textBoxNewKeyName
            // 
            this.textBoxNewKeyName.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBoxNewKeyName.Location = new System.Drawing.Point(18, 174);
            this.textBoxNewKeyName.Name = "textBoxNewKeyName";
            this.textBoxNewKeyName.Size = new System.Drawing.Size(434, 23);
            this.textBoxNewKeyName.TabIndex = 11;
            // 
            // radioButtonNewKey
            // 
            this.radioButtonNewKey.AutoSize = true;
            this.radioButtonNewKey.Location = new System.Drawing.Point(20, 58);
            this.radioButtonNewKey.Name = "radioButtonNewKey";
            this.radioButtonNewKey.Size = new System.Drawing.Size(242, 19);
            this.radioButtonNewKey.TabIndex = 10;
            this.radioButtonNewKey.TabStop = true;
            this.radioButtonNewKey.Text = "Create a new authentication key";
            this.radioButtonNewKey.UseVisualStyleBackColor = true;
            this.radioButtonNewKey.Click += new System.EventHandler(this.LoginKeySettingTypeClicked);
            // 
            // radioButtonOwnKey
            // 
            this.radioButtonOwnKey.AutoSize = true;
            this.radioButtonOwnKey.Location = new System.Drawing.Point(20, 33);
            this.radioButtonOwnKey.Name = "radioButtonOwnKey";
            this.radioButtonOwnKey.Size = new System.Drawing.Size(256, 19);
            this.radioButtonOwnKey.TabIndex = 9;
            this.radioButtonOwnKey.TabStop = true;
            this.radioButtonOwnKey.Text = "Using your own authentication key";
            this.radioButtonOwnKey.UseVisualStyleBackColor = true;
            this.radioButtonOwnKey.Click += new System.EventHandler(this.LoginKeySettingTypeClicked);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(171, 380);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(139, 23);
            this.buttonSave.TabIndex = 8;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(319, 380);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(139, 23);
            this.buttonCancel.TabIndex = 7;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // textBoxComment
            // 
            this.textBoxComment.Location = new System.Drawing.Point(24, 278);
            this.textBoxComment.Multiline = true;
            this.textBoxComment.Name = "textBoxComment";
            this.textBoxComment.Size = new System.Drawing.Size(435, 100);
            this.textBoxComment.TabIndex = 9;
            this.textBoxComment.Text = resources.GetString("textBoxComment.Text");
            // 
            // FormLoginKey
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(484, 412);
            this.Controls.Add(this.textBoxComment);
            this.Controls.Add(this.groupBoxOuter);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonCancel);
            this.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormLoginKey";
            this.Text = "Login Key";
            this.Load += new System.EventHandler(this.LoadData);
            this.groupBoxOuter.ResumeLayout(false);
            this.groupBoxOuter.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxOuter;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TextBox textBoxComment;
        private System.Windows.Forms.Label labelTextBoxRemoteKeyServerUrl;
        private System.Windows.Forms.Label labelTextBoxLocalKey;
        private System.Windows.Forms.TextBox textBoxNewKeyName;
        private System.Windows.Forms.RadioButton radioButtonNewKey;
        private System.Windows.Forms.RadioButton radioButtonOwnKey;
        private System.Windows.Forms.ComboBox comboBoxSelectKey;
        private System.Windows.Forms.Button buttonChangeKeySavePath;
        private System.Windows.Forms.TextBox textBoxKeySavePath;
        private System.Windows.Forms.Label labelKeySavePath;
        private System.Windows.Forms.Button buttonReload;
    }
}