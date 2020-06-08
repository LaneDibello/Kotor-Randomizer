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
    //This is no where near finished, and partially temporary
    public partial class RandoForm : Form
    {

        public RandoForm()
        {
            InitializeComponent();
        }

        #region Private Properties

        private string curr_task = "";

        //Class for easy access and auto-generation of Paths
        private Globals.KPaths paths = new Globals.KPaths(Properties.Settings.Default.Kotor1Path);

        #endregion

        #region private methods

        //Creates the back-ups for teh passed category
        private void CreateBackUps(string category)
        {
            DirectoryInfo modules_dir = new DirectoryInfo(paths.modules);
            FileInfo[] modules_files = modules_dir.GetFiles();
            DirectoryInfo lips_dir = new DirectoryInfo(paths.lips);
            FileInfo[] lips_files = lips_dir.GetFiles();
            DirectoryInfo music_dir = new DirectoryInfo(paths.music);
            FileInfo[] music_files = music_dir.GetFiles();
            DirectoryInfo sounds_dir = new DirectoryInfo(paths.sounds);
            FileInfo[] sounds_files = sounds_dir.GetFiles();
            DirectoryInfo override_dir = new DirectoryInfo(paths.Override);
            FileInfo[] override_files = override_dir.GetFiles();
            DirectoryInfo Texture_dir = new DirectoryInfo(paths.TexturePacks);
            FileInfo[] Texture_files = Texture_dir.GetFiles();

            switch (category)
            {
                case "module":

                    if (!Directory.Exists(paths.get_backup(paths.modules)))
                    {
                        Directory.CreateDirectory(paths.get_backup(paths.modules));

                        foreach (FileInfo file in modules_files)
                        {
                            string temppath = paths.get_backup(paths.modules) + file.Name;
                            file.CopyTo(temppath, true);
                        }
                    }

                    if (!Directory.Exists(paths.get_backup(paths.lips)))
                    {
                        Directory.CreateDirectory(paths.get_backup(paths.lips));

                        foreach (FileInfo file in lips_files)
                        {
                            string temppath = paths.get_backup(paths.lips) + file.Name;
                            file.CopyTo(temppath, true);
                        }
                    }

                    if (!Directory.Exists(paths.get_backup(paths.Override)))
                    {
                        Directory.CreateDirectory(paths.get_backup(paths.Override));

                        foreach (FileInfo file in override_files)
                        {
                            string temppath = paths.get_backup(paths.Override) + file.Name;
                            file.CopyTo(temppath, true);
                        }
                    }
                    break;
                case "item":
                    if (!File.Exists(paths.get_backup(paths.chitin)))
                    {
                        File.Copy(paths.chitin, paths.get_backup(paths.chitin));
                    }
                    break;
                case "sound":

                    if (!Directory.Exists(paths.get_backup(paths.music)))
                    {
                        Directory.CreateDirectory(paths.get_backup(paths.music));

                        foreach (FileInfo file in music_files)
                        {
                            string temppath = paths.get_backup(paths.music) + file.Name;
                            file.CopyTo(temppath, true);
                        }
                    }

                    if (!Directory.Exists(paths.get_backup(paths.sounds)))
                    {
                        Directory.CreateDirectory(paths.get_backup(paths.sounds));

                        foreach (FileInfo file in sounds_files)
                        {
                            string temppath = paths.get_backup(paths.sounds) + file.Name;
                            file.CopyTo(temppath, true);
                        }
                    }
                    break;
                case "model":
                    if (!Directory.Exists(paths.get_backup(paths.modules)))
                    {
                        Directory.CreateDirectory(paths.get_backup(paths.modules));

                        foreach (FileInfo file in modules_files)
                        {
                            string temppath = paths.get_backup(paths.modules) + file.Name;
                            file.CopyTo(temppath, true);
                        }
                    }
                    break;
                case "texture":
                    if (!Directory.Exists(paths.get_backup(paths.TexturePacks)))
                    {
                        Directory.CreateDirectory(paths.get_backup(paths.TexturePacks));

                        foreach (FileInfo file in Texture_files)
                        {
                            string temppath = paths.get_backup(paths.TexturePacks) + file.Name;
                            file.CopyTo(temppath, true);
                        }
                    }
                    break;
                case "twoda":
                    if (!File.Exists(paths.get_backup(paths.chitin)))
                    {
                        File.Copy(paths.chitin, paths.get_backup(paths.chitin));
                    }
                    if (!Directory.Exists(paths.get_backup(paths.Override)))
                    {
                        Directory.CreateDirectory(paths.get_backup(paths.Override));

                        foreach (FileInfo file in override_files)
                        {
                            string temppath = paths.get_backup(paths.Override) + file.Name;
                            file.CopyTo(temppath, true);
                        }
                    }
                    break;
                case "text":
                    break;
                case "other":
                    if (!Directory.Exists(paths.get_backup(paths.Override)))
                    {
                        Directory.CreateDirectory(paths.get_backup(paths.Override));

                        foreach (FileInfo file in override_files)
                        {
                            string temppath = paths.get_backup(paths.Override) + file.Name;
                            file.CopyTo(temppath, true);
                        }

                        if (!File.Exists(paths.get_backup(paths.chitin)))
                        {
                            File.Copy(paths.chitin, paths.get_backup(paths.chitin));
                        }
                    }
                    break;
            }
        }

        //Runs the necessary Randomization Scripts
        private void RunRando()
        {
            //Determine Step size and throw error if no categories are selected.
            int ActiveCats = CountActiveCategories();

            if (ActiveCats == 0)
            {
                MessageBox.Show("No Randomization Categories Selected");
                return;
            }
            ActiveCats++;

            int step_size = 100 / ActiveCats;
            int curr_progress = 0;

            //Rando Categories
            if (Properties.Settings.Default.module_rando_active)
            {
                curr_task = "Randomizing Modules";
                bwRandomizing.ReportProgress(curr_progress);
                CreateBackUps("module");
                ModuleForm.Module_rando(paths);//run appropriate rando script
                curr_progress += step_size;
            }
            if (Properties.Settings.Default.item_rando_active)
            {
                curr_task = "Randomizing Items";
                bwRandomizing.ReportProgress(curr_progress);
                CreateBackUps("item");
                ItemRando.item_rando(paths);//run appropriate rando script
                curr_progress += step_size;
            }
            if (Properties.Settings.Default.sound_rando_active)
            {
                curr_task = "Randomizing Music and Sounds";
                bwRandomizing.ReportProgress(curr_progress);
                CreateBackUps("sound");
                SoundRando.sound_rando(paths);//run appropriate rando script
                curr_progress += step_size;
            }
            if (Properties.Settings.Default.model_rando_active)
            {
                curr_task = "Randomizing Models";
                bwRandomizing.ReportProgress(curr_progress);
                CreateBackUps("model");
                ModelRando.model_rando(paths);//run appropriate rando script
                curr_progress += step_size;
            }
            if (Properties.Settings.Default.texture_rando_active)
            {
                curr_task = "Randomizing Textures";
                bwRandomizing.ReportProgress(curr_progress);
                CreateBackUps("texture");
                TextureRando.texture_rando(paths);//run appropriate rando script
                curr_progress += step_size;
            }
            if (Properties.Settings.Default.twoda_rando_active)
            {
                curr_task = "Randomizing 2-D Arrays";
                bwRandomizing.ReportProgress(curr_progress);
                CreateBackUps("twoda");
                TwodaRandom.Twoda_rando(paths);//run appropriate rando script
                curr_progress += step_size;
            }
            if (Properties.Settings.Default.text_rando_active)
            {
                curr_task = "Randomizing Text";
                bwRandomizing.ReportProgress(curr_progress);
                CreateBackUps("text");
                //run appropriate rando script
                curr_progress += step_size;
            }
            if (Properties.Settings.Default.other_rando_active)
            {
                curr_task = "Randomizing Other Things";
                bwRandomizing.ReportProgress(curr_progress);
                CreateBackUps("other");
                OtherRando.other_rando(paths);//run appropriate rando script
                curr_progress += step_size;
            }

            Properties.Settings.Default.KotorIsRandomized = true;

            //Creates a basic log file with a date, version, and things done.
            curr_task = "Finishing Up";
            bwRandomizing.ReportProgress(curr_progress);
            StreamWriter sw = new StreamWriter(paths.swkotor + "RANDOMIZED.log");
            sw.WriteLine(DateTime.Now.ToString());
            sw.WriteLine("Kotor Randomizer V2.0\n");
            if (Properties.Settings.Default.module_rando_active) { sw.WriteLine("Modules Randomized"); }
            if (Properties.Settings.Default.item_rando_active) { sw.WriteLine("Items Randomized"); }
            if (Properties.Settings.Default.sound_rando_active) { sw.WriteLine("Sounds Randomized"); }
            if (Properties.Settings.Default.model_rando_active) { sw.WriteLine("Models Randomized"); }
            if (Properties.Settings.Default.texture_rando_active) { sw.WriteLine("Textures Randomized"); }
            if (Properties.Settings.Default.twoda_rando_active) { sw.WriteLine("2-Dimensional Arrays Randomized"); }
            if (Properties.Settings.Default.text_rando_active) { sw.WriteLine("Text Randomized"); }
            if (Properties.Settings.Default.other_rando_active) { sw.WriteLine("\'Other\' Randomized"); }
            sw.WriteLine("\nThe Kotor Randomizer was created by Lane Dibello, with help from Glasnonck, and the greater Kotor Speedrunning community.");
            sw.WriteLine("If you encounter any issues please try to contact me @Lane#5847 on Discord");
            sw.Close();
            curr_progress += step_size;
        }

        //Unused - I'm keeping this around In case I try to tackle the release config issues again
        private void UnRando_new()
        {
            if (!File.Exists(Properties.Settings.Default.Kotor1Path + "\\RANDOMIZED.log"))
            {
                MessageBox.Show("Game is not randomized!");
                return;
            }

            //Checks for and loads back-up folders
            if (Directory.Exists(paths.get_backup(paths.modules)))
            {
                Directory.Delete(paths.modules, true);
                Directory.Move(paths.get_backup(paths.modules), paths.modules);
            }
            if (Directory.Exists(paths.get_backup(paths.lips)))
            {
                Directory.Delete(paths.lips, true);
                Directory.Move(paths.get_backup(paths.lips), paths.lips);
            }
            if (Directory.Exists(paths.get_backup(paths.Override)))
            {
                Directory.Delete(paths.Override, true);
                Directory.Move(paths.get_backup(paths.Override), paths.Override);
            }
            if (Directory.Exists(paths.get_backup(paths.music)))
            {
                Directory.Delete(paths.music, true);
                Directory.Move(paths.get_backup(paths.music), paths.music);
            }
            if (Directory.Exists(paths.get_backup(paths.sounds)))
            {
                Directory.Delete(paths.sounds, true);
                Directory.Move(paths.get_backup(paths.sounds), paths.sounds);
            }
            if (Directory.Exists(paths.get_backup(paths.TexturePacks)))
            {
                Directory.Delete(paths.TexturePacks, true);
                Directory.Move(paths.get_backup(paths.TexturePacks), paths.TexturePacks);
            }
            if (File.Exists(paths.get_backup(paths.chitin)))
            {
                File.Delete(paths.chitin);
                File.Move(paths.get_backup(paths.chitin), paths.chitin);
            }

            //Removing log file
            File.Delete(paths.swkotor + "RANDOMIZED.log");

            Properties.Settings.Default.KotorIsRandomized = false;

        }

        //Unrandomizes Things **CURRENTLY BROKEN IN RELEASE BUILDS**
        private void UnRando()
        {
            if (!File.Exists(Properties.Settings.Default.Kotor1Path + "\\RANDOMIZED.log"))
            {
                MessageBox.Show("Game is not randomized!");
                return;
            }

            int step_size = 13;
            int curr_progress = 0;

            //Checks for and loads back-up folders
            if (Directory.Exists(paths.get_backup(paths.modules)))
            {
                curr_task = "Unrandomizing Modules";
                bwUnrandomizing.ReportProgress(curr_progress);
                Directory.Delete(paths.modules, true);
                Directory.Move(paths.get_backup(paths.modules), paths.modules);
                curr_progress += step_size;
            }
            if (Directory.Exists(paths.get_backup(paths.lips)))
            {
                curr_task = "Unrandomizing Lips";
                bwUnrandomizing.ReportProgress(curr_progress);
                Directory.Delete(paths.lips, true);
                Directory.Move(paths.get_backup(paths.lips), paths.lips);
                curr_progress += step_size;
            }
            if (Directory.Exists(paths.get_backup(paths.Override)))
            {
                curr_task = "Unrandomizing Overrides";
                bwUnrandomizing.ReportProgress(curr_progress);
                Directory.Delete(paths.Override, true);
                Directory.Move(paths.get_backup(paths.Override), paths.Override);
                curr_progress += step_size;
            }
            if (Directory.Exists(paths.get_backup(paths.music)))
            {
                curr_task = "Unrandomizing Msuic";
                bwUnrandomizing.ReportProgress(curr_progress);
                Directory.Delete(paths.music, true);
                Directory.Move(paths.get_backup(paths.music), paths.music);
                curr_progress += step_size;
            }
            if (Directory.Exists(paths.get_backup(paths.sounds)))
            {
                curr_task = "Unrandomizing Sounds";
                bwUnrandomizing.ReportProgress(curr_progress);
                Directory.Delete(paths.sounds, true);
                Directory.Move(paths.get_backup(paths.sounds), paths.sounds);
                curr_progress += step_size;
            }
            if (Directory.Exists(paths.get_backup(paths.TexturePacks)))
            {
                curr_task = "Unrandomizing Textures";
                bwUnrandomizing.ReportProgress(curr_progress);
                Directory.Delete(paths.TexturePacks, true);
                Directory.Move(paths.get_backup(paths.TexturePacks), paths.TexturePacks);
                curr_progress += step_size;
            }
            if (File.Exists(paths.get_backup(paths.chitin)))
            {
                curr_task = "Unrandomizing Key Table";
                bwUnrandomizing.ReportProgress(curr_progress);
                File.Delete(paths.chitin);
                File.Move(paths.get_backup(paths.chitin), paths.chitin);
                curr_progress += step_size;
            }

            //Removing log file
            curr_task = "Finishing Up";
            bwUnrandomizing.ReportProgress(curr_progress);
            File.Delete(paths.swkotor + "RANDOMIZED.log");

            Properties.Settings.Default.KotorIsRandomized = false;

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
            currentRandoTask_label.Text = "Done!";
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
            currentRandoTask_label.Text = "Done!";
            RandomizationProgress.Value = 100;
        }

        #endregion
    }
}
