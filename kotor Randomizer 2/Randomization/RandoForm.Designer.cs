namespace kotor_Randomizer_2
{
    partial class RandoForm
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
            this.RandomizationProgress = new System.Windows.Forms.ProgressBar();
            this.currentRandoTask_label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // RandomizationProgress
            // 
            this.RandomizationProgress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(22)))));
            this.RandomizationProgress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.RandomizationProgress.Location = new System.Drawing.Point(20, 50);
            this.RandomizationProgress.Name = "RandomizationProgress";
            this.RandomizationProgress.Size = new System.Drawing.Size(480, 30);
            this.RandomizationProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.RandomizationProgress.TabIndex = 0;
            this.RandomizationProgress.Click += new System.EventHandler(this.RandomizationProgress_Click);
            // 
            // currentRandoTask_label
            // 
            this.currentRandoTask_label.Font = new System.Drawing.Font("Unispace", 9.749999F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.currentRandoTask_label.Location = new System.Drawing.Point(20, 20);
            this.currentRandoTask_label.Name = "currentRandoTask_label";
            this.currentRandoTask_label.Size = new System.Drawing.Size(480, 20);
            this.currentRandoTask_label.TabIndex = 1;
            this.currentRandoTask_label.Text = "Current Task";
            this.currentRandoTask_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // RandoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(22)))));
            this.ClientSize = new System.Drawing.Size(521, 101);
            this.Controls.Add(this.currentRandoTask_label);
            this.Controls.Add(this.RandomizationProgress);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.Name = "RandoForm";
            this.Text = "RandoForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar RandomizationProgress;
        private System.Windows.Forms.Label currentRandoTask_label;
    }
}