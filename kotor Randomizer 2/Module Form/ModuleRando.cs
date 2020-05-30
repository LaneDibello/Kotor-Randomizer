using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace kotor_Randomizer_2
{
    public partial class ModuleForm
    {
        //Populates and shuffles the the modules flagged to be randomized. Returns true if override files should be added.
        public static void Module_rando(Globals.KPaths paths)
        {
            //Set up the bound module collection if it hasn't been already
            if (!Properties.Settings.Default.ModulesInitialized)
            {
                foreach (string s in Globals.MODULES)
                {
                    Globals.BoundModules.Add(new Globals.Mod_Entry(s, true));
                }
                Properties.Settings.Default.ModulesInitialized = true;
            }

            if (!Properties.Settings.Default.ModulePresetSelected)
            {
                //Figure something out here
            }





            List<string> Shuffled_Mods = new List<string>();

            //Split the Bound modules into their respective list
            Shuffled_Mods = Globals.BoundModules.Where(x => !x.ommitted).Select(x => x.name).ToList();

            Randomize.FisherYatesShuffle(Shuffled_Mods);

            if (Properties.Settings.Default.AddOverideFiles.Count > 0)
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
                        File.WriteAllBytes(paths.Override + "modulesave.2da", Properties.Resources.NODELETE_ALLINCLUDED_modulesave);
                        break;
                    case 7:
                        File.WriteAllBytes(paths.Override + "modulesave.2da", Properties.Resources.ALLINCLUDED_modulesave);
                        break;
                }

                if (Properties.Settings.Default.AddOverideFiles.Contains("k_ren_visionland.ncs"))
                {
                    File.WriteAllBytes(paths.Override + "k_ren_visionland.ncs", Properties.Resources.k_ren_visionland);
                }

                if (Properties.Settings.Default.AddOverideFiles.Contains("k_pebn_galaxy.ncs"))
                {
                    File.WriteAllBytes(paths.Override + "k_pebn_galaxy.ncs", Properties.Resources.k_pebn_galaxy);
                }
            }

            int k = 0;
            foreach (string M in Globals.BoundModules.Where(x => !x.ommitted).Select(x => x.name))
            {
                File.Copy(paths.get_backup(paths.modules) + M + ".rim", paths.modules + Shuffled_Mods[k] + ".rim", true);
                File.Copy(paths.get_backup(paths.modules) + M + "_s.rim", paths.modules + Shuffled_Mods[k] + "_s.rim", true);
                File.Copy(paths.get_backup(paths.lips) + M + "_loc.mod", paths.lips + Shuffled_Mods[k] + "_loc.mod", true);
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
                File.Copy(paths.get_backup(paths.lips) + L, paths.lips + L, true);
            }

        }
    }
}