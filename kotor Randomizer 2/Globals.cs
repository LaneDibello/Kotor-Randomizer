using kotor_Randomizer_2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace kotor_Randomizer_2
{
    // Don't rememeber why I made this Serializable but I'm too afraid to remove it lmao
    [Serializable]
    public enum RandomizationLevel // Thank you Glasnonck
    {
        /// <summary> No randomization. </summary>
        None = 0,
        /// <summary> Randomize similar types within the same category. </summary>
        Subtype = 1,
        /// <summary> Randomize within the same category. </summary>
        Type = 2,
        /// <summary> Randomize with everything else set to Max. </summary>
        Max = 3,
    }

    [Flags]
    [Serializable]
    public enum RandomizationCategory
    {
        /// <summary> Uninitialized value - nothing will be done. </summary>
        None    = 0x00, // 0b00000000
        /// <summary> Randomize module (RIM) files so loading zones lead to an unknown destination. </summary>
        Module  = 0x01, // 0b00000001
        /// <summary> Randomize items. </summary>
        Item    = 0x02, // 0b00000010
        /// <summary> Randomize music and sound files. </summary>
        Sound   = 0x04, // 0b00000100
        /// <summary> Randomize models. </summary>
        Model   = 0x08, // 0b00001000
        /// <summary> Randomize texture maps. </summary>
        Texture = 0x10, // 0b00010000
        /// <summary> Randomize 2DA tables. </summary>
        TwoDA   = 0x20, // 0b00100000
        /// <summary> Randomize text. -- Not yet implemented -- </summary>
        Text    = 0x40, // 0b01000000
        /// <summary> Perform other types of randomization. </summary>
        Other   = 0x80, // 0b10000000
    }

    [Flags]
    [Serializable]
    public enum ModuleExtras
    {
        /// <summary> (Default Behavior) Delete milestone save data. </summary>
        Default             = 0x00000, // 0b0000 00000000 00000000
        /// <summary> Do not delete milestone save data. </summary>
        NoSaveDelete        = 0x00001, // 0b0000 00000000 00000001
        /// <summary> Include minigame data in the save file. </summary>
        SaveMiniGames       = 0x00002, // 0b0000 00000000 00000010
        /// <summary> Include all module data in the save file. </summary>
        SaveAllModules      = 0x00004, // 0b0000 00000000 00000100
        /// <summary> Fix dream cutscenes. </summary>
        FixDream            = 0x00008, // 0b0000 00000000 00001000
        /// <summary> Unlock all destinations on the galaxy map. </summary>
        UnlockGalaxyMap     = 0x00010, // 0b0000 00000000 00010000
        /// <summary> Fix warp spawn coordinates in certain modules. </summary>
        FixCoordinates      = 0x00020, // 0b0000 00000000 00100000
        /// <summary> Fix Rakatan mind prison to prevent soft-locks. </summary>
        FixMindPrison       = 0x00040, // 0b0000 00000000 01000000
        /// <summary> Unlock ancient doors to Dantooine ruins, and on the Lehon Temple roof. </summary>
        UnlockDanRuins      = 0x00080, // 0b0000 00000000 10000000
        /// <summary> Allows Leviathan elevators to go to any of the other decks without prerequisites. </summary>
        UnlockLevElev       = 0x00100, // 0b0000 00000001 00000000
        /// <summary> Adds a Load Zone leading the the Vulkar Spice in the rear of the Vulkar base main floor, next to the pool. </summary>
        VulkarSpiceLZ       = 0x00200, // 0b0000 00000010 00000000
        /// <summary> Unlock the Republic Embassy door and the door to the submersible in Manaan's republic embassy. </summary>
        UnlockManEmbassy    = 0x00400, // 0b0000 00000100 00000000
        /// <summary> Keep the door to Bastila on the Command Center (Deck 3) of the Star Forge unlocked, even after fighting her. </summary>
        UnlockStaBastila    = 0x00800, // 0b0000 00001000 00000000
        /// <summary> Unlock the door that exits from the Temple Summit on the Unknown World. </summary>
        UnlockUnkSummit     = 0x01000, // 0b0000 00010000 00000000
        /// <summary> Ensure Sith Tomb and Sith Academy stay unlocked regardless of Uthar/Yuthura outcome. </summary>
        UnlockKorValley     = 0x02000, // 0b0000 00100000 00000000
        /// <summary> Unlock the Sith Hangar door on Manaan. </summary>
        UnlockManHangar     = 0x04000, // 0b0000 01000000 00000000
        /// <summary> Unlock Undercity door in the Lower City. </summary>
        UnlockTarUndercity  = 0x08000, // 0b0000 10000000 00000000
        /// <summary> Unlock Vulkar Base door in the Lower City. </summary>
        UnlockTarVulkar     = 0x10000, // 0b0001 00000000 00000000
        /// <summary> Unlock the Lehon Temple exit on the Main Floor. </summary>
        UnlockUnkTempleExit = 0x20000, // 0b0010 00000000 00000000
        /// <summary> Speeds up the envirosuit to match normal run speed. </summary>
        FastEnvirosuit      = 0x40000, // 0b0100 00000000 00000000
        /// <summary> Allows T3M4 to be purchased before winning the Taris Swoop Race and speaking with Canderous. </summary>
        EarlyT3             = 0x80000, // 0b1000 00000000 00000000
    }

    [Flags]
    [Serializable]
    public enum TextSettings
    {
        /// <summary> (Default Behavior) No Randomization</summary>
        Default               = 0x00, // 0b00000000
        /// <summary> Randomizes the Text in conversation entries (The words spoken by NPCs) </summary>
        RandoDialogEntries    = 0x01, // 0b00000001
        /// <summary> Randomizes the Text in conversation replies (The options the MC may choose from) </summary>
        RandoDialogReplies    = 0x02, // 0b00000010
        /// <summary> Uses the TLK file to match Entry text with it's corresponding sound (This is about 90% effective as the included TLK file isn't entirely accurate) </summary>
        MatchEntrySoundsWText = 0x04, // 0b00000100
        /// <summary> This randomizes all the strings in the TLK file, which randomizes all the remaining in game text not in conversations. (Note: This TLK can still be used to match sounds) </summary>
        RandoFullTLK          = 0x08, // 0b00001000
        /// <summary> Attempts to match strings in TLK rando with ones of similar length (This will make some GUIs more pleasing to the eye)</summary>
        MatchSimLengthStrings = 0x10, // 0b00010000
    }

    [Serializable]
    public enum TexturePack
    {
        /// <summary> High Quality </summary>
        [Description("High Quality")]
        HighQuality = 0,
        /// <summary> Medium Quality </summary>
        [Description("Medium Quality")]
        MedQuality = 1,
        /// <summary> Low Quality </summary>
        [Description("Low Quality")]
        LowQuality = 2,
    }

    public class Globals
    {
        #region Constants
        public const string AREA_UNDERCITY = "tar_m04aa";
        public const string AREA_TOMB_TULAK = "korr_m38ab";
        public const string AREA_LEVI_HANGAR = "lev_m40ac";
        public const string AREA_AHTO_WEST = "manm26aa";
        public const string AREA_MANAAN_SITH = "manm27aa";
        public const string AREA_RAKA_SETTLE = "unk_m43aa";
        public const string AREA_TEMPLE_MAIN = "unk_m44aa";

        /// <summary>
        /// New coordinates for bad randomizer spawn locations.
        /// </summary>
        public static readonly Dictionary<string, Tuple<float, float, float>> FIXED_COORDINATES = new Dictionary<string, Tuple<float, float, float>>()
        {
            { AREA_UNDERCITY, new Tuple<float, float, float>(
                (183.5f),
                (167.4f),
                (1.5f)) },
            { AREA_TOMB_TULAK, new Tuple<float, float, float>(
                (15.8f),
                (55.6f),
                (0.75f)) },
            { AREA_LEVI_HANGAR, new Tuple<float, float, float>(
                (12.5f),
                (155.2f),
                (3.0f)) },
            { AREA_AHTO_WEST, new Tuple<float, float, float>(
                (5.7f),
                (-10.7f),
                (59.2f)) },
            { AREA_MANAAN_SITH, new Tuple<float, float, float>(
                (112.8f),
                (2.4f),
                (0f)) },
            { AREA_RAKA_SETTLE, new Tuple<float, float, float>(
                (202.2f),
                (31.5f),
                (40.7f)) },
            { AREA_TEMPLE_MAIN, new Tuple<float, float, float>(
                (95.3f),
                (42.0f),
                (0.44f)) },
        };

        /// <summary>
        /// Large Creature Models
        /// </summary>
        public static readonly List<int> LARGE_CHARS = new List<int>() { 354, 334, 87, 80, 77, 81, 54 };

        /// <summary>
        /// Broken Creature Models
        /// </summary>
        public static readonly List<int> BROKEN_CHARS = new List<int>() { 0, 29, 82 };

        /// <summary>
        /// Large Placeable Models
        /// </summary>
        public static readonly List<int> LARGE_PLACE = new List<int>() { 1, 2, 55, 56, 57, 58, 65, 66, 110, 111, 142, 172, 176, 194, 195, 217, 218, 226 }; // NEED TO RESEARCH

        /// <summary>
        /// Broken Placeable Models
        /// </summary>
        public static readonly List<int> BROKEN_PLACE = new List<int>() { 0, 8, 9, 47, 54, 62, 78, 84, 90, 94, 96, 97, 115, 121, 158, 167, 159, 219 };

        /// <summary>
        /// Valid Floor Panel Placeables
        /// </summary>
        public static readonly List<int> PANEL_PLACE = new List<int>() { 14, 15, 29, 30, 31, 36, 45, 48, 49, 53, 68, 69, 79, 108, 109, 114, 130, 131, 141, 143, 145, 148, 152, 154, 157, 193, 227, 228, 229, 230, 231 };

        /// <summary>
        /// Extra Files found in the 'lips' directory
        /// </summary>
        public static readonly List<string> lipXtras = new List<string>() { "global.mod", "legal.mod", "localization.mod", "mainmenu.mod", "miniglobal.mod", "subglobal.mod", };

        /// <summary>
        /// Characters that the letter-combo probability files can handle.
        /// </summary>
        public static readonly string NAMEGEN_CHARS = "qwertyuiopasdfghjklzxcvbnm-'";

        /// <summary>
        /// All items in the game
        /// </summary>
        public static readonly List<string> ITEMS = new List<string>() {
            "g1_a_class5001", "g1_a_class5002", "g1_a_class6001", "g1_a_class8001",         // 
            "g1_i_belt001", "g1_i_drdcomspk01", "g1_i_drdhvplat01", "g1_i_drdshld001",      // 
            "g1_i_drdutldev01", "g1_i_drdutldev02", "g1_i_drdutldev03", "g1_i_gauntlet01",  // 
            "g1_i_implant301", "g1_i_implant302", "g1_i_implant303", "g1_i_implant304",
            "g1_i_mask01", "g1_i_mask02", "g1_i_mask03", "g1_w_dblsbr001",
            "g1_w_dblsbr002", "g1_w_dsrptrfl001", "g1_w_hvrptbltr01", "g1_w_ionrfl01",
            "g1_w_lghtsbr01", "g1_w_lghtsbr02", "g1_w_rptnblstr01", "g1_w_sbrcrstl20",
            "g1_w_sbrcrstl21", "g1_w_shortsbr01", "g1_w_shortsbr02", "g1_w_vbroswrd01",
            "g_a_class4001", "g_a_class4002", "g_a_class4003", "g_a_class4004",
            "g_a_class4005", "g_a_class4006", "g_a_class4007", "g_a_class4008",
            "g_a_class4009", "g_a_class5001", "g_a_class5002", "g_a_class5003",
            "g_a_class5004", "g_a_class5005", "g_a_class5006", "g_a_class5007",
            "g_a_class5008", "g_a_class5009", "g_a_class5010", "g_a_class6001",
            "g_a_class6002", "g_a_class6003", "g_a_class6004", "g_a_class6005",
            "g_a_class6006", "g_a_class6007", "g_a_class6008", "g_a_class6009",
            "g_a_class7001", "g_a_class7002", "g_a_class7003", "g_a_class7004",
            "g_a_class7005", "g_a_class7006", "g_a_class8001", "g_a_class8002",
            "g_a_class8003", "g_a_class8004", "g_a_class8005", "g_a_class8006",
            "g_a_class8007", "g_a_class8009", "g_a_class9001", "g_a_class9002",
            "g_a_class9003", "g_a_class9004", "g_a_class9005", "g_a_class9006",
            "g_a_class9007", "g_a_class9009", "g_a_class9010", "g_a_class9011",
            "g_a_clothes01", "g_a_clothes02", "g_a_clothes03", "g_a_clothes04",
            "g_a_clothes05", "g_a_clothes06", "g_a_clothes07", "g_a_clothes08",
            "g_a_clothes09", "g_a_jedirobe01", "g_a_jedirobe02", "g_a_jedirobe03",
            "g_a_jedirobe04", "g_a_jedirobe05", "g_a_jedirobe06", "g_a_kghtrobe01",
            "g_a_kghtrobe02", "g_a_kghtrobe03", "g_a_kghtrobe04", "g_a_kghtrobe05",
            "g_a_mstrrobe01", "g_a_mstrrobe02", "g_a_mstrrobe03", "g_a_mstrrobe04",
            "g_a_mstrrobe05", "g_a_mstrrobe06", "g_a_mstrrobe07", "g_i_adrnaline001",
            "g_i_adrnaline002", "g_i_adrnaline003", "g_i_adrnaline004", "g_i_adrnaline005",
            "g_i_adrnaline006", "g_i_asthitem001", "g_i_belt001", "g_i_belt002",
            "g_i_belt003", "g_i_belt004", "g_i_belt005", "g_i_belt006",
            "g_i_belt007", "g_i_belt008", "g_i_belt009", "g_i_belt010",
            "g_i_belt011", "g_i_belt012", "g_i_belt013", "g_i_belt014",
            "g_i_bithitem001", "g_i_bithitem002", "g_i_bithitem003", "g_i_bithitem004",
            "g_i_cmbtshot001", "g_i_cmbtshot002", "g_i_cmbtshot003", "g_i_collarlgt001",
            "g_i_credits001", "g_i_credits002", "g_i_credits003", "g_i_credits004",
            "g_i_credits005", "g_i_credits006", "g_i_credits007", "g_i_credits008",
            "g_i_credits009", "g_i_credits010", "g_i_credits011", "g_i_credits012",
            "g_i_credits013", "g_i_credits014", "g_i_credits015", "g_i_crhide001",
            "g_i_crhide002", "g_i_crhide003", "g_i_crhide004", "g_i_crhide005",
            "g_i_crhide006", "g_i_crhide007", "g_i_crhide008", "g_i_crhide009",
            "g_i_crhide010", "g_i_crhide011", "g_i_crhide012", "g_i_crhide013",
            "g_i_datapad001", "g_i_drdcomspk001", "g_i_drdcomspk002", "g_i_drdcomspk003",
            "g_i_drdhvplat001", "g_i_drdhvplat002", "g_i_drdhvplat003", "g_i_drdltplat001",
            "g_i_drdltplat002", "g_i_drdltplat003", "g_i_drdmdplat001", "g_i_drdmdplat002",
            "g_i_drdmdplat003", "g_i_drdmtnsen001", "g_i_drdmtnsen002", "g_i_drdmtnsen003",
            "g_i_drdrepeqp001", "g_i_drdrepeqp002", "g_i_drdrepeqp003", "g_i_drdsecspk001",
            "g_i_drdsecspk002", "g_i_drdsecspk003", "g_i_drdshld001", "g_i_drdshld002",
            "g_i_drdshld003", "g_i_drdshld005", "g_i_drdshld006", "g_i_drdshld007",
            "g_i_drdsncsen001", "g_i_drdsncsen002", "g_i_drdsncsen003", "g_i_drdsrcscp001",
            "g_i_drdsrcscp002", "g_i_drdsrcscp003", "g_i_drdtrgcom001", "g_i_drdtrgcom002",
            "g_i_drdtrgcom003", "g_i_drdtrgcom004", "g_i_drdtrgcom005", "g_i_drdtrgcom006",
            "g_i_drdutldev001", "g_i_drdutldev002", "g_i_drdutldev003", "g_i_drdutldev004",
            "g_i_drdutldev005", "g_i_drdutldev006", "g_i_drdutldev007", "g_i_drdutldev008",
            "g_i_drdutldev009", "g_i_drdutldev010", "g_i_drdutldev011", "g_i_frarmbnds01",
            "g_i_frarmbnds02", "g_i_frarmbnds03", "g_i_frarmbnds04", "g_i_frarmbnds05",
            "g_i_frarmbnds06", "g_i_frarmbnds07", "g_i_frarmbnds08", "g_i_frarmbnds09",
            "g_i_frarmbnds10", "g_i_frarmbnds11", "g_i_frarmbnds12", "g_i_frarmbnds13",
            "g_i_frarmbnds14", "g_i_frarmbnds15", "g_i_frarmbnds16", "g_i_frarmbnds17",
            "g_i_frarmbnds18", "g_i_frarmbnds19", "g_i_frarmbnds20", "g_i_frarmbnds21",
            "g_i_gauntlet01", "g_i_gauntlet02", "g_i_gauntlet03", "g_i_gauntlet04",
            "g_i_gauntlet05", "g_i_gauntlet06", "g_i_gauntlet07", "g_i_gauntlet08",
            "g_i_gauntlet09", "g_i_gizkapois001", "g_i_glowrod01", "g_i_implant101",
            "g_i_implant102", "g_i_implant103", "g_i_implant104", "g_i_implant201",
            "g_i_implant202", "g_i_implant203", "g_i_implant204", "g_i_implant301",
            "g_i_implant302", "g_i_implant303", "g_i_implant304", "g_i_implant305",
            "g_i_implant306", "g_i_implant307", "g_i_implant308", "g_i_implant309",
            "g_i_implant310", "g_i_mask01", "g_i_mask02", "g_i_mask03", "g_i_mask04",
            "g_i_mask05", "g_i_mask06", "g_i_mask07", "g_i_mask08", "g_i_mask09",
            "g_i_mask10", "g_i_mask11", "g_i_mask12", "g_i_mask13", "g_i_mask14",
            "g_i_mask15", "g_i_mask16", "g_i_mask17", "g_i_mask18", "g_i_mask19",
            "g_i_mask20", "g_i_mask21", "g_i_mask22", "g_i_mask23", "g_i_mask24",
            "g_i_medeqpmnt01", "g_i_medeqpmnt02", "g_i_medeqpmnt03", "g_i_medeqpmnt04",
            "g_i_medeqpmnt05", "g_i_medeqpmnt06", "g_i_medeqpmnt07", "g_i_medeqpmnt08",
            "g_i_parts01", "g_i_pazcard_001", "g_i_pazcard_002", "g_i_pazcard_003",
            "g_i_pazcard_004", "g_i_pazcard_005", "g_i_pazcard_006", "g_i_pazcard_007",
            "g_i_pazcard_008", "g_i_pazcard_009", "g_i_pazcard_010", "g_i_pazcard_011",
            "g_i_pazcard_012", "g_i_pazcard_013", "g_i_pazcard_014", "g_i_pazcard_015",
            "g_i_pazcard_016", "g_i_pazcard_017", "g_i_pazcard_018", "g_i_pazdeck",
            "g_i_pazsidebd001", "g_i_pltuseitm01", "g_i_progspike003", "g_i_progspike01",
            "g_i_progspike02", "g_i_recordrod01", "g_i_secspike01", "g_i_secspike02",
            "g_i_torch01", "g_i_trapkit001", "g_i_trapkit002", "g_i_trapkit003",
            "g_i_trapkit004", "g_i_trapkit005", "g_i_trapkit006", "g_i_trapkit007",
            "g_i_trapkit008", "g_i_trapkit009", "g_i_trapkit01", "g_i_trapkit010",
            "g_i_trapkit011", "g_i_trapkit012", "g_i_trapkit02", "g_i_trapkit03",
            "g_i_trapkit04", "g_i_upgrade001", "g_i_upgrade002", "g_i_upgrade003",
            "g_i_upgrade004", "g_i_upgrade005", "g_i_upgrade006", "g_i_upgrade007",
            "g_i_upgrade008", "g_i_upgrade009", "g_w_adhsvgren001", "g_w_blstrcrbn001",
            "g_w_blstrcrbn002", "g_w_blstrcrbn003", "g_w_blstrcrbn004", "g_w_blstrcrbn005",
            "g_w_blstrcrbn006", "g_w_blstrcrbn007", "g_w_blstrcrbn008", "g_w_blstrcrbn009",
            "g_w_blstrpstl001", "g_w_blstrpstl002", "g_w_blstrpstl003", "g_w_blstrpstl004",
            "g_w_blstrpstl005", "g_w_blstrpstl006", "g_w_blstrpstl007", "g_w_blstrpstl008",
            "g_w_blstrpstl009", "g_w_blstrpstl010", "g_w_blstrpstl020", "g_w_blstrrfl001",
            "g_w_blstrrfl002", "g_w_blstrrfl003", "g_w_blstrrfl004", "g_w_blstrrfl005",
            "g_w_blstrrfl006", "g_w_blstrrfl007", "g_w_blstrrfl008", "g_w_blstrrfl009",
            "g_w_bowcstr001", "g_w_bowcstr002", "g_w_bowcstr003", "g_w_crgore001",
            "g_w_crgore002", "g_w_crslash001", "g_w_crslash002", "g_w_crslash003",
            "g_w_crslash004", "g_w_crslash005", "g_w_crslash006", "g_w_crslash007",
            "g_w_crslash008", "g_w_crslash009", "g_w_crslash010", "g_w_crslash011",
            "g_w_crslash012", "g_w_crslprc001", "g_w_crslprc002", "g_w_crslprc003",
            "g_w_crslprc004", "g_w_crslprc005", "g_w_cryobgren001", "g_w_dblsbr001",
            "g_w_dblsbr002", "g_w_dblsbr003", "g_w_dblsbr004", "g_w_dblsbr005",
            "g_w_dblsbr006", "g_w_dblswrd001", "g_w_dblswrd002", "g_w_dblswrd003",
            "g_w_dblswrd005", "g_w_drkjdisbr001", "g_w_drkjdisbr002", "g_w_dsrptpstl001",
            "g_w_dsrptpstl002", "g_w_dsrptrfl001", "g_w_dsrptrfl002", "g_w_firegren001",
            "g_w_flashgren001", "g_w_fraggren01", "g_w_gaffi001", "g_w_hldoblstr01",
            "g_w_hldoblstr02", "g_w_hldoblstr03", "g_w_hldoblstr04", "g_w_hvrptbltr002",
            "g_w_hvrptbltr01", "g_w_hvrptbltr02", "g_w_hvyblstr01", "g_w_hvyblstr02",
            "g_w_hvyblstr03", "g_w_hvyblstr04", "g_w_hvyblstr05", "g_w_hvyblstr06",
            "g_w_hvyblstr07", "g_w_hvyblstr08", "g_w_hvyblstr09", "g_w_ionblstr01",
            "g_w_ionblstr02", "g_w_iongren01", "g_w_ionrfl01", "g_w_ionrfl02",
            "g_w_ionrfl03", "g_w_lghtsbr01", "g_w_lghtsbr02", "g_w_lghtsbr03",
            "g_w_lghtsbr04", "g_w_lghtsbr05", "g_w_lghtsbr06", "g_w_lngswrd01",
            "g_w_lngswrd02", "g_w_lngswrd03", "g_w_null001", "g_w_null002",
            "g_w_null003", "g_w_null004", "g_w_null005", "g_w_null006",
            "g_w_poisngren01", "g_w_qtrstaff01", "g_w_qtrstaff02", "g_w_qtrstaff03",
            "g_w_rptnblstr01", "g_w_rptnblstr02", "g_w_rptnblstr03", "g_w_sbrcrstl01",
            "g_w_sbrcrstl02", "g_w_sbrcrstl03", "g_w_sbrcrstl04", "g_w_sbrcrstl05",
            "g_w_sbrcrstl06", "g_w_sbrcrstl07", "g_w_sbrcrstl08", "g_w_sbrcrstl09",
            "g_w_sbrcrstl10", "g_w_sbrcrstl11", "g_w_sbrcrstl12", "g_w_sbrcrstl13",
            "g_w_sbrcrstl14", "g_w_sbrcrstl15", "g_w_sbrcrstl16", "g_w_sbrcrstl17",
            "g_w_sbrcrstl18", "g_w_sbrcrstl19", "g_w_shortsbr01", "g_w_shortsbr02",
            "g_w_shortsbr03", "g_w_shortsbr04", "g_w_shortsbr05", "g_w_shortswrd01",
            "g_w_shortswrd02", "g_w_shortswrd03", "g_w_sonicgren01", "g_w_sonicpstl01",
            "g_w_sonicpstl02", "g_w_sonicrfl01", "g_w_sonicrfl02", "g_w_sonicrfl03",
            "g_w_stunbaton01", "g_w_stunbaton02", "g_w_stunbaton03", "g_w_stunbaton04",
            "g_w_stunbaton05", "g_w_stunbaton06", "g_w_stunbaton07", "g_w_stungren01",
            "g_w_thermldet01", "g_w_vbrdblswd01", "g_w_vbrdblswd02", "g_w_vbrdblswd03",
            "g_w_vbrdblswd04", "g_w_vbrdblswd05", "g_w_vbrdblswd06", "g_w_vbrdblswd07",
            "g_w_vbroshort01", "g_w_vbroshort02", "g_w_vbroshort03", "g_w_vbroshort04",
            "g_w_vbroshort05", "g_w_vbroshort06", "g_w_vbroshort07", "g_w_vbroshort08",
            "g_w_vbroshort09", "g_w_vbroswrd01", "g_w_vbroswrd02", "g_w_vbroswrd03",
            "g_w_vbroswrd04", "g_w_vbroswrd05", "g_w_vbroswrd06", "g_w_vbroswrd07",
            "g_w_vbroswrd08", "g_w_waraxe001", "g_w_warblade001", "geno_armor",
            "geno_blade", "geno_blaster", "geno_datapad", "geno_gloves",
            "geno_stealth", "geno_visor", "kas25_wookcrysta", "ptar_rakghoulser",
            "ptar_sbpasscrd", "ptar_sitharmor", "tat17_sandperdis", "tat18_dragonprl",
            "w_blhvy001", "w_bstrcrbn", "w_lghtsbr001", "w_null" };


        /// <summary>
        /// Bound list of items to be omitted from randomization. This is necessary because certain items can result in soft locks if randomized.
        /// </summary>
        public static readonly List<string> DEFAULT_OMIT_ITEMS = new List<string>()
        {
            "g_i_collarlgt001",     // Collar Light (broken item)
            //"g_i_drdutldev005",     // Oil Slick (equippable, but unusable and unobtainable)
            "g_i_glowrod01",        // Glow Rod
            "g_i_implant104",       // Stamina Boost Implant
            "g_i_progspike02",      // Single-Use Programming Spikes
            "g_i_torch01",          // Torch (broken item)
            //"ptar_rakghoulser",     // Rakghoul Serum (plot)
            "ptar_sitharmor",       // Sith Armor
            "tat17_sandperdis",     // Sand People Disguise
            "w_null",
        };

        public static readonly List<RandomizableItem> ITEM_LIST_FULL = new List<RandomizableItem>()
        {
            new RandomizableItem() { ID = 24117330, Code = "g1_a_class5001",   Label = "Light Exoskeleton" },
            new RandomizableItem() { ID = 24117331, Code = "g1_a_class5002",   Label = "Baragwin Shadow Armor" },
            new RandomizableItem() { ID = 24117332, Code = "g1_a_class6001",   Label = "Environmental Bastion Armor" },
            new RandomizableItem() { ID = 24117333, Code = "g1_a_class8001",   Label = "Heavy Exoskeleton" },
            new RandomizableItem() { ID = 24117334, Code = "g1_i_belt001",     Label = "Baragwin Stealth Unit" },
            new RandomizableItem() { ID = 24117335, Code = "g1_i_drdcomspk01", Label = "Advanced Droid Interface" },
            new RandomizableItem() { ID = 24117336, Code = "g1_i_drdhvplat01", Label = "Composite Heavy Plating" },
            new RandomizableItem() { ID = 24117337, Code = "g1_i_drdshld001",  Label = "Baragwin Droid Shield" },
            new RandomizableItem() { ID = 24117338, Code = "g1_i_drdutldev01", Label = "Baragwin Flame Thrower" },
            new RandomizableItem() { ID = 24117339, Code = "g1_i_drdutldev02", Label = "Baragwin Stun Ray" },
            new RandomizableItem() { ID = 24117340, Code = "g1_i_drdutldev03", Label = "Baragwin Shield Disruptor" },
            new RandomizableItem() { ID = 24117341, Code = "g1_i_gauntlet01",  Label = "Advanced Stabilizer Gloves" },
            new RandomizableItem() { ID = 24117342, Code = "g1_i_implant301",  Label = "Advanced Sensory Implant" },
            new RandomizableItem() { ID = 24117343, Code = "g1_i_implant302",  Label = "Advanced Bio-Stabilizer Implant" },
            new RandomizableItem() { ID = 24117344, Code = "g1_i_implant303",  Label = "Advanced Combat Implant" },
            new RandomizableItem() { ID = 24117345, Code = "g1_i_implant304",  Label = "Advanced Alacrity Impant" },
            new RandomizableItem() { ID = 24117346, Code = "g1_i_mask01",      Label = "Advanced Bio-Stabilizer Mask" },
            new RandomizableItem() { ID = 24117347, Code = "g1_i_mask02",      Label = "Medical Interface Visor" },
            new RandomizableItem() { ID = 24117348, Code = "g1_i_mask03",      Label = "Advanced Agent Interface" },
            new RandomizableItem() { ID = 24117349, Code = "g1_w_dblsbr001",   Label = "Double-Bladed Lightsaber, HotG" },
            new RandomizableItem() { ID = 24117350, Code = "g1_w_dblsbr002",   Label = "Double-Bladed Lightsaber, MotF" },
            new RandomizableItem() { ID = 24117351, Code = "g1_w_dsrptrfl001", Label = "Baragwin Disruptor-X Weapon" },
            new RandomizableItem() { ID = 24117352, Code = "g1_w_hvrptbltr01", Label = "Baragwin Heavy Repeating Blaster" },
            new RandomizableItem() { ID = 24117353, Code = "g1_w_ionrfl01",    Label = "Baragwin Ion-X Weapon" },
            new RandomizableItem() { ID = 24117354, Code = "g1_w_lghtsbr01",   Label = "Lightsaber, HotG" },
            new RandomizableItem() { ID = 24117355, Code = "g1_w_lghtsbr02",   Label = "Lightsaber, MotF" },
            new RandomizableItem() { ID = 24117356, Code = "g1_w_rptnblstr01", Label = "Baragwin Assault Gun" },
            new RandomizableItem() { ID = 24117357, Code = "g1_w_sbrcrstl20",  Label = "Heart of the Guardian" },
            new RandomizableItem() { ID = 24117358, Code = "g1_w_sbrcrstl21",  Label = "Mantle of the Force" },
            new RandomizableItem() { ID = 24117359, Code = "g1_w_shortsbr01",  Label = "Short Lightsaber, HotG" },
            new RandomizableItem() { ID = 24117360, Code = "g1_w_shortsbr02",  Label = "Short Lightsaber, MotF" },
            new RandomizableItem() { ID = 24117361, Code = "g1_w_vbroswrd01",  Label = "Baragwin Assault Blade" },
            new RandomizableItem() { ID = 24117362, Code = "g_a_class4001",    Label = "Combat Suit" },
            new RandomizableItem() { ID = 24117363, Code = "g_a_class4002",    Label = "Zabrak Combat Suit" },
            new RandomizableItem() { ID = 24117364, Code = "g_a_class4003",    Label = "Echani Light Armor" },
            new RandomizableItem() { ID = 24117365, Code = "g_a_class4004",    Label = "Cinnagar Weave Armor" },
            new RandomizableItem() { ID = 24117366, Code = "g_a_class4005",    Label = "Massassi Ceremonial Armor" },
            new RandomizableItem() { ID = 24117367, Code = "g_a_class4006",    Label = "Bandon's Fiber Armor" }, // lgt, 5d+5, ug, 25% fire res (dark color)
            new RandomizableItem() { ID = 24117368, Code = "g_a_class4007",    Label = "Bandon's Fiber Armor +1" }, // lgt, 7d+5, !ug, 25% fire res
            new RandomizableItem() { ID = 24117369, Code = "g_a_class4008",    Label = "Bandon's Fiber Armor +2" }, // lgt, 7d+5, !ug, 25% fire res, mind Imm
            new RandomizableItem() { ID = 24117370, Code = "g_a_class4009",    Label = "Echani Fiber Armor" },
            new RandomizableItem() { ID = 24117371, Code = "g_a_class5001",    Label = "Heavy Combat Suit" },
            new RandomizableItem() { ID = 24117372, Code = "g_a_class5002",    Label = "Bonadan Alloy Heavy Suit" },
            new RandomizableItem() { ID = 24117373, Code = "g_a_class5003",    Label = "Zabrak Battle Armor" },
            new RandomizableItem() { ID = 24117374, Code = "g_a_class5004",    Label = "Zabrak Field Armor" },
            new RandomizableItem() { ID = 24117375, Code = "g_a_class5005",    Label = "Reinforced Fiber Armor" },
            new RandomizableItem() { ID = 24117376, Code = "g_a_class5006",    Label = "Ulic Qel Droma's Mesh Suit" },
            new RandomizableItem() { ID = 24117377, Code = "g_a_class5007",    Label = "Eriadu Prototype Armor" }, // lgt, 6+4, ug
            new RandomizableItem() { ID = 24117378, Code = "g_a_class5008",    Label = "Eriadu Prototype Armor +1" }, // lgt, 9+4, fug
            new RandomizableItem() { ID = 24117379, Code = "g_a_class5009",    Label = "Eriadu Prototype Armor +2" }, // lgt, 9+4, !ug, 30% cold res, mind imm
            new RandomizableItem() { ID = 24117380, Code = "g_a_class5010",    Label = "Republic Mod Armor" },
            new RandomizableItem() { ID = 24117381, Code = "g_a_class6001",    Label = "Military Suit" },
            new RandomizableItem() { ID = 24117382, Code = "g_a_class6002",    Label = "Echani Battle Armor" },
            new RandomizableItem() { ID = 24117383, Code = "g_a_class6003",    Label = "Cinnagar War Suit" },
            new RandomizableItem() { ID = 24117384, Code = "g_a_class6004",    Label = "Verpine Fiber Mesh" },
            new RandomizableItem() { ID = 24117385, Code = "g_a_class6005",    Label = "Arkanian Bond Armor" },
            new RandomizableItem() { ID = 24117386, Code = "g_a_class6006",    Label = "Exar Kun's Light Battle Suit" },
            new RandomizableItem() { ID = 24117387, Code = "g_a_class6007",    Label = "Davik's War Suit" }, // med, 8+3, 10% cold 10% fire res
            new RandomizableItem() { ID = 24117388, Code = "g_a_class6008",    Label = "Davik's War Suit +1" }, // med, 10+3, !ug, 10% cold 10% fire res
            new RandomizableItem() { ID = 24117389, Code = "g_a_class6009",    Label = "Davik's War Suit +2" }, // med, 10+3, !ug, 20% cold 20% fire res, mind imm
            new RandomizableItem() { ID = 24117390, Code = "g_a_class7001",    Label = "Light Battle Armor" },
            new RandomizableItem() { ID = 24117391, Code = "g_a_class7002",    Label = "Bronzium Light Battle Armor" },
            new RandomizableItem() { ID = 24117392, Code = "g_a_class7003",    Label = "Powered Light Battle Armor" },
            new RandomizableItem() { ID = 24117393, Code = "g_a_class7004",    Label = "Krath Heavy Armor" },
            new RandomizableItem() { ID = 24117394, Code = "g_a_class7005",    Label = "Krath Holy Battle Suit" },
            new RandomizableItem() { ID = 24117395, Code = "g_a_class7006",    Label = "Jamoh Hogra's Battle Armor" },
            new RandomizableItem() { ID = 24117396, Code = "g_a_class8001",    Label = "Battle Armor" },
            new RandomizableItem() { ID = 24117397, Code = "g_a_class8002",    Label = "Powered Battle Armor" },
            new RandomizableItem() { ID = 24117398, Code = "g_a_class8003",    Label = "Cinnagar Plate Armor" },
            new RandomizableItem() { ID = 24117399, Code = "g_a_class8004",    Label = "Mandalorian Armor" },
            new RandomizableItem() { ID = 24117400, Code = "g_a_class8005",    Label = "Calo Nord's Battle Armor" }, // hvy, 9+1, ug, 10% cold fire sonic
            new RandomizableItem() { ID = 24117401, Code = "g_a_class8006",    Label = "Calo Nord's Battle Armor +1" }, // hvy, 12+1, !ug, 25% cold fire sonic, crit imm
            new RandomizableItem() { ID = 24117402, Code = "g_a_class8007",    Label = "Calo Nord's Battle Armor +2" }, // hvy, 12+1, !ug, 25% cold fire sonic, crit & mind imm
            new RandomizableItem() { ID = 24117403, Code = "g_a_class8009",    Label = "Verpine Zal Alloy Mesh" },
            new RandomizableItem() { ID = 24117404, Code = "g_a_class9001",    Label = "Heavy Battle Armor" },
            new RandomizableItem() { ID = 24117405, Code = "g_a_class9002",    Label = "Durasteel Heavy Armor" },
            new RandomizableItem() { ID = 24117406, Code = "g_a_class9003",    Label = "Mandalorian Battle Armor" },
            new RandomizableItem() { ID = 24117407, Code = "g_a_class9004",    Label = "Mandalorian Heavy Armor" },
            new RandomizableItem() { ID = 24117408, Code = "g_a_class9005",    Label = "Jurgan Kalta's Power Suit" }, // hvy, 10+0, ug, 10% cold fire sonic
            new RandomizableItem() { ID = 24117409, Code = "g_a_class9006",    Label = "Jurgan Kalta's Power Suit +1" }, // hvy, 13+0, !ug, 25% cold fire sonic
            new RandomizableItem() { ID = 24117410, Code = "g_a_class9007",    Label = "Jurgan Kalta's Power Suit +2" }, // hvy, 13+0, !ug, 30% cold fire sonic, mind imm
            new RandomizableItem() { ID = 24117411, Code = "g_a_class9009",    Label = "Cassus Fett's Battle Armor" }, // hvy, 10+0, ug, 10% cold fire sonic
            new RandomizableItem() { ID = 24117412, Code = "g_a_class9010",    Label = "Mandalorian Assault Armor" },
            new RandomizableItem() { ID = 24117413, Code = "g_a_class9011",    Label = "Cassus Fett's Battle Armor +1" }, // hvy, 14+0, !ug, 25% cold fire sonic, str+1
            new RandomizableItem() { ID = 24117414, Code = "g_a_clothes01",    Label = "Clothing" },
            new RandomizableItem() { ID = 24117415, Code = "g_a_clothes02",    Label = "Clothing Variant 2" },
            new RandomizableItem() { ID = 24117416, Code = "g_a_clothes03",    Label = "Clothing Variant 3" },
            new RandomizableItem() { ID = 24117417, Code = "g_a_clothes04",    Label = "Clothing Variant 4" },
            new RandomizableItem() { ID = 24117418, Code = "g_a_clothes05",    Label = "Clothing Variant 5" },
            new RandomizableItem() { ID = 24117419, Code = "g_a_clothes06",    Label = "Clothing Variant 6" },
            new RandomizableItem() { ID = 24117420, Code = "g_a_clothes07",    Label = "Clothing Variant 1" },
            new RandomizableItem() { ID = 24117421, Code = "g_a_clothes08",    Label = "Clothing Variant 7" },
            new RandomizableItem() { ID = 24117422, Code = "g_a_clothes09",    Label = "Clothing Variant 8" },
            new RandomizableItem() { ID = 24117423, Code = "g_a_jedirobe01",   Label = "Jedi Robe, brown" },
            new RandomizableItem() { ID = 24117424, Code = "g_a_jedirobe02",   Label = "Dark Jedi Robe, black" },
            new RandomizableItem() { ID = 24117425, Code = "g_a_jedirobe03",   Label = "Jedi Robe, red" },
            new RandomizableItem() { ID = 24117426, Code = "g_a_jedirobe04",   Label = "Jedi Robe, blue" },
            new RandomizableItem() { ID = 24117427, Code = "g_a_jedirobe05",   Label = "Dark Jedi Robe, blue" },
            new RandomizableItem() { ID = 24117428, Code = "g_a_jedirobe06",   Label = "Qel-Droma Robes" },
            new RandomizableItem() { ID = 24117429, Code = "g_a_kghtrobe01",   Label = "Jedi Knight Robe, brown" },
            new RandomizableItem() { ID = 24117430, Code = "g_a_kghtrobe02",   Label = "Dark Jedi Knight Robe, black" },
            new RandomizableItem() { ID = 24117431, Code = "g_a_kghtrobe03",   Label = "Jedi Knight Robe, red" },
            new RandomizableItem() { ID = 24117432, Code = "g_a_kghtrobe04",   Label = "Jedi Knight Robe, blue" },
            new RandomizableItem() { ID = 24117433, Code = "g_a_kghtrobe05",   Label = "Dark Jedi Knight Robe, blue" },
            new RandomizableItem() { ID = 24117434, Code = "g_a_mstrrobe01",   Label = "Jedi Master Robe, brown" },
            new RandomizableItem() { ID = 24117435, Code = "g_a_mstrrobe02",   Label = "Dark Jedi Master Robe, black" },
            new RandomizableItem() { ID = 24117436, Code = "g_a_mstrrobe03",   Label = "Jedi Master Robe, red" },
            new RandomizableItem() { ID = 24117437, Code = "g_a_mstrrobe04",   Label = "Jedi Master Robe, blue" },
            new RandomizableItem() { ID = 24117438, Code = "g_a_mstrrobe05",   Label = "Dark Jedi Master Robe, blue" },
            new RandomizableItem() { ID = 24117439, Code = "g_a_mstrrobe06",   Label = "Darth Revan's Robes" },
            new RandomizableItem() { ID = 24117440, Code = "g_a_mstrrobe07",   Label = "Star Forge Robes" },
            new RandomizableItem() { ID = 24117490, Code = "g_i_adrnaline001", Label = "Adrenal Strength" },
            new RandomizableItem() { ID = 24117491, Code = "g_i_adrnaline002", Label = "Adrenal Alacrity" },
            new RandomizableItem() { ID = 24117492, Code = "g_i_adrnaline003", Label = "Adrenal Stamina" },
            new RandomizableItem() { ID = 24117493, Code = "g_i_adrnaline004", Label = "Hyper-adrenal Strength" },
            new RandomizableItem() { ID = 24117494, Code = "g_i_adrnaline005", Label = "Hyper-adrenal Alacrity" },
            new RandomizableItem() { ID = 24117495, Code = "g_i_adrnaline006", Label = "Hyper-adrenal Stamina" },
            new RandomizableItem() { ID = 24117496, Code = "g_i_asthitem001",  Label = "Aesthetic Item" },
            new RandomizableItem() { ID = 24117497, Code = "g_i_belt001",      Label = "Cardio-Regulator" },
            new RandomizableItem() { ID = 24117498, Code = "g_i_belt002",      Label = "Verpine Cardio-Regulator" },
            new RandomizableItem() { ID = 24117499, Code = "g_i_belt003",      Label = "Adrenaline Amplifier" },
            new RandomizableItem() { ID = 24117500, Code = "g_i_belt004",      Label = "Advanced Adrenaline Amplifier" },
            new RandomizableItem() { ID = 24117501, Code = "g_i_belt005",      Label = "Nerve Amplifier Belt" },
            new RandomizableItem() { ID = 24117502, Code = "g_i_belt006",      Label = "Sound Dampening Stealth Unit" },
            new RandomizableItem() { ID = 24117503, Code = "g_i_belt007",      Label = "Advanced Stealth Unit" },
            new RandomizableItem() { ID = 24117504, Code = "g_i_belt008",      Label = "Eriadu Stealth Unit" },
            new RandomizableItem() { ID = 24117505, Code = "g_i_belt009",      Label = "Calrissian's Utility Belt" },
            new RandomizableItem() { ID = 24117506, Code = "g_i_belt010",      Label = "Stealth Field Generator" },
            new RandomizableItem() { ID = 24117507, Code = "g_i_belt011",      Label = "Adrenaline Stimulator" },
            new RandomizableItem() { ID = 24117508, Code = "g_i_belt012",      Label = "CNS Strength Enhancer" },
            new RandomizableItem() { ID = 24117509, Code = "g_i_belt013",      Label = "Electrical Capacitance Shield" },
            new RandomizableItem() { ID = 24117510, Code = "g_i_belt014",      Label = "Thermal Shield Generator" },
            new RandomizableItem() { ID = 24117511, Code = "g_i_bithitem001",  Label = "Bith Guitar" },
            new RandomizableItem() { ID = 24117512, Code = "g_i_bithitem002",  Label = "Bith Clarinet" },
            new RandomizableItem() { ID = 24117513, Code = "g_i_bithitem003",  Label = "Bith Trombone" },
            new RandomizableItem() { ID = 24117514, Code = "g_i_bithitem004",  Label = "Bith Accordian" },
            new RandomizableItem() { ID = 24117515, Code = "g_i_cmbtshot001",  Label = "Battle Stimulant" },
            new RandomizableItem() { ID = 24117516, Code = "g_i_cmbtshot002",  Label = "Hyper-battle Stimulant" },
            new RandomizableItem() { ID = 24117517, Code = "g_i_cmbtshot003",  Label = "Echani Battle Stimulant" },
            new RandomizableItem() { ID = 24117518, Code = "g_i_collarlgt001", Label = "Collar Light" },
            new RandomizableItem() { ID = 24117519, Code = "g_i_credits001",   Label = "Credits, 0005" },
            new RandomizableItem() { ID = 24117520, Code = "g_i_credits002",   Label = "Credits, 0010" },
            new RandomizableItem() { ID = 24117521, Code = "g_i_credits003",   Label = "Credits, 0025" },
            new RandomizableItem() { ID = 24117522, Code = "g_i_credits004",   Label = "Credits, 0050" },
            new RandomizableItem() { ID = 24117523, Code = "g_i_credits005",   Label = "Credits, 0100" },
            new RandomizableItem() { ID = 24117524, Code = "g_i_credits006",   Label = "Credits, 0200" },
            new RandomizableItem() { ID = 24117525, Code = "g_i_credits007",   Label = "Credits, 0300" },
            new RandomizableItem() { ID = 24117526, Code = "g_i_credits008",   Label = "Credits, 0400" },
            new RandomizableItem() { ID = 24117527, Code = "g_i_credits009",   Label = "Credits, 0500" },
            new RandomizableItem() { ID = 24117528, Code = "g_i_credits010",   Label = "Credits, 1000" },
            new RandomizableItem() { ID = 24117529, Code = "g_i_credits011",   Label = "Credits, 2000" },
            new RandomizableItem() { ID = 24117530, Code = "g_i_credits012",   Label = "Credits, 3000" },
            new RandomizableItem() { ID = 24117531, Code = "g_i_credits013",   Label = "Credits, 4000" },
            new RandomizableItem() { ID = 24117532, Code = "g_i_credits014",   Label = "Credits, 5000" },
            new RandomizableItem() { ID = 24117533, Code = "g_i_credits015",   Label = "Credits, 0001" },
            new RandomizableItem() { ID = 24117534, Code = "g_i_crhide001",    Label = "Assault Droid Mark I Hide" },
            new RandomizableItem() { ID = 24117535, Code = "g_i_crhide002",    Label = "Assault Droid Mark IV Hide" },
            new RandomizableItem() { ID = 24117536, Code = "g_i_crhide003",    Label = "Rakghoul Fiend & Kataarn Hide" },
            new RandomizableItem() { ID = 24117537, Code = "g_i_crhide004",    Label = "Tuk'ata Hide" }, // FRes+32, Regen+1
            new RandomizableItem() { ID = 24117538, Code = "g_i_crhide005",    Label = "Katarn Hide" },
            new RandomizableItem() { ID = 24117539, Code = "g_i_crhide006",    Label = "Tuk'ata Hide" }, // FRes+32, Regen+1
            new RandomizableItem() { ID = 24117540, Code = "g_i_crhide007",    Label = "Canderous' Hide" },
            new RandomizableItem() { ID = 24117541, Code = "g_i_crhide008",    Label = "Droid Hide" },
            new RandomizableItem() { ID = 24117542, Code = "g_i_crhide009",    Label = "HK-47 Hide 1" },
            new RandomizableItem() { ID = 24117543, Code = "g_i_crhide010",    Label = "HK-47 Hide 2" },
            new RandomizableItem() { ID = 24117544, Code = "g_i_crhide011",    Label = "HK-47 Hide 3" },
            new RandomizableItem() { ID = 24117545, Code = "g_i_crhide012",    Label = "HK-47 Hide 4" },
            new RandomizableItem() { ID = 24117546, Code = "g_i_crhide013",    Label = "HK-47 Hide 0" },
            new RandomizableItem() { ID = 24117547, Code = "g_i_datapad001",   Label = "Datapad, template" },
            new RandomizableItem() { ID = 24117548, Code = "g_i_drdcomspk001", Label = "Computer Probe" },
            new RandomizableItem() { ID = 24117549, Code = "g_i_drdcomspk002", Label = "Universal Computer Interface" },
            new RandomizableItem() { ID = 24117550, Code = "g_i_drdcomspk003", Label = "Advanced Computer Tool" },
            new RandomizableItem() { ID = 24117551, Code = "g_i_drdhvplat001", Label = "Droid Heavy Plating Type 1" },
            new RandomizableItem() { ID = 24117552, Code = "g_i_drdhvplat002", Label = "Droid Heavy Plating Type 2" },
            new RandomizableItem() { ID = 24117553, Code = "g_i_drdhvplat003", Label = "Droid Heavy Plating Type 3" },
            new RandomizableItem() { ID = 24117554, Code = "g_i_drdltplat001", Label = "Droid Light Plating Type 1" },
            new RandomizableItem() { ID = 24117555, Code = "g_i_drdltplat002", Label = "Droid Light Plating Type 2" },
            new RandomizableItem() { ID = 24117556, Code = "g_i_drdltplat003", Label = "Droid Light Plating Type 3" },
            new RandomizableItem() { ID = 24117557, Code = "g_i_drdmdplat001", Label = "Droid Medium Plating Type 1" },
            new RandomizableItem() { ID = 24117558, Code = "g_i_drdmdplat002", Label = "Droid Medium Plating Type 2" },
            new RandomizableItem() { ID = 24117559, Code = "g_i_drdmdplat003", Label = "Droid Medium Plating Type 3" },
            new RandomizableItem() { ID = 24117560, Code = "g_i_drdmtnsen001", Label = "Droid Motion Sensors Type 1" },
            new RandomizableItem() { ID = 24117561, Code = "g_i_drdmtnsen002", Label = "Droid Motion Sensors Type 2" },
            new RandomizableItem() { ID = 24117562, Code = "g_i_drdmtnsen003", Label = "Droid Motion Sensors Type 3" },
            new RandomizableItem() { ID = 24117563, Code = "g_i_drdrepeqp001", Label = "Repair Kit" },
            new RandomizableItem() { ID = 24117564, Code = "g_i_drdrepeqp002", Label = "Advanced Repair Kit" },
            new RandomizableItem() { ID = 24117565, Code = "g_i_drdrepeqp003", Label = "Construction Kit" },
            new RandomizableItem() { ID = 24117566, Code = "g_i_drdsecspk001", Label = "Security Interface Tool" },
            new RandomizableItem() { ID = 24117567, Code = "g_i_drdsecspk002", Label = "Security Domination Interface" },
            new RandomizableItem() { ID = 24117568, Code = "g_i_drdsecspk003", Label = "Security Decryption Interface" },
            new RandomizableItem() { ID = 24117569, Code = "g_i_drdshld001",   Label = "Energy Shield Level 1" },
            new RandomizableItem() { ID = 24117570, Code = "g_i_drdshld002",   Label = "Energy Shield Level 2" },
            new RandomizableItem() { ID = 24117571, Code = "g_i_drdshld003",   Label = "Energy Shield Level 3" },
            new RandomizableItem() { ID = 24117572, Code = "g_i_drdshld005",   Label = "Environment Shield Level 1" },
            new RandomizableItem() { ID = 24117573, Code = "g_i_drdshld006",   Label = "Environment Shield Level 2" },
            new RandomizableItem() { ID = 24117574, Code = "g_i_drdshld007",   Label = "Environment Shield Level 3" },
            new RandomizableItem() { ID = 24117575, Code = "g_i_drdsncsen001", Label = "Droid Sonic Sensors Type 1" },
            new RandomizableItem() { ID = 24117576, Code = "g_i_drdsncsen002", Label = "Droid Sonic Sensors Type 2" },
            new RandomizableItem() { ID = 24117577, Code = "g_i_drdsncsen003", Label = "Droid Sonic Sensors Type 3" },
            new RandomizableItem() { ID = 24117578, Code = "g_i_drdsrcscp001", Label = "Droid Search Scope Type 1" },
            new RandomizableItem() { ID = 24117579, Code = "g_i_drdsrcscp002", Label = "Droid Search Scope Type 2" },
            new RandomizableItem() { ID = 24117580, Code = "g_i_drdsrcscp003", Label = "Droid Search Scope Type 3" },
            new RandomizableItem() { ID = 24117581, Code = "g_i_drdtrgcom001", Label = "Basic Targeting Computer" },
            new RandomizableItem() { ID = 24117582, Code = "g_i_drdtrgcom002", Label = "Advanced Targeting Computer" },
            new RandomizableItem() { ID = 24117583, Code = "g_i_drdtrgcom003", Label = "Superior Targeting Computer" },
            new RandomizableItem() { ID = 24117584, Code = "g_i_drdtrgcom004", Label = "Sensor Probe" },
            new RandomizableItem() { ID = 24117585, Code = "g_i_drdtrgcom005", Label = "Verpine Demolitions Probe" },
            new RandomizableItem() { ID = 24117586, Code = "g_i_drdtrgcom006", Label = "Bothan Demolitions Probe" },
            new RandomizableItem() { ID = 24117587, Code = "g_i_drdutldev001", Label = "Stun Ray" },
            new RandomizableItem() { ID = 24117588, Code = "g_i_drdutldev002", Label = "Advanced Stun Ray" },
            new RandomizableItem() { ID = 24117589, Code = "g_i_drdutldev003", Label = "Shield Disruptor" },
            new RandomizableItem() { ID = 24117590, Code = "g_i_drdutldev004", Label = "Advanced Shield Disruptor" },
            new RandomizableItem() { ID = 24117591, Code = "g_i_drdutldev005", Label = "Oil Slick" },
            new RandomizableItem() { ID = 24117592, Code = "g_i_drdutldev006", Label = "Flame Thrower" },
            new RandomizableItem() { ID = 24117593, Code = "g_i_drdutldev007", Label = "Advanced Flame Thrower" },
            new RandomizableItem() { ID = 24117594, Code = "g_i_drdutldev008", Label = "Carbonite Projector" },
            new RandomizableItem() { ID = 24117595, Code = "g_i_drdutldev009", Label = "Carbonite Projector Mark II" },
            new RandomizableItem() { ID = 24117596, Code = "g_i_drdutldev010", Label = "Gravity Generator" },
            new RandomizableItem() { ID = 24117597, Code = "g_i_drdutldev011", Label = "Adv. Gravity Generator" },
            new RandomizableItem() { ID = 24117598, Code = "g_i_frarmbnds01",  Label = "Energy Shield" },
            new RandomizableItem() { ID = 24117599, Code = "g_i_frarmbnds02",  Label = "Sith Energy Shield" },
            new RandomizableItem() { ID = 24117600, Code = "g_i_frarmbnds03",  Label = "Arkanian Energy Shield" },
            new RandomizableItem() { ID = 24117601, Code = "g_i_frarmbnds04",  Label = "Echani Shield" },
            new RandomizableItem() { ID = 24117602, Code = "g_i_frarmbnds05",  Label = "Mandalorian Melee Shield" },
            new RandomizableItem() { ID = 24117603, Code = "g_i_frarmbnds06",  Label = "Mandalorian Power Shield" },
            new RandomizableItem() { ID = 24117604, Code = "g_i_frarmbnds07",  Label = "Echani Dueling Shield" },
            new RandomizableItem() { ID = 24117605, Code = "g_i_frarmbnds08",  Label = "Yusanis' Dueling Shield" },
            new RandomizableItem() { ID = 24117606, Code = "g_i_frarmbnds09",  Label = "Verpine Prototype Shield" },
            new RandomizableItem() { ID = 24117607, Code = "g_i_frarmbnds10",  Label = "Lower Saves, All 2" },
            new RandomizableItem() { ID = 24117608, Code = "g_i_frarmbnds11",  Label = "Lower Saves, All 4" },
            new RandomizableItem() { ID = 24117609, Code = "g_i_frarmbnds12",  Label = "Lower Saves, All 5" },
            new RandomizableItem() { ID = 24117610, Code = "g_i_frarmbnds13",  Label = "Lower Saves, Fortitude 2" },
            new RandomizableItem() { ID = 24117611, Code = "g_i_frarmbnds14",  Label = "Lower Saves, Fortitude 4" },
            new RandomizableItem() { ID = 24117612, Code = "g_i_frarmbnds15",  Label = "Lower Saves, Fortitude 5" },
            new RandomizableItem() { ID = 24117613, Code = "g_i_frarmbnds16",  Label = "Lower Saves, Reflex 2" },
            new RandomizableItem() { ID = 24117614, Code = "g_i_frarmbnds17",  Label = "Lower Saves, Reflex 4" },
            new RandomizableItem() { ID = 24117615, Code = "g_i_frarmbnds18",  Label = "Lower Saves, Reflex 5" },
            new RandomizableItem() { ID = 24117616, Code = "g_i_frarmbnds19",  Label = "Lower Saves, Will 2" },
            new RandomizableItem() { ID = 24117617, Code = "g_i_frarmbnds20",  Label = "Lower Saves, Will 4" },
            new RandomizableItem() { ID = 24117618, Code = "g_i_frarmbnds21",  Label = "Lower Saves, Will 5" },
            new RandomizableItem() { ID = 24117619, Code = "g_i_gauntlet01",   Label = "Strength Gauntlets" },
            new RandomizableItem() { ID = 24117620, Code = "g_i_gauntlet02",   Label = "Eriadu Strength Amplifier" },
            new RandomizableItem() { ID = 24117621, Code = "g_i_gauntlet03",   Label = "Sith Power Gauntlets" },
            new RandomizableItem() { ID = 24117622, Code = "g_i_gauntlet04",   Label = "Stabilizer Gauntlets" },
            new RandomizableItem() { ID = 24117623, Code = "g_i_gauntlet05",   Label = "Bothan \"Machinist\" Gloves" },
            new RandomizableItem() { ID = 24117624, Code = "g_i_gauntlet06",   Label = "Verpine Bond Gauntlets" },
            new RandomizableItem() { ID = 24117625, Code = "g_i_gauntlet07",   Label = "Dominator Gauntlets" },
            new RandomizableItem() { ID = 24117626, Code = "g_i_gauntlet08",   Label = "Karakan Gauntlets" },
            new RandomizableItem() { ID = 24117627, Code = "g_i_gauntlet09",   Label = "Infiltrator Gloves" },
            new RandomizableItem() { ID = 24117628, Code = "g_i_gizkapois001", Label = "Gizka Poison" },
            new RandomizableItem() { ID = 24117629, Code = "g_i_glowrod01",    Label = "Glow Rod" },
            new RandomizableItem() { ID = 24117630, Code = "g_i_implant101",   Label = "Cardio Package" },
            new RandomizableItem() { ID = 24117631, Code = "g_i_implant102",   Label = "Response Package" },
            new RandomizableItem() { ID = 24117632, Code = "g_i_implant103",   Label = "Memory Package" },
            new RandomizableItem() { ID = 24117633, Code = "g_i_implant104",   Label = "Done" },
            new RandomizableItem() { ID = 24117634, Code = "g_i_implant201",   Label = "Biotech Package" },
            new RandomizableItem() { ID = 24117635, Code = "g_i_implant202",   Label = "Retinal Combat Implant" },
            new RandomizableItem() { ID = 24117636, Code = "g_i_implant203",   Label = "Nerve Enhancement Package" },
            new RandomizableItem() { ID = 24117637, Code = "g_i_implant204",   Label = "I need to make the party selections screen available" },
            new RandomizableItem() { ID = 24117638, Code = "g_i_implant301",   Label = "Bavakar Cardio Package" },
            new RandomizableItem() { ID = 24117639, Code = "g_i_implant302",   Label = "Bavakar Reflex Enhancement" },
            new RandomizableItem() { ID = 24117640, Code = "g_i_implant303",   Label = "Bavakar Memory Chip" },
            new RandomizableItem() { ID = 24117641, Code = "g_i_implant304",   Label = "Bio-Antidote Package" },
            new RandomizableItem() { ID = 24117642, Code = "g_i_implant305",   Label = "Cardio Power System" },
            new RandomizableItem() { ID = 24117643, Code = "g_i_implant306",   Label = "Gordulan Reaction System" },
            new RandomizableItem() { ID = 24117644, Code = "g_i_implant307",   Label = "Navardan Regenerator" },
            new RandomizableItem() { ID = 24117645, Code = "g_i_implant308",   Label = "Sith Regenerator" },
            new RandomizableItem() { ID = 24117646, Code = "g_i_implant309",   Label = "Beemon Package" },
            new RandomizableItem() { ID = 24117647, Code = "g_i_implant310",   Label = "Cyber Reaction System" },
            new RandomizableItem() { ID = 24117648, Code = "g_i_mask01",       Label = "Light-Scan Visor" },
            new RandomizableItem() { ID = 24117649, Code = "g_i_mask02",       Label = "Motion Detection Goggles" },
            new RandomizableItem() { ID = 24117650, Code = "g_i_mask03",       Label = "Bothan Perception Visor" },
            new RandomizableItem() { ID = 24117651, Code = "g_i_mask04",       Label = "Verpine Ocular Enhancer" },
            new RandomizableItem() { ID = 24117652, Code = "g_i_mask05",       Label = "Bothan Sensory Visor" },
            new RandomizableItem() { ID = 24117653, Code = "g_i_mask06",       Label = "Vacuum Mask" },
            new RandomizableItem() { ID = 24117654, Code = "g_i_mask07",       Label = "Sonic Nullifiers" },
            new RandomizableItem() { ID = 24117655, Code = "g_i_mask08",       Label = "Aural Amplifier" },
            new RandomizableItem() { ID = 24117656, Code = "g_i_mask09",       Label = "Advanced Aural Amplifier" },
            new RandomizableItem() { ID = 24117657, Code = "g_i_mask10",       Label = "Neural Band" },
            new RandomizableItem() { ID = 24117658, Code = "g_i_mask11",       Label = "Verpine Headband" },
            new RandomizableItem() { ID = 24117659, Code = "g_i_mask12",       Label = "Breath Mask" },
            new RandomizableItem() { ID = 24117660, Code = "g_i_mask13",       Label = "Teta's Royal Band" },
            new RandomizableItem() { ID = 24117661, Code = "g_i_mask14",       Label = "Sith Mask" },
            new RandomizableItem() { ID = 24117662, Code = "g_i_mask15",       Label = "Stabilizer Mask" },
            new RandomizableItem() { ID = 24117663, Code = "g_i_mask16",       Label = "Interface Band" },
            new RandomizableItem() { ID = 24117664, Code = "g_i_mask17",       Label = "Demolitions Sensor" },
            new RandomizableItem() { ID = 24117665, Code = "g_i_mask18",       Label = "Combat Sensor" },
            new RandomizableItem() { ID = 24117666, Code = "g_i_mask19",       Label = "Stealth Field Enhancer" },
            new RandomizableItem() { ID = 24117667, Code = "g_i_mask20",       Label = "Stealth Field Reinforcement" },
            new RandomizableItem() { ID = 24117668, Code = "g_i_mask21",       Label = "Interface Visor" },
            new RandomizableItem() { ID = 24117669, Code = "g_i_mask22",       Label = "Circlet of Saresh" },
            new RandomizableItem() { ID = 24117670, Code = "g_i_mask23",       Label = "Pistol Targetting Optics" },
            new RandomizableItem() { ID = 24117671, Code = "g_i_mask24",       Label = "Heavy Targetting Optics" },
            new RandomizableItem() { ID = 24117672, Code = "g_i_medeqpmnt01",  Label = "Medpac" },
            new RandomizableItem() { ID = 24117673, Code = "g_i_medeqpmnt02",  Label = "Advanced Medpac" },
            new RandomizableItem() { ID = 24117674, Code = "g_i_medeqpmnt03",  Label = "Life Support Pack" },
            new RandomizableItem() { ID = 24117675, Code = "g_i_medeqpmnt04",  Label = "Antidote Kit" },
            new RandomizableItem() { ID = 24117676, Code = "g_i_medeqpmnt05",  Label = "Antibiotic Kit" },
            new RandomizableItem() { ID = 24117677, Code = "g_i_medeqpmnt06",  Label = "Advanced Medpac" },
            new RandomizableItem() { ID = 24117678, Code = "g_i_medeqpmnt07",  Label = "Life Support Pack" },
            new RandomizableItem() { ID = 24117679, Code = "g_i_medeqpmnt08",  Label = "Squad Recovery Stim" },
            new RandomizableItem() { ID = 24117680, Code = "g_i_parts01",      Label = "Parts" },
            new RandomizableItem() { ID = 24117681, Code = "g_i_pazcard_001",  Label = "Pazaak Card +1" },
            new RandomizableItem() { ID = 24117682, Code = "g_i_pazcard_002",  Label = "Pazaak Card +2" },
            new RandomizableItem() { ID = 24117683, Code = "g_i_pazcard_003",  Label = "Pazaak Card +3" },
            new RandomizableItem() { ID = 24117684, Code = "g_i_pazcard_004",  Label = "Pazaak Card +4" },
            new RandomizableItem() { ID = 24117685, Code = "g_i_pazcard_005",  Label = "Pazaak Card +5" },
            new RandomizableItem() { ID = 24117686, Code = "g_i_pazcard_006",  Label = "Pazaak Card +6" },
            new RandomizableItem() { ID = 24117687, Code = "g_i_pazcard_007",  Label = "Pazaak Card -1" },
            new RandomizableItem() { ID = 24117688, Code = "g_i_pazcard_008",  Label = "Pazaak Card -2" },
            new RandomizableItem() { ID = 24117689, Code = "g_i_pazcard_009",  Label = "Pazaak Card -3" },
            new RandomizableItem() { ID = 24117690, Code = "g_i_pazcard_010",  Label = "Pazaak Card -4" },
            new RandomizableItem() { ID = 24117691, Code = "g_i_pazcard_011",  Label = "Pazaak Card -5" },
            new RandomizableItem() { ID = 24117692, Code = "g_i_pazcard_012",  Label = "Pazaak Card -6" },
            new RandomizableItem() { ID = 24117693, Code = "g_i_pazcard_013",  Label = "Pazaak Card +/-1" },
            new RandomizableItem() { ID = 24117694, Code = "g_i_pazcard_014",  Label = "Pazaak Card +/-2" },
            new RandomizableItem() { ID = 24117695, Code = "g_i_pazcard_015",  Label = "Pazaak Card +/-3" },
            new RandomizableItem() { ID = 24117696, Code = "g_i_pazcard_016",  Label = "Pazaak Card +/-4" },
            new RandomizableItem() { ID = 24117697, Code = "g_i_pazcard_017",  Label = "Pazaak Card +/-5" },
            new RandomizableItem() { ID = 24117698, Code = "g_i_pazcard_018",  Label = "Pazaak Card +/-6" },
            new RandomizableItem() { ID = 24117699, Code = "g_i_pazdeck",      Label = "Pazaak Deck" },
            new RandomizableItem() { ID = 24117700, Code = "g_i_pazsidebd001", Label = "Pazaak Side Deck" },
            new RandomizableItem() { ID = 24117701, Code = "g_i_pltuseitm01",  Label = "Keycard, template" },
            new RandomizableItem() { ID = 24117702, Code = "g_i_progspike003", Label = "Repair Spike" },
            new RandomizableItem() { ID = 24117703, Code = "g_i_progspike01",  Label = "Computer Spike" },
            new RandomizableItem() { ID = 24117704, Code = "g_i_progspike02",  Label = "SYSTEM LOADING...COMPLETE...ENTER COMMAND" },
            new RandomizableItem() { ID = 24117705, Code = "g_i_recordrod01",  Label = "Recording Rod" },
            new RandomizableItem() { ID = 24117706, Code = "g_i_secspike01",   Label = "Security Spike" },
            new RandomizableItem() { ID = 24117707, Code = "g_i_secspike02",   Label = "Security Spike Tunneler" },
            new RandomizableItem() { ID = 24117708, Code = "g_i_torch01",      Label = "Torch" },
            new RandomizableItem() { ID = 24117709, Code = "g_i_trapkit001",   Label = "Minor Flash Mine" },
            new RandomizableItem() { ID = 24117710, Code = "g_i_trapkit002",   Label = "Average Flash Mine" },
            new RandomizableItem() { ID = 24117711, Code = "g_i_trapkit003",   Label = "Deadly Flash Mine" },
            new RandomizableItem() { ID = 24117712, Code = "g_i_trapkit004",   Label = "Minor Frag Mine" },
            new RandomizableItem() { ID = 24117713, Code = "g_i_trapkit005",   Label = "Average Frag Mine" },
            new RandomizableItem() { ID = 24117714, Code = "g_i_trapkit006",   Label = "Deadly Frag Mine" },
            new RandomizableItem() { ID = 24117715, Code = "g_i_trapkit007",   Label = "Minor Plasma Mine" },
            new RandomizableItem() { ID = 24117716, Code = "g_i_trapkit008",   Label = "Average Plasma Mine" },
            new RandomizableItem() { ID = 24117717, Code = "g_i_trapkit009",   Label = "Deadly Plasma Mine" },
            new RandomizableItem() { ID = 24117718, Code = "g_i_trapkit01",    Label = "Flash Mine" },      //Turns into Minor once placed.
            new RandomizableItem() { ID = 24117719, Code = "g_i_trapkit010",   Label = "Minor Gas Mine" },
            new RandomizableItem() { ID = 24117720, Code = "g_i_trapkit011",   Label = "Average Gas Mine" },
            new RandomizableItem() { ID = 24117721, Code = "g_i_trapkit012",   Label = "Deadly Gas Mine" },
            new RandomizableItem() { ID = 24117722, Code = "g_i_trapkit02",    Label = "Frag Mine" },       //Turns into Minor once placed.
            new RandomizableItem() { ID = 24117723, Code = "g_i_trapkit03",    Label = "Laser Mine" },      //Turns into Minor Plasma once placed.
            new RandomizableItem() { ID = 24117724, Code = "g_i_trapkit04",    Label = "Gas Mine" },        //Turns into Minor once placed.
            new RandomizableItem() { ID = 24117725, Code = "g_i_upgrade001",   Label = "Scope" },
            new RandomizableItem() { ID = 24117726, Code = "g_i_upgrade002",   Label = "Improved Energy Cell" },
            new RandomizableItem() { ID = 24117727, Code = "g_i_upgrade003",   Label = "Beam Splitter" },
            new RandomizableItem() { ID = 24117728, Code = "g_i_upgrade004",   Label = "Hair Trigger" },
            new RandomizableItem() { ID = 24117729, Code = "g_i_upgrade005",   Label = "Armor Reinforcement" },
            new RandomizableItem() { ID = 24117730, Code = "g_i_upgrade006",   Label = "Mesh Underlay" },
            new RandomizableItem() { ID = 24117731, Code = "g_i_upgrade007",   Label = "Vibration Cell" },
            new RandomizableItem() { ID = 24117732, Code = "g_i_upgrade008",   Label = "Durasteel Bonding Alloy" },
            new RandomizableItem() { ID = 24117733, Code = "g_i_upgrade009",   Label = "Energy Projector" },
            new RandomizableItem() { ID = 24117906, Code = "g_w_adhsvgren001", Label = "Adhesive Grenade" },
            new RandomizableItem() { ID = 24117907, Code = "g_w_blstrcrbn001", Label = "Blaster Carbine" },
            new RandomizableItem() { ID = 24117908, Code = "g_w_blstrcrbn002", Label = "Sith Assault Gun" },
            new RandomizableItem() { ID = 24117909, Code = "g_w_blstrcrbn003", Label = "Cinnagaran Carbine" },
            new RandomizableItem() { ID = 24117910, Code = "g_w_blstrcrbn004", Label = "Jurgan Kalta's Carbine" },
            new RandomizableItem() { ID = 24117911, Code = "g_w_blstrcrbn005", Label = "Jamoh Hogra's Carbine" }, // br,  ug, e3-10, p1-4, 10%ct, +2a
            new RandomizableItem() { ID = 24117912, Code = "g_w_blstrcrbn006", Label = "Jamoh Hogra's Carbine +1" }, // br, !ug, e2-9,  p2,   20%ct, +1a
            new RandomizableItem() { ID = 24117913, Code = "g_w_blstrcrbn007", Label = "Jamoh Hogra's Carbine +2" }, // br, !ug, e2-9,  p4,   20%ct, +1a
            new RandomizableItem() { ID = 24117914, Code = "g_w_blstrcrbn008", Label = "Jamoh Hogra's Carbine +3" }, // br, !ug, e5-12, p4,   20%ct, +4a
            new RandomizableItem() { ID = 24117915, Code = "g_w_blstrcrbn009", Label = "Jamoh Hogra's Carbine +4" }, // br, !ug, e5-12, p4,   20%ct+1-8, +5a
            new RandomizableItem() { ID = 24117916, Code = "g_w_blstrpstl001", Label = "Blaster Pistol" },
            new RandomizableItem() { ID = 24117917, Code = "g_w_blstrpstl002", Label = "Mandalorian Blaster" },
            new RandomizableItem() { ID = 24117918, Code = "g_w_blstrpstl003", Label = "Arkanian Pistol" },
            new RandomizableItem() { ID = 24117919, Code = "g_w_blstrpstl004", Label = "Zabrak Blaster Pistol" },
            new RandomizableItem() { ID = 24117920, Code = "g_w_blstrpstl005", Label = "Bendak's Blaster" }, // bp,  ug, e2-7, 5%ct, bal, +1a
            new RandomizableItem() { ID = 24117921, Code = "g_w_blstrpstl006", Label = "Bendak's Blaster +1" }, // bp, !ug, e3-8, 5%ct, bal, +5a
            new RandomizableItem() { ID = 24117922, Code = "g_w_blstrpstl007", Label = "Bendak's Blaster +2" }, // bp, !ug, e5-10,5%ct, bal, +5a
            new RandomizableItem() { ID = 24117923, Code = "g_w_blstrpstl008", Label = "Bendak's Blaster +3" }, // bp, !ug, e6-11,5%ct, bal, +5a
            new RandomizableItem() { ID = 24117924, Code = "g_w_blstrpstl009", Label = "Bendak's Blaster +4" }, // bp, !ug, e6-11,5%ct+2-12, bal, +5a
            new RandomizableItem() { ID = 24117925, Code = "g_w_blstrpstl010", Label = "Carth's Blaster" },
            new RandomizableItem() { ID = 24117926, Code = "g_w_blstrpstl020", Label = "Insta-kill Pistol" },
            new RandomizableItem() { ID = 24117927, Code = "g_w_blstrrfl001",  Label = "Blaster Rifle" },
            new RandomizableItem() { ID = 24117928, Code = "g_w_blstrrfl002",  Label = "Sith Sniper Rifle" },
            new RandomizableItem() { ID = 24117929, Code = "g_w_blstrrfl003",  Label = "Mandalorian Assault Rifle" },
            new RandomizableItem() { ID = 24117930, Code = "g_w_blstrrfl004",  Label = "Zabrak Battle Cannon" },
            new RandomizableItem() { ID = 24117931, Code = "g_w_blstrrfl005",  Label = "Jurgan Kalta's Assault Rifle" }, // br,  ug, e1d8, i1d6, 10%ct, +3a
            new RandomizableItem() { ID = 24117932, Code = "g_w_blstrrfl006",  Label = "Jurgan Kalta's Assault Rifle +1" }, // br, !ug, e1d8+4, i1d8, 20%ct, +3a
            new RandomizableItem() { ID = 24117933, Code = "g_w_blstrrfl007",  Label = "Jurgan Kalta's Assault Rifle +2" }, // br, !ug, e2d8, i1d8, 20%ct, +4a
            new RandomizableItem() { ID = 24117934, Code = "g_w_blstrrfl008",  Label = "Jurgan Kalta's Assault Rifle +3" }, // br, !ug, e2d8, i1d8, 20%ct, +4a, 50%DC10stun
            new RandomizableItem() { ID = 24117935, Code = "g_w_blstrrfl009",  Label = "Jurgan Kalta's Assault Rifle +4" }, // br, !ug, e2d8, i1d8, 20%ct+1d8, +5a, 50%DC10stun
            new RandomizableItem() { ID = 24117936, Code = "g_w_bowcstr001",   Label = "Bowcaster" },
            new RandomizableItem() { ID = 24117937, Code = "g_w_bowcstr002",   Label = "Chuundar's Bowcaster" },
            new RandomizableItem() { ID = 24117938, Code = "g_w_bowcstr003",   Label = "Zaalbar's Bowcaster" },
            new RandomizableItem() { ID = 24117939, Code = "g_w_crgore001",    Label = "Katarn Slam" },
            new RandomizableItem() { ID = 24117940, Code = "g_w_crgore002",    Label = "Dire Katarn Slam" },
            new RandomizableItem() { ID = 24117941, Code = "g_w_crslash001",   Label = "Claw1d4" },
            new RandomizableItem() { ID = 24117942, Code = "g_w_crslash002",   Label = "Claw1d6" },
            new RandomizableItem() { ID = 24117943, Code = "g_w_crslash003",   Label = "Claw1d10" },
            new RandomizableItem() { ID = 24117944, Code = "g_w_crslash004",   Label = "Claw2d12" },
            new RandomizableItem() { ID = 24117945, Code = "g_w_crslash005",   Label = "Claw3d6" },
            new RandomizableItem() { ID = 24117946, Code = "g_w_crslash006",   Label = "Claw10d6" },
            new RandomizableItem() { ID = 24117947, Code = "g_w_crslash007",   Label = "Selkath Lesser Claw" },
            new RandomizableItem() { ID = 24117948, Code = "g_w_crslash008",   Label = "Selkath Greater Claw" },
            new RandomizableItem() { ID = 24117949, Code = "g_w_crslash009",   Label = "Kinrath Claw" },
            new RandomizableItem() { ID = 24117950, Code = "g_w_crslash010",   Label = "Tach Claw" },
            new RandomizableItem() { ID = 24117951, Code = "g_w_crslash011",   Label = "Kinrath Claw" },
            new RandomizableItem() { ID = 24117952, Code = "g_w_crslash012",   Label = "Kinrath Stalker Claw" },
            new RandomizableItem() { ID = 24117953, Code = "g_w_crslprc001",   Label = "Veerkal Bite" },
            new RandomizableItem() { ID = 24117954, Code = "g_w_crslprc002",   Label = "Tuk'ata Bite" },
            new RandomizableItem() { ID = 24117955, Code = "g_w_crslprc003",   Label = "Gizka Bite" },
            new RandomizableItem() { ID = 24117956, Code = "g_w_crslprc004",   Label = "Dire Tuk'ata Bite" },
            new RandomizableItem() { ID = 24117957, Code = "g_w_crslprc005",   Label = "Shyrack Wyrm Bite" },
            new RandomizableItem() { ID = 24117958, Code = "g_w_cryobgren001", Label = "CryoBan Grenade" },
            new RandomizableItem() { ID = 24117959, Code = "g_w_dblsbr001",    Label = "Double-Bladed Lightsaber, blue" },  // unobtainable
            new RandomizableItem() { ID = 24117960, Code = "g_w_dblsbr002",    Label = "Double-Bladed Lightsaber, red" },
            new RandomizableItem() { ID = 24117961, Code = "g_w_dblsbr003",    Label = "Double-Bladed Lightsaber, green" }, // unobtainable
            new RandomizableItem() { ID = 24117962, Code = "g_w_dblsbr004",    Label = "Double-Bladed Lightsaber, yellow" },
            new RandomizableItem() { ID = 24117963, Code = "g_w_dblsbr005",    Label = "Double-Bladed Lightsaber, purple" },// unobtainable
            new RandomizableItem() { ID = 24117964, Code = "g_w_dblsbr006",    Label = "Bastila's Lightsaber" },
            new RandomizableItem() { ID = 24117965, Code = "g_w_dblswrd001",   Label = "Double-Bladed Sword" },
            new RandomizableItem() { ID = 24117966, Code = "g_w_dblswrd002",   Label = "Echani Ritual Brand" },
            new RandomizableItem() { ID = 24117967, Code = "g_w_dblswrd003",   Label = "Krath Double Sword" },
            new RandomizableItem() { ID = 24117968, Code = "g_w_dblswrd005",   Label = "Ajunta Pall's Blade" },
            new RandomizableItem() { ID = 24117969, Code = "g_w_drkjdisbr001", Label = "Dark Jedi Lightsaber" },    // unobtainable
            new RandomizableItem() { ID = 24117970, Code = "g_w_drkjdisbr002", Label = "Double-Bladed Lightsaber, red, not upgradable" },   // unobtainable
            new RandomizableItem() { ID = 24117971, Code = "g_w_dsrptpstl001", Label = "Disruptor Pistol" },
            new RandomizableItem() { ID = 24117972, Code = "g_w_dsrptpstl002", Label = "Mandalorian Ripper" },
            new RandomizableItem() { ID = 24117973, Code = "g_w_dsrptrfl001",  Label = "Disruptor Rifle" },
            new RandomizableItem() { ID = 24117974, Code = "g_w_dsrptrfl002",  Label = "Zabrak Disruptor Cannon" },
            new RandomizableItem() { ID = 24117975, Code = "g_w_firegren001",  Label = "Plasma Grenade" },
            new RandomizableItem() { ID = 24117976, Code = "g_w_flashgren001", Label = "Plasma Grenade, broken" },  // unobtainable
            new RandomizableItem() { ID = 24117977, Code = "g_w_fraggren01",   Label = "Frag Grenade" },
            new RandomizableItem() { ID = 24117978, Code = "g_w_gaffi001",     Label = "Gaffi Stick" },
            new RandomizableItem() { ID = 24117979, Code = "g_w_hldoblstr01",  Label = "Hold Out Blaster" },
            new RandomizableItem() { ID = 24117980, Code = "g_w_hldoblstr02",  Label = "Bothan Quick Draw" },
            new RandomizableItem() { ID = 24117981, Code = "g_w_hldoblstr03",  Label = "Sith Assassin Pistol" },
            new RandomizableItem() { ID = 24117982, Code = "g_w_hldoblstr04",  Label = "Bothan Needler" },
            new RandomizableItem() { ID = 24117983, Code = "g_w_hvrptbltr002", Label = "Ordo's Repeating Blaster" },
            new RandomizableItem() { ID = 24117984, Code = "g_w_hvrptbltr01",  Label = "Heavy Repeating Blaster" },
            new RandomizableItem() { ID = 24117985, Code = "g_w_hvrptbltr02",  Label = "Mandalorian Heavy Repeater" },
            new RandomizableItem() { ID = 24117986, Code = "g_w_hvyblstr01",   Label = "Heavy Blaster" },
            new RandomizableItem() { ID = 24117987, Code = "g_w_hvyblstr02",   Label = "Arkanian Heavy Pistol" },
            new RandomizableItem() { ID = 24117988, Code = "g_w_hvyblstr03",   Label = "Zabrak Tystel Mark III" },
            new RandomizableItem() { ID = 24117989, Code = "g_w_hvyblstr04",   Label = "Mandalorian Heavy Pistol" },
            new RandomizableItem() { ID = 24117990, Code = "g_w_hvyblstr05",   Label = "Cassus Fett's Heavy Pistol" }, // bp, ug, e1d8+3, 25%stn6sDC10, +3a
            new RandomizableItem() { ID = 24117991, Code = "g_w_hvyblstr06",   Label = "Cassus Fett's Heavy Pistol +1" }, // bp,!ug, e1d8+3, i+4, 25%stn6sDC10, +5a
            new RandomizableItem() { ID = 24117992, Code = "g_w_hvyblstr07",   Label = "Cassus Fett's Heavy Pistol +2" }, // bp,!ug, e1d8+5, i1d8, 25%stn6sDC10, +5a
            new RandomizableItem() { ID = 24117993, Code = "g_w_hvyblstr08",   Label = "Cassus Fett's Heavy Pistol +3" }, // bp,!ug, e1d8+5, i1d8, 25%stn9sDC10, +5a
            new RandomizableItem() { ID = 24117994, Code = "g_w_hvyblstr09",   Label = "Cassus Fett's Heavy Pistol +4" }, // bp,!ug, e1d8+5, i1d8, 25%stn9sDC10, +5a, ct+2d6
            new RandomizableItem() { ID = 24117995, Code = "g_w_ionblstr01",   Label = "Ion Blaster" },
            new RandomizableItem() { ID = 24117996, Code = "g_w_ionblstr02",   Label = "Verpine Prototype Ion Blaster" },
            new RandomizableItem() { ID = 24117997, Code = "g_w_iongren01",    Label = "Ion Grenade" },
            new RandomizableItem() { ID = 24117998, Code = "g_w_ionrfl01",     Label = "Ion Rifle" },
            new RandomizableItem() { ID = 24117999, Code = "g_w_ionrfl02",     Label = "Bothan Droid Disruptor" },
            new RandomizableItem() { ID = 24118000, Code = "g_w_ionrfl03",     Label = "Verpine Droid Disruptor" },
            new RandomizableItem() { ID = 24118001, Code = "g_w_lghtsbr01",    Label = "Lightsaber, blue" },
            new RandomizableItem() { ID = 24118002, Code = "g_w_lghtsbr02",    Label = "Lightsaber, red" },
            new RandomizableItem() { ID = 24118003, Code = "g_w_lghtsbr03",    Label = "Lightsaber, green" },
            new RandomizableItem() { ID = 24118004, Code = "g_w_lghtsbr04",    Label = "Lightsaber, yellow" },  // unobtainable, unless sentinel
            new RandomizableItem() { ID = 24118005, Code = "g_w_lghtsbr05",    Label = "Lightsaber, purple" },
            new RandomizableItem() { ID = 24118006, Code = "g_w_lghtsbr06",    Label = "Malak's Lightsaber" },  // not upgradable, longer saber
            new RandomizableItem() { ID = 24118007, Code = "g_w_lngswrd01",    Label = "Long Sword" },
            new RandomizableItem() { ID = 24118008, Code = "g_w_lngswrd02",    Label = "Krath War Blade" },
            new RandomizableItem() { ID = 24118009, Code = "g_w_lngswrd03",    Label = "Naga Sadow's Poison Blade" },
            new RandomizableItem() { ID = 24118010, Code = "g_w_null001",      Label = "Blaster Pistol: Null" },// unobtainable
            new RandomizableItem() { ID = 24118011, Code = "g_w_null002",      Label = "Heavy Blaster: Null" }, // unobtainable
            new RandomizableItem() { ID = 24118012, Code = "g_w_null003",      Label = "Ion Rifle: Null" },     // unobtainable
            new RandomizableItem() { ID = 24118013, Code = "g_w_null004",      Label = "Light Repeating Blaster: Null" },   // unobtainable
            new RandomizableItem() { ID = 24118014, Code = "g_w_null005",      Label = "Blaster Rifle: Null" }, // unobtainable
            new RandomizableItem() { ID = 24118015, Code = "g_w_null006",      Label = "Blaster Carbine: Null" },   // unobtainable
            new RandomizableItem() { ID = 24118016, Code = "g_w_poisngren01",  Label = "Poison Grenade" },
            new RandomizableItem() { ID = 24118017, Code = "g_w_qtrstaff01",   Label = "Quarterstaff" },
            new RandomizableItem() { ID = 24118018, Code = "g_w_qtrstaff02",   Label = "Massassi Battle Staff" },   // unobtainable
            new RandomizableItem() { ID = 24118019, Code = "g_w_qtrstaff03",   Label = "Raito's Gaderffii" },
            new RandomizableItem() { ID = 24118020, Code = "g_w_rptnblstr01",  Label = "Light Repeating Blaster" },
            new RandomizableItem() { ID = 24118021, Code = "g_w_rptnblstr02",  Label = "Medium Repeating Blaster" },
            new RandomizableItem() { ID = 24118022, Code = "g_w_rptnblstr03",  Label = "Blaster Cannon" },
            new RandomizableItem() { ID = 24118023, Code = "g_w_sbrcrstl01",   Label = "Crystal, Rubat" },
            new RandomizableItem() { ID = 24118024, Code = "g_w_sbrcrstl02",   Label = "Crystal, Damind" },
            new RandomizableItem() { ID = 24118025, Code = "g_w_sbrcrstl03",   Label = "Crystal, Eralam" },
            new RandomizableItem() { ID = 24118026, Code = "g_w_sbrcrstl04",   Label = "Crystal, Sapith" },
            new RandomizableItem() { ID = 24118027, Code = "g_w_sbrcrstl05",   Label = "Crystal, Nextor" },
            new RandomizableItem() { ID = 24118028, Code = "g_w_sbrcrstl06",   Label = "Crystal, Opila" },
            new RandomizableItem() { ID = 24118029, Code = "g_w_sbrcrstl07",   Label = "Crystal, Jenruax" },
            new RandomizableItem() { ID = 24118030, Code = "g_w_sbrcrstl08",   Label = "Crystal, Phond" },
            new RandomizableItem() { ID = 24118031, Code = "g_w_sbrcrstl09",   Label = "Crystal, Luxum" },
            new RandomizableItem() { ID = 24118032, Code = "g_w_sbrcrstl10",   Label = "Crystal, Firkrann" },
            new RandomizableItem() { ID = 24118033, Code = "g_w_sbrcrstl11",   Label = "Crystal, Bondar" },
            new RandomizableItem() { ID = 24118034, Code = "g_w_sbrcrstl12",   Label = "Crystal, Sigil" },
            new RandomizableItem() { ID = 24118035, Code = "g_w_sbrcrstl13",   Label = "Crystal, Upari" },
            new RandomizableItem() { ID = 24118036, Code = "g_w_sbrcrstl14",   Label = "Crystal, Blue" },
            new RandomizableItem() { ID = 24118037, Code = "g_w_sbrcrstl15",   Label = "Crystal, Yellow" },
            new RandomizableItem() { ID = 24118038, Code = "g_w_sbrcrstl16",   Label = "Crystal, Green" },
            new RandomizableItem() { ID = 24118039, Code = "g_w_sbrcrstl17",   Label = "Crystal, Violet" },
            new RandomizableItem() { ID = 24118040, Code = "g_w_sbrcrstl18",   Label = "Crystal, Red" },
            new RandomizableItem() { ID = 24118041, Code = "g_w_sbrcrstl19",   Label = "Crystal, Solari" },
            new RandomizableItem() { ID = 24118042, Code = "g_w_shortsbr01",   Label = "Short Lightsaber, blue" },  // unobtainable
            new RandomizableItem() { ID = 24118043, Code = "g_w_shortsbr02",   Label = "Short Lightsaber, red" },
            new RandomizableItem() { ID = 24118044, Code = "g_w_shortsbr03",   Label = "Short Lightsaber, green" }, // unobtainable
            new RandomizableItem() { ID = 24118045, Code = "g_w_shortsbr04",   Label = "Short Lightsaber, yellow" },// unobtainable
            new RandomizableItem() { ID = 24118046, Code = "g_w_shortsbr05",   Label = "Short Lightsaber, purple" },
            new RandomizableItem() { ID = 24118047, Code = "g_w_shortswrd01",  Label = "Short Sword" },
            new RandomizableItem() { ID = 24118048, Code = "g_w_shortswrd02",  Label = "Massassi Brand" },  // unobtainable
            new RandomizableItem() { ID = 24118049, Code = "g_w_shortswrd03",  Label = "Teta's Blade" },    // unobtainable
            new RandomizableItem() { ID = 24118050, Code = "g_w_sonicgren01",  Label = "Sonic Grenade" },
            new RandomizableItem() { ID = 24118051, Code = "g_w_sonicpstl01",  Label = "Sonic Pistol" },
            new RandomizableItem() { ID = 24118052, Code = "g_w_sonicpstl02",  Label = "Bothan Shrieker" },
            new RandomizableItem() { ID = 24118053, Code = "g_w_sonicrfl01",   Label = "Sonic Rifle" },
            new RandomizableItem() { ID = 24118054, Code = "g_w_sonicrfl02",   Label = "Bothan Discord Gun" },
            new RandomizableItem() { ID = 24118055, Code = "g_w_sonicrfl03",   Label = "Arkanian Sonic Rifle" },// unobtainable
            new RandomizableItem() { ID = 24118056, Code = "g_w_stunbaton01",  Label = "Stun Baton" },
            new RandomizableItem() { ID = 24118057, Code = "g_w_stunbaton02",  Label = "Bothan Stun Stick" },
            new RandomizableItem() { ID = 24118058, Code = "g_w_stunbaton03",  Label = "Bothan Chuka" },
            new RandomizableItem() { ID = 24118059, Code = "g_w_stunbaton04",  Label = "Rakatan Battle Wand" },//p3, 5%ct, 50%stn06sDC14, a+2, ug
            new RandomizableItem() { ID = 24118060, Code = "g_w_stunbaton05",  Label = "Rakatan Battle Wand +1" },//p4, 5%ct,100%stn12sDC10, a+3,!ug
            new RandomizableItem() { ID = 24118061, Code = "g_w_stunbaton06",  Label = "Rakatan Battle Wand +2" },//p5, 5%ct,100%stn12sDC10, a+4,!ug
            new RandomizableItem() { ID = 24118062, Code = "g_w_stunbaton07",  Label = "Rakatan Battle Wand +3" },//p5, 5%ct,100%stn12sDC14, a+4,!ug, i1d10
            new RandomizableItem() { ID = 24118063, Code = "g_w_stungren01",   Label = "Concussion Grenade" },
            new RandomizableItem() { ID = 24118064, Code = "g_w_thermldet01",  Label = "Thermal Detonator" },
            new RandomizableItem() { ID = 24118065, Code = "g_w_vbrdblswd01",  Label = "Vibro Double-Blade" },
            new RandomizableItem() { ID = 24118066, Code = "g_w_vbrdblswd02",  Label = "Sith War Sword" },  // Unobtainable
            new RandomizableItem() { ID = 24118067, Code = "g_w_vbrdblswd03",  Label = "Echani Double-Brand" },
            new RandomizableItem() { ID = 24118068, Code = "g_w_vbrdblswd04",  Label = "Yusanis' Brand" },//p2d8+2, i+5, 5%ct, a+2, ug
            new RandomizableItem() { ID = 24118069, Code = "g_w_vbrdblswd05",  Label = "Yusanis' Brand +1" },//p2d8+3, i+5, 5%ct, a+3,!ug
            new RandomizableItem() { ID = 24118070, Code = "g_w_vbrdblswd06",  Label = "Yusanis' Brand +2" },//p2d8+3, i+5, e+3, 5%ct, a+3,!ug
            new RandomizableItem() { ID = 24118071, Code = "g_w_vbrdblswd07",  Label = "Yusanis' Brand +3" },//p2d8+3, i+5, e+3, f1d4, 10%ct, a+3,!ug
            new RandomizableItem() { ID = 24118072, Code = "g_w_vbroshort01",  Label = "Vibroblade" },
            new RandomizableItem() { ID = 24118073, Code = "g_w_vbroshort02",  Label = "Krath Blood Blade" },
            new RandomizableItem() { ID = 24118074, Code = "g_w_vbroshort03",  Label = "Echani Vibroblade" },
            new RandomizableItem() { ID = 24118075, Code = "g_w_vbroshort04",  Label = "Sanasiki's Blade" },//p1d10+2, i+3, 10%ct, a+2, ug
            new RandomizableItem() { ID = 24118076, Code = "g_w_vbroshort05",  Label = "Sanasiki's Blade +1" },//p1d10+4, i+5, 20%ct, a+4,!ug
            new RandomizableItem() { ID = 24118077, Code = "g_w_vbroshort06",  Label = "Sanasiki's Blade +2" },//p1d10+7, i+5, 20%ct, a+4,!ug
            new RandomizableItem() { ID = 24118078, Code = "g_w_vbroshort07",  Label = "Sanasiki's Blade +3" },//p1d10+7, i+5, e1d4, 20%ct, a+4,!ug
            new RandomizableItem() { ID = 24118079, Code = "g_w_vbroshort08",  Label = "Mission's Vibroblade" },
            new RandomizableItem() { ID = 24118080, Code = "g_w_vbroshort09",  Label = "Prototype Vibroblade" },
            new RandomizableItem() { ID = 24118081, Code = "g_w_vbroswrd01",   Label = "Vibrosword" },
            new RandomizableItem() { ID = 24118082, Code = "g_w_vbroswrd02",   Label = "Krath Dire Sword" },
            new RandomizableItem() { ID = 24118083, Code = "g_w_vbroswrd03",   Label = "Sith Tremor Sword" },
            new RandomizableItem() { ID = 24118084, Code = "g_w_vbroswrd04",   Label = "Echani Foil" },
            new RandomizableItem() { ID = 24118085, Code = "g_w_vbroswrd05",   Label = "Bacca's Ceremonial Blade" },//p2d6+2, e+4 , 10%ct, a+2, ug
            new RandomizableItem() { ID = 24118086, Code = "g_w_vbroswrd06",   Label = "Bacca's Ceremonial Blade +1" },//p2d6  , e1d6, 20%ct, a+5,!ug
            new RandomizableItem() { ID = 24118087, Code = "g_w_vbroswrd07",   Label = "Bacca's Ceremonial Blade +2" },//p2d6  , e1d8, 20%ct, a+5,!ug
            new RandomizableItem() { ID = 24118088, Code = "g_w_vbroswrd08",   Label = "Bacca's Ceremonial Blade +3" },//p2d6  , e2d6, 20%ct, a+5,!ug
            new RandomizableItem() { ID = 24118089, Code = "g_w_waraxe001",    Label = "Gamorrean Battleaxe" }, // Unobtainable
            new RandomizableItem() { ID = 24118090, Code = "g_w_warblade001",  Label = "Wookiee Warblade" },
            new RandomizableItem() { ID = 24118108, Code = "geno_armor",       Label = "GenoHaradan Mesh Armor" },
            new RandomizableItem() { ID = 24118109, Code = "geno_blade",       Label = "GenoHaradan Poison Blade" },
            new RandomizableItem() { ID = 24118110, Code = "geno_blaster",     Label = "GenoHaradan Blaster" },
            new RandomizableItem() { ID = 24118111, Code = "geno_datapad",     Label = "GenoHaradan Datapad" },
            new RandomizableItem() { ID = 24118112, Code = "geno_gloves",      Label = "GenoHaradan Power Gloves" },
            new RandomizableItem() { ID = 24118113, Code = "geno_stealth",     Label = "GenoHaradan Stealth Unit" },
            new RandomizableItem() { ID = 24118114, Code = "geno_visor",       Label = "GenoHaradan Visor" },
            new RandomizableItem() { ID = 24118156, Code = "kas25_wookcrysta", Label = "Rough-cut Upari Amulet" },
            new RandomizableItem() { ID = 24118555, Code = "ptar_rakghoulser", Label = "Rakghoul Serum" },
            new RandomizableItem() { ID = 24118556, Code = "ptar_sbpasscrd",   Label = "Sith Base Passcard" },
            new RandomizableItem() { ID = 24118557, Code = "ptar_sitharmor",   Label = "Sith Armor" },
            new RandomizableItem() { ID = 24118620, Code = "tat17_sandperdis", Label = "Sand People Clothing" },
            new RandomizableItem() { ID = 24118621, Code = "tat18_dragonprl",  Label = "Krayt Dragon Pearl" },
            new RandomizableItem() { ID = 24118625, Code = "w_blhvy001",       Label = "Heavy Blaster Pistol, broken" },//+1 attack, melee
            new RandomizableItem() { ID = 24118626, Code = "w_bstrcrbn",       Label = "Clothing, weapons +1" },//+1 attack and damage
            new RandomizableItem() { ID = 24118627, Code = "w_lghtsbr001",     Label = "Lightsaber, broken" },//+1 attack, melee
            new RandomizableItem() { ID = 24118628, Code = "w_null",           Label = "Null Item" },//not usable
        };

        /// <summary>
        /// All modules in the game
        /// </summary>
        public static readonly List<string> MODULES = new List<string>() {
            "danm13","danm14aa","danm14ab","danm14ac","danm14ad","danm14ae",            // Jedi Enclave, Courtyard, Matale Grounds, Grove, Sandral Grounds, Crystal Caves,
            "danm15","danm16","ebo_m12aa","ebo_m40aa","ebo_m40ad","ebo_m41aa",          // Dantooine Ruins, Sandral Estate, Ebon Hawk, Leviathan Game-Plan CS, Escaped the Leviathan, Lehon Hawk,
            "ebo_m46ab","end_m01aa","end_m01ab","kas_m22aa","kas_m22ab","kas_m23aa",    // Mystery Box, Command Module, Starboard Section, Czerka Landing Port, Great Walkway, Village of Rwookrrorro,
            "kas_m23ab","kas_m23ac","kas_m23ad","kas_m24aa","kas_m25aa","korr_m33aa",   // Woorwill's Home, Worrroznor's Home, Chieftain's Hall, Upper Shadowlands, Lower Shadowlands, Dreshdae,
            "korr_m33ab","korr_m34aa","korr_m35aa","korr_m36aa","korr_m37aa",           // Sith Academy Entrance, Shyrack Caves, Sith Academy, Valley of the Dark Lords, Tomb of Ajunta Pall,
            "korr_m38aa","korr_m38ab","korr_m39aa","lev_m40aa","lev_m40ab",             // Tomb of Marka Ragnos, Tomb of Tulak Hord, Tomb of Naga Sadow, Prison Block, Command Deck,
            "lev_m40ac","lev_m40ad","liv_m99aa","M12ab","manm26aa","manm26ab",          // Leviathan Hangar, Leviathan Bridge, Yavin Station, Fighter Skirmish, Ahto West, Ahto East,
            "manm26ac","manm26ad","manm26ae","manm26mg","manm27aa","manm28aa",          // West Central, Manaan Docking Bay, East Central, Manaan Swoops, Manaan Sith Base, Hrakert Station,
            "manm28ab","manm28ac","manm28ad","sta_m45aa","sta_m45ab","sta_m45ac",       // Sea Floor, Kolto Control, Hrakert Rift, Deck 1, Deck 2, Command Center,
            "sta_m45ad","STUNT_00","STUNT_03a","STUNT_06","STUNT_07","STUNT_12",        // Viewing Platform, Dream Sequence, Taris Destruction Orders, Taris Destruction Update, Taris Escape, Calo Nord Lives,
            "STUNT_14","STUNT_16","STUNT_18","STUNT_19","STUNT_31b","STUNT_34",         // Darth Bandon Recruited, My Old Mentor, Bastila Tortured, Jaw Drop, Revan Reveal, Star Forge Arrival,
            "STUNT_35","STUNT_42","STUNT_44","STUNT_50a","STUNT_51a","STUNT_54a",       // Lehon Distruptor Field, LS Pre-Star Forge CS, DS Pre-Star Forge CS, Green Squadron, Bastila Destroys Fleet, Republic Doomed,
            "STUNT_55a","STUNT_56a","STUNT_57","tar_m02aa","tar_m02ab","tar_m02ac",     // DS Credits, Star Forge Destroyed, LS Credits, South Apartments, Upper City North, Upper City South,
            "tar_m02ad","tar_m02ae","tar_m02af","tar_m03aa","tar_m03ab","tar_m03ad",    // North Apartments, Upper City Cantina, Hideout, Lower City, Lower City Apt West, Lower City Apt East,
            "tar_m03ae","tar_m03af","tar_m03mg","tar_m04aa","tar_m05aa","tar_m05ab",    // Javyar's Cantina, Swoop Platform, Taris Swoops, Undercity, Lower Sewers, Upper Sewers,
            "tar_m08aa","tar_m09aa","tar_m09ab","tar_m10aa","tar_m10ab","tar_m10ac",    // Davik's Estate, Taris Sith Base, Governor Office, Vulkar Base, Vulkar Spice Lab, Vulkar Garage,
            "tar_m11aa","tar_m11ab","tat_m17aa","tat_m17ab","tat_m17ac","tat_m17ad",    // Bek Base, Gadon's Office, Anchorhead, Tatooine Docking Bay, Droid Shop, Hunting Lodge,
            "tat_m17ae","tat_m17af","tat_m17ag","tat_m17mg","tat_m18aa","tat_m18ab",    // Swoop Registration, Tatooine Cantina, Czerka Office, Tatooine Swoops, Dune Sea, Sand People Territory,
            "tat_m18ac","tat_m20aa","unk_m41aa","unk_m41ab","unk_m41ac","unk_m41ad",    // Eastern Dune Sea, Sand People Enclave, Central Beach, South Beach, North Beach, Temple Exterior,
            "unk_m42aa","unk_m43aa","unk_m44aa","unk_m44ab","unk_m44ac" };              // Elder Settlement, Rakatan Settlement, Temple Main Floor, Temple Catacombs, Temple Summit

        /// <summary>
        /// If a module has one exit, it cannot be in its parent's location.
        /// Prevents binary infinite loops that you can't escape from.
        /// <para/>Key cannot replace any listed value.
        /// </summary>
        public static readonly Dictionary<string, List<string>> RULE1 = new Dictionary<string, List<string>>()
        {
            // Tatooine
            { "tat_m17ag", new List<string>()    // Czerka Office
                {
                    "tat_m17aa", // Anchorhead
                }
            },
            { "tat_m17ac", new List<string>()    // Droid Shop
                {
                    "tat_m17aa", // Anchorhead
                }
            },
            { "tat_m17ad", new List<string>()    // Hunting Lodge
                {
                    "tat_m17aa", // Anchorhead
                }
            },
            { "tat_m17af", new List<string>()    // Tatooine Cantina
                {
                    "tat_m17aa", // Anchorhead
                }
            },
            { "tat_m17mg", new List<string>()    // Tatooine Swoops
                {
                    "tat_m17ae", // Swoop Registration
                }
            },
            { "tat_m20aa", new List<string>()    // Sand People Enclave
                {
                    "tat_m18ab", // Sand People Territory
                }
            },
            // Unknown World
            { "unk_m42aa", new List<string>()    // Elder Settlement
                {
                    "unk_m41ab", // South Beach
                }
            },
            { "unk_m43aa", new List<string>()    // Rakatan Settlement
                {
                    "unk_m41ac", // North Beach
                }
            },
            { "unk_m44ab", new List<string>()    // Temple Catacombs
                {
                    "unk_m44aa", // Temple Main Floor
                }
            },
            { "unk_m44ac", new List<string>()    // Temple Summit
                {
                    "unk_m44aa", // Temple Main Floor
                }
            },
            // Taris
            { "tar_m11ab", new List<string>()    // Gadon's Office
                {
                    "tar_m11aa", // Bek Base
                }
            },
            { "tar_m08aa", new List<string>()    // Davik's Estate
                {
                    "danm13", // Jedi Enclave
                }
            },
            { "tar_m09ab", new List<string>()    // Governor Office
                {
                    "tar_m09aa", // Taris Sith Base
                }
            },
            { "tar_m02af", new List<string>()    // Hideout
                {
                    "tar_m02aa", // South Apartments
                }
            },
            { "tar_m03ad", new List<string>()    // Lower City Apt East
                {
                    "tar_m03aa", // Lower City
                }
            },
            { "tar_m03ab", new List<string>()    // Lower City Apt West
                {
                    "tar_m03aa", // Lower City
                }
            },
            { "tar_m02ad", new List<string>()    // North Apartments
                {
                    "tar_m02ab", // Upper City North
                }
            },
            { "tar_m03mg", new List<string>()    // Taris Swoops
                {
                    "tar_m03af", // Swoop Platform
                }
            },
            { "tar_m10ac", new List<string>()    // Vulkar Garage
                {
                    "tar_m10aa", // Vulkar Base
                }
            },
            { "tar_m10ab", new List<string>()    // Vulkar Spice Lab
                {
                    "tar_m10aa", // Vulkar Base
                }
            },
            // Star Forge
            { "sta_m45ad", new List<string>()    // Viewing Platform
                {
                    "sta_m45ac", // Command Center
                }
            },
            // Leviathan
            { "lev_m40ad", new List<string>()    // Leviathan Bridge
                {
                    "lev_m40ab", // Command Deck
                }
            },
            { "lev_m40ac", new List<string>()    // Leviathan Hangar
                {
                    "ebo_m12aa", // Ebon Hawk
                }
            },
            // Kashyyyk
            { "kas_m23ad", new List<string>()    // Chieftain's Hall
                {
                    "kas_m23aa", // Village of Rwookrrorro
                }
            },
            { "kas_m25aa", new List<string>()    // Lower Shadowlands
                {
                    "kas_m24aa", // Upper Shadowlands
                }
            },
            { "kas_m23ab", new List<string>()    // Woorwill's Home
                {
                    "kas_m23aa", // Village of Rwookrrorro
                }
            },
            { "kas_m23ac", new List<string>()    // Worrroznor's Home
                {
                    "kas_m23aa", // Village of Rwookrrorro
                }
            },
            // Korriban
            { "korr_m34aa", new List<string>()    // Shyrack Caves
                {
                    "korr_m36aa", // Valley of the Dark Lords
                }
            },
            { "korr_m37aa", new List<string>()    // Tomb of Ajunta Pall
                {
                    "korr_m36aa", // Valley of the Dark Lords
                }
            },
            { "korr_m38aa", new List<string>()    // Tomb of Marka Ragnos
                {
                    "korr_m36aa", // Valley of the Dark Lords
                }
            },
            { "korr_m39aa", new List<string>()    // Tomb of Naga Sadow
                {
                    "korr_m36aa", // Valley of the Dark Lords
                }
            },
            { "korr_m38ab", new List<string>()    // Tomb of Tulak Hord
                {
                    "korr_m36aa", // Valley of the Dark Lords
                }
            },
            // Endar Spire
            { "end_m01aa", new List<string>()    // Command Module
                {
                    "end_m01ab", // Starboard Section
                }
            },
            { "end_m01ab", new List<string>()    // Starboard Section
                {
                    "STUNT_00",  // leads to dream sequence
                    "tar_m02af", // Hideout
                }
            },
            // Dantooine
            { "danm14ae", new List<string>()    // Crystal Caves
                {
                    "danm14ad", // Sandral Grounds
                }
            },
            { "danm15", new List<string>()    // Dantooine Ruins
                {
                    "danm14aa", // Courtyard
                }
            },
            { "danm16", new List<string>()    // Sandral Estate
                {
                    "danm14ad", // Sandral Grounds
                }
            },
            // Manaan
            { "manm26aa", new List<string>()    // Ahto West
                {
                    "manm26ac", // West Central
                }
            },
            { "manm27aa", new List<string>()    // Manaan Sith Base
                {
                    "manm26ab", // Ahto East
                }
            },
            { "manm26mg", new List<string>()    // Manaan Swoops
                {
                    "manm26ab", // Ahto East
                }
            },
            // Ebon Hawk
            { "ebo_m41aa", new List<string>()    // Lehon Hawk
                {
                    "unk_m41aa", // Central Beach
                }
            },
            { "ebo_m46ab", new List<string>()    // Mystery Box
                {
                    "ebo_m12aa", // Ebon Hawk
                }
            },
            { "liv_m99aa", new List<string>()    // Yavin Station
                {
                    "ebo_m12aa", // Ebon Hawk
                }
            },
        };
        /// <summary>
        /// The parent of a module with only one entrance cannot be inside that child.
        /// Prevents some modules from becoming completely unreachable.
        /// <para/>Key cannot replace any listed value.
        /// </summary>
        public static readonly Dictionary<string, List<string>> RULE2 = new Dictionary<string, List<string>>()
        {
            // Tatooine
            { "tat_m17aa", new List<string>()    // Anchorhead
                {
                    "tat_m17ac", // Droid Shop
                    "tat_m17ad", // Hunting Lodge
                    "tat_m17af", // Tatooine Cantina
                    "tat_m17ag", // Czerka Office
                }
            },
            { "tat_m18ab", new List<string>()    // Sand People Territory
                {
                    "tat_m20aa", // Sand People Enclave
                }
            },
            { "tat_m17ae", new List<string>()    // Swoop Registration
                {
                    "tat_m17mg", // Tatooine Swoops
                }
            },
            // Unknown World
            { "unk_m41aa", new List<string>()    // Central Beach
                {
                    "ebo_m41aa", // Lehon Hawk
                }
            },
            { "unk_m41ab", new List<string>()    // South Beach
                {
                    "unk_m42aa", // Elder Settlement
                }
            },
            { "unk_m41ac", new List<string>()    // North Beach
                {
                    "unk_m43aa", // Rakatan Settlement
                }
            },
            { "unk_m44aa", new List<string>()    // Temple Main Floor
                {
                    "unk_m44ab", // Temple Catacombs
                    "unk_m44ac", // Temple Summit
                }
            },
            // Taris
            { "tar_m11aa", new List<string>()    // Bek Base
                {
                    "tar_m11ab", // Gadon's Office
                }
            },
            { "tar_m03ae", new List<string>()    // Javyar's Cantina
                {
                    "tar_m08aa", // Davik's Estate
                }
            },
            { "tar_m03aa", new List<string>()    // Lower City
                {
                    "tar_m03ae", // Javyar's Cantina
                    "tar_m03ad", // Lower City Apt East
                    "tar_m03ab", // Lower City Apt West
                }
            },
            { "tar_m02aa", new List<string>()    // South Apartments
                {
                    "tar_m02af", // Hideout
                }
            },
            { "tar_m03af", new List<string>()    // Swoop Platform
                {
                    "tar_m03mg", // Taris Swoops
                }
            },
            { "tar_m09aa", new List<string>()    // Taris Sith Base
                {
                    "tar_m09ab", // Governor's Office
                }
            },
            { "tar_m02ab", new List<string>()    // Upper City North
                {
                    "tar_m02ad", // North Apartments
                }
            },
            { "tar_m02ac", new List<string>()    // Upper City South
                {
                    "tar_m02ae", // Upper City Cantina
                }
            },
            { "tar_m10aa", new List<string>()    // Vulkar Base
                {
                    "tar_m10ac", // Vulkar Garage
                    "tar_m10ab", // Vulkar Spice Lab
                    "tar_m05ab", // Upper Sewers (?? -- the only other entrance is locked)
                }
            },
            // Star Forge
            { "sta_m45ac", new List<string>()    // Command Center
                {
                    "sta_m45ad", // Viewing Platform
                }
            },
            // Leviathan
            { "lev_m40ab", new List<string>()    // Command Deck
                {
                    "lev_m40ad", // Leviathan Bridge
                    "lev_m40aa", // Prison Block
                }
            },
            // Kashyyyk
            { "kas_m24aa", new List<string>()    // Upper Shadowlands
                {
                    "kas_m25aa", // Lower Shadowlands
                }
            },
            { "kas_m23aa", new List<string>()    // Village of Rwookrrorro
                {
                    "kas_m23ab", // Woorwill's Home
                    "kas_m23ac", // Worrroznor's Home
                    "kas_m23ad", // Chieftain's Hall
                }
            },
            // Korriban
            { "korr_m36aa", new List<string>()    // Valley of the Dark Lords
                {
                    "korr_m37aa", // Tomb of Ajunta Pall
                    "korr_m38aa", // Tomb of Marka Ragnos
                    "korr_m39aa", // Tomb of Naga Sadow
                    "korr_m38ab", // Tomb of Tulka Hord
                    "korr_m34aa", // Shyrack Caves
                    "korr_m35aa", // Sith Academy (?? - the other two entrances are locked and/or once)
                }
            },
            // Endar Spire
            { "end_m01ab", new List<string>()    // Starboard Section
                {
                    "end_m01aa", // Command Module
                }
            },
            // Dantooine
            { "danm14aa", new List<string>()    // Courtyard
                {
                    "danm15", // Dantooine Ruins
                }
            },
            { "danm14ad", new List<string>()    // Sandral Grounds
                {
                    "danm16", // Sandral Estate
                    "danm14ae", // Crystal Caves
                }
            },
            // Manaan
            { "manm26ab", new List<string>()    // Ahto East
                {
                    "manm26mg", // Manaan Swoops
                }
            },
            //{ "manm28ad", new List<string>()    // Hrakert Rift
            //    {
            //        "manm28ac", // Kolto Control (?? - not sure why)
            //    }
            //},
            { "manm26ac", new List<string>()    // West Central
                {
                    "manm26aa", // Ahto West
                }
            },
            // Ebon Hawk
            { "ebo_m12aa", new List<string>()    // Ebon Hawk
                {
                    "ebo_m46ab", // Mystery Box
                    "lev_m40ac", // Leviathan Hangar
                    "liv_m99aa", // Yavin Station
                }
            },
        };
        /// <summary>
        /// If a module has multiple exits but only one is unlocked, it cannot be in the unlocked location.
        /// Prevents binary infinite loops that you can't escape from, unless the other door is unlocked.
        /// <para/>Key cannot replace any listed value.
        /// </summary>
        public static readonly Dictionary<string, List<string>> RULE3 = new Dictionary<string, List<string>>()
        {
            // Taris
            { "tar_m03ae", new List<string>()    // Javyar's Cantina
                {
                    "tar_m03aa", // Lower City
                }
            },
            { "tar_m05aa", new List<string>()    // Lower Sewers
                {
                    "tar_m04aa", // Undercity
                }
            },
            { "tar_m02ae", new List<string>()    // Upper City Cantina
                {
                    "tar_m02ac", // Upper City South
                }
            },
            // Leviathan
            { "lev_m40aa", new List<string>()    // Prison Block
                {
                    "lev_m40ab", // Command Deck
                }
            },
            // Korriban
            { "korr_m33ab", new List<string>()    // Sith Academy Entrance
                {
                    "korr_m33aa", // Dreshdae
                }
            },
        };

        /// <summary>
        /// Built-in module omission presets. The key being the preset name, and the value being a string list of modules to omit. Not to be confused with user-defined presets.
        /// </summary>
        public static readonly Dictionary<string, List<string>> OMIT_PRESETS = new Dictionary<string, List<string>>()
        {
            //{ "<Custom>", null },
            { "Off", new List<string>()
                {
                "danm13","danm14aa","danm14ab","danm14ac","danm14ad","danm14ae","danm15","danm16",
                "ebo_m12aa","ebo_m40aa","ebo_m40ad","ebo_m41aa","ebo_m46ab",
                "end_m01aa","end_m01ab",
                "kas_m22aa","kas_m22ab","kas_m23aa","kas_m23ab","kas_m23ac","kas_m23ad","kas_m24aa","kas_m25aa",
                "korr_m33aa","korr_m33ab","korr_m34aa","korr_m35aa","korr_m36aa","korr_m37aa","korr_m38aa","korr_m38ab","korr_m39aa",
                "lev_m40aa","lev_m40ab","lev_m40ac","lev_m40ad",
                "liv_m99aa",
                "M12ab",
                "manm26aa","manm26ab","manm26ac","manm26ad","manm26ae","manm26mg","manm27aa","manm28aa","manm28ab","manm28ac","manm28ad",
                "sta_m45aa","sta_m45ab","sta_m45ac","sta_m45ad",
                "STUNT_00","STUNT_03a","STUNT_06","STUNT_07","STUNT_12","STUNT_14","STUNT_16","STUNT_18","STUNT_19","STUNT_31b","STUNT_34","STUNT_35","STUNT_42",
                "STUNT_44","STUNT_50a","STUNT_51a","STUNT_54a","STUNT_55a","STUNT_56a","STUNT_57",
                "tar_m02aa","tar_m02ab","tar_m02ac","tar_m02ad","tar_m02ae","tar_m02af","tar_m03aa","tar_m03ab","tar_m03ad","tar_m03ae","tar_m03af","tar_m03mg",
                "tar_m04aa","tar_m05aa","tar_m05ab","tar_m08aa","tar_m09aa","tar_m09ab","tar_m10aa","tar_m10ab","tar_m10ac","tar_m11aa","tar_m11ab",
                "tat_m17aa","tat_m17ab","tat_m17ac","tat_m17ad","tat_m17ae","tat_m17af","tat_m17ag","tat_m17mg","tat_m18aa","tat_m18ab","tat_m18ac","tat_m20aa",
                "unk_m41aa","unk_m41ab","unk_m41ac","unk_m41ad","unk_m42aa","unk_m43aa","unk_m44aa","unk_m44ab","unk_m44ac"
                }
            },
            { "Default", new List<string>()
                {
                "M12ab", "end_m01aa", "end_m01ab", "ebo_m40aa", "ebo_m12aa",
                "ebo_m40ad", "STUNT_00", "STUNT_03a", "STUNT_06", "STUNT_07",
                "STUNT_12", "STUNT_14", "STUNT_16", "STUNT_18", "STUNT_19",
                "STUNT_31b", "STUNT_34", "STUNT_35", "STUNT_42", "STUNT_44",
                "STUNT_50a", "STUNT_51a", "STUNT_54a", "STUNT_55a", "STUNT_56a",
                "STUNT_57"
                }
            },
            { "No Major Hubs", new List<string>()
                {
                "tar_m10ab", "ebo_m46ab", "liv_m99aa", "unk_m44ac", "manm26mg",
                "tar_m03mg", "tat_m17mg", "unk_m43aa", "tar_m03aa", "tat_m17aa",
                "korr_m36aa", "M12ab", "end_m01aa", "end_m01ab", "ebo_m40aa",
                "ebo_m12aa", "ebo_m40ad", "STUNT_00", "STUNT_03a", "STUNT_06",
                "STUNT_07", "STUNT_12", "STUNT_14", "STUNT_16", "STUNT_18",
                "STUNT_19", "STUNT_31b", "STUNT_34", "STUNT_35", "STUNT_42",
                "STUNT_44", "STUNT_50a", "STUNT_51a", "STUNT_54a", "STUNT_55a",
                "STUNT_56a", "STUNT_57"
                }
            },
            { "All Modules", new List<string>() { } }
        };

        /// <summary>
        /// Built-in item omission presets. The key is the preset name, and the value is a string list of items to omit.
        /// </summary>
        public static readonly Dictionary<string, List<string>> OMIT_ITEM_PRESETS = new Dictionary<string, List<string>>()
        {
            { "Default (Omit Broken/Plot)", new List<string>()
                {
                    "g_i_collarlgt001",     // Collar Light (broken item)
                    "g_i_drdutldev005",     // Oil Slick (equippable, but unusable and unobtainable)
                    "g_i_glowrod01",        // Glow Rod
                    "g_i_implant104",       // Stamina Boost Implant
                    "g_i_implant204",       // I need to make the party selections screen available
                    "g_i_progspike02",      // Single-Use Programming Spikes
                    "g_i_torch01",          // Torch (broken item)
                    "g_w_flashgren001",     // Plasma Grenade, broken
                    "ptar_rakghoulser",     // Rakghoul Serum (plot)
                    "ptar_sitharmor",       // Sith Armor
                    "tat17_sandperdis",     // Sand People Disguise
                    "w_blhvy001",           // Hvy Blaster Pistol, broken
                    "w_lghtsbr001",         // Lightsaber, broken
                    "w_null",               // Unusable null item
                }
            },
            { "Default + Omit Duplicates", new List<string>()   // Default + More
                {
                    // Default
                    "g_i_collarlgt001", "g_i_drdutldev005", "g_i_glowrod01",
                    "g_i_implant104", "g_i_implant204", "g_i_progspike02",
                    "g_i_torch01", "g_w_flashgren001", "ptar_rakghoulser",
                    "ptar_sitharmor", "tat17_sandperdis", "w_blhvy001",
                    "w_lghtsbr001", "w_null",
                    // Duplicates
                    "g_a_class4007", "g_a_class4008",   // Bandon's Fiber Armor
                    "g_a_class5008", "g_a_class5009",   // Eriadu Prototype Armor
                    "g_a_class6008", "g_a_class6009",   // Davik's War Suit
                    "g_a_class8006", "g_a_class8007",   // Calo Nord's Battle Armor
                    "g_a_class9006", "g_a_class9007",   // Jurgan Kalta's Power Suit
                    "g_a_class9011",    // Cassus Fett's Battle Armor
                    "g_a_clothes02", "g_a_clothes03", "g_a_clothes04", "g_a_clothes05",
                    "g_a_clothes06", "g_a_clothes07", "g_a_clothes08", "g_a_clothes09", // Clothing Variants
                    "g_i_crhide004", "g_i_crhide006",   // Tuk'ata Hide
                    "g_w_blstrcrbn006", "g_w_blstrcrbn007", "g_w_blstrcrbn008", "g_w_blstrcrbn009", // Jamoh Hogra's Carbine
                    "g_w_blstrpstl006", "g_w_blstrpstl007", "g_w_blstrpstl008", "g_w_blstrpstl009", // Bendak's Blaster
                    "g_w_blstrrfl006",  "g_w_blstrrfl007",  "g_w_blstrrfl008",  "g_w_blstrrfl009",  // Jurgan Kalta's Assault Rifle
                    "g_w_hvyblstr06",   "g_w_hvyblstr07",   "g_w_hvyblstr08",   "g_w_hvyblstr09",   // Cassus Fett's Heavy Pistol
                    "g_w_null001", "g_w_null002", "g_w_null003", "g_w_null004", "g_w_null005", "g_w_null006",   // Null weapons
                    "g_w_stunbaton05", "g_w_stunbaton06", "g_w_stunbaton07",    // Rakatan Battle Wand
                    "g_w_vbrdblswd05", "g_w_vbrdblswd06", "g_w_vbrdblswd07",    // Yusanis' Brand
                    "g_w_vbroshort05", "g_w_vbroshort06", "g_w_vbroshort07",    // Sanasiki's Blade
                    "g_w_vbroswrd06",  "g_w_vbroswrd07",  "g_w_vbroswrd08",     // Bacca's Ceremonial Blade
                    "g_i_frarmbnds10", "g_i_frarmbnds11", "g_i_frarmbnds12",    // Lower Saves, All
                    "g_i_frarmbnds13", "g_i_frarmbnds14", "g_i_frarmbnds15",    // Lower Saves, Fortitude
                    "g_i_frarmbnds16", "g_i_frarmbnds17", "g_i_frarmbnds18",    // Lower Saves, Reflex
                    "g_i_frarmbnds19", "g_i_frarmbnds20", "g_i_frarmbnds21",    // Loser Saves, Will
                }
            },
            { "Default + Omit Inaccessible", new List<string>()   // Default + Omit Duplicates + More
                {
                    // Default
                    "g_i_collarlgt001", "g_i_drdutldev005", "g_i_glowrod01",
                    "g_i_implant104", "g_i_implant204", "g_i_progspike02",
                    "g_i_torch01", "g_w_flashgren001", "ptar_rakghoulser",
                    "ptar_sitharmor", "tat17_sandperdis", "w_blhvy001",
                    "w_lghtsbr001", "w_null",
                    // Duplicates
                    "g_a_class4007", "g_a_class4008",   // Bandon's Fiber Armor
                    "g_a_class5008", "g_a_class5009",   // Eriadu Prototype Armor
                    "g_a_class6008", "g_a_class6009",   // Davik's War Suit
                    "g_a_class8006", "g_a_class8007",   // Calo Nord's Battle Armor
                    "g_a_class9006", "g_a_class9007",   // Jurgan Kalta's Power Suit
                    "g_a_class9011",    // Cassus Fett's Battle Armor
                    "g_a_clothes02", "g_a_clothes03", "g_a_clothes04", "g_a_clothes05",
                    "g_a_clothes06", "g_a_clothes07", "g_a_clothes08", "g_a_clothes09", // Clothing Variants
                    "g_i_crhide004", "g_i_crhide006",   // Tuk'ata Hide
                    "g_w_blstrcrbn006", "g_w_blstrcrbn007", "g_w_blstrcrbn008", "g_w_blstrcrbn009", // Jamoh Hogra's Carbine
                    "g_w_blstrpstl006", "g_w_blstrpstl007", "g_w_blstrpstl008", "g_w_blstrpstl009", // Bendak's Blaster
                    "g_w_blstrrfl006",  "g_w_blstrrfl007",  "g_w_blstrrfl008",  "g_w_blstrrfl009",  // Jurgan Kalta's Assault Rifle
                    "g_w_hvyblstr06",   "g_w_hvyblstr07",   "g_w_hvyblstr08",   "g_w_hvyblstr09",   // Cassus Fett's Heavy Pistol
                    "g_w_null001", "g_w_null002", "g_w_null003", "g_w_null004", "g_w_null005", "g_w_null006",   // Null weapons
                    "g_w_stunbaton05", "g_w_stunbaton06", "g_w_stunbaton07",    // Rakatan Battle Wand
                    "g_w_vbrdblswd05", "g_w_vbrdblswd06", "g_w_vbrdblswd07",    // Yusanis' Brand
                    "g_w_vbroshort05", "g_w_vbroshort06", "g_w_vbroshort07",    // Sanasiki's Blade
                    "g_w_vbroswrd06",  "g_w_vbroswrd07",  "g_w_vbroswrd08",     // Bacca's Ceremonial Blade
                    "g_i_frarmbnds10", "g_i_frarmbnds11", "g_i_frarmbnds12",    // Lower Saves, All
                    "g_i_frarmbnds13", "g_i_frarmbnds14", "g_i_frarmbnds15",    // Lower Saves, Fortitude
                    "g_i_frarmbnds16", "g_i_frarmbnds17", "g_i_frarmbnds18",    // Lower Saves, Reflex
                    "g_i_frarmbnds19", "g_i_frarmbnds20", "g_i_frarmbnds21",    // Loser Saves, Will
                    // Inaccessible
                    "g_a_class4004", "g_a_class5004", "g_a_class5006", "g_a_class6005",
                    "g_a_class8003", "g_a_class8004", "g_a_class9003", "g_a_class9004", // Armors
                    "g_a_jedirobe02", "g_a_jedirobe03", "g_a_jedirobe04", "g_a_jedirobe05", // Jedi Robes (r,b,dB,db)
                    "g_a_mstrrobe03", "g_a_mstrrobe04", // Jedi Master Robes (r,b)
                    "g_i_asthitem001",  // Aesthetic Item
                    "g_i_bithitem001", "g_i_bithitem002", "g_i_bithitem003", "g_i_bithitem004", // Bith Instruments
                    "g_i_credits001", "g_i_credits002", "g_i_credits003", "g_i_credits004",
                    "g_i_credits005", "g_i_credits006", "g_i_credits007", "g_i_credits008",
                    "g_i_credits009", "g_i_credits010", "g_i_credits011", "g_i_credits012",
                    "g_i_credits013", "g_i_credits014", "g_i_credits015",   // Credit Denominations
                    "g_i_crhide001", "g_i_crhide002", "g_i_crhide003", "g_i_crhide004",
                    "g_i_crhide005", "g_i_crhide006", "g_i_crhide007", "g_i_crhide008",
                    "g_i_crhide009", "g_i_crhide010", "g_i_crhide011", "g_i_crhide012",
                    "g_i_crhide013",    // Creature Hides
                    "g_i_datapad001",   // Datapad
                    "g_i_drdsncsen001", "g_i_drdsncsen002", "g_i_drdsncsen003", // Sonic Sensors
                    "g_i_drdsrcscp001", "g_i_drdsrcscp002", "g_i_drdsrcscp003", // Search Scopes
                    "g_i_drdtrgcom006", // Bothan Demolitions Probe
                    "g_i_gauntlet05",   // Bothan 'Machinist' Gloves
                    "g_i_implant201", "g_i_implant302", "g_i_implant303",   // Implants
                    "g_i_mask13", "g_i_mask19", // Masks
                    "g_i_medeqpmnt05", "g_i_medeqpmnt06", "g_i_medeqpmnt07", "g_i_medeqpmnt08", // Medpacks
                    "g_i_pltuseitm01", "g_i_progspike003", "g_i_recordrod01",   // Unused Items
                    "g_i_trapkit01", "g_i_trapkit02", "g_i_trapkit03", "g_i_trapkit04", // Mines
                    "g_w_blstrpstl020", // Insta-kill Pistol
                    "g_w_crgore001", "g_w_crgore002", "g_w_crslash001", "g_w_crslash002",
                    "g_w_crslash003", "g_w_crslash004", "g_w_crslash005", "g_w_crslash006",
                    "g_w_crslash007", "g_w_crslash008", "g_w_crslash009", "g_w_crslash010",
                    "g_w_crslash011", "g_w_crslash012", "g_w_crslprc001", "g_w_crslprc002",
                    "g_w_crslprc003", "g_w_crslprc004", "g_w_crslprc005",   // Creature Weapons
                    "g_w_dblsbr001", "g_w_dblsbr003", "g_w_dblsbr005", "g_w_drkjdisbr002",  // Double-Bladed Sabers
                    "g_w_lghtsbr04", "g_w_lghtsbr06", "g_w_drkjdisbr001",   // Lightsabers
                    "g_w_qtrstaff02",   // Massassi Staff
                    "g_w_shortsbr01", "g_w_shortsbr03", "g_w_shortsbr04",   // Short-sabers
                    "g_w_shortswrd02", "g_w_shortswrd03",   // Shortswords
                    "g_w_sonicrfl03",   // Sonic rifle
                    "g_w_vbrdblswd02", "g_w_vbrdblswd03",   // Sith War Sword
                    "g_w_waraxe001",    // Gamorrean Battleaxe
                    "w_bstrcrbn",   // Clothing, weapons +1
                }
            },
        };

        /// <summary>
        /// Collectiuon of acceptable 2da files and collumns to randomize
        /// </summary>
        public static readonly Dictionary<string, List<string>> TWODA_COLLUMNS = new Dictionary<string, List<string>>()
        {
            { "acbonus", new List<string>()
                { "scd", "sol", "sct", "jdc", "jds", "jdg" }
            },
            { "aliensound", new List<string>()
                { "filename" }
            },
            { "appearance", new List<string>()
                { "walkdist", "rundist", "moverate", "body_bag" }
            },
            { "baseitems", new List<string>()
                { "name", "equipableslots", "defaulticon", "reqfeat0" }
            },
            { "bodybag", new List<string>()
                { "appearance" }
            },
            { "camerastyle", new List<string>()
                { "distance", "pitch", "height" }
            },
            { "classes", new List<string>()
                { "name", "icon", "hitdie", "attackbonustable", "featstable", "savingthrowtable", "skillstable", "skillpointbase", "armorclasscolumn", "featgain" }
            },
            { "classpowergain", new List<string>()
                { "jcn", "jsn", "jgd" }
            },
            { "cls_atk_1", new List<string>()
                { "bab" }
            },
            { "cls_atk_2", new List<string>()
                { "bab" }
            },
            { "cls_atk_3", new List<string>()
                { "bab" }
            },
            { "cls_st_cm_drd", new List<string>()
                { "fortsave", "refsave", "willsave" }
            },
            { "cls_st_ex_drd", new List<string>()
                { "fortsave", "refsave", "willsave" }
            },
            { "cls_st_jedi_c", new List<string>()
                { "fortsave", "refsave", "willsave" }
            },
            { "cls_st_jedi_g", new List<string>()
                { "fortsave", "refsave", "willsave" }
            },
            { "cls_st_jedi_s", new List<string>()
                { "fortsave", "refsave", "willsave" }
            },
            { "cls_st_minion", new List<string>()
                { "fortsave", "refsave", "willsave" }
            },
            { "cls_st_scndrl", new List<string>()
                { "fortsave", "refsave", "willsave" }
            },
            { "cls_st_scout", new List<string>()
                { "fortsave", "refsave", "willsave" }
            },
            { "cls_st_soldier", new List<string>()
                { "fortsave", "refsave", "willsave" }
            },
            { "comptypes", new List<string>()
                { "computerbackground" }
            },
            { "creaturespeed", new List<string>()
                { "walkrate", "runrate" }
            },
            { "effecticon", new List<string>()
                { "iconresref", "good", "priority" }
            },
            { "feat", new List<string>()
                { "name", "description", "icon" }
            },
            { "forceadjust", new List<string>()
                { "goodcost", "evilcost" }
            },
            { "forceshields", new List<string>()
                { "visualeffectdef", "amount", }
            },
            { "genericdoors", new List<string>()
                { "soundapptype" }
            },
            { "guisounds", new List<string>()
                { "soundresref" }
            },
            { "heads", new List<string>()
                { "head" }
            },
            { "loadscreenhints", new List<string>()
                { "gameplayhint" }
            },
            { "placeableobjsnds", new List<string>()
                { "opened", "closed", "locked" }
            },
            { "skills", new List<string>()
                { "name", "description", "icon", "keyability", "scd_class", "sol_class", "sct_class", "jcn_class", "jgd_class", "jsn_class", "drx_class", "drc_class" }
            },
            { "soundset", new List<string>()
                { "resref" }
            },
            { "spells", new List<string>()
                { "name", "spelldesc", "forcepoints", "goodevil", "iconresref", "castanim" }
            },
            { "tilecolor", new List<string>()
                { "red", "green", "blue" }
            },
            { "traps", new List<string>()
                { "setdc", "detectdcmod", "disarmdcmod", "trapname", "explosionsound" }
            },
            { "upcrystals", new List<string>()
                { "shortmdlvar", "longmdlvar", "doublemdlvar" }
            },
            { "upgradetypes", new List<string>()
                { "label" }
            },
            { "videoeffects", new List<string>()
                { "modulationred", "modulationgreen", "modulationblue", "saturation" }
            },
            { "xpbaseconst", new List<string>()
                { "balance", "bonus" }
            },
            { "xptable", new List<string>()
                { "c0", "c1", "c2", "c3", "c4", "c5", "c6", "c7", "c8", "c9", "c10", "c11", "c12", "c13", "c14", "c15", "c16", "c17", "c18", "c19", "c20" }
            }
        };
        #endregion

        #region Variables
        /// <summary>
        /// Bound list of modules where the mod entries with selected omissions will be stored.
        /// </summary>
        public static BindingList<Mod_Entry> BoundModules = new BindingList<Mod_Entry>();

        /// <summary>
        /// Bound list of items to be omitted from randomization. This is necessary because certain items can result in soft locks if randomized.
        /// </summary>
        public static BindingList<string> OmitItems = new BindingList<string>(new List<string>(DEFAULT_OMIT_ITEMS));

        /// <summary>
        /// Dictionary of selected 2DAs to be randomized.
        /// </summary>
        public static Dictionary<string, List<string>> Selected2DAs = new Dictionary<string, List<string>>();
        #endregion

        #region Types
        /// <summary>
        /// Struct used in Collections for module randomization denoting omission.
        /// </summary>
        public struct Mod_Entry
        {
            public string Code { get; }
            public string Common { get; }
            public bool Omitted { get; set; }

            public Mod_Entry(string code, bool omitted)
            {
                Code = code;
                Common = ModuleRando.GetModuleCommonName(code) ?? code;
                Omitted = omitted;
            }

            public override string ToString()
            {
                StringBuilder sb = new StringBuilder(/*Omitted ? "X:" : "I:"*/);
                sb.Append(Code);
                sb.Append(": ".PadRight(12 - Code.Length));
                sb.Append(Common);
                return sb.ToString();
            }
        }
        #endregion
    }
}
