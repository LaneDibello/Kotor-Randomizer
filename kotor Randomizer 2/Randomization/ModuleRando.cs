using ClosedXML.Excel;
using KotOR_IO;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System;

namespace kotor_Randomizer_2
{
    public static class ModuleRando
    {
        #region Consts
        private const string AREA_DAN_COURTYARD = "danm14aa";
        private const string AREA_EBO_BOX = "ebo_m46ab";
        private const string AREA_EBO_HAWK = "ebo_m12aa";
        private const string AREA_KOR_ENTRANCE = "korr_m33ab";
        private const string AREA_KOR_VALLEY = "korr_m36aa";
        private const string AREA_LEV_COMMAND = "lev_m40ab";
        private const string AREA_LEV_HANGAR = "lev_m40ac";
        private const string AREA_LEV_PRISON = "lev_m40aa";
        private const string AREA_MAN_DOCKING_BAY = "manm26ad";
        private const string AREA_MAN_EAST_CENTRAL = "manm26ae";
        private const string AREA_STA_DECK3 = "sta_m45ac";
        private const string AREA_TAR_LOWER_CITY = "tar_m03aa";
        private const string AREA_TAR_VULK_BASE = "tar_m10aa";
        private const string AREA_UNK_MAIN_FLOOR = "unk_m44aa";
        private const string AREA_UNK_SUMMIT = "unk_m44ac";
        private const string FIXED_DREAM_OVERRIDE = "k_ren_visionland.ncs";
        private const string LABEL_DANT_DOOR = "man14aa_door04";
        private const string LABEL_EBO_BOX = "pebn_mystery";
        private const string LABEL_EBO_PRISON = "g_brakatan003";
        private const string LABEL_KOR_ENTRANCE_ACADEMY = "k33b_dor_academy";
        private const string LABEL_KOR_VALLEY_AJUNTA = "kor36_kor37";
        private const string LABEL_KOR_VALLEY_MARKA = "kor36_kor38a";
        private const string LABEL_KOR_VALLEY_NAGA = "kor36_kor39";
        private const string LABEL_KOR_VALLEY_TULAK = "kor36_kor38b";
        private const string LABEL_KOR_VALLEY_ACADEMY = "kor36_kor35";
        private const string LABEL_LEV_ELEVATOR_A = "plev_elev_dlg";
        private const string LABEL_LEV_ELEVATOR_B = "plev_elev_dlg";
        private const string LABEL_LEV_ELEVATOR_C = "lev40_accntl_dlg";
        private const string LABEL_MAN_SITH_HANGAR = "man26ad_door02";
        private const string LABEL_MAN_SUB_DOOR03 = "man26ac_door03";   // Door into the Republic Embassy.
        private const string LABEL_MAN_SUB_DOOR05 = "man26ac_door05";   // Door to the submersible.
        private const string LABEL_STA_BAST_DOOR = "k45_door_bast1";
        private const string LABEL_TAR_UNDERCITY = "tar03_underdoor";
        private const string LABEL_TAR_VULKAR = "tar03_blkdoor";
        private const string LABEL_TAR_VULK_GIT = "m10aa";
        private const string LABEL_UNK_DOOR = "unk44_tpllckdoor";
        private const string LABEL_UNK_EXIT_DOOR = "unk44_exitdoor";

        private const int MAX_ITERATIONS = 10000;   // A large number to give enough chances to find a valid shuffle.

        private const string TwoDA_MODULE_SAVE = "modulesave.2da";
        private const string UNLOCK_MAP_OVERRIDE = "k_pebn_galaxy.ncs";
        #endregion Consts

        #region Properties
        /// <summary>
        /// A lookup table used to know how the modules are randomized.
        /// Usage: LookupTable[Original] = Randomized;
        /// </summary>
        internal static Dictionary<string, string> LookupTable { get; private set; } = new Dictionary<string, string>();

        /// <summary>
        /// A directional graph mapping the modules and loading zones throughout the game.
        /// </summary>
        private static ModuleDigraph Digraph { get; set; } = new ModuleDigraph(Path.Combine(Environment.CurrentDirectory, "Xml", "KotorModules.xml"));

        public static bool UseRandoRules { get; set; }
        public static bool VerifyReachability { get; set; }
        public static ModuleExtras ModuleExtrasValue { get; set; }
        public static List<string> RandomizedModules { get; set; }
        public static List<string> OmittedModules { get; set; }
        private static string ShufflePreset { get; set; }
        #endregion Properties

        #region Methods
        /// <summary>
        /// Creates a worksheet in the given XLWorkbook containing the general settings
        /// used during the latest randomization.
        /// </summary>
        public static void CreateGeneralSpoilerLog(XLWorkbook workbook, int seed = -1)
        {
            var ws = workbook.Worksheets.Add("General");
            int i = 1;

            // Write randomization seed.
            ws.Cell(i, 1).Value = "Seed";
            ws.Cell(i, 1).Style.Font.Bold = true;
            if (seed < 0) ws.Cell(i, 2).Value = Properties.Settings.Default.Seed;
            else          ws.Cell(i, 2).Value = seed;
            i++;

            // Write assembly version.
            Version version = typeof(StartForm).Assembly.GetName().Version;
            ws.Cell(i, 1).Value = "Version";
            ws.Cell(i, 1).Style.Font.Bold = true;
            ws.Cell(i, 2).Value = $"v{version.Major}.{version.Minor}.{version.Build}";
            ws.Cell(i, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            i += 2;     // Skip a row.

            // Write Save Data settings.
            ws.Cell(i, 1).Value = "Save Setting";
            ws.Cell(i, 2).Value = "Is Enabled";
            ws.Cell(i, 1).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            ws.Cell(i, 2).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            ws.Cell(i, 1).Style.Font.Bold = true;
            ws.Cell(i, 2).Style.Font.Bold = true;
            i++;

            List<Tuple<string, string>> settings = new List<Tuple<string, string>>()
            {
                new Tuple<string, string>("Prevent Milestone Save Data Deletion", ModuleExtrasValue.HasFlag(ModuleExtras.NoSaveDelete  ).ToString()),
                new Tuple<string, string>("Include Minigames in Save",            ModuleExtrasValue.HasFlag(ModuleExtras.SaveMiniGames ).ToString()),
                new Tuple<string, string>("Include All Modules in Save",          ModuleExtrasValue.HasFlag(ModuleExtras.SaveAllModules).ToString()),
                new Tuple<string, string>("", ""),  // Skip a row.
            };

            foreach (var setting in settings)
            {
                ws.Cell(i, 1).Value = setting.Item1;
                ws.Cell(i, 2).Value = setting.Item2;
                ws.Cell(i, 1).Style.Font.Italic = true;
                i++;
            }

            // Write Quality of Life settings.
            ws.Cell(i, 1).Value = "QoL Setting";
            ws.Cell(i, 2).Value = "Is Enabled";
            ws.Cell(i, 1).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            ws.Cell(i, 2).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            ws.Cell(i, 1).Style.Font.Bold = true;
            ws.Cell(i, 2).Style.Font.Bold = true;
            i++;

            settings = new List<Tuple<string, string>>()
            {
                new Tuple<string, string>("Add Spice Lab Load Zone", ModuleExtrasValue.HasFlag(ModuleExtras.VulkarSpiceLZ ).ToString()),
                new Tuple<string, string>("Fix Dream Sequence",      ModuleExtrasValue.HasFlag(ModuleExtras.FixDream      ).ToString()),
                new Tuple<string, string>("Fix Mind Prison",         ModuleExtrasValue.HasFlag(ModuleExtras.FixMindPrison ).ToString()),
                new Tuple<string, string>("Fix Module Coordinates",  ModuleExtrasValue.HasFlag(ModuleExtras.FixCoordinates).ToString()),
                new Tuple<string, string>("", ""),  // Skip a row.
            };

            foreach (var setting in settings)
            {
                ws.Cell(i, 1).Value = setting.Item1;
                ws.Cell(i, 2).Value = setting.Item2;
                ws.Cell(i, 1).Style.Font.Italic = true;
                i++;
            }

            // Write Door Unlocks settings.
            ws.Cell(i, 1).Value = "Door Unlocks";
            ws.Cell(i, 2).Value = "State";
            ws.Cell(i, 1).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            ws.Cell(i, 2).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            ws.Cell(i, 1).Style.Font.Bold = true;
            ws.Cell(i, 2).Style.Font.Bold = true;
            i++;

            settings = new List<Tuple<string, string>>()
            {
                new Tuple<string, string>("Unlock DAN Ruins Door",      ModuleExtrasValue.HasFlag(ModuleExtras.UnlockDanRuins     ) ? "Unlocked" : "Locked"),
                new Tuple<string, string>("Unlock EBO Galaxy Map",      ModuleExtrasValue.HasFlag(ModuleExtras.UnlockGalaxyMap    ) ? "Unlocked" : "Locked"),
                new Tuple<string, string>("Unlock KOR Valley",          ModuleExtrasValue.HasFlag(ModuleExtras.UnlockKorValley    ) ? "Unlocked" : "Locked"),
                new Tuple<string, string>("Unlock LEV Elevators",       ModuleExtrasValue.HasFlag(ModuleExtras.UnlockLevElev      ) ? "Unlocked" : "Locked"),
                new Tuple<string, string>("Unlock MAN Embassy",         ModuleExtrasValue.HasFlag(ModuleExtras.UnlockManEmbassy   ) ? "Unlocked" : "Locked"),
                new Tuple<string, string>("Unlock MAN Hangar",          ModuleExtrasValue.HasFlag(ModuleExtras.UnlockManHangar    ) ? "Unlocked" : "Locked"),
                new Tuple<string, string>("Unlock STA Door to Bastila", ModuleExtrasValue.HasFlag(ModuleExtras.UnlockStaBastila   ) ? "Unlocked" : "Locked"),
                new Tuple<string, string>("Unlock TAR Undercity",       ModuleExtrasValue.HasFlag(ModuleExtras.UnlockTarUndercity ) ? "Unlocked" : "Locked"),
                new Tuple<string, string>("Unlock TAR Vulkar",          ModuleExtrasValue.HasFlag(ModuleExtras.UnlockTarVulkar    ) ? "Unlocked" : "Locked"),
                new Tuple<string, string>("Unlock UNK Summit Exit",     ModuleExtrasValue.HasFlag(ModuleExtras.UnlockUnkSummit    ) ? "Unlocked" : "Locked"),
                new Tuple<string, string>("Unlock UNK Temple Exit",     ModuleExtrasValue.HasFlag(ModuleExtras.UnlockUnkTempleExit) ? "Unlocked" : "Locked"),
            };

            foreach (var setting in settings)
            {
                ws.Cell(i, 1).Value = setting.Item1;
                ws.Cell(i, 2).Value = setting.Item2;
                ws.Cell(i, 1).Style.Font.Italic = true;
                i++;
            }

            // Resize Columns
            ws.Column(1).AdjustToContents();
            ws.Column(2).AdjustToContents();
        }

        /// <summary>
        /// Creates a worksheet in the given XLWorkbook containing the list of changes made
        /// during randomization.
        /// </summary>
        public static void CreateSpoilerLog(XLWorkbook workbook, bool includeGeneral = true)
        {
            if (LookupTable.Count == 0) { return; }
            var ws = workbook.Worksheets.Add("Module");
            int i = 1;

            List<Tuple<string, string>> settings;

            if (includeGeneral)
            {
                // Write General settings.
                ws.Cell(i, 1).Value = "Seed";
                ws.Cell(i, 1).Style.Font.Bold = true;
                ws.Cell(i, 2).Value = Properties.Settings.Default.Seed;
                i++;

                Version version = typeof(StartForm).Assembly.GetName().Version;
                ws.Cell(i, 1).Value = "Version";
                ws.Cell(i, 1).Style.Font.Bold = true;
                ws.Cell(i, 2).Value = $"v{version.Major}.{version.Minor}.{version.Build}";
                ws.Cell(i, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                i += 2;     // Skip a row.

                ws.Cell(i, 1).Value = "General Setting";
                ws.Cell(i, 2).Value = "Is Enabled";
                ws.Cell(i, 1).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                ws.Cell(i, 2).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                ws.Cell(i, 1).Style.Font.Bold = true;
                ws.Cell(i, 2).Style.Font.Bold = true;
                i++;

                settings = new List<Tuple<string, string>>()
                {
                    new Tuple<string, string>("Delete Milestone Save Data", (!ModuleExtrasValue.HasFlag(ModuleExtras.NoSaveDelete)).ToString()),
                    new Tuple<string, string>("Include Minigames in Save",    ModuleExtrasValue.HasFlag(ModuleExtras.SaveMiniGames).ToString()),
                    new Tuple<string, string>("Include All Modules in Save",  ModuleExtrasValue.HasFlag(ModuleExtras.SaveAllModules).ToString()),
                    new Tuple<string, string>("", ""),  // Skip a row.
                    new Tuple<string, string>("Add Spice Lab Load Zone",      ModuleExtrasValue.HasFlag(ModuleExtras.VulkarSpiceLZ).ToString()),
                    new Tuple<string, string>("Fix Dream Sequence",           ModuleExtrasValue.HasFlag(ModuleExtras.FixDream).ToString()),
                    new Tuple<string, string>("Fix Mind Prison",              ModuleExtrasValue.HasFlag(ModuleExtras.FixMindPrison).ToString()),
                    new Tuple<string, string>("Fix Module Coordinates",       ModuleExtrasValue.HasFlag(ModuleExtras.FixCoordinates).ToString()),
                    new Tuple<string, string>("", ""),  // Skip a row.
                    new Tuple<string, string>("Unlock DAN Ruins Door",        ModuleExtrasValue.HasFlag(ModuleExtras.UnlockDanRuins     ) ? "Unlocked" : "Locked"),
                    new Tuple<string, string>("Unlock EBO Galaxy Map",        ModuleExtrasValue.HasFlag(ModuleExtras.UnlockGalaxyMap    ) ? "Unlocked" : "Locked"),
                    new Tuple<string, string>("Unlock KOR Valley",            ModuleExtrasValue.HasFlag(ModuleExtras.UnlockKorValley    ) ? "Unlocked" : "Locked"),
                    new Tuple<string, string>("Unlock LEV Elevators",         ModuleExtrasValue.HasFlag(ModuleExtras.UnlockLevElev      ) ? "Unlocked" : "Locked"),
                    new Tuple<string, string>("Unlock MAN Embassy",           ModuleExtrasValue.HasFlag(ModuleExtras.UnlockManEmbassy   ) ? "Unlocked" : "Locked"),
                    new Tuple<string, string>("Unlock MAN Hangar",            ModuleExtrasValue.HasFlag(ModuleExtras.UnlockManHangar    ) ? "Unlocked" : "Locked"),
                    new Tuple<string, string>("Unlock STA Door to Bastila",   ModuleExtrasValue.HasFlag(ModuleExtras.UnlockStaBastila   ) ? "Unlocked" : "Locked"),
                    new Tuple<string, string>("Unlock TAR Undercity",         ModuleExtrasValue.HasFlag(ModuleExtras.UnlockTarUndercity ) ? "Unlocked" : "Locked"),
                    new Tuple<string, string>("Unlock TAR Vulkar",            ModuleExtrasValue.HasFlag(ModuleExtras.UnlockTarVulkar    ) ? "Unlocked" : "Locked"),
                    new Tuple<string, string>("Unlock UNK Summit Exit",       ModuleExtrasValue.HasFlag(ModuleExtras.UnlockUnkSummit    ) ? "Unlocked" : "Locked"),
                    new Tuple<string, string>("Unlock UNK Temple Exit",       ModuleExtrasValue.HasFlag(ModuleExtras.UnlockUnkTempleExit) ? "Unlocked" : "Locked"),
                    new Tuple<string, string>("", ""),  // Skip a row.
                };

                foreach (var setting in settings)
                {
                    ws.Cell(i, 1).Value = setting.Item1;
                    ws.Cell(i, 2).Value = setting.Item2;
                    ws.Cell(i, 1).Style.Font.Italic = true;
                    i++;
                }
            }

            // Write logic settings.
            ws.Cell(i, 1).Value = "Reachability Logic";
            ws.Cell(i, 2).Value = "Is Enabled";
            ws.Cell(i, 1).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            ws.Cell(i, 2).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            ws.Cell(i, 1).Style.Font.Bold = true;
            ws.Cell(i, 2).Style.Font.Bold = true;
            i += 2;     // Skip a row.

            string presetName = ShufflePreset;
            bool isCustomPreset = false;
            if (string.IsNullOrWhiteSpace(ShufflePreset))
            {
                presetName = "Custom";
                isCustomPreset = true;
            }

            settings = new List<Tuple<string, string>>()
            {
                new Tuple<string, string>("Use Rando Exclusion Rules", UseRandoRules.ToString()),
                new Tuple<string, string>("Verify Reachability",       VerifyReachability.ToString()),
                new Tuple<string, string>("Goal Is Malak",             Digraph.GoalIsMalak.ToString()),
                new Tuple<string, string>("Goal Is Star Maps",         Digraph.GoalIsStarMap.ToString()),
                new Tuple<string, string>("Goal Is Pazaak",            Digraph.GoalIsPazaak.ToString()),
                new Tuple<string, string>("Allow Glitch Clipping",     Digraph.AllowGlitchClip.ToString()),
                new Tuple<string, string>("Allow Glitch DLZ",          Digraph.AllowGlitchDlz.ToString()),
                new Tuple<string, string>("Allow Glitch FLU",          Digraph.AllowGlitchFlu.ToString()),
                new Tuple<string, string>("Allow Glitch GPW",          Digraph.AllowGlitchGpw.ToString()),
                new Tuple<string, string>("Ignore Single-Use Edges",   Digraph.IgnoreOnceEdges.ToString()),
                new Tuple<string, string>("", ""),  // Skip a row.
                new Tuple<string, string>("Shuffle Preset",            presetName),
                new Tuple<string, string>("", ""),  // Skip a row.
            };

            foreach (var setting in settings)
            {
                ws.Cell(i, 1).Value = setting.Item1;
                ws.Cell(i, 2).Value = setting.Item2;
                ws.Cell(i, 1).Style.Font.Italic = true;
                i++;
            }

            // Custom Omitted Modules
            var omittedModules = Digraph.Modules.Where(m => OmittedModules.Contains(m.WarpCode)).OrderBy(m => m.WarpCode);

            if (isCustomPreset)
            {
                // List the omitted modules if the omitted modules have been customized.
                int iMax = i;
                i = 1;  // Restart at the top of the settings list.

                ws.Cell(i, 4).Value = "Omitted Modules";
                ws.Cell(i, 4).Style.Font.Bold = true;
                ws.Range(i, 4, i, 5).Merge().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                i++;

                ws.Cell(i, 4).Value = "Warp Code";
                ws.Cell(i, 5).Value = "Common Name";
                ws.Cell(i, 4).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                ws.Cell(i, 5).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                ws.Cell(i, 4).Style.Font.Italic = true;
                ws.Cell(i, 5).Style.Font.Italic = true;
                i++;

                foreach (var mod in omittedModules)
                {
                    ws.Cell(i, 4).Value = mod.WarpCode;
                    ws.Cell(i, 5).Value = mod.CommonName;
                    i++;
                }

                // Handle variable length omitted modules list.
                if (iMax > i) i = iMax; // Return to bottom of settings list.
                else i++;      // Skip a row.
            }

            // Module Shuffle
            ws.Cell(i, 1).Value = "Module Shuffle";
            ws.Cell(i, 1).Style.Font.Bold = true;
            ws.Range(i, 1, i, 5).Merge().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            i++;

            ws.Cell(i, 1).Value = "Has Changed";
            ws.Cell(i, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Cell(i, 1).Style.Font.Bold = true;
            ws.Range(i, 1, i + 1, 1).Merge().Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            ws.Cell(i, 2).Value = "New Destination";
            ws.Cell(i, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Cell(i, 2).Style.Font.Bold = true;
            ws.Cell(i, 2).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
            ws.Range(i, 2, i, 3).Merge();
            ws.Cell(i, 4).Value = "Old Destination";
            ws.Cell(i, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Cell(i, 4).Style.Font.Bold = true;
            ws.Cell(i, 4).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
            ws.Range(i, 4, i, 5).Merge();
            i++;

            ws.Cell(i, 2).Value = "Default Code";
            ws.Cell(i, 3).Value = "Default Name";
            ws.Cell(i, 4).Value = "Shuffled Code";
            ws.Cell(i, 5).Value = "Shuffled Name";
            ws.Cell(i, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Cell(i, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Cell(i, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Cell(i, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Cell(i, 2).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            ws.Cell(i, 2).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
            ws.Cell(i, 3).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            ws.Cell(i, 4).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            ws.Cell(i, 4).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
            ws.Cell(i, 5).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
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
                var omitted = omittedModules.Any(x => x.WarpCode == kvp.Key);   // Was the module omitted from the shuffle?
                var changed = kvp.Key != kvp.Value; // Has the shuffle changed this module?

                ws.Cell(i, 1).Value = omitted ? "OMITTED" : changed.ToString();
                ws.Cell(i, 2).Value = kvp.Key;
                ws.Cell(i, 2).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                ws.Cell(i, 3).Value = defaultName;
                ws.Cell(i, 4).Value = kvp.Value;
                ws.Cell(i, 4).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                ws.Cell(i, 5).Value = randomizedName;

                if (omitted)
                {
                    // Center "OMITTED" text.
                    ws.Cell(i, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                }
                else
                {
                    // Set color of "Has Changed" column. Booleans are automatically centered.
                    if (changed) ws.Cell(i, 1).Style.Font.FontColor = XLColor.Green;
                    else ws.Cell(i, 1).Style.Font.FontColor = XLColor.Red;
                }
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
        /// Populates and shuffles the the modules flagged to be randomized. Returns true if override files should be added.
        /// </summary>
        /// <param name="paths">KPaths object for this game.</param>
        /// <param name="k1rando">Kotor1Randomizer object that contains settings to use.</param>
        public static void Module_rando(KPaths paths, Models.Kotor1Randomizer k1rando = null)
        {
            // Prepare for a new randomization.
            Reset(k1rando);
            AssignSettings(k1rando);

            // Split the Bound modules into their respective lists.
            bool reachable = false;
            int iterations = 0;

            // Only shuffle if there is more than 1 module in the shuffle.
            if (RandomizedModules.Count > 1)
            {
                if (UseRandoRules || VerifyReachability)
                {
                    System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                    sw.Start();

                    while (!reachable && iterations < MAX_ITERATIONS)
                    {
                        iterations++;

                        Console.WriteLine($"Iteration {iterations}:");

                        CreateLookupTableShuffle();

                        Digraph.SetRandomizationLookup(LookupTable);

                        if (UseRandoRules)
                        {
                            // Skip to the next iteration if the rules are violated.
                            if (AreRulesViolated()) continue;
                        }

                        if (VerifyReachability)
                        {
                            Digraph.CheckReachability();
                            reachable = Digraph.IsGoalReachable();
                        }
                        else
                        {
                            reachable = true;
                        }
                    }

                    if (VerifyReachability)
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
                    CreateLookupTableShuffle();
                }
            }
            else
            {
                CreateLookupTableNoShuffle();
            }

            WriteFilesToModulesDirectory(paths);

            // Write additional override files (and unlock galaxy map).
            WriteOverrideFiles(paths);

            // Fix warp coordinates.
            if (ModuleExtrasValue.HasFlag(ModuleExtras.FixCoordinates))
            {
                FixWarpCoordinates(paths);
            }

            // Fixed Rakata riddle Man in Mind Prison.
            if (ModuleExtrasValue.HasFlag(ModuleExtras.FixMindPrison))
            {
                FixMindPrison(paths);
            }

            // Unlock locked doors or elevators.
            UnlockDoors(paths);

            // Vulkar Spice Lab Transition
            if (ModuleExtrasValue.HasFlag(ModuleExtras.VulkarSpiceLZ))
            {
                var vulk_files = paths.FilesInModules.Where(fi => fi.Name.Contains(LookupTable[AREA_TAR_VULK_BASE]));
                foreach (FileInfo fi in vulk_files)
                {
                    // Skip any files that end in "s.rim".
                    if (fi.Name[fi.Name.Length - 5] == 's') { continue; }

                    RIM r_vul = new RIM(fi.FullName);
                    r_vul.File_Table.FirstOrDefault(x => x.Label == LABEL_TAR_VULK_GIT && x.TypeID == (int)ResourceType.GIT).File_Data = Properties.Resources.m10aa;

                    r_vul.WriteToFile(fi.FullName);
                }
            }
        }

        private static void AssignSettings(Models.Kotor1Randomizer k1rando)
        {
            if (k1rando == null)
            {
                UseRandoRules      = Properties.Settings.Default.UseRandoRules;
                VerifyReachability = Properties.Settings.Default.VerifyReachability;
                ModuleExtrasValue  = Properties.Settings.Default.ModuleExtrasValue;
                RandomizedModules  = Globals.BoundModules.Where(x => !x.Omitted).Select(x => x.Code).ToList();
                OmittedModules     = Globals.BoundModules.Where(x =>  x.Omitted).Select(x => x.Code).ToList();
                if (Properties.Settings.Default.LastPresetComboIndex >= 0)
                    ShufflePreset  = Globals.OMIT_PRESETS.Keys.ToList()[Properties.Settings.Default.LastPresetComboIndex];
                else
                    ShufflePreset  = "";
            }
            else
            {
                UseRandoRules      = k1rando.ModuleLogicRandoRules;
                VerifyReachability = k1rando.ModuleLogicReachability;
                ModuleExtrasValue  = k1rando.GeneralModuleExtrasValue;
                foreach (var door in k1rando.GeneralUnlockedDoors)
                    ModuleExtrasValue |= door.Tag;
                RandomizedModules  = k1rando.ModuleRandomizedList.Select(x => x.WarpCode).ToList();
                OmittedModules     = k1rando.ModuleOmittedList.Select(x => x.WarpCode).ToList();
                ShufflePreset      = k1rando.ModuleShufflePreset;
            }
        }

        /// <summary>
        /// Creates backups for files modified during this randomization.
        /// </summary>
        /// <param name="paths"></param>
        internal static void CreateBackups(KPaths paths)
        {
            paths.BackUpModulesDirectory();
            paths.BackUpLipsDirectory();
            paths.BackUpOverrideDirectory();
        }

        /// <summary>
        /// Returns the common name of the given module code. Returns null if the code isn't found.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        internal static string GetModuleCommonName(string code)
        {
            return Digraph.Modules.FirstOrDefault(m => m.WarpCode == code)?.CommonName;
        }

        /// <summary>
        /// Resets any fields to prepare for a new shuffle.
        /// </summary>
        internal static void Reset(Models.Kotor1Randomizer k1rando = null)
        {
            // Reset digraph reachability settings.
            Digraph.ResetSettings(k1rando);
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
                    Console.WriteLine($" - Rule 1 violated: {ruleKVP.Key} replaces {lookup}");
                    return true;
                }
            }

            // Check rule 2
            foreach (var ruleKVP in Globals.RULE2)
            {
                var lookup = LookupTable[ruleKVP.Key];
                if (ruleKVP.Value.Contains(lookup))
                {
                    Console.WriteLine($" - Rule 2 violated: {ruleKVP.Key} replaces {lookup}");
                    return true;
                }
            }

            // Check rule 3
            foreach (var ruleKVP in Globals.RULE3)
            {
                var lookup = LookupTable[ruleKVP.Key];
                if (ruleKVP.Value.Contains(lookup))
                {
                    Console.WriteLine($" - Rule 3 violated: {ruleKVP.Key} replaces {lookup}");
                    return true;
                }
            }

            //Console.WriteLine("No rules violated.");
            return false;
        }

        /// <summary>
        /// LookupTable is created from the global BoundModules without shuffling.
        /// </summary>
        private static void CreateLookupTableNoShuffle()
        {
            // Create lookup table for later features.
            LookupTable.Clear();
            foreach (var item in Digraph.Modules)
            {
                LookupTable.Add(item.WarpCode, item.WarpCode);
            }
        }

        /// <summary>
        /// LookupTable is created from the global BoundModules after shuffling included modules.
        /// </summary>
        private static void CreateLookupTableShuffle()
        {
            // Shuffle the list of included modules.
            List<string> shuffle = new List<string>(RandomizedModules);
            Randomize.FisherYatesShuffle(shuffle);
            LookupTable.Clear();

            for (int i = 0; i < RandomizedModules.Count; i++)
            {
                LookupTable.Add(RandomizedModules[i], shuffle[i]);
            }

            // Include the unmodified list of excluded modules.
            foreach (string name in OmittedModules)
            {
                LookupTable.Add(name, name);
            }
        }

        /// <summary>
        /// Unlock Leviathan Hangar option in the other two elevator access, and enables the use of the Hangar elevator.
        /// </summary>
        /// <param name="paths">KPaths object for this game.</param>
        private static void FixLeviathanElevators(KPaths paths)
        {
            var lev_files_a = paths.FilesInModules.Where(fi => fi.Name.Contains(LookupTable[AREA_LEV_PRISON]));
            var lev_files_b = paths.FilesInModules.Where(fi => fi.Name.Contains(LookupTable[AREA_LEV_COMMAND]));
            var lev_files_c = paths.FilesInModules.Where(fi => fi.Name.Contains(LookupTable[AREA_LEV_HANGAR]));

            // Prison Block Fix - Unlock option to visit Hangar.
            foreach (FileInfo fi in lev_files_a)
            {
                // Skip any files that don't end in "s.rim".
                if (fi.Name[fi.Name.Length - 5] != 's') { continue; }

                RIM r_lev = new RIM(fi.FullName);
                GFF g_lev = new GFF(r_lev.File_Table.FirstOrDefault(x => x.Label == LABEL_LEV_ELEVATOR_A).File_Data);

                // Change Entry connecting for bridge option Index to 3, which will transition to the command deck
                (((g_lev.Top_Level.Fields.FirstOrDefault(x => x.Label == "ReplyList")
                    as GFF.LIST).Structs.FirstOrDefault(x => x.Struct_Type == 3).Fields.FirstOrDefault(x => x.Label == "EntriesList")
                    as GFF.LIST).Structs.FirstOrDefault(x => x.Struct_Type == 0).Fields.FirstOrDefault(x => x.Label == "Index")
                    as GFF.DWORD).Value = 3;

                // Sets the active reference for the hangar option to nothing, meaning there is no requirement to transition to the hangar
                (((g_lev.Top_Level.Fields.FirstOrDefault(x => x.Label == "ReplyList")
                    as GFF.LIST).Structs.FirstOrDefault(x => x.Struct_Type == 1).Fields.FirstOrDefault(x => x.Label == "EntriesList")
                    as GFF.LIST).Structs.FirstOrDefault(x => x.Struct_Type == 0).Fields.FirstOrDefault(x => x.Label == "Active")
                    as GFF.ResRef).Reference = "";

                r_lev.File_Table.FirstOrDefault(x => x.Label == LABEL_LEV_ELEVATOR_A).File_Data = g_lev.ToRawData();

                r_lev.WriteToFile(fi.FullName);
            }

            // Command Deck Fix - Unlock option to visit Hangar.
            foreach (FileInfo fi in lev_files_b)
            {
                // Skip any files that don't end in "s.rim".
                if (fi.Name[fi.Name.Length - 5] != 's') { continue; }

                RIM r_lev = new RIM(fi.FullName);
                GFF g_lev = new GFF(r_lev.File_Table.FirstOrDefault(x => x.Label == LABEL_LEV_ELEVATOR_B).File_Data);

                // Sets the active reference for the hangar option to nothing, meaning there is no requirement to transition to the hangar
                (((g_lev.Top_Level.Fields.FirstOrDefault(x => x.Label == "ReplyList")
                    as GFF.LIST).Structs.FirstOrDefault(x => x.Struct_Type == 1).Fields.FirstOrDefault(x => x.Label == "EntriesList")
                    as GFF.LIST).Structs.FirstOrDefault(x => x.Struct_Type == 1).Fields.FirstOrDefault(x => x.Label == "Active")
                    as GFF.ResRef).Reference = "";

                r_lev.File_Table.FirstOrDefault(x => x.Label == LABEL_LEV_ELEVATOR_B).File_Data = g_lev.ToRawData();

                r_lev.WriteToFile(fi.FullName);
            }

            // Hangar Fix - Enable the elevator so it can be used.
            foreach (FileInfo fi in lev_files_c)
            {
                // Skip any files that don't end in "s.rim".
                if (fi.Name[fi.Name.Length - 5] != 's') { continue; }

                RIM r_lev = new RIM(fi.FullName);

                // While I possess the ability to edit this file programmatically, due to the complexity I have opted to just load the modded file into resources.
                r_lev.File_Table.FirstOrDefault(x => x.Label == LABEL_LEV_ELEVATOR_C).File_Data = Properties.Resources.lev40_accntl_dlg;

                // Adding module transition scripts to RIM...
                // Prison Block
                r_lev.File_Table.Add(new RIM.rFile
                {
                    TypeID = (int)ResourceType.NCS,
                    Label = "k_plev_goto40aa",
                    File_Data = Properties.Resources.k_plev_goto40aa
                });
                // Command Deck
                r_lev.File_Table.Add(new RIM.rFile
                {
                    TypeID = (int)ResourceType.NCS,
                    Label = "k_plev_goto40ab",
                    File_Data = Properties.Resources.k_plev_goto40ab
                });

                r_lev.WriteToFile(fi.FullName);
            }
        }

        /// <summary>
        /// Allow the Mystery Box and Mind Prison to be used more than once.
        /// </summary>
        /// <param name="paths">KPaths object for this game.</param>
        private static void FixMindPrison(KPaths paths)
        {
            // Allowing the Mystery Box to be accessed multiple times.
            ReplaceSRimFileData(paths, AREA_EBO_HAWK, LABEL_EBO_BOX, Properties.Resources.pebn_mystery);

            // Allowing Riddles to be done more than once.
            ReplaceSRimFileData(paths, AREA_EBO_BOX, LABEL_EBO_PRISON, Properties.Resources.g_brakatan003);
        }

        /// <summary>
        /// Update warp coordinates that are in bad locations by default.
        /// </summary>
        /// <param name="paths">KPaths object for this game.</param>
        private static void FixWarpCoordinates(KPaths paths)
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
                RIM.rFile rf = r.File_Table.FirstOrDefault(x => x.TypeID == (int)ResourceType.IFO);

                GFF g = new GFF(rf.File_Data);

                // Update coordinate data.
                (g.Top_Level.Fields.FirstOrDefault(x => x.Label == Properties.Resources.ModuleEntryX) as GFF.FLOAT).Value = Globals.FIXED_COORDINATES[kvp.Key].Item1;
                (g.Top_Level.Fields.FirstOrDefault(x => x.Label == Properties.Resources.ModuleEntryY) as GFF.FLOAT).Value = Globals.FIXED_COORDINATES[kvp.Key].Item2;
                (g.Top_Level.Fields.FirstOrDefault(x => x.Label == Properties.Resources.ModuleEntryZ) as GFF.FLOAT).Value = Globals.FIXED_COORDINATES[kvp.Key].Item3;

                // Write updated data to RIM file.
                rf.File_Data = g.ToRawData();
                r.WriteToFile(kvp.Value.FullName);
            }
        }

        /// <summary>
        /// Replace file data within an SRim file.
        /// </summary>
        /// <param name="paths">KPaths object for this game.</param>
        /// <param name="area">Name of the SRim file to modify.</param>
        /// <param name="label">Label of the rFile to update.</param>
        /// <param name="rawData">File data to store in the rFile.</param>
        private static void ReplaceSRimFileData(KPaths paths, string area, string label, byte[] rawData)
        {
            // Find the files associated with this area.
            var area_files = paths.FilesInModules.Where(fi => fi.Name.Contains(LookupTable[area]));
            foreach (FileInfo file in area_files)
            {
                // Skip any files that don't end in "s.rim".
                if (file.Name[file.Name.Length - 5] != 's') { continue; }

                // Check the RIM's File_Table for any rFiles with the given label.
                RIM rim = new RIM(file.FullName);
                var rFiles = rim.File_Table.Where(x => x.Label == label);
                foreach (RIM.rFile rFile in rFiles)
                {
                    rFile.File_Data = rawData;
                }

                rim.WriteToFile(file.FullName);
            }
        }

        /// <summary>
        /// Unlock a specific door within an SRim file.
        /// </summary>
        /// <param name="paths">KPaths object for this game.</param>
        /// <param name="area">Name of the SRim file to modify.</param>
        /// <param name="label">Label of the door to unlock.</param>
        private static void UnlockDoorInFile(KPaths paths, string area, string label)
        {
            var areaFiles = paths.FilesInModules.Where(fi => fi.Name.Contains(LookupTable[area]));
            foreach (FileInfo fi in areaFiles)
            {
                // Skip any files that don't end in "s.rim".
                if (fi.Name[fi.Name.Length - 5] != 's') { continue; }

                RIM r = new RIM(fi.FullName);   // Open what replaced this area.
                RIM.rFile rf = r.File_Table.FirstOrDefault(x => x.TypeID == (int)ResourceType.UTD && x.Label == label);
                GFF g = new GFF(rf.File_Data);  // Grab the door out of the file.

                // Set fields related to opening and unlocking.
                (g.Top_Level.Fields.FirstOrDefault(x => x.Label == "KeyRequired") as GFF.BYTE).Value = 0;
                (g.Top_Level.Fields.FirstOrDefault(x => x.Label == "Locked") as GFF.BYTE).Value = 0;
                (g.Top_Level.Fields.FirstOrDefault(x => x.Label == "OpenLockDC") as GFF.BYTE).Value = 0;
                (g.Top_Level.Fields.FirstOrDefault(x => x.Label == "Plot") as GFF.BYTE).Value = 0;

                // Set fields related to bashing open.
                (g.Top_Level.Fields.FirstOrDefault(x => x.Label == "Hardness") as GFF.BYTE).Value = 0;
                (g.Top_Level.Fields.FirstOrDefault(x => x.Label == "HP") as GFF.SHORT).Value = 1;
                (g.Top_Level.Fields.FirstOrDefault(x => x.Label == "CurrentHP") as GFF.SHORT).Value = 1;
                (g.Top_Level.Fields.FirstOrDefault(x => x.Label == "Min1HP") as GFF.BYTE).Value = 0;

                // Write change(s) to file.
                rf.File_Data = g.ToRawData();
                r.WriteToFile(fi.FullName);
            }
        }

        /// <summary>
        /// Unlock the doors requested by the user.
        /// </summary>
        /// <param name="paths">KPaths object for this game.</param>
        private static void UnlockDoors(KPaths paths)
        {
            var extrasValue = ModuleExtrasValue;

            // Dantooine Ruins
            if (extrasValue.HasFlag(ModuleExtras.UnlockDanRuins))
            {
                UnlockDoorInFile(paths, AREA_DAN_COURTYARD, LABEL_DANT_DOOR);
            }

            // Korriban After the Tomb Encounter
            if (extrasValue.HasFlag(ModuleExtras.UnlockKorValley))
            {
                UnlockDoorInFile(paths, AREA_KOR_ENTRANCE, LABEL_KOR_ENTRANCE_ACADEMY);
                UnlockDoorInFile(paths, AREA_KOR_VALLEY, LABEL_KOR_VALLEY_ACADEMY);
                UnlockDoorInFile(paths, AREA_KOR_VALLEY, LABEL_KOR_VALLEY_AJUNTA);
                UnlockDoorInFile(paths, AREA_KOR_VALLEY, LABEL_KOR_VALLEY_MARKA);
                UnlockDoorInFile(paths, AREA_KOR_VALLEY, LABEL_KOR_VALLEY_NAGA);
                UnlockDoorInFile(paths, AREA_KOR_VALLEY, LABEL_KOR_VALLEY_TULAK);
            }

            // Leviathan Elevators
            if (extrasValue.HasFlag(ModuleExtras.UnlockLevElev))
            {
                FixLeviathanElevators(paths);
            }

            // Manaan Embassy Door to Submersible
            if (extrasValue.HasFlag(ModuleExtras.UnlockManEmbassy))
            {
                UnlockDoorInFile(paths, AREA_MAN_EAST_CENTRAL, LABEL_MAN_SUB_DOOR03);   // Unlock door into Republic Embassy.
                UnlockDoorInFile(paths, AREA_MAN_EAST_CENTRAL, LABEL_MAN_SUB_DOOR05);   // Unlock door to submersible.
            }

            // Manaan Sith Hangar Door
            if (extrasValue.HasFlag(ModuleExtras.UnlockManHangar))
            {
                UnlockDoorInFile(paths, AREA_MAN_DOCKING_BAY, LABEL_MAN_SITH_HANGAR);   // Unlock door into Republic Embassy.
            }

            // Star Forge Door to Bastila
            if (extrasValue.HasFlag(ModuleExtras.UnlockStaBastila))
            {
                UnlockDoorInFile(paths, AREA_STA_DECK3, LABEL_STA_BAST_DOOR);
            }

            // Taris Lower City Door to Undercity
            if (extrasValue.HasFlag(ModuleExtras.UnlockTarUndercity))
            {
                UnlockDoorInFile(paths, AREA_TAR_LOWER_CITY, LABEL_TAR_UNDERCITY);
            }

            // Taris Lower City Door to Vulkar Base
            if (extrasValue.HasFlag(ModuleExtras.UnlockTarVulkar))
            {
                UnlockDoorInFile(paths, AREA_TAR_LOWER_CITY, LABEL_TAR_VULKAR);
            }

            // Lehon Temple Roof
            if (extrasValue.HasFlag(ModuleExtras.UnlockUnkSummit))
            {
                UnlockDoorInFile(paths, AREA_UNK_SUMMIT, LABEL_UNK_DOOR);
            }

            // Lehon Temple Main Floor
            if (extrasValue.HasFlag(ModuleExtras.UnlockUnkTempleExit))
            {
                UnlockDoorInFile(paths, AREA_UNK_MAIN_FLOOR, LABEL_UNK_EXIT_DOOR);
            }
        }

        /// <summary>
        /// Copy backup module files to the modules directory based on the current shuffle.
        /// </summary>
        /// <param name="paths">KPaths object for this game.</param>
        private static void WriteFilesToModulesDirectory(KPaths paths)
        {
            // Copy shuffled modules into the base directory.
            foreach (var name in LookupTable)
            {
                File.Copy($"{paths.modules_backup}{name.Key}.rim", $"{paths.modules}{name.Value}.rim", true);
                File.Copy($"{paths.modules_backup}{name.Key}_s.rim", $"{paths.modules}{name.Value}_s.rim", true);
                File.Copy($"{paths.lips_backup}{name.Key}_loc.mod", $"{paths.lips}{name.Value}_loc.mod", true);
            }

            // Copy lips extras into the base directory.
            foreach (string name in Globals.lipXtras)
            {
                File.Copy($"{paths.lips_backup}{name}", $"{paths.lips}{name}", true);
            }
        }

        /// <summary>
        /// Write special files to the override folder - save data, dream fix, galaxy map unlock, etc.
        /// </summary>
        /// <param name="paths">KPaths object for this game.</param>
        private static void WriteOverrideFiles(KPaths paths)
        {
            string moduleSavePath = Path.Combine(paths.Override, TwoDA_MODULE_SAVE);
            ModuleExtras saveFileExtras = ModuleExtrasValue & (ModuleExtras.SaveAllModules | ModuleExtras.SaveMiniGames | ModuleExtras.NoSaveDelete);

            // Save Data File
            switch ((int)saveFileExtras)
            {
                default:
                    // 0b000 - Milestone Delete (Default)
                    // Do nothing.
                    break;

                case (int)(ModuleExtras.NoSaveDelete):
                    // 0b001 - No Milestone Delete
                    File.WriteAllBytes(moduleSavePath, Properties.Resources.NODELETE_modulesave);
                    break;

                case (int)(ModuleExtras.SaveMiniGames):
                    // 0b010 - Save Minigames | Milestone Delete
                    File.WriteAllBytes(moduleSavePath, Properties.Resources.MGINCLUDED_modulesave);
                    break;

                case (int)(ModuleExtras.NoSaveDelete | ModuleExtras.SaveMiniGames):
                    // 0b011 - Save Minigames | No Milestone Delete
                    File.WriteAllBytes(moduleSavePath, Properties.Resources.NODELETE_MGINCLUDED_modulesave);
                    break;

                case (int)(ModuleExtras.SaveAllModules):
                case (int)(ModuleExtras.SaveMiniGames | ModuleExtras.SaveAllModules):
                    // Treat both the same.
                    // 0b100 - Save All Modules | Milestone Delete
                    // 0b110 - Save All Modules | Save Minigames | Milestone Delete
                    File.WriteAllBytes(moduleSavePath, Properties.Resources.ALLINCLUDED_modulesave);
                    break;

                case (int)(ModuleExtras.NoSaveDelete | ModuleExtras.SaveAllModules):
                case (int)(ModuleExtras.NoSaveDelete | ModuleExtras.SaveMiniGames | ModuleExtras.SaveAllModules):
                    // Treat both the same.
                    // 0b101 - Save All Modules | No Milestone Delete
                    // 0b111 - Save All Modules | Save Minigames | No Milestone Delete
                    File.WriteAllBytes(moduleSavePath, Properties.Resources.NODELETE_ALLINCLUDED_modulesave);
                    break;
            }

            // Fix Dream File
            if (ModuleExtrasValue.HasFlag(ModuleExtras.FixDream))
            {
                File.WriteAllBytes(Path.Combine(paths.Override, FIXED_DREAM_OVERRIDE), Properties.Resources.k_ren_visionland);
            }

            // Unlock Galaxy Map File
            if (ModuleExtrasValue.HasFlag(ModuleExtras.UnlockGalaxyMap))
            {
                File.WriteAllBytes(Path.Combine(paths.Override, UNLOCK_MAP_OVERRIDE), Properties.Resources.k_pebn_galaxy);
            }
        }

        #endregion Methods
    }
}