using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kotor_Randomizer_2
{
    public partial class SeedForm : Form
    {
        public SeedForm()
        {
            InitializeComponent();

            // Pull the seed that was generated at start-time from settings.
            SeedText.Text = Convert.ToString(Properties.Settings.Default.Seed);

            // You cannot edit the seed while the game is currently randomized.
            SeedText.Enabled = !Properties.Settings.Default.KotorIsRandomized;
        }

        /// <summary>
        /// Prevents non-numeric characters from being input.
        /// </summary>
        private void SeedText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Parses the user-defined seed.
        /// </summary>
        private void SeedText_TextChanged(object sender, EventArgs e)
        {
            int o;
            if(int.TryParse(SeedText.Text, out o) && SeedText.Focused)
            {
                Properties.Settings.Default.Seed = o;
                Properties.Settings.Default.SeedSelected = true;
            }
        }

        /// <summary>
        /// Handle enable / disable of SeedText
        /// </summary>
        private void SeedForm_Activated(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.KotorIsRandomized)
            {
                SeedText.Enabled = false;
                btnNewSeed.Enabled = false;
            }
            else
            {
                SeedText.Enabled = true;
                btnNewSeed.Enabled = true;
            }
        }

        /// <summary>
        /// Generate new seed when clicked.
        /// </summary>
        private void btnNewSeed_Click(object sender, EventArgs e)
        {
            if (!Properties.Settings.Default.KotorIsRandomized)
            {
                Properties.Settings.Default.Seed = Randomize.Rng.Next();
                SeedText.Text = Properties.Settings.Default.Seed.ToString();
            }
        }
    }
}
