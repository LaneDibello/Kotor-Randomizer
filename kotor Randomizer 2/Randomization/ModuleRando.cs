using ClosedXML.Excel;
using KotOR_IO;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System;
using kotor_Randomizer_2.Extensions;
using System.Threading.Tasks;

namespace kotor_Randomizer_2
{
    public static class ModuleRando
    {
        #region Consts
        #region K1 Consts

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
        private const string FIXED_FIGHTER_OVERRIDE = "k_pebo_mgheart.ncs";

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

        private const string UNLOCK_MAP_OVERRIDE = "k_pebn_galaxy.ncs";
        private const string KOR_OPEN_ACADEMY = "k33b_openacademy.ncs";
        private const string KOR_VALLEY_ENTER = "k36_pkor_enter.ncs";

        #endregion

        #region K2 Consts

        // Zones
        private const string AREA_K2_PER_ADMIN     = "101PER";
        private const string AREA_K2_PER_FUEL      = "103PER";
        private const string AREA_K2_PER_ASTROID   = "104PER";
        private const string AREA_K2_PER_DORMS     = "105PER";
        private const string AREA_K2_PER_HANGER    = "106PER";
        private const string AREA_K2_TEL_RES       = "203TEL";
        private const string AREA_K2_TEL_ENTER_WAR = "222TEL";
        private const string AREA_K2_TEL_ACAD      = "262TEL";
        private const string AREA_K2_NAR_DOCKS     = "303NAR";
        private const string AREA_K2_NAR_JEKK      = "304NAR";
        private const string AREA_K2_NAR_J_TUNNELS = "305NAR";
        private const string AREA_K2_NAR_G0T0      = "351NAR";
        private const string AREA_K2_DAN_COURTYARD = "605DAN";
        private const string AREA_K2_KOR_ACAD      = "702KOR";
        private const string AREA_K2_KOR_SHY       = "710KOR";
        private const string AREA_K2_MAL_SURFACE   = "901MAL";

        // Locked Doors
        private const string LABEL_K2_101PERTODORMS         = "sw_door_per006";     // Dorms from Admin
        private const string LABEL_K2_101PERTOMININGTUNNELS = "sw_door_per004";     // Mining Tunnels from Admin
        private const string LABEL_K2_101PERTOFUELDEPOT     = "sw_door_per007";     // Fuel Depot from Admin
        private const string LABEL_K2_101PERTOHARBINGER     = "sw_door_taris009";   // Though the docking bay form Admin
        private const string LABEL_K2_103PERTOMININGTUNNELS = "sw_door_per006";     // Explosion door at start of module
        private const string LABEL_K2_103PERFORCESHIELDS    = "sw_door_per005";     // Force fields splitting fuel depot into two sections
        private const string LABEL_K2_103PERSHIELD2         = "sw_door_per010";     // Secondary Fuel Depot Shield blocking way down
        private const string LABEL_K2_105PERTOASTROID       = "sw_door_per005";     // Return to astroid exterior from Dormitory
        private const string LABEL_K2_106PEREASTDOOR        = "sw_door_per003";     // Door leading to Ebon Hawk
        private const string LABEL_K2_203TELAPPTDOOR        = "adoor_intro";        // Appartment door we spawn behind
        private const string LABEL_K2_203TELEXCHANGE        = "sw_door_telos002";   // Entacnce to Exchange base
        private const string LABEL_K2_222TELRAVAGER         = "d_222doorseal001";   // Board the ravager with mandalore, Visas, or the horrid DLZ
        private const string LABEL_K2_262TELPLATEAU         = "sw_door_sforg001";   // Polar Plateau from Atris's academy
        private const string LABEL_K2_303NARZEZDOOR         = "door_flophouse_s";   // Into the Secret zone with Mira and Zez that frequently breaks even in normal gameplay
        private const string LABEL_K2_304NARBACKROOM        = "visquisdoor";        // Door in Jekk'Jekk Tarr leading to Visquis's private suit, and the Tunnels
        private const string LABEL_K2_305NARTOJEKKJEKK      = "door_narshad002";    // Leave the tunnels and return to the cantina for once
        private const string LABEL_K2_351NARG0T0EBONHAWK    = "door_narshad008";    // Reboard the Ebon Hawk without doing the entirity of G0T0's yacht, though the CS it leads to breaks frequently, look into other options
        private const string LABEL_K2_605DANREBUILTENCLAVE  = "door_650";           // Enter the Rebuilt Jedi Enclave Early
        private const string LABEL_K2_702KORVALLEY          = "door_enter";         // Leave the Sith acadmeny without doing 10 minutes of puzzles or a DLZ
        private const string LABEL_K2_710KORLUDOKRESSH      = "sealeddoor";         // Enter the secret tomb in the shyrack cave without heavy alignment

        // Patches
        private const string PATCH_K2_MODULESAVE = "modulesave.2da";
        private const string PATCH_K2_GALAXYMAP = "a_galaxymap.ncs";
        private const string PATCH_K2_DISC_JOIN = "a_disc_join.ncs";

        #endregion

        private const int MAX_ITERATIONS = 10000;   // A large number to give enough chances to find a valid shuffle.
        private const string TwoDA_MODULE_SAVE = "modulesave.2da";

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

        public static Models.Game GameRandomized { get; set; }

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
                new Tuple<string, string>("Prevent Milestone Save Data Deletion", ModuleExtrasValue.HasFlag(ModuleExtras.NoSaveDelete  ).ToEnabledDisabled()),
                new Tuple<string, string>("Include Minigames in Save",            ModuleExtrasValue.HasFlag(ModuleExtras.SaveMiniGames ).ToEnabledDisabled()),
                new Tuple<string, string>("Include All Modules in Save",          ModuleExtrasValue.HasFlag(ModuleExtras.SaveAllModules).ToEnabledDisabled()),
                new Tuple<string, string>("", ""),  // Skip a row.
            };

