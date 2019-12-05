namespace HaTool.Tools
{
    partial class UcExecuterMultiSql
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UcExecuterMultiSql));
            this.panelGroupAndQuery = new System.Windows.Forms.Panel();
            this.splitContainerServerGroup = new System.Windows.Forms.SplitContainer();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.dgvServerList = new System.Windows.Forms.DataGridView();
            this.groupBoxSelectHaGroup = new System.Windows.Forms.GroupBox();
            this.button5 = new System.Windows.Forms.Button();
            this.comboBoxSelectServerGroup = new System.Windows.Forms.ComboBox();
            this.buttonReload = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonConnectionTestSelectedServer = new System.Windows.Forms.Button();
            this.buttonExecute = new System.Windows.Forms.Button();
            this.groupBoxScriptAndResults = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.fastColoredTextBoxTemplate = new FastColoredTextBoxNS.FastColoredTextBox();
            this.fastColoredTextBoxResult = new FastColoredTextBoxNS.FastColoredTextBox();
            this.groupBoxConnectionInfo = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxThinkTimeSec = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxConnectionTimeoutSec = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxCommandTimeoutSec = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxThreadCount = new System.Windows.Forms.TextBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxUserId = new System.Windows.Forms.TextBox();
            this.groupBoxScriptTemplate = new System.Windows.Forms.GroupBox();
            this.labelColumnDelimiter = new System.Windows.Forms.Label();
            this.textBoxColumnDelimiter = new System.Windows.Forms.TextBox();
            this.buttonDeleteTemplate = new System.Windows.Forms.Button();
            this.comboBoxScriptTemplates = new System.Windows.Forms.ComboBox();
            this.buttonNewTemplate = new System.Windows.Forms.Button();
            this.buttonModifyTemplate = new System.Windows.Forms.Button();
            this.groupBoxLoadBalancer = new System.Windows.Forms.GroupBox();
            this.panelGroupAndQuery.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerServerGroup)).BeginInit();
            this.splitContainerServerGroup.Panel1.SuspendLayout();
            this.splitContainerServerGroup.Panel2.SuspendLayout();
            this.splitContainerServerGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvServerList)).BeginInit();
            this.groupBoxSelectHaGroup.SuspendLayout();
            this.groupBoxScriptAndResults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fastColoredTextBoxTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fastColoredTextBoxResult)).BeginInit();
            this.groupBoxConnectionInfo.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBoxScriptTemplate.SuspendLayout();
            this.groupBoxLoadBalancer.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelGroupAndQuery
            // 
            this.panelGroupAndQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelGroupAndQuery.Controls.Add(this.splitContainerServerGroup);
            this.panelGroupAndQuery.Location = new System.Drawing.Point(6, 22);
            this.panelGroupAndQuery.Name = "panelGroupAndQuery";
            this.panelGroupAndQuery.Size = new System.Drawing.Size(925, 628);
            this.panelGroupAndQuery.TabIndex = 160;
            // 
            // splitContainerServerGroup
            // 
            this.splitContainerServerGroup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.splitContainerServerGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerServerGroup.Location = new System.Drawing.Point(0, 0);
            this.splitContainerServerGroup.Name = "splitContainerServerGroup";
            // 
            // splitContainerServerGroup.Panel1
            // 
            this.splitContainerServerGroup.Panel1.BackColor = System.Drawing.Color.White;
            this.splitContainerServerGroup.Panel1.Controls.Add(this.button4);
            this.splitContainerServerGroup.Panel1.Controls.Add(this.button3);
            this.splitContainerServerGroup.Panel1.Controls.Add(this.button2);
            this.splitContainerServerGroup.Panel1.Controls.Add(this.dgvServerList);
            this.splitContainerServerGroup.Panel1.Controls.Add(this.groupBoxSelectHaGroup);
            // 
            // splitContainerServerGroup.Panel2
            // 
            this.splitContainerServerGroup.Panel2.BackColor = System.Drawing.Color.White;
            this.splitContainerServerGroup.Panel2.Controls.Add(this.buttonCancel);
            this.splitContainerServerGroup.Panel2.Controls.Add(this.buttonConnectionTestSelectedServer);
            this.splitContainerServerGroup.Panel2.Controls.Add(this.buttonExecute);
            this.splitContainerServerGroup.Panel2.Controls.Add(this.groupBoxScriptAndResults);
            this.splitContainerServerGroup.Panel2.Controls.Add(this.groupBoxConnectionInfo);
            this.splitContainerServerGroup.Panel2.Controls.Add(this.groupBoxScriptTemplate);
            this.splitContainerServerGroup.Size = new System.Drawing.Size(925, 628);
            this.splitContainerServerGroup.SplitterDistance = 343;
            this.splitContainerServerGroup.TabIndex = 0;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(121, 96);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(54, 25);
            this.button4.TabIndex = 162;
            this.button4.Text = "Rev";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.buttonAllReverse_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(63, 96);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(54, 25);
            this.button3.TabIndex = 161;
            this.button3.Text = "UnChk";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.buttonAllUnCheck_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(5, 96);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(54, 25);
            this.button2.TabIndex = 160;
            this.button2.Text = "Chk";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.buttonAllCheck_Click);
            // 
            // dgvServerList
            // 
            this.dgvServerList.AllowUserToAddRows = false;
            this.dgvServerList.AllowUserToDeleteRows = false;
            this.dgvServerList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvServerList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvServerList.Location = new System.Drawing.Point(5, 127);
            this.dgvServerList.Name = "dgvServerList";
            this.dgvServerList.Size = new System.Drawing.Size(335, 495);
            this.dgvServerList.TabIndex = 15;
            this.dgvServerList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvServerList_CellContentClick);
            // 
            // groupBoxSelectHaGroup
            // 
            this.groupBoxSelectHaGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxSelectHaGroup.Controls.Add(this.button5);
            this.groupBoxSelectHaGroup.Controls.Add(this.comboBoxSelectServerGroup);
            this.groupBoxSelectHaGroup.Controls.Add(this.buttonReload);
            this.groupBoxSelectHaGroup.Location = new System.Drawing.Point(5, 3);
            this.groupBoxSelectHaGroup.Name = "groupBoxSelectHaGroup";
            this.groupBoxSelectHaGroup.Size = new System.Drawing.Size(335, 87);
            this.groupBoxSelectHaGroup.TabIndex = 159;
            this.groupBoxSelectHaGroup.TabStop = false;
            this.groupBoxSelectHaGroup.Text = "Select Server Group";
            // 
            // button5
            // 
            this.button5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button5.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.button5.Location = new System.Drawing.Point(233, 51);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(96, 25);
            this.button5.TabIndex = 164;
            this.button5.Text = "Modify";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.buttonServerGroupModify_Click);
            // 
            // comboBoxSelectServerGroup
            // 
            this.comboBoxSelectServerGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxSelectServerGroup.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.comboBoxSelectServerGroup.FormattingEnabled = true;
            this.comboBoxSelectServerGroup.Location = new System.Drawing.Point(6, 22);
            this.comboBoxSelectServerGroup.Name = "comboBoxSelectServerGroup";
            this.comboBoxSelectServerGroup.Size = new System.Drawing.Size(323, 23);
            this.comboBoxSelectServerGroup.TabIndex = 157;
            this.comboBoxSelectServerGroup.SelectedIndexChanged += new System.EventHandler(this.comboBoxSelectServerGroup_SelectedIndexChanged);
            // 
            // buttonReload
            // 
            this.buttonReload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonReload.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.buttonReload.Location = new System.Drawing.Point(131, 51);
            this.buttonReload.Name = "buttonReload";
            this.buttonReload.Size = new System.Drawing.Size(96, 25);
            this.buttonReload.TabIndex = 156;
            this.buttonReload.Text = "Reload";
            this.buttonReload.UseVisualStyleBackColor = true;
            this.buttonReload.Click += new System.EventHandler(this.buttonLoadServerList_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonCancel.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.buttonCancel.Location = new System.Drawing.Point(372, 143);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(156, 25);
            this.buttonCancel.TabIndex = 165;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonConnectionTestSelectedServer
            // 
            this.buttonConnectionTestSelectedServer.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonConnectionTestSelectedServer.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.buttonConnectionTestSelectedServer.Location = new System.Drawing.Point(54, 143);
            this.buttonConnectionTestSelectedServer.Name = "buttonConnectionTestSelectedServer";
            this.buttonConnectionTestSelectedServer.Size = new System.Drawing.Size(151, 25);
            this.buttonConnectionTestSelectedServer.TabIndex = 164;
            this.buttonConnectionTestSelectedServer.Text = "Connection Test";
            this.buttonConnectionTestSelectedServer.UseVisualStyleBackColor = true;
            this.buttonConnectionTestSelectedServer.Click += new System.EventHandler(this.buttonConnectionTestSelectedServer_Click);
            // 
            // buttonExecute
            // 
            this.buttonExecute.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonExecute.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.buttonExecute.Location = new System.Drawing.Point(211, 143);
            this.buttonExecute.Name = "buttonExecute";
            this.buttonExecute.Size = new System.Drawing.Size(155, 25);
            this.buttonExecute.TabIndex = 162;
            this.buttonExecute.Text = "Execute";
            this.buttonExecute.UseVisualStyleBackColor = true;
            this.buttonExecute.Click += new System.EventHandler(this.buttonExecute_Click);
            // 
            // groupBoxScriptAndResults
            // 
            this.groupBoxScriptAndResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxScriptAndResults.Controls.Add(this.splitContainer1);
            this.groupBoxScriptAndResults.Location = new System.Drawing.Point(3, 268);
            this.groupBoxScriptAndResults.Name = "groupBoxScriptAndResults";
            this.groupBoxScriptAndResults.Size = new System.Drawing.Size(572, 357);
            this.groupBoxScriptAndResults.TabIndex = 161;
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
            this.splitContainer1.Size = new System.Drawing.Size(566, 335);
            this.splitContainer1.SplitterDistance = 152;
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
            this.fastColoredTextBoxTemplate.AutoScrollMinSize = new System.Drawing.Size(2, 15);
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
            this.fastColoredTextBoxTemplate.Size = new System.Drawing.Size(566, 152);
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
            this.fastColoredTextBoxResult.AutoScrollMinSize = new System.Drawing.Size(2, 15);
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
            this.fastColoredTextBoxResult.Size = new System.Drawing.Size(566, 179);
            this.fastColoredTextBoxResult.TabIndex = 7;
            this.fastColoredTextBoxResult.Zoom = 100;
            // 
            // groupBoxConnectionInfo
            // 
            this.groupBoxConnectionInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxConnectionInfo.Controls.Add(this.tableLayoutPanel1);
            this.groupBoxConnectionInfo.Location = new System.Drawing.Point(3, 3);
            this.groupBoxConnectionInfo.Name = "groupBoxConnectionInfo";
            this.groupBoxConnectionInfo.Size = new System.Drawing.Size(569, 131);
            this.groupBoxConnectionInfo.TabIndex = 157;
            this.groupBoxConnectionInfo.TabStop = false;
            this.groupBoxConnectionInfo.Text = "Connection Info";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.textBoxThinkTimeSec, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.label7, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.textBoxConnectionTimeoutSec, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label4, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.textBoxCommandTimeoutSec, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label5, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.textBoxThreadCount, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBoxPassword, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label6, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.textBoxUserId, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(9, 22);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 26F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 26F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 26F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 26F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(553, 102);
            this.tableLayoutPanel1.TabIndex = 33;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.label1.Location = new System.Drawing.Point(46, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 15);
            this.label1.TabIndex = 23;
            this.label1.Text = "Thread Count";
            // 
            // textBoxThinkTimeSec
            // 
            this.textBoxThinkTimeSec.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxThinkTimeSec.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.textBoxThinkTimeSec.Location = new System.Drawing.Point(371, 78);
            this.textBoxThinkTimeSec.Name = "textBoxThinkTimeSec";
            this.textBoxThinkTimeSec.Size = new System.Drawing.Size(179, 23);
            this.textBoxThinkTimeSec.TabIndex = 32;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.label7.Location = new System.Drawing.Point(415, 60);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(91, 15);
            this.label7.TabIndex = 31;
            this.label7.Text = "ThinkTimeSec";
            // 
            // textBoxConnectionTimeoutSec
            // 
            this.textBoxConnectionTimeoutSec.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxConnectionTimeoutSec.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.textBoxConnectionTimeoutSec.Location = new System.Drawing.Point(187, 78);
            this.textBoxConnectionTimeoutSec.Name = "textBoxConnectionTimeoutSec";
            this.textBoxConnectionTimeoutSec.Size = new System.Drawing.Size(178, 23);
            this.textBoxConnectionTimeoutSec.TabIndex = 27;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.label4.Location = new System.Drawing.Point(251, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 15);
            this.label4.TabIndex = 26;
            this.label4.Text = "UserId";
            // 
            // textBoxCommandTimeoutSec
            // 
            this.textBoxCommandTimeoutSec.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxCommandTimeoutSec.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.textBoxCommandTimeoutSec.Location = new System.Drawing.Point(3, 78);
            this.textBoxCommandTimeoutSec.Name = "textBoxCommandTimeoutSec";
            this.textBoxCommandTimeoutSec.Size = new System.Drawing.Size(178, 23);
            this.textBoxCommandTimeoutSec.TabIndex = 21;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.label5.Location = new System.Drawing.Point(429, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 15);
            this.label5.TabIndex = 28;
            this.label5.Text = "Password";
            // 
            // textBoxThreadCount
            // 
            this.textBoxThreadCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxThreadCount.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.textBoxThreadCount.Location = new System.Drawing.Point(3, 28);
            this.textBoxThreadCount.Name = "textBoxThreadCount";
            this.textBoxThreadCount.Size = new System.Drawing.Size(178, 23);
            this.textBoxThreadCount.TabIndex = 24;
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxPassword.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.textBoxPassword.Location = new System.Drawing.Point(371, 28);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(179, 23);
            this.textBoxPassword.TabIndex = 29;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.label6.Location = new System.Drawing.Point(202, 60);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(147, 15);
            this.label6.TabIndex = 30;
            this.label6.Text = "ConnectionTimeoutSec";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.label3.Location = new System.Drawing.Point(29, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(126, 15);
            this.label3.TabIndex = 22;
            this.label3.Text = "CommandTimeoutSec";
            // 
            // textBoxUserId
            // 
            this.textBoxUserId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxUserId.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.textBoxUserId.Location = new System.Drawing.Point(187, 28);
            this.textBoxUserId.Name = "textBoxUserId";
            this.textBoxUserId.Size = new System.Drawing.Size(178, 23);
            this.textBoxUserId.TabIndex = 25;
            // 
            // groupBoxScriptTemplate
            // 
            this.groupBoxScriptTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxScriptTemplate.Controls.Add(this.labelColumnDelimiter);
            this.groupBoxScriptTemplate.Controls.Add(this.textBoxColumnDelimiter);
            this.groupBoxScriptTemplate.Controls.Add(this.buttonDeleteTemplate);
            this.groupBoxScriptTemplate.Controls.Add(this.comboBoxScriptTemplates);
            this.groupBoxScriptTemplate.Controls.Add(this.buttonNewTemplate);
            this.groupBoxScriptTemplate.Controls.Add(this.buttonModifyTemplate);
            this.groupBoxScriptTemplate.Location = new System.Drawing.Point(3, 174);
            this.groupBoxScriptTemplate.Name = "groupBoxScriptTemplate";
            this.groupBoxScriptTemplate.Size = new System.Drawing.Size(569, 88);
            this.groupBoxScriptTemplate.TabIndex = 158;
            this.groupBoxScriptTemplate.TabStop = false;
            this.groupBoxScriptTemplate.Text = "Script Template";
            // 
            // labelColumnDelimiter
            // 
            this.labelColumnDelimiter.AutoSize = true;
            this.labelColumnDelimiter.Location = new System.Drawing.Point(13, 56);
            this.labelColumnDelimiter.Name = "labelColumnDelimiter";
            this.labelColumnDelimiter.Size = new System.Drawing.Size(119, 15);
            this.labelColumnDelimiter.TabIndex = 164;
            this.labelColumnDelimiter.Text = "Column Delimiter";
            // 
            // textBoxColumnDelimiter
            // 
            this.textBoxColumnDelimiter.Location = new System.Drawing.Point(134, 52);
            this.textBoxColumnDelimiter.Name = "textBoxColumnDelimiter";
            this.textBoxColumnDelimiter.Size = new System.Drawing.Size(23, 23);
            this.textBoxColumnDelimiter.TabIndex = 163;
            this.textBoxColumnDelimiter.Text = "|";
            // 
            // buttonDeleteTemplate
            // 
            this.buttonDeleteTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDeleteTemplate.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.buttonDeleteTemplate.Location = new System.Drawing.Point(432, 51);
            this.buttonDeleteTemplate.Name = "buttonDeleteTemplate";
            this.buttonDeleteTemplate.Size = new System.Drawing.Size(128, 25);
            this.buttonDeleteTemplate.TabIndex = 162;
            this.buttonDeleteTemplate.Text = "Delete Template";
            this.buttonDeleteTemplate.UseVisualStyleBackColor = true;
            this.buttonDeleteTemplate.Click += new System.EventHandler(this.buttonDeleteTemplate_Click_1);
            // 
            // comboBoxScriptTemplates
            // 
            this.comboBoxScriptTemplates.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxScriptTemplates.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.comboBoxScriptTemplates.FormattingEnabled = true;
            this.comboBoxScriptTemplates.Location = new System.Drawing.Point(11, 22);
            this.comboBoxScriptTemplates.Name = "comboBoxScriptTemplates";
            this.comboBoxScriptTemplates.Size = new System.Drawing.Size(548, 23);
            this.comboBoxScriptTemplates.TabIndex = 159;
            this.comboBoxScriptTemplates.SelectedIndexChanged += new System.EventHandler(this.ComboBoxScriptTemplatesChanged);
            // 
            // buttonNewTemplate
            // 
            this.buttonNewTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonNewTemplate.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.buttonNewTemplate.Location = new System.Drawing.Point(299, 51);
            this.buttonNewTemplate.Name = "buttonNewTemplate";
            this.buttonNewTemplate.Size = new System.Drawing.Size(128, 25);
            this.buttonNewTemplate.TabIndex = 161;
            this.buttonNewTemplate.Text = "New Template";
            this.buttonNewTemplate.UseVisualStyleBackColor = true;
            this.buttonNewTemplate.Click += new System.EventHandler(this.buttonNewTemplate_Click);
            // 
            // buttonModifyTemplate
            // 
            this.buttonModifyTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonModifyTemplate.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.buttonModifyTemplate.Location = new System.Drawing.Point(163, 51);
            this.buttonModifyTemplate.Name = "buttonModifyTemplate";
            this.buttonModifyTemplate.Size = new System.Drawing.Size(130, 25);
            this.buttonModifyTemplate.TabIndex = 160;
            this.buttonModifyTemplate.Text = "Modify Template";
            this.buttonModifyTemplate.UseVisualStyleBackColor = true;
            this.buttonModifyTemplate.Click += new System.EventHandler(this.buttonModifyTemplate_Click);
            // 
            // groupBoxLoadBalancer
            // 
            this.groupBoxLoadBalancer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxLoadBalancer.Controls.Add(this.panelGroupAndQuery);
            this.groupBoxLoadBalancer.Location = new System.Drawing.Point(3, 3);
            this.groupBoxLoadBalancer.Name = "groupBoxLoadBalancer";
            this.groupBoxLoadBalancer.Size = new System.Drawing.Size(937, 656);
            this.groupBoxLoadBalancer.TabIndex = 1;
            this.groupBoxLoadBalancer.TabStop = false;
            this.groupBoxLoadBalancer.Text = "Tools > Executer Multi Server Sql";
            // 
            // UcExecuterMultiSql
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.groupBoxLoadBalancer);
            this.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.Name = "UcExecuterMultiSql";
            this.Size = new System.Drawing.Size(943, 662);
            this.Load += new System.EventHandler(this.LoadData);
            this.panelGroupAndQuery.ResumeLayout(false);
            this.splitContainerServerGroup.Panel1.ResumeLayout(false);
            this.splitContainerServerGroup.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerServerGroup)).EndInit();
            this.splitContainerServerGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvServerList)).EndInit();
            this.groupBoxSelectHaGroup.ResumeLayout(false);
            this.groupBoxScriptAndResults.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fastColoredTextBoxTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fastColoredTextBoxResult)).EndInit();
            this.groupBoxConnectionInfo.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBoxScriptTemplate.ResumeLayout(false);
            this.groupBoxScriptTemplate.PerformLayout();
            this.groupBoxLoadBalancer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelGroupAndQuery;
        private System.Windows.Forms.SplitContainer splitContainerServerGroup;
        private System.Windows.Forms.DataGridView dgvServerList;
        private System.Windows.Forms.GroupBox groupBoxConnectionInfo;
        private System.Windows.Forms.GroupBox groupBoxSelectHaGroup;
        private System.Windows.Forms.ComboBox comboBoxSelectServerGroup;
        private System.Windows.Forms.Button buttonReload;
        private System.Windows.Forms.GroupBox groupBoxScriptTemplate;
        private System.Windows.Forms.Button buttonDeleteTemplate;
        private System.Windows.Forms.ComboBox comboBoxScriptTemplates;
        private System.Windows.Forms.Button buttonNewTemplate;
        private System.Windows.Forms.Button buttonModifyTemplate;
        private System.Windows.Forms.GroupBox groupBoxLoadBalancer;
        private System.Windows.Forms.GroupBox groupBoxScriptAndResults;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private FastColoredTextBoxNS.FastColoredTextBox fastColoredTextBoxTemplate;
        private FastColoredTextBoxNS.FastColoredTextBox fastColoredTextBoxResult;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonConnectionTestSelectedServer;
        private System.Windows.Forms.Button buttonExecute;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxThinkTimeSec;
        private System.Windows.Forms.TextBox textBoxConnectionTimeoutSec;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.TextBox textBoxUserId;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxThreadCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxCommandTimeoutSec;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label labelColumnDelimiter;
        private System.Windows.Forms.TextBox textBoxColumnDelimiter;
    }
}
