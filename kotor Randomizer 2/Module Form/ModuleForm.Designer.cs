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
            this.SuspendLayout();
            // 
            // OmittedListBox
            // 
            this.OmittedListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(22)))));
            this.OmittedListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.OmittedListBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.OmittedListBox.FormattingEnabled = true;
            this.OmittedListBox.Location = new System.Drawing.Point(280, 20);
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
            this.RandomizedListBox.Location = new System.Drawing.Point(20, 20);
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
            this.PresetComboBox.Location = new System.Drawing.Point(20, 430);
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
            this.modDelete_checkbox.Location = new System.Drawing.Point(20, 460);
            this.modDelete_checkbox.Name = "modDelete_checkbox";
            this.modDelete_checkbox.Size = new System.Drawing.Size(159, 17);
            this.modDelete_checkbox.TabIndex = 19;
            this.modDelete_checkbox.Text = "Delete Milestone Save Data";
            this.modDelete_checkbox.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Unispace", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.label1.Location = new System.Drawing.Point(20, 410);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 14);
            this.label1.TabIndex = 20;
            this.label1.Text = "Presets:";
            // 
            // allSave_checkbox
            // 
            this.allSave_checkbox.AutoSize = true;
            this.allSave_checkbox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.allSave_checkbox.Location = new System.Drawing.Point(340, 460);
            this.allSave_checkbox.Name = "allSave_checkbox";
            this.allSave_checkbox.Size = new System.Drawing.Size(157, 17);
            this.allSave_checkbox.TabIndex = 21;
            this.allSave_checkbox.Text = "Include All Modules in Save";
            this.allSave_checkbox.UseVisualStyleBackColor = true;
            // 
            // mgSave_checkbox
            // 
            this.mgSave_checkbox.AutoSize = true;
            this.mgSave_checkbox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.mgSave_checkbox.Location = new System.Drawing.Point(180, 460);
            this.mgSave_checkbox.Name = "mgSave_checkbox";
            this.mgSave_checkbox.Size = new System.Drawing.Size(153, 17);
            this.mgSave_checkbox.TabIndex = 22;
            this.mgSave_checkbox.Text = "Include Minigames in Save";
            this.mgSave_checkbox.UseVisualStyleBackColor = true;
            // 
            // ModuleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(22)))));
            this.ClientSize = new System.Drawing.Size(541, 490);
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
    }
}