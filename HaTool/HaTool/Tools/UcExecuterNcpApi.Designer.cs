namespace HaTool.Tools
{
    partial class UcExecuterNcpApi
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UcExecuterNcpApi));
            this.groupBoxExecuterNcpApi = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.fastColoredTextBoxTemplate = new FastColoredTextBoxNS.FastColoredTextBox();
            this.fastColoredTextBoxResult = new FastColoredTextBoxNS.FastColoredTextBox();
            this.groupBoxApiActionTemplate = new System.Windows.Forms.GroupBox();
            this.buttonDeleteTemplate = new System.Windows.Forms.Button();
            this.comboBoxScriptTemplates = new System.Windows.Forms.ComboBox();
            this.buttonNewTemplate = new System.Windows.Forms.Button();
            this.buttonExecute = new System.Windows.Forms.Button();
            this.buttonModifyTemplate = new System.Windows.Forms.Button();
            this.groupBoxConnectionInfo = new System.Windows.Forms.GroupBox();
            this.labelSecretKey = new System.Windows.Forms.Label();
            this.labelAccessKey = new System.Windows.Forms.Label();
            this.textBoxSecretKey = new System.Windows.Forms.MaskedTextBox();
            this.textBoxAccessKey = new System.Windows.Forms.TextBox();
            this.labelEndpoint = new System.Windows.Forms.Label();
            this.textBoxEndPoint = new System.Windows.Forms.TextBox();
            this.groupBoxExecuterNcpApi.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fastColoredTextBoxTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fastColoredTextBoxResult)).BeginInit();
            this.groupBoxApiActionTemplate.SuspendLayout();
            this.groupBoxConnectionInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxExecuterNcpApi
            // 
            this.groupBoxExecuterNcpApi.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxExecuterNcpApi.Controls.Add(this.groupBox4);
            this.groupBoxExecuterNcpApi.Controls.Add(this.groupBoxApiActionTemplate);
            this.groupBoxExecuterNcpApi.Controls.Add(this.groupBoxConnectionInfo);
            this.groupBoxExecuterNcpApi.Location = new System.Drawing.Point(3, 3);
            this.groupBoxExecuterNcpApi.Name = "groupBoxExecuterNcpApi";
            this.groupBoxExecuterNcpApi.Size = new System.Drawing.Size(894, 694);
            this.groupBoxExecuterNcpApi.TabIndex = 1;
            this.groupBoxExecuterNcpApi.TabStop = false;
            this.groupBoxExecuterNcpApi.Text = "Tools > Executer NCP API";
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.splitContainer1);
            this.groupBox4.Location = new System.Drawing.Point(22, 239);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(849, 449);
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
            this.splitContainer1.Size = new System.Drawing.Size(843, 427);
            this.splitContainer1.SplitterDistance = 214;
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
            this.fastColoredTextBoxTemplate.AutoIndentCharsPatterns = "\r\n^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;]+);\r\n";
            this.fastColoredTextBoxTemplate.AutoScrollMinSize = new System.Drawing.Size(25, 15);
            this.fastColoredTextBoxTemplate.BackBrush = null;
            this.fastColoredTextBoxTemplate.BracketsHighlightStrategy = FastColoredTextBoxNS.BracketsHighlightStrategy.Strategy2;
            this.fastColoredTextBoxTemplate.CharHeight = 15;
            this.fastColoredTextBoxTemplate.CharWidth = 7;
            this.fastColoredTextBoxTemplate.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.fastColoredTextBoxTemplate.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.fastColoredTextBoxTemplate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fastColoredTextBoxTemplate.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.fastColoredTextBoxTemplate.IsReplaceMode = false;
            this.fastColoredTextBoxTemplate.Language = FastColoredTextBoxNS.Language.JS;
            this.fastColoredTextBoxTemplate.LeftBracket = '(';
            this.fastColoredTextBoxTemplate.LeftBracket2 = '{';
            this.fastColoredTextBoxTemplate.Location = new System.Drawing.Point(0, 0);
            this.fastColoredTextBoxTemplate.Name = "fastColoredTextBoxTemplate";
            this.fastColoredTextBoxTemplate.Paddings = new System.Windows.Forms.Padding(0);
            this.fastColoredTextBoxTemplate.RightBracket = ')';
            this.fastColoredTextBoxTemplate.RightBracket2 = '}';
            this.fastColoredTextBoxTemplate.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.fastColoredTextBoxTemplate.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("fastColoredTextBoxTemplate.ServiceColors")));
            this.fastColoredTextBoxTemplate.Size = new System.Drawing.Size(843, 214);
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
            this.fastColoredTextBoxResult.AutoIndentCharsPatterns = "\r\n^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;]+);\r\n";
            this.fastColoredTextBoxResult.AutoScrollMinSize = new System.Drawing.Size(25, 15);
            this.fastColoredTextBoxResult.BackBrush = null;
            this.fastColoredTextBoxResult.BracketsHighlightStrategy = FastColoredTextBoxNS.BracketsHighlightStrategy.Strategy2;
            this.fastColoredTextBoxResult.CharHeight = 15;
            this.fastColoredTextBoxResult.CharWidth = 7;
            this.fastColoredTextBoxResult.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.fastColoredTextBoxResult.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.fastColoredTextBoxResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fastColoredTextBoxResult.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.fastColoredTextBoxResult.IsReplaceMode = false;
            this.fastColoredTextBoxResult.Language = FastColoredTextBoxNS.Language.JS;
            this.fastColoredTextBoxResult.LeftBracket = '(';
            this.fastColoredTextBoxResult.LeftBracket2 = '{';
            this.fastColoredTextBoxResult.Location = new System.Drawing.Point(0, 0);
            this.fastColoredTextBoxResult.Name = "fastColoredTextBoxResult";
            this.fastColoredTextBoxResult.Paddings = new System.Windows.Forms.Padding(0);
            this.fastColoredTextBoxResult.RightBracket = ')';
            this.fastColoredTextBoxResult.RightBracket2 = '}';
            this.fastColoredTextBoxResult.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.fastColoredTextBoxResult.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("fastColoredTextBoxResult.ServiceColors")));
            this.fastColoredTextBoxResult.Size = new System.Drawing.Size(843, 209);
            this.fastColoredTextBoxResult.TabIndex = 7;
            this.fastColoredTextBoxResult.Zoom = 100;
            // 
            // groupBoxApiActionTemplate
            // 
            this.groupBoxApiActionTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxApiActionTemplate.Controls.Add(this.buttonDeleteTemplate);
            this.groupBoxApiActionTemplate.Controls.Add(this.comboBoxScriptTemplates);
            this.groupBoxApiActionTemplate.Controls.Add(this.buttonNewTemplate);
            this.groupBoxApiActionTemplate.Controls.Add(this.buttonExecute);
            this.groupBoxApiActionTemplate.Controls.Add(this.buttonModifyTemplate);
            this.groupBoxApiActionTemplate.Location = new System.Drawing.Point(22, 145);
            this.groupBoxApiActionTemplate.Name = "groupBoxApiActionTemplate";
            this.groupBoxApiActionTemplate.Size = new System.Drawing.Size(849, 88);
            this.groupBoxApiActionTemplate.TabIndex = 158;
            this.groupBoxApiActionTemplate.TabStop = false;
            this.groupBoxApiActionTemplate.Text = "API Action Template";
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
            this.groupBoxConnectionInfo.Controls.Add(this.labelSecretKey);
            this.groupBoxConnectionInfo.Controls.Add(this.labelAccessKey);
            this.groupBoxConnectionInfo.Controls.Add(this.textBoxSecretKey);
            this.groupBoxConnectionInfo.Controls.Add(this.textBoxAccessKey);
            this.groupBoxConnectionInfo.Controls.Add(this.labelEndpoint);
            this.groupBoxConnectionInfo.Controls.Add(this.textBoxEndPoint);
            this.groupBoxConnectionInfo.Location = new System.Drawing.Point(22, 22);
            this.groupBoxConnectionInfo.Name = "groupBoxConnectionInfo";
            this.groupBoxConnectionInfo.Size = new System.Drawing.Size(849, 117);
            this.groupBoxConnectionInfo.TabIndex = 157;
            this.groupBoxConnectionInfo.TabStop = false;
            this.groupBoxConnectionInfo.Text = "Connection Info";
            // 
            // labelSecretKey
            // 
            this.labelSecretKey.AutoSize = true;
            this.labelSecretKey.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.labelSecretKey.Location = new System.Drawing.Point(181, 68);
            this.labelSecretKey.Name = "labelSecretKey";
            this.labelSecretKey.Size = new System.Drawing.Size(70, 15);
            this.labelSecretKey.TabIndex = 166;
            this.labelSecretKey.Text = "SecretKey";
            // 
            // labelAccessKey
            // 
            this.labelAccessKey.AutoSize = true;
            this.labelAccessKey.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.labelAccessKey.Location = new System.Drawing.Point(18, 68);
            this.labelAccessKey.Name = "labelAccessKey";
            this.labelAccessKey.Size = new System.Drawing.Size(70, 15);
            this.labelAccessKey.TabIndex = 165;
            this.labelAccessKey.Text = "AccessKey";
            // 
            // textBoxSecretKey
            // 
            this.textBoxSecretKey.Location = new System.Drawing.Point(181, 86);
            this.textBoxSecretKey.Name = "textBoxSecretKey";
            this.textBoxSecretKey.PasswordChar = '*';
            this.textBoxSecretKey.Size = new System.Drawing.Size(345, 23);
            this.textBoxSecretKey.TabIndex = 164;
            // 
            // textBoxAccessKey
            // 
            this.textBoxAccessKey.Location = new System.Drawing.Point(14, 86);
            this.textBoxAccessKey.Name = "textBoxAccessKey";
            this.textBoxAccessKey.Size = new System.Drawing.Size(161, 23);
            this.textBoxAccessKey.TabIndex = 163;
            // 
            // labelEndpoint
            // 
            this.labelEndpoint.AutoSize = true;
            this.labelEndpoint.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.labelEndpoint.Location = new System.Drawing.Point(18, 24);
            this.labelEndpoint.Name = "labelEndpoint";
            this.labelEndpoint.Size = new System.Drawing.Size(63, 15);
            this.labelEndpoint.TabIndex = 140;
            this.labelEndpoint.Text = "Endpoint";
            // 
            // textBoxEndPoint
            // 
            this.textBoxEndPoint.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.textBoxEndPoint.Location = new System.Drawing.Point(14, 42);
            this.textBoxEndPoint.Name = "textBoxEndPoint";
            this.textBoxEndPoint.Size = new System.Drawing.Size(512, 23);
            this.textBoxEndPoint.TabIndex = 146;
            // 
            // UcExecuterNcpApi
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.groupBoxExecuterNcpApi);
            this.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.Name = "UcExecuterNcpApi";
            this.Size = new System.Drawing.Size(900, 700);
            this.Load += new System.EventHandler(this.LoadData);
            this.groupBoxExecuterNcpApi.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fastColoredTextBoxTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fastColoredTextBoxResult)).EndInit();
            this.groupBoxApiActionTemplate.ResumeLayout(false);
            this.groupBoxConnectionInfo.ResumeLayout(false);
            this.groupBoxConnectionInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBoxExecuterNcpApi;
        private System.Windows.Forms.GroupBox groupBoxApiActionTemplate;
        private System.Windows.Forms.Button buttonDeleteTemplate;
        private System.Windows.Forms.ComboBox comboBoxScriptTemplates;
        private System.Windows.Forms.Button buttonNewTemplate;
        private System.Windows.Forms.Button buttonExecute;
        private System.Windows.Forms.Button buttonModifyTemplate;
        private System.Windows.Forms.GroupBox groupBoxConnectionInfo;
        private System.Windows.Forms.Label labelEndpoint;
        private System.Windows.Forms.TextBox textBoxEndPoint;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private FastColoredTextBoxNS.FastColoredTextBox fastColoredTextBoxTemplate;
        private FastColoredTextBoxNS.FastColoredTextBox fastColoredTextBoxResult;
        private System.Windows.Forms.Label labelSecretKey;
        private System.Windows.Forms.Label labelAccessKey;
        private System.Windows.Forms.MaskedTextBox textBoxSecretKey;
        private System.Windows.Forms.TextBox textBoxAccessKey;
    }
}
