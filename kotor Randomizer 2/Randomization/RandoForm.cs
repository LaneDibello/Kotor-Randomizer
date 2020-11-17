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

namespace kotor_Randomizer_2
{
    // This is no where near finished, and partially temporary
    public partial class RandoForm : Form
    {
        public RandoForm()
        {
            InitializeComponent();
        }

        #region Private Properties

        private string curr_task = "";

        // Class for easy access and auto-generation of Paths.
        private KPaths paths = new KPaths(Properties.Settings.Default.Kotor1Path);

        #endregion

        #region private methods

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

        private void CreateSoundBackups()
        {
            paths.BackUpMusicDirectory();
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
            // Not yet implemented.
        }

        private void CreateOtherBackups()
        {
            paths.BackUpChitinFile();
            paths.BackUpOverrideDirectory();
        }

        // Runs the necessary Randomization Scripts
        private void RunRando()
        {
            // Determine Step size and throw error if no categories are selected.
            int ActiveCategories = CountActiveCategories();

            if (ActiveCategories == 0)
            {
                MessageBox.Show(Properties.Resources.ErrorNoRandomization);
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

                // Randomize the categories.
                if (Properties.Settings.Default.module_rando_active)
                {
                    Randomize.RestartRng();
                    curr_task = Properties.Resources.RandomizingModules;
                    bwRandomizing.ReportProgress(curr_progress);
                    CreateModuleBackups();
                    ModuleRando.Module_rando(paths); // Run appropriate rando script.
                    sw.WriteLine(Properties.Resources.LogModulesDone);
                    curr_progress += step_size;
                }
                if (Properties.Settings.Default.item_rando_active)
                {
                    Randomize.RestartRng();
                    curr_task = Properties.Resources.RandomizingItems;
                    bwRandomizing.ReportProgress(curr_progress);
                    CreateItemBackups();
                    ItemRando.item_rando(paths); // Run appropriate rando script.
                    sw.WriteLine(Properties.Resources.LogItemsDone);
                    curr_progress += step_size;
                }
                if (Properties.Settings.Default.sound_rando_active)
                {
                    Randomize.RestartRng();
                    curr_task = Properties.Resources.RandomizingMusicSound;
                    bwRandomizing.ReportProgress(curr_progress);
                    CreateSoundBackups();
                    SoundRando.sound_rando(paths); // Run appropriate rando script.
                    sw.WriteLine(Properties.Resources.LogMusicSoundDone);
                    curr_progress += step_size;
                }
                if (Properties.Settings.Default.model_rando_active)
                {
                    Randomize.RestartRng();
                    curr_task = Properties.Resources.RandomizingModels;
                    bwRandomizing.ReportProgress(curr_progress);
                    CreateModelBackups();
                    ModelRando.model_rando(paths); // Run appropriate rando script.
                    sw.WriteLine(Properties.Resources.LogModelsDone);
                    curr_progress += step_size;
                }
                if (Properties.Settings.Default.texture_rando_active)
                {
                    Randomize.RestartRng();
                    curr_task = Properties.Resources.RandomizingTextures;
                    bwRandomizing.ReportProgress(curr_progress);
                    CreateTextureBackups();
                    TextureRando.texture_rando(paths); // Run appropriate rando script.
                    sw.WriteLine(Properties.Resources.LogTexturesDone);
                    curr_progress += step_size;
                }
                if (Properties.Settings.Default.twoda_rando_active)
                {
                    Randomize.RestartRng();
                    curr_task = Properties.Resources.Randomizing2DA;
                    bwRandomizing.ReportProgress(curr_progress);
                    CreateTwoDABackups();
                    TwodaRandom.Twoda_rando(paths); // Run appropriate rando script.
                    sw.WriteLine(Properties.Resources.Log2DADone);
                    curr_progress += step_size;
                }
                if (Properties.Settings.Default.text_rando_active)
                {
                    Randomize.RestartRng();
                    curr_task = Properties.Resources.RandomizingText;
                    bwRandomizing.ReportProgress(curr_progress);
                    CreateTextBackups();
                    // ** Not yet implemented. ** // Run appropriate rando script.
                    sw.WriteLine(Properties.Resources.LogTextDone);
                    curr_progress += step_size;
                }
                if (Properties.Settings.Default.other_rando_active)
                {
                    Randomize.RestartRng();
                    curr_task = Properties.Resources.RandomizingOther;
                    bwRandomizing.ReportProgress(curr_progress);
                    CreateOtherBackups();
                    OtherRando.other_rando(paths); // Run appropriate rando script.
                    sw.WriteLine(Properties.Resources.LogOtherDone);
                    curr_progress += step_size;
                }

                // Creates a basic log file with a date, version, and things done.
                curr_task = Properties.Resources.TaskFinishing;
                bwRandomizing.ReportProgress(curr_progress);
                sw.WriteLine("\nThe Kotor Randomizer was created by Lane Dibello, with help from Glasnonck, and the greater Kotor Speedrunning community.");
                sw.WriteLine("If you encounter any issues please try to contact me @Lane#5847 on Discord");
            }
            curr_progress += step_size;
        }

