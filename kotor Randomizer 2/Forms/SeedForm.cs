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
            //Pull the seed that was generated at start-time from settings
            seed_text.Text = Convert.ToString(Properties.Settings.Default.Seed);
            //You cannot edit the seed while the game is currently randomized.
            seed_text.Enabled = !Properties.Settings.Default.KotorIsRandomized;
        }

        //Prevents non-numeric characters from being input
        private void seed_text_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        //Parses the user-defined seed 
        private void seed_text_TextChanged(object sender, EventArgs e)
        {
            int o;
            if(int.TryParse(seed_text.Text, out o) && seed_text.Focused)
            {
                Properties.Settings.Default.Seed = o;
                Properties.Settings.Default.SeedSelected = true;
            }
        }
    }
}
