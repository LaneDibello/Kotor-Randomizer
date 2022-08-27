using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using KotOR_IO;
using System.IO;
using ClosedXML.Excel;
using kotor_Randomizer_2.Models;

namespace kotor_Randomizer_2
{
    public static class TextureRando
    {
        private static List<int> MaxRando { get; } = new List<int>();

        private static List<List<int>> TypeLists { get; } = new List<List<int>>();

        /// <summary>
        /// Lookup table for how models are randomized.
        /// Usage: LookupTable[OriginalID] = RandomizedID;
        /// </summary>
        private static Dictionary<int, int> LookupTable { get; set; } = new Dictionary<int, int>();

        /// <summary>
        /// Lookup table for the name of each Texture ID.
        /// </summary>
        private static Dictionary<int, string> NameLookup { get; set; } = new Dictionary<int, string>();

        private static RandomizationLevel RandomizeCreatures    { get; set; }
        private static RandomizationLevel RandomizeCubeMaps     { get; set; }
        private static RandomizationLevel RandomizeEffects      { get; set; }
        private static RandomizationLevel RandomizeItems        { get; set; }
        private static RandomizationLevel RandomizeNPC          { get; set; }
        private static RandomizationLevel RandomizeOther        { get; set; }
        private static RandomizationLevel RandomizeParty        { get; set; }
        private static RandomizationLevel RandomizePlaceables   { get; set; }
        private static RandomizationLevel RandomizePlanetary    { get; set; }
        private static RandomizationLevel RandomizePlayerBodies { get; set; }
        private static RandomizationLevel RandomizePlayerHeads  { get; set; }
        private static RandomizationLevel RandomizeStunt        { get; set; }
        private static RandomizationLevel RandomizeVehicles     { get; set; }
        private static RandomizationLevel RandomizeWeapons      { get; set; }
        private static TexturePack        SelectedPack          { get; set; }

