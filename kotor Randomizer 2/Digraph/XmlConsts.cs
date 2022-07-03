namespace kotor_Randomizer_2.Digraph
{
    /// <summary>
    /// Collection of constants used for parsing modules and edges from an XML file.
    /// </summary>
    public struct XmlConsts
    {
        // Games
        public const string GAME_KOTOR_1 = "Kotor1"; // Modules
        public const string GAME_KOTOR_2 = "Kotor2"; // Modules

        // Elements
        public const string ELEM_MODULES  = "Modules"; // Modules
        public const string ELEM_MODULE   = "Module";  // Vertex
        public const string ELEM_LEADS_TO = "LeadsTo"; // Edge

        // Attributes
        public const string ATTR_GAME       = "Game";       // Modules
        public const string ATTR_PLANET     = "Planet";     // Vertex
        public const string ATTR_CODE       = "WarpCode";   // Vertex, Edge
        public const string ATTR_NAME       = "CommonName"; // Vertex, Edge
        public const string ATTR_TAGS       = "Tags";       // Vertex, Edge
        public const string ATTR_LOCKED_TAG = "LockedTag";  // Vertex
        public const string ATTR_UNLOCK     = "Unlock";     // Vertex, Edge

        // General
        public const char   TAG_SEPARATOR_COMMA = ',';
        public const char   TAG_SEPARATOR_SEMICOLON = ';';

        // Blocking Tags ... Edge (Tags)
        public const string TAG_FAKE      = "Fake";
        public const string TAG_LOCKED    = "Locked";
        public const string TAG_ONCE      = "Once";

        // Goal Tags ... Vertex (Tags)
        public const string TAG_START    = "Start";
        public const string TAG_MALAK    = "Malak";
        public const string TAG_STAR_MAP = "StarMap";
        public const string TAG_TRAYA    = "Traya";
        public const string TAG_MASTER   = "Master";
        public const string TAG_PAZAAK   = "Pazaak";

        // Party Tags ... Vertex (Tags, LockedTag), Edge (Unlock)
        public const string TAG_BASTILA   = "Bastila";
        public const string TAG_CANDEROUS = "Canderous";
        public const string TAG_CARTH     = "Carth";
        public const string TAG_HK47      = "HK47";
        public const string TAG_JOLEE     = "Jolee";
        public const string TAG_JUHANI    = "Juhani";
        public const string TAG_MISSION   = "Mission";
        public const string TAG_T3M4      = "T3M4";
        public const string TAG_ZAALBAR   = "Zaalbar";

        public const string TAG_ATTON      = "Atton";
        public const string TAG_BAODUR     = "BaoDur";
        public const string TAG_DISCIPLE   = "Disciple";
        public const string TAG_G0T0       = "G0T0";
        public const string TAG_HANDMAIDEN = "Handmaiden";
        public const string TAG_HANHARR    = "Hanharr";
        public const string TAG_KREIA      = "Kreia";
        public const string TAG_MANDALORE  = "Mandalore";
        public const string TAG_MIRA       = "Mira";
        public const string TAG_VISAS      = "Visas";

        // Glitch Tags ... Edge (Tags)
        public const string TAG_CLIP = "Clip";
        public const string TAG_DLZ = "DLZ";
        public const string TAG_FLU = "FLU";
        public const string TAG_GPW = "GPW";

        // Fix Tags ... Edge (Tags)
        public const string TAG_FIX_BOX   = "FixBox";
        public const string TAG_FIX_ELEV  = "FixElev";
        public const string TAG_FIX_MAP   = "FixMap";
        public const string TAG_FIX_SPICE = "FixSpice";
        public const string TAG_HANGAR_ACCESS = "HangarAccess";

        // Unlock Tags ... Edge (Tags)
        public const string TAG_UNLOCK_DAN_RUINS       = "UL_Ruins";
        public const string TAG_UNLOCK_KOR_ACADEMY     = "UL_Academy";
        public const string TAG_UNLOCK_MAN_EMBASSY     = "UL_Embassy";
        public const string TAG_UNLOCK_MAN_HANGAR      = "UL_Hangar";
        public const string TAG_UNLOCK_STA_BASTILA     = "UL_Deck3";
        public const string TAG_UNLOCK_TAR_VULKAR      = "UL_Vulkar";
        public const string TAG_UNLOCK_TAR_UNDERCITY   = "UL_Undercity";
        public const string TAG_UNLOCK_UNK_SUMMIT      = "UL_Summit";
        public const string TAG_UNLOCK_UNK_TEMPLE_EXIT = "UL_TempleExit";

        // Early Party Tags ... Vertex (Unlock)
        public const string TAG_UNLOCK_EARLY_T3M4 = "EarlyT3";
    }
}
