namespace kanboxSDKDemo
{
    partial class FrmDownload
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
            this.labSpeed = new System.Windows.Forms.Label();
            this.prs = new System.Windows.Forms.ProgressBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // labSpeed
            // 
            this.labSpeed.AutoSize = true;
            this.labSpeed.Location = new System.Drawing.Point(40, 58);
            this.labSpeed.Name = "labSpeed";
            this.labSpeed.Size = new System.Drawing.Size(53, 12);
            this.labSpeed.TabIndex = 3;
            this.labSpeed.Text = "labSpeed";
            // 
            // prs
            // 
            this.prs.Location = new System.Drawing.Point(40, 12);
            this.prs.Name = "prs";
            this.prs.Size = new System.Drawing.Size(777, 23);
            this.prs.TabIndex = 2;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FrmDownload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(846, 84);
            this.Controls.Add(this.labSpeed);
            this.Controls.Add(this.prs);
            this.Name = "FrmDownload";
            this.Text = "FrmDownload";
            this.Shown += new System.EventHandler(this.FrmDownload_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labSpeed;
        private System.Windows.Forms.ProgressBar prs;
        private System.Windows.Forms.Timer timer1;
    }
}