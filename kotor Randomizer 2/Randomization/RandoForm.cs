using System;
using System.Collections.Generic;
using System.IO;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;
using ClosedXML.Excel;

namespace kotor_Randomizer_2
{
    // This is no where near finished, and partially temporary
    public partial class RandoForm : Form
    {
        public RandoForm()
        {
            InitializeComponent();
        }

        public bool IsInProgress
        {
            get { return bwRandomizing.IsBusy || bwUnrandomizing.IsBusy || bwSpoilers.IsBusy; }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                const int PARAM_NOCLOSE = 0x200;
                CreateParams param = base.CreateParams;
                param.ClassStyle = param.ClassStyle | PARAM_NOCLOSE;
                return param;
            }
        }

        #region Private Properties

        private string curr_task = "";
        private bool RandomizationError = false;
        private bool SpoilerCreated = false;
        private string SpoilerMessage;

        // Class for easy access and auto-generation of Paths.
        private KPaths paths = new KPaths(Properties.Settings.Default.Kotor1Path);

        #endregion

        #region private methods

        // Runs the necessary Randomization Scripts
        private void RunRando()
        {
            // Final check for already randomized game before randomizing.
            if (File.Exists(paths.RANDOMIZED_LOG))
            {
                RandomizationError = true;
                MessageBox.Show(Properties.Resources.AlreadyRandomized, Properties.Resources.RandomizationError);
                return;
            }

            // Determine Step size and throw error if no categories are selected.
            int ActiveCategories = CountActiveCategories();

            if (ActiveCategories == 0)
            {
                RandomizationError = true;
                MessageBox.Show(Properties.Resources.ErrorNoRandomization, Properties.Resources.RandomizationError);
                return;
            }
            ActiveCategories++;

            int step_size = 100 / ActiveCategories;
            int curr_progress = 0;

            using (StreamWriter sw = new StreamWriter(paths.RANDOMIZED_LOG))
            {
                sw.WriteLine(DateTime.Now.ToString());
                sw.WriteLine(Properties.Resources.LogHeader);
                Properties.Settings.Default.KotorIsRandomized = true;
                Randomize.SetSeed(Properties.Settings.Default.Seed);    // Not sure when is the best time to set the seed.
                ResetRandomizationCategories();

                // Randomize the categories.
                try
                {
                    // Copy over custom appearance.2da to fix potential model and texture issues.
                    paths.BackUpOverrideDirectory();
                    File.WriteAllBytes(Path.Combine(paths.Override, "appearance.2da"), Properties.Resources.appearance);
                    File.WriteAllBytes(Path.Combine(paths.Override, "k_pdan_13_area.ncs"), Properties.Resources.k_pdan_13_area);

                    if (Properties.Settings.Default.DoRandomization_Module)
                    {
                        Randomize.RestartRng();
                        curr_task = Properties.Resources.RandomizingModules;
                        bwRandomizing.ReportProgress(curr_progress);
                        CreateModuleBackups();
                        ModuleRando.Module_rando(paths); // Run appropriate rando script.
                        sw.WriteLine(Properties.Resources.LogModulesDone);
                        curr_progress += step_size;
                    }
                    if (Properties.Settings.Default.DoRandomization_Item)
                    {
                        Randomize.RestartRng();
                        curr_task = Properties.Resources.RandomizingItems;
                        bwRandomizing.ReportProgress(curr_progress);
                        CreateItemBackups();
                        ItemRando.item_rando(paths); // Run appropriate rando script.
                        sw.WriteLine(Properties.Resources.LogItemsDone);
                        curr_progress += step_size;
                    }
                    if (Properties.Settings.Default.DoRandomization_Sound)
                    {
                        Randomize.RestartRng();
                        curr_task = Properties.Resources.RandomizingMusicSound;
                        bwRandomizing.ReportProgress(curr_progress);

                        // If music files are to be randomized, create backups.
                        if (Properties.Settings.Default.RandomizeAreaMusic     != (int)RandomizationLevel.None ||
                            Properties.Settings.Default.RandomizeAmbientNoise  != (int)RandomizationLevel.None ||
                            Properties.Settings.Default.RandomizeBattleMusic   != (int)RandomizationLevel.None ||
                            Properties.Settings.Default.RandomizeCutsceneNoise != (int)RandomizationLevel.None ||
                            Properties.Settings.Default.RemoveDmcaMusic)
                        {
                            CreateMusicBackups();
                        }

                        // If sound files are to be randomized, create backups.
                        if (Properties.Settings.Default.RandomizeAmbientNoise != (int)RandomizationLevel.None ||
                            Properties.Settings.Default.RandomizeBattleMusic  != (int)RandomizationLevel.None ||
                            Properties.Settings.Default.RandomizeNpcSounds    != (int)RandomizationLevel.None ||
                            Properties.Settings.Default.RandomizePartySounds  != (int)RandomizationLevel.None)
                        {
                            CreateSoundBackups();
                        }

                        SoundRando.sound_rando(paths); // Run appropriate rando script.
                        sw.WriteLine(Properties.Resources.LogMusicSoundDone);
                        curr_progress += step_size;
                    }
                    if (Properties.Settings.Default.DoRandomization_Model)
                    {
                        Randomize.RestartRng();
                        curr_task = Properties.Resources.RandomizingModels;
                        bwRandomizing.ReportProgress(curr_progress);
                        CreateModelBackups();
                        ModelRando.model_rando(paths); // Run appropriate rando script.
                        sw.WriteLine(Properties.Resources.LogModelsDone);
                        curr_progress += step_size;
                    }
                    if (Properties.Settings.Default.DoRandomization_Texture)
                    {
                        Randomize.RestartRng();
                        curr_task = Properties.Resources.RandomizingTextures;
                        bwRandomizing.ReportProgress(curr_progress);
                        CreateTextureBackups();
                        TextureRando.texture_rando(paths); // Run appropriate rando script.
                        sw.WriteLine(Properties.Resources.LogTexturesDone);
                        curr_progress += step_size;
                    }
                    if (Properties.Settings.Default.DoRandomization_TwoDA)
                    {
                        Randomize.RestartRng();
                        curr_task = Properties.Resources.Randomizing2DA;
                        bwRandomizing.ReportProgress(curr_progress);
                        CreateTwoDABackups();
                        TwodaRandom.Twoda_rando(paths); // Run appropriate rando script.
                        sw.WriteLine(Properties.Resources.Log2DADone);
                        curr_progress += step_size;
                    }
                    if (Properties.Settings.Default.DoRandomization_Text)
                    {
                        Randomize.RestartRng();
                        curr_task = Properties.Resources.RandomizingText;
                        bwRandomizing.ReportProgress(curr_progress);
                        CreateTextBackups();
                        TextRando.text_rando(paths); // Run appropriate rando script.
                        sw.WriteLine(Properties.Resources.LogTextDone);
                        curr_progress += step_size;
                    }
                    if (Properties.Settings.Default.DoRandomization_Other)
                    {
                        Randomize.RestartRng();
                        curr_task = Properties.Resources.RandomizingOther;
                        bwRandomizing.ReportProgress(curr_progress);
                        CreateOtherBackups();
                        OtherRando.other_rando(paths); // Run appropriate rando script.
                        sw.WriteLine(Properties.Resources.LogOtherDone);
                        curr_progress += step_size;
                    }
                }
                catch (Exception e)
                {
                    // Catch any randomization errors (e.g., reachability failure) and print a message.
                    RandomizationError = true;
                    MessageBox.Show($"Error encountered during randomization: {Environment.NewLine}{e.Message}", Properties.Resources.RandomizationError, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                finally
                {
                    // Creates a basic log file with a date, version, and things done.
                    curr_task = Properties.Resources.TaskFinishing;
                    bwRandomizing.ReportProgress(curr_progress);
                    sw.WriteLine("\nThe Kotor Randomizer was created by Lane Dibello, with help from Glasnonck, and the greater Kotor Speedrunning community.");
                    sw.WriteLine("If you encounter any issues please try to contact me @Lane#5847 on Discord");
                }
            }
            curr_progress += step_size;
        }

        // Unused - I'm keeping this around In case I try to tackle the release config issues again.
        private void UnRando_new()
        {
            if (!File.Exists(paths.RANDOMIZED_LOG))
            {
                MessageBox.Show(Properties.Resources.ErrorNotRandomized, Properties.Resources.RandomizationError);
                return;
            }

            // Restore any backup folders to their active directories.
            paths.RestoreModulesDirectory();
            paths.RestoreLipsDirectory();
            paths.RestoreOverrideDirectory();
            paths.RestoreMusicDirectory();
            paths.RestoreSoundsDirectory();
            paths.RestoreTexturePacksDirectory();
            paths.RestoreChitinFile();

            // Removing log file.
            File.Delete(paths.RANDOMIZED_LOG);
            Properties.Settings.Default.KotorIsRandomized = false;
        }

        // Unrandomizes Things
        private void UnRando()
        {
            if (!File.Exists(paths.RANDOMIZED_LOG))
            {
                MessageBox.Show(Properties.Resources.ErrorNotRandomized, Properties.Resources.RandomizationError);
                return;
            }

            try
            {
                int step_size = 12;
                int curr_progress = 0;
                bwUnrandomizing.ReportProgress(curr_progress);

                // Restore any backup folders to their active directories.
                curr_task = Properties.Resources.UnrandomizingModules;
                paths.RestoreModulesDirectory();
                bwUnrandomizing.ReportProgress(curr_progress += step_size);

                curr_task = Properties.Resources.UnrandomizingLips;
                paths.RestoreLipsDirectory();
                bwUnrandomizing.ReportProgress(curr_progress += step_size);

                curr_task = Properties.Resources.UnrandomizingOverrides;
                paths.RestoreOverrideDirectory();
                bwUnrandomizing.ReportProgress(curr_progress += step_size);

                curr_task = Properties.Resources.UnrandomizingMusic;
                paths.RestoreMusicDirectory();
                bwUnrandomizing.ReportProgress(curr_progress += step_size);

                curr_task = Properties.Resources.UnrandomizingSounds;
                paths.RestoreSoundsDirectory();
                bwUnrandomizing.ReportProgress(curr_progress += step_size);

                curr_task = Properties.Resources.UnrandomizingTextures;
                paths.RestoreTexturePacksDirectory();
                bwUnrandomizing.ReportProgress(curr_progress += step_size);

                curr_task = Properties.Resources.UnrandomizingKeyTable;
                paths.RestoreChitinFile();
                bwUnrandomizing.ReportProgress(curr_progress += step_size);

                curr_task = Properties.Resources.UnrandomizingTLKFile;
                paths.RestoreDialogFile();
                bwUnrandomizing.ReportProgress(curr_progress += step_size);

                // Removing log file.
                curr_task = Properties.Resources.TaskFinishing;
                File.Delete(paths.RANDOMIZED_LOG);
                Properties.Settings.Default.KotorIsRandomized = false;

                ResetRandomizationCategories();
            }
            catch (Exception e)
            {
                MessageBox.Show($"Exception caught while {curr_task}. {e.Message}", Properties.Resources.RandomizationError);
            }
        }

        public void CreateSpoilerLogs()
        {
            int step_size = 11;     // Calculation: 100 / (8 categories + savepath)
            int curr_progress = 0;  // 

            string spoilersPath = Path.Combine(Environment.CurrentDirectory, "Spoilers");
            //if (Directory.Exists(spoilersPath)) { Directory.Delete(spoilersPath, true); }
            Directory.CreateDirectory(spoilersPath);

            //var timestamp = DateTime.Now.ToString("yy-MM-dd_HH-mm-ss");
            var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");
            var filename = $"{timestamp}, Seed {Properties.Settings.Default.Seed}.xlsx";
            var path = Path.Combine(spoilersPath, filename);

            if (File.Exists(path)) { File.Delete(path); }

            try
            {
                using (var workbook = new XLWorkbook())
                {
                    curr_task = "Creating Spoilers - Item";
                    bwSpoilers.ReportProgress(curr_progress);
                    ItemRando.CreateSpoilerLog(workbook);

                    curr_task = "Creating Spoilers - Model";
                    bwSpoilers.ReportProgress(curr_progress += step_size);
                    ModelRando.CreateSpoilerLog(workbook);

                    curr_task = "Creating Spoilers - Module";
                    bwSpoilers.ReportProgress(curr_progress += step_size);
                    ModuleRando.CreateSpoilerLog(workbook);

                    curr_task = "Creating Spoilers - Music / Sound";
                    bwSpoilers.ReportProgress(curr_progress += step_size);
                    SoundRando.CreateSpoilerLog(workbook);

                    curr_task = "Creating Spoilers - Other";
                    bwSpoilers.ReportProgress(curr_progress += step_size);
                    OtherRando.CreateSpoilerLog(workbook);

                    curr_task = "Creating Spoilers - Text";
                    bwSpoilers.ReportProgress(curr_progress += step_size);
                    TextRando.CreateSpoilerLog(workbook);

                    curr_task = "Creating Spoilers - Texture";
                    bwSpoilers.ReportProgress(curr_progress += step_size);
                    TextureRando.CreateSpoilerLog(workbook);

                    curr_task = "Creating Spoilers - 2DA";
                    bwSpoilers.ReportProgress(curr_progress += step_size);
                    TwodaRandom.CreateSpoilerLog(workbook);

                    curr_task = "Creating Spoilers - Saving File";
                    bwSpoilers.ReportProgress(curr_progress += step_size);

                    // If any worksheets have been added, save the spoiler log.
                    if (workbook.Worksheets.Count > 0)
                    {
                        StringBuilder wsList = new StringBuilder();
                        foreach (var sheet in workbook.Worksheets)
                        {
                            wsList.Append($"{sheet.Name}, ");
                        }

                        wsList.Remove(wsList.Length - 2, 2);
                        workbook.SaveAs(path);

                        SpoilerCreated = true;
                    }
                    else
                    {
                        SpoilerCreated = false;
                        SpoilerMessage = $"No spoilers created. Either the game has not been randomized, or the selected randomizations do not generate spoilers.";
                    }
                }
            }
            catch (Exception e)
            {
                SpoilerCreated = false;
                SpoilerMessage = $"Exception caught while creating spoilers: {e.ToString()}";
            }
        }

        private void ResetRandomizationCategories()
        {
            ModuleRando.Reset();
            ItemRando.Reset();
            SoundRando.Reset();
            ModelRando.Reset();
            TextureRando.Reset();
            TwodaRandom.Reset();
            TextRando.Reset();
            OtherRando.Reset();
        }

        private int CountActiveCategories()
        {
            int i = 0;
            if (Properties.Settings.Default.DoRandomization_Module) { i++; }
            if (Properties.Settings.Default.DoRandomization_Item) { i++; }
            if (Properties.Settings.Default.DoRandomization_Sound) { i++; }
            if (Properties.Settings.Default.DoRandomization_Model) { i++; }
            if (Properties.Settings.Default.DoRandomization_Texture) { i++; }
            if (Properties.Settings.Default.DoRandomization_TwoDA) { i++; }
            if (Properties.Settings.Default.DoRandomization_Text) { i++; }
            if (Properties.Settings.Default.DoRandomization_Other) { i++; }
            return i;
        }

        private void CreateModuleBackups()
        {
            paths.BackUpModulesDirectory();
            paths.BackUpLipsDirectory();
            paths.BackUpOverrideDirectory();
        }

        private void CreateItemBackups()
        {
            paths.BackUpChitinFile();
        }

        private void CreateMusicBackups()
        {
            paths.BackUpMusicDirectory();
        }

        private void CreateSoundBackups()
        {
            paths.BackUpSoundDirectory();
        }

        private void CreateModelBackups()
        {
            paths.BackUpModulesDirectory();
        }

        private void CreateTextureBackups()
        {
            paths.BackUpTexturesDirectory();
        }

        private void CreateTwoDABackups()
        {
            paths.BackUpChitinFile();
            paths.BackUpOverrideDirectory();
        }

        private void CreateTextBackups()
        {
            paths.BackUpDialogFile();
            paths.BackUpModulesDirectory();
        }

        private void CreateOtherBackups()
        {
            paths.BackUpChitinFile();
            paths.BackUpOverrideDirectory();
        }

        #endregion

        #region Events

        // Form Events
        private void RandoForm_Shown(object sender, EventArgs e)
        {
            if (!Properties.Settings.Default.KotorIsRandomized) { bwRandomizing.RunWorkerAsync(); }
            else { bwUnrandomizing.RunWorkerAsync(); }
        }

        // Button Events
        private void bDone_Click(object sender, EventArgs e)
        {
            // Avoid closing the window if background workers are still running.
            if (!IsInProgress) Close();
        }

        private void bSpoilers_Click(object sender, EventArgs e)
        {
            StartForm.OpenSpoilersFolder();
        }

        // BW Randomizing Events
        private void bwRandomizing_DoWork(object sender, DoWorkEventArgs e)
        {
            RunRando();
        }

        private void bwRandomizing_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            RandomizationProgress.Value = e.ProgressPercentage;
            currentRandoTask_label.Text = curr_task;
        }

        private void bwRandomizing_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            currentRandoTask_label.Text = Properties.Resources.Randomized;
            RandomizationProgress.Value = 100;

            // Don't create spoilers if there was an error during randomization.
            if (!RandomizationError)
            {
                // Automatically create spoilers if desired.
                if (Properties.Settings.Default.AutoGenerateSpoilers)
                {
                    bwSpoilers.RunWorkerAsync();
                }
                else
                {
                    // Ask about creating spoilers. If no, enabled the "Close" button.
                    var result = MessageBox.Show(Properties.Resources.CreateSpoilerLogQuestion, Properties.Resources.CreateSpoilerLog, MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes) bwSpoilers.RunWorkerAsync();
                    else bDone.Enabled = true;
                }
            }
            else
            {
                bDone.Enabled = true;
            }
        }

