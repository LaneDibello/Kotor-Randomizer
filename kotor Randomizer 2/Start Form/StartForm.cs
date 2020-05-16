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
    public partial class StartForm : Form
    {
        public StartForm()
        {
            InitializeComponent();

            Properties.Settings.Default.ModulesInitialized = false;

            //Checks if a path is saved
            if (Properties.Settings.Default.Kotor1Path == "")
            {
                path_button_Click(0, new EventArgs());
            }

            //Active Rando Categories (start false)
            Properties.Settings.Default.module_rando_active = false;
            Properties.Settings.Default.sound_rando_active = false;
            Properties.Settings.Default.model_rando_active = false;
            Properties.Settings.Default.item_rando_active = false;
        }

        //Events

        private void module_button_Click(object sender, EventArgs e)
        {
            ModuleForm MF = new ModuleForm();
            MF.Show();
            if (!Properties.Settings.Default.module_rando_active) { module_radio_Click(sender, e); module_radio_MouseLeave(sender, e); }
        }

        private void sound_button_Click(object sender, EventArgs e)
        {
            SoundForm SF = new SoundForm();
            SF.Show();
            if (!Properties.Settings.Default.sound_rando_active) { sound_radio_Click(sender, e); sound_radio_MouseLeave(sender, e); }
        }

        private void path_button_Click(object sender, EventArgs e)
        {
            PathForm PF = new PathForm();
            PF.Show();
            PF.BringToFront();
        }

        private void model_button_Click(object sender, EventArgs e)
        {
            ModelForm MF = new ModelForm();
            MF.Show();
            if (!Properties.Settings.Default.model_rando_active) { model_radio_Click(sender, e); model_radio_MouseLeave(sender, e); }
        }

        private void item_button_Click(object sender, EventArgs e)
        {
            ItemForm IF = new ItemForm();
            IF.Show();
            if (!Properties.Settings.Default.item_rando_active) { item_radio_Click(sender, e); item_radio_MouseLeave(sender, e); }
        }

        private void seed_button_Click(object sender, EventArgs e)
        {
            SeedForm SF = new SeedForm();
            SF.Show();
        }

        private void texture_button_Click(object sender, EventArgs e)
        {
            TextureForm TF = new TextureForm();
            TF.Show();
            if (!Properties.Settings.Default.texture_rando_active) { texture_radio_Click(sender, e); texture_radio_MouseLeave(sender, e); }
        }

        private void twoda_button_Click(object sender, EventArgs e)
        {
            TwodaForm TF = new TwodaForm();
            TF.Show();
            if (!Properties.Settings.Default.twoda_rando_active) { twoda_radio_Click(sender, e); twoda_radio_MouseLeave(sender, e); }
        }

        private void text_button_Click(object sender, EventArgs e)
        {
            TextF TF = new TextF();
            TF.Show();
            if (!Properties.Settings.Default.text_rando_active) { text_radio_Click(sender, e); text_radio_MouseLeave(sender, e); }
        }

        private void other_button_Click(object sender, EventArgs e)
        {
            OtherForm OF = new OtherForm();
            OF.Show();
            if (!Properties.Settings.Default.other_rando_active) { other_radio_Click(sender, e); other_radio_MouseLeave(sender, e); }
        }
    }
}