        // Unused - I'm keeping this around In case I try to tackle the release config issues again.
        private void UnRando_new()
        {
            if (!File.Exists(paths.RANDOMIZED_LOG))
            {
                MessageBox.Show(Properties.Resources.ErrorNotRandomized);
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

        // Unrandomizes Things **CURRENTLY BROKEN IN RELEASE BUILDS**
        private void UnRando()
        {
            if (!File.Exists(paths.RANDOMIZED_LOG))
            {
                MessageBox.Show(Properties.Resources.ErrorNotRandomized);
                return;
            }

            try
            {
                int step_size = 13;
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

                // Removing log file.
                curr_task = Properties.Resources.TaskFinishing;
                File.Delete(paths.RANDOMIZED_LOG);
                Properties.Settings.Default.KotorIsRandomized = false;
            }
            catch (Exception e)
            {
                MessageBox.Show($"Exception caught while {curr_task}. {e.Message}");
            }
        }

        private int CountActiveCategories()
        {
            int i = 0;
            if (Properties.Settings.Default.module_rando_active) { i++; }
            if (Properties.Settings.Default.item_rando_active) { i++; }
            if (Properties.Settings.Default.sound_rando_active) { i++; }
            if (Properties.Settings.Default.model_rando_active) { i++; }
            if (Properties.Settings.Default.texture_rando_active) { i++; }
            if (Properties.Settings.Default.twoda_rando_active) { i++; }
            if (Properties.Settings.Default.text_rando_active) { i++; }
            if (Properties.Settings.Default.other_rando_active) { i++; }
            return i;
        }

        #endregion

        #region Events

        private void bDone_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void bwRandomizing_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            bDone.Enabled = true;
            currentRandoTask_label.Text = Properties.Resources.Done;
            RandomizationProgress.Value = 100;
        }

        private void bwRandomizing_DoWork(object sender, DoWorkEventArgs e)
        {
            RunRando();
        }

        private void bwRandomizing_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            RandomizationProgress.Value = e.ProgressPercentage;
            currentRandoTask_label.Text = curr_task;
        }

        private void RandoForm_Shown(object sender, EventArgs e)
        {
            if (!Properties.Settings.Default.KotorIsRandomized) { bwRandomizing.RunWorkerAsync(); }
            else
            {
                bwUnrandomizing.RunWorkerAsync();

                //TEMPORARY
                //RandomizationProgress.Style = ProgressBarStyle.Marquee;
                //UnRando_new(); //Until I figure out what the hell is wrong with the other one.
                //RandomizationProgress.Style = ProgressBarStyle.Continuous;
                //bDone.Enabled = true;
                //currentRandoTask_label.Text = "Done!";
                //RandomizationProgress.Value = 100;
            }
        }

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
            currentRandoTask_label.Text = Properties.Resources.Done;
            RandomizationProgress.Value = 100;
        }

        #endregion
    }
}