        // BW Unrandomizing Events
        private void bwUnrandomizing_DoWork(object sender, DoWorkEventArgs e)
        {
            UnRando();
        }

        private void bwUnrandomizing_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            RandomizationProgress.Value = e.ProgressPercentage;
            currentRandoTask_label.Text = curr_task;
        }

        private void bwUnrandomizing_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            bDone.Enabled = true;
            currentRandoTask_label.Text = Properties.Resources.Unrandomized;
            RandomizationProgress.Value = 100;
        }

        // BW Spoilers Events
        private void bwSpoilers_DoWork(object sender, DoWorkEventArgs e)
        {
            // Ensure the "Close" button is disabled, then create the spoiler logs.
            bDone.Invoke((MethodInvoker) delegate { bDone.Enabled = false; });
            CreateSpoilerLogs();
        }

        private void bwSpoilers_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            RandomizationProgress.Value = e.ProgressPercentage;
            currentRandoTask_label.Text = curr_task;
        }

        private void bwSpoilers_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            RandomizationProgress.Value = 100;
            bSpoilers.Enabled = true;
            bSpoilers.Visible = true;

            if (SpoilerCreated)
            {
                currentRandoTask_label.Text = Properties.Resources.RandomizedWithSpoilers;
            }
            else
            {
                // If spoilers weren't generated, then an error occurred. Display the message to the user.
                currentRandoTask_label.Text = Properties.Resources.Randomized;
                var result = MessageBox.Show(SpoilerMessage, Properties.Resources.CreateSpoilerLog, MessageBoxButtons.OK);
            }

            bDone.Enabled = true;
        }

        #endregion
    }
}
