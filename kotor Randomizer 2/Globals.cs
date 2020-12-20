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
        Default         = 0x00, // 0b00000000
        /// <summary> Do not delete milestone save data. </summary>
        NoSaveDelete    = 0x01, // 0b00000001
        /// <summary> Include minigame data in the save file. </summary>
        SaveMiniGames   = 0x02, // 0b00000010
        /// <summary> Include all module data in the save file. </summary>
        SaveAllModules  = 0x04, // 0b00000100
        /// <summary> Fix dream cutscenes. </summary>
        FixDream        = 0x08, // 0b00001000
        /// <summary> Unlock all destinations on the galaxy map. </summary>
        UnlockGalaxyMap = 0x10, // 0b00010000
        /// <summary> Fix warp spawn coordinates in certain modules. </summary>
        FixCoordinates  = 0x20, // 0b00100000
        /// <summary> Fix Rakatan mind prison to prevent soft-locks. </summary>
        FixMindPrison   = 0x40, // 0b01000000
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
        public static readonly Dictionary<string, Tuple<int, int, int>> FIXED_COORDINATES = new Dictionary<string, Tuple<int, int, int>>()
        {
            { AREA_UNDERCITY, new Tuple<int, int, int>(
                BitConverter.ToInt32(BitConverter.GetBytes(183.5f), 0),
                BitConverter.ToInt32(BitConverter.GetBytes(167.4f), 0),
                BitConverter.ToInt32(BitConverter.GetBytes(1.5f), 0)) },
            { AREA_TOMB_TULAK, new Tuple<int, int, int>(
                BitConverter.ToInt32(BitConverter.GetBytes(15.8f), 0),
                BitConverter.ToInt32(BitConverter.GetBytes(55.6f), 0),
                BitConverter.ToInt32(BitConverter.GetBytes(0.75f), 0)) },
            { AREA_LEVI_HANGAR, new Tuple<int, int, int>(
                BitConverter.ToInt32(BitConverter.GetBytes(12.5f), 0),
                BitConverter.ToInt32(BitConverter.GetBytes(155.2f), 0),
                BitConverter.ToInt32(BitConverter.GetBytes(3.0f), 0)) },
            { AREA_AHTO_WEST, new Tuple<int, int, int>(
                BitConverter.ToInt32(BitConverter.GetBytes(5.7f), 0),
                BitConverter.ToInt32(BitConverter.GetBytes(-10.7f), 0),
                BitConverter.ToInt32(BitConverter.GetBytes(59.2f), 0)) },
            { AREA_MANAAN_SITH, new Tuple<int, int, int>(
                BitConverter.ToInt32(BitConverter.GetBytes(112.8f), 0),
                BitConverter.ToInt32(BitConverter.GetBytes(2.4f), 0),
                BitConverter.ToInt32(BitConverter.GetBytes(0f), 0)) },
            { AREA_RAKA_SETTLE, new Tuple<int, int, int>(
                BitConverter.ToInt32(BitConverter.GetBytes(202.2f), 0),
                BitConverter.ToInt32(BitConverter.GetBytes(31.5f), 0),
                BitConverter.ToInt32(BitConverter.GetBytes(40.7f), 0)) },
            { AREA_TEMPLE_MAIN, new Tuple<int, int, int>(
                BitConverter.ToInt32(BitConverter.GetBytes(95.3f), 0),
                BitConverter.ToInt32(BitConverter.GetBytes(42.0f), 0),
                BitConverter.ToInt32(BitConverter.GetBytes(0.44f), 0)) },
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
        public static readonly List<int> LARGE_PLACE = new List<int>() { 1, 2, 55, 56, 57, 58, 65, 66, 110, 111, 142, 172, 176, 217, 218, 226 }; // NEED TO RESEARCH

        /// <summary>
        /// Broken Placeable Models
        /// </summary>
        public static readonly List<int> BROKEN_PLACE = new List<int>() { 0, 8, 9, 47, 62, 78, 84, 90, 94, 97, 115, 158, 159 };

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
        /// Built-in module omission presets. The key being the preset name, and the value being a string list of modules to omit. Not to be confused with user-defined presets.
        /// </summary>
        public static readonly Dictionary<string, List<string>> OMIT_PRESETS = new Dictionary<string, List<string>>()
        {
            //{ "<Custom>", null },
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
            { "Max Random", new List<string>()
                {
                }
            }

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
        public static BindingList<string> OmitItems = new BindingList<string>()
        {
            "g_i_collarlgt001", "g_i_glowrod01", "g_i_torch01", "ptar_sitharmor", "tat17_sandperdis", "g_i_progspike02", "g_i_implant104"
        };

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
            public string Name { get; }
            public bool Omitted { get; set; }

            public Mod_Entry(string name, bool omitted)
            {
                Name = name;
                Omitted = omitted;
            }

            public override string ToString()
            {
                StringBuilder sb = new StringBuilder(Omitted ? "X:" : "I:");
                sb.Append(Name);
                return sb.ToString();
            }
        }
        #endregion
    }
}
