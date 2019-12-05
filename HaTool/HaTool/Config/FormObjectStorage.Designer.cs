namespace HaTool.Config
{
    partial class FormObjectStorage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormObjectStorage));
            this.groupBoxOuter = new System.Windows.Forms.GroupBox();
            this.buttonBucketTest = new System.Windows.Forms.Button();
            this.labelBucketName = new System.Windows.Forms.Label();
            this.textBoxBucketName = new System.Windows.Forms.TextBox();
            this.labelObjectStorageEndpoint = new System.Windows.Forms.Label();
            this.textBoxObjectStorageEndPoint = new System.Windows.Forms.TextBox();
            this.buttonCreateBucket = new System.Windows.Forms.Button();
            this.labelSecretKey = new System.Windows.Forms.Label();
            this.labelAccessKey = new System.Windows.Forms.Label();
            this.textBoxSecretKey = new System.Windows.Forms.TextBox();
            this.textBoxAccessKey = new System.Windows.Forms.TextBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.textBoxComment = new System.Windows.Forms.TextBox();
            this.groupBoxOuter.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxOuter
            // 
            this.groupBoxOuter.Controls.Add(this.buttonBucketTest);
            this.groupBoxOuter.Controls.Add(this.labelBucketName);
            this.groupBoxOuter.Controls.Add(this.textBoxBucketName);
            this.groupBoxOuter.Controls.Add(this.labelObjectStorageEndpoint);
            this.groupBoxOuter.Controls.Add(this.textBoxObjectStorageEndPoint);
            this.groupBoxOuter.Controls.Add(this.buttonCreateBucket);
            this.groupBoxOuter.Controls.Add(this.labelSecretKey);
            this.groupBoxOuter.Controls.Add(this.labelAccessKey);
            this.groupBoxOuter.Controls.Add(this.textBoxSecretKey);
            this.groupBoxOuter.Controls.Add(this.textBoxAccessKey);
            this.groupBoxOuter.Location = new System.Drawing.Point(7, 7);
            this.groupBoxOuter.Name = "groupBoxOuter";
            this.groupBoxOuter.Size = new System.Drawing.Size(470, 284);
            this.groupBoxOuter.TabIndex = 0;
            this.groupBoxOuter.TabStop = false;
            this.groupBoxOuter.Text = "Config > Object Storage Setting";
            // 
            // buttonBucketTest
            // 
            this.buttonBucketTest.Location = new System.Drawing.Point(164, 251);
            this.buttonBucketTest.Name = "buttonBucketTest";
            this.buttonBucketTest.Size = new System.Drawing.Size(139, 23);
            this.buttonBucketTest.TabIndex = 14;
            this.buttonBucketTest.Text = "Bucket Test";
            this.buttonBucketTest.UseVisualStyleBackColor = true;
            this.buttonBucketTest.Click += new System.EventHandler(this.buttonBucketTest_Click);
            // 
            // labelBucketName
            // 
            this.labelBucketName.AutoSize = true;
            this.labelBucketName.Location = new System.Drawing.Point(21, 201);
            this.labelBucketName.Name = "labelBucketName";
            this.labelBucketName.Size = new System.Drawing.Size(84, 15);
            this.labelBucketName.TabIndex = 13;
            this.labelBucketName.Text = "Bucket Name";
            // 
            // textBoxBucketName
            // 
            this.textBoxBucketName.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBoxBucketName.Location = new System.Drawing.Point(18, 222);
            this.textBoxBucketName.Name = "textBoxBucketName";
            this.textBoxBucketName.Size = new System.Drawing.Size(434, 23);
            this.textBoxBucketName.TabIndex = 12;
            // 
            // labelObjectStorageEndpoint
            // 
            this.labelObjectStorageEndpoint.AutoSize = true;
            this.labelObjectStorageEndpoint.Location = new System.Drawing.Point(20, 33);
            this.labelObjectStorageEndpoint.Name = "labelObjectStorageEndpoint";
            this.labelObjectStorageEndpoint.Size = new System.Drawing.Size(168, 15);
            this.labelObjectStorageEndpoint.TabIndex = 11;
            this.labelObjectStorageEndpoint.Text = "Object Storage Endpoint";
            // 
            // textBoxObjectStorageEndPoint
            // 
            this.textBoxObjectStorageEndPoint.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBoxObjectStorageEndPoint.Location = new System.Drawing.Point(17, 54);
            this.textBoxObjectStorageEndPoint.Name = "textBoxObjectStorageEndPoint";
            this.textBoxObjectStorageEndPoint.Size = new System.Drawing.Size(434, 23);
            this.textBoxObjectStorageEndPoint.TabIndex = 10;
            // 
            // buttonCreateBucket
            // 
            this.buttonCreateBucket.Location = new System.Drawing.Point(313, 251);
            this.buttonCreateBucket.Name = "buttonCreateBucket";
            this.buttonCreateBucket.Size = new System.Drawing.Size(139, 23);
            this.buttonCreateBucket.TabIndex = 9;
            this.buttonCreateBucket.Text = "Create Bucket";
            this.buttonCreateBucket.UseVisualStyleBackColor = true;
            this.buttonCreateBucket.Click += new System.EventHandler(this.buttonCreateBucket_Click);
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
            this.textBoxSecretKey.Size = new System.Drawing.Size(434, 23);
            this.textBoxSecretKey.TabIndex = 3;
            this.textBoxSecretKey.UseSystemPasswordChar = true;
            // 
            // textBoxAccessKey
            // 
            this.textBoxAccessKey.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBoxAccessKey.Location = new System.Drawing.Point(17, 113);
            this.textBoxAccessKey.Name = "textBoxAccessKey";
            this.textBoxAccessKey.Size = new System.Drawing.Size(434, 23);
            this.textBoxAccessKey.TabIndex = 2;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(171, 368);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(139, 23);
            this.buttonSave.TabIndex = 8;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(319, 368);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(139, 23);
            this.buttonCancel.TabIndex = 7;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // textBoxComment
            // 
            this.textBoxComment.Location = new System.Drawing.Point(24, 297);
            this.textBoxComment.Multiline = true;
            this.textBoxComment.Name = "textBoxComment";
            this.textBoxComment.Size = new System.Drawing.Size(435, 67);
            this.textBoxComment.TabIndex = 9;
            this.textBoxComment.Text = "Object storage is used as server information and database backup temporary storag" +
    "e. Setting up NCP object storage to use HATool is required, and if not set, subs" +
    "equent processes will not proceed.\r\n";
            // 
            // FormObjectStorage
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(484, 400);
            this.Controls.Add(this.textBoxComment);
            this.Controls.Add(this.groupBoxOuter);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonCancel);
            this.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormObjectStorage";
            this.Text = "Object Storage";
            this.Load += new System.EventHandler(this.LoadData);
            this.groupBoxOuter.ResumeLayout(false);
            this.groupBoxOuter.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxOuter;
        private System.Windows.Forms.TextBox textBoxSecretKey;
        private System.Windows.Forms.TextBox textBoxAccessKey;
        private System.Windows.Forms.Label labelSecretKey;
        private System.Windows.Forms.Label labelAccessKey;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonCreateBucket;
        private System.Windows.Forms.Label labelBucketName;
        private System.Windows.Forms.TextBox textBoxBucketName;
        private System.Windows.Forms.Label labelObjectStorageEndpoint;
        private System.Windows.Forms.TextBox textBoxObjectStorageEndPoint;
        private System.Windows.Forms.Button buttonBucketTest;
        private System.Windows.Forms.TextBox textBoxComment;
    }
}