            foreach (var setting in settings)
            {
                ws.Cell(i, 1).Value = setting.Item1;
                ws.Cell(i, 2).Value = setting.Item2;
                ws.Cell(i, 1).Style.Font.Italic = true;
                ws.Cell(i, 2).DataType = XLDataType.Text;
                ws.Cell(i, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
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
                new Tuple<string, string>("Add Spice Lab Load Zone", ModuleExtrasValue.HasFlag(ModuleExtras.VulkarSpiceLZ      ).ToEnabledDisabled()),
                new Tuple<string, string>("Fix Dream Sequence",      ModuleExtrasValue.HasFlag(ModuleExtras.FixDream           ).ToEnabledDisabled()),
                new Tuple<string, string>("Fix Fighter Encounter",   ModuleExtrasValue.HasFlag(ModuleExtras.FixFighterEncounter).ToEnabledDisabled()),
                new Tuple<string, string>("Fix Mind Prison",         ModuleExtrasValue.HasFlag(ModuleExtras.FixMindPrison      ).ToEnabledDisabled()),
                new Tuple<string, string>("Fix Module Coordinates",  ModuleExtrasValue.HasFlag(ModuleExtras.FixCoordinates     ).ToEnabledDisabled()),
                new Tuple<string, string>("", ""),  // Skip a row.
            };

            foreach (var setting in settings)
            {
                ws.Cell(i, 1).Value = setting.Item1;
                ws.Cell(i, 2).Value = setting.Item2;
                ws.Cell(i, 1).Style.Font.Italic = true;
                ws.Cell(i, 2).DataType = XLDataType.Text;
                ws.Cell(i, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
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
                new Tuple<string, string>("Unlock DAN Ruins Door",      ModuleExtrasValue.HasFlag(ModuleExtras.UnlockDanRuins     ).ToLockedUnlocked(true)),
                new Tuple<string, string>("Unlock EBO Galaxy Map",      ModuleExtrasValue.HasFlag(ModuleExtras.UnlockGalaxyMap    ).ToLockedUnlocked(true)),
                new Tuple<string, string>("Unlock KOR Valley",          ModuleExtrasValue.HasFlag(ModuleExtras.UnlockKorValley    ).ToLockedUnlocked(true)),
                new Tuple<string, string>("Unlock LEV Hangar Access",   ModuleExtrasValue.HasFlag(ModuleExtras.UnlockLevElev      ).ToLockedUnlocked(true)),
                new Tuple<string, string>("Enable LEV Hangar Elevator", ModuleExtrasValue.HasFlag(ModuleExtras.EnableLevHangarElev).ToEnabledDisabled()),
                new Tuple<string, string>("Unlock MAN Embassy",         ModuleExtrasValue.HasFlag(ModuleExtras.UnlockManEmbassy   ).ToLockedUnlocked(true)),
                new Tuple<string, string>("Unlock MAN Hangar",          ModuleExtrasValue.HasFlag(ModuleExtras.UnlockManHangar    ).ToLockedUnlocked(true)),
                new Tuple<string, string>("Unlock STA Door to Bastila", ModuleExtrasValue.HasFlag(ModuleExtras.UnlockStaBastila   ).ToLockedUnlocked(true)),
                new Tuple<string, string>("Unlock TAR Undercity",       ModuleExtrasValue.HasFlag(ModuleExtras.UnlockTarUndercity ).ToLockedUnlocked(true)),
                new Tuple<string, string>("Unlock TAR Vulkar",          ModuleExtrasValue.HasFlag(ModuleExtras.UnlockTarVulkar    ).ToLockedUnlocked(true)),
                new Tuple<string, string>("Unlock UNK Summit Exit",     ModuleExtrasValue.HasFlag(ModuleExtras.UnlockUnkSummit    ).ToLockedUnlocked(true)),
                new Tuple<string, string>("Unlock UNK Temple Exit",     ModuleExtrasValue.HasFlag(ModuleExtras.UnlockUnkTempleExit).ToLockedUnlocked(true)),
            };

            foreach (var setting in settings)
            {
                ws.Cell(i, 1).Value = setting.Item1;
                ws.Cell(i, 2).Value = setting.Item2;
                ws.Cell(i, 1).Style.Font.Italic = true;
                ws.Cell(i, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
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
            var i = 1;

            List<Tuple<string, string>> settings;

            if (includeGeneral)
            {
                // Write General settings.
                ws.Cell(i, 1).Value = "Seed";
                ws.Cell(i, 1).Style.Font.Bold = true;
                ws.Cell(i, 2).Value = Properties.Settings.Default.Seed;
                i++;

                var version = typeof(StartForm).Assembly.GetName().Version;
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

                settings = GameRandomized == Models.Game.Kotor1
                    ? new List<Tuple<string, string>>()
                    {
                        new Tuple<string, string>("Delete Milestone Save Data", (!ModuleExtrasValue.HasFlag(ModuleExtras.NoSaveDelete )).ToEnabledDisabled()),
                        new Tuple<string, string>("Include Minigames in Save",    ModuleExtrasValue.HasFlag(ModuleExtras.SaveMiniGames ).ToEnabledDisabled()),
                        new Tuple<string, string>("Include All Modules in Save",  ModuleExtrasValue.HasFlag(ModuleExtras.SaveAllModules).ToEnabledDisabled()),
                        new Tuple<string, string>("", ""),  // Skip a row.
                        new Tuple<string, string>("Add Spice Lab Load Zone",      ModuleExtrasValue.HasFlag(ModuleExtras.VulkarSpiceLZ      ).ToEnabledDisabled()),
                        new Tuple<string, string>("Fix Dream Sequence",           ModuleExtrasValue.HasFlag(ModuleExtras.FixDream           ).ToEnabledDisabled()),
                        new Tuple<string, string>("Fix Fighter Encounter",        ModuleExtrasValue.HasFlag(ModuleExtras.FixFighterEncounter).ToEnabledDisabled()),
                        new Tuple<string, string>("Fix Mind Prison",              ModuleExtrasValue.HasFlag(ModuleExtras.FixMindPrison      ).ToEnabledDisabled()),
                        new Tuple<string, string>("Fix Module Coordinates",       ModuleExtrasValue.HasFlag(ModuleExtras.FixCoordinates     ).ToEnabledDisabled()),
                        new Tuple<string, string>("", ""),  // Skip a row.
                        new Tuple<string, string>("Unlock DAN Ruins Door",        ModuleExtrasValue.HasFlag(ModuleExtras.UnlockDanRuins     ).ToLockedUnlocked(true)),
                        new Tuple<string, string>("Unlock EBO Galaxy Map",        ModuleExtrasValue.HasFlag(ModuleExtras.UnlockGalaxyMap    ).ToLockedUnlocked(true)),
                        new Tuple<string, string>("Unlock KOR Valley",            ModuleExtrasValue.HasFlag(ModuleExtras.UnlockKorValley    ).ToLockedUnlocked(true)),
                        new Tuple<string, string>("Unlock LEV Hangar Access",     ModuleExtrasValue.HasFlag(ModuleExtras.UnlockLevElev      ).ToLockedUnlocked(true)),
                        new Tuple<string, string>("Enable LEV Hangar Elevator",   ModuleExtrasValue.HasFlag(ModuleExtras.EnableLevHangarElev).ToEnabledDisabled()),
                        new Tuple<string, string>("Unlock MAN Embassy",           ModuleExtrasValue.HasFlag(ModuleExtras.UnlockManEmbassy   ).ToLockedUnlocked(true)),
                        new Tuple<string, string>("Unlock MAN Hangar",            ModuleExtrasValue.HasFlag(ModuleExtras.UnlockManHangar    ).ToLockedUnlocked(true)),
                        new Tuple<string, string>("Unlock STA Door to Bastila",   ModuleExtrasValue.HasFlag(ModuleExtras.UnlockStaBastila   ).ToLockedUnlocked(true)),
                        new Tuple<string, string>("Unlock TAR Undercity",         ModuleExtrasValue.HasFlag(ModuleExtras.UnlockTarUndercity ).ToLockedUnlocked(true)),
                        new Tuple<string, string>("Unlock TAR Vulkar",            ModuleExtrasValue.HasFlag(ModuleExtras.UnlockTarVulkar    ).ToLockedUnlocked(true)),
                        new Tuple<string, string>("Unlock UNK Summit Exit",       ModuleExtrasValue.HasFlag(ModuleExtras.UnlockUnkSummit    ).ToLockedUnlocked(true)),
                        new Tuple<string, string>("Unlock UNK Temple Exit",       ModuleExtrasValue.HasFlag(ModuleExtras.UnlockUnkTempleExit).ToLockedUnlocked(true)),
                        new Tuple<string, string>("", ""),  // Skip a row.
                    }
                    : new List<Tuple<string, string>>()
                    {
                        new Tuple<string, string>("Prevent Save Deletion", ModuleExtrasValue.HasFlag(ModuleExtras.K2SavePatch    ).ToEnabledDisabled()),
                        new Tuple<string, string>("Unlock Galaxy Map",     ModuleExtrasValue.HasFlag(ModuleExtras.K2GalaxyMap    ).ToEnabledDisabled()),
                        new Tuple<string, string>("Patch Disciple Crash",  ModuleExtrasValue.HasFlag(ModuleExtras.K2DisciplePatch).ToEnabledDisabled()),
                    };

                foreach (var setting in settings)
                {
                    ws.Cell(i, 1).Value = setting.Item1;
                    ws.Cell(i, 2).Value = setting.Item2;
                    ws.Cell(i, 1).Style.Font.Italic = true;
                    ws.Cell(i, 2).DataType = XLDataType.Text;
                    ws.Cell(i, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    i++;
                }
            }

            // Write logic settings.
            ws.Cell(i, 1).Value = "Reachability Logic";
            ws.Cell(i, 2).Value = "Setting";
            ws.Cell(i, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Cell(i, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Cell(i, 1).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            ws.Cell(i, 2).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            ws.Cell(i, 1).Style.Font.Bold = true;
            ws.Cell(i, 2).Style.Font.Bold = true;
            i++;    // Skip a row.

            string presetName = ShufflePreset;
            bool isCustomPreset = false;
            if (string.IsNullOrWhiteSpace(ShufflePreset))
            {
                presetName = "Custom";
                isCustomPreset = true;
            }

            settings = GameRandomized == Models.Game.Kotor1
                ? new List<Tuple<string, string>>()
                {
                    new Tuple<string, string>("Use Rando Exclusion Rules", UseRandoRules          .ToEnabledDisabled()),
                    new Tuple<string, string>("Verify Reachability",       VerifyReachability     .ToEnabledDisabled()),
                    new Tuple<string, string>("Goal Is Malak",             Digraph.GoalIsMalak    .ToEnabledDisabled()),
                    new Tuple<string, string>("Goal Is Star Maps",         Digraph.GoalIsStarMap  .ToEnabledDisabled()),
                    new Tuple<string, string>("Goal Is Full Party",        Digraph.GoalIsFullParty.ToEnabledDisabled()),
                    new Tuple<string, string>("Goal Is Pazaak",            Digraph.GoalIsPazaak   .ToEnabledDisabled()),
                    new Tuple<string, string>("Allow Glitch Clipping",     Digraph.AllowGlitchClip.ToEnabledDisabled()),
                    new Tuple<string, string>("Allow Glitch DLZ",          Digraph.AllowGlitchDlz .ToEnabledDisabled()),
                    new Tuple<string, string>("Allow Glitch FLU",          Digraph.AllowGlitchFlu .ToEnabledDisabled()),
                    new Tuple<string, string>("Allow Glitch GPW",          Digraph.AllowGlitchGpw .ToEnabledDisabled()),
                    new Tuple<string, string>("Ignore Single-Use Edges",   Digraph.IgnoreOnceEdges.ToEnabledDisabled()),
                    new Tuple<string, string>("", ""),  // Skip a row.
                    new Tuple<string, string>("Shuffle Preset",            presetName),
                    new Tuple<string, string>("", ""),  // Skip a row.
                }
                : new List<Tuple<string, string>>()
                {
                    new Tuple<string, string>("Use Rando Exclusion Rules", UseRandoRules          .ToEnabledDisabled()),
                    new Tuple<string, string>("Verify Reachability",       VerifyReachability     .ToEnabledDisabled()),
                    new Tuple<string, string>("Goal Is Kreia",             Digraph.GoalIsMalak    .ToEnabledDisabled()),
                    new Tuple<string, string>("Goal Is Jedi Masters",      Digraph.GoalIsStarMap  .ToEnabledDisabled()),
                    new Tuple<string, string>("Goal Is Full Party",        Digraph.GoalIsFullParty.ToEnabledDisabled()),
                    new Tuple<string, string>("Goal Is Pazaak",            Digraph.GoalIsPazaak   .ToEnabledDisabled()),
                    new Tuple<string, string>("Allow Glitch Clipping",     Digraph.AllowGlitchClip.ToEnabledDisabled()),
                    new Tuple<string, string>("Allow Glitch DLZ",          Digraph.AllowGlitchDlz .ToEnabledDisabled()),
                    new Tuple<string, string>("Allow Glitch FLU",          Digraph.AllowGlitchFlu .ToEnabledDisabled()),
                    new Tuple<string, string>("Allow Glitch GPW",          Digraph.AllowGlitchGpw .ToEnabledDisabled()),
                    new Tuple<string, string>("Ignore Single-Use Edges",   Digraph.IgnoreOnceEdges.ToEnabledDisabled()),
                    new Tuple<string, string>("", ""),  // Skip a row.
                    new Tuple<string, string>("Shuffle Preset",            presetName),
                    new Tuple<string, string>("", ""),  // Skip a row.
                };

            foreach (var setting in settings)
            {
                ws.Cell(i, 1).Value = setting.Item1;
                ws.Cell(i, 2).Value = setting.Item2;
                ws.Cell(i, 1).Style.Font.Italic = true;
                ws.Cell(i, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                i++;
            }

            // Custom Omitted Modules
            var omittedModules = Digraph.Modules.Where(m => OmittedModules.Contains(m.WarpCode)).OrderBy(m => m.WarpCode);

            if (isCustomPreset)
            {
                // List the omitted modules if the omitted modules have been customized.
                var iMax = i;
                i = 1;  // Restart at the top of the settings list.

                ws.Cell(i, 4).Value = "Omitted Modules";
                ws.Cell(i, 4).Style.Font.Bold = true;
                ws.Range(i, 4, i, 5).Merge().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                i++;

                ws.Cell(i, 4).Value = "Warp Code";
                ws.Cell(i, 5).Value = "Common Name";
                ws.Cell(i, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Cell(i, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
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

                ws.Cell(i, 1).Value = omitted ? "OMITTED" : changed.ToString().ToUpper();
                ws.Cell(i, 1).DataType = XLDataType.Text;
                ws.Cell(i, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
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
        /// <param name="rando">Kotor1Randomizer object that contains settings to use.</param>
        public static void Module_rando(KPaths paths, Models.RandomizerBase rando = null)
        {
            // Prepare for a new randomization.
            Reset(rando);
            AssignSettings(rando);

            // Split the Bound modules into their respective lists.
            var reachable = false;
            var iterations = 0;

            // Only shuffle if there is more than 1 module in the shuffle.
            if (RandomizedModules.Count > 1)
            {
                if (UseRandoRules || VerifyReachability)
                {
                    var sw = new System.Diagnostics.Stopwatch();
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
            if (rando.Game == Models.Game.Kotor1)
                UnlockK1Doors(paths);
            else
                UnlockK2Doors(paths);

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

        private static void AssignSettings(Models.RandomizerBase rando)
        {
            if (rando == null)
            {
                GameRandomized     = Models.Game.Kotor1;
                UseRandoRules      = Properties.Settings.Default.UseRandoRules;
                VerifyReachability = Properties.Settings.Default.VerifyReachability;
                ModuleExtrasValue  = Properties.Settings.Default.ModuleExtrasValue;
                RandomizedModules  = Globals.BoundModules.Where(x => !x.Omitted).Select(x => x.Code).ToList();
                OmittedModules     = Globals.BoundModules.Where(x =>  x.Omitted).Select(x => x.Code).ToList();
                if (Properties.Settings.Default.LastPresetComboIndex >= 0)
                    ShufflePreset  = Globals.K1_MODULE_OMIT_PRESETS.Keys.ToList()[Properties.Settings.Default.LastPresetComboIndex];
                else
                    ShufflePreset  = "";
            }
            else
            {
                var moduleRando = rando as Models.IRandomizeModules;
                var genSettings = rando as Models.IGeneralSettings;

                GameRandomized     = rando.Game;
                UseRandoRules      = moduleRando.ModuleLogicRandoRules;
                VerifyReachability = moduleRando.ModuleLogicReachability;
                ModuleExtrasValue  = genSettings.GeneralModuleExtrasValue;
                foreach (var door in genSettings.GeneralUnlockedDoors)
                    ModuleExtrasValue |= door.Tag;
                RandomizedModules  = moduleRando.ModuleRandomizedList.Select(x => x.WarpCode).ToList();
                OmittedModules     = moduleRando.ModuleOmittedList.Select(x => x.WarpCode).ToList();
                ShufflePreset      = moduleRando.ModuleShufflePreset;
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
        internal static void Reset(Models.RandomizerBase rando = null)
        {
            // Reset digraph reachability settings.
            var modulesPath = GameRandomized == Models.Game.Kotor1
                ? Path.Combine(Environment.CurrentDirectory, "Xml", "KotorModules.xml")
                : Path.Combine(Environment.CurrentDirectory, "Xml", "Kotor2Modules.xml");
            Digraph.ResetSettings(rando, modulesPath);
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
        /// Unlock Leviathan Hangar option in the other two elevator access.
        /// </summary>
        /// <param name="paths">KPaths object for this game.</param>
        private static void FixLeviathanHangarAccess(KPaths paths)
        {
            var lev_files_a = paths.FilesInModules.Where(fi => fi.Name.Contains(LookupTable[AREA_LEV_PRISON]));
            var lev_files_b = paths.FilesInModules.Where(fi => fi.Name.Contains(LookupTable[AREA_LEV_COMMAND]));

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
        }

        /// <summary>
        /// Enables the use of the Leviathan Hangar elevator.
        /// </summary>
        /// <param name="paths">KPaths object for this game.</param>
        private static void FixLeviathanHangarElevator(KPaths paths)
        {
            var lev_files_c = paths.FilesInModules.Where(fi => fi.Name.Contains(LookupTable[AREA_LEV_HANGAR]));

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
                var rim = new RIM(kvp.Value.FullName);
                var rfile = rim.File_Table.FirstOrDefault(x => x.TypeID == (int)ResourceType.IFO);
                if (rfile == null) throw new Exception($"IFO file not found in rim '{kvp.Key}'.");

                var gff = new GFF(rfile.File_Data);
                var fields = gff.Top_Level.Fields;
                var ignoreCase = StringComparison.CurrentCultureIgnoreCase;
                var invalidType = "Field '{0}' is not of type FLOAT in the IFO of " + $"'{kvp.Key}'.";
                var fieldMissing = "Field '{0}' is missing from IFO of " + $"'{kvp.Key}'.";

                // Update x coordinate data.
                if (fields.FirstOrDefault(x => x.Label.Equals(Properties.Resources.ModuleEntryX, ignoreCase)) is GFF.FIELD xfield)
                {
                    if (xfield is GFF.FLOAT xval) xval.Value = Globals.FIXED_COORDINATES[kvp.Key].Item1;
                    else throw new Exception(string.Format(invalidType, Properties.Resources.ModuleEntryX));
                }
                else
                {
                    throw new Exception(string.Format(fieldMissing, Properties.Resources.ModuleEntryX));
                }

                // Update y coordinate data.
                if (fields.FirstOrDefault(x => x.Label.Equals(Properties.Resources.ModuleEntryY, ignoreCase)) is GFF.FIELD yfield)
                {
                    if (yfield is GFF.FLOAT yval) yval.Value = Globals.FIXED_COORDINATES[kvp.Key].Item2;
                    else throw new Exception(string.Format(invalidType, Properties.Resources.ModuleEntryY));
                }
                else
                {
                    throw new Exception(string.Format(fieldMissing, Properties.Resources.ModuleEntryY));
                }

                // Update z coordinate data.
                if (fields.FirstOrDefault(x => x.Label.Equals(Properties.Resources.ModuleEntryZ, ignoreCase)) is GFF.FIELD zfield)
                {
                    if (zfield is GFF.FLOAT zval) zval.Value = Globals.FIXED_COORDINATES[kvp.Key].Item3;
                    else throw new Exception(string.Format(invalidType, Properties.Resources.ModuleEntryZ));
                }
                else
                {
                    throw new Exception(string.Format(fieldMissing, Properties.Resources.ModuleEntryZ));
                }

                // Write updated data to RIM file.
                rfile.File_Data = gff.ToRawData();
                rim.WriteToFile(kvp.Value.FullName);
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
        private static void UnlockK1Doors(KPaths paths)
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
                FixLeviathanHangarAccess(paths);
            }
            if (extrasValue.HasFlag(ModuleExtras.EnableLevHangarElev))
            {
                FixLeviathanHangarElevator(paths);
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
        /// Unlock the doors requested by the user.
        /// </summary>
        /// <param name="paths">KPaths object for this game.</param>
        private static void UnlockK2Doors(KPaths paths)
        {
            // In the future these'll be split into options, but for now here's all of them
            UnlockDoorInFile(paths, AREA_K2_PER_ADMIN,     LABEL_K2_101PERTODORMS);
            UnlockDoorInFile(paths, AREA_K2_PER_ADMIN,     LABEL_K2_101PERTOMININGTUNNELS);
            UnlockDoorInFile(paths, AREA_K2_PER_ADMIN,     LABEL_K2_101PERTOFUELDEPOT);
            UnlockDoorInFile(paths, AREA_K2_PER_ADMIN,     LABEL_K2_101PERTOHARBINGER);
            UnlockDoorInFile(paths, AREA_K2_PER_FUEL,      LABEL_K2_103PERTOMININGTUNNELS);
            UnlockDoorInFile(paths, AREA_K2_PER_FUEL,      LABEL_K2_103PERFORCESHIELDS);
            UnlockDoorInFile(paths, AREA_K2_PER_FUEL,      LABEL_K2_103PERSHIELD2);
            UnlockDoorInFile(paths, AREA_K2_PER_DORMS,     LABEL_K2_105PERTOASTROID);
            UnlockDoorInFile(paths, AREA_K2_PER_HANGER,    LABEL_K2_106PEREASTDOOR);
            UnlockDoorInFile(paths, AREA_K2_TEL_RES,       LABEL_K2_203TELAPPTDOOR);
            UnlockDoorInFile(paths, AREA_K2_TEL_RES,       LABEL_K2_203TELEXCHANGE);
            UnlockDoorInFile(paths, AREA_K2_TEL_ENTER_WAR, LABEL_K2_222TELRAVAGER);
            UnlockDoorInFile(paths, AREA_K2_TEL_ACAD,      LABEL_K2_262TELPLATEAU);
            UnlockDoorInFile(paths, AREA_K2_NAR_DOCKS,     LABEL_K2_303NARZEZDOOR);
            UnlockDoorInFile(paths, AREA_K2_NAR_JEKK,      LABEL_K2_304NARBACKROOM);
            UnlockDoorInFile(paths, AREA_K2_NAR_J_TUNNELS, LABEL_K2_305NARTOJEKKJEKK);
            UnlockDoorInFile(paths, AREA_K2_NAR_G0T0,      LABEL_K2_351NARG0T0EBONHAWK);
            UnlockDoorInFile(paths, AREA_K2_DAN_COURTYARD, LABEL_K2_605DANREBUILTENCLAVE);
            UnlockDoorInFile(paths, AREA_K2_KOR_ACAD,      LABEL_K2_702KORVALLEY);
            UnlockDoorInFile(paths, AREA_K2_KOR_SHY,       LABEL_K2_710KORLUDOKRESSH);

            // Enable tranistions for these doors with linking modules but no flags
            EnableDoorTransition(paths, AREA_K2_PER_FUEL,  LABEL_K2_103PERTOMININGTUNNELS);
            EnableDoorTransition(paths, AREA_K2_PER_DORMS, LABEL_K2_105PERTOASTROID, AREA_K2_PER_ASTROID);
            EnableDoorTransition(paths, AREA_K2_TEL_ACAD,  LABEL_K2_262TELPLATEAU);

            // Add a transition to the Astroid Exterior
            Add104PERTransition(paths);

            // Add elevator to 901MAL
            Add901MALEbonElevator(paths);
        }

        /// <summary>
        /// Allows a door to transition to its listed module
        /// </summary>
        /// <param name="paths">KPaths object for this game.</param>
        /// <param name="area">Name of the SRim file to modify.</param>
        /// <param name="label">Label of the door to unlock.</param>
        /// <param name="destination">The module this will transition to, if null then leave field unchanged.</param>
        private static void EnableDoorTransition(KPaths paths, string area, string label, string destination = null)
        {
            var areaFiles = paths.FilesInModules.Where(fi => fi.Name.Contains(LookupTable[area]));
            foreach (var fi in areaFiles)
            {
                // Skip any files that aren't the default format.
                if (fi.Name.Length > 10) { continue; }

                var r = new RIM(fi.FullName);   // Open what replaced this area.
                var rf = r.File_Table.FirstOrDefault(x => x.TypeID == (int)ResourceType.GIT);
                var g = new GFF(rf.File_Data);  // Grab the git out of the file.

                // Get ready for the nastiest Linq query you've ever seen, we may want to clean this up some
                var fields = (g.Top_Level.Fields.FirstOrDefault(x => x.Label == "Door List") as GFF.LIST).Structs   // from the door list struct
                    .FirstOrDefault(y => (y.Fields.FirstOrDefault(z => z.Label == "TemplateResRef") as GFF.ResRef).Reference == label).Fields;  // grab the fields for this door
                (fields.FirstOrDefault(a => a.Label == "LinkedToFlags") as GFF.BYTE).Value = 2; // set LinkedToFlags to 2.

                // Change the destination module if provided.
                if (destination != null)
                    (fields.FirstOrDefault(a => a.Label == "LinkedToModule") as GFF.ResRef).Reference = destination;

                // Write change(s) to file.
                rf.File_Data = g.ToRawData();
                r.WriteToFile(fi.FullName);
            }
        }

        private static void Add104PERTransition(KPaths paths)
        {
            var filename = LookupTable[AREA_K2_PER_ASTROID] + ".rim";
            var fi = paths.FilesInModules.FirstOrDefault(f => f.Name == filename);
            if (fi.Exists)
            {
                var r = new RIM(fi.FullName);   // Open what replaced this the astroid exterior.
                var rf = r.File_Table.FirstOrDefault(x => x.TypeID == (int)ResourceType.GIT);
                var g = new GFF(rf.File_Data);  // Grab the git out of the file.

                //Create Tranistion Struct
                var TransitionStruct = new GFF.STRUCT("", 1, new List<GFF.FIELD>()
                {
                    new GFF.LIST("Geometry", new List<GFF.STRUCT>()
                    {
                        new GFF.STRUCT("", 3, new List<GFF.FIELD>()
                        {
                            new GFF.FLOAT("PointX", 0.0f),
                            new GFF.FLOAT("PointY", 0.0f),
                            new GFF.FLOAT("PointZ", 0.0f)
                        }),
                        new GFF.STRUCT("", 3, new List<GFF.FIELD>()
                        {
                            new GFF.FLOAT("PointX", 0.0f),
                            new GFF.FLOAT("PointY", -1.4f),
                            new GFF.FLOAT("PointZ", 2.0f)
                        }),
                        new GFF.STRUCT("", 3, new List<GFF.FIELD>()
                        {
                            new GFF.FLOAT("PointX", 6.0f),
                            new GFF.FLOAT("PointY", -1.4f),
                            new GFF.FLOAT("PointZ", 2.0f)
                        }),
                        new GFF.STRUCT("", 3, new List<GFF.FIELD>()
                        {
                            new GFF.FLOAT("PointX", 6.0f),
                            new GFF.FLOAT("PointY", 0.0f),
                            new GFF.FLOAT("PointZ", 0.0f)
                        })
                    }),
                    new GFF.CExoString("LinkedTo", "From_104PER"),
                    new GFF.BYTE("LinkedToFlags", 2),
                    new GFF.ResRef("LinkedToModule", AREA_K2_PER_FUEL),
                    new GFF.CExoString("Tag", "To_103PER"),
                    new GFF.ResRef("TemplateResRef", "newtransition"),
                    new GFF.CExoLocString("TransitionDestin", 75950, new List<GFF.SubString>()),
                    new GFF.FLOAT("XOrientation", 0.0f),
                    new GFF.FLOAT("YOrientation", 0.0f),
                    new GFF.FLOAT("ZOrientation", 0.0f),
                    new GFF.FLOAT("XPosition", 70.6f),
                    new GFF.FLOAT("YPosition", -115.1f),
                    new GFF.FLOAT("ZPosition", 255.0f)
                });

                (g.Top_Level.Fields.FirstOrDefault(f => f.Label == "TriggerList") as GFF.LIST).Structs.Add(TransitionStruct);

                // Write change(s) to file.
                rf.File_Data = g.ToRawData();
                r.WriteToFile(fi.FullName);
            }
        }

        private static void Add901MALEbonElevator(KPaths paths)
        {
            var filename = LookupTable[AREA_K2_MAL_SURFACE] + ".rim";
            var fi = paths.FilesInModules.FirstOrDefault(f => f.Name == filename);
            if (fi.Exists)
            {
                var r = new RIM(fi.FullName);   // Open what replaced this the astroid exterior.
                var rf = r.File_Table.FirstOrDefault(x => x.TypeID == (int)ResourceType.GIT);
                var g = new GFF(rf.File_Data);  // Grab the git out of the file.

                //Create Tranistion Struct
                var PlaceStruct = new GFF.STRUCT("", 9, new List<GFF.FIELD>()
                {
                    new GFF.FLOAT("Bearing", 0.0f),
                    new GFF.ResRef("TemplateResRef", "ebo_elev"),
                    new GFF.DWORD("TweakColor",0),
                    new GFF.BYTE("UseTweakColor",0),
                    new GFF.FLOAT("X", 6.23f),
                    new GFF.FLOAT("Y", -24.63f),
                    new GFF.FLOAT("Z", 84.43f)
                });
                (g.Top_Level.Fields.FirstOrDefault(f => f.Label == "Placeable List") as GFF.LIST).Structs.Add(PlaceStruct);

                //Add Placeable and script to overide
                File.WriteAllBytes(paths.Override + "ebo_elev.utp", Properties.Resources.ebo_elev);
                File.WriteAllBytes(paths.Override + "r_to003EBO.ncs", Properties.Resources.r_to003EBO);

                // Write change(s) to file.
                rf.File_Data = g.ToRawData();
                r.WriteToFile(fi.FullName);
            }
        }

        /// <summary>
        /// Copy backup module files to the modules directory based on the current shuffle.
        /// </summary>
        /// <param name="paths">KPaths object for this game.</param>
        private static void WriteFilesToModulesDirectory(KPaths paths)
        {
            var tasks = new List<Task>();

            // Copy shuffled modules into the base directory.
            foreach (var name in LookupTable)
            {
                tasks.Add(Task.Run(() => File.Copy($"{paths.modules_backup}{name.Key}.rim", $"{paths.modules}{name.Value}.rim", true)));
                tasks.Add(Task.Run(() => File.Copy($"{paths.modules_backup}{name.Key}_s.rim", $"{paths.modules}{name.Value}_s.rim", true)));
                if (File.Exists($"{paths.lips_backup}{name.Key}_loc.mod"))
                    tasks.Add(Task.Run(() => File.Copy($"{paths.lips_backup}{name.Key}_loc.mod", $"{paths.lips}{name.Value}_loc.mod", true)));
                if (File.Exists($"{paths.modules_backup}{name.Key}_dlg.erf"))
                    tasks.Add(Task.Run(() => File.Copy($"{paths.modules_backup}{name.Key}_dlg.erf", $"{paths.modules}{name.Value}_dlg.erf", true)));
            }

            // Copy lips extras into the base directory.
            foreach (var name in Globals.lipXtras)
            {
                if (File.Exists($"{paths.lips_backup}{name}"))
                    tasks.Add(Task.Run(() => File.Copy($"{paths.lips_backup}{name}", $"{paths.lips}{name}", true)));
            }

            Task.WhenAll(tasks).Wait();
        }

        /// <summary>
        /// Write special files to the override folder - save data, dream fix, galaxy map unlock, etc.
        /// </summary>
        /// <param name="paths">KPaths object for this game.</param>
        private static void WriteOverrideFiles(KPaths paths)
        {
            var moduleSavePath = Path.Combine(paths.Override, TwoDA_MODULE_SAVE);
            var saveFileExtras = ModuleExtrasValue & (ModuleExtras.SaveAllModules | ModuleExtras.SaveMiniGames | ModuleExtras.NoSaveDelete);
            var tasks = new List<Task>();

            // Save Data File
            switch ((int)saveFileExtras)
            {
                default:
                    // 0b000 - Milestone Delete (Default)
                    // Do nothing.
                    break;

                case (int)(ModuleExtras.NoSaveDelete):
                    // 0b001 - No Milestone Delete
                    tasks.Add(Task.Run(() => File.WriteAllBytes(moduleSavePath, Properties.Resources.NODELETE_modulesave)));
                    break;

                case (int)(ModuleExtras.SaveMiniGames):
                    // 0b010 - Save Minigames | Milestone Delete
                    tasks.Add(Task.Run(() => File.WriteAllBytes(moduleSavePath, Properties.Resources.MGINCLUDED_modulesave)));
                    break;

                case (int)(ModuleExtras.NoSaveDelete | ModuleExtras.SaveMiniGames):
                    // 0b011 - Save Minigames | No Milestone Delete
                    tasks.Add(Task.Run(() => File.WriteAllBytes(moduleSavePath, Properties.Resources.NODELETE_MGINCLUDED_modulesave)));
                    break;

                case (int)(ModuleExtras.SaveAllModules):
                case (int)(ModuleExtras.SaveMiniGames | ModuleExtras.SaveAllModules):
                    // Treat both the same.
                    // 0b100 - Save All Modules | Milestone Delete
                    // 0b110 - Save All Modules | Save Minigames | Milestone Delete
                    tasks.Add(Task.Run(() => File.WriteAllBytes(moduleSavePath, Properties.Resources.ALLINCLUDED_modulesave)));
                    break;

                case (int)(ModuleExtras.NoSaveDelete | ModuleExtras.SaveAllModules):
                case (int)(ModuleExtras.NoSaveDelete | ModuleExtras.SaveMiniGames | ModuleExtras.SaveAllModules):
                    // Treat both the same.
                    // 0b101 - Save All Modules | No Milestone Delete
                    // 0b111 - Save All Modules | Save Minigames | No Milestone Delete
                    tasks.Add(Task.Run(() => File.WriteAllBytes(moduleSavePath, Properties.Resources.NODELETE_ALLINCLUDED_modulesave)));
                    break;
            }

            // Fix Dream File
            if (ModuleExtrasValue.HasFlag(ModuleExtras.FixDream))
            {
                tasks.Add(Task.Run(() => File.WriteAllBytes(Path.Combine(paths.Override, FIXED_DREAM_OVERRIDE), Properties.Resources.k_ren_visionland)));
            }

            // Fix Fighter Encounter
            if (ModuleExtrasValue.HasFlag(ModuleExtras.FixFighterEncounter))
            {
                tasks.Add(Task.Run(() => File.WriteAllBytes(Path.Combine(paths.Override, FIXED_FIGHTER_OVERRIDE), Properties.Resources.k_pebo_mgheart)));
            }

            // Unlock Galaxy Map File
            if (ModuleExtrasValue.HasFlag(ModuleExtras.UnlockGalaxyMap))
            {
                tasks.Add(Task.Run(() => File.WriteAllBytes(Path.Combine(paths.Override, UNLOCK_MAP_OVERRIDE), Properties.Resources.k_pebn_galaxy)));
            }

            // Keep Korriban Doors Unlocked
            if (ModuleExtrasValue.HasFlag(ModuleExtras.UnlockKorValley))
            {
                tasks.Add(Task.Run(() => File.WriteAllBytes(Path.Combine(paths.Override, KOR_OPEN_ACADEMY), Properties.Resources.k33b_openacademy)));
                tasks.Add(Task.Run(() => File.WriteAllBytes(Path.Combine(paths.Override, KOR_VALLEY_ENTER), Properties.Resources.k36_pkor_enter)));
            }

            /// KotOR 2 Settings
            // Save Patch
            if (ModuleExtrasValue.HasFlag(ModuleExtras.K2SavePatch))
                tasks.Add(Task.Run(() => File.WriteAllBytes(Path.Combine(paths.Override, PATCH_K2_MODULESAVE), Properties.Resources.modulesave)));

            // Unlock Galaxy Map
            if (ModuleExtrasValue.HasFlag(ModuleExtras.K2GalaxyMap))
                tasks.Add(Task.Run(() => File.WriteAllBytes(Path.Combine(paths.Override, PATCH_K2_GALAXYMAP), Properties.Resources.a_galaxymap)));

            // Patch Disciple Crash
            if (ModuleExtrasValue.HasFlag(ModuleExtras.K2DisciplePatch))
                tasks.Add(Task.Run(() => File.WriteAllBytes(Path.Combine(paths.Override, PATCH_K2_DISC_JOIN), Properties.Resources.a_disc_join)));

            Task.WhenAll(tasks).Wait();
        }

        #endregion Methods
    }
}