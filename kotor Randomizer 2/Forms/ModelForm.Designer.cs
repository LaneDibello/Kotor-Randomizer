namespace kotor_Randomizer_2
{
    partial class ModelForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModelForm));
            this.cbCharRando = new System.Windows.Forms.CheckBox();
            this.cbDoorRando = new System.Windows.Forms.CheckBox();
            this.cbPlaceRando = new System.Windows.Forms.CheckBox();
            this.cbBrokenChars = new System.Windows.Forms.CheckBox();
            this.cbLargeChars = new System.Windows.Forms.CheckBox();
            this.cbBrokenPlace = new System.Windows.Forms.CheckBox();
            this.cbLargePlace = new System.Windows.Forms.CheckBox();
            this.cbBrokenDoor = new System.Windows.Forms.CheckBox();
            this.cbLargeDoor = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.flpCharRando = new System.Windows.Forms.FlowLayoutPanel();
            this.flpPlaceRando = new System.Windows.Forms.FlowLayoutPanel();
            this.cbFloorPanels = new System.Windows.Forms.CheckBox();
            this.flpDoorRando = new System.Windows.Forms.FlowLayoutPanel();
            this.ModelToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.flpCharRando.SuspendLayout();
            this.flpPlaceRando.SuspendLayout();
            this.flpDoorRando.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbCharRando
            // 
            this.cbCharRando.AutoSize = true;
            this.cbCharRando.Location = new System.Drawing.Point(20, 20);
            this.cbCharRando.Name = "cbCharRando";
            this.cbCharRando.Size = new System.Drawing.Size(109, 17);
            this.cbCharRando.TabIndex = 0;
            this.cbCharRando.Text = "Character Models";
            this.ModelToolTip.SetToolTip(this.cbCharRando, "Character models include all of the NPCs and MOBs that\r\nare not spawned by templa" +
        "te (such as party members,\r\nthe player, and a few odd creatures).");
            this.cbCharRando.UseVisualStyleBackColor = true;
            this.cbCharRando.CheckedChanged += new System.EventHandler(this.cbCharRando_CheckedChanged);
            // 
            // cbDoorRando
            // 
            this.cbDoorRando.AutoSize = true;
            this.cbDoorRando.Location = new System.Drawing.Point(20, 161);
            this.cbDoorRando.Name = "cbDoorRando";
            this.cbDoorRando.Size = new System.Drawing.Size(86, 17);
            this.cbDoorRando.TabIndex = 1;
            this.cbDoorRando.Text = "Door Models";
            this.ModelToolTip.SetToolTip(this.cbDoorRando, "Door models include all door objects such as regular doors,\r\nforce-fields, and ev" +
        "en sith sarcophagi.");
            this.cbDoorRando.UseVisualStyleBackColor = true;
            this.cbDoorRando.CheckedChanged += new System.EventHandler(this.cbDoorRando_CheckedChanged);
            // 
            // cbPlaceRando
            // 
            this.cbPlaceRando.AutoSize = true;
            this.cbPlaceRando.Location = new System.Drawing.Point(20, 79);
            this.cbPlaceRando.Name = "cbPlaceRando";
            this.cbPlaceRando.Size = new System.Drawing.Size(110, 17);
            this.cbPlaceRando.TabIndex = 2;
            this.cbPlaceRando.Text = "Placeable Models";
            this.ModelToolTip.SetToolTip(this.cbPlaceRando, "Placeable models include all placeable objects such as\r\nfootlockers, computer con" +
        "soles, or marker-posts.");
            this.cbPlaceRando.UseVisualStyleBackColor = true;
            this.cbPlaceRando.CheckedChanged += new System.EventHandler(this.cbPlaceRando_CheckedChanged);
            // 
            // cbBrokenChars
            // 
            this.cbBrokenChars.AutoSize = true;
            this.cbBrokenChars.Location = new System.Drawing.Point(3, 26);
            this.cbBrokenChars.Name = "cbBrokenChars";
            this.cbBrokenChars.Size = new System.Drawing.Size(121, 17);
            this.cbBrokenChars.TabIndex = 1;
            this.cbBrokenChars.Text = "Omit Broken Models";
            this.ModelToolTip.SetToolTip(this.cbBrokenChars, "Prevents characters from being randomized to entirely\r\ninvisible or testing model" +
        "s.");
            this.cbBrokenChars.UseVisualStyleBackColor = true;
            this.cbBrokenChars.CheckedChanged += new System.EventHandler(this.cbBrokenChars_CheckedChanged);
            // 
            // cbLargeChars
            // 
            this.cbLargeChars.AutoSize = true;
            this.cbLargeChars.Location = new System.Drawing.Point(3, 3);
            this.cbLargeChars.Name = "cbLargeChars";
            this.cbLargeChars.Size = new System.Drawing.Size(114, 17);
            this.cbLargeChars.TabIndex = 0;
            this.cbLargeChars.Text = "Omit Large Models";
            this.ModelToolTip.SetToolTip(this.cbLargeChars, "Prevents characters from being randomized to some of the\r\nhuge models, such as Kr" +
        "ayt Dragon or Rancor.");
            this.cbLargeChars.UseVisualStyleBackColor = true;
            this.cbLargeChars.CheckedChanged += new System.EventHandler(this.cbLargeChars_CheckedChanged);
            // 
            // cbBrokenPlace
            // 
            this.cbBrokenPlace.AutoSize = true;
            this.cbBrokenPlace.Location = new System.Drawing.Point(3, 26);
            this.cbBrokenPlace.Name = "cbBrokenPlace";
            this.cbBrokenPlace.Size = new System.Drawing.Size(121, 17);
            this.cbBrokenPlace.TabIndex = 2;
            this.cbBrokenPlace.Text = "Omit Broken Models";
            this.ModelToolTip.SetToolTip(this.cbBrokenPlace, "Prevents placeables from being randomized to entirely\r\ninvisible or testing model" +
        "s.");
            this.cbBrokenPlace.UseVisualStyleBackColor = true;
            this.cbBrokenPlace.CheckedChanged += new System.EventHandler(this.cbBrokenPlace_CheckedChanged);
            // 
            // cbLargePlace
            // 
            this.cbLargePlace.AutoSize = true;
            this.cbLargePlace.Location = new System.Drawing.Point(3, 3);
            this.cbLargePlace.Name = "cbLargePlace";
            this.cbLargePlace.Size = new System.Drawing.Size(114, 17);
            this.cbLargePlace.TabIndex = 1;
            this.cbLargePlace.Text = "Omit Large Models";
            this.ModelToolTip.SetToolTip(this.cbLargePlace, "Prevents placeables from being randomized to some of the\r\nhuge models, such as th" +
        "e invisible acid wall or some land-\r\nspeeders.");
            this.cbLargePlace.UseVisualStyleBackColor = true;
            this.cbLargePlace.CheckedChanged += new System.EventHandler(this.cbLargePlace_CheckedChanged);
            // 
            // cbBrokenDoor
            // 
            this.cbBrokenDoor.AutoSize = true;
            this.cbBrokenDoor.Location = new System.Drawing.Point(3, 26);
            this.cbBrokenDoor.Name = "cbBrokenDoor";
            this.cbBrokenDoor.Size = new System.Drawing.Size(121, 17);
            this.cbBrokenDoor.TabIndex = 3;
            this.cbBrokenDoor.Text = "Omit Broken Models";
            this.ModelToolTip.SetToolTip(this.cbBrokenDoor, "Prevents doors from being randomized to entirely invisible\r\nor testing models.");
            this.cbBrokenDoor.UseVisualStyleBackColor = true;
            this.cbBrokenDoor.CheckedChanged += new System.EventHandler(this.cbBrokenDoor_CheckedChanged);
            // 
            // cbLargeDoor
            // 
            this.cbLargeDoor.AutoSize = true;
            this.cbLargeDoor.Location = new System.Drawing.Point(3, 3);
            this.cbLargeDoor.Name = "cbLargeDoor";
            this.cbLargeDoor.Size = new System.Drawing.Size(87, 17);
            this.cbLargeDoor.TabIndex = 2;
            this.cbLargeDoor.Text = "Omit Airlocks";
            this.ModelToolTip.SetToolTip(this.cbLargeDoor, "Prevents the airlocks in Hrakert Station from being changed,\r\nwhich can sometimes" +
        " cause them not to function.");
            this.cbLargeDoor.UseVisualStyleBackColor = true;
            this.cbLargeDoor.CheckedChanged += new System.EventHandler(this.cbLargeDoor_CheckedChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(280, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 193);
            this.label1.TabIndex = 6;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // flpCharRando
            // 
            this.flpCharRando.AutoSize = true;
            this.flpCharRando.Controls.Add(this.cbLargeChars);
            this.flpCharRando.Controls.Add(this.cbBrokenChars);
            this.flpCharRando.Enabled = false;
            this.flpCharRando.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpCharRando.Location = new System.Drawing.Point(138, 17);
            this.flpCharRando.Name = "flpCharRando";
            this.flpCharRando.Size = new System.Drawing.Size(129, 53);
            this.flpCharRando.TabIndex = 7;
            // 
            // flpPlaceRando
            // 
            this.flpPlaceRando.AutoSize = true;
            this.flpPlaceRando.Controls.Add(this.cbLargePlace);
            this.flpPlaceRando.Controls.Add(this.cbBrokenPlace);
            this.flpPlaceRando.Controls.Add(this.cbFloorPanels);
            this.flpPlaceRando.Enabled = false;
            this.flpPlaceRando.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpPlaceRando.Location = new System.Drawing.Point(138, 76);
            this.flpPlaceRando.Name = "flpPlaceRando";
            this.flpPlaceRando.Size = new System.Drawing.Size(127, 76);
            this.flpPlaceRando.TabIndex = 8;
            // 
            // cbFloorPanels
            // 
            this.cbFloorPanels.AutoSize = true;
            this.cbFloorPanels.Location = new System.Drawing.Point(3, 49);
            this.cbFloorPanels.Name = "cbFloorPanels";
            this.cbFloorPanels.Size = new System.Drawing.Size(110, 17);
            this.cbFloorPanels.TabIndex = 3;
            this.cbFloorPanels.Text = "Easy Floor Panels";
            this.ModelToolTip.SetToolTip(this.cbFloorPanels, "Ensures the floor panels in the Temple Catacombs are not\r\nrandomized to objects t" +
        "hat have collision too large for the\r\npuzzle to be solved.");
            this.cbFloorPanels.UseVisualStyleBackColor = true;
            this.cbFloorPanels.CheckedChanged += new System.EventHandler(this.cbFloorSwitches_CheckedChanged);
            // 
            // flpDoorRando
            // 
            this.flpDoorRando.AutoSize = true;
            this.flpDoorRando.Controls.Add(this.cbLargeDoor);
            this.flpDoorRando.Controls.Add(this.cbBrokenDoor);
            this.flpDoorRando.Enabled = false;
            this.flpDoorRando.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpDoorRando.Location = new System.Drawing.Point(138, 158);
            this.flpDoorRando.Name = "flpDoorRando";
            this.flpDoorRando.Size = new System.Drawing.Size(130, 55);
            this.flpDoorRando.TabIndex = 9;
            // 
            // ModelToolTip
            // 
            this.ModelToolTip.AutoPopDelay = 10000;
            this.ModelToolTip.InitialDelay = 500;
            this.ModelToolTip.ReshowDelay = 100;
            // 
            // ModelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(22)))));
            this.ClientSize = new System.Drawing.Size(444, 230);
            this.Controls.Add(this.flpDoorRando);
            this.Controls.Add(this.flpPlaceRando);
            this.Controls.Add(this.flpCharRando);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbPlaceRando);
            this.Controls.Add(this.cbDoorRando);
            this.Controls.Add(this.cbCharRando);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ModelForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Model";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ModelForm_FormClosed);
            this.flpCharRando.ResumeLayout(false);
            this.flpCharRando.PerformLayout();
            this.flpPlaceRando.ResumeLayout(false);
            this.flpPlaceRando.PerformLayout();
            this.flpDoorRando.ResumeLayout(false);
            this.flpDoorRando.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbCharRando;
        private System.Windows.Forms.CheckBox cbDoorRando;
        private System.Windows.Forms.CheckBox cbPlaceRando;
        private System.Windows.Forms.CheckBox cbBrokenChars;
        private System.Windows.Forms.CheckBox cbLargeChars;
        private System.Windows.Forms.CheckBox cbBrokenPlace;
        private System.Windows.Forms.CheckBox cbLargePlace;
        private System.Windows.Forms.CheckBox cbBrokenDoor;
        private System.Windows.Forms.CheckBox cbLargeDoor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel flpCharRando;
        private System.Windows.Forms.FlowLayoutPanel flpPlaceRando;
        private System.Windows.Forms.FlowLayoutPanel flpDoorRando;
        private System.Windows.Forms.CheckBox cbFloorPanels;
        private System.Windows.Forms.ToolTip ModelToolTip;
    }
}