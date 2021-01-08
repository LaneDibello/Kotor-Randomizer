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

            Properties.Settings.Default.ModulesInitialized = false; // Might need to change this

            // Checks if a path is saved
            if (Properties.Settings.Default.Kotor1Path == "")
            {
                path_button_Click(0, new EventArgs());
            }

            //Active Rando Categories (start false)
            Properties.Settings.Default.DoRandomization_Module = false;
            Properties.Settings.Default.DoRandomization_Sound = false;
            Properties.Settings.Default.DoRandomization_Model = false;
            Properties.Settings.Default.DoRandomization_Item = false;
            Properties.Settings.Default.DoRandomization_Texture = false;
            Properties.Settings.Default.DoRandomization_TwoDA = false;
            Properties.Settings.Default.DoRandomization_Text = false;
            Properties.Settings.Default.DoRandomization_Other = false;

            if (File.Exists(Properties.Settings.Default.Kotor1Path + "\\RANDOMIZED.log"))
            {
                Properties.Settings.Default.KotorIsRandomized = true;
                randomize_button.Text = "Unrandomize!";
            }
            else
            {
                Properties.Settings.Default.KotorIsRandomized = false;
                randomize_button.Text = "Randomize!";
            }

            if (fn != "")
            {
                new PresetForm(fn).Show();
            }
        }

        private void LogCurrentSettings()
        {
            string path = Path.Combine(Environment.CurrentDirectory, "current.krp");
            if (File.Exists(path)) { File.Delete(path); }
            KRP.WriteKRP(File.OpenWrite(path));
        }

        private void CreateSpoilerLogs()
        {
            string spoilersPath = Path.Combine(Environment.CurrentDirectory, "Spoilers");
            //if (Directory.Exists(spoilersPath)) { Directory.Delete(spoilersPath, true); }
            Directory.CreateDirectory(spoilersPath);

            var timestamp = DateTime.Now.ToString("yy-MM-dd_HH-mm-ss");
            ItemRando.GenerateSpoilerLog(Path.Combine(spoilersPath, $"{timestamp}_items.csv"));
            ModelRando.GenerateSpoilerLog(Path.Combine(spoilersPath, $"{timestamp}_models.csv"));
            ModuleRando.GenerateSpoilerLog(Path.Combine(spoilersPath, $"{timestamp}_modules.csv"));

            MessageBox.Show("Spoiler logs created.");
        }

        #region  Events

        /// <summary>
        /// Searches through the open forms to find the first of the specified type.
        /// </summary>
        /// <typeparam name="T">Type of the form to search for.</typeparam>
        /// <returns>The form itself if it exists; null otherwise.</returns>
        private T FindOpenForm<T>() where T : Form
        {
            T tForm = null;
            foreach (Form form in Application.OpenForms)
            {
                tForm = form as T;
                if (null != tForm)
                {
                    return tForm;
                }
            }

            return null;
        }

        /// <summary>
        /// This foreach seen in the different buttons events keeps multiple instances of a form from loading, and prevents everything breaking. :D
        /// </summary>
        /// <typeparam name="T">Type of the form to search for.</typeparam>
        /// <returns>True if the form is found; false otherwise.</returns>
        private bool FocusOpenForm<T>() where T : Form
        {
            foreach (Form f in Application.OpenForms)
            {
                if (f is T)
                {
                    f.Focus();
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// This sets up the seeding at program start.
        /// </summary>
        private void StartForm_Load(object sender, EventArgs e)
        {
            //Randomize.GenerateSeed();
            //Properties.Settings.Default.Seed = Randomize.Rng.Next();
            //Properties.Settings.Default.Seed = Randomize.Seed;
            Properties.Settings.Default.Seed = Randomize.GenerateSeed();
            Randomize.RestartRng();
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

        private void module_button_Click(object sender, EventArgs e)
        {
            if (false == FocusOpenForm<ModuleForm>())
            {
                new ModuleForm().Show();
                if (!Properties.Settings.Default.DoRandomization_Module) { module_radio_Click(sender, e); module_radio_MouseLeave(sender, e); }
            }
        }

        private void item_button_Click(object sender, EventArgs e)
        {
            if (false == FocusOpenForm<ItemForm>())
            {
                new ItemForm().Show();
                if (!Properties.Settings.Default.DoRandomization_Item) { item_radio_Click(sender, e); item_radio_MouseLeave(sender, e); }
            }
        }

        private void sound_button_Click(object sender, EventArgs e)
        {
            if (false == FocusOpenForm<SoundForm>())
            {
                new SoundForm().Show();
                if (!Properties.Settings.Default.DoRandomization_Sound) { sound_radio_Click(sender, e); sound_radio_MouseLeave(sender, e); }
            }
        }

        private void model_button_Click(object sender, EventArgs e)
        {
            if (false == FocusOpenForm<ModelForm>())
            {
                new ModelForm().Show();
                if (!Properties.Settings.Default.DoRandomization_Model) { model_radio_Click(sender, e); model_radio_MouseLeave(sender, e); }
            }
        }

        private void texture_button_Click(object sender, EventArgs e)
        {
            if (false == FocusOpenForm<TextureForm>())
            {
                new TextureForm().Show();
                if (!Properties.Settings.Default.DoRandomization_Texture) { texture_radio_Click(sender, e); texture_radio_MouseLeave(sender, e); }
            }
        }

        private void twoda_button_Click(object sender, EventArgs e)
        {
            if (false == FocusOpenForm<TwodaForm>())
            {
                new TwodaForm().Show();
                if (!Properties.Settings.Default.DoRandomization_TwoDA) { twoda_radio_Click(sender, e); twoda_radio_MouseLeave(sender, e); }
            }
        }

        private void text_button_Click(object sender, EventArgs e)
        {
            if (false == FocusOpenForm<TextF>())
            {
                new TextF().Show();
                if (!Properties.Settings.Default.DoRandomization_Text) { text_radio_Click(sender, e); text_radio_MouseLeave(sender, e); }
            }
        }

        private void other_button_Click(object sender, EventArgs e)
        {
            if (false == FocusOpenForm<OtherForm>())
            {
                new OtherForm().Show();
                if (!Properties.Settings.Default.DoRandomization_Other) { other_radio_Click(sender, e); other_radio_MouseLeave(sender, e); }
            }
        }

        private void path_button_Click(object sender, EventArgs e)
        {
            if (false == FocusOpenForm<PathForm>())
            {
                PathForm PF = new PathForm();
                PF.Show();
                PF.BringToFront();
            }
        }

        private void seed_button_Click(object sender, EventArgs e)
        {
            if (false == FocusOpenForm<SeedForm>())
            {
                new SeedForm().Show();
            }
        }

        private void randomize_button_Click(object sender, EventArgs e)
        {
            if (false == FocusOpenForm<RandoForm>())
            {
                if (Properties.Settings.Default.KotorIsRandomized)
                {
                    randomize_button.Text = "Randomize!";
                }
                else
                {
                    randomize_button.Text = "Unrandomize!";
                    LogCurrentSettings();
                    generateSpoilersToolStripMenuItem.Enabled = true;
                }

                new RandoForm().Show();
            }
        }

        private void bPresets_Click(object sender, EventArgs e)
        {
            if (false == FocusOpenForm<PresetForm>())
            {
                new PresetForm().Show();
            }
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (false == FocusOpenForm<HelpForm>())
            {
                new HelpForm().Show();
            }
        }

        private void generateSpoilersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateSpoilerLogs();
        }

        private void openSpoilersFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Path.Combine(Environment.CurrentDirectory, "Spoilers"));
        }
        #endregion
    }
}
