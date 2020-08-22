using System;
using System.Windows.Forms;
using System.IO;

namespace kotor_Randomizer_2
{
    public partial class StartForm : Form
    {
        public StartForm(string fn = "")
        {
            InitializeComponent();

            Properties.Settings.Default.ModulesInitialized = false;//Might need to change this

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
            Properties.Settings.Default.texture_rando_active = false;
            Properties.Settings.Default.twoda_rando_active = false;
            Properties.Settings.Default.text_rando_active = false;
            Properties.Settings.Default.other_rando_active = false;

            if (File.Exists(Properties.Settings.Default.Kotor1Path + "\\RANDOMIZED.log"))
            {
                Properties.Settings.Default.KotorIsRandomized = true;
            }
            else
            {
                Properties.Settings.Default.KotorIsRandomized = false;
            }

            if (Properties.Settings.Default.KotorIsRandomized)
            {
                randomize_button.Text = "Unrandomize!";
            }
            else
            {
                randomize_button.Text = "Randomize!";
            }

            if (fn != "")
            {
                new PresetForm(fn).Show();
            }
        }

        #region  Events

        private void module_button_Click(object sender, EventArgs e)
        {
            //This foreach seen in the different buttons events keeps multiple instances of a form from loading, and prevents everything breaking :D
            foreach (Form f in Application.OpenForms)
            {
                if (f is ModuleForm)
                {
                    f.Focus();
                    return;
                }
            }

            new ModuleForm().Show();
            if (!Properties.Settings.Default.module_rando_active) { module_radio_Click(sender, e); module_radio_MouseLeave(sender, e); }
        }

        private void sound_button_Click(object sender, EventArgs e)
        {
            foreach (Form f in Application.OpenForms)
            {
                if (f is SoundForm)
                {
                    f.Focus();
                    return;
                }
            }

            new SoundForm().Show();
            if (!Properties.Settings.Default.sound_rando_active) { sound_radio_Click(sender, e); sound_radio_MouseLeave(sender, e); }
        }

        private void path_button_Click(object sender, EventArgs e)
        {
            foreach (Form f in Application.OpenForms)
            {
                if (f is PathForm)
                {
                    f.Focus();
                    return;
                }
            }

            PathForm PF = new PathForm();
            PF.Show();
            PF.BringToFront();
        }

        private void model_button_Click(object sender, EventArgs e)
        {
            foreach (Form f in Application.OpenForms)
            {
                if (f is ModelForm)
                {
                    f.Focus();
                    return;
                }
            }

            new ModelForm().Show();
            if (!Properties.Settings.Default.model_rando_active) { model_radio_Click(sender, e); model_radio_MouseLeave(sender, e); }
        }

        private void item_button_Click(object sender, EventArgs e)
        {
            foreach (Form f in Application.OpenForms)
            {
                if (f is ItemForm)
                {
                    f.Focus();
                    return;
                }
            }

            new ItemForm().Show();
            if (!Properties.Settings.Default.item_rando_active) { item_radio_Click(sender, e); item_radio_MouseLeave(sender, e); }
        }

        private void seed_button_Click(object sender, EventArgs e)
        {
            foreach (Form f in Application.OpenForms)
            {
                if (f is SeedForm)
                {
                    f.Focus();
                    return;
                }
            }

            new SeedForm().Show();
        }

        private void texture_button_Click(object sender, EventArgs e)
        {
            foreach (Form f in Application.OpenForms)
            {
                if (f is TextureForm)
                {
                    f.Focus();
                    return;
                }
            }

            new TextureForm().Show();
            if (!Properties.Settings.Default.texture_rando_active) { texture_radio_Click(sender, e); texture_radio_MouseLeave(sender, e); }
        }

        private void twoda_button_Click(object sender, EventArgs e)
        {
            foreach (Form f in Application.OpenForms)
            {
                if (f is TwodaForm)
                {
                    f.Focus();
                    return;
                }
            }

            new TwodaForm().Show();
            if (!Properties.Settings.Default.twoda_rando_active) { twoda_radio_Click(sender, e); twoda_radio_MouseLeave(sender, e); }
        }

        private void text_button_Click(object sender, EventArgs e)
        {
            foreach (Form f in Application.OpenForms)
            {
                if (f is TextF)
                {
                    f.Focus();
                    return;
                }
            }

            new TextF().Show();
            if (!Properties.Settings.Default.text_rando_active) { text_radio_Click(sender, e); text_radio_MouseLeave(sender, e); }
        }

        private void other_button_Click(object sender, EventArgs e)
        {
            foreach (Form f in Application.OpenForms)
            {
                if (f is OtherForm)
                {
                    f.Focus();
                    return;
                }
            }

            new OtherForm().Show();
            if (!Properties.Settings.Default.other_rando_active) { other_radio_Click(sender, e); other_radio_MouseLeave(sender, e); }
        }

        //This sets up the seeding at program start.
        private void StartForm_Load(object sender, EventArgs e)
        {
            Randomize.GenerateSeed();
            Properties.Settings.Default.Seed = Randomize.Rng.Next();
        }

        private void randomize_button_Click(object sender, EventArgs e)
        {
            foreach (Form f in Application.OpenForms)
            {
                if (f is RandoForm)
                {
                    f.Focus();
                    return;
                }
            }

            new RandoForm().Show();

            if (Properties.Settings.Default.KotorIsRandomized)
            {
                randomize_button.Text = "Randomize!";
            }
            else
            {
                randomize_button.Text = "Unrandomize!";
            }
        }

        private void bPresets_Click(object sender, EventArgs e)
        {
            foreach (Form f in Application.OpenForms)
            {
                if (f is PresetForm)
                {
                    f.Focus();
                    return;
                }
            }

            new PresetForm().Show();
        }

        private void StartForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.ModulesInitialized = false;
            Properties.Settings.Default.Save();
        }

        private void StartForm_Activated(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.KotorIsRandomized)
            {
                randomize_button.Text = "Unrandomize!";
            }
            else
            {
                randomize_button.Text = "Randomize!";
            }
            module_radio_MouseLeave(sender, e);
            sound_radio_MouseLeave(sender, e);
            model_radio_MouseLeave(sender, e);
            item_radio_MouseLeave(sender, e);
            texture_radio_MouseLeave(sender, e);
            twoda_radio_MouseLeave(sender, e);
            text_radio_MouseLeave(sender, e);
            other_radio_MouseLeave(sender, e);
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form f in Application.OpenForms)
            {
                if (f is HelpForm)
                {
                    f.Focus();
                    return;
                }
            }

            new HelpForm().Show();
        }
        #endregion
    }
}
