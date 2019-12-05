namespace HaTool.Tools
{
    partial class UcExecuterSql
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UcExecuterSql));
            this.groupBoxLoadBalancer = new System.Windows.Forms.GroupBox();
            this.groupBoxScriptAndResults = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.fastColoredTextBoxTemplate = new FastColoredTextBoxNS.FastColoredTextBox();
            this.fastColoredTextBoxResult = new FastColoredTextBoxNS.FastColoredTextBox();
            this.groupBoxSelectHaGroup = new System.Windows.Forms.GroupBox();
            this.comboBoxloadBalancerName = new System.Windows.Forms.ComboBox();
            this.textBoxServerName = new System.Windows.Forms.TextBox();
            this.radioButtonMaster = new System.Windows.Forms.RadioButton();
            this.radioButtonSlave = new System.Windows.Forms.RadioButton();
            this.buttonReload = new System.Windows.Forms.Button();
            this.radioButtonDomain = new System.Windows.Forms.RadioButton();
            this.labelServerName = new System.Windows.Forms.Label();
            this.groupBoxScriptTemplate = new System.Windows.Forms.GroupBox();
            this.checkBoxResultUpdateByGo = new System.Windows.Forms.CheckBox();
            this.buttonDeleteTemplate = new System.Windows.Forms.Button();
            this.comboBoxScriptTemplates = new System.Windows.Forms.ComboBox();
            this.buttonNewTemplate = new System.Windows.Forms.Button();
            this.buttonExecute = new System.Windows.Forms.Button();
            this.buttonModifyTemplate = new System.Windows.Forms.Button();
            this.groupBoxConnectionInfo = new System.Windows.Forms.GroupBox();
            this.checkBoxUsePrivateIp = new System.Windows.Forms.CheckBox();
            this.labelIp = new System.Windows.Forms.Label();
            this.textBoxConnectionTimeoutSec = new System.Windows.Forms.TextBox();
            this.labelPort = new System.Windows.Forms.Label();
            this.labelTimeoutSec = new System.Windows.Forms.Label();
            this.comboBoxDatabase = new System.Windows.Forms.ComboBox();
            this.textBoxCommandTimeoutSec = new System.Windows.Forms.TextBox();
            this.labeUserId = new System.Windows.Forms.Label();
            this.labelCmdTimeoutSec = new System.Windows.Forms.Label();
            this.labelPassword = new System.Windows.Forms.Label();
            this.labelDatabase = new System.Windows.Forms.Label();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.textBoxUserId = new System.Windows.Forms.TextBox();
            this.textBoxIP = new System.Windows.Forms.TextBox();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.groupBoxLoadBalancer.SuspendLayout();
            this.groupBoxScriptAndResults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fastColoredTextBoxTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fastColoredTextBoxResult)).BeginInit();
            this.groupBoxSelectHaGroup.SuspendLayout();
            this.groupBoxScriptTemplate.SuspendLayout();
            this.groupBoxConnectionInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxLoadBalancer
            // 
            this.groupBoxLoadBalancer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxLoadBalancer.Controls.Add(this.groupBoxScriptAndResults);
            this.groupBoxLoadBalancer.Controls.Add(this.groupBoxSelectHaGroup);
            this.groupBoxLoadBalancer.Controls.Add(this.groupBoxScriptTemplate);
            this.groupBoxLoadBalancer.Controls.Add(this.groupBoxConnectionInfo);
            this.groupBoxLoadBalancer.Location = new System.Drawing.Point(3, 3);
            this.groupBoxLoadBalancer.Name = "groupBoxLoadBalancer";
            this.groupBoxLoadBalancer.Size = new System.Drawing.Size(894, 694);
            this.groupBoxLoadBalancer.TabIndex = 1;
            this.groupBoxLoadBalancer.TabStop = false;
            this.groupBoxLoadBalancer.Text = "Tools > Executer Sql";
            // 
            // groupBoxScriptAndResults
            // 
            this.groupBoxScriptAndResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxScriptAndResults.Controls.Add(this.splitContainer1);
            this.groupBoxScriptAndResults.Location = new System.Drawing.Point(22, 305);
            this.groupBoxScriptAndResults.Name = "groupBoxScriptAndResults";
            this.groupBoxScriptAndResults.Size = new System.Drawing.Size(849, 383);
            this.groupBoxScriptAndResults.TabIndex = 160;
            this.groupBoxScriptAndResults.TabStop = false;
            this.groupBoxScriptAndResults.Text = "Script And Results";
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 19);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.fastColoredTextBoxTemplate);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.fastColoredTextBoxResult);
            this.splitContainer1.Size = new System.Drawing.Size(843, 361);
            this.splitContainer1.SplitterDistance = 181;
            this.splitContainer1.TabIndex = 0;
            // 
            // fastColoredTextBoxTemplate
            // 
            this.fastColoredTextBoxTemplate.AutoCompleteBracketsList = new char[] {
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
            this.fastColoredTextBoxTemplate.AutoIndentCharsPatterns = "";
            this.fastColoredTextBoxTemplate.AutoScrollMinSize = new System.Drawing.Size(25, 15);
            this.fastColoredTextBoxTemplate.BackBrush = null;
            this.fastColoredTextBoxTemplate.CharHeight = 15;
            this.fastColoredTextBoxTemplate.CharWidth = 7;
            this.fastColoredTextBoxTemplate.CommentPrefix = "--";
            this.fastColoredTextBoxTemplate.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.fastColoredTextBoxTemplate.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.fastColoredTextBoxTemplate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fastColoredTextBoxTemplate.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.fastColoredTextBoxTemplate.IsReplaceMode = false;
            this.fastColoredTextBoxTemplate.Language = FastColoredTextBoxNS.Language.SQL;
            this.fastColoredTextBoxTemplate.LeftBracket = '(';
            this.fastColoredTextBoxTemplate.Location = new System.Drawing.Point(0, 0);
            this.fastColoredTextBoxTemplate.Name = "fastColoredTextBoxTemplate";
            this.fastColoredTextBoxTemplate.Paddings = new System.Windows.Forms.Padding(0);
            this.fastColoredTextBoxTemplate.RightBracket = ')';
            this.fastColoredTextBoxTemplate.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.fastColoredTextBoxTemplate.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("fastColoredTextBoxTemplate.ServiceColors")));
            this.fastColoredTextBoxTemplate.Size = new System.Drawing.Size(843, 181);
            this.fastColoredTextBoxTemplate.TabIndex = 1;
            this.fastColoredTextBoxTemplate.Zoom = 100;
            this.fastColoredTextBoxTemplate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ExecuteHotKeyDown);
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
            this.fastColoredTextBoxResult.AutoIndentCharsPatterns = "";
            this.fastColoredTextBoxResult.AutoScrollMinSize = new System.Drawing.Size(25, 15);
            this.fastColoredTextBoxResult.BackBrush = null;
            this.fastColoredTextBoxResult.CharHeight = 15;
            this.fastColoredTextBoxResult.CharWidth = 7;
            this.fastColoredTextBoxResult.CommentPrefix = "--";
            this.fastColoredTextBoxResult.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.fastColoredTextBoxResult.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.fastColoredTextBoxResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fastColoredTextBoxResult.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.fastColoredTextBoxResult.IsReplaceMode = false;
            this.fastColoredTextBoxResult.Language = FastColoredTextBoxNS.Language.SQL;
            this.fastColoredTextBoxResult.LeftBracket = '(';
            this.fastColoredTextBoxResult.Location = new System.Drawing.Point(0, 0);
            this.fastColoredTextBoxResult.Name = "fastColoredTextBoxResult";
            this.fastColoredTextBoxResult.Paddings = new System.Windows.Forms.Padding(0);
            this.fastColoredTextBoxResult.RightBracket = ')';
            this.fastColoredTextBoxResult.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.fastColoredTextBoxResult.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("fastColoredTextBoxResult.ServiceColors")));
            this.fastColoredTextBoxResult.Size = new System.Drawing.Size(843, 176);
            this.fastColoredTextBoxResult.TabIndex = 7;
            this.fastColoredTextBoxResult.Zoom = 100;
            // 
            // groupBoxSelectHaGroup
            // 
            this.groupBoxSelectHaGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxSelectHaGroup.Controls.Add(this.comboBoxloadBalancerName);
            this.groupBoxSelectHaGroup.Controls.Add(this.textBoxServerName);
            this.groupBoxSelectHaGroup.Controls.Add(this.radioButtonMaster);
            this.groupBoxSelectHaGroup.Controls.Add(this.radioButtonSlave);
            this.groupBoxSelectHaGroup.Controls.Add(this.buttonReload);
            this.groupBoxSelectHaGroup.Controls.Add(this.radioButtonDomain);
            this.groupBoxSelectHaGroup.Controls.Add(this.labelServerName);
            this.groupBoxSelectHaGroup.Location = new System.Drawing.Point(22, 22);
            this.groupBoxSelectHaGroup.Name = "groupBoxSelectHaGroup";
            this.groupBoxSelectHaGroup.Size = new System.Drawing.Size(849, 60);
            this.groupBoxSelectHaGroup.TabIndex = 159;
            this.groupBoxSelectHaGroup.TabStop = false;
            this.groupBoxSelectHaGroup.Text = "Select HA Group";
            // 
            // comboBoxloadBalancerName
            // 
            this.comboBoxloadBalancerName.FormattingEnabled = true;
            this.comboBoxloadBalancerName.Location = new System.Drawing.Point(16, 22);
            this.comboBoxloadBalancerName.Name = "comboBoxloadBalancerName";
            this.comboBoxloadBalancerName.Size = new System.Drawing.Size(264, 23);
            this.comboBoxloadBalancerName.TabIndex = 45;
            this.comboBoxloadBalancerName.SelectedIndexChanged += new System.EventHandler(this.ConnectionTypeProperty_ClickOrChanged);
            // 
            // textBoxServerName
            // 
            this.textBoxServerName.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.textBoxServerName.Location = new System.Drawing.Point(698, 31);
            this.textBoxServerName.Name = "textBoxServerName";
            this.textBoxServerName.Size = new System.Drawing.Size(128, 23);
            this.textBoxServerName.TabIndex = 155;
            // 
            // radioButtonMaster
            // 
            this.radioButtonMaster.AutoSize = true;
            this.radioButtonMaster.Location = new System.Drawing.Point(440, 23);
            this.radioButtonMaster.Name = "radioButtonMaster";
            this.radioButtonMaster.Size = new System.Drawing.Size(67, 19);
            this.radioButtonMaster.TabIndex = 124;
            this.radioButtonMaster.TabStop = true;
            this.radioButtonMaster.Text = "MASTER";
            this.radioButtonMaster.UseVisualStyleBackColor = true;
            this.radioButtonMaster.Click += new System.EventHandler(this.ConnectionTypeProperty_ClickOrChanged);
            // 
            // radioButtonSlave
            // 
            this.radioButtonSlave.AutoSize = true;
            this.radioButtonSlave.Location = new System.Drawing.Point(513, 23);
            this.radioButtonSlave.Name = "radioButtonSlave";
            this.radioButtonSlave.Size = new System.Drawing.Size(60, 19);
            this.radioButtonSlave.TabIndex = 125;
            this.radioButtonSlave.TabStop = true;
            this.radioButtonSlave.Text = "SLAVE";
            this.radioButtonSlave.UseVisualStyleBackColor = true;
            this.radioButtonSlave.Click += new System.EventHandler(this.ConnectionTypeProperty_ClickOrChanged);
            // 
            // buttonReload
            // 
            this.buttonReload.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.buttonReload.Location = new System.Drawing.Point(288, 21);
            this.buttonReload.Name = "buttonReload";
            this.buttonReload.Size = new System.Drawing.Size(128, 25);
            this.buttonReload.TabIndex = 156;
            this.buttonReload.Text = "Reload";
            this.buttonReload.UseVisualStyleBackColor = true;
            this.buttonReload.Click += new System.EventHandler(this.buttonLoadServerList_Click);
            // 
            // radioButtonDomain
            // 
            this.radioButtonDomain.AutoSize = true;
            this.radioButtonDomain.Location = new System.Drawing.Point(579, 23);
            this.radioButtonDomain.Name = "radioButtonDomain";
            this.radioButtonDomain.Size = new System.Drawing.Size(67, 19);
            this.radioButtonDomain.TabIndex = 126;
            this.radioButtonDomain.TabStop = true;
            this.radioButtonDomain.Text = "DOMAIN";
            this.radioButtonDomain.UseVisualStyleBackColor = true;
            this.radioButtonDomain.CheckedChanged += new System.EventHandler(this.ConnectionTypeProperty_ClickOrChanged);
            this.radioButtonDomain.Click += new System.EventHandler(this.ConnectionTypeProperty_ClickOrChanged);
            // 
            // labelServerName
            // 
            this.labelServerName.AutoSize = true;
            this.labelServerName.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.labelServerName.Location = new System.Drawing.Point(699, 14);
            this.labelServerName.Name = "labelServerName";
            this.labelServerName.Size = new System.Drawing.Size(77, 15);
            this.labelServerName.TabIndex = 154;
            this.labelServerName.Text = "ServerName";
            // 
            // groupBoxScriptTemplate
            // 
            this.groupBoxScriptTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxScriptTemplate.Controls.Add(this.checkBoxResultUpdateByGo);
            this.groupBoxScriptTemplate.Controls.Add(this.buttonDeleteTemplate);
            this.groupBoxScriptTemplate.Controls.Add(this.comboBoxScriptTemplates);
            this.groupBoxScriptTemplate.Controls.Add(this.buttonNewTemplate);
            this.groupBoxScriptTemplate.Controls.Add(this.buttonExecute);
            this.groupBoxScriptTemplate.Controls.Add(this.buttonModifyTemplate);
            this.groupBoxScriptTemplate.Location = new System.Drawing.Point(22, 211);
            this.groupBoxScriptTemplate.Name = "groupBoxScriptTemplate";
            this.groupBoxScriptTemplate.Size = new System.Drawing.Size(849, 88);
            this.groupBoxScriptTemplate.TabIndex = 158;
            this.groupBoxScriptTemplate.TabStop = false;
            this.groupBoxScriptTemplate.Text = "Script Template";
            // 
            // checkBoxResultUpdateByGo
            // 
            this.checkBoxResultUpdateByGo.AutoSize = true;
            this.checkBoxResultUpdateByGo.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxResultUpdateByGo.Location = new System.Drawing.Point(34, 56);
            this.checkBoxResultUpdateByGo.Name = "checkBoxResultUpdateByGo";
            this.checkBoxResultUpdateByGo.Size = new System.Drawing.Size(110, 19);
            this.checkBoxResultUpdateByGo.TabIndex = 163;
            this.checkBoxResultUpdateByGo.Text = "Result By Go";
            this.checkBoxResultUpdateByGo.UseVisualStyleBackColor = true;
            // 
            // buttonDeleteTemplate
            // 
            this.buttonDeleteTemplate.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.buttonDeleteTemplate.Location = new System.Drawing.Point(698, 52);
            this.buttonDeleteTemplate.Name = "buttonDeleteTemplate";
            this.buttonDeleteTemplate.Size = new System.Drawing.Size(128, 25);
            this.buttonDeleteTemplate.TabIndex = 162;
            this.buttonDeleteTemplate.Text = "Delete Template";
            this.buttonDeleteTemplate.UseVisualStyleBackColor = true;
            this.buttonDeleteTemplate.Click += new System.EventHandler(this.buttonDeleteTemplate_Click_1);
            // 
            // comboBoxScriptTemplates
            // 
            this.comboBoxScriptTemplates.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.comboBoxScriptTemplates.FormattingEnabled = true;
            this.comboBoxScriptTemplates.Location = new System.Drawing.Point(17, 22);
            this.comboBoxScriptTemplates.Name = "comboBoxScriptTemplates";
            this.comboBoxScriptTemplates.Size = new System.Drawing.Size(809, 23);
            this.comboBoxScriptTemplates.TabIndex = 159;
            this.comboBoxScriptTemplates.SelectedIndexChanged += new System.EventHandler(this.ComboBoxScriptTemplatesChanged);
            // 
            // buttonNewTemplate
            // 
            this.buttonNewTemplate.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.buttonNewTemplate.Location = new System.Drawing.Point(562, 52);
            this.buttonNewTemplate.Name = "buttonNewTemplate";
            this.buttonNewTemplate.Size = new System.Drawing.Size(128, 25);
            this.buttonNewTemplate.TabIndex = 161;
            this.buttonNewTemplate.Text = "New Template";
            this.buttonNewTemplate.UseVisualStyleBackColor = true;
            this.buttonNewTemplate.Click += new System.EventHandler(this.buttonNewTemplate_Click);
            // 
            // buttonExecute
            // 
            this.buttonExecute.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.buttonExecute.Location = new System.Drawing.Point(152, 52);
            this.buttonExecute.Name = "buttonExecute";
            this.buttonExecute.Size = new System.Drawing.Size(264, 25);
            this.buttonExecute.TabIndex = 159;
            this.buttonExecute.Text = "Execute";
            this.buttonExecute.UseVisualStyleBackColor = true;
            this.buttonExecute.Click += new System.EventHandler(this.buttonExecute_Click);
            // 
            // buttonModifyTemplate
            // 
            this.buttonModifyTemplate.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.buttonModifyTemplate.Location = new System.Drawing.Point(422, 52);
            this.buttonModifyTemplate.Name = "buttonModifyTemplate";
            this.buttonModifyTemplate.Size = new System.Drawing.Size(130, 25);
            this.buttonModifyTemplate.TabIndex = 160;
            this.buttonModifyTemplate.Text = "Modify Template";
            this.buttonModifyTemplate.UseVisualStyleBackColor = true;
            this.buttonModifyTemplate.Click += new System.EventHandler(this.buttonModifyTemplate_Click);
            // 
            // groupBoxConnectionInfo
            // 
            this.groupBoxConnectionInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxConnectionInfo.Controls.Add(this.checkBoxUsePrivateIp);
            this.groupBoxConnectionInfo.Controls.Add(this.labelIp);
            this.groupBoxConnectionInfo.Controls.Add(this.textBoxConnectionTimeoutSec);
            this.groupBoxConnectionInfo.Controls.Add(this.labelPort);
            this.groupBoxConnectionInfo.Controls.Add(this.labelTimeoutSec);
            this.groupBoxConnectionInfo.Controls.Add(this.comboBoxDatabase);
            this.groupBoxConnectionInfo.Controls.Add(this.textBoxCommandTimeoutSec);
            this.groupBoxConnectionInfo.Controls.Add(this.labeUserId);
            this.groupBoxConnectionInfo.Controls.Add(this.labelCmdTimeoutSec);
            this.groupBoxConnectionInfo.Controls.Add(this.labelPassword);
            this.groupBoxConnectionInfo.Controls.Add(this.labelDatabase);
            this.groupBoxConnectionInfo.Controls.Add(this.textBoxPassword);
            this.groupBoxConnectionInfo.Controls.Add(this.textBoxUserId);
            this.groupBoxConnectionInfo.Controls.Add(this.textBoxIP);
            this.groupBoxConnectionInfo.Controls.Add(this.textBoxPort);
            this.groupBoxConnectionInfo.Location = new System.Drawing.Point(22, 88);
            this.groupBoxConnectionInfo.Name = "groupBoxConnectionInfo";
            this.groupBoxConnectionInfo.Size = new System.Drawing.Size(849, 117);
            this.groupBoxConnectionInfo.TabIndex = 157;
            this.groupBoxConnectionInfo.TabStop = false;
            this.groupBoxConnectionInfo.Text = "Connection Info";
            // 
            // checkBoxUsePrivateIp
            // 
            this.checkBoxUsePrivateIp.AutoSize = true;
            this.checkBoxUsePrivateIp.Location = new System.Drawing.Point(564, 84);
            this.checkBoxUsePrivateIp.Name = "checkBoxUsePrivateIp";
            this.checkBoxUsePrivateIp.Size = new System.Drawing.Size(124, 19);
            this.checkBoxUsePrivateIp.TabIndex = 156;
            this.checkBoxUsePrivateIp.Text = "Use Private Ip";
            this.checkBoxUsePrivateIp.UseVisualStyleBackColor = true;
            this.checkBoxUsePrivateIp.Click += new System.EventHandler(this.ConnectionTypeProperty_ClickOrChanged);
            // 
            // labelIp
            // 
            this.labelIp.AutoSize = true;
            this.labelIp.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.labelIp.Location = new System.Drawing.Point(15, 19);
            this.labelIp.Name = "labelIp";
            this.labelIp.Size = new System.Drawing.Size(21, 15);
            this.labelIp.TabIndex = 140;
            this.labelIp.Text = "IP";
            // 
            // textBoxConnectionTimeoutSec
            // 
            this.textBoxConnectionTimeoutSec.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.textBoxConnectionTimeoutSec.Location = new System.Drawing.Point(288, 82);
            this.textBoxConnectionTimeoutSec.Name = "textBoxConnectionTimeoutSec";
            this.textBoxConnectionTimeoutSec.Size = new System.Drawing.Size(128, 23);
            this.textBoxConnectionTimeoutSec.TabIndex = 153;
            // 
            // labelPort
            // 
            this.labelPort.AutoSize = true;
            this.labelPort.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.labelPort.Location = new System.Drawing.Point(428, 19);
            this.labelPort.Name = "labelPort";
            this.labelPort.Size = new System.Drawing.Size(35, 15);
            this.labelPort.TabIndex = 141;
            this.labelPort.Text = "Port";
            // 
            // labelTimeoutSec
            // 
            this.labelTimeoutSec.AutoSize = true;
            this.labelTimeoutSec.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.labelTimeoutSec.Location = new System.Drawing.Point(290, 64);
            this.labelTimeoutSec.Name = "labelTimeoutSec";
            this.labelTimeoutSec.Size = new System.Drawing.Size(119, 15);
            this.labelTimeoutSec.TabIndex = 152;
            this.labelTimeoutSec.Text = "Conn Timeout Sec";
            // 
            // comboBoxDatabase
            // 
            this.comboBoxDatabase.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.comboBoxDatabase.FormattingEnabled = true;
            this.comboBoxDatabase.Location = new System.Drawing.Point(17, 82);
            this.comboBoxDatabase.Name = "comboBoxDatabase";
            this.comboBoxDatabase.Size = new System.Drawing.Size(263, 23);
            this.comboBoxDatabase.TabIndex = 150;
            // 
            // textBoxCommandTimeoutSec
            // 
            this.textBoxCommandTimeoutSec.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.textBoxCommandTimeoutSec.Location = new System.Drawing.Point(424, 82);
            this.textBoxCommandTimeoutSec.Name = "textBoxCommandTimeoutSec";
            this.textBoxCommandTimeoutSec.Size = new System.Drawing.Size(128, 23);
            this.textBoxCommandTimeoutSec.TabIndex = 151;
            // 
            // labeUserId
            // 
            this.labeUserId.AutoSize = true;
            this.labeUserId.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.labeUserId.Location = new System.Drawing.Point(566, 19);
            this.labeUserId.Name = "labeUserId";
            this.labeUserId.Size = new System.Drawing.Size(49, 15);
            this.labeUserId.TabIndex = 142;
            this.labeUserId.Text = "UserId";
            // 
            // labelCmdTimeoutSec
            // 
            this.labelCmdTimeoutSec.AutoSize = true;
            this.labelCmdTimeoutSec.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.labelCmdTimeoutSec.Location = new System.Drawing.Point(423, 64);
            this.labelCmdTimeoutSec.Name = "labelCmdTimeoutSec";
            this.labelCmdTimeoutSec.Size = new System.Drawing.Size(112, 15);
            this.labelCmdTimeoutSec.TabIndex = 145;
            this.labelCmdTimeoutSec.Text = "Cmd Timeout Sec";
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.labelPassword.Location = new System.Drawing.Point(701, 19);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(63, 15);
            this.labelPassword.TabIndex = 143;
            this.labelPassword.Text = "Password";
            // 
            // labelDatabase
            // 
            this.labelDatabase.AutoSize = true;
            this.labelDatabase.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.labelDatabase.Location = new System.Drawing.Point(15, 64);
            this.labelDatabase.Name = "labelDatabase";
            this.labelDatabase.Size = new System.Drawing.Size(63, 15);
            this.labelDatabase.TabIndex = 144;
            this.labelDatabase.Text = "Database";
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.textBoxPassword.Location = new System.Drawing.Point(698, 37);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(128, 23);
            this.textBoxPassword.TabIndex = 149;
            // 
            // textBoxUserId
            // 
            this.textBoxUserId.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.textBoxUserId.Location = new System.Drawing.Point(562, 37);
            this.textBoxUserId.Name = "textBoxUserId";
            this.textBoxUserId.Size = new System.Drawing.Size(128, 23);
            this.textBoxUserId.TabIndex = 148;
            // 
            // textBoxIP
            // 
            this.textBoxIP.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.textBoxIP.Location = new System.Drawing.Point(18, 37);
            this.textBoxIP.Name = "textBoxIP";
            this.textBoxIP.Size = new System.Drawing.Size(398, 23);
            this.textBoxIP.TabIndex = 146;
            // 
            // textBoxPort
            // 
            this.textBoxPort.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.textBoxPort.Location = new System.Drawing.Point(424, 37);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(130, 23);
            this.textBoxPort.TabIndex = 147;
            // 
            // UcExecuterSql
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.groupBoxLoadBalancer);
            this.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.Name = "UcExecuterSql";
            this.Size = new System.Drawing.Size(900, 700);
            this.Load += new System.EventHandler(this.LoadData);
            this.groupBoxLoadBalancer.ResumeLayout(false);
            this.groupBoxScriptAndResults.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fastColoredTextBoxTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fastColoredTextBoxResult)).EndInit();
            this.groupBoxSelectHaGroup.ResumeLayout(false);
            this.groupBoxSelectHaGroup.PerformLayout();
            this.groupBoxScriptTemplate.ResumeLayout(false);
            this.groupBoxScriptTemplate.PerformLayout();
            this.groupBoxConnectionInfo.ResumeLayout(false);
            this.groupBoxConnectionInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBoxLoadBalancer;
        private System.Windows.Forms.GroupBox groupBoxSelectHaGroup;
        private System.Windows.Forms.ComboBox comboBoxloadBalancerName;
        private System.Windows.Forms.RadioButton radioButtonMaster;
        private System.Windows.Forms.RadioButton radioButtonSlave;
        private System.Windows.Forms.Button buttonReload;
        private System.Windows.Forms.RadioButton radioButtonDomain;
        private System.Windows.Forms.GroupBox groupBoxScriptTemplate;
        private System.Windows.Forms.CheckBox checkBoxResultUpdateByGo;
        private System.Windows.Forms.Button buttonDeleteTemplate;
        private System.Windows.Forms.ComboBox comboBoxScriptTemplates;
        private System.Windows.Forms.Button buttonNewTemplate;
        private System.Windows.Forms.Button buttonExecute;
        private System.Windows.Forms.Button buttonModifyTemplate;
        private System.Windows.Forms.GroupBox groupBoxConnectionInfo;
        private System.Windows.Forms.CheckBox checkBoxUsePrivateIp;
        private System.Windows.Forms.TextBox textBoxServerName;
        private System.Windows.Forms.Label labelIp;
        private System.Windows.Forms.TextBox textBoxConnectionTimeoutSec;
        private System.Windows.Forms.Label labelPort;
        private System.Windows.Forms.Label labelTimeoutSec;
        private System.Windows.Forms.Label labelServerName;
        private System.Windows.Forms.ComboBox comboBoxDatabase;
        private System.Windows.Forms.TextBox textBoxCommandTimeoutSec;
        private System.Windows.Forms.Label labeUserId;
        private System.Windows.Forms.Label labelCmdTimeoutSec;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.Label labelDatabase;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.TextBox textBoxUserId;
        private System.Windows.Forms.TextBox textBoxIP;
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.GroupBox groupBoxScriptAndResults;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private FastColoredTextBoxNS.FastColoredTextBox fastColoredTextBoxTemplate;
        private FastColoredTextBoxNS.FastColoredTextBox fastColoredTextBoxResult;
    }
}
