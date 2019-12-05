namespace HaTool.Server
{
    partial class UcCreateServer
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
            this.groupBoxCreateServer = new System.Windows.Forms.GroupBox();
            this.groupBox4CreateServer = new System.Windows.Forms.GroupBox();
            this.labelServerName = new System.Windows.Forms.Label();
            this.buttonServerNameCheck = new System.Windows.Forms.Button();
            this.buttonCommandPreview = new System.Windows.Forms.Button();
            this.textBoxCreateServerCommnet = new System.Windows.Forms.TextBox();
            this.buttonDbDelete = new System.Windows.Forms.Button();
            this.buttonCreateServer = new System.Windows.Forms.Button();
            this.buttonDbSave = new System.Windows.Forms.Button();
            this.groupBoxAccessControlGroup = new System.Windows.Forms.GroupBox();
            this.comboBoxACG4 = new System.Windows.Forms.ComboBox();
            this.comboBoxACG1 = new System.Windows.Forms.ComboBox();
            this.comboBoxACG2 = new System.Windows.Forms.ComboBox();
            this.comboBoxACG3 = new System.Windows.Forms.ComboBox();
            this.comboBoxACG5 = new System.Windows.Forms.ComboBox();
            this.labelConfigurationNo = new System.Windows.Forms.Label();
            this.textBoxACLCommnet = new System.Windows.Forms.TextBox();
            this.groupBoxServerImageAndSpec = new System.Windows.Forms.GroupBox();
            this.textBoxCommnet2 = new System.Windows.Forms.TextBox();
            this.labelServerImage = new System.Windows.Forms.Label();
            this.comboBoxServerImage = new System.Windows.Forms.ComboBox();
            this.labelServer = new System.Windows.Forms.Label();
            this.comboBoxServer = new System.Windows.Forms.ComboBox();
            this.groupBoxServerLocation = new System.Windows.Forms.GroupBox();
            this.comboBoxRegion = new System.Windows.Forms.ComboBox();
            this.labelRegion = new System.Windows.Forms.Label();
            this.comboBoxZone = new System.Windows.Forms.ComboBox();
            this.labelZone = new System.Windows.Forms.Label();
            this.textBoxServerName = new System.Windows.Forms.TextBox();
            this.groupBoxCreateServer.SuspendLayout();
            this.groupBox4CreateServer.SuspendLayout();
            this.groupBoxAccessControlGroup.SuspendLayout();
            this.groupBoxServerImageAndSpec.SuspendLayout();
            this.groupBoxServerLocation.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxCreateServer
            // 
            this.groupBoxCreateServer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxCreateServer.Controls.Add(this.groupBox4CreateServer);
            this.groupBoxCreateServer.Controls.Add(this.groupBoxAccessControlGroup);
            this.groupBoxCreateServer.Controls.Add(this.groupBoxServerImageAndSpec);
            this.groupBoxCreateServer.Controls.Add(this.groupBoxServerLocation);
            this.groupBoxCreateServer.Location = new System.Drawing.Point(3, 3);
            this.groupBoxCreateServer.Name = "groupBoxCreateServer";
            this.groupBoxCreateServer.Size = new System.Drawing.Size(894, 694);
            this.groupBoxCreateServer.TabIndex = 0;
            this.groupBoxCreateServer.TabStop = false;
            this.groupBoxCreateServer.Text = "Server > CreateServer";
            // 
            // groupBox4CreateServer
            // 
            this.groupBox4CreateServer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4CreateServer.Controls.Add(this.labelServerName);
            this.groupBox4CreateServer.Controls.Add(this.buttonServerNameCheck);
            this.groupBox4CreateServer.Controls.Add(this.buttonCommandPreview);
            this.groupBox4CreateServer.Controls.Add(this.textBoxCreateServerCommnet);
            this.groupBox4CreateServer.Controls.Add(this.textBoxServerName);
            this.groupBox4CreateServer.Controls.Add(this.buttonDbDelete);
            this.groupBox4CreateServer.Controls.Add(this.buttonCreateServer);
            this.groupBox4CreateServer.Controls.Add(this.buttonDbSave);
            this.groupBox4CreateServer.Location = new System.Drawing.Point(22, 440);
            this.groupBox4CreateServer.Name = "groupBox4CreateServer";
            this.groupBox4CreateServer.Size = new System.Drawing.Size(849, 248);
            this.groupBox4CreateServer.TabIndex = 163;
            this.groupBox4CreateServer.TabStop = false;
            this.groupBox4CreateServer.Text = "Create Server";
            // 
            // labelServerName
            // 
            this.labelServerName.AutoSize = true;
            this.labelServerName.Location = new System.Drawing.Point(19, 33);
            this.labelServerName.Name = "labelServerName";
            this.labelServerName.Size = new System.Drawing.Size(161, 15);
            this.labelServerName.TabIndex = 24;
            this.labelServerName.Text = "Server Name (hostname)";
            // 
            // buttonServerNameCheck
            // 
            this.buttonServerNameCheck.Location = new System.Drawing.Point(281, 55);
            this.buttonServerNameCheck.Name = "buttonServerNameCheck";
            this.buttonServerNameCheck.Size = new System.Drawing.Size(84, 23);
            this.buttonServerNameCheck.TabIndex = 26;
            this.buttonServerNameCheck.Text = "Check";
            this.buttonServerNameCheck.UseVisualStyleBackColor = true;
            this.buttonServerNameCheck.Click += new System.EventHandler(this.buttonServerNameCheck_Click);
            // 
            // buttonCommandPreview
            // 
            this.buttonCommandPreview.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.buttonCommandPreview.Location = new System.Drawing.Point(16, 113);
            this.buttonCommandPreview.Name = "buttonCommandPreview";
            this.buttonCommandPreview.Size = new System.Drawing.Size(259, 23);
            this.buttonCommandPreview.TabIndex = 37;
            this.buttonCommandPreview.Text = "Command Preview";
            this.buttonCommandPreview.UseVisualStyleBackColor = true;
            this.buttonCommandPreview.Click += new System.EventHandler(this.buttonCommandPreview_Click);
            // 
            // textBoxCreateServerCommnet
            // 
            this.textBoxCreateServerCommnet.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxCreateServerCommnet.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.textBoxCreateServerCommnet.Location = new System.Drawing.Point(15, 154);
            this.textBoxCreateServerCommnet.Multiline = true;
            this.textBoxCreateServerCommnet.Name = "textBoxCreateServerCommnet";
            this.textBoxCreateServerCommnet.Size = new System.Drawing.Size(471, 69);
            this.textBoxCreateServerCommnet.TabIndex = 44;
            this.textBoxCreateServerCommnet.Text = "It takes about 10 minutes to create the server. Ask them to create two servers to" +
    " be redundant and start the next step after a cup of coffee.";
            // 
            // buttonDbDelete
            // 
            this.buttonDbDelete.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.buttonDbDelete.Location = new System.Drawing.Point(372, 113);
            this.buttonDbDelete.Name = "buttonDbDelete";
            this.buttonDbDelete.Size = new System.Drawing.Size(84, 23);
            this.buttonDbDelete.TabIndex = 46;
            this.buttonDbDelete.Text = "db delete";
            this.buttonDbDelete.UseVisualStyleBackColor = true;
            this.buttonDbDelete.Click += new System.EventHandler(this.buttonDbDelete_Click);
            // 
            // buttonCreateServer
            // 
            this.buttonCreateServer.Location = new System.Drawing.Point(16, 84);
            this.buttonCreateServer.Name = "buttonCreateServer";
            this.buttonCreateServer.Size = new System.Drawing.Size(259, 23);
            this.buttonCreateServer.TabIndex = 15;
            this.buttonCreateServer.Text = "Create Server";
            this.buttonCreateServer.UseVisualStyleBackColor = true;
            this.buttonCreateServer.Click += new System.EventHandler(this.buttonCreateServer_Click);
            // 
            // buttonDbSave
            // 
            this.buttonDbSave.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.buttonDbSave.Location = new System.Drawing.Point(281, 113);
            this.buttonDbSave.Name = "buttonDbSave";
            this.buttonDbSave.Size = new System.Drawing.Size(85, 23);
            this.buttonDbSave.TabIndex = 45;
            this.buttonDbSave.Text = "db save";
            this.buttonDbSave.UseVisualStyleBackColor = true;
            this.buttonDbSave.Click += new System.EventHandler(this.buttonDbSave_Click);
            // 
            // groupBoxAccessControlGroup
            // 
            this.groupBoxAccessControlGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxAccessControlGroup.Controls.Add(this.comboBoxACG4);
            this.groupBoxAccessControlGroup.Controls.Add(this.comboBoxACG1);
            this.groupBoxAccessControlGroup.Controls.Add(this.comboBoxACG2);
            this.groupBoxAccessControlGroup.Controls.Add(this.comboBoxACG3);
            this.groupBoxAccessControlGroup.Controls.Add(this.comboBoxACG5);
            this.groupBoxAccessControlGroup.Controls.Add(this.labelConfigurationNo);
            this.groupBoxAccessControlGroup.Controls.Add(this.textBoxACLCommnet);
            this.groupBoxAccessControlGroup.Location = new System.Drawing.Point(22, 266);
            this.groupBoxAccessControlGroup.Name = "groupBoxAccessControlGroup";
            this.groupBoxAccessControlGroup.Size = new System.Drawing.Size(849, 168);
            this.groupBoxAccessControlGroup.TabIndex = 162;
            this.groupBoxAccessControlGroup.TabStop = false;
            this.groupBoxAccessControlGroup.Text = "Access Control Group";
            // 
            // comboBoxACG4
            // 
            this.comboBoxACG4.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxACG4.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxACG4.FormattingEnabled = true;
            this.comboBoxACG4.Location = new System.Drawing.Point(16, 78);
            this.comboBoxACG4.Name = "comboBoxACG4";
            this.comboBoxACG4.Size = new System.Drawing.Size(259, 23);
            this.comboBoxACG4.TabIndex = 41;
            // 
            // comboBoxACG1
            // 
            this.comboBoxACG1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxACG1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxACG1.FormattingEnabled = true;
            this.comboBoxACG1.Location = new System.Drawing.Point(16, 49);
            this.comboBoxACG1.Name = "comboBoxACG1";
            this.comboBoxACG1.Size = new System.Drawing.Size(259, 23);
            this.comboBoxACG1.TabIndex = 38;
            // 
            // comboBoxACG2
            // 
            this.comboBoxACG2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxACG2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxACG2.FormattingEnabled = true;
            this.comboBoxACG2.Location = new System.Drawing.Point(282, 49);
            this.comboBoxACG2.Name = "comboBoxACG2";
            this.comboBoxACG2.Size = new System.Drawing.Size(259, 23);
            this.comboBoxACG2.TabIndex = 39;
            // 
            // comboBoxACG3
            // 
            this.comboBoxACG3.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxACG3.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxACG3.FormattingEnabled = true;
            this.comboBoxACG3.Location = new System.Drawing.Point(546, 49);
            this.comboBoxACG3.Name = "comboBoxACG3";
            this.comboBoxACG3.Size = new System.Drawing.Size(259, 23);
            this.comboBoxACG3.TabIndex = 40;
            // 
            // comboBoxACG5
            // 
            this.comboBoxACG5.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxACG5.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxACG5.FormattingEnabled = true;
            this.comboBoxACG5.Location = new System.Drawing.Point(281, 78);
            this.comboBoxACG5.Name = "comboBoxACG5";
            this.comboBoxACG5.Size = new System.Drawing.Size(259, 23);
            this.comboBoxACG5.TabIndex = 42;
            // 
            // labelConfigurationNo
            // 
            this.labelConfigurationNo.AutoSize = true;
            this.labelConfigurationNo.Location = new System.Drawing.Point(19, 30);
            this.labelConfigurationNo.Name = "labelConfigurationNo";
            this.labelConfigurationNo.Size = new System.Drawing.Size(175, 15);
            this.labelConfigurationNo.TabIndex = 31;
            this.labelConfigurationNo.Text = "Configuration No (1...5)";
            // 
            // textBoxACLCommnet
            // 
            this.textBoxACLCommnet.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxACLCommnet.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.textBoxACLCommnet.Location = new System.Drawing.Point(16, 107);
            this.textBoxACLCommnet.Multiline = true;
            this.textBoxACLCommnet.Name = "textBoxACLCommnet";
            this.textBoxACLCommnet.Size = new System.Drawing.Size(470, 45);
            this.textBoxACLCommnet.TabIndex = 43;
            this.textBoxACLCommnet.Text = "You can first create an ACL group in the console and select up to five groups. Th" +
    "e ports used by the Agent (9090), MSTSC (3389), and sql (1433) services must be " +
    "included.";
            // 
            // groupBoxServerImageAndSpec
            // 
            this.groupBoxServerImageAndSpec.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxServerImageAndSpec.Controls.Add(this.textBoxCommnet2);
            this.groupBoxServerImageAndSpec.Controls.Add(this.labelServerImage);
            this.groupBoxServerImageAndSpec.Controls.Add(this.comboBoxServerImage);
            this.groupBoxServerImageAndSpec.Controls.Add(this.labelServer);
            this.groupBoxServerImageAndSpec.Controls.Add(this.comboBoxServer);
            this.groupBoxServerImageAndSpec.Location = new System.Drawing.Point(22, 110);
            this.groupBoxServerImageAndSpec.Name = "groupBoxServerImageAndSpec";
            this.groupBoxServerImageAndSpec.Size = new System.Drawing.Size(849, 150);
            this.groupBoxServerImageAndSpec.TabIndex = 161;
            this.groupBoxServerImageAndSpec.TabStop = false;
            this.groupBoxServerImageAndSpec.Text = "Server Image and Spec";
            // 
            // textBoxCommnet2
            // 
            this.textBoxCommnet2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxCommnet2.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.textBoxCommnet2.Location = new System.Drawing.Point(271, 75);
            this.textBoxCommnet2.Multiline = true;
            this.textBoxCommnet2.Name = "textBoxCommnet2";
            this.textBoxCommnet2.Size = new System.Drawing.Size(285, 25);
            this.textBoxCommnet2.TabIndex = 23;
            this.textBoxCommnet2.Text = "mssql(2016std)-win-2012-64-R2-en only";
            // 
            // labelServerImage
            // 
            this.labelServerImage.AutoSize = true;
            this.labelServerImage.Location = new System.Drawing.Point(19, 28);
            this.labelServerImage.Name = "labelServerImage";
            this.labelServerImage.Size = new System.Drawing.Size(91, 15);
            this.labelServerImage.TabIndex = 2;
            this.labelServerImage.Text = "Server Image";
            // 
            // comboBoxServerImage
            // 
            this.comboBoxServerImage.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxServerImage.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxServerImage.FormattingEnabled = true;
            this.comboBoxServerImage.Location = new System.Drawing.Point(16, 46);
            this.comboBoxServerImage.Name = "comboBoxServerImage";
            this.comboBoxServerImage.Size = new System.Drawing.Size(524, 23);
            this.comboBoxServerImage.TabIndex = 20;
            // 
            // labelServer
            // 
            this.labelServer.AutoSize = true;
            this.labelServer.Location = new System.Drawing.Point(19, 92);
            this.labelServer.Name = "labelServer";
            this.labelServer.Size = new System.Drawing.Size(98, 15);
            this.labelServer.TabIndex = 22;
            this.labelServer.Text = "Server (Spec)";
            // 
            // comboBoxServer
            // 
            this.comboBoxServer.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxServer.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxServer.FormattingEnabled = true;
            this.comboBoxServer.Location = new System.Drawing.Point(16, 110);
            this.comboBoxServer.Name = "comboBoxServer";
            this.comboBoxServer.Size = new System.Drawing.Size(789, 23);
            this.comboBoxServer.TabIndex = 21;
            // 
            // groupBoxServerLocation
            // 
            this.groupBoxServerLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxServerLocation.Controls.Add(this.comboBoxRegion);
            this.groupBoxServerLocation.Controls.Add(this.labelRegion);
            this.groupBoxServerLocation.Controls.Add(this.comboBoxZone);
            this.groupBoxServerLocation.Controls.Add(this.labelZone);
            this.groupBoxServerLocation.Location = new System.Drawing.Point(22, 22);
            this.groupBoxServerLocation.Name = "groupBoxServerLocation";
            this.groupBoxServerLocation.Size = new System.Drawing.Size(849, 82);
            this.groupBoxServerLocation.TabIndex = 160;
            this.groupBoxServerLocation.TabStop = false;
            this.groupBoxServerLocation.Text = "Server Locaion";
            // 
            // comboBoxRegion
            // 
            this.comboBoxRegion.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxRegion.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxRegion.FormattingEnabled = true;
            this.comboBoxRegion.Location = new System.Drawing.Point(16, 46);
            this.comboBoxRegion.Name = "comboBoxRegion";
            this.comboBoxRegion.Size = new System.Drawing.Size(259, 23);
            this.comboBoxRegion.TabIndex = 17;
            this.comboBoxRegion.SelectedIndexChanged += new System.EventHandler(this.ComboBoxRegionChanged);
            // 
            // labelRegion
            // 
            this.labelRegion.AutoSize = true;
            this.labelRegion.Location = new System.Drawing.Point(19, 28);
            this.labelRegion.Name = "labelRegion";
            this.labelRegion.Size = new System.Drawing.Size(49, 15);
            this.labelRegion.TabIndex = 0;
            this.labelRegion.Text = "Region";
            // 
            // comboBoxZone
            // 
            this.comboBoxZone.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxZone.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxZone.FormattingEnabled = true;
            this.comboBoxZone.Location = new System.Drawing.Point(282, 46);
            this.comboBoxZone.Name = "comboBoxZone";
            this.comboBoxZone.Size = new System.Drawing.Size(259, 23);
            this.comboBoxZone.TabIndex = 18;
            // 
            // labelZone
            // 
            this.labelZone.AutoSize = true;
            this.labelZone.Location = new System.Drawing.Point(285, 28);
            this.labelZone.Name = "labelZone";
            this.labelZone.Size = new System.Drawing.Size(35, 15);
            this.labelZone.TabIndex = 19;
            this.labelZone.Text = "Zone";
            // 
            // textBoxServerName
            // 
            this.textBoxServerName.Location = new System.Drawing.Point(16, 56);
            this.textBoxServerName.Name = "textBoxServerName";
            this.textBoxServerName.Size = new System.Drawing.Size(259, 23);
            this.textBoxServerName.TabIndex = 25;
            // 
            // UcCreateServer
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.groupBoxCreateServer);
            this.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.Name = "UcCreateServer";
            this.Size = new System.Drawing.Size(900, 700);
            this.Load += new System.EventHandler(this.LoadData);
            this.groupBoxCreateServer.ResumeLayout(false);
            this.groupBox4CreateServer.ResumeLayout(false);
            this.groupBox4CreateServer.PerformLayout();
            this.groupBoxAccessControlGroup.ResumeLayout(false);
            this.groupBoxAccessControlGroup.PerformLayout();
            this.groupBoxServerImageAndSpec.ResumeLayout(false);
            this.groupBoxServerImageAndSpec.PerformLayout();
            this.groupBoxServerLocation.ResumeLayout(false);
            this.groupBoxServerLocation.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxCreateServer;
        private System.Windows.Forms.Label labelServerImage;
        private System.Windows.Forms.Label labelRegion;
        private System.Windows.Forms.Button buttonCreateServer;
        private System.Windows.Forms.Label labelZone;
        private System.Windows.Forms.ComboBox comboBoxZone;
        private System.Windows.Forms.ComboBox comboBoxRegion;
        private System.Windows.Forms.TextBox textBoxCommnet2;
        private System.Windows.Forms.Label labelServer;
        private System.Windows.Forms.ComboBox comboBoxServer;
        private System.Windows.Forms.ComboBox comboBoxServerImage;
        private System.Windows.Forms.Label labelConfigurationNo;
        private System.Windows.Forms.Button buttonServerNameCheck;
        private System.Windows.Forms.TextBox textBoxServerName;
        private System.Windows.Forms.Label labelServerName;
        private System.Windows.Forms.Button buttonCommandPreview;
        private System.Windows.Forms.ComboBox comboBoxACG5;
        private System.Windows.Forms.ComboBox comboBoxACG4;
        private System.Windows.Forms.ComboBox comboBoxACG3;
        private System.Windows.Forms.ComboBox comboBoxACG2;
        private System.Windows.Forms.ComboBox comboBoxACG1;
        private System.Windows.Forms.TextBox textBoxACLCommnet;
        private System.Windows.Forms.TextBox textBoxCreateServerCommnet;
        private System.Windows.Forms.Button buttonDbDelete;
        private System.Windows.Forms.Button buttonDbSave;
        private System.Windows.Forms.GroupBox groupBoxServerLocation;
        private System.Windows.Forms.GroupBox groupBox4CreateServer;
        private System.Windows.Forms.GroupBox groupBoxAccessControlGroup;
        private System.Windows.Forms.GroupBox groupBoxServerImageAndSpec;
    }
}
