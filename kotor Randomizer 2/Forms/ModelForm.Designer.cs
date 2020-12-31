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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModelForm));
            this.cbCharRando = new System.Windows.Forms.CheckBox();
            this.cbDoorRando = new System.Windows.Forms.CheckBox();
            this.cbPlaceRando = new System.Windows.Forms.CheckBox();
            this.pCharRando = new System.Windows.Forms.Panel();
            this.cbBrokenChars = new System.Windows.Forms.CheckBox();
            this.cbLargeChars = new System.Windows.Forms.CheckBox();
            this.pPlaceRando = new System.Windows.Forms.Panel();
            this.cbBrokenPlace = new System.Windows.Forms.CheckBox();
            this.cbLargePlace = new System.Windows.Forms.CheckBox();
            this.pDoorRando = new System.Windows.Forms.Panel();
            this.cbBrokenDoor = new System.Windows.Forms.CheckBox();
            this.cbLargeDoor = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pCharRando.SuspendLayout();
            this.pPlaceRando.SuspendLayout();
            this.pDoorRando.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbCharRando
            // 
            this.cbCharRando.Location = new System.Drawing.Point(20, 30);
            this.cbCharRando.Name = "cbCharRando";
            this.cbCharRando.Size = new System.Drawing.Size(110, 20);
            this.cbCharRando.TabIndex = 0;
            this.cbCharRando.Text = "Character Models";
            this.cbCharRando.UseVisualStyleBackColor = true;
            this.cbCharRando.CheckedChanged += new System.EventHandler(this.cbCharRando_CheckedChanged);
            // 
            // cbDoorRando
            // 
            this.cbDoorRando.Location = new System.Drawing.Point(20, 130);
            this.cbDoorRando.Name = "cbDoorRando";
            this.cbDoorRando.Size = new System.Drawing.Size(110, 20);
            this.cbDoorRando.TabIndex = 1;
            this.cbDoorRando.Text = "Door Models";
            this.cbDoorRando.UseVisualStyleBackColor = true;
            this.cbDoorRando.CheckedChanged += new System.EventHandler(this.cbDoorRando_CheckedChanged);
            // 
            // cbPlaceRando
            // 
            this.cbPlaceRando.Location = new System.Drawing.Point(20, 80);
            this.cbPlaceRando.Name = "cbPlaceRando";
            this.cbPlaceRando.Size = new System.Drawing.Size(110, 20);
            this.cbPlaceRando.TabIndex = 2;
            this.cbPlaceRando.Text = "Placeable Models";
            this.cbPlaceRando.UseVisualStyleBackColor = true;
            this.cbPlaceRando.CheckedChanged += new System.EventHandler(this.cbPlaceRando_CheckedChanged);
            // 
            // pCharRando
            // 
            this.pCharRando.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pCharRando.Controls.Add(this.cbBrokenChars);
            this.pCharRando.Controls.Add(this.cbLargeChars);
            this.pCharRando.Enabled = false;
            this.pCharRando.Location = new System.Drawing.Point(140, 20);
            this.pCharRando.Name = "pCharRando";
            this.pCharRando.Size = new System.Drawing.Size(270, 40);
            this.pCharRando.TabIndex = 3;
            // 
            // cbBrokenChars
            // 
            this.cbBrokenChars.Location = new System.Drawing.Point(140, 10);
            this.cbBrokenChars.Name = "cbBrokenChars";
            this.cbBrokenChars.Size = new System.Drawing.Size(130, 20);
            this.cbBrokenChars.TabIndex = 1;
            this.cbBrokenChars.Text = "Omit Broken Models";
            this.cbBrokenChars.UseVisualStyleBackColor = true;
            this.cbBrokenChars.CheckedChanged += new System.EventHandler(this.cbBrokenChars_CheckedChanged);
            // 
            // cbLargeChars
            // 
            this.cbLargeChars.Location = new System.Drawing.Point(10, 10);
            this.cbLargeChars.Name = "cbLargeChars";
            this.cbLargeChars.Size = new System.Drawing.Size(120, 20);
            this.cbLargeChars.TabIndex = 0;
            this.cbLargeChars.Text = "Omit Large Models";
            this.cbLargeChars.UseVisualStyleBackColor = true;
            this.cbLargeChars.CheckedChanged += new System.EventHandler(this.cbLargeChars_CheckedChanged);
            // 
            // pPlaceRando
            // 
            this.pPlaceRando.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pPlaceRando.Controls.Add(this.cbBrokenPlace);
            this.pPlaceRando.Controls.Add(this.cbLargePlace);
            this.pPlaceRando.Enabled = false;
            this.pPlaceRando.Location = new System.Drawing.Point(140, 70);
            this.pPlaceRando.Name = "pPlaceRando";
            this.pPlaceRando.Size = new System.Drawing.Size(270, 40);
            this.pPlaceRando.TabIndex = 4;
            // 
            // cbBrokenPlace
            // 
            this.cbBrokenPlace.Location = new System.Drawing.Point(140, 10);
            this.cbBrokenPlace.Name = "cbBrokenPlace";
            this.cbBrokenPlace.Size = new System.Drawing.Size(130, 20);
            this.cbBrokenPlace.TabIndex = 2;
            this.cbBrokenPlace.Text = "Omit Broken Models";
            this.cbBrokenPlace.UseVisualStyleBackColor = true;
            this.cbBrokenPlace.CheckedChanged += new System.EventHandler(this.cbBrokenPlace_CheckedChanged);
            // 
            // cbLargePlace
            // 
            this.cbLargePlace.Location = new System.Drawing.Point(10, 10);
            this.cbLargePlace.Name = "cbLargePlace";
            this.cbLargePlace.Size = new System.Drawing.Size(120, 20);
            this.cbLargePlace.TabIndex = 1;
            this.cbLargePlace.Text = "Omit Large Models";
            this.cbLargePlace.UseVisualStyleBackColor = true;
            this.cbLargePlace.CheckedChanged += new System.EventHandler(this.cbLargePlace_CheckedChanged);
            // 
            // pDoorRando
            // 
            this.pDoorRando.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pDoorRando.Controls.Add(this.cbBrokenDoor);
            this.pDoorRando.Controls.Add(this.cbLargeDoor);
            this.pDoorRando.Enabled = false;
            this.pDoorRando.Location = new System.Drawing.Point(140, 120);
            this.pDoorRando.Name = "pDoorRando";
            this.pDoorRando.Size = new System.Drawing.Size(270, 40);
            this.pDoorRando.TabIndex = 5;
            // 
            // cbBrokenDoor
            // 
            this.cbBrokenDoor.Location = new System.Drawing.Point(140, 10);
            this.cbBrokenDoor.Name = "cbBrokenDoor";
            this.cbBrokenDoor.Size = new System.Drawing.Size(130, 20);
            this.cbBrokenDoor.TabIndex = 3;
            this.cbBrokenDoor.Text = "Omit Broken Models";
            this.cbBrokenDoor.UseVisualStyleBackColor = true;
            this.cbBrokenDoor.CheckedChanged += new System.EventHandler(this.cbBrokenDoor_CheckedChanged);
            // 
            // cbLargeDoor
            // 
            this.cbLargeDoor.Location = new System.Drawing.Point(10, 10);
            this.cbLargeDoor.Name = "cbLargeDoor";
            this.cbLargeDoor.Size = new System.Drawing.Size(120, 20);
            this.cbLargeDoor.TabIndex = 2;
            this.cbLargeDoor.Text = "Omit Airlocks";
            this.cbLargeDoor.UseVisualStyleBackColor = true;
            this.cbLargeDoor.CheckedChanged += new System.EventHandler(this.cbLargeDoor_CheckedChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(20, 170);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(390, 70);
            this.label1.TabIndex = 6;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // ModelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(22)))));
            this.ClientSize = new System.Drawing.Size(432, 261);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pDoorRando);
            this.Controls.Add(this.pPlaceRando);
            this.Controls.Add(this.pCharRando);
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
            this.pCharRando.ResumeLayout(false);
            this.pPlaceRando.ResumeLayout(false);
            this.pDoorRando.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox cbCharRando;
        private System.Windows.Forms.CheckBox cbDoorRando;
        private System.Windows.Forms.CheckBox cbPlaceRando;
        private System.Windows.Forms.Panel pCharRando;
        private System.Windows.Forms.Panel pPlaceRando;
        private System.Windows.Forms.Panel pDoorRando;
        private System.Windows.Forms.CheckBox cbBrokenChars;
        private System.Windows.Forms.CheckBox cbLargeChars;
        private System.Windows.Forms.CheckBox cbBrokenPlace;
        private System.Windows.Forms.CheckBox cbLargePlace;
        private System.Windows.Forms.CheckBox cbBrokenDoor;
        private System.Windows.Forms.CheckBox cbLargeDoor;
        private System.Windows.Forms.Label label1;
    }
}