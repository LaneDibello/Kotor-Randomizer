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
            this.modDelete_checkbox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.allSave_checkbox = new System.Windows.Forms.CheckBox();
            this.mgSave_checkbox = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.FixedDream_checkBox = new System.Windows.Forms.CheckBox();
            this.galmap_checkbox = new System.Windows.Forms.CheckBox();
            this.missionSpawn_checkbox = new System.Windows.Forms.CheckBox();
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
            this.PresetComboBox.Enter += new System.EventHandler(this.PresetComboBox_Enter);
            // 
            // modDelete_checkbox
            // 
            this.modDelete_checkbox.AutoSize = true;
            this.modDelete_checkbox.Checked = true;
            this.modDelete_checkbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.modDelete_checkbox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.modDelete_checkbox.Location = new System.Drawing.Point(20, 490);
            this.modDelete_checkbox.Name = "modDelete_checkbox";
            this.modDelete_checkbox.Size = new System.Drawing.Size(159, 17);
            this.modDelete_checkbox.TabIndex = 19;
            this.modDelete_checkbox.Text = "Delete Milestone Save Data";
            this.modDelete_checkbox.UseVisualStyleBackColor = true;
            this.modDelete_checkbox.CheckedChanged += new System.EventHandler(this.modDelete_checkbox_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Unispace", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.label1.Location = new System.Drawing.Point(20, 440);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 14);
            this.label1.TabIndex = 20;
            this.label1.Text = "Presets:";
            // 
            // allSave_checkbox
            // 
            this.allSave_checkbox.AutoSize = true;
            this.allSave_checkbox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.allSave_checkbox.Location = new System.Drawing.Point(340, 490);
            this.allSave_checkbox.Name = "allSave_checkbox";
            this.allSave_checkbox.Size = new System.Drawing.Size(157, 17);
            this.allSave_checkbox.TabIndex = 21;
            this.allSave_checkbox.Text = "Include All Modules in Save";
            this.allSave_checkbox.UseVisualStyleBackColor = true;
            this.allSave_checkbox.CheckedChanged += new System.EventHandler(this.allSave_checkbox_CheckedChanged);
            // 
            // mgSave_checkbox
            // 
            this.mgSave_checkbox.AutoSize = true;
            this.mgSave_checkbox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.mgSave_checkbox.Location = new System.Drawing.Point(180, 490);
            this.mgSave_checkbox.Name = "mgSave_checkbox";
            this.mgSave_checkbox.Size = new System.Drawing.Size(153, 17);
            this.mgSave_checkbox.TabIndex = 22;
            this.mgSave_checkbox.Text = "Include Minigames in Save";
            this.mgSave_checkbox.UseVisualStyleBackColor = true;
            this.mgSave_checkbox.CheckedChanged += new System.EventHandler(this.mgSave_checkbox_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.label2.Location = new System.Drawing.Point(20, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 16);
            this.label2.TabIndex = 23;
            this.label2.Text = "Randomized";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.label3.Location = new System.Drawing.Point(460, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 16);
            this.label3.TabIndex = 24;
            this.label3.Text = "Omitted";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FixedDream_checkBox
            // 
            this.FixedDream_checkBox.AutoSize = true;
            this.FixedDream_checkBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.FixedDream_checkBox.Location = new System.Drawing.Point(20, 510);
            this.FixedDream_checkBox.Name = "FixedDream_checkBox";
            this.FixedDream_checkBox.Size = new System.Drawing.Size(137, 17);
            this.FixedDream_checkBox.TabIndex = 25;
            this.FixedDream_checkBox.Text = "Fixed Dream Sequence";
            this.FixedDream_checkBox.UseVisualStyleBackColor = true;
            this.FixedDream_checkBox.CheckedChanged += new System.EventHandler(this.FixedDream_checkBox_CheckedChanged);
            // 
            // galmap_checkbox
            // 
            this.galmap_checkbox.AutoSize = true;
            this.galmap_checkbox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.galmap_checkbox.Location = new System.Drawing.Point(180, 510);
            this.galmap_checkbox.Name = "galmap_checkbox";
            this.galmap_checkbox.Size = new System.Drawing.Size(131, 17);
            this.galmap_checkbox.TabIndex = 26;
            this.galmap_checkbox.Text = "Unlocked Galaxy Map";
            this.galmap_checkbox.UseVisualStyleBackColor = true;
            this.galmap_checkbox.CheckedChanged += new System.EventHandler(this.galmap_checkbox_CheckedChanged);
            // 
            // missionSpawn_checkbox
            // 
            this.missionSpawn_checkbox.AutoSize = true;
            this.missionSpawn_checkbox.Enabled = false;
            this.missionSpawn_checkbox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.missionSpawn_checkbox.Location = new System.Drawing.Point(340, 510);
            this.missionSpawn_checkbox.Name = "missionSpawn_checkbox";
            this.missionSpawn_checkbox.Size = new System.Drawing.Size(125, 17);
            this.missionSpawn_checkbox.TabIndex = 27;
            this.missionSpawn_checkbox.Text = "Fixed Mission Spawn";
            this.missionSpawn_checkbox.UseVisualStyleBackColor = true;
            this.missionSpawn_checkbox.CheckedChanged += new System.EventHandler(this.missionSpawn_checkbox_CheckedChanged);
            // 
            // ModuleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(22)))));
            this.ClientSize = new System.Drawing.Size(541, 551);
            this.Controls.Add(this.missionSpawn_checkbox);
            this.Controls.Add(this.galmap_checkbox);
            this.Controls.Add(this.FixedDream_checkBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.mgSave_checkbox);
            this.Controls.Add(this.allSave_checkbox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.modDelete_checkbox);
            this.Controls.Add(this.PresetComboBox);
            this.Controls.Add(this.OmittedListBox);
            this.Controls.Add(this.RandomizedListBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ModuleForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Modules";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ModuleForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListBox OmittedListBox;
        private System.Windows.Forms.ListBox RandomizedListBox;
        private System.Windows.Forms.ComboBox PresetComboBox;
        private System.Windows.Forms.CheckBox modDelete_checkbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox allSave_checkbox;
        private System.Windows.Forms.CheckBox mgSave_checkbox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox FixedDream_checkBox;
        private System.Windows.Forms.CheckBox galmap_checkbox;
        private System.Windows.Forms.CheckBox missionSpawn_checkbox;
    }
}