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
        private const string AREA_EBON_HAWK = "ebo_m12aa";
        private const string AREA_DANTOOINE_COURTYARD = "danm14aa";
        private const string AREA_TEMPLE_ROOF = "unk_m44ac";
        private const string AREA_LEVI_PRISON = "lev_m40aa";
        private const string AREA_LEVI_COMMAND = "lev_m40ab";
        private const string AREA_LEVI_HANGER = "lev_m40ac";
        private const string AREA_VULKAR_BASE = "tar_m10aa";
        private const string LABEL_MIND_PRISON = "g_brakatan003";
        private const string LABEL_MYSTERY_BOX = "pebn_mystery";
        private const string LABEL_DANTOOINE_DOOR = "man14aa_door04";
        private const string LABEL_LEHON_DOOR = "unk44_tpllckdoor";
        private const string LABEL_LEVI_ELEVATOR_A = "plev_elev_dlg";
        private const string LABEL_LEVI_ELEVATOR_B = "plev_elev_dlg";
        private const string LABEL_LEVI_ELEVATOR_C = "lev40_accntl_dlg";
        private const string LABEL_VULK_GIT = "m10aa";
        private const string TwoDA_MODULE_SAVE = "modulesave.2da";
        private const string FIXED_DREAM_OVERRIDE = "k_ren_visionland.ncs";
        private const string UNLOCK_MAP_OVERRIDE = "k_pebn_galaxy.ncs";

        private const int MAX_ITERATIONS = 10;

        /// <summary>
        /// A lookup table used to know how the modules are randomized.
        /// </summary>
        private static Dictionary<string, string> LookupTable { get; set; } = new Dictionary<string, string>();

        // Populates and shuffles the the modules flagged to be randomized. Returns true if override files should be added.
        public static void Module_rando(KPaths paths)
        {
            LookupTable.Clear();

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
            Dictionary<string, string> LookupTable = new Dictionary<string, string>();  // Create lookup table to find a given module's new "name".
            bool reachable = true;
            int iterations = 0;

            // ------------------------------------------------------------

            if (Properties.Settings.Default.VerifyReachability)
            {
                // Construct digraph and initialize reachability settings.
                ModuleDigraph digraph = new ModuleDigraph(Path.Combine(Environment.CurrentDirectory, "Xml", "KotorModules.xml"));

                System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                sw.Start();

                do
                {
                    iterations++;

                    // Shuffle the list of included modules.
                    List<string> ShuffledModules = IncludedModules.ToList();
                    Randomize.FisherYatesShuffle(ShuffledModules);
                    LookupTable.Clear();

                    for (int i = 0; i < IncludedModules.Count; i++)
                    {
                        LookupTable.Add(IncludedModules[i], ShuffledModules[i]);
                    }

                    foreach (string name in ExcludedModules)
                    {
                        LookupTable.Add(name, name);
                    }

                    digraph.SetRandomizationLookup(LookupTable);
                    digraph.CheckReachability();
                    reachable = digraph.IsGoalReachable();
                } while (!reachable && iterations < MAX_ITERATIONS);

                if (reachable) Console.WriteLine($"Reachable solution found after {iterations} shuffles. Time elapsed: {sw.Elapsed}");
                else Console.WriteLine($"No reachable solution found over {iterations} shuffles. Time elapsed: {sw.Elapsed}");
            }
            else
            {
                // Shuffle the list of included modules.
                List<string> ShuffledModules = IncludedModules.ToList();
                Randomize.FisherYatesShuffle(ShuffledModules);
                LookupTable.Clear();

                for (int i = 0; i < IncludedModules.Count; i++)
                {
                    LookupTable.Add(IncludedModules[i], ShuffledModules[i]);
                }

                foreach (string name in ExcludedModules)
                {
                    LookupTable.Add(name, name);
                }
            }

            // ------------------------------------------------------------

            // Copy shuffled modules into the base directory.
            foreach (var name in LookupTable)
            {
                File.Copy($"{paths.modules_backup}{name.Key}.rim",   $"{paths.modules}{name.Value}.rim",   true);
                File.Copy($"{paths.modules_backup}{name.Key}_s.rim", $"{paths.modules}{name.Value}_s.rim", true);
                File.Copy($"{paths.lips_backup}{name.Key}_loc.mod",  $"{paths.lips}{name.Value}_loc.mod",  true);
            }

            //for (int i = 0; i < IncludedModules.Count; i++)
            //{
            //    //LookupTable.Add(IncludedModules[i], ShuffledModules[i]);
            //    File.Copy($"{paths.modules_backup}{IncludedModules[i]}.rim",   $"{paths.modules}{ShuffledModules[i]}.rim",   true);
            //    File.Copy($"{paths.modules_backup}{IncludedModules[i]}_s.rim", $"{paths.modules}{ShuffledModules[i]}_s.rim", true);
            //    File.Copy($"{paths.lips_backup}{IncludedModules[i]}_loc.mod",  $"{paths.lips}{ShuffledModules[i]}_loc.mod",  true);
            //}

            //// Copy excluded, untouched modules into the base directory.
            //foreach (string name in ExcludedModules)
            //{
            //    //LookupTable.Add(name, name);
            //    File.Copy($"{paths.modules_backup}{name}.rim",   $"{paths.modules}{name}.rim",   true);
            //    File.Copy($"{paths.modules_backup}{name}_s.rim", $"{paths.modules}{name}_s.rim", true);
            //    File.Copy($"{paths.lips_backup}{name}_loc.mod",  $"{paths.lips}{name}_loc.mod",  true);
            //}

            // Copy lips extras into the base directory.
            foreach (string name in Globals.lipXtras)
            {
                File.Copy($"{paths.lips_backup}{name}", $"{paths.lips}{name}", true);
            }

            // Write additional override files.
            string moduleSavePath = Path.Combine(paths.Override, TwoDA_MODULE_SAVE);
            ModuleExtras saveFileExtras = Properties.Settings.Default.ModuleExtrasValue & (ModuleExtras.SaveAllModules | ModuleExtras.SaveMiniGames | ModuleExtras.NoSaveDelete);

            //if (0 == (saveFileExtras ^ (ModuleExtras.Default)))
            //{
            //    // 0b000 - Milestone Delete (Default)
            //    // Do nothing.
            //}

            if (0 == (saveFileExtras ^ (ModuleExtras.NoSaveDelete)))
            {
                // 0b001 - No Milestone Delete
                File.WriteAllBytes(moduleSavePath, Properties.Resources.NODELETE_modulesave);
            }

            if (0 == (saveFileExtras ^ (ModuleExtras.SaveMiniGames)))
            {
                // 0b010 - Include Minigames | Milestone Delete
                File.WriteAllBytes(moduleSavePath, Properties.Resources.MGINCLUDED_modulesave);
            }

            if (0 == (saveFileExtras ^ (ModuleExtras.NoSaveDelete | ModuleExtras.SaveMiniGames)))
            {
                // 0b011 - Include Minigames | No Milestone Delete
                File.WriteAllBytes(moduleSavePath, Properties.Resources.NODELETE_MGINCLUDED_modulesave);
            }

            if (0 == (saveFileExtras ^ (ModuleExtras.SaveAllModules)) ||
                0 == (saveFileExtras ^ (ModuleExtras.SaveMiniGames | ModuleExtras.SaveAllModules)))
            {
                // Treat both the same.
                // 0b100 - Include All Modules | Milestone Delete
                // 0b110 - Include All Modules | Include Minigames | Milestone Delete
                File.WriteAllBytes(moduleSavePath, Properties.Resources.ALLINCLUDED_modulesave);
            }

            if (0 == (saveFileExtras ^ (ModuleExtras.NoSaveDelete | ModuleExtras.SaveAllModules)) ||
                0 == (saveFileExtras ^ (ModuleExtras.NoSaveDelete | ModuleExtras.SaveMiniGames | ModuleExtras.SaveAllModules)))
            {
                // Treat both the same.
                // 0b101 - Include All Modules | No Milestone Delete
                // 0b111 - Include All Modules | Include Minigames | No Milestone Delete
                File.WriteAllBytes(moduleSavePath, Properties.Resources.NODELETE_ALLINCLUDED_modulesave);
            }

            if (Properties.Settings.Default.ModuleExtrasValue.HasFlag(ModuleExtras.FixDream))
            {
                File.WriteAllBytes(Path.Combine(paths.Override, FIXED_DREAM_OVERRIDE), Properties.Resources.k_ren_visionland);
            }

            if (Properties.Settings.Default.ModuleExtrasValue.HasFlag(ModuleExtras.UnlockGalaxyMap))
            {
                File.WriteAllBytes(Path.Combine(paths.Override, UNLOCK_MAP_OVERRIDE), Properties.Resources.k_pebn_galaxy);
            }

            // Fix warp coordinates.
            if (Properties.Settings.Default.ModuleExtrasValue.HasFlag(ModuleExtras.FixCoordinates))
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

                    //Update coordinate data.
                    (g.Top_Level.Fields.Where(x => x.Label == Properties.Resources.ModuleEntryX).FirstOrDefault() as GFF.FLOAT).value = Globals.FIXED_COORDINATES[kvp.Key].Item1;
                    (g.Top_Level.Fields.Where(x => x.Label == Properties.Resources.ModuleEntryY).FirstOrDefault() as GFF.FLOAT).value = Globals.FIXED_COORDINATES[kvp.Key].Item2;
                    (g.Top_Level.Fields.Where(x => x.Label == Properties.Resources.ModuleEntryZ).FirstOrDefault() as GFF.FLOAT).value = Globals.FIXED_COORDINATES[kvp.Key].Item3;

                    //OLD IMPLEMENTATION:
                    //GFF_old g = new GFF_old(rf.File_Data);

                    //// Update coordinate data.
                    //g.Field_Array.Where(x => x.Label == Properties.Resources.ModuleEntryX).FirstOrDefault().DataOrDataOffset = Globals.FIXED_COORDINATES[kvp.Key].Item1;
                    //g.Field_Array.Where(x => x.Label == Properties.Resources.ModuleEntryY).FirstOrDefault().DataOrDataOffset = Globals.FIXED_COORDINATES[kvp.Key].Item2;
                    //g.Field_Array.Where(x => x.Label == Properties.Resources.ModuleEntryZ).FirstOrDefault().DataOrDataOffset = Globals.FIXED_COORDINATES[kvp.Key].Item3;

                    // Write updated data to RIM file.
                    rf.File_Data = g.ToRawData();
                    r.WriteToFile(kvp.Value.FullName);
                }
            }

            // Fixed Rakata riddle Man in Mind Prison.
            if (Properties.Settings.Default.ModuleExtrasValue.HasFlag(ModuleExtras.FixMindPrison))
            {
                //Allowing Mystery Box to be accessed multiple times
                // Find the files associated with AREA_EBON_HAWK.
                var hawk_files = paths.FilesInModules.Where(fi => fi.Name.Contains(LookupTable[AREA_EBON_HAWK]));
                foreach (FileInfo fi in hawk_files)
                {
                    // Skip any files that don't end in "s.rim".
                    if (fi.Name[fi.Name.Length - 5] != 's') { continue; }

                    // Check the RIM's File_Table for any rFiles labeled with LABEL_MYSTERY_BOX.
                    RIM r = new RIM(fi.FullName);
                    if (r.File_Table.Where(x => x.Label == LABEL_MYSTERY_BOX).Any())
                    {
                        foreach (RIM.rFile rf in r.File_Table)
                        {
                            // For the rFile with LABEL_MIND_PRISON, update the file data with the fix.
                            if (rf.Label == LABEL_MYSTERY_BOX)
                            {
                                rf.File_Data = Properties.Resources.pebn_mystery;
                                continue;
                            }
                        }

                        // Write updated RIM data to file.
                        r.WriteToFile(fi.FullName);
                    }
                }

                //Allowing Riddles to be done more than once
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
                        foreach (RIM.rFile rf in r.File_Table)
                        {
                            // For the rFile with LABEL_MIND_PRISON, update the file data with the fix.
                            if (rf.Label == LABEL_MIND_PRISON)
                            {
                                rf.File_Data = Properties.Resources.g_brakatan003;
                                continue;
                            }
                        }

                        // Write updated RIM data to file.
                        r.WriteToFile(fi.FullName);
                    }
                }
            }

            //Unlock doors to dantooine ruins and on Lehon Temple Roof
            if (Properties.Settings.Default.ModuleExtrasValue.HasFlag(ModuleExtras.UnlockVarDoors))
            {
                //Dantooine Ruins
                var dan_files = paths.FilesInModules.Where(fi => fi.Name.Contains(LookupTable[AREA_DANTOOINE_COURTYARD]));
                foreach (FileInfo fi in dan_files)
                {
                    // Skip any files that don't end in "s.rim".
                    if (fi.Name[fi.Name.Length - 5] != 's') { continue; }

                    RIM r_dan = new RIM(fi.FullName); //Open what replaced danm14aa_s.rim
                    GFF g_dan = new GFF(r_dan.File_Table.Where(x => x.Label == LABEL_DANTOOINE_DOOR).FirstOrDefault().File_Data); //Grab the door out of there

                    //Set the "Locked" field to 0 (false)
                    (g_dan.Top_Level.Fields.Where(x => x.Label == "Locked").FirstOrDefault() as GFF.BYTE).value = 0;

                    r_dan.File_Table.Where(x => x.Label == LABEL_DANTOOINE_DOOR).FirstOrDefault().File_Data = g_dan.ToRawData();

                    r_dan.WriteToFile(fi.FullName);
                }

                //Lehon Temple Roof
                var unk_files = paths.FilesInModules.Where(fi => fi.Name.Contains(LookupTable[AREA_TEMPLE_ROOF]));
                foreach (FileInfo fi in unk_files)
                {
                    // Skip any files that don't end in "s.rim".
                    if (fi.Name[fi.Name.Length - 5] != 's') { continue; }

                    RIM r_unk = new RIM(fi.FullName); //Open what replaced unk_m44aa_s.rim
                    GFF g_unk = new GFF(r_unk.File_Table.Where(x => x.Label == LABEL_LEHON_DOOR).FirstOrDefault().File_Data); //Grab the door out of there

                    //Set the "Locked" field to 0 (false)
                    (g_unk.Top_Level.Fields.Where(x => x.Label == "Locked").FirstOrDefault() as GFF.BYTE).value = 0;

                    r_unk.File_Table.Where(x => x.Label == LABEL_LEHON_DOOR).FirstOrDefault().File_Data = g_unk.ToRawData();

                    r_unk.WriteToFile(fi.FullName);
                }
            }

            //Leviathan Elevators
            if (Properties.Settings.Default.ModuleExtrasValue.HasFlag(ModuleExtras.FixLevElevators))
            {
                var lev_files_a = paths.FilesInModules.Where(fi => fi.Name.Contains(LookupTable[AREA_LEVI_PRISON]));
                var lev_files_b = paths.FilesInModules.Where(fi => fi.Name.Contains(LookupTable[AREA_LEVI_COMMAND]));
                var lev_files_c = paths.FilesInModules.Where(fi => fi.Name.Contains(LookupTable[AREA_LEVI_HANGER]));

                //Prison Block Fix
                foreach (FileInfo fi in lev_files_a)
                {
                    // Skip any files that don't end in "s.rim".
                    if (fi.Name[fi.Name.Length - 5] != 's') { continue; }

                    RIM r_lev = new RIM(fi.FullName);
                    GFF g_lev = new GFF(r_lev.File_Table.Where(x => x.Label == LABEL_LEVI_ELEVATOR_A).FirstOrDefault().File_Data);

                    //Change Entry connecting for bridge option Index to 3, which will transition to the command deck
                    (((g_lev.Top_Level.Fields.Where(x => x.Label == "ReplyList").FirstOrDefault() as GFF.LIST).Structs.Where(x => x.Struct_Type == 3).FirstOrDefault().Fields.Where(x => x.Label == "EntriesList").FirstOrDefault() as GFF.LIST).Structs.Where(x => x.Struct_Type == 0).FirstOrDefault().Fields.Where(x => x.Label == "Index").FirstOrDefault() as GFF.DWORD).value = 3;
                    //Sets the active reference for the hanger option to nothing, meaning there is no requirement to transition to the hanger
                    (((g_lev.Top_Level.Fields.Where(x => x.Label == "ReplyList").FirstOrDefault() as GFF.LIST).Structs.Where(x => x.Struct_Type == 1).FirstOrDefault().Fields.Where(x => x.Label == "EntriesList").FirstOrDefault() as GFF.LIST).Structs.Where(x => x.Struct_Type == 0).FirstOrDefault().Fields.Where(x => x.Label == "Active").FirstOrDefault() as GFF.ResRef).Reference = "";

                    r_lev.File_Table.Where(x => x.Label == LABEL_LEVI_ELEVATOR_A).FirstOrDefault().File_Data = g_lev.ToRawData();

                    r_lev.WriteToFile(fi.FullName);
                }

                //Commadn Deck Fix
                foreach (FileInfo fi in lev_files_b)
                {
                    // Skip any files that don't end in "s.rim".
                    if (fi.Name[fi.Name.Length - 5] != 's') { continue; }

                    RIM r_lev = new RIM(fi.FullName);
                    GFF g_lev = new GFF(r_lev.File_Table.Where(x => x.Label == LABEL_LEVI_ELEVATOR_B).FirstOrDefault().File_Data);

                    //Sets the active reference for the hanger option to nothing, meaning there is no requirement to transition to the hanger
                    (((g_lev.Top_Level.Fields.Where(x => x.Label == "ReplyList").FirstOrDefault() as GFF.LIST).Structs.Where(x => x.Struct_Type == 1).FirstOrDefault().Fields.Where(x => x.Label == "EntriesList").FirstOrDefault() as GFF.LIST).Structs.Where(x => x.Struct_Type == 1).FirstOrDefault().Fields.Where(x => x.Label == "Active").FirstOrDefault() as GFF.ResRef).Reference = "";

                    r_lev.File_Table.Where(x => x.Label == LABEL_LEVI_ELEVATOR_B).FirstOrDefault().File_Data = g_lev.ToRawData();

                    r_lev.WriteToFile(fi.FullName);
                }

                //Hanger Fix
                foreach (FileInfo fi in lev_files_c)
                {
                    // Skip any files that don't end in "s.rim".
                    if (fi.Name[fi.Name.Length - 5] != 's') { continue; }

                    RIM r_lev = new RIM(fi.FullName);

                    //While I possess the ability to edit this file programmatically, due to the complexity I have opted to just load the modded file into resources.
                    r_lev.File_Table.Where(x => x.Label == LABEL_LEVI_ELEVATOR_C).FirstOrDefault().File_Data = Properties.Resources.lev40_accntl_dlg;
                    //Adding module transition scripts to RIM
                    //Prison Block
                    RIM.rFile to40aa = new RIM.rFile();
                    to40aa.TypeID = Reference_Tables.TypeCodes["NCS "];
                    to40aa.Label = "k_plev_goto40aa";
                    to40aa.File_Data = Properties.Resources.k_plev_goto40aa;
                    //Command Deck
                    RIM.rFile to40ab = new RIM.rFile();
                    to40ab.TypeID = Reference_Tables.TypeCodes["NCS "];
                    to40ab.Label = "k_plev_goto40ab";
                    to40ab.File_Data = Properties.Resources.k_plev_goto40ab;
                    //adding scripts
                    r_lev.File_Table.Add(to40aa);
                    r_lev.File_Table.Add(to40ab);

                    r_lev.WriteToFile(fi.FullName);

                }
            }

            //Vulkar Spice Lab Transition
            if (Properties.Settings.Default.ModuleExtrasValue.HasFlag(ModuleExtras.VulkarSpiceLZ))
            {
                var vulk_files = paths.FilesInModules.Where(fi => fi.Name.Contains(LookupTable[AREA_VULKAR_BASE]));
                foreach (FileInfo fi in vulk_files)
                {
                    // Skip any files that end in "s.rim".
                    if (fi.Name[fi.Name.Length - 5] == 's') { continue; }

                    RIM r_vul = new RIM(fi.FullName);
                    r_vul.File_Table.Where(x => x.Label == LABEL_VULK_GIT && x.TypeID == Reference_Tables.TypeCodes["GIT "]).FirstOrDefault().File_Data = Properties.Resources.m10aa;

                    r_vul.WriteToFile(fi.FullName);
                }
            }
        }

        /// <summary>
        /// Creates a CSV file containing a list of the changes made during randomization.
        /// If the file already exists, this method will append the data.
        /// If no randomization has been performed, no file will be created.
        /// </summary>
        /// <param name="path">Path to desired output file.</param>
        public static void GenerateSpoilerLog(string path)
        {
            if (LookupTable.Count == 0) { return; }
            var sortedLookup = LookupTable.OrderBy(kvp => kvp.Key);

            using (StreamWriter sw = new StreamWriter(path))
            {
                //sw.WriteLine($"Modules");
                sw.WriteLine($"Seed,{Properties.Settings.Default.Seed}");
                sw.WriteLine();

                sw.WriteLine("Module Extra,Is Enabled");
                sw.WriteLine($"Delete Milestone Save Data,{!Properties.Settings.Default.ModuleExtrasValue.HasFlag(ModuleExtras.NoSaveDelete)}");
                sw.WriteLine($"Include Minigames in Save,{Properties.Settings.Default.ModuleExtrasValue.HasFlag(ModuleExtras.SaveMiniGames)}");
                sw.WriteLine($"Include All Modules in Save,{Properties.Settings.Default.ModuleExtrasValue.HasFlag(ModuleExtras.SaveAllModules)}");
                sw.WriteLine($"Fix Dream Sequence,{Properties.Settings.Default.ModuleExtrasValue.HasFlag(ModuleExtras.FixDream)}");
                sw.WriteLine($"Unlock Galaxy Map,{Properties.Settings.Default.ModuleExtrasValue.HasFlag(ModuleExtras.UnlockGalaxyMap)}");
                sw.WriteLine($"Fix Module Coordinates,{Properties.Settings.Default.ModuleExtrasValue.HasFlag(ModuleExtras.FixCoordinates)}");
                sw.WriteLine($"Fix Mind Prison,{Properties.Settings.Default.ModuleExtrasValue.HasFlag(ModuleExtras.FixMindPrison)}");
                sw.WriteLine($"Unlock Various Doors,{Properties.Settings.Default.ModuleExtrasValue.HasFlag(ModuleExtras.UnlockVarDoors)}");
                sw.WriteLine($"Fix Leviathan Elevators,{Properties.Settings.Default.ModuleExtrasValue.HasFlag(ModuleExtras.FixLevElevators)}");
                sw.WriteLine($"Add Spice Lab Load Zone,{Properties.Settings.Default.ModuleExtrasValue.HasFlag(ModuleExtras.VulkarSpiceLZ)}");
                sw.WriteLine();

                sw.WriteLine("Has Changed,Original,Randomized");
                foreach (var kvp in sortedLookup)
                {
                    sw.WriteLine($"{(kvp.Key != kvp.Value).ToString()},{kvp.Key},{kvp.Value}");
                }
                sw.WriteLine();
            }
        }
    }
}
