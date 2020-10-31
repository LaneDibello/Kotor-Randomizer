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
        public static void texture_rando(Globals.KPaths paths)
        {
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
            ERF e = KReader.ReadERF(File.OpenRead(paths.TexturePacks + pack_name));

            // Handle categories.
            handle_category(e, RegexCubeMaps, Properties.Settings.Default.TextureRandomizeCubeMaps);
            handle_category(e, RegexCreatures, Properties.Settings.Default.TextureRandomizeCreatures);
            handle_category(e, RegexEffects, Properties.Settings.Default.TextureRandomizeEffects);
            handle_category(e, RegexItems, Properties.Settings.Default.TextureRandomizeItems);
            handle_category(e, RegexPlanetary, Properties.Settings.Default.TextureRandomizePlanetary);
            handle_category(e, RegexNPC, Properties.Settings.Default.TextureRandomizeNPC);
            handle_category(e, RegexPlayHeads, Properties.Settings.Default.TextureRandomizePlayHeads);
            handle_category(e, RegexPlayBodies, Properties.Settings.Default.TextureRandomizePlayBodies);
            handle_category(e, RegexPlaceables, Properties.Settings.Default.TextureRandomizePlaceables);
            handle_category(e, RegexParty, Properties.Settings.Default.TextureRandomizeParty);
            handle_category(e, RegexStunt, Properties.Settings.Default.TextureRandomizeStunt);
            handle_category(e, RegexVehicles, Properties.Settings.Default.TextureRandomizeVehicles);
            handle_category(e, RegexWeapons, Properties.Settings.Default.TextureRandomizeWeapons);

            // Handle other.
            switch ((RandomizationLevel)Properties.Settings.Default.TextureRandomizeOther)
            {
                default:
                case RandomizationLevel.None:
                    break; // Do nothing.
                case RandomizationLevel.Type:
                    List<int> type = new List<int>(e.Key_List.Where(x => Matches_None(x.ResRef) && !Is_Forbidden(x.ResRef)).Select(x => x.ResID));
                    Type_Lists.Add(type);
                    break;
                case RandomizationLevel.Max:
                    Max_Rando.AddRange(e.Key_List.Where(x => Matches_None(x.ResRef) && !Is_Forbidden(x.ResRef)).Select(x => x.ResID));
                    break;
            }

            // Max Rando.
            List<int> Max_Rando_Iterator = new List<int>(Max_Rando);
            Randomize.FisherYatesShuffle(Max_Rando);
            int j = 0;
            foreach (ERF.Key k in e.Key_List.Where(x => Max_Rando_Iterator.Contains(x.ResID)))
            {
                k.ResID = Max_Rando[j];
                j++;
            }

            // Type Rando.
            foreach (List<int> li in Type_Lists)
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

            kWriter.Write(e, File.OpenWrite(paths.TexturePacks + pack_name));
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

        private static List<int> Max_Rando = new List<int>();

        private static List<List<int>> Type_Lists = new List<List<int>>();

        private static void handle_category(ERF e, Regex r, int randomizationlevel)
        {
            switch ((RandomizationLevel)randomizationlevel)
            {
                default:
                case RandomizationLevel.None:
                    break; // Do nothing.
                case RandomizationLevel.Type:
                    List<int> type = new List<int>(e.Key_List.Where(x => r.IsMatch(x.ResRef) && !Is_Forbidden(x.ResRef)).Select(x => x.ResID));
                    Type_Lists.Add(type);
                    break;
                case RandomizationLevel.Max:
                    Max_Rando.AddRange(e.Key_List.Where(x => r.IsMatch(x.ResRef) && !Is_Forbidden(x.ResRef)).Select(x => x.ResID));
                    break;
            }
        }

        #region Regexes
        private static Regex RegexCubeMaps { get { return new Regex("^CM_", RegexOptions.Compiled | RegexOptions.IgnoreCase); } }
        private static Regex RegexCreatures { get { return new Regex("^C_", RegexOptions.Compiled | RegexOptions.IgnoreCase); } }
        private static Regex RegexEffects { get { return new Regex("^FX_", RegexOptions.Compiled | RegexOptions.IgnoreCase); } }
        private static Regex RegexItems { get { return new Regex("^I_", RegexOptions.Compiled | RegexOptions.IgnoreCase); } }
        private static Regex RegexPlanetary { get { return new Regex("^L.{2}_", RegexOptions.Compiled | RegexOptions.IgnoreCase); } }
        private static Regex RegexNPC { get { return new Regex("^N_", RegexOptions.Compiled | RegexOptions.IgnoreCase); } }
        private static Regex RegexPlayHeads { get { return new Regex("^P(F|M)H", RegexOptions.Compiled | RegexOptions.IgnoreCase); } }
        private static Regex RegexPlayBodies { get { return new Regex("^P(F|M)B", RegexOptions.Compiled | RegexOptions.IgnoreCase); } }
        private static Regex RegexPlaceables { get { return new Regex("^PLC_", RegexOptions.Compiled | RegexOptions.IgnoreCase); } }
        private static Regex RegexParty { get { return new Regex("^P_", RegexOptions.Compiled | RegexOptions.IgnoreCase); } }
        private static Regex RegexStunt { get { return new Regex("^Stunt", RegexOptions.Compiled | RegexOptions.IgnoreCase); } }
        private static Regex RegexVehicles { get { return new Regex("^V_", RegexOptions.Compiled | RegexOptions.IgnoreCase); } }
        private static Regex RegexWeapons { get { return new Regex("^W_", RegexOptions.Compiled | RegexOptions.IgnoreCase); } }
        #endregion
    }
}
