namespace kanboxSDKDemo
{
    partial class FrmUpload
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
            this.prs = new System.Windows.Forms.ProgressBar();
            this.labSpeed = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // prs
            // 
            this.prs.Location = new System.Drawing.Point(44, 26);
            this.prs.Name = "prs";
            this.prs.Size = new System.Drawing.Size(777, 23);
            this.prs.TabIndex = 0;
            // 
            // labSpeed
            // 
            this.labSpeed.AutoSize = true;
            this.labSpeed.Location = new System.Drawing.Point(44, 72);
            this.labSpeed.Name = "labSpeed";
            this.labSpeed.Size = new System.Drawing.Size(53, 12);
            this.labSpeed.TabIndex = 1;
            this.labSpeed.Text = "labSpeed";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FrmUpload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(879, 117);
            this.Controls.Add(this.labSpeed);
            this.Controls.Add(this.prs);
            this.Name = "FrmUpload";
            this.Text = "FrmUpload";
            this.Shown += new System.EventHandler(this.FrmUpload_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar prs;
        private System.Windows.Forms.Label labSpeed;
        private System.Windows.Forms.Timer timer1;
    }
}