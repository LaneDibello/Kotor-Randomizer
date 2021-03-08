using System;
using System.Windows.Forms;
using System.IO;
using ClosedXML.Excel;
using System.Collections.Generic;

namespace kotor_Randomizer_2
{
    public partial class StartForm : Form
    {
        public StartForm(string fn = "")
        {
            InitializeComponent();

            Version version = typeof(StartForm).Assembly.GetName().Version;
            this.Text = $"{this.Text} v{version.Major}.{version.Minor}";

            Properties.Settings settings = Properties.Settings.Default;

            // Initialize the bound modules list.
            Globals.BoundModules.Clear();
            foreach (string s in Globals.MODULES)
            {
                Globals.BoundModules.Add(new Globals.Mod_Entry(s, settings.OmittedModules.Contains(s)));
            }

            // Checks if a path is saved
            if (Properties.Settings.Default.Kotor1Path == "")
            {
                path_button_Click(0, new EventArgs());
            }

            //Active Rando Categories (start false)
            settings.DoRandomization_Module = false;
            settings.DoRandomization_Sound = false;
            settings.DoRandomization_Model = false;
            settings.DoRandomization_Item = false;
            settings.DoRandomization_Texture = false;
            settings.DoRandomization_TwoDA = false;
            settings.DoRandomization_Text = false;
            settings.DoRandomization_Other = false;

            if (File.Exists(settings.Kotor1Path + "\\RANDOMIZED.log"))
            {
                settings.KotorIsRandomized = true;
                randomize_button.Text = "Unrandomize!";
            }
            else
            {
                settings.KotorIsRandomized = false;
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

            //var timestamp = DateTime.Now.ToString("yy-MM-dd_HH-mm-ss");
            var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");
            var filename = $"{timestamp}, Seed={Properties.Settings.Default.Seed}.xlsx";
            var path = Path.Combine(spoilersPath, filename);

            if (File.Exists(path)) { File.Delete(path); }

            using (var workbook = new XLWorkbook())
            {
                ItemRando.GenerateSpoilerLog(workbook);
                ModelRando.GenerateSpoilerLog(workbook);
                ModuleRando.GenerateSpoilerLog(workbook);
                SoundRando.GenerateSpoilerLog(workbook);
                OtherRando.GenerateSpoilerLog(workbook);
                TextRando.GenerateSpoilerLog(workbook);
                TextureRando.GenerateSpoilerLog(workbook);
                TwodaRandom.GenerateSpoilerLog(workbook);

                // If any worksheets have been added, save the spoiler log.
                if (workbook.Worksheets.Count > 0)
                {
                    System.Text.StringBuilder wsList = new System.Text.StringBuilder();
                    foreach (var sheet in workbook.Worksheets)
                    {
                        wsList.Append($"{sheet.Name}, ");
                    }
                    wsList.Remove(wsList.Length - 2, 2);

                    workbook.SaveAs(path);
                    var result = MessageBox.Show($"Spoiler logs created: {wsList.ToString()}{Environment.NewLine}Open the spoilers folder?", Properties.Resources.GenerateSpoilerLogs, MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes) { OpenSpoilersFolder(); }
                }
                else
                {
                    MessageBox.Show($"No spoiler logs created. Either the game has not been randomized, or the selected randomizations do not generate spoilers.", Properties.Resources.GenerateSpoilerLogs);
                }
            }
        }

        private static void OpenSpoilersFolder()
        {
            var dir = Path.Combine(Environment.CurrentDirectory, "Spoilers");
            Directory.CreateDirectory(dir); // Does nothing if directory exists.
            System.Diagnostics.Process.Start(dir);
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
            Properties.Settings.Default.Seed = Randomize.GenerateSeed();
            Randomize.RestartRng();
        }

        private void StartForm_FormClosing(object sender, FormClosingEventArgs e)
        {
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
                if (!Directory.Exists(Properties.Settings.Default.Kotor1Path))
                {
                    // Kotor1Path directory doesn't exist.
                    MessageBox.Show(this, "Kotor 1 path does not exist. Please update your path settings.", "Path Error");
                    return;
                }
                else if (!File.Exists(Path.Combine(Properties.Settings.Default.Kotor1Path, "swkotor.exe")))
                {
                    // Kotor1Path directory doesn't contain swkotor.exe.
                    MessageBox.Show(this, "Kotor 1 path does not contain 'swkotor.exe' and is therefore an invalid directory. Please update your path settings.", "Path Error");
                    return;
                }

                if (Properties.Settings.Default.KotorIsRandomized)
                {
                    randomize_button.Text = "Randomize!";
                    generateSpoilersToolStripMenuItem.Enabled = false;
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
            OpenSpoilersFolder();
        }

        private void closeAllOtherWindowsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var formsToClose = new List<Form>();
            foreach (Form f in Application.OpenForms)
            {
                if (f is StartForm) continue;
                if (f is RandoForm && (f as RandoForm).IsInProgress) continue;
                formsToClose.Add(f);
            }

            for (int i = 0; i < formsToClose.Count; i++)
            {
                formsToClose[i].Close();
            }
        }
        #endregion
    }
}
