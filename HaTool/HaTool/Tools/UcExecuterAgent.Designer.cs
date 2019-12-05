namespace HaTool.Tools
{
    partial class UcExecuterAgent
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UcExecuterAgent));
            this.groupBoxExecuterAgent = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.fastColoredTextBoxTemplate = new FastColoredTextBoxNS.FastColoredTextBox();
            this.fastColoredTextBoxResult = new FastColoredTextBoxNS.FastColoredTextBox();
            this.groupBoxSelectHaGroup = new System.Windows.Forms.GroupBox();
            this.comboBoxloadBalancerName = new System.Windows.Forms.ComboBox();
            this.radioButtonMaster = new System.Windows.Forms.RadioButton();
            this.radioButtonSlave = new System.Windows.Forms.RadioButton();
            this.buttonReload = new System.Windows.Forms.Button();
            this.textBoxServerName = new System.Windows.Forms.TextBox();
            this.labelServerName = new System.Windows.Forms.Label();
            this.groupBoxScriptTemplate = new System.Windows.Forms.GroupBox();
            this.buttonDeleteTemplate = new System.Windows.Forms.Button();
            this.comboBoxScriptTemplates = new System.Windows.Forms.ComboBox();
            this.buttonNewTemplate = new System.Windows.Forms.Button();
            this.buttonExecute = new System.Windows.Forms.Button();
            this.buttonModifyTemplate = new System.Windows.Forms.Button();
            this.groupBoxConnectionInfo = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxSecretKey = new System.Windows.Forms.MaskedTextBox();
            this.textBoxAccessKey = new System.Windows.Forms.TextBox();
            this.labelCmdType = new System.Windows.Forms.Label();
            this.labelCmd = new System.Windows.Forms.Label();
            this.comboBoxCmdType = new System.Windows.Forms.ComboBox();
            this.comboBoxCmd = new System.Windows.Forms.ComboBox();
            this.labelAction = new System.Windows.Forms.Label();
            this.textBoxAction = new System.Windows.Forms.TextBox();
            this.checkBoxUsePrivateIp = new System.Windows.Forms.CheckBox();
            this.labelIp = new System.Windows.Forms.Label();
            this.labelPort = new System.Windows.Forms.Label();
            this.textBoxIP = new System.Windows.Forms.TextBox();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.groupBoxExecuterAgent.SuspendLayout();
            this.groupBox4.SuspendLayout();
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
            // groupBoxExecuterAgent
            // 
            this.groupBoxExecuterAgent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxExecuterAgent.Controls.Add(this.groupBox4);
            this.groupBoxExecuterAgent.Controls.Add(this.groupBoxSelectHaGroup);
            this.groupBoxExecuterAgent.Controls.Add(this.groupBoxScriptTemplate);
            this.groupBoxExecuterAgent.Controls.Add(this.groupBoxConnectionInfo);
            this.groupBoxExecuterAgent.Location = new System.Drawing.Point(3, 3);
            this.groupBoxExecuterAgent.Name = "groupBoxExecuterAgent";
            this.groupBoxExecuterAgent.Size = new System.Drawing.Size(894, 694);
            this.groupBoxExecuterAgent.TabIndex = 1;
            this.groupBoxExecuterAgent.TabStop = false;
            this.groupBoxExecuterAgent.Text = "Tools > Executer Agent";
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.splitContainer1);
            this.groupBox4.Location = new System.Drawing.Point(22, 305);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(849, 383);
            this.groupBox4.TabIndex = 160;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Script And Results";
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
            this.groupBoxSelectHaGroup.Controls.Add(this.radioButtonMaster);
            this.groupBoxSelectHaGroup.Controls.Add(this.radioButtonSlave);
            this.groupBoxSelectHaGroup.Controls.Add(this.buttonReload);
            this.groupBoxSelectHaGroup.Controls.Add(this.textBoxServerName);
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
            // textBoxServerName
            // 
            this.textBoxServerName.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.textBoxServerName.Location = new System.Drawing.Point(699, 32);
            this.textBoxServerName.Name = "textBoxServerName";
            this.textBoxServerName.Size = new System.Drawing.Size(128, 23);
            this.textBoxServerName.TabIndex = 155;
            // 
            // labelServerName
            // 
            this.labelServerName.AutoSize = true;
            this.labelServerName.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.labelServerName.Location = new System.Drawing.Point(701, 14);
            this.labelServerName.Name = "labelServerName";
            this.labelServerName.Size = new System.Drawing.Size(77, 15);
            this.labelServerName.TabIndex = 154;
            this.labelServerName.Text = "ServerName";
            // 
            // groupBoxScriptTemplate
            // 
            this.groupBoxScriptTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.groupBoxConnectionInfo.Controls.Add(this.label7);
            this.groupBoxConnectionInfo.Controls.Add(this.label4);
            this.groupBoxConnectionInfo.Controls.Add(this.textBoxSecretKey);
            this.groupBoxConnectionInfo.Controls.Add(this.textBoxAccessKey);
            this.groupBoxConnectionInfo.Controls.Add(this.labelCmdType);
            this.groupBoxConnectionInfo.Controls.Add(this.labelCmd);
            this.groupBoxConnectionInfo.Controls.Add(this.comboBoxCmdType);
            this.groupBoxConnectionInfo.Controls.Add(this.comboBoxCmd);
            this.groupBoxConnectionInfo.Controls.Add(this.labelAction);
            this.groupBoxConnectionInfo.Controls.Add(this.textBoxAction);
            this.groupBoxConnectionInfo.Controls.Add(this.checkBoxUsePrivateIp);
            this.groupBoxConnectionInfo.Controls.Add(this.labelIp);
            this.groupBoxConnectionInfo.Controls.Add(this.labelPort);
            this.groupBoxConnectionInfo.Controls.Add(this.textBoxIP);
            this.groupBoxConnectionInfo.Controls.Add(this.textBoxPort);
            this.groupBoxConnectionInfo.Location = new System.Drawing.Point(22, 88);
            this.groupBoxConnectionInfo.Name = "groupBoxConnectionInfo";
            this.groupBoxConnectionInfo.Size = new System.Drawing.Size(849, 117);
            this.groupBoxConnectionInfo.TabIndex = 157;
            this.groupBoxConnectionInfo.TabStop = false;
            this.groupBoxConnectionInfo.Text = "Connection Info";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.label7.Location = new System.Drawing.Point(481, 68);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 15);
            this.label7.TabIndex = 166;
            this.label7.Text = "SecretKey";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.label4.Location = new System.Drawing.Point(318, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 15);
            this.label4.TabIndex = 165;
            this.label4.Text = "AccessKey";
            // 
            // textBoxSecretKey
            // 
            this.textBoxSecretKey.Location = new System.Drawing.Point(481, 86);
            this.textBoxSecretKey.Name = "textBoxSecretKey";
            this.textBoxSecretKey.PasswordChar = '*';
            this.textBoxSecretKey.Size = new System.Drawing.Size(345, 23);
            this.textBoxSecretKey.TabIndex = 164;
            // 
            // textBoxAccessKey
            // 
            this.textBoxAccessKey.Location = new System.Drawing.Point(314, 86);
            this.textBoxAccessKey.Name = "textBoxAccessKey";
            this.textBoxAccessKey.Size = new System.Drawing.Size(161, 23);
            this.textBoxAccessKey.TabIndex = 163;
            // 
            // labelCmdType
            // 
            this.labelCmdType.AutoSize = true;
            this.labelCmdType.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.labelCmdType.Location = new System.Drawing.Point(156, 68);
            this.labelCmdType.Name = "labelCmdType";
            this.labelCmdType.Size = new System.Drawing.Size(56, 15);
            this.labelCmdType.TabIndex = 162;
            this.labelCmdType.Text = "CmdType";
            // 
            // labelCmd
            // 
            this.labelCmd.AutoSize = true;
            this.labelCmd.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.labelCmd.Location = new System.Drawing.Point(20, 68);
            this.labelCmd.Name = "labelCmd";
            this.labelCmd.Size = new System.Drawing.Size(28, 15);
            this.labelCmd.TabIndex = 161;
            this.labelCmd.Text = "Cmd";
            // 
            // comboBoxCmdType
            // 
            this.comboBoxCmdType.FormattingEnabled = true;
            this.comboBoxCmdType.Location = new System.Drawing.Point(152, 86);
            this.comboBoxCmdType.Name = "comboBoxCmdType";
            this.comboBoxCmdType.Size = new System.Drawing.Size(156, 23);
            this.comboBoxCmdType.TabIndex = 160;
            // 
            // comboBoxCmd
            // 
            this.comboBoxCmd.FormattingEnabled = true;
            this.comboBoxCmd.Location = new System.Drawing.Point(16, 86);
            this.comboBoxCmd.Name = "comboBoxCmd";
            this.comboBoxCmd.Size = new System.Drawing.Size(128, 23);
            this.comboBoxCmd.TabIndex = 159;
            this.comboBoxCmd.SelectedIndexChanged += new System.EventHandler(this.cmd_SelectedIndexChanged);
            // 
            // labelAction
            // 
            this.labelAction.AutoSize = true;
            this.labelAction.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.labelAction.Location = new System.Drawing.Point(386, 19);
            this.labelAction.Name = "labelAction";
            this.labelAction.Size = new System.Drawing.Size(49, 15);
            this.labelAction.TabIndex = 157;
            this.labelAction.Text = "Action";
            // 
            // textBoxAction
            // 
            this.textBoxAction.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.textBoxAction.Location = new System.Drawing.Point(380, 37);
            this.textBoxAction.Name = "textBoxAction";
            this.textBoxAction.Size = new System.Drawing.Size(310, 23);
            this.textBoxAction.TabIndex = 158;
            // 
            // checkBoxUsePrivateIp
            // 
            this.checkBoxUsePrivateIp.AutoSize = true;
            this.checkBoxUsePrivateIp.Location = new System.Drawing.Point(702, 41);
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
            this.labelIp.Location = new System.Drawing.Point(20, 19);
            this.labelIp.Name = "labelIp";
            this.labelIp.Size = new System.Drawing.Size(21, 15);
            this.labelIp.TabIndex = 140;
            this.labelIp.Text = "IP";
            // 
            // labelPort
            // 
            this.labelPort.AutoSize = true;
            this.labelPort.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.labelPort.Location = new System.Drawing.Point(294, 19);
            this.labelPort.Name = "labelPort";
            this.labelPort.Size = new System.Drawing.Size(35, 15);
            this.labelPort.TabIndex = 141;
            this.labelPort.Text = "Port";
            // 
            // textBoxIP
            // 
            this.textBoxIP.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.textBoxIP.Location = new System.Drawing.Point(16, 37);
            this.textBoxIP.Name = "textBoxIP";
            this.textBoxIP.Size = new System.Drawing.Size(266, 23);
            this.textBoxIP.TabIndex = 146;
            // 
            // textBoxPort
            // 
            this.textBoxPort.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.textBoxPort.Location = new System.Drawing.Point(288, 37);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(86, 23);
            this.textBoxPort.TabIndex = 147;
            // 
            // UcExecuterAgent
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.groupBoxExecuterAgent);
            this.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.Name = "UcExecuterAgent";
            this.Size = new System.Drawing.Size(900, 700);
            this.Load += new System.EventHandler(this.LoadData);
            this.groupBoxExecuterAgent.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fastColoredTextBoxTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fastColoredTextBoxResult)).EndInit();
            this.groupBoxSelectHaGroup.ResumeLayout(false);
            this.groupBoxSelectHaGroup.PerformLayout();
            this.groupBoxScriptTemplate.ResumeLayout(false);
            this.groupBoxConnectionInfo.ResumeLayout(false);
            this.groupBoxConnectionInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBoxExecuterAgent;
        private System.Windows.Forms.GroupBox groupBoxSelectHaGroup;
        private System.Windows.Forms.ComboBox comboBoxloadBalancerName;
        private System.Windows.Forms.RadioButton radioButtonMaster;
        private System.Windows.Forms.RadioButton radioButtonSlave;
        private System.Windows.Forms.Button buttonReload;
        private System.Windows.Forms.GroupBox groupBoxScriptTemplate;
        private System.Windows.Forms.Button buttonDeleteTemplate;
        private System.Windows.Forms.ComboBox comboBoxScriptTemplates;
        private System.Windows.Forms.Button buttonNewTemplate;
        private System.Windows.Forms.Button buttonExecute;
        private System.Windows.Forms.Button buttonModifyTemplate;
        private System.Windows.Forms.GroupBox groupBoxConnectionInfo;
        private System.Windows.Forms.CheckBox checkBoxUsePrivateIp;
        private System.Windows.Forms.TextBox textBoxServerName;
        private System.Windows.Forms.Label labelIp;
        private System.Windows.Forms.Label labelPort;
        private System.Windows.Forms.Label labelServerName;
        private System.Windows.Forms.TextBox textBoxIP;
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private FastColoredTextBoxNS.FastColoredTextBox fastColoredTextBoxTemplate;
        private FastColoredTextBoxNS.FastColoredTextBox fastColoredTextBoxResult;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.MaskedTextBox textBoxSecretKey;
        private System.Windows.Forms.TextBox textBoxAccessKey;
        private System.Windows.Forms.Label labelCmdType;
        private System.Windows.Forms.Label labelCmd;
        private System.Windows.Forms.ComboBox comboBoxCmdType;
        private System.Windows.Forms.ComboBox comboBoxCmd;
        private System.Windows.Forms.Label labelAction;
        private System.Windows.Forms.TextBox textBoxAction;
    }
}
