namespace kotor_Randomizer_2
{
    partial class PresetForm
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
            this.ofdPresets = new System.Windows.Forms.OpenFileDialog();
            this.sfdPresets = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // ofdPresets
            // 
            this.ofdPresets.DefaultExt = "krp";
            this.ofdPresets.Filter = "Randomizer File|*.krp|All files|*.*";
            this.ofdPresets.Title = "Load Preset";
            // 
            // sfdPresets
            // 
            this.sfdPresets.DefaultExt = "krp";
            this.sfdPresets.Filter = "Randomizer File|*.krp|All files|*.*";
            this.sfdPresets.Title = "Save Preset";
            // 
            // PresetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(22)))));
            this.ClientSize = new System.Drawing.Size(583, 463);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "PresetForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "User Presets";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog ofdPresets;
        private System.Windows.Forms.SaveFileDialog sfdPresets;
    }
}