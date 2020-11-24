namespace kotor_Randomizer_2
{
    partial class HelpForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HelpForm));
            this.tbInfo1 = new System.Windows.Forms.TextBox();
            this.cbCata = new System.Windows.Forms.ComboBox();
            this.tbInfo2 = new System.Windows.Forms.TextBox();
            this.tbAbout = new System.Windows.Forms.TextBox();
            this.llDisc = new System.Windows.Forms.LinkLabel();
            this.llSRC = new System.Windows.Forms.LinkLabel();
            this.llTwitch = new System.Windows.Forms.LinkLabel();
            this.llStratWiki = new System.Windows.Forms.LinkLabel();
            this.llYoutube = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // tbInfo1
            // 
            this.tbInfo1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(22)))));
            this.tbInfo1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.tbInfo1.Location = new System.Drawing.Point(230, 20);
            this.tbInfo1.Multiline = true;
            this.tbInfo1.Name = "tbInfo1";
            this.tbInfo1.ReadOnly = true;
            this.tbInfo1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbInfo1.Size = new System.Drawing.Size(260, 400);
            this.tbInfo1.TabIndex = 0;
            // 
            // cbCata
            // 
            this.cbCata.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(22)))));
            this.cbCata.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.cbCata.FormattingEnabled = true;
            this.cbCata.Location = new System.Drawing.Point(20, 20);
            this.cbCata.Name = "cbCata";
            this.cbCata.Size = new System.Drawing.Size(200, 21);
            this.cbCata.TabIndex = 1;
            this.cbCata.SelectedIndexChanged += new System.EventHandler(this.cbCata_SelectedIndexChanged);
            // 
            // tbInfo2
            // 
            this.tbInfo2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(22)))));
            this.tbInfo2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.tbInfo2.Location = new System.Drawing.Point(500, 20);
            this.tbInfo2.Multiline = true;
            this.tbInfo2.Name = "tbInfo2";
            this.tbInfo2.ReadOnly = true;
            this.tbInfo2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbInfo2.Size = new System.Drawing.Size(260, 400);
            this.tbInfo2.TabIndex = 2;
            // 
            // tbAbout
            // 
            this.tbAbout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(22)))));
            this.tbAbout.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.tbAbout.Location = new System.Drawing.Point(20, 50);
            this.tbAbout.Multiline = true;
            this.tbAbout.Name = "tbAbout";
            this.tbAbout.ReadOnly = true;
            this.tbAbout.Size = new System.Drawing.Size(200, 210);
            this.tbAbout.TabIndex = 3;
            this.tbAbout.Text = resources.GetString("tbAbout.Text");
            // 
            // llDisc
            // 
            this.llDisc.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(216)))), ((int)(((byte)(8)))));
            this.llDisc.AutoSize = true;
            this.llDisc.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.llDisc.Location = new System.Drawing.Point(20, 270);
            this.llDisc.Name = "llDisc";
            this.llDisc.Size = new System.Drawing.Size(141, 13);
            this.llDisc.TabIndex = 4;
            this.llDisc.TabStop = true;
            this.llDisc.Text = "Speedrunning Discord Invite";
            this.llDisc.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(216)))), ((int)(((byte)(8)))));
            this.llDisc.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llDisc_LinkClicked);
            // 
            // llSRC
            // 
            this.llSRC.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(216)))), ((int)(((byte)(8)))));
            this.llSRC.AutoSize = true;
            this.llSRC.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.llSRC.Location = new System.Drawing.Point(20, 300);
            this.llSRC.Name = "llSRC";
            this.llSRC.Size = new System.Drawing.Size(76, 13);
            this.llSRC.TabIndex = 5;
            this.llSRC.TabStop = true;
            this.llSRC.Text = "Speedrun.com";
            this.llSRC.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(216)))), ((int)(((byte)(8)))));
            this.llSRC.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llSRC_LinkClicked);
            // 
            // llTwitch
            // 
            this.llTwitch.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(216)))), ((int)(((byte)(8)))));
            this.llTwitch.AutoSize = true;
            this.llTwitch.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.llTwitch.Location = new System.Drawing.Point(20, 360);
            this.llTwitch.Name = "llTwitch";
            this.llTwitch.Size = new System.Drawing.Size(98, 13);
            this.llTwitch.TabIndex = 6;
            this.llTwitch.TabStop = true;
            this.llTwitch.Text = "My Twitch Channel";
            this.llTwitch.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(216)))), ((int)(((byte)(8)))));
            this.llTwitch.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llTwitch_LinkClicked);
            // 
            // llStratWiki
            // 
            this.llStratWiki.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(216)))), ((int)(((byte)(8)))));
            this.llStratWiki.AutoSize = true;
            this.llStratWiki.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.llStratWiki.Location = new System.Drawing.Point(20, 330);
            this.llStratWiki.Name = "llStratWiki";
            this.llStratWiki.Size = new System.Drawing.Size(145, 13);
            this.llStratWiki.TabIndex = 7;
            this.llStratWiki.TabStop = true;
            this.llStratWiki.Text = "Strategy Wiki Cheats Section";
            this.llStratWiki.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(216)))), ((int)(((byte)(8)))));
            this.llStratWiki.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llStratWiki_LinkClicked);
            // 
            // llYoutube
            // 
            this.llYoutube.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(216)))), ((int)(((byte)(8)))));
            this.llYoutube.AutoSize = true;
            this.llYoutube.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.llYoutube.Location = new System.Drawing.Point(20, 390);
            this.llYoutube.Name = "llYoutube";
            this.llYoutube.Size = new System.Drawing.Size(110, 13);
            this.llYoutube.TabIndex = 8;
            this.llYoutube.TabStop = true;
            this.llYoutube.Text = "My YouTube Channel";
            this.llYoutube.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(216)))), ((int)(((byte)(8)))));
            this.llYoutube.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llYoutube_LinkClicked);
            // 
            // HelpForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(22)))));
            this.ClientSize = new System.Drawing.Size(784, 441);
            this.Controls.Add(this.llYoutube);
            this.Controls.Add(this.llStratWiki);
            this.Controls.Add(this.llTwitch);
            this.Controls.Add(this.llSRC);
            this.Controls.Add(this.llDisc);
            this.Controls.Add(this.tbAbout);
            this.Controls.Add(this.tbInfo2);
            this.Controls.Add(this.cbCata);
            this.Controls.Add(this.tbInfo1);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "HelpForm";
            this.ShowIcon = false;
            this.Text = "Info";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbInfo1;
        private System.Windows.Forms.ComboBox cbCata;
        private System.Windows.Forms.TextBox tbInfo2;
        private System.Windows.Forms.TextBox tbAbout;
        private System.Windows.Forms.LinkLabel llDisc;
        private System.Windows.Forms.LinkLabel llSRC;
        private System.Windows.Forms.LinkLabel llTwitch;
        private System.Windows.Forms.LinkLabel llStratWiki;
        private System.Windows.Forms.LinkLabel llYoutube;
    }
}