namespace HaTool
{
    partial class FormLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLogin));
            this.groupBoxLogin = new System.Windows.Forms.GroupBox();
            this.checkBoxSave = new System.Windows.Forms.CheckBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelApiGatewayEndpoint = new System.Windows.Forms.Label();
            this.textBoxApiGatewayEndpoint = new System.Windows.Forms.TextBox();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.labelSecretKey = new System.Windows.Forms.Label();
            this.labelAccessKey = new System.Windows.Forms.Label();
            this.textBoxSecretKey = new System.Windows.Forms.TextBox();
            this.textBoxAccessKey = new System.Windows.Forms.TextBox();
            this.groupBoxLogin.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxLogin
            // 
            this.groupBoxLogin.Controls.Add(this.checkBoxSave);
            this.groupBoxLogin.Controls.Add(this.buttonCancel);
            this.groupBoxLogin.Controls.Add(this.labelApiGatewayEndpoint);
            this.groupBoxLogin.Controls.Add(this.textBoxApiGatewayEndpoint);
            this.groupBoxLogin.Controls.Add(this.buttonLogin);
            this.groupBoxLogin.Controls.Add(this.labelSecretKey);
            this.groupBoxLogin.Controls.Add(this.labelAccessKey);
            this.groupBoxLogin.Controls.Add(this.textBoxSecretKey);
            this.groupBoxLogin.Controls.Add(this.textBoxAccessKey);
            this.groupBoxLogin.Location = new System.Drawing.Point(7, 7);
            this.groupBoxLogin.Name = "groupBoxLogin";
            this.groupBoxLogin.Size = new System.Drawing.Size(470, 252);
            this.groupBoxLogin.TabIndex = 0;
            this.groupBoxLogin.TabStop = false;
            this.groupBoxLogin.Text = "Login";
            // 
            // checkBoxSave
            // 
            this.checkBoxSave.AutoSize = true;
            this.checkBoxSave.Location = new System.Drawing.Point(397, 195);
            this.checkBoxSave.Name = "checkBoxSave";
            this.checkBoxSave.Size = new System.Drawing.Size(54, 19);
            this.checkBoxSave.TabIndex = 15;
            this.checkBoxSave.Text = "Save";
            this.checkBoxSave.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(312, 220);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(139, 23);
            this.buttonCancel.TabIndex = 14;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // labelApiGatewayEndpoint
            // 
            this.labelApiGatewayEndpoint.AutoSize = true;
            this.labelApiGatewayEndpoint.Location = new System.Drawing.Point(20, 33);
            this.labelApiGatewayEndpoint.Name = "labelApiGatewayEndpoint";
            this.labelApiGatewayEndpoint.Size = new System.Drawing.Size(147, 15);
            this.labelApiGatewayEndpoint.TabIndex = 11;
            this.labelApiGatewayEndpoint.Text = "API Gateway Endpoint";
            // 
            // textBoxApiGatewayEndpoint
            // 
            this.textBoxApiGatewayEndpoint.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBoxApiGatewayEndpoint.Location = new System.Drawing.Point(17, 54);
            this.textBoxApiGatewayEndpoint.Name = "textBoxApiGatewayEndpoint";
            this.textBoxApiGatewayEndpoint.Size = new System.Drawing.Size(434, 23);
            this.textBoxApiGatewayEndpoint.TabIndex = 10;
            // 
            // buttonLogin
            // 
            this.buttonLogin.Location = new System.Drawing.Point(167, 220);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(139, 23);
            this.buttonLogin.TabIndex = 9;
            this.buttonLogin.Text = "Login";
            this.buttonLogin.UseVisualStyleBackColor = true;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // labelSecretKey
            // 
            this.labelSecretKey.AutoSize = true;
            this.labelSecretKey.Location = new System.Drawing.Point(20, 145);
            this.labelSecretKey.Name = "labelSecretKey";
            this.labelSecretKey.Size = new System.Drawing.Size(77, 15);
            this.labelSecretKey.TabIndex = 5;
            this.labelSecretKey.Text = "Secret Key";
            // 
            // labelAccessKey
            // 
            this.labelAccessKey.AutoSize = true;
            this.labelAccessKey.Location = new System.Drawing.Point(20, 92);
            this.labelAccessKey.Name = "labelAccessKey";
            this.labelAccessKey.Size = new System.Drawing.Size(77, 15);
            this.labelAccessKey.TabIndex = 4;
            this.labelAccessKey.Text = "Access Key";
            // 
            // textBoxSecretKey
            // 
            this.textBoxSecretKey.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBoxSecretKey.Location = new System.Drawing.Point(17, 166);
            this.textBoxSecretKey.Name = "textBoxSecretKey";
            this.textBoxSecretKey.PasswordChar = '*';
            this.textBoxSecretKey.Size = new System.Drawing.Size(434, 23);
            this.textBoxSecretKey.TabIndex = 3;
            // 
            // textBoxAccessKey
            // 
            this.textBoxAccessKey.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBoxAccessKey.Location = new System.Drawing.Point(17, 113);
            this.textBoxAccessKey.Name = "textBoxAccessKey";
            this.textBoxAccessKey.Size = new System.Drawing.Size(434, 23);
            this.textBoxAccessKey.TabIndex = 2;
            // 
            // FormLogin
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(484, 267);
            this.Controls.Add(this.groupBoxLogin);
            this.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SQL Server DBA Tool";
            this.Load += new System.EventHandler(this.LoadData);
            this.groupBoxLogin.ResumeLayout(false);
            this.groupBoxLogin.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxLogin;
        private System.Windows.Forms.TextBox textBoxSecretKey;
        private System.Windows.Forms.TextBox textBoxAccessKey;
        private System.Windows.Forms.Label labelSecretKey;
        private System.Windows.Forms.Label labelAccessKey;
        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.Label labelApiGatewayEndpoint;
        private System.Windows.Forms.TextBox textBoxApiGatewayEndpoint;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.CheckBox checkBoxSave;
    }
}