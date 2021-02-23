using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using KotOR_IO;
using ClosedXML.Excel;

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

        private const int MAX_ITERATIONS = 10000; // Just a random large number to give enough chances to find a valid shuffle.

        /// <summary>
        /// A lookup table used to know how the modules are randomized.
        /// Usage: LookupTable[Original] = Randomized;
        /// </summary>
        internal static Dictionary<string, string> LookupTable { get; private set; } = new Dictionary<string, string>();

        /// <summary>
        /// A directional graph mapping the modules and loading zones throughout the game.
        /// </summary>
        private static ModuleDigraph Digraph { get; set; } = new ModuleDigraph(Path.Combine(Environment.CurrentDirectory, "Xml", "KotorModules.xml"));

        /// <summary>
        /// Populates and shuffles the the modules flagged to be randomized. Returns true if override files should be added.
        /// </summary>
        public static void Module_rando(KPaths paths)
        {
            // Reset digraph reachability settings.
            Digraph.ResetSettings();
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
            List<string> ExcludedModules = Globals.BoundModules.Where(x => x.Omitted).Select(x => x.Code).ToList();
            List<string> IncludedModules = Globals.BoundModules.Where(x => !x.Omitted).Select(x => x.Code).ToList();
            bool reachable = false;
            int iterations = 0;

            if (Properties.Settings.Default.UseRandoRules ||
                Properties.Settings.Default.VerifyReachability)
            {
                System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                sw.Start();

                while (!reachable && iterations < MAX_ITERATIONS)
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

                    Digraph.SetRandomizationLookup(LookupTable);

                    if (Properties.Settings.Default.UseRandoRules)
                    {
                        // Skip to the next iteration if the rules are violated.
                        if (AreRulesViolated()) continue;
                    }

                    if (Properties.Settings.Default.VerifyReachability)
                    {
                        Digraph.CheckReachability();
                        reachable = Digraph.IsGoalReachable();
                    }
                    else
                    {
                        reachable = true;
                    }
                }

                if (Properties.Settings.Default.VerifyReachability)
                {
                    if (reachable)
                    {
                        var message = $"Reachable solution found after {iterations} shuffles. Time elapsed: {sw.Elapsed}";
                        Console.WriteLine(message);
                    }
                    else
                    {
                        // Throw an exception if not reachable.
                        var message = $"No reachable solution found over {iterations} shuffles. Time elapsed: {sw.Elapsed}";
                        Console.WriteLine(message);
                        throw new TimeoutException(message);
                    }
                }

                //digraph.WriteReachableToConsole();
                Console.WriteLine();
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

            // Copy shuffled modules into the base directory.
            foreach (var name in LookupTable)
            {
                File.Copy($"{paths.modules_backup}{name.Key}.rim",   $"{paths.modules}{name.Value}.rim",   true);
                File.Copy($"{paths.modules_backup}{name.Key}_s.rim", $"{paths.modules}{name.Value}_s.rim", true);
                File.Copy($"{paths.lips_backup}{name.Key}_loc.mod",  $"{paths.lips}{name.Value}_loc.mod",  true);
            }

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
                    (g.Top_Level.Fields.Where(x => x.Label == Properties.Resources.ModuleEntryX).FirstOrDefault() as GFF.FLOAT).Value = Globals.FIXED_COORDINATES[kvp.Key].Item1;
                    (g.Top_Level.Fields.Where(x => x.Label == Properties.Resources.ModuleEntryY).FirstOrDefault() as GFF.FLOAT).Value = Globals.FIXED_COORDINATES[kvp.Key].Item2;
                    (g.Top_Level.Fields.Where(x => x.Label == Properties.Resources.ModuleEntryZ).FirstOrDefault() as GFF.FLOAT).Value = Globals.FIXED_COORDINATES[kvp.Key].Item3;

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
                    (g_dan.Top_Level.Fields.Where(x => x.Label == "Locked").FirstOrDefault() as GFF.BYTE).Value = 0;

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
                    (g_unk.Top_Level.Fields.Where(x => x.Label == "Locked").FirstOrDefault() as GFF.BYTE).Value = 0;

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
                    (((g_lev.Top_Level.Fields.Where(x => x.Label == "ReplyList").FirstOrDefault() as GFF.LIST).Structs.Where(x => x.Struct_Type == 3).FirstOrDefault().Fields.Where(x => x.Label == "EntriesList").FirstOrDefault() as GFF.LIST).Structs.Where(x => x.Struct_Type == 0).FirstOrDefault().Fields.Where(x => x.Label == "Index").FirstOrDefault() as GFF.DWORD).Value = 3;
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
                    RIM.rFile to40aa = new RIM.rFile
                    {
                        TypeID = (int)ResourceType.NCS,
                        Label = "k_plev_goto40aa",
                        File_Data = Properties.Resources.k_plev_goto40aa
                    };
                    //Command Deck
                    RIM.rFile to40ab = new RIM.rFile
                    {
                        TypeID = (int)ResourceType.NCS,
                        Label = "k_plev_goto40ab",
                        File_Data = Properties.Resources.k_plev_goto40ab
                    };
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
                    r_vul.File_Table.Where(x => x.Label == LABEL_VULK_GIT && x.TypeID == (int)ResourceType.GIT).FirstOrDefault().File_Data = Properties.Resources.m10aa;

                    r_vul.WriteToFile(fi.FullName);
                }
            }
        }

        internal static void Reset()
        {
            // Reset digraph reachability settings.
            Digraph.ResetSettings();
            // Prepare lists for new randomization.
            LookupTable.Clear();
        }

        /// <summary>
        /// Check to see if the rules are violated.
        /// If a module's list of bad randomizations contains what replaces it now, the rule is violated.
        /// </summary>
        /// <returns></returns>
        private static bool AreRulesViolated()
        {
            // Rule key cannot replace any of the listed values.
            // LookupTable[Original] = Randomized;
            //    Pseudocode:
            // Original = RuleKey;
            // Randomized = LookupTable[Original];
            // If RuleList.Contains(Randomized),
            //    Rule is violated.

            // Check rule 1
            foreach (var ruleKVP in Globals.RULE1)
            {
                var lookup = LookupTable[ruleKVP.Key];
                if (ruleKVP.Value.Contains(lookup))
                {
                    Console.WriteLine($"Rule 1 violated: {ruleKVP.Key} replaces {lookup}");
                    return true;
                }
            }

            // Check rule 2
            foreach (var ruleKVP in Globals.RULE2)
            {
                var lookup = LookupTable[ruleKVP.Key];
                if (ruleKVP.Value.Contains(lookup))
                {
                    Console.WriteLine($"Rule 2 violated: {ruleKVP.Key} replaces {lookup}");
                    return true;
                }
            }

            // Check rule 3
            foreach (var ruleKVP in Globals.RULE3)
            {
                var lookup = LookupTable[ruleKVP.Key];
                if (ruleKVP.Value.Contains(lookup))
                {
                    Console.WriteLine($"Rule 3 violated: {ruleKVP.Key} replaces {lookup}");
                    return true;
                }
            }

            Console.WriteLine("No rules violated.");
            return false;
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

                sw.WriteLine($"Verify Reachability,{Properties.Settings.Default.VerifyReachability}");
                sw.WriteLine($"Ignore Single-Use Transitions,{Properties.Settings.Default.IgnoreOnceEdges}");
                sw.WriteLine($"Goal Is Malak,{Properties.Settings.Default.GoalIsMalak}");
                sw.WriteLine($"Goal Is Star Maps,{Properties.Settings.Default.GoalIsStarMaps}");
                sw.WriteLine($"Goal Is Pazaak,{Properties.Settings.Default.GoalIsPazaak}");
                sw.WriteLine($"Allow Glitch Clipping,{Properties.Settings.Default.AllowGlitchClip}");
                sw.WriteLine($"Allow Glitch DLZ,{Properties.Settings.Default.AllowGlitchDlz}");
                sw.WriteLine($"Allow Glitch FLU,{Properties.Settings.Default.AllowGlitchFlu}");
                sw.WriteLine($"Allow Glitch GPW,{Properties.Settings.Default.AllowGlitchGpw}");
                sw.WriteLine();

                sw.WriteLine("Has Changed,Default Code,Default Name,Randomized Code,Randomized Name");
                foreach (var kvp in sortedLookup)
                {
                    var defaultName = Digraph.Modules.FirstOrDefault(m => m.WarpCode == kvp.Key)?.CommonName;
                    var randomizedName = Digraph.Modules.FirstOrDefault(m => m.WarpCode == kvp.Value)?.CommonName;
                    sw.WriteLine($"{(kvp.Key != kvp.Value).ToString()},{kvp.Key},{defaultName},{kvp.Value},{randomizedName}");
                }
                sw.WriteLine();
            }
        }

        public static void GenerateSpoilerLog(XLWorkbook workbook)
        {
            if (LookupTable.Count == 0) { return; }
            var ws = workbook.Worksheets.Add("Module");

            int i = 1;
            ws.Cell(i, 1).Value = "Seed";
            ws.Cell(i, 2).Value = Properties.Settings.Default.Seed;
            ws.Cell(i, 1).Style.Font.Bold = true;
            i += 2;     // Skip a row.

            // Module Randomization Settings
            ws.Cell(i, 1).Value = "Module Extra";
            ws.Cell(i, 2).Value = "Is Enabled";
            ws.Cell(i, 1).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            ws.Cell(i, 2).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            ws.Cell(i, 1).Style.Font.Bold = true;
            ws.Cell(i, 2).Style.Font.Bold = true;
            i++;

            var settings = new List<Tuple<string, string>>()
            {
                new Tuple<string, string>("Delete Milestone Save Data", (!Properties.Settings.Default.ModuleExtrasValue.HasFlag(ModuleExtras.NoSaveDelete)).ToString()),
                new Tuple<string, string>("Include Minigames in Save", Properties.Settings.Default.ModuleExtrasValue.HasFlag(ModuleExtras.SaveMiniGames).ToString()),
                new Tuple<string, string>("Include All Modules in Save", Properties.Settings.Default.ModuleExtrasValue.HasFlag(ModuleExtras.SaveAllModules).ToString()),
                new Tuple<string, string>("Fix Dream Sequence", Properties.Settings.Default.ModuleExtrasValue.HasFlag(ModuleExtras.FixDream).ToString()),
                new Tuple<string, string>("Unlock Galaxy Map", Properties.Settings.Default.ModuleExtrasValue.HasFlag(ModuleExtras.UnlockGalaxyMap).ToString()),
                new Tuple<string, string>("Fix Module Coordinates", Properties.Settings.Default.ModuleExtrasValue.HasFlag(ModuleExtras.FixCoordinates).ToString()),
                new Tuple<string, string>("Fix Mind Prison", Properties.Settings.Default.ModuleExtrasValue.HasFlag(ModuleExtras.FixMindPrison).ToString()),
                new Tuple<string, string>("Unlock Various Doors", Properties.Settings.Default.ModuleExtrasValue.HasFlag(ModuleExtras.UnlockVarDoors).ToString()),
                new Tuple<string, string>("Fix Leviathan Elevators", Properties.Settings.Default.ModuleExtrasValue.HasFlag(ModuleExtras.FixLevElevators).ToString()),
                new Tuple<string, string>("Add Spice Lab Load Zone", Properties.Settings.Default.ModuleExtrasValue.HasFlag(ModuleExtras.VulkarSpiceLZ).ToString()),
                new Tuple<string, string>("", ""),  // Skip a row.
                new Tuple<string, string>("Use Rando Exclusion Rules", Properties.Settings.Default.UseRandoRules.ToString()),
                new Tuple<string, string>("Verify Reachability", Properties.Settings.Default.VerifyReachability.ToString()),
                new Tuple<string, string>("Ignore Single-Use Transitions", Properties.Settings.Default.IgnoreOnceEdges.ToString()),
                new Tuple<string, string>("Goal Is Malak", Properties.Settings.Default.GoalIsMalak.ToString()),
                new Tuple<string, string>("Goal Is Star Maps", Properties.Settings.Default.GoalIsStarMaps.ToString()),
                new Tuple<string, string>("Goal Is Pazaak", Properties.Settings.Default.GoalIsPazaak.ToString()),
                new Tuple<string, string>("Allow Glitch Clipping", Properties.Settings.Default.AllowGlitchClip.ToString()),
                new Tuple<string, string>("Allow Glitch DLZ", Properties.Settings.Default.AllowGlitchDlz.ToString()),
                new Tuple<string, string>("Allow Glitch FLU", Properties.Settings.Default.AllowGlitchFlu.ToString()),
                new Tuple<string, string>("Allow Glitch GPW", Properties.Settings.Default.AllowGlitchGpw.ToString()),
                new Tuple<string, string>("", ""),  // Skip a row.
            };

            foreach (var setting in settings)
            {
                ws.Cell(i, 1).Value = setting.Item1;
                ws.Cell(i, 2).Value = setting.Item2;
                ws.Cell(i, 1).Style.Font.Italic = true;
                i++;
            }

            // Module Shuffle
            ws.Cell(i, 1).Value = "Has Changed";
            ws.Cell(i, 2).Value = "Default Code";
            ws.Cell(i, 3).Value = "Default Name";
            ws.Cell(i, 4).Value = "Randomized Code";
            ws.Cell(i, 5).Value = "Randomized Name";
            ws.Cell(i, 1).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            ws.Cell(i, 2).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            ws.Cell(i, 3).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            ws.Cell(i, 4).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            ws.Cell(i, 5).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            ws.Cell(i, 1).Style.Font.Bold = true;
            ws.Cell(i, 2).Style.Font.Bold = true;
            ws.Cell(i, 3).Style.Font.Bold = true;
            ws.Cell(i, 4).Style.Font.Bold = true;
            ws.Cell(i, 5).Style.Font.Bold = true;
            i++;

            var sortedLookup = LookupTable.OrderBy(kvp => kvp.Key);
            foreach (var kvp in sortedLookup)
            {
                var defaultName = Digraph.Modules.FirstOrDefault(m => m.WarpCode == kvp.Key)?.CommonName;
                var randomizedName = Digraph.Modules.FirstOrDefault(m => m.WarpCode == kvp.Value)?.CommonName;

                ws.Cell(i, 1).Value = (kvp.Key != kvp.Value).ToString();
                ws.Cell(i, 2).Value = kvp.Key;
                ws.Cell(i, 3).Value = defaultName;
                ws.Cell(i, 4).Value = kvp.Value;
                ws.Cell(i, 5).Value = randomizedName;
                if (kvp.Key != kvp.Value) ws.Cell(i, 1).Style.Font.FontColor = XLColor.Green;
                else ws.Cell(i, 1).Style.Font.FontColor = XLColor.Red;
                i++;
            }

            // Resize Columns
            ws.Column(1).AdjustToContents();
            ws.Column(2).AdjustToContents();
            ws.Column(3).AdjustToContents();
            ws.Column(4).AdjustToContents();
            ws.Column(5).AdjustToContents();
        }

        /// <summary>
        /// Returns the common name of the given module code. Returns null if the code isn't found.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string GetModuleCommonName(string code)
        {
            return Digraph.Modules.FirstOrDefault(m => m.WarpCode == code)?.CommonName;
        }
    }
}
