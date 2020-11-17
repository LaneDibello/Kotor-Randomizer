using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using KotOR_IO;

namespace kotor_Randomizer_2
{
    public static class ModuleRando
    {
        private const string AREA_MYSTERY_BOX = "ebo_m46ab";
        private const string LABEL_MIND_PRISON = "g_brakatan003";
        private const string TwoDA_MODULE_SAVE = "modulesave.2da";
        private const string FIXED_DREAM_OVERRIDE = "k_ren_visionland.ncs";
        private const string UNLOCK_MAP_OVERRIDE = "k_pebn_galaxy.ncs";

        // Populates and shuffles the the modules flagged to be randomized. Returns true if override files should be added.
        public static void Module_rando(KPaths paths)
        {
            // Set up the bound module collection if it hasn't been already.
            if (!Properties.Settings.Default.ModulesInitialized)
            {
                Globals.BoundModules.Clear();
                foreach (string s in Globals.MODULES)
                {
                    Globals.BoundModules.Add(new Globals.Mod_Entry(s, true));
                }
                Properties.Settings.Default.ModulesInitialized = true;
            }

            //if (!Properties.Settings.Default.ModulePresetSelected)
            //{
            //    //Figure something out here
            //}

            // Split the Bound modules into their respective lists.
            List<string> ExcludedModules = Globals.BoundModules.Where(x => x.Omitted).Select(x => x.Name).ToList();
            List<string> IncludedModules = Globals.BoundModules.Where(x => !x.Omitted).Select(x => x.Name).ToList();

            // Shuffle the list of included modules.
            List<string> ShuffledModules = IncludedModules.ToList();
            Randomize.FisherYatesShuffle(ShuffledModules);

            // Copy shuffled modules into the base directory.
            Dictionary<string, string> LookupTable = new Dictionary<string, string>();  // Create lookup table to find a given module's new "name".
            for (int i = 0; i < IncludedModules.Count; i++)
            {
                LookupTable.Add(IncludedModules[i], ShuffledModules[i]);
                File.Copy($"{paths.modules_backup}{IncludedModules[i]}.rim",   $"{paths.modules}{ShuffledModules[i]}.rim",   true);
                File.Copy($"{paths.modules_backup}{IncludedModules[i]}_s.rim", $"{paths.modules}{ShuffledModules[i]}_s.rim", true);
                File.Copy($"{paths.lips_backup}{IncludedModules[i]}_loc.mod",  $"{paths.lips}{ShuffledModules[i]}_loc.mod",  true);
            }

            // Copy excluded, untouched modules into the base directory.
            foreach (string name in ExcludedModules)
            {
                LookupTable.Add(name, name);
                File.Copy($"{paths.modules_backup}{name}.rim",   $"{paths.modules}{name}.rim",   true);
                File.Copy($"{paths.modules_backup}{name}_s.rim", $"{paths.modules}{name}_s.rim", true);
                File.Copy($"{paths.lips_backup}{name}_loc.mod",  $"{paths.lips}{name}_loc.mod",  true);
            }

            // Copy lips extras into the base directory.
            foreach (string name in Globals.lipXtras)
            {
                File.Copy($"{paths.lips_backup}{name}", $"{paths.lips}{name}", true);
            }

            // Write additional override files.
            if (Properties.Settings.Default.AddOverideFiles.Count > 0)
            {
                string moduleSavePath = Path.Combine(paths.Override, TwoDA_MODULE_SAVE);

                switch (Properties.Settings.Default.ModuleSaveStatus)
                {
                    case 0: // 0000
                        File.WriteAllBytes(moduleSavePath, Properties.Resources.NODELETE_modulesave);
                        break;
                    default:
                    case 1: // 0001
                        //This is kotor's default configuration
                        break;
                    case 2: // 0010
                        File.WriteAllBytes(moduleSavePath, Properties.Resources.NODELETE_MGINCLUDED_modulesave);
                        break;
                    case 3: // 0011
                        File.WriteAllBytes(moduleSavePath, Properties.Resources.MGINCLUDED_modulesave);
                        break;
                    case 6: // 0110
                        File.WriteAllBytes(moduleSavePath, Properties.Resources.NODELETE_ALLINCLUDED_modulesave);
                        break;
                    case 7: // 0111
                        File.WriteAllBytes(moduleSavePath, Properties.Resources.ALLINCLUDED_modulesave);
                        break;
                }

                if (Properties.Settings.Default.AddOverideFiles.Contains(FIXED_DREAM_OVERRIDE))
                {
                    File.WriteAllBytes(Path.Combine(paths.Override, FIXED_DREAM_OVERRIDE), Properties.Resources.k_ren_visionland);
                }

                if (Properties.Settings.Default.AddOverideFiles.Contains(UNLOCK_MAP_OVERRIDE))
                {
                    File.WriteAllBytes(Path.Combine(paths.Override, UNLOCK_MAP_OVERRIDE), Properties.Resources.k_pebn_galaxy);
                }
            }

            // Fix warp coordinates.
            if (Properties.Settings.Default.FixWarpCoords)
            {
                // Create a lookup for modules needing coordinate fix with their newly shuffled FileInfos.
                var shuffleFileLookup = new Dictionary<string, FileInfo>();
                foreach (var key in Globals.FIXED_COORDINATES.Keys)
                {
                    shuffleFileLookup.Add(key, paths.FilesInModules.FirstOrDefault(fi => fi.Name.Contains(LookupTable[key])));
                }

                foreach (var kvp in shuffleFileLookup)
                {
                    // Set up objects.
                    RIM r = new RIM(kvp.Value.FullName);
                    RIM.rFile rf = r.File_Table.Where(x => x.TypeID == (int)ResourceType.IFO).FirstOrDefault();
                    GFF g = new GFF(rf.File_Data);

                    // Update coordinate data.
                    g.Field_Array.Where(x => x.Label == Properties.Resources.ModuleEntryX).FirstOrDefault().DataOrDataOffset = Globals.FIXED_COORDINATES[kvp.Key].Item1;
                    g.Field_Array.Where(x => x.Label == Properties.Resources.ModuleEntryY).FirstOrDefault().DataOrDataOffset = Globals.FIXED_COORDINATES[kvp.Key].Item2;
                    g.Field_Array.Where(x => x.Label == Properties.Resources.ModuleEntryZ).FirstOrDefault().DataOrDataOffset = Globals.FIXED_COORDINATES[kvp.Key].Item3;

                    // Write updated data to RIM file.
                    rf.File_Data = g.ToRawData();
                    r.WriteToFile(kvp.Value.FullName);
                }
            }

            // Fixed Rakata riddle Man in Mind Prison.
            if (Properties.Settings.Default.FixMindPrison)
            {
                // Find the files associated with AREA_MYSTERY_BOX.
                var files = paths.FilesInModules.Where(fi => fi.Name.Contains(LookupTable[AREA_MYSTERY_BOX]));
                foreach (FileInfo fi in files)
                {
                    // Skip any files that don't end in "s.rim".
                    if (fi.Name[fi.Name.Length - 5] != 's') { continue; }

                    // Check the RIM's File_Table for any rFiles labeled with LABEL_MIND_PRISON.
                    RIM r = new RIM(fi.FullName);
                    if (r.File_Table.Where(x => x.Label == LABEL_MIND_PRISON).Any())
                    {
                        bool offadjust = false;
                        foreach (RIM.rFile rf in r.File_Table)
                        {
                            // For the rFile with LABEL_MIND_PRISON, update the file data with the fix.
                            if (rf.Label == LABEL_MIND_PRISON)
                            {
                                rf.File_Data = Properties.Resources.g_brakatan003;
                                rf.DataSize += 192;
                                offadjust = true;
                                continue;
                            }
                            // For rFiles after LABEL_MIND_PRISON, add the additional data offset.
                            if (offadjust)
                            {
                                rf.DataOffset += 192;
                            }
                        }

                        // Write updated RIM data to file.
                        r.WriteToFile(fi.FullName);
                    }
                }
            }
        }
    }
}
