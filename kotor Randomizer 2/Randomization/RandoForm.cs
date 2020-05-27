using System;
using System.Collections.Generic;
using System.IO;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kotor_Randomizer_2
{
    //This is no where near finished, and partially temporary
    public partial class RandoForm : Form
    {
        public RandoForm()
        {
            InitializeComponent();

            if (!Properties.Settings.Default.KotorIsRandomized) { RunRando(); }
            else { UnRando(); }

        }

        //Class for easy access and auto-generation of Paths
        Globals.KPaths paths = new Globals.KPaths(Properties.Settings.Default.Kotor1Path);

        //Creates the back-ups for teh passed category
        private void CreateBackUps(string category)
        {
            switch (category)
            {
                case "module":
                    DirectoryInfo modules_dir = new DirectoryInfo(paths.modules);
                    FileInfo[] modules_files = modules_dir.GetFiles();
                    DirectoryInfo lips_dir = new DirectoryInfo(paths.lips);
                    FileInfo[] lips_files = lips_dir.GetFiles();

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
                    break;
                case "item":
                    if (!File.Exists(paths.get_backup(paths.chitin)))
                    {
                        File.Copy(paths.chitin, paths.get_backup(paths.chitin));
                    }
                    break;
                case "sound":
                    DirectoryInfo music_dir = new DirectoryInfo(paths.music);
                    FileInfo[] music_files = music_dir.GetFiles();
                    DirectoryInfo sounds_dir = new DirectoryInfo(paths.sounds);
                    FileInfo[] sounds_files = sounds_dir.GetFiles();

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
                    break;
                case "texture":
                    break;
                case "twoda":
                    break;
                case "text":
                    break;
                case "other":
                    break;
            }
        }

        private void RunRando()
        {

            //TEMPORARY - PREVENT EXECUTION
            //return;
            //******************************

            int ActiveCats = CountActiveCategories();

            if (ActiveCats == 0)
            {
                MessageBox.Show("No Randomization Categories Selected");
                ActiveCats++;
            }

            RandomizationProgress.Step = 100 / ActiveCats;

            #region Module Randomization
            if (Properties.Settings.Default.module_rando_active)
            {
                currentRandoTask_label.Text = "Randomizing Modules";
                CreateBackUps("module");
                //run appropriate rando script
                List<string> Randomized_modules = new List<string>();
                if (ModuleForm.Module_rando(out Randomized_modules))
                {
                    switch (Properties.Settings.Default.ModuleSaveStatus)
                    {
                        case 0:
                            File.WriteAllBytes(paths.Override + "modulesave.2da", Properties.Resources.NODELETE_modulesave);
                            break;
                        default:
                        case 1:
                            //This is kotor's default configuration
                            break;
                        case 2:
                            File.WriteAllBytes(paths.Override + "modulesave.2da", Properties.Resources.NODELETE_MGINCLUDED_modulesave);
                            break;
                        case 3:
                            File.WriteAllBytes(paths.Override + "modulesave.2da", Properties.Resources.MGINCLUDED_modulesave);
                            break;
                        case 6:
                            File.WriteAllBytes(paths.Override + "modulesave.2da", Properties.Resources.ALLINCLUDED_modulesave);
                            break;
                        case 7:
                            File.WriteAllBytes(paths.Override + "modulesave.2da", Properties.Resources.NODELETE_ALLINCLUDED_modulesave);
                            break;
                    }

                    if (Properties.Settings.Default.AddOverideFiles.Contains("k_ren_visionland.ncs"))
                    {
                        File.WriteAllBytes(paths.Override + "k_ren_visionland.ncs", Properties.Resources.k_ren_visionland);
                    }

                    if (Properties.Settings.Default.AddOverideFiles.Contains("k_ren_visionland.ncs"))
                    {
                        File.WriteAllBytes(paths.Override + "k_pebn_galaxy.ncs", Properties.Resources.k_pebn_galaxy);
                    }
                }

                int k = 0;
                foreach (string M in Globals.BoundModules.Where(x => !x.ommitted).Select(x => x.name))
                {
                    File.Copy(paths.get_backup(paths.modules) + M + ".rim", paths.modules + Randomized_modules[k] + ".rim", true);
                    File.Copy(paths.get_backup(paths.modules) + M + "_s.rim", paths.modules + Randomized_modules[k] + "_s.rim", true);
                    File.Copy(paths.get_backup(paths.lips) + M + "_loc.mod", paths.lips + Randomized_modules[k] + "_loc.mod", true);
                    k++;
                }

                foreach (string M in Globals.BoundModules.Where(x => x.ommitted).Select(x => x.name))
                {
                    File.Copy(paths.get_backup(paths.modules) + M + ".rim", paths.modules + M + ".rim", true);
                    File.Copy(paths.get_backup(paths.modules) + M + "_s.rim", paths.modules + M + "_s.rim", true);
                    File.Copy(paths.get_backup(paths.lips) + M + "_loc.mod", paths.lips + M + "_loc.mod", true);
                }

                foreach (string L in Globals.lipXtras)
                {
                    File.Copy(paths.get_backup(paths.lips) + L,  paths.lips + L, true);
                }

                RandomizationProgress.PerformStep();
            }
            #endregion
            if (Properties.Settings.Default.item_rando_active)
            {
                currentRandoTask_label.Text = "Randomizing Items";
                CreateBackUps("item");
                //run appropriate rando script
                RandomizationProgress.PerformStep();
            }
            if (Properties.Settings.Default.sound_rando_active)
            {
                currentRandoTask_label.Text = "Randomizing Music and Sounds";
                CreateBackUps("sound");
                //run appropriate rando script
                RandomizationProgress.PerformStep();
            }
            if (Properties.Settings.Default.model_rando_active)
            {
                currentRandoTask_label.Text = "Randomizing Models";
                CreateBackUps("model");
                //run appropriate rando script
                RandomizationProgress.PerformStep();
            }
            if (Properties.Settings.Default.texture_rando_active)
            {
                currentRandoTask_label.Text = "Randomizing Textures";
                CreateBackUps("texture");
                //run appropriate rando script
                RandomizationProgress.PerformStep();
            }
            if (Properties.Settings.Default.twoda_rando_active)
            {
                currentRandoTask_label.Text = "Randomizing 2-D Arrays";
                CreateBackUps("twoda");
                //run appropriate rando script
                RandomizationProgress.PerformStep();
            }
            if (Properties.Settings.Default.text_rando_active)
            {
                currentRandoTask_label.Text = "Randomizing Text";
                CreateBackUps("text");
                //run appropriate rando script
                RandomizationProgress.PerformStep();
            }
            if (Properties.Settings.Default.other_rando_active)
            {
                currentRandoTask_label.Text = "Randomizing Other Things";
                CreateBackUps("other");
                //run appropriate rando script
                RandomizationProgress.PerformStep();
            }

            currentRandoTask_label.Text = "Done!";
            //The Last Step in the Rando Process should be to set the progress value to 100
            RandomizationProgress.Value = 100;
        }

        private void UnRando()
        {
            //Figure out how many rando categories have been done
            //Consider placing a file in swkotor during the rando process the details waht was done so it can be more easily un-done.

            //Set the step to 100/those categories
            //RandomizationProgress.Step = 100 / ActiveCats;

            //Undo each category
            //if (CategoryRandomized)
            //{
            //    currentRandoTask_label.Text = "Unrandomizing Category";
            //    run appropriate unrando script
            //    RandomizationProgress.PerformStep();
            //}

            currentRandoTask_label.Text = "Done!";
            //The Last Step in the Rando Process should be to set the progress value to 100
            RandomizationProgress.Value = 100;
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

        private void RandomizationProgress_Click(object sender, EventArgs e)
        {
            //TEMPORARY, DELETE LATER
            //RandomizationProgress.PerformStep();
        }
    }
}