        #region Regexes
        private static readonly Regex RegexCubeMaps = new Regex("^CM_", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Regex RegexCreatures = new Regex("^C_", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Regex RegexEffects = new Regex("^FX_", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Regex RegexItems = new Regex("^I_", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Regex RegexPlanetary = new Regex("^L.{2}_", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Regex RegexNPC = new Regex("^N_", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Regex RegexPlayHeads = new Regex("^P(F|M)H", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Regex RegexPlayBodies = new Regex("^P(F|M)B", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Regex RegexPlaceables = new Regex("^PLC_", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Regex RegexParty = new Regex("^P_", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Regex RegexStunt = new Regex("^Stunt", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Regex RegexVehicles = new Regex("^V_", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Regex RegexWeapons = new Regex("^W_", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        #endregion

        /// <summary>
        /// Creates backups for files modified during this randomization.
        /// </summary>
        /// <param name="paths"></param>
        internal static void CreateTextureBackups(KPaths paths)
        {
            paths.BackUpTexturesDirectory();
        }

        public static void texture_rando(KPaths paths, Kotor1Randomizer k1rando = null)
        {
            // Prepare for new randomization.
            Reset();
            AssignSettings(k1rando);

            // Load in texture pack.
            string pack_name;
            switch (SelectedPack)
            {
                default:
                case TexturePack.HighQuality:
                    pack_name = "\\swpc_tex_tpa.erf";
                    break;
                case TexturePack.MedQuality:
                    pack_name = "\\swpc_tex_tpb.erf";
                    break;
                case TexturePack.LowQuality:
                    pack_name = "\\swpc_tex_tpc.erf";
                    break;
            }

            ERF e = new ERF(paths.TexturePacks + pack_name);

            foreach (var key in e.Key_List)
            {
                if (!NameLookup.ContainsKey(key.ResID))
                    NameLookup.Add(key.ResID, key.ResRef);
            }

            // Handle categories.
            HandleCategory(e, RegexCubeMaps,   RandomizeCubeMaps);
            HandleCategory(e, RegexCreatures,  RandomizeCreatures);
            HandleCategory(e, RegexEffects,    RandomizeEffects);
            HandleCategory(e, RegexItems,      RandomizeItems);
            HandleCategory(e, RegexPlanetary,  RandomizePlanetary);
            HandleCategory(e, RegexNPC,        RandomizeNPC);
            HandleCategory(e, RegexPlayHeads,  RandomizePlayerHeads);
            HandleCategory(e, RegexPlayBodies, RandomizePlayerBodies);
            HandleCategory(e, RegexPlaceables, RandomizePlaceables);
            HandleCategory(e, RegexParty,      RandomizeParty);
            HandleCategory(e, RegexStunt,      RandomizeStunt);
            HandleCategory(e, RegexVehicles,   RandomizeVehicles);
            HandleCategory(e, RegexWeapons,    RandomizeWeapons);

            // Handle other.
            switch (RandomizeOther)
            {
                default:
                case RandomizationLevel.None:
                    break; // Do nothing.
                case RandomizationLevel.Type:
                    List<int> type = new List<int>(e.Key_List.Where(x => Matches_None(x.ResRef) && !Is_Forbidden(x.ResRef)).Select(x => x.ResID));
                    TypeLists.Add(type);
                    break;
                case RandomizationLevel.Max:
                    MaxRando.AddRange(e.Key_List.Where(x => Matches_None(x.ResRef) && !Is_Forbidden(x.ResRef)).Select(x => x.ResID));
                    break;
            }

            // Max Rando.
            List<int> Max_Rando_Iterator = new List<int>(MaxRando);
            Randomize.FisherYatesShuffle(MaxRando);
            int j = 0;
            foreach (ERF.Key k in e.Key_List.Where(x => Max_Rando_Iterator.Contains(x.ResID)))
            {
                LookupTable.Add(k.ResID, MaxRando[j]);
                k.ResID = MaxRando[j];
                j++;
            }

            // Type Rando.
            foreach (List<int> li in TypeLists)
            {
                List<int> type_copy = new List<int>(li);
                Randomize.FisherYatesShuffle(type_copy);
                j = 0;
                foreach (ERF.Key k in e.Key_List.Where(x => li.Contains(x.ResID)))
                {
                    LookupTable.Add(k.ResID, type_copy[j]);
                    k.ResID = type_copy[j];
                    j++;
                }
            }

            e.WriteToFile(paths.TexturePacks + pack_name);
        }

        private static void AssignSettings(Kotor1Randomizer k1rando)
        {
            if (k1rando == null)
            {
                RandomizeCreatures    = Properties.Settings.Default.TextureRandomizeCreatures;
                RandomizeCubeMaps     = Properties.Settings.Default.TextureRandomizeCubeMaps;
                RandomizeEffects      = Properties.Settings.Default.TextureRandomizeEffects;
                RandomizeItems        = Properties.Settings.Default.TextureRandomizeItems;
                RandomizeNPC          = Properties.Settings.Default.TextureRandomizeNPC;
                RandomizeOther        = Properties.Settings.Default.TextureRandomizeOther;
                RandomizeParty        = Properties.Settings.Default.TextureRandomizeParty;
                RandomizePlaceables   = Properties.Settings.Default.TextureRandomizePlaceables;
                RandomizePlanetary    = Properties.Settings.Default.TextureRandomizePlanetary;
                RandomizePlayerBodies = Properties.Settings.Default.TextureRandomizePlayBodies;
                RandomizePlayerHeads  = Properties.Settings.Default.TextureRandomizePlayHeads;
                RandomizeStunt        = Properties.Settings.Default.TextureRandomizeStunt;
                RandomizeVehicles     = Properties.Settings.Default.TextureRandomizeVehicles;
                RandomizeWeapons      = Properties.Settings.Default.TextureRandomizeWeapons;
                SelectedPack          = Properties.Settings.Default.TexturePack;
            }
            else
            {
                RandomizeCreatures    = k1rando.TextureCreatures;
                RandomizeCubeMaps     = k1rando.TextureCubeMaps;
                RandomizeEffects      = k1rando.TextureEffects;
                RandomizeItems        = k1rando.TextureItems;
                RandomizeNPC          = k1rando.TextureNPC;
                RandomizeOther        = k1rando.TextureOther;
                RandomizeParty        = k1rando.TextureParty;
                RandomizePlaceables   = k1rando.TexturePlaceables;
                RandomizePlanetary    = k1rando.TexturePlanetary;
                RandomizePlayerBodies = k1rando.TexturePlayerBodies;
                RandomizePlayerHeads  = k1rando.TexturePlayerHeads;
                RandomizeStunt        = k1rando.TextureStunt;
                RandomizeVehicles     = k1rando.TextureVehicles;
                RandomizeWeapons      = k1rando.TextureWeapons;
                SelectedPack          = k1rando.TextureSelectedPack;
            }
        }

        private static bool Matches_None(string s)
        {
            return
                (
                    !RegexCubeMaps.IsMatch(s) &&
                    !RegexCreatures.IsMatch(s) &&
                    !RegexEffects.IsMatch(s) &&
                    !RegexItems.IsMatch(s) &&
                    !RegexPlanetary.IsMatch(s) &&
                    !RegexNPC.IsMatch(s) &&
                    !RegexPlayHeads.IsMatch(s) &&
                    !RegexPlayBodies.IsMatch(s) &&
                    !RegexPlaceables.IsMatch(s) &&
                    !RegexParty.IsMatch(s) &&
                    !RegexStunt.IsMatch(s) &&
                    !RegexVehicles.IsMatch(s) &&
                    !RegexWeapons.IsMatch(s)
                );
        }

        private static bool Is_Forbidden(string s)
        {
            return
            (
                s.ToUpper().Last() == 'B' ||
                s.ToUpper().Contains("BMP") ||
                s.ToUpper().Contains("BUMP") ||
                s == "MGG_ebonhawkB01"
            );
        }

        private static void HandleCategory(ERF e, Regex r, RandomizationLevel randomizationlevel)
        {
            switch (randomizationlevel)
            {
                default:
                case RandomizationLevel.None:
                    break; // Do nothing.
                case RandomizationLevel.Type:
                    List<int> type = new List<int>(e.Key_List.Where(x => r.IsMatch(x.ResRef) && !Is_Forbidden(x.ResRef)).Select(x => x.ResID));
                    TypeLists.Add(type);
                    break;
                case RandomizationLevel.Max:
                    MaxRando.AddRange(e.Key_List.Where(x => r.IsMatch(x.ResRef) && !Is_Forbidden(x.ResRef)).Select(x => x.ResID));
                    break;
            }
        }

        internal static void Reset()
        {
            // Prepare lists for new randomization.
            MaxRando.Clear();
            TypeLists.Clear();
            LookupTable.Clear();
            NameLookup.Clear();
        }

        internal static void CreateSpoilerLog(XLWorkbook workbook)
        {
            if (LookupTable.Count == 0) { return; }
            var ws = workbook.Worksheets.Add("Texture");
            int i = 1;

            // Texture Randomization Settings
            ws.Cell(i, 1).Value = "Texture Pack";
            ws.Cell(i, 2).Value = SelectedPack.ToDescription();
            ws.Cell(i, 1).Style.Font.Bold = true;
            i += 2;     // Skip a row.

            ws.Cell(i, 1).Value = "Texture Type";
            ws.Cell(i, 2).Value = "Rando Level";
            ws.Cell(i, 1).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            ws.Cell(i, 1).Style.Font.Bold = true;
            ws.Cell(i, 2).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            ws.Cell(i, 2).Style.Font.Bold = true;
            i++;

            var settings = new List<Tuple<string, string>>()
            {
                new Tuple<string, string>("Cube Maps",     RandomizeCubeMaps.ToString()),
                new Tuple<string, string>("Creatures",     RandomizeCreatures.ToString()),
                new Tuple<string, string>("Effects",       RandomizeEffects.ToString()),
                new Tuple<string, string>("Items",         RandomizeItems.ToString()),
                new Tuple<string, string>("Planetary",     RandomizePlanetary.ToString()),
                new Tuple<string, string>("NPC",           RandomizeNPC.ToString()),
                new Tuple<string, string>("Player Heads",  RandomizePlayerHeads.ToString()),
                new Tuple<string, string>("Player Bodies", RandomizePlayerBodies.ToString()),
                new Tuple<string, string>("Placeables",    RandomizePlaceables.ToString()),
                new Tuple<string, string>("Party",         RandomizeParty.ToString()),
                new Tuple<string, string>("Stunt",         RandomizeStunt.ToString()),
                new Tuple<string, string>("Vehicles",      RandomizeVehicles.ToString()),
                new Tuple<string, string>("Weapons",       RandomizeWeapons.ToString()),
                new Tuple<string, string>("Other",         RandomizeOther.ToString()),
                new Tuple<string, string>("", ""),  // Skip a row.
            };

            foreach (var setting in settings)
            {
                ws.Cell(i, 1).Value = setting.Item1;
                ws.Cell(i, 2).Value = setting.Item2;
                ws.Cell(i, 1).Style.Font.Italic = true;
                i++;
            }

            i++;    // Skip a row.

            // Texture Shuffle
            ws.Cell(i, 1).Value = "Has Changed";
            ws.Cell(i, 2).Value = "Orig ID";
            ws.Cell(i, 3).Value = "Orig Ref";
            ws.Cell(i, 4).Value = "Rand ID";
            ws.Cell(i, 5).Value = "Rand Ref";
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

            foreach (var kvp in LookupTable)
            {
                var hasChanged = kvp.Key != kvp.Value;
                ws.Cell(i, 1).Value = hasChanged.ToString().ToUpper();
                ws.Cell(i, 1).DataType = XLDataType.Text;
                ws.Cell(i, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Cell(i, 2).Value = kvp.Key;
                if (NameLookup.ContainsKey(kvp.Key))
                    ws.Cell(i, 3).Value = NameLookup[kvp.Key];
                ws.Cell(i, 4).Value = kvp.Value;
                if (NameLookup.ContainsKey(kvp.Value))
                    ws.Cell(i, 5).Value = NameLookup[kvp.Value];

                if (hasChanged) ws.Cell(i, 1).Style.Font.FontColor = XLColor.Green;
                else            ws.Cell(i, 1).Style.Font.FontColor = XLColor.Red;
                i++;
            }

            // Resize Columns
            ws.Column(1).AdjustToContents();
            ws.Column(2).AdjustToContents();
            ws.Column(3).AdjustToContents();
            ws.Column(4).AdjustToContents();
            ws.Column(5).AdjustToContents();
        }
    }
}
