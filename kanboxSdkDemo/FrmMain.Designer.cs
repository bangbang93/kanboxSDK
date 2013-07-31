namespace kanboxSDKDemo
{
    partial class FrmMain
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.labUsage = new System.Windows.Forms.Label();
            this.prsUsage = new System.Windows.Forms.ProgressBar();
            this.labUser = new System.Windows.Forms.Label();
            this.btnUpload = new System.Windows.Forms.Button();
            this.listFile = new System.Windows.Forms.ListView();
            this.btnDownload = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnUpFolder = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.labUsage);
            this.splitContainer1.Panel1.Controls.Add(this.prsUsage);
            this.splitContainer1.Panel1.Controls.Add(this.labUser);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnUpFolder);
            this.splitContainer1.Panel2.Controls.Add(this.btnDelete);
            this.splitContainer1.Panel2.Controls.Add(this.btnDownload);
            this.splitContainer1.Panel2.Controls.Add(this.listFile);
            this.splitContainer1.Panel2.Controls.Add(this.btnUpload);
            this.splitContainer1.Size = new System.Drawing.Size(821, 364);
            this.splitContainer1.SplitterDistance = 33;
            this.splitContainer1.TabIndex = 0;
            // 
            // labUsage
            // 
            this.labUsage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labUsage.AutoSize = true;
            this.labUsage.Location = new System.Drawing.Point(733, 12);
            this.labUsage.Name = "labUsage";
            this.labUsage.Size = new System.Drawing.Size(53, 12);
            this.labUsage.TabIndex = 2;
            this.labUsage.Text = "labUsage";
            // 
            // prsUsage
            // 
            this.prsUsage.Enabled = false;
            this.prsUsage.Location = new System.Drawing.Point(581, 9);
            this.prsUsage.Name = "prsUsage";
            this.prsUsage.Size = new System.Drawing.Size(146, 18);
            this.prsUsage.TabIndex = 1;
            // 
            // labUser
            // 
            this.labUser.AutoSize = true;
            this.labUser.Location = new System.Drawing.Point(12, 12);
            this.labUser.Name = "labUser";
            this.labUser.Size = new System.Drawing.Size(47, 12);
            this.labUser.TabIndex = 0;
            this.labUser.Text = "labUser";
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(14, 263);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(114, 52);
            this.btnUpload.TabIndex = 1;
            this.btnUpload.Text = "上传";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // listFile
            // 
            this.listFile.Location = new System.Drawing.Point(3, 3);
            this.listFile.Name = "listFile";
            this.listFile.Size = new System.Drawing.Size(815, 254);
            this.listFile.TabIndex = 2;
            this.listFile.UseCompatibleStateImageBehavior = false;
            this.listFile.View = System.Windows.Forms.View.Details;
            this.listFile.DoubleClick += new System.EventHandler(this.listFile_DoubleClick);
            // 
            // btnDownload
            // 
            this.btnDownload.Location = new System.Drawing.Point(160, 264);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(114, 52);
            this.btnDownload.TabIndex = 3;
            this.btnDownload.Text = "下载";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(299, 264);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(114, 52);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnUpFolder
            // 
            this.btnUpFolder.Location = new System.Drawing.Point(440, 264);
            this.btnUpFolder.Name = "btnUpFolder";
            this.btnUpFolder.Size = new System.Drawing.Size(114, 52);
            this.btnUpFolder.TabIndex = 5;
            this.btnUpFolder.Text = "向上";
            this.btnUpFolder.UseVisualStyleBackColor = true;
            this.btnUpFolder.Click += new System.EventHandler(this.btnUpFolder_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(821, 364);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FrmMain";
            this.Text = "kanboxSDKDemo";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label labUser;
        private System.Windows.Forms.Label labUsage;
        private System.Windows.Forms.ProgressBar prsUsage;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.ListView listFile;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnUpFolder;
    }
}