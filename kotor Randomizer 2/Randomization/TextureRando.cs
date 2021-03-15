using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using KotOR_IO;
using System.IO;
using ClosedXML.Excel;

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

        public static void texture_rando(KPaths paths)
        {
            // Prepare lists for new randomization.
            MaxRando.Clear();
            TypeLists.Clear();

            // Load in texture pack.
            string pack_name;
            switch (Properties.Settings.Default.TexturePack)
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
            HandleCategory(e, RegexCubeMaps,   Properties.Settings.Default.TextureRandomizeCubeMaps);
            HandleCategory(e, RegexCreatures,  Properties.Settings.Default.TextureRandomizeCreatures);
            HandleCategory(e, RegexEffects,    Properties.Settings.Default.TextureRandomizeEffects);
            HandleCategory(e, RegexItems,      Properties.Settings.Default.TextureRandomizeItems);
            HandleCategory(e, RegexPlanetary,  Properties.Settings.Default.TextureRandomizePlanetary);
            HandleCategory(e, RegexNPC,        Properties.Settings.Default.TextureRandomizeNPC);
            HandleCategory(e, RegexPlayHeads,  Properties.Settings.Default.TextureRandomizePlayHeads);
            HandleCategory(e, RegexPlayBodies, Properties.Settings.Default.TextureRandomizePlayBodies);
            HandleCategory(e, RegexPlaceables, Properties.Settings.Default.TextureRandomizePlaceables);
            HandleCategory(e, RegexParty,      Properties.Settings.Default.TextureRandomizeParty);
            HandleCategory(e, RegexStunt,      Properties.Settings.Default.TextureRandomizeStunt);
            HandleCategory(e, RegexVehicles,   Properties.Settings.Default.TextureRandomizeVehicles);
            HandleCategory(e, RegexWeapons,    Properties.Settings.Default.TextureRandomizeWeapons);

            // Handle other.
            switch (Properties.Settings.Default.TextureRandomizeOther)
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
            ws.Cell(i, 1).Value = "Seed";
            ws.Cell(i, 2).Value = Properties.Settings.Default.Seed;
            ws.Cell(i, 1).Style.Font.Bold = true;
            i++;

            // Texture Randomization Settings
            ws.Cell(i, 1).Value = "Texture Pack";
            ws.Cell(i, 2).Value = Properties.Settings.Default.TexturePack.ToDescription();
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
                new Tuple<string, string>("Cube Maps", Properties.Settings.Default.TextureRandomizeCubeMaps.ToString()),
                new Tuple<string, string>("Creatures", Properties.Settings.Default.TextureRandomizeCreatures.ToString()),
                new Tuple<string, string>("Effects", Properties.Settings.Default.TextureRandomizeEffects.ToString()),
                new Tuple<string, string>("Items", Properties.Settings.Default.TextureRandomizeItems.ToString()),
                new Tuple<string, string>("Planetary", Properties.Settings.Default.TextureRandomizePlanetary.ToString()),
                new Tuple<string, string>("NPC", Properties.Settings.Default.TextureRandomizeNPC.ToString()),
                new Tuple<string, string>("Player Heads", Properties.Settings.Default.TextureRandomizePlayHeads.ToString()),
                new Tuple<string, string>("Player Bodies", Properties.Settings.Default.TextureRandomizePlayBodies.ToString()),
                new Tuple<string, string>("Placeables", Properties.Settings.Default.TextureRandomizePlaceables.ToString()),
                new Tuple<string, string>("Party", Properties.Settings.Default.TextureRandomizeParty.ToString()),
                new Tuple<string, string>("Stunt", Properties.Settings.Default.TextureRandomizeStunt.ToString()),
                new Tuple<string, string>("Vehicles", Properties.Settings.Default.TextureRandomizeVehicles.ToString()),
                new Tuple<string, string>("Weapons", Properties.Settings.Default.TextureRandomizeWeapons.ToString()),
                new Tuple<string, string>("Other", Properties.Settings.Default.TextureRandomizeOther.ToString()),
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
                ws.Cell(i, 1).Value = hasChanged;
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
