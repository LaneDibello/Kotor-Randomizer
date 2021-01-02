namespace kotor_Randomizer_2
{
    partial class OtherForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbNameData = new System.Windows.Forms.TextBox();
            this.cbNameList = new System.Windows.Forms.ComboBox();
            this.cbPolymorph = new System.Windows.Forms.CheckBox();
            this.cbNameGen = new System.Windows.Forms.CheckBox();
            this.cbPazaak = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(20, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 40);
            this.label1.TabIndex = 48;
            this.label1.Text = "Add line break separated words to use as training data";
            // 
            // tbNameData
            // 
            this.tbNameData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(22)))));
            this.tbNameData.Enabled = false;
            this.tbNameData.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.tbNameData.Location = new System.Drawing.Point(20, 90);
            this.tbNameData.Multiline = true;
            this.tbNameData.Name = "tbNameData";
            this.tbNameData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbNameData.Size = new System.Drawing.Size(150, 260);
            this.tbNameData.TabIndex = 49;
            this.tbNameData.TextChanged += new System.EventHandler(this.tbNameData_TextChanged);
            this.tbNameData.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbNameData_KeyPress);
            // 
            // cbNameList
            // 
            this.cbNameList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(22)))));
            this.cbNameList.Enabled = false;
            this.cbNameList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.cbNameList.FormattingEnabled = true;
            this.cbNameList.Items.AddRange(new object[] {
            "Male Firstname",
            "Female Firstname",
            "Lastname"});
            this.cbNameList.Location = new System.Drawing.Point(20, 360);
            this.cbNameList.Name = "cbNameList";
            this.cbNameList.Size = new System.Drawing.Size(150, 21);
            this.cbNameList.TabIndex = 50;
            this.cbNameList.SelectedIndexChanged += new System.EventHandler(this.cbNameList_SelectedIndexChanged);
            // 
            // cbPolymorph
            // 
            this.cbPolymorph.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.cbPolymorph.Location = new System.Drawing.Point(180, 20);
            this.cbPolymorph.Name = "cbPolymorph";
            this.cbPolymorph.Size = new System.Drawing.Size(120, 140);
            this.cbPolymorph.TabIndex = 51;
            this.cbPolymorph.Text = "Polymorph Mode - Causes equipable items to have random disguise modifiers, essent" +
    "ially allowing the player to polymorph. (Doesn\'t synergize well with model rando" +
    ")";
            this.cbPolymorph.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.cbPolymorph.UseVisualStyleBackColor = true;
            this.cbPolymorph.CheckedChanged += new System.EventHandler(this.cbPolymorph_CheckedChanged);
            // 
            // cbNameGen
            // 
            this.cbNameGen.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbNameGen.Location = new System.Drawing.Point(20, 20);
            this.cbNameGen.Name = "cbNameGen";
            this.cbNameGen.Size = new System.Drawing.Size(150, 20);
            this.cbNameGen.TabIndex = 52;
            this.cbNameGen.Text = "Name Generator Rando:";
            this.cbNameGen.UseVisualStyleBackColor = true;
            this.cbNameGen.CheckedChanged += new System.EventHandler(this.cbNameGen_CheckedChanged);
            // 
            // cbPazaak
            // 
            this.cbPazaak.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.cbPazaak.Location = new System.Drawing.Point(180, 170);
            this.cbPazaak.Name = "cbPazaak";
            this.cbPazaak.Size = new System.Drawing.Size(120, 90);
            this.cbPazaak.TabIndex = 53;
            this.cbPazaak.Text = "Random Pazaak Decks - Randomizes the cards contained within the NPC pazaak decks." +
    "";
            this.cbPazaak.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.cbPazaak.UseVisualStyleBackColor = true;
            this.cbPazaak.CheckedChanged += new System.EventHandler(this.cbPazaak_CheckedChanged);
            // 
            // OtherForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(22)))));
            this.ClientSize = new System.Drawing.Size(365, 401);
            this.Controls.Add(this.cbPazaak);
            this.Controls.Add(this.cbNameGen);
            this.Controls.Add(this.cbPolymorph);
            this.Controls.Add(this.cbNameList);
            this.Controls.Add(this.tbNameData);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "OtherForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Other";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OtherForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbNameData;
        private System.Windows.Forms.ComboBox cbNameList;
        private System.Windows.Forms.CheckBox cbPolymorph;
        private System.Windows.Forms.CheckBox cbNameGen;
        private System.Windows.Forms.CheckBox cbPazaak;
    }
}