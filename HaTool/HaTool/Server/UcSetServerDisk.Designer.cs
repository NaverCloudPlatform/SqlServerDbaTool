namespace HaTool.Server
{
    partial class UcSetServerDisk
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
            this.groupBoxSetServerDisk = new System.Windows.Forms.GroupBox();
            this.groupBoxServerList = new System.Windows.Forms.GroupBox();
            this.dgvServerList = new System.Windows.Forms.DataGridView();
            this.buttonGetBlockStorageInfo = new System.Windows.Forms.Button();
            this.buttonServerListReload = new System.Windows.Forms.Button();
            this.groupBoxBlockStorageInfo = new System.Windows.Forms.GroupBox();
            this.buttonStorageReload = new System.Windows.Forms.Button();
            this.comboBoxType = new System.Windows.Forms.ComboBox();
            this.buttonStorageCreate = new System.Windows.Forms.Button();
            this.buttonBlockStorageDelete = new System.Windows.Forms.Button();
            this.buttonServerGetDiskInfo = new System.Windows.Forms.Button();
            this.dgvStorageList = new System.Windows.Forms.DataGridView();
            this.buttonMountStorage = new System.Windows.Forms.Button();
            this.labelStorageDescription = new System.Windows.Forms.Label();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.labelStorageSize = new System.Windows.Forms.Label();
            this.textBoxStorageSize = new System.Windows.Forms.TextBox();
            this.textBoxStorageName = new System.Windows.Forms.TextBox();
            this.labelStorageName = new System.Windows.Forms.Label();
            this.labeStorageType = new System.Windows.Forms.Label();
            this.groupBoxSetServerDisk.SuspendLayout();
            this.groupBoxServerList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvServerList)).BeginInit();
            this.groupBoxBlockStorageInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStorageList)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxSetServerDisk
            // 
            this.groupBoxSetServerDisk.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxSetServerDisk.Controls.Add(this.groupBoxServerList);
            this.groupBoxSetServerDisk.Controls.Add(this.groupBoxBlockStorageInfo);
            this.groupBoxSetServerDisk.Location = new System.Drawing.Point(3, 3);
            this.groupBoxSetServerDisk.Name = "groupBoxSetServerDisk";
            this.groupBoxSetServerDisk.Size = new System.Drawing.Size(803, 632);
            this.groupBoxSetServerDisk.TabIndex = 1;
            this.groupBoxSetServerDisk.TabStop = false;
            this.groupBoxSetServerDisk.Text = "Server > Set Server Disk";
            // 
            // groupBoxServerList
            // 
            this.groupBoxServerList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxServerList.Controls.Add(this.dgvServerList);
            this.groupBoxServerList.Controls.Add(this.buttonGetBlockStorageInfo);
            this.groupBoxServerList.Controls.Add(this.buttonServerListReload);
            this.groupBoxServerList.Location = new System.Drawing.Point(22, 22);
            this.groupBoxServerList.Name = "groupBoxServerList";
            this.groupBoxServerList.Size = new System.Drawing.Size(772, 298);
            this.groupBoxServerList.TabIndex = 54;
            this.groupBoxServerList.TabStop = false;
            this.groupBoxServerList.Text = "Server List";
            // 
            // dgvServerList
            // 
            this.dgvServerList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvServerList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvServerList.Location = new System.Drawing.Point(11, 22);
            this.dgvServerList.Name = "dgvServerList";
            this.dgvServerList.Size = new System.Drawing.Size(755, 239);
            this.dgvServerList.TabIndex = 1;
            
            // 
            // buttonGetBlockStorageInfo
            // 
            this.buttonGetBlockStorageInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonGetBlockStorageInfo.Location = new System.Drawing.Point(121, 267);
            this.buttonGetBlockStorageInfo.Name = "buttonGetBlockStorageInfo";
            this.buttonGetBlockStorageInfo.Size = new System.Drawing.Size(214, 23);
            this.buttonGetBlockStorageInfo.TabIndex = 53;
            this.buttonGetBlockStorageInfo.Text = "Get BlockStorage Info";
            this.buttonGetBlockStorageInfo.UseVisualStyleBackColor = true;
            this.buttonGetBlockStorageInfo.Click += new System.EventHandler(this.buttonGetBlockStorageInfo_Click);
            // 
            // buttonServerListReload
            // 
            this.buttonServerListReload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonServerListReload.Location = new System.Drawing.Point(11, 267);
            this.buttonServerListReload.Name = "buttonServerListReload";
            this.buttonServerListReload.Size = new System.Drawing.Size(107, 23);
            this.buttonServerListReload.TabIndex = 39;
            this.buttonServerListReload.Text = "Reload";
            this.buttonServerListReload.UseVisualStyleBackColor = true;
            this.buttonServerListReload.Click += new System.EventHandler(this.buttonServerListReload_Click);
            // 
            // groupBoxBlockStorageInfo
            // 
            this.groupBoxBlockStorageInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxBlockStorageInfo.Controls.Add(this.buttonStorageReload);
            this.groupBoxBlockStorageInfo.Controls.Add(this.comboBoxType);
            this.groupBoxBlockStorageInfo.Controls.Add(this.buttonStorageCreate);
            this.groupBoxBlockStorageInfo.Controls.Add(this.buttonBlockStorageDelete);
            this.groupBoxBlockStorageInfo.Controls.Add(this.buttonServerGetDiskInfo);
            this.groupBoxBlockStorageInfo.Controls.Add(this.dgvStorageList);
            this.groupBoxBlockStorageInfo.Controls.Add(this.buttonMountStorage);
            this.groupBoxBlockStorageInfo.Controls.Add(this.labelStorageDescription);
            this.groupBoxBlockStorageInfo.Controls.Add(this.textBoxDescription);
            this.groupBoxBlockStorageInfo.Controls.Add(this.labelStorageSize);
            this.groupBoxBlockStorageInfo.Controls.Add(this.textBoxStorageSize);
            this.groupBoxBlockStorageInfo.Controls.Add(this.textBoxStorageName);
            this.groupBoxBlockStorageInfo.Controls.Add(this.labelStorageName);
            this.groupBoxBlockStorageInfo.Controls.Add(this.labeStorageType);
            this.groupBoxBlockStorageInfo.Location = new System.Drawing.Point(22, 331);
            this.groupBoxBlockStorageInfo.Name = "groupBoxBlockStorageInfo";
            this.groupBoxBlockStorageInfo.Size = new System.Drawing.Size(772, 295);
            this.groupBoxBlockStorageInfo.TabIndex = 0;
            this.groupBoxBlockStorageInfo.TabStop = false;
            this.groupBoxBlockStorageInfo.Text = "Block Storage List";
            // 
            // buttonStorageReload
            // 
            this.buttonStorageReload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonStorageReload.Location = new System.Drawing.Point(11, 262);
            this.buttonStorageReload.Name = "buttonStorageReload";
            this.buttonStorageReload.Size = new System.Drawing.Size(107, 23);
            this.buttonStorageReload.TabIndex = 54;
            this.buttonStorageReload.Text = "Reload";
            this.buttonStorageReload.UseVisualStyleBackColor = true;
            this.buttonStorageReload.Click += new System.EventHandler(this.buttonStorageReload_Click);
            // 
            // comboBoxType
            // 
            this.comboBoxType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBoxType.FormattingEnabled = true;
            this.comboBoxType.Items.AddRange(new object[] {
            "SSD",
            "HDD"});
            this.comboBoxType.Location = new System.Drawing.Point(177, 231);
            this.comboBoxType.Name = "comboBoxType";
            this.comboBoxType.Size = new System.Drawing.Size(158, 23);
            this.comboBoxType.TabIndex = 59;
            // 
            // buttonStorageCreate
            // 
            this.buttonStorageCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonStorageCreate.Location = new System.Drawing.Point(121, 262);
            this.buttonStorageCreate.Name = "buttonStorageCreate";
            this.buttonStorageCreate.Size = new System.Drawing.Size(107, 23);
            this.buttonStorageCreate.TabIndex = 56;
            this.buttonStorageCreate.Text = "Create";
            this.buttonStorageCreate.UseVisualStyleBackColor = true;
            this.buttonStorageCreate.Click += new System.EventHandler(this.buttonStorageCreate_Click);
            // 
            // buttonBlockStorageDelete
            // 
            this.buttonBlockStorageDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonBlockStorageDelete.Location = new System.Drawing.Point(553, 262);
            this.buttonBlockStorageDelete.Name = "buttonBlockStorageDelete";
            this.buttonBlockStorageDelete.Size = new System.Drawing.Size(107, 23);
            this.buttonBlockStorageDelete.TabIndex = 54;
            this.buttonBlockStorageDelete.Text = "Delete";
            this.buttonBlockStorageDelete.UseVisualStyleBackColor = true;
            this.buttonBlockStorageDelete.Click += new System.EventHandler(this.buttonStorageDelete_Click);
            // 
            // buttonServerGetDiskInfo
            // 
            this.buttonServerGetDiskInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonServerGetDiskInfo.Location = new System.Drawing.Point(342, 262);
            this.buttonServerGetDiskInfo.Name = "buttonServerGetDiskInfo";
            this.buttonServerGetDiskInfo.Size = new System.Drawing.Size(208, 23);
            this.buttonServerGetDiskInfo.TabIndex = 58;
            this.buttonServerGetDiskInfo.Text = "Server Get-Disk Info";
            this.buttonServerGetDiskInfo.UseVisualStyleBackColor = true;
            this.buttonServerGetDiskInfo.Click += new System.EventHandler(this.buttonServerGetDiskInfo_Click);
            // 
            // dgvStorageList
            // 
            this.dgvStorageList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvStorageList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStorageList.Location = new System.Drawing.Point(11, 22);
            this.dgvStorageList.Name = "dgvStorageList";
            this.dgvStorageList.Size = new System.Drawing.Size(755, 175);
            this.dgvStorageList.TabIndex = 54;
            

            // 
            // buttonMountStorage
            // 
            this.buttonMountStorage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonMountStorage.Location = new System.Drawing.Point(232, 262);
            this.buttonMountStorage.Name = "buttonMountStorage";
            this.buttonMountStorage.Size = new System.Drawing.Size(107, 23);
            this.buttonMountStorage.TabIndex = 57;
            this.buttonMountStorage.Text = "Mount";
            this.buttonMountStorage.UseVisualStyleBackColor = true;
            this.buttonMountStorage.Click += new System.EventHandler(this.buttonMountStorage_Click);
            // 
            // labelStorageDescription
            // 
            this.labelStorageDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelStorageDescription.AutoSize = true;
            this.labelStorageDescription.Location = new System.Drawing.Point(505, 212);
            this.labelStorageDescription.Name = "labelStorageDescription";
            this.labelStorageDescription.Size = new System.Drawing.Size(84, 15);
            this.labelStorageDescription.TabIndex = 54;
            this.labelStorageDescription.Text = "Description";
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxDescription.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBoxDescription.Location = new System.Drawing.Point(506, 231);
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(160, 23);
            this.textBoxDescription.TabIndex = 53;
            // 
            // labelStorageSize
            // 
            this.labelStorageSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelStorageSize.AutoSize = true;
            this.labelStorageSize.Location = new System.Drawing.Point(339, 212);
            this.labelStorageSize.Name = "labelStorageSize";
            this.labelStorageSize.Size = new System.Drawing.Size(126, 15);
            this.labelStorageSize.TabIndex = 52;
            this.labelStorageSize.Text = "Size 10~2000 [GB]";
            // 
            // textBoxStorageSize
            // 
            this.textBoxStorageSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxStorageSize.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBoxStorageSize.Location = new System.Drawing.Point(341, 231);
            this.textBoxStorageSize.Name = "textBoxStorageSize";
            this.textBoxStorageSize.Size = new System.Drawing.Size(160, 23);
            this.textBoxStorageSize.TabIndex = 51;
            // 
            // textBoxStorageName
            // 
            this.textBoxStorageName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxStorageName.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBoxStorageName.Location = new System.Drawing.Point(12, 231);
            this.textBoxStorageName.Name = "textBoxStorageName";
            this.textBoxStorageName.Size = new System.Drawing.Size(160, 23);
            this.textBoxStorageName.TabIndex = 40;
            // 
            // labelStorageName
            // 
            this.labelStorageName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelStorageName.AutoSize = true;
            this.labelStorageName.Location = new System.Drawing.Point(12, 212);
            this.labelStorageName.Name = "labelStorageName";
            this.labelStorageName.Size = new System.Drawing.Size(91, 15);
            this.labelStorageName.TabIndex = 42;
            this.labelStorageName.Text = "Storage Name";
            // 
            // labeStorageType
            // 
            this.labeStorageType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labeStorageType.AutoSize = true;
            this.labeStorageType.Location = new System.Drawing.Point(177, 212);
            this.labeStorageType.Name = "labeStorageType";
            this.labeStorageType.Size = new System.Drawing.Size(35, 15);
            this.labeStorageType.TabIndex = 49;
            this.labeStorageType.Text = "Type";
            // 
            // UcSetServerDisk
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.groupBoxSetServerDisk);
            this.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.Name = "UcSetServerDisk";
            this.Size = new System.Drawing.Size(813, 642);
            this.Load += new System.EventHandler(this.LoadData);
            this.groupBoxSetServerDisk.ResumeLayout(false);
            this.groupBoxServerList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvServerList)).EndInit();
            this.groupBoxBlockStorageInfo.ResumeLayout(false);
            this.groupBoxBlockStorageInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStorageList)).EndInit();
            this.ResumeLayout(false);



        }

        #endregion
        private System.Windows.Forms.GroupBox groupBoxSetServerDisk;
        private System.Windows.Forms.DataGridView dgvServerList;
        private System.Windows.Forms.Button buttonServerListReload;
        private System.Windows.Forms.Label labelStorageName;
        private System.Windows.Forms.TextBox textBoxStorageName;
        private System.Windows.Forms.Label labeStorageType;
        private System.Windows.Forms.GroupBox groupBoxBlockStorageInfo;
        private System.Windows.Forms.Label labelStorageDescription;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Label labelStorageSize;
        private System.Windows.Forms.TextBox textBoxStorageSize;
        private System.Windows.Forms.Button buttonGetBlockStorageInfo;
        private System.Windows.Forms.Button buttonBlockStorageDelete;
        private System.Windows.Forms.Button buttonServerGetDiskInfo;
        private System.Windows.Forms.Button buttonMountStorage;
        private System.Windows.Forms.Button buttonStorageCreate;
        private System.Windows.Forms.DataGridView dgvStorageList;
        private System.Windows.Forms.ComboBox comboBoxType;
        private System.Windows.Forms.Button buttonStorageReload;
        private System.Windows.Forms.GroupBox groupBoxServerList;
    }
}
