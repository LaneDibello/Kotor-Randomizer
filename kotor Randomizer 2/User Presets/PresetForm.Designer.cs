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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PresetForm));
            this.ofdPresets = new System.Windows.Forms.OpenFileDialog();
            this.sfdPresets = new System.Windows.Forms.SaveFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.lbPresetPaths = new System.Windows.Forms.ListBox();
            this.bload = new System.Windows.Forms.Button();
            this.bsave = new System.Windows.Forms.Button();
            this.cbIncModu = new System.Windows.Forms.CheckBox();
            this.cbIncItem = new System.Windows.Forms.CheckBox();
            this.cbIncSound = new System.Windows.Forms.CheckBox();
            this.cbIncModel = new System.Windows.Forms.CheckBox();
            this.cbInc2da = new System.Windows.Forms.CheckBox();
            this.cbIncText = new System.Windows.Forms.CheckBox();
            this.cbIncTexture = new System.Windows.Forms.CheckBox();
            this.cbIncOther = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
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
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Unispace", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.label1.Location = new System.Drawing.Point(20, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 30);
            this.label1.TabIndex = 1;
            this.label1.Text = "Loaded Presets:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbPresetPaths
            // 
            this.lbPresetPaths.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(22)))));
            this.lbPresetPaths.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbPresetPaths.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.lbPresetPaths.FormattingEnabled = true;
            this.lbPresetPaths.Location = new System.Drawing.Point(20, 60);
            this.lbPresetPaths.Name = "lbPresetPaths";
            this.lbPresetPaths.Size = new System.Drawing.Size(250, 340);
            this.lbPresetPaths.TabIndex = 18;
            this.lbPresetPaths.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lbPresetPaths_KeyPress);
            this.lbPresetPaths.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lbPresetPaths_MouseDoubleClick);
            // 
            // bload
            // 
            this.bload.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.bload.FlatAppearance.BorderSize = 2;
            this.bload.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.bload.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(22)))));
            this.bload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bload.Font = new System.Drawing.Font("Unispace", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bload.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.bload.Location = new System.Drawing.Point(180, 20);
            this.bload.Name = "bload";
            this.bload.Size = new System.Drawing.Size(90, 30);
            this.bload.TabIndex = 19;
            this.bload.Text = "Browse...";
            this.bload.UseVisualStyleBackColor = false;
            this.bload.Click += new System.EventHandler(this.bload_Click);
            // 
            // bsave
            // 
            this.bsave.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.bsave.FlatAppearance.BorderSize = 2;
            this.bsave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.bsave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(22)))));
            this.bsave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bsave.Font = new System.Drawing.Font("Unispace", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bsave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.bsave.Location = new System.Drawing.Point(280, 20);
            this.bsave.Name = "bsave";
            this.bsave.Size = new System.Drawing.Size(220, 30);
            this.bsave.TabIndex = 20;
            this.bsave.Text = "Save Preset";
            this.bsave.UseVisualStyleBackColor = false;
            this.bsave.Click += new System.EventHandler(this.bsave_Click);
            // 
            // cbIncModu
            // 
            this.cbIncModu.Checked = true;
            this.cbIncModu.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbIncModu.Location = new System.Drawing.Point(20, 20);
            this.cbIncModu.Name = "cbIncModu";
            this.cbIncModu.Size = new System.Drawing.Size(180, 30);
            this.cbIncModu.TabIndex = 0;
            this.cbIncModu.Text = "Include Modules";
            this.cbIncModu.UseVisualStyleBackColor = true;
            // 
            // cbIncItem
            // 
            this.cbIncItem.Checked = true;
            this.cbIncItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbIncItem.Location = new System.Drawing.Point(20, 60);
            this.cbIncItem.Name = "cbIncItem";
            this.cbIncItem.Size = new System.Drawing.Size(180, 30);
            this.cbIncItem.TabIndex = 1;
            this.cbIncItem.Text = "Include Items";
            this.cbIncItem.UseVisualStyleBackColor = true;
            // 
            // cbIncSound
            // 
            this.cbIncSound.Checked = true;
            this.cbIncSound.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbIncSound.Location = new System.Drawing.Point(20, 100);
            this.cbIncSound.Name = "cbIncSound";
            this.cbIncSound.Size = new System.Drawing.Size(180, 30);
            this.cbIncSound.TabIndex = 2;
            this.cbIncSound.Text = "Include Music and Sounds";
            this.cbIncSound.UseVisualStyleBackColor = true;
            // 
            // cbIncModel
            // 
            this.cbIncModel.Checked = true;
            this.cbIncModel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbIncModel.Location = new System.Drawing.Point(20, 140);
            this.cbIncModel.Name = "cbIncModel";
            this.cbIncModel.Size = new System.Drawing.Size(180, 30);
            this.cbIncModel.TabIndex = 3;
            this.cbIncModel.Text = "Include Models";
            this.cbIncModel.UseVisualStyleBackColor = true;
            // 
            // cbInc2da
            // 
            this.cbInc2da.Checked = true;
            this.cbInc2da.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbInc2da.Location = new System.Drawing.Point(20, 220);
            this.cbInc2da.Name = "cbInc2da";
            this.cbInc2da.Size = new System.Drawing.Size(180, 30);
            this.cbInc2da.TabIndex = 4;
            this.cbInc2da.Text = "Include  2DAs";
            this.cbInc2da.UseVisualStyleBackColor = true;
            // 
            // cbIncText
            // 
            this.cbIncText.Checked = true;
            this.cbIncText.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbIncText.Location = new System.Drawing.Point(20, 260);
            this.cbIncText.Name = "cbIncText";
            this.cbIncText.Size = new System.Drawing.Size(180, 30);
            this.cbIncText.TabIndex = 5;
            this.cbIncText.Text = "Include Text";
            this.cbIncText.UseVisualStyleBackColor = true;
            // 
            // cbIncTexture
            // 
            this.cbIncTexture.Checked = true;
            this.cbIncTexture.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbIncTexture.Location = new System.Drawing.Point(20, 180);
            this.cbIncTexture.Name = "cbIncTexture";
            this.cbIncTexture.Size = new System.Drawing.Size(180, 30);
            this.cbIncTexture.TabIndex = 6;
            this.cbIncTexture.Text = "Include Textures";
            this.cbIncTexture.UseVisualStyleBackColor = true;
            // 
            // cbIncOther
            // 
            this.cbIncOther.Checked = true;
            this.cbIncOther.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbIncOther.Location = new System.Drawing.Point(20, 300);
            this.cbIncOther.Name = "cbIncOther";
            this.cbIncOther.Size = new System.Drawing.Size(180, 30);
            this.cbIncOther.TabIndex = 7;
            this.cbIncOther.Text = "Include Other";
            this.cbIncOther.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.cbIncOther);
            this.panel1.Controls.Add(this.cbIncTexture);
            this.panel1.Controls.Add(this.cbIncText);
            this.panel1.Controls.Add(this.cbInc2da);
            this.panel1.Controls.Add(this.cbIncModel);
            this.panel1.Controls.Add(this.cbIncSound);
            this.panel1.Controls.Add(this.cbIncItem);
            this.panel1.Controls.Add(this.cbIncModu);
            this.panel1.Location = new System.Drawing.Point(280, 60);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(220, 340);
            this.panel1.TabIndex = 21;
            // 
            // PresetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(22)))));
            this.ClientSize = new System.Drawing.Size(521, 421);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.bsave);
            this.Controls.Add(this.bload);
            this.Controls.Add(this.lbPresetPaths);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "PresetForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "User Presets";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog ofdPresets;
        private System.Windows.Forms.SaveFileDialog sfdPresets;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lbPresetPaths;
        private System.Windows.Forms.Button bload;
        private System.Windows.Forms.Button bsave;
        private System.Windows.Forms.CheckBox cbIncModu;
        private System.Windows.Forms.CheckBox cbIncItem;
        private System.Windows.Forms.CheckBox cbIncSound;
        private System.Windows.Forms.CheckBox cbIncModel;
        private System.Windows.Forms.CheckBox cbInc2da;
        private System.Windows.Forms.CheckBox cbIncText;
        private System.Windows.Forms.CheckBox cbIncTexture;
        private System.Windows.Forms.CheckBox cbIncOther;
        private System.Windows.Forms.Panel panel1;
    }
}