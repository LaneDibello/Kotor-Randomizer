namespace kotor_Randomizer_2
{
    partial class TwodaForm
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
            this.selected_twoda = new System.Windows.Forms.ListBox();
            this.cbTwodaOptions = new System.Windows.Forms.ComboBox();
            this.cbColumnOptions = new System.Windows.Forms.ComboBox();
            this.selected_columns = new System.Windows.Forms.ListBox();
            this.bAllOff = new System.Windows.Forms.Button();
            this.bMax = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.bAddColumn = new System.Windows.Forms.Button();
            this.bAdd2da = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // selected_twoda
            // 
            this.selected_twoda.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(22)))));
            this.selected_twoda.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.selected_twoda.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.selected_twoda.FormattingEnabled = true;
            this.selected_twoda.Location = new System.Drawing.Point(20, 20);
            this.selected_twoda.Name = "selected_twoda";
            this.selected_twoda.Size = new System.Drawing.Size(180, 340);
            this.selected_twoda.TabIndex = 17;
            this.selected_twoda.SelectedIndexChanged += new System.EventHandler(this.selected_twoda_SelectedIndexChanged);
            this.selected_twoda.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.selected_twoda_MouseDoubleClick);
            // 
            // cbTwodaOptions
            // 
            this.cbTwodaOptions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(22)))));
            this.cbTwodaOptions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTwodaOptions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbTwodaOptions.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.cbTwodaOptions.FormattingEnabled = true;
            this.cbTwodaOptions.Location = new System.Drawing.Point(20, 370);
            this.cbTwodaOptions.Name = "cbTwodaOptions";
            this.cbTwodaOptions.Size = new System.Drawing.Size(130, 21);
            this.cbTwodaOptions.TabIndex = 19;
            // 
            // cbColumnOptions
            // 
            this.cbColumnOptions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(22)))));
            this.cbColumnOptions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbColumnOptions.Enabled = false;
            this.cbColumnOptions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbColumnOptions.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.cbColumnOptions.FormattingEnabled = true;
            this.cbColumnOptions.Location = new System.Drawing.Point(210, 370);
            this.cbColumnOptions.Name = "cbColumnOptions";
            this.cbColumnOptions.Size = new System.Drawing.Size(130, 21);
            this.cbColumnOptions.TabIndex = 21;
            // 
            // selected_columns
            // 
            this.selected_columns.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(22)))));
            this.selected_columns.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.selected_columns.Enabled = false;
            this.selected_columns.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.selected_columns.FormattingEnabled = true;
            this.selected_columns.Location = new System.Drawing.Point(210, 20);
            this.selected_columns.Name = "selected_columns";
            this.selected_columns.Size = new System.Drawing.Size(180, 340);
            this.selected_columns.TabIndex = 20;
            this.selected_columns.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.selected_columns_MouseDoubleClick);
            // 
            // bAllOff
            // 
            this.bAllOff.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.bAllOff.FlatAppearance.BorderSize = 2;
            this.bAllOff.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.bAllOff.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(22)))));
            this.bAllOff.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bAllOff.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bAllOff.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.bAllOff.Location = new System.Drawing.Point(400, 110);
            this.bAllOff.Name = "bAllOff";
            this.bAllOff.Size = new System.Drawing.Size(90, 80);
            this.bAllOff.TabIndex = 48;
            this.bAllOff.Text = "Disable All";
            this.bAllOff.UseVisualStyleBackColor = false;
            this.bAllOff.Click += new System.EventHandler(this.bAllOff_Click);
            // 
            // bMax
            // 
            this.bMax.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.bMax.FlatAppearance.BorderSize = 2;
            this.bMax.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.bMax.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(22)))));
            this.bMax.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bMax.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bMax.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.bMax.Location = new System.Drawing.Point(400, 20);
            this.bMax.Name = "bMax";
            this.bMax.Size = new System.Drawing.Size(90, 80);
            this.bMax.TabIndex = 49;
            this.bMax.Text = "All Max";
            this.bMax.UseVisualStyleBackColor = false;
            this.bMax.Click += new System.EventHandler(this.bMax_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(400, 200);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 190);
            this.label1.TabIndex = 50;
            this.label1.Text = "Randomizations of 2-dimensional arrays allows for the scrambling of a variety of " +
    "in-game features. The available options have been selected because they are unli" +
    "kely to break the entire game.";
            // 
            // bAddColumn
            // 
            this.bAddColumn.Enabled = false;
            this.bAddColumn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.bAddColumn.FlatAppearance.BorderSize = 2;
            this.bAddColumn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.bAddColumn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(22)))));
            this.bAddColumn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bAddColumn.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bAddColumn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.bAddColumn.Location = new System.Drawing.Point(350, 370);
            this.bAddColumn.Name = "bAddColumn";
            this.bAddColumn.Size = new System.Drawing.Size(40, 20);
            this.bAddColumn.TabIndex = 51;
            this.bAddColumn.Text = "Add";
            this.bAddColumn.UseVisualStyleBackColor = false;
            this.bAddColumn.Click += new System.EventHandler(this.bAddColumn_Click);
            // 
            // bAdd2da
            // 
            this.bAdd2da.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.bAdd2da.FlatAppearance.BorderSize = 2;
            this.bAdd2da.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.bAdd2da.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(22)))));
            this.bAdd2da.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bAdd2da.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bAdd2da.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.bAdd2da.Location = new System.Drawing.Point(160, 370);
            this.bAdd2da.Name = "bAdd2da";
            this.bAdd2da.Size = new System.Drawing.Size(40, 20);
            this.bAdd2da.TabIndex = 52;
            this.bAdd2da.Text = "Add";
            this.bAdd2da.UseVisualStyleBackColor = false;
            this.bAdd2da.Click += new System.EventHandler(this.bAdd2da_Click);
            // 
            // TwodaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(22)))));
            this.ClientSize = new System.Drawing.Size(510, 411);
            this.Controls.Add(this.bAdd2da);
            this.Controls.Add(this.bAddColumn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bMax);
            this.Controls.Add(this.bAllOff);
            this.Controls.Add(this.cbColumnOptions);
            this.Controls.Add(this.selected_columns);
            this.Controls.Add(this.cbTwodaOptions);
            this.Controls.Add(this.selected_twoda);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(255)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "TwodaForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "2DAs";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox selected_twoda;
        private System.Windows.Forms.ComboBox cbTwodaOptions;
        private System.Windows.Forms.ComboBox cbColumnOptions;
        private System.Windows.Forms.ListBox selected_columns;
        private System.Windows.Forms.Button bAllOff;
        private System.Windows.Forms.Button bMax;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bAddColumn;
        private System.Windows.Forms.Button bAdd2da;
    }
}