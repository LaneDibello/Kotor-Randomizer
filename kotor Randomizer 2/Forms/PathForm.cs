using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace kotor_Randomizer_2
{
    public partial class PathForm : Form
    {
        public PathForm()
        {
            InitializeComponent();

            // Set up initial state from the previously saved settings.
            tbKotor1Path.Text = Properties.Settings.Default.Kotor1Path;
        }

        public string Kotor1Path
        {
            get { return tbKotor1Path.Text; }
            set { tbKotor1Path.Text = value; }
        }

        //I have my doubts that I will expand this to kotor II, but just in case.
        public string Kotor2Path
        {
            get { return tbKotor2Path.Text; }
            set { tbKotor2Path.Text = value; }
        }



        private void bKotor1PathBrowse_Click(object sender, EventArgs e)
        {
            fbdKotor1.SelectedPath = tbKotor1Path.Text;
            if (fbdKotor1.ShowDialog() == DialogResult.OK)
            {
                tbKotor1Path.Text = fbdKotor1.SelectedPath;
            }
        }

        //Glasnonck's swkotor finder.
        private void bAutoFind_Click(object sender, EventArgs e)
        {
            bool K1PathFound = false;
            List<string> Kotor1Paths = new List<string>()
            {
                @"C:\Program Files (x86)\Steam\steamapps\common\swkotor",
                @"C:\Program Files\Steam\steamapps\common\swkotor",
                @"C:\Program Files (x86)\LucasArts\SWKotOR",
                @"C:\Program Files\LucasArts\SWKotOR",
            };

            foreach (string path in Kotor1Paths)
            {
                DirectoryInfo di = new DirectoryInfo(path);
                if (di.Exists)
                {
                    tbKotor1Path.Text = path;
                    K1PathFound = true;
                    break;
                }
            }

            if (K1PathFound)
            {
                MessageBox.Show(this, "KotOR 1 path found!");
            }
            else
            {
                MessageBox.Show(this, "KotOR 1 path not found. Please enter it manually.");
            }
        }

        private void PathForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Kotor1Path = Kotor1Path;
            Properties.Settings.Default.Save();
        }
    }
}
