namespace kotor_Randomizer_2
{
    partial class PathForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.tbKotor1Path = new System.Windows.Forms.TextBox();
            this.tbKotor2Path = new System.Windows.Forms.TextBox();
            this.bKotor1PathBrowse = new System.Windows.Forms.Button();
            this.bKotor2PathBrowse = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.fbdKotor1 = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(20, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "KotOR 1:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline) 
                | System.Drawing.FontStyle.Strikeout))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(20, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "KotOR 2:";
            // 
            // tbKotor1Path
            // 
            this.tbKotor1Path.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(22)))));
            this.tbKotor1Path.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.tbKotor1Path.Location = new System.Drawing.Point(100, 20);
            this.tbKotor1Path.Name = "tbKotor1Path";
            this.tbKotor1Path.Size = new System.Drawing.Size(470, 20);
            this.tbKotor1Path.TabIndex = 2;
            this.tbKotor1Path.TextChanged += new System.EventHandler(this.tbKotor1Path_TextChanged);
            // 
            // tbKotor2Path
            // 
            this.tbKotor2Path.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(22)))));
            this.tbKotor2Path.Enabled = false;
            this.tbKotor2Path.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.tbKotor2Path.Location = new System.Drawing.Point(100, 50);
            this.tbKotor2Path.Name = "tbKotor2Path";
            this.tbKotor2Path.ReadOnly = true;
            this.tbKotor2Path.Size = new System.Drawing.Size(470, 20);
            this.tbKotor2Path.TabIndex = 3;
            this.tbKotor2Path.Text = "KotOR 2 randomization not currently available.";
            // 
            // bKotor1PathBrowse
            // 
            this.bKotor1PathBrowse.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.bKotor1PathBrowse.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.bKotor1PathBrowse.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(216)))), ((int)(((byte)(8)))));
            this.bKotor1PathBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bKotor1PathBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bKotor1PathBrowse.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.bKotor1PathBrowse.Location = new System.Drawing.Point(580, 20);
            this.bKotor1PathBrowse.Name = "bKotor1PathBrowse";
            this.bKotor1PathBrowse.Size = new System.Drawing.Size(20, 20);
            this.bKotor1PathBrowse.TabIndex = 9;
            this.bKotor1PathBrowse.Text = "...";
            this.bKotor1PathBrowse.UseVisualStyleBackColor = false;
            this.bKotor1PathBrowse.Click += new System.EventHandler(this.bKotor1PathBrowse_Click);
            // 
            // bKotor2PathBrowse
            // 
            this.bKotor2PathBrowse.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.bKotor2PathBrowse.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.bKotor2PathBrowse.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(216)))), ((int)(((byte)(8)))));
            this.bKotor2PathBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bKotor2PathBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bKotor2PathBrowse.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.bKotor2PathBrowse.Location = new System.Drawing.Point(580, 50);
            this.bKotor2PathBrowse.Name = "bKotor2PathBrowse";
            this.bKotor2PathBrowse.Size = new System.Drawing.Size(20, 20);
            this.bKotor2PathBrowse.TabIndex = 10;
            this.bKotor2PathBrowse.Text = "...";
            this.bKotor2PathBrowse.UseVisualStyleBackColor = false;
            // 
            // button1
            // 
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(216)))), ((int)(((byte)(8)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.button1.Location = new System.Drawing.Point(610, 20);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(60, 50);
            this.button1.TabIndex = 11;
            this.button1.Text = "Auto Find";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.bAutoFind_Click);
            // 
            // PathForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(22)))));
            this.ClientSize = new System.Drawing.Size(691, 91);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.bKotor2PathBrowse);
            this.Controls.Add(this.bKotor1PathBrowse);
            this.Controls.Add(this.tbKotor2Path);
            this.Controls.Add(this.tbKotor1Path);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "PathForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Path";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PathForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbKotor1Path;
        private System.Windows.Forms.TextBox tbKotor2Path;
        private System.Windows.Forms.Button bKotor1PathBrowse;
        private System.Windows.Forms.Button bKotor2PathBrowse;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.FolderBrowserDialog fbdKotor1;
    }
}