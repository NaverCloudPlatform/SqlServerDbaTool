namespace HaTool.Tools
{
    partial class FormServerGroupModify
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormServerGroupModify));
            this.buttonExecute = new System.Windows.Forms.Button();
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.fastColoredTextBoxScript = new FastColoredTextBoxNS.FastColoredTextBox();
            this.groupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fastColoredTextBoxScript)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonExecute
            // 
            this.buttonExecute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExecute.Location = new System.Drawing.Point(320, 421);
            this.buttonExecute.Name = "buttonExecute";
            this.buttonExecute.Size = new System.Drawing.Size(225, 25);
            this.buttonExecute.TabIndex = 90;
            this.buttonExecute.Text = "Save";
            this.buttonExecute.UseVisualStyleBackColor = true;
            this.buttonExecute.Click += new System.EventHandler(this.buttonSaveAndClose_Click);
            // 
            // groupBox
            // 
            this.groupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox.Controls.Add(this.fastColoredTextBoxScript);
            this.groupBox.Location = new System.Drawing.Point(12, 12);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(533, 403);
            this.groupBox.TabIndex = 92;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "Script Preview";
            // 
            // fastColoredTextBoxScript
            // 
            this.fastColoredTextBoxScript.AutoCompleteBracketsList = new char[] {
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
            this.fastColoredTextBoxScript.AutoScrollMinSize = new System.Drawing.Size(27, 14);
            this.fastColoredTextBoxScript.BackBrush = null;
            this.fastColoredTextBoxScript.CharHeight = 14;
            this.fastColoredTextBoxScript.CharWidth = 8;
            this.fastColoredTextBoxScript.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.fastColoredTextBoxScript.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.fastColoredTextBoxScript.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fastColoredTextBoxScript.IsReplaceMode = false;
            this.fastColoredTextBoxScript.Location = new System.Drawing.Point(3, 19);
            this.fastColoredTextBoxScript.Name = "fastColoredTextBoxScript";
            this.fastColoredTextBoxScript.Paddings = new System.Windows.Forms.Padding(0);
            this.fastColoredTextBoxScript.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.fastColoredTextBoxScript.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("fastColoredTextBoxScript.ServiceColors")));
            this.fastColoredTextBoxScript.Size = new System.Drawing.Size(527, 381);
            this.fastColoredTextBoxScript.TabIndex = 63;
            this.fastColoredTextBoxScript.Zoom = 100;
            // 
            // FormServerGroupModify
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(557, 458);
            this.Controls.Add(this.groupBox);
            this.Controls.Add(this.buttonExecute);
            this.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormServerGroupModify";
            this.Text = "Server Group Modify";
            this.groupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fastColoredTextBoxScript)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonExecute;
        private System.Windows.Forms.GroupBox groupBox;
        private FastColoredTextBoxNS.FastColoredTextBox fastColoredTextBoxScript;
    }
}