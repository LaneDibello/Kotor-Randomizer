using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using KotOR_IO;
using System.IO;

namespace kotor_Randomizer_2
{
    public static class TextureRando
    {
        private static List<int> MaxRando { get; } = new List<int>();

        private static List<List<int>> TypeLists { get; } = new List<List<int>>();

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
                case 0: // High quality
                    pack_name = "\\swpc_tex_tpa.erf";
                    break;
                case 1: // Medium quality
                    pack_name = "\\swpc_tex_tpb.erf";
                    break;
                case 2: // Low quality
                    pack_name = "\\swpc_tex_tpc.erf";
                    break;
            }

            ERF e = new ERF(paths.TexturePacks + pack_name);

            // Handle categories.
            HandleCategory(e, RegexCubeMaps, Properties.Settings.Default.TextureRandomizeCubeMaps);
            HandleCategory(e, RegexCreatures, Properties.Settings.Default.TextureRandomizeCreatures);
            HandleCategory(e, RegexEffects, Properties.Settings.Default.TextureRandomizeEffects);
            HandleCategory(e, RegexItems, Properties.Settings.Default.TextureRandomizeItems);
            HandleCategory(e, RegexPlanetary, Properties.Settings.Default.TextureRandomizePlanetary);
            HandleCategory(e, RegexNPC, Properties.Settings.Default.TextureRandomizeNPC);
            HandleCategory(e, RegexPlayHeads, Properties.Settings.Default.TextureRandomizePlayHeads);
            HandleCategory(e, RegexPlayBodies, Properties.Settings.Default.TextureRandomizePlayBodies);
            HandleCategory(e, RegexPlaceables, Properties.Settings.Default.TextureRandomizePlaceables);
            HandleCategory(e, RegexParty, Properties.Settings.Default.TextureRandomizeParty);
            HandleCategory(e, RegexStunt, Properties.Settings.Default.TextureRandomizeStunt);
            HandleCategory(e, RegexVehicles, Properties.Settings.Default.TextureRandomizeVehicles);
            HandleCategory(e, RegexWeapons, Properties.Settings.Default.TextureRandomizeWeapons);

            // Handle other.
            switch ((RandomizationLevel)Properties.Settings.Default.TextureRandomizeOther)
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

        private static void HandleCategory(ERF e, Regex r, int randomizationlevel)
        {
            switch ((RandomizationLevel)randomizationlevel)
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
    }
}
