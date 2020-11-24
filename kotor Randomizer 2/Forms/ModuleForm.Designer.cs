namespace kotor_Randomizer_2
{
    partial class ModuleForm
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
            this.OmittedListBox = new System.Windows.Forms.ListBox();
            this.RandomizedListBox = new System.Windows.Forms.ListBox();
            this.PresetComboBox = new System.Windows.Forms.ComboBox();
            this.cbDeleteMilestones = new System.Windows.Forms.CheckBox();
            this.lblPresets = new System.Windows.Forms.Label();
            this.cbSaveAllMods = new System.Windows.Forms.CheckBox();
            this.cbSaveMiniGame = new System.Windows.Forms.CheckBox();
            this.lblRandomized = new System.Windows.Forms.Label();
            this.lblOmitted = new System.Windows.Forms.Label();
            this.cbFixDream = new System.Windows.Forms.CheckBox();
            this.cbUnlockGalaxyMap = new System.Windows.Forms.CheckBox();
            this.cbFixCoordinates = new System.Windows.Forms.CheckBox();
            this.cbFixMindPrison = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // OmittedListBox
            // 
            this.OmittedListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(22)))));
            this.OmittedListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.OmittedListBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.OmittedListBox.FormattingEnabled = true;
            this.OmittedListBox.Location = new System.Drawing.Point(280, 50);
            this.OmittedListBox.Name = "OmittedListBox";
            this.OmittedListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.OmittedListBox.Size = new System.Drawing.Size(240, 379);
            this.OmittedListBox.TabIndex = 17;
            this.OmittedListBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OmittedListBox_KeyPress);
            this.OmittedListBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.OmittedListBox_MouseDoubleClick);
            // 
            // RandomizedListBox
            // 
            this.RandomizedListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(22)))));
            this.RandomizedListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RandomizedListBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.RandomizedListBox.FormattingEnabled = true;
            this.RandomizedListBox.Location = new System.Drawing.Point(20, 50);
            this.RandomizedListBox.Name = "RandomizedListBox";
            this.RandomizedListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.RandomizedListBox.Size = new System.Drawing.Size(240, 379);
            this.RandomizedListBox.TabIndex = 16;
            this.RandomizedListBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.RandomizedListBox_KeyPress);
            this.RandomizedListBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.RandomizedListBox_MouseDoubleClick);
            // 
            // PresetComboBox
            // 
            this.PresetComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(22)))));
            this.PresetComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PresetComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PresetComboBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.PresetComboBox.FormattingEnabled = true;
            this.PresetComboBox.Location = new System.Drawing.Point(20, 460);
            this.PresetComboBox.Name = "PresetComboBox";
            this.PresetComboBox.Size = new System.Drawing.Size(500, 21);
            this.PresetComboBox.TabIndex = 18;
            this.PresetComboBox.SelectedIndexChanged += new System.EventHandler(this.PresetComboBox_SelectedIndexChanged);
            // 
            // cbDeleteMilestones
            // 
            this.cbDeleteMilestones.AutoSize = true;
            this.cbDeleteMilestones.Checked = true;
            this.cbDeleteMilestones.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbDeleteMilestones.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.cbDeleteMilestones.Location = new System.Drawing.Point(20, 490);
            this.cbDeleteMilestones.Name = "cbDeleteMilestones";
            this.cbDeleteMilestones.Size = new System.Drawing.Size(159, 17);
            this.cbDeleteMilestones.TabIndex = 19;
            this.cbDeleteMilestones.Text = "Delete Milestone Save Data";
            this.cbDeleteMilestones.UseVisualStyleBackColor = true;
            this.cbDeleteMilestones.CheckedChanged += new System.EventHandler(this.cbDeleteMilestones_CheckedChanged);
            // 
            // lblPresets
            // 
            this.lblPresets.AutoSize = true;
            this.lblPresets.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPresets.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.lblPresets.Location = new System.Drawing.Point(20, 440);
            this.lblPresets.Name = "lblPresets";
            this.lblPresets.Size = new System.Drawing.Size(53, 13);
            this.lblPresets.TabIndex = 20;
            this.lblPresets.Text = "Presets:";
            // 
            // cbSaveAllMods
            // 
            this.cbSaveAllMods.AutoSize = true;
            this.cbSaveAllMods.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.cbSaveAllMods.Location = new System.Drawing.Point(340, 490);
            this.cbSaveAllMods.Name = "cbSaveAllMods";
            this.cbSaveAllMods.Size = new System.Drawing.Size(157, 17);
            this.cbSaveAllMods.TabIndex = 21;
            this.cbSaveAllMods.Text = "Include All Modules in Save";
            this.cbSaveAllMods.UseVisualStyleBackColor = true;
            this.cbSaveAllMods.CheckedChanged += new System.EventHandler(this.cbSaveAllMods_CheckedChanged);
            // 
            // cbSaveMiniGame
            // 
            this.cbSaveMiniGame.AutoSize = true;
            this.cbSaveMiniGame.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.cbSaveMiniGame.Location = new System.Drawing.Point(180, 490);
            this.cbSaveMiniGame.Name = "cbSaveMiniGame";
            this.cbSaveMiniGame.Size = new System.Drawing.Size(153, 17);
            this.cbSaveMiniGame.TabIndex = 22;
            this.cbSaveMiniGame.Text = "Include Minigames in Save";
            this.cbSaveMiniGame.UseVisualStyleBackColor = true;
            this.cbSaveMiniGame.CheckedChanged += new System.EventHandler(this.cbSaveMiniGame_CheckedChanged);
            // 
            // lblRandomized
            // 
            this.lblRandomized.AutoSize = true;
            this.lblRandomized.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRandomized.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.lblRandomized.Location = new System.Drawing.Point(20, 20);
            this.lblRandomized.Name = "lblRandomized";
            this.lblRandomized.Size = new System.Drawing.Size(95, 16);
            this.lblRandomized.TabIndex = 23;
            this.lblRandomized.Text = "Randomized";
            this.lblRandomized.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblRandomized.Click += new System.EventHandler(this.lblRandomized_Click);
            // 
            // lblOmitted
            // 
            this.lblOmitted.AutoSize = true;
            this.lblOmitted.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOmitted.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.lblOmitted.Location = new System.Drawing.Point(460, 20);
            this.lblOmitted.Name = "lblOmitted";
            this.lblOmitted.Size = new System.Drawing.Size(61, 16);
            this.lblOmitted.TabIndex = 24;
            this.lblOmitted.Text = "Omitted";
            this.lblOmitted.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbFixDream
            // 
            this.cbFixDream.AutoSize = true;
            this.cbFixDream.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.cbFixDream.Location = new System.Drawing.Point(20, 510);
            this.cbFixDream.Name = "cbFixDream";
            this.cbFixDream.Size = new System.Drawing.Size(125, 17);
            this.cbFixDream.TabIndex = 25;
            this.cbFixDream.Text = "Fix Dream Sequence";
            this.cbFixDream.UseVisualStyleBackColor = true;
            this.cbFixDream.CheckedChanged += new System.EventHandler(this.cbFixDream_CheckedChanged);
            // 
            // cbUnlockGalaxyMap
            // 
            this.cbUnlockGalaxyMap.AutoSize = true;
            this.cbUnlockGalaxyMap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.cbUnlockGalaxyMap.Location = new System.Drawing.Point(180, 510);
            this.cbUnlockGalaxyMap.Name = "cbUnlockGalaxyMap";
            this.cbUnlockGalaxyMap.Size = new System.Drawing.Size(119, 17);
            this.cbUnlockGalaxyMap.TabIndex = 26;
            this.cbUnlockGalaxyMap.Text = "Unlock Galaxy Map";
            this.cbUnlockGalaxyMap.UseVisualStyleBackColor = true;
            this.cbUnlockGalaxyMap.CheckedChanged += new System.EventHandler(this.cbUnlockGalaxyMap_CheckedChanged);
            // 
            // cbFixCoordinates
            // 
            this.cbFixCoordinates.AutoSize = true;
            this.cbFixCoordinates.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.cbFixCoordinates.Location = new System.Drawing.Point(340, 510);
            this.cbFixCoordinates.Name = "cbFixCoordinates";
            this.cbFixCoordinates.Size = new System.Drawing.Size(136, 17);
            this.cbFixCoordinates.TabIndex = 27;
            this.cbFixCoordinates.Text = "Fix Module Coordinates";
            this.cbFixCoordinates.UseVisualStyleBackColor = true;
            this.cbFixCoordinates.CheckedChanged += new System.EventHandler(this.cbFixCoordinates_CheckedChanged);
            // 
            // cbFixMindPrison
            // 
            this.cbFixMindPrison.AutoSize = true;
            this.cbFixMindPrison.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.cbFixMindPrison.Location = new System.Drawing.Point(20, 530);
            this.cbFixMindPrison.Name = "cbFixMindPrison";
            this.cbFixMindPrison.Size = new System.Drawing.Size(97, 17);
            this.cbFixMindPrison.TabIndex = 28;
            this.cbFixMindPrison.Text = "Fix Mind Prison";
            this.cbFixMindPrison.UseVisualStyleBackColor = true;
            this.cbFixMindPrison.CheckedChanged += new System.EventHandler(this.cbFixMindPrison_CheckedChanged);
            // 
            // ModuleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(22)))));
            this.ClientSize = new System.Drawing.Size(541, 572);
            this.Controls.Add(this.cbFixMindPrison);
            this.Controls.Add(this.cbFixCoordinates);
            this.Controls.Add(this.cbUnlockGalaxyMap);
            this.Controls.Add(this.cbFixDream);
            this.Controls.Add(this.lblOmitted);
            this.Controls.Add(this.lblRandomized);
            this.Controls.Add(this.cbSaveMiniGame);
            this.Controls.Add(this.cbSaveAllMods);
            this.Controls.Add(this.lblPresets);
            this.Controls.Add(this.cbDeleteMilestones);
            this.Controls.Add(this.PresetComboBox);
            this.Controls.Add(this.OmittedListBox);
            this.Controls.Add(this.RandomizedListBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ModuleForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Modules";
            this.Activated += new System.EventHandler(this.ModuleForm_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ModuleForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListBox OmittedListBox;
        private System.Windows.Forms.ListBox RandomizedListBox;
        private System.Windows.Forms.ComboBox PresetComboBox;
        private System.Windows.Forms.CheckBox cbDeleteMilestones;
        private System.Windows.Forms.Label lblPresets;
        private System.Windows.Forms.CheckBox cbSaveAllMods;
        private System.Windows.Forms.CheckBox cbSaveMiniGame;
        private System.Windows.Forms.Label lblRandomized;
        private System.Windows.Forms.Label lblOmitted;
        private System.Windows.Forms.CheckBox cbFixDream;
        private System.Windows.Forms.CheckBox cbUnlockGalaxyMap;
        private System.Windows.Forms.CheckBox cbFixCoordinates;
        private System.Windows.Forms.CheckBox cbFixMindPrison;
    }
}