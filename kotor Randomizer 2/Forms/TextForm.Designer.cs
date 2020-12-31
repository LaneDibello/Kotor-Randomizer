namespace kotor_Randomizer_2
{
    partial class TextF
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
            this.cbDialogRando = new System.Windows.Forms.CheckBox();
            this.pDialogRando = new System.Windows.Forms.Panel();
            this.cbReplies = new System.Windows.Forms.CheckBox();
            this.cbEntries = new System.Windows.Forms.CheckBox();
            this.cbMatchEntrySound = new System.Windows.Forms.CheckBox();
            this.pTLK = new System.Windows.Forms.Panel();
            this.cbTLKRando = new System.Windows.Forms.CheckBox();
            this.cbMatchStringLen = new System.Windows.Forms.CheckBox();
            this.pDialogRando.SuspendLayout();
            this.pTLK.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbDialogRando
            // 
            this.cbDialogRando.Location = new System.Drawing.Point(20, 30);
            this.cbDialogRando.Name = "cbDialogRando";
            this.cbDialogRando.Size = new System.Drawing.Size(150, 20);
            this.cbDialogRando.TabIndex = 1;
            this.cbDialogRando.Text = "Dialogue Randomization";
            this.cbDialogRando.UseVisualStyleBackColor = true;
            this.cbDialogRando.CheckedChanged += new System.EventHandler(this.cbDialogRando_CheckedChanged);
            // 
            // pDialogRando
            // 
            this.pDialogRando.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pDialogRando.Controls.Add(this.cbMatchEntrySound);
            this.pDialogRando.Controls.Add(this.cbReplies);
            this.pDialogRando.Controls.Add(this.cbEntries);
            this.pDialogRando.Enabled = false;
            this.pDialogRando.Location = new System.Drawing.Point(180, 20);
            this.pDialogRando.Name = "pDialogRando";
            this.pDialogRando.Size = new System.Drawing.Size(400, 40);
            this.pDialogRando.TabIndex = 4;
            // 
            // cbReplies
            // 
            this.cbReplies.Location = new System.Drawing.Point(140, 10);
            this.cbReplies.Name = "cbReplies";
            this.cbReplies.Size = new System.Drawing.Size(130, 20);
            this.cbReplies.TabIndex = 1;
            this.cbReplies.Text = "Randomize Replies";
            this.cbReplies.UseVisualStyleBackColor = true;
            this.cbReplies.CheckedChanged += new System.EventHandler(this.cbReplies_CheckedChanged);
            // 
            // cbEntries
            // 
            this.cbEntries.Location = new System.Drawing.Point(10, 10);
            this.cbEntries.Name = "cbEntries";
            this.cbEntries.Size = new System.Drawing.Size(120, 20);
            this.cbEntries.TabIndex = 0;
            this.cbEntries.Text = "Randomize Entries";
            this.cbEntries.UseVisualStyleBackColor = true;
            this.cbEntries.CheckedChanged += new System.EventHandler(this.cbEntries_CheckedChanged);
            // 
            // cbMatchEntrySound
            // 
            this.cbMatchEntrySound.Enabled = false;
            this.cbMatchEntrySound.Location = new System.Drawing.Point(270, 10);
            this.cbMatchEntrySound.Name = "cbMatchEntrySound";
            this.cbMatchEntrySound.Size = new System.Drawing.Size(130, 20);
            this.cbMatchEntrySound.TabIndex = 2;
            this.cbMatchEntrySound.Text = "Match Entry Sounds";
            this.cbMatchEntrySound.UseVisualStyleBackColor = true;
            this.cbMatchEntrySound.CheckedChanged += new System.EventHandler(this.cbMatchEntrySound_CheckedChanged);
            // 
            // pTLK
            // 
            this.pTLK.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pTLK.Controls.Add(this.cbMatchStringLen);
            this.pTLK.Enabled = false;
            this.pTLK.Location = new System.Drawing.Point(180, 70);
            this.pTLK.Name = "pTLK";
            this.pTLK.Size = new System.Drawing.Size(400, 40);
            this.pTLK.TabIndex = 5;
            // 
            // cbTLKRando
            // 
            this.cbTLKRando.Location = new System.Drawing.Point(20, 80);
            this.cbTLKRando.Name = "cbTLKRando";
            this.cbTLKRando.Size = new System.Drawing.Size(160, 20);
            this.cbTLKRando.TabIndex = 6;
            this.cbTLKRando.Text = "Randomize Additional Text";
            this.cbTLKRando.UseVisualStyleBackColor = true;
            this.cbTLKRando.CheckedChanged += new System.EventHandler(this.cbTLKRando_CheckedChanged);
            // 
            // cbMatchStringLen
            // 
            this.cbMatchStringLen.Location = new System.Drawing.Point(10, 10);
            this.cbMatchStringLen.Name = "cbMatchStringLen";
            this.cbMatchStringLen.Size = new System.Drawing.Size(170, 20);
            this.cbMatchStringLen.TabIndex = 2;
            this.cbMatchStringLen.Text = "Match Similair String Length";
            this.cbMatchStringLen.UseVisualStyleBackColor = true;
            this.cbMatchStringLen.CheckedChanged += new System.EventHandler(this.cbMatchStringLen_CheckedChanged);
            // 
            // TextF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(22)))));
            this.ClientSize = new System.Drawing.Size(601, 131);
            this.Controls.Add(this.cbTLKRando);
            this.Controls.Add(this.pTLK);
            this.Controls.Add(this.pDialogRando);
            this.Controls.Add(this.cbDialogRando);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "TextF";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Text";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TextF_FormClosing);
            this.pDialogRando.ResumeLayout(false);
            this.pTLK.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox cbDialogRando;
        private System.Windows.Forms.Panel pDialogRando;
        private System.Windows.Forms.CheckBox cbMatchEntrySound;
        private System.Windows.Forms.CheckBox cbReplies;
        private System.Windows.Forms.CheckBox cbEntries;
        private System.Windows.Forms.Panel pTLK;
        private System.Windows.Forms.CheckBox cbMatchStringLen;
        private System.Windows.Forms.CheckBox cbTLKRando;
    }
}