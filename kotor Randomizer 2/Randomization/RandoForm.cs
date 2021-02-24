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

        // Runs the necessary Randomization Scripts
        private void RunRando()
        {
            // Determine Step size and throw error if no categories are selected.
            int ActiveCategories = CountActiveCategories();

            if (ActiveCategories == 0)
            {
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

        // Unrandomizes Things **CURRENTLY BROKEN IN RELEASE BUILDS**
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
