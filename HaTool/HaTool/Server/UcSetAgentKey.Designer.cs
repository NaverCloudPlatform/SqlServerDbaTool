namespace HaTool.Server
{
    partial class UcSetAgentKey
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
            this.groupBoxSetAgentKey = new System.Windows.Forms.GroupBox();
            this.groupBoxAgentKeySettingType = new System.Windows.Forms.GroupBox();
            this.radioButtonModifyAgentKey = new System.Windows.Forms.RadioButton();
            this.labelOld = new System.Windows.Forms.Label();
            this.labelSecretKey = new System.Windows.Forms.Label();
            this.textBoxOldAccessKey = new System.Windows.Forms.TextBox();
            this.textBoxOldSecretKey = new System.Windows.Forms.TextBox();
            this.labelAccessKey = new System.Windows.Forms.Label();
            this.labelNew = new System.Windows.Forms.Label();
            this.textBoxNewSecretKey = new System.Windows.Forms.TextBox();
            this.textBoxNewAccessKey = new System.Windows.Forms.TextBox();
            this.buttonSetAgentKey = new System.Windows.Forms.Button();
            this.radioButtonInitailAgentKeySetting = new System.Windows.Forms.RadioButton();
            this.groupBoxServerList = new System.Windows.Forms.GroupBox();
            this.dgvServerList = new System.Windows.Forms.DataGridView();
            this.buttonServerListReload = new System.Windows.Forms.Button();
            this.groupBoxSetAgentKey.SuspendLayout();
            this.groupBoxAgentKeySettingType.SuspendLayout();
            this.groupBoxServerList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvServerList)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxSetAgentKey
            // 
            this.groupBoxSetAgentKey.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxSetAgentKey.Controls.Add(this.groupBoxAgentKeySettingType);
            this.groupBoxSetAgentKey.Controls.Add(this.groupBoxServerList);
            this.groupBoxSetAgentKey.Location = new System.Drawing.Point(3, 3);
            this.groupBoxSetAgentKey.Name = "groupBoxSetAgentKey";
            this.groupBoxSetAgentKey.Size = new System.Drawing.Size(644, 583);
            this.groupBoxSetAgentKey.TabIndex = 1;
            this.groupBoxSetAgentKey.TabStop = false;
            this.groupBoxSetAgentKey.Text = "Server > Set Agent Key";
            // 
            // groupBox2
            // 
            this.groupBoxAgentKeySettingType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxAgentKeySettingType.Controls.Add(this.radioButtonModifyAgentKey);
            this.groupBoxAgentKeySettingType.Controls.Add(this.buttonSetAgentKey);
            this.groupBoxAgentKeySettingType.Controls.Add(this.labelOld);
            this.groupBoxAgentKeySettingType.Controls.Add(this.radioButtonInitailAgentKeySetting);
            this.groupBoxAgentKeySettingType.Controls.Add(this.labelSecretKey);
            this.groupBoxAgentKeySettingType.Controls.Add(this.textBoxOldAccessKey);
            this.groupBoxAgentKeySettingType.Controls.Add(this.textBoxNewAccessKey);
            this.groupBoxAgentKeySettingType.Controls.Add(this.textBoxOldSecretKey);
            this.groupBoxAgentKeySettingType.Controls.Add(this.textBoxNewSecretKey);
            this.groupBoxAgentKeySettingType.Controls.Add(this.labelAccessKey);
            this.groupBoxAgentKeySettingType.Controls.Add(this.labelNew);
            this.groupBoxAgentKeySettingType.Location = new System.Drawing.Point(22, 22);
            this.groupBoxAgentKeySettingType.Name = "groupBox2";
            this.groupBoxAgentKeySettingType.Size = new System.Drawing.Size(615, 201);
            this.groupBoxAgentKeySettingType.TabIndex = 54;
            this.groupBoxAgentKeySettingType.TabStop = false;
            this.groupBoxAgentKeySettingType.Text = "Agent Key Setting Type";
            // 
            // radioButtonModifyAgentKey
            // 
            this.radioButtonModifyAgentKey.AutoSize = true;
            this.radioButtonModifyAgentKey.Location = new System.Drawing.Point(22, 59);
            this.radioButtonModifyAgentKey.Name = "radioButtonModifyAgentKey";
            this.radioButtonModifyAgentKey.Size = new System.Drawing.Size(137, 19);
            this.radioButtonModifyAgentKey.TabIndex = 46;
            this.radioButtonModifyAgentKey.TabStop = true;
            this.radioButtonModifyAgentKey.Text = "Modify Agent Key";
            this.radioButtonModifyAgentKey.UseVisualStyleBackColor = true;
            this.radioButtonModifyAgentKey.CheckedChanged += new System.EventHandler(this.radioButtonInitailAgentKeySetting_CheckedChanged);
            // 
            // labelOld
            // 
            this.labelOld.AutoSize = true;
            this.labelOld.Location = new System.Drawing.Point(44, 107);
            this.labelOld.Name = "labelOld";
            this.labelOld.Size = new System.Drawing.Size(28, 15);
            this.labelOld.TabIndex = 51;
            this.labelOld.Text = "Old";
            // 
            // labelSecretKey
            // 
            this.labelSecretKey.AutoSize = true;
            this.labelSecretKey.Location = new System.Drawing.Point(279, 85);
            this.labelSecretKey.Name = "labelSecretKey";
            this.labelSecretKey.Size = new System.Drawing.Size(77, 15);
            this.labelSecretKey.TabIndex = 50;
            this.labelSecretKey.Text = "Secret Key";
            // 
            // textBoxOldAccessKey
            // 
            this.textBoxOldAccessKey.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBoxOldAccessKey.Location = new System.Drawing.Point(77, 103);
            this.textBoxOldAccessKey.Name = "textBoxOldAccessKey";
            this.textBoxOldAccessKey.Size = new System.Drawing.Size(193, 23);
            this.textBoxOldAccessKey.TabIndex = 40;
            // 
            // textBoxOldSecretKey
            // 
            this.textBoxOldSecretKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxOldSecretKey.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBoxOldSecretKey.Location = new System.Drawing.Point(276, 103);
            this.textBoxOldSecretKey.Name = "textBoxOldSecretKey";
            this.textBoxOldSecretKey.Size = new System.Drawing.Size(292, 23);
            this.textBoxOldSecretKey.TabIndex = 41;
            // 
            // labelAccessKey
            // 
            this.labelAccessKey.AutoSize = true;
            this.labelAccessKey.Location = new System.Drawing.Point(80, 86);
            this.labelAccessKey.Name = "labelAccessKey";
            this.labelAccessKey.Size = new System.Drawing.Size(77, 15);
            this.labelAccessKey.TabIndex = 42;
            this.labelAccessKey.Text = "Access Key";
            // 
            // labelNew
            // 
            this.labelNew.AutoSize = true;
            this.labelNew.Location = new System.Drawing.Point(44, 134);
            this.labelNew.Name = "labelNew";
            this.labelNew.Size = new System.Drawing.Size(28, 15);
            this.labelNew.TabIndex = 49;
            this.labelNew.Text = "New";
            // 
            // textBoxNewSecretKey
            // 
            this.textBoxNewSecretKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxNewSecretKey.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBoxNewSecretKey.Location = new System.Drawing.Point(276, 130);
            this.textBoxNewSecretKey.Name = "textBoxNewSecretKey";
            this.textBoxNewSecretKey.Size = new System.Drawing.Size(292, 23);
            this.textBoxNewSecretKey.TabIndex = 48;
            // 
            // textBoxNewAccessKey
            // 
            this.textBoxNewAccessKey.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBoxNewAccessKey.Location = new System.Drawing.Point(77, 130);
            this.textBoxNewAccessKey.Name = "textBoxNewAccessKey";
            this.textBoxNewAccessKey.Size = new System.Drawing.Size(193, 23);
            this.textBoxNewAccessKey.TabIndex = 47;
            // 
            // buttonSetAgentKey
            // 
            this.buttonSetAgentKey.Location = new System.Drawing.Point(22, 168);
            this.buttonSetAgentKey.Name = "buttonSetAgentKey";
            this.buttonSetAgentKey.Size = new System.Drawing.Size(107, 23);
            this.buttonSetAgentKey.TabIndex = 38;
            this.buttonSetAgentKey.Text = "Set Agent Key";
            this.buttonSetAgentKey.UseVisualStyleBackColor = true;
            this.buttonSetAgentKey.Click += new System.EventHandler(this.buttonSetAgentKey_Click);
            // 
            // radioButtonInitailAgentKeySetting
            // 
            this.radioButtonInitailAgentKeySetting.AutoSize = true;
            this.radioButtonInitailAgentKeySetting.Location = new System.Drawing.Point(22, 34);
            this.radioButtonInitailAgentKeySetting.Name = "radioButtonInitailAgentKeySetting";
            this.radioButtonInitailAgentKeySetting.Size = new System.Drawing.Size(200, 19);
            this.radioButtonInitailAgentKeySetting.TabIndex = 45;
            this.radioButtonInitailAgentKeySetting.TabStop = true;
            this.radioButtonInitailAgentKeySetting.Text = "Initail Agent Key Setting";
            this.radioButtonInitailAgentKeySetting.UseVisualStyleBackColor = true;
            this.radioButtonInitailAgentKeySetting.CheckedChanged += new System.EventHandler(this.radioButtonInitailAgentKeySetting_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBoxServerList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxServerList.Controls.Add(this.dgvServerList);
            this.groupBoxServerList.Controls.Add(this.buttonServerListReload);
            this.groupBoxServerList.Location = new System.Drawing.Point(22, 228);
            this.groupBoxServerList.Name = "groupBox1";
            this.groupBoxServerList.Size = new System.Drawing.Size(615, 346);
            this.groupBoxServerList.TabIndex = 53;
            this.groupBoxServerList.TabStop = false;
            this.groupBoxServerList.Text = "Server List";
            // 
            // dgvServerList
            // 
            this.dgvServerList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvServerList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvServerList.Location = new System.Drawing.Point(21, 22);
            this.dgvServerList.Name = "dgvServerList";
            this.dgvServerList.Size = new System.Drawing.Size(582, 289);
            this.dgvServerList.TabIndex = 1;
            
            // 
            // buttonServerListReload
            // 
            this.buttonServerListReload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonServerListReload.Location = new System.Drawing.Point(22, 317);
            this.buttonServerListReload.Name = "buttonServerListReload";
            this.buttonServerListReload.Size = new System.Drawing.Size(107, 23);
            this.buttonServerListReload.TabIndex = 39;
            this.buttonServerListReload.Text = "Reload";
            this.buttonServerListReload.UseVisualStyleBackColor = true;
            this.buttonServerListReload.Click += new System.EventHandler(this.buttonServerListReload_Click);
            // 
            // UcSetAgentKey
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.groupBoxSetAgentKey);
            this.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.Name = "UcSetAgentKey";
            this.Size = new System.Drawing.Size(656, 594);
            this.Load += new System.EventHandler(this.LoadData);
            this.groupBoxSetAgentKey.ResumeLayout(false);
            this.groupBoxAgentKeySettingType.ResumeLayout(false);
            this.groupBoxAgentKeySettingType.PerformLayout();
            this.groupBoxServerList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvServerList)).EndInit();
            this.ResumeLayout(false);


        }

        #endregion
        private System.Windows.Forms.GroupBox groupBoxSetAgentKey;
        private System.Windows.Forms.DataGridView dgvServerList;
        private System.Windows.Forms.Button buttonSetAgentKey;
        private System.Windows.Forms.Button buttonServerListReload;
        private System.Windows.Forms.Label labelAccessKey;
        private System.Windows.Forms.TextBox textBoxOldSecretKey;
        private System.Windows.Forms.TextBox textBoxOldAccessKey;
        private System.Windows.Forms.Label labelNew;
        private System.Windows.Forms.TextBox textBoxNewSecretKey;
        private System.Windows.Forms.TextBox textBoxNewAccessKey;
        private System.Windows.Forms.RadioButton radioButtonModifyAgentKey;
        private System.Windows.Forms.RadioButton radioButtonInitailAgentKeySetting;
        private System.Windows.Forms.Label labelOld;
        private System.Windows.Forms.Label labelSecretKey;
        private System.Windows.Forms.GroupBox groupBoxAgentKeySettingType;
        private System.Windows.Forms.GroupBox groupBoxServerList;
    }
}
