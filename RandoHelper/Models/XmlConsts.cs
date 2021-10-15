namespace RandoHelper.Models
{
    public struct XmlConsts
    {
        // Games
        public const string Kotor1 = "Kotor1"; // Modules
        public const string Kotor2 = "Kotor2"; // Modules

        // Elements
        public const string Modules = "Modules"; // Modules
        public const string Module = "Module";  // Vertex
        public const string LeadsTo = "LeadsTo"; // Edge

        // Attributes
        public const string Game = "Game";       // Modules
        public const string Planet = "Planet";     // Vertex
        public const string Code = "WarpCode";   // Vertex, Edge
        public const string Name = "CommonName"; // Vertex, Edge
        public const string Tags = "Tags";       // Vertex, Edge
        public const string LockedTag = "LockedTag";  // Vertex
        public const string Unlock = "Unlock";     // Vertex, Edge

        // General
        public const char SeparatorComma = ',';
        public const char SeparatorSemicolon = ';';

        // Blocking Tags ... Edge (Tags)
        public const string Fake = "Fake";
        public const string Locked = "Locked";
        public const string Once = "Once";

        // Goal Tags ... Vertex (Tags)
        public const string Malak = "Malak";
        public const string Pazaak = "Pazaak";
        public const string Start = "Start";
        public const string StarMap = "StarMap";

        // Party Tags ... Vertex (Tags, LockedTag), Edge (Unlock)
        public const string Bastila = "Bastila";
        public const string Canderous = "Canderous";
        public const string Carth = "Carth";
        public const string HK47 = "HK47";
        public const string Jolee = "Jolee";
        public const string Juhani = "Juhani";
        public const string Mission = "Mission";
        public const string T3M4 = "T3M4";
        public const string Zaalbar = "Zaalbar";

        // Glitch Tags ... Edge (Tags)
        public const string Clip = "Clip";
        public const string DLZ = "DLZ";
        public const string FLU = "FLU";
        public const string GPW = "GPW";

        // Fix Tags ... Edge (Tags)
        public const string FixBox = "FixBox";
        public const string FixElev = "FixElev";
        public const string FixMap = "FixMap";
        public const string FixSpice = "FixSpice";
        public const string HangarAccess = "HangarAccess";

        // Unlock Tags ... Edge (Tags)
        public const string UnlockRuins = "UL_Ruins";
        public const string UnlockAcademy = "UL_Academy";
        public const string UnlockEmbassy = "UL_Embassy";
        public const string UnlockHangar = "UL_Hangar";
        public const string UnlockBastila = "UL_Deck3";
        public const string UnlockVulkar = "UL_Vulkar";
        public const string UnlockUndercity = "UL_Undercity";
        public const string UnlockSummit = "UL_Summit";
        public const string UnlockTempleExit = "UL_TempleExit";

        // Early Party Tags ... Vertex (Unlock)
        public const string UnlockEarlyT3 = "EarlyT3";
    }
}
