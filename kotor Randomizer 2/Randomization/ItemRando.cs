using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using KotOR_IO;
using System.Text.RegularExpressions;
using ClosedXML.Excel;
using kotor_Randomizer_2.Models;

namespace kotor_Randomizer_2
{
    public static class ItemRando
    {
        #region Private Properties
        //private static KPaths             Paths                { get; set; }
        private static RandomizationLevel RandomizeArmbands    { get; set; }
        private static RandomizationLevel RandomizeArmor       { get; set; }
        private static RandomizationLevel RandomizeBelts       { get; set; }
        private static RandomizationLevel RandomizeBlasters    { get; set; }
        private static RandomizationLevel RandomizeHides       { get; set; }
        private static RandomizationLevel RandomizeCreature    { get; set; }
        private static RandomizationLevel RandomizeDroid       { get; set; }
        private static RandomizationLevel RandomizeGloves      { get; set; }
        private static RandomizationLevel RandomizeGrenades    { get; set; }
        private static RandomizationLevel RandomizeImplants    { get; set; }
        private static RandomizationLevel RandomizeLightsabers { get; set; }
        private static RandomizationLevel RandomizeMask        { get; set; }
        private static RandomizationLevel RandomizeMelee       { get; set; }
        private static RandomizationLevel RandomizeMines       { get; set; }
        private static RandomizationLevel RandomizePaz         { get; set; }
        private static RandomizationLevel RandomizeStims       { get; set; }
        private static RandomizationLevel RandomizeUpgrade     { get; set; }
        private static RandomizationLevel RandomizeVarious     { get; set; }
        private static List<string>       OmittedItems         { get; set; }
        private static string             OmitPreset           { get; set; }
        #endregion Private Properties

        /// <summary>
        /// A lookup table used to know how the items are randomized.
        /// Usage: List(Old ID, New ID)
        /// </summary>
        internal static List<Tuple<string, string>> LookupTable { get; set; } = new List<Tuple<string, string>>();

        /// <summary>
        /// Creates backups for files modified during this randomization.
        /// </summary>
        /// <param name="paths"></param>
        internal static void CreateItemBackups(KPaths paths)
        {
            paths.BackUpChitinFile();
        }

        /// <summary>
        /// Randomizes the types of items requested.
        /// </summary>
        /// <param name="paths">KPaths object for this game.</param>
        /// <param name="k1rando">Kotor1Randomizer object that contains settings to use.</param>
        public static void item_rando(KPaths paths, Kotor1Randomizer k1rando = null)
        {
            // Prepare for new randomization.
            Reset();
            AssignSettings(k1rando);

            // Load KEY file.
            KEY k = new KEY(paths.chitin);

            // Handle categories
            HandleCategory(k, ArmbandsRegs,    RandomizeArmbands);
            HandleCategory(k, ArmorRegs,       RandomizeArmor);
            HandleCategory(k, BeltsRegs,       RandomizeBelts);
            HandleCategory(k, BlastersRegs,    RandomizeBlasters);
            HandleCategory(k, HidesRegs,       RandomizeHides);
            HandleCategory(k, CreatureRegs,    RandomizeCreature);
            HandleCategory(k, DroidRegs,       RandomizeDroid);
            HandleCategory(k, GlovesRegs,      RandomizeGloves);
            HandleCategory(k, GrenadesRegs,    RandomizeGrenades);
            HandleCategory(k, ImplantsRegs,    RandomizeImplants);
            HandleCategory(k, LightsabersRegs, RandomizeLightsabers);
            HandleCategory(k, MaskRegs,        RandomizeMask);
            HandleCategory(k, MeleeRegs,       RandomizeMelee);
            HandleCategory(k, MinesRegs,       RandomizeMines);
            HandleCategory(k, PazRegs,         RandomizePaz);
            HandleCategory(k, StimsRegs,       RandomizeStims);
            HandleCategory(k, UpgradeRegs,     RandomizeUpgrade);

            // Handle Various
            switch (RandomizeVarious)
            {
                default:
                case RandomizationLevel.None:
                    break;
                case RandomizationLevel.Type:
                    List<string> type = new List<string>(k.KeyTable.Where(x => Matches_None(x.ResRef) && !Is_Forbidden(x.ResRef) && x.ResourceType == (short)ResourceType.UTI).Select(x => x.ResRef));
                    Type_Lists.Add(type);
                    break;
                case RandomizationLevel.Max:
                    Max_Rando.AddRange(k.KeyTable.Where(x => Matches_None(x.ResRef) && !Is_Forbidden(x.ResRef) && x.ResourceType == (short)ResourceType.UTI).Select(x => x.ResRef));
                    break;
            }

            // Omitted Items
            foreach (var item in OmittedItems)
            {
                LookupTable.Add(new Tuple<string, string>(item, item));
            }

            // Max Rando
            List<string> Max_Rando_Iterator = new List<string>(Max_Rando);
            Randomize.FisherYatesShuffle(Max_Rando);
            int j = 0;
            foreach (KEY.KeyEntry ke in k.KeyTable.Where(x => Max_Rando_Iterator.Contains(x.ResRef)))
            {
                LookupTable.Add(new Tuple<string, string>(ke.ResRef, Max_Rando[j]));
                ke.ResRef = Max_Rando[j];
                j++;
            }

            // Type Rando
            foreach (List<string> li in Type_Lists)
            {
                List<string> type_copy = new List<string>(li);
                Randomize.FisherYatesShuffle(type_copy);
                j = 0;
                foreach (KEY.KeyEntry ke in k.KeyTable.Where(x => li.Contains(x.ResRef)))
                {
                    LookupTable.Add(new Tuple<string, string>(ke.ResRef, type_copy[j]));
                    ke.ResRef = type_copy[j];
                    j++;
                }
            }

            k.WriteToFile(paths.chitin);
        }

        private static void AssignSettings(Kotor1Randomizer k1rando)
        {
            if (k1rando == null)
            {
                //Paths = new KPaths(Properties.Settings.Default.Kotor1Path);
                RandomizeArmbands = Properties.Settings.Default.RandomizeArmbands;
                RandomizeArmor = Properties.Settings.Default.RandomizeArmor;
                RandomizeBelts = Properties.Settings.Default.RandomizeBelts;
                RandomizeBlasters = Properties.Settings.Default.RandomizeBlasters;
                RandomizeHides = Properties.Settings.Default.RandomizeHides;
                RandomizeCreature = Properties.Settings.Default.RandomizeCreature;
                RandomizeDroid = Properties.Settings.Default.RandomizeDroid;
                RandomizeGloves = Properties.Settings.Default.RandomizeGloves;
                RandomizeGrenades = Properties.Settings.Default.RandomizeGrenades;
                RandomizeImplants = Properties.Settings.Default.RandomizeImplants;
                RandomizeLightsabers = Properties.Settings.Default.RandomizeLightsabers;
                RandomizeMask = Properties.Settings.Default.RandomizeMask;
                RandomizeMelee = Properties.Settings.Default.RandomizeMelee;
                RandomizeMines = Properties.Settings.Default.RandomizeMines;
                RandomizePaz = Properties.Settings.Default.RandomizePaz;
                RandomizeStims = Properties.Settings.Default.RandomizeStims;
                RandomizeUpgrade = Properties.Settings.Default.RandomizeUpgrade;
                RandomizeVarious = Properties.Settings.Default.RandomizeVarious;
                OmittedItems = Globals.OmitItems.ToList();
                OmitPreset = "";
            }
            else
            {
                //Paths = k1rando.Paths;
                RandomizeArmbands = k1rando.ItemArmbands;
                RandomizeArmor = k1rando.ItemArmor;
                RandomizeBelts = k1rando.ItemBelts;
                RandomizeBlasters = k1rando.ItemBlasters;
                RandomizeHides = k1rando.ItemCreatureHides;
                RandomizeCreature = k1rando.ItemCreatureWeapons;
                RandomizeDroid = k1rando.ItemDroidEquipment;
                RandomizeGloves = k1rando.ItemGloves;
                RandomizeGrenades = k1rando.ItemGrenades;
                RandomizeImplants = k1rando.ItemImplants;
                RandomizeLightsabers = k1rando.ItemLightsabers;
                RandomizeMask = k1rando.ItemMasks;
                RandomizeMelee = k1rando.ItemMeleeWeapons;
                RandomizeMines = k1rando.ItemMines;
                RandomizePaz = k1rando.ItemPazaakCards;
                RandomizeStims = k1rando.ItemMedical;
                RandomizeUpgrade = k1rando.ItemUpgrades;
                RandomizeVarious = k1rando.ItemVarious;
                OmittedItems = k1rando.ItemOmittedList.Select(x => x.Code).ToList();
                OmitPreset = k1rando.ItemOmittedPreset;
            }
        }

        private static void HandleCategory(KEY k, List<Regex> r, RandomizationLevel randomizationLevel)
        {
            switch (randomizationLevel)
            {
                case RandomizationLevel.None:
                default:
                    break;
                case RandomizationLevel.Subtype:
                    for (int i = 1; i < r.Count; i++)
                    {
                        List<string> temp = new List<string>(k.KeyTable.Where(x => r[i].IsMatch(x.ResRef) && !Is_Forbidden(x.ResRef) && x.ResourceType == (short)ResourceType.UTI).Select(x => x.ResRef));
                        Type_Lists.Add(temp);
                    }
                    break;
                case RandomizationLevel.Type:
                    List<string> type = new List<string>(k.KeyTable.Where(x => r[0].IsMatch(x.ResRef) && !Is_Forbidden(x.ResRef) && x.ResourceType == (short)ResourceType.UTI).Select(x => x.ResRef));
                    Type_Lists.Add(type);
                    break;
                case RandomizationLevel.Max:
                    Max_Rando.AddRange(k.KeyTable.Where(x => r[0].IsMatch(x.ResRef) && !Is_Forbidden(x.ResRef) && x.ResourceType == (short)ResourceType.UTI).Select(x => x.ResRef));
                    break;
            }
        }

        private static bool Is_Forbidden(string s)
        {
            return OmittedItems.Contains(s);
        }

        private static bool Matches_None(string s)
        {
            return
                (
                    !ArmorRegs[0].IsMatch(s) &&
                    !StimsRegs[0].IsMatch(s) &&
                    !BeltsRegs[0].IsMatch(s) &&
                    !HidesRegs[0].IsMatch(s) &&
                    !DroidRegs[0].IsMatch(s) &&
                    !ArmbandsRegs[0].IsMatch(s) &&
                    !GlovesRegs[0].IsMatch(s) &&
                    !ImplantsRegs[0].IsMatch(s) &&
                    !MaskRegs[0].IsMatch(s) &&
                    !PazRegs[0].IsMatch(s) &&
                    !MinesRegs[0].IsMatch(s) &&
                    !UpgradeRegs[0].IsMatch(s) &&
                    !BlastersRegs[0].IsMatch(s) &&
                    !CreatureRegs[0].IsMatch(s) &&
                    !LightsabersRegs[0].IsMatch(s) &&
                    !GrenadesRegs[0].IsMatch(s) &&
                    !MeleeRegs[0].IsMatch(s)
                );
        }

        private static List<string> Max_Rando = new List<string>();

        private static List<List<string>> Type_Lists = new List<List<string>>();

        ///// <summary>
        ///// Creates a CSV file containing a list of the changes made during randomization.
        ///// If the file already exists, this method will append the data.
        ///// If no randomization has been performed, no file will be created.
        ///// </summary>
        ///// <param name="path">Path to desired output file.</param>
        //public static void GenerateSpoilerLog(string path)
        //{
        //    if (LookupTable.Count == 0) { return; }
        //    var sortedLookup = LookupTable.OrderBy(x => x.Item1);

        //    using (StreamWriter sw = new StreamWriter(path))
        //    {
        //        //sw.WriteLine("Items,");
        //        sw.WriteLine($"Seed,{Properties.Settings.Default.Seed}");
        //        sw.WriteLine();

        //        sw.WriteLine("Item Type,Rando Level");
        //        sw.WriteLine($"Armbands,{Properties.Settings.Default.RandomizeArmbands}");
        //        sw.WriteLine($"Armor,{Properties.Settings.Default.RandomizeArmor}");
        //        sw.WriteLine($"Belts,{Properties.Settings.Default.RandomizeBelts}");
        //        sw.WriteLine($"Blasters,{Properties.Settings.Default.RandomizeBlasters}");
        //        sw.WriteLine($"Creature Hides,{Properties.Settings.Default.RandomizeHides}");
        //        sw.WriteLine($"Creature Weapons,{Properties.Settings.Default.RandomizeCreature}");
        //        sw.WriteLine($"Droid Equipment,{Properties.Settings.Default.RandomizeDroid}");
        //        sw.WriteLine($"Gauntlets,{Properties.Settings.Default.RandomizeGloves}");
        //        sw.WriteLine($"Grenades,{Properties.Settings.Default.RandomizeGrenades}");
        //        sw.WriteLine($"Implants,{Properties.Settings.Default.RandomizeImplants}");
        //        sw.WriteLine($"Lightsabers,{Properties.Settings.Default.RandomizeLightsabers}");
        //        sw.WriteLine($"Masks,{Properties.Settings.Default.RandomizeMask}");
        //        sw.WriteLine($"Melee Weapons,{Properties.Settings.Default.RandomizeMelee}");
        //        sw.WriteLine($"Mines,{Properties.Settings.Default.RandomizeMines}");
        //        sw.WriteLine($"Pazaak Cards,{Properties.Settings.Default.RandomizePaz}");
        //        sw.WriteLine($"Stims/Medpacs,{Properties.Settings.Default.RandomizeStims}");
        //        sw.WriteLine($"Upgrades/Crystals,{Properties.Settings.Default.RandomizeUpgrade}");
        //        sw.WriteLine($"Various,{Properties.Settings.Default.RandomizeVarious}");
        //        sw.WriteLine();

        //        sw.WriteLine("Omitted Items");
        //        foreach (var item in Globals.OmitItems)
        //        {
        //            sw.WriteLine(item);
        //        }
        //        sw.WriteLine();

        //        sw.WriteLine("Has Changed,Original,Randomized");
        //        foreach (var tpl in sortedLookup)
        //        {
        //            sw.WriteLine($"{(tpl.Item1 != tpl.Item2).ToString()},{tpl.Item1},{tpl.Item2}");
        //        }
        //        sw.WriteLine();
        //    }
        //}

        internal static void Reset()
        {
            // Prepare lists for new randomization.
            Max_Rando.Clear();
            Type_Lists.Clear();
            LookupTable.Clear();
        }

        public static void CreateSpoilerLog(XLWorkbook workbook)
        {
            if (LookupTable.Count == 0) { return; }
            var ws = workbook.Worksheets.Add("Item");

            //KEY k = new KEY(Paths.chitin_backup);
            //BIF b = new BIF(Path.Combine(Paths.data, "templates.bif"));
            //b.AttachKey(k, "data\\templates.bif");
            //var items = b.VariableResourceTable.Where(x => x.ResourceType == ResourceType.UTI);
            //TLK t = new TLK(File.Exists(Paths.dialog_backup) ? Paths.dialog_backup : Paths.dialog);

            int i = 1;
            //ws.Cell(i, 1).Value = "Seed";
            //ws.Cell(i, 2).Value = Properties.Settings.Default.Seed;
            //ws.Cell(i, 1).Style.Font.Bold = true;
            //i++;

            Version version = typeof(StartForm).Assembly.GetName().Version;
            ws.Cell(i, 1).Value = "Version";
            ws.Cell(i, 1).Style.Font.Bold = true;
            ws.Cell(i, 2).Value = $"v{version.Major}.{version.Minor}.{version.Build}";
            ws.Cell(i, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            i += 2;     // Skip a row.

            // Item Randomization Settings
            ws.Cell(i, 1).Value = "Item Type";
            ws.Cell(i, 2).Value = "Rando Level";
            ws.Cell(i, 1).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            ws.Cell(i, 2).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            ws.Cell(i, 1).Style.Font.Bold = true;
            ws.Cell(i, 2).Style.Font.Bold = true;
            i++;

            string presetName = OmitPreset;
            bool isCustomPreset = false;
            if (string.IsNullOrWhiteSpace(OmitPreset))
            {
                presetName = "Custom";
                isCustomPreset = true;
            }

            var settings = new List<Tuple<string, string>>()
            {
                new Tuple<string, string>("Armbands",          RandomizeArmbands.ToString()),
                new Tuple<string, string>("Armor",             RandomizeArmor.ToString()),
                new Tuple<string, string>("Belts",             RandomizeBelts.ToString()),
                new Tuple<string, string>("Blasters",          RandomizeBlasters.ToString()),
                new Tuple<string, string>("Creature Hides",    RandomizeHides.ToString()),
                new Tuple<string, string>("Creature Weapons",  RandomizeCreature.ToString()),
                new Tuple<string, string>("Droid Equipment",   RandomizeDroid.ToString()),
                new Tuple<string, string>("Gauntlets",         RandomizeGloves.ToString()),
                new Tuple<string, string>("Grenades",          RandomizeGrenades.ToString()),
                new Tuple<string, string>("Implants",          RandomizeImplants.ToString()),
                new Tuple<string, string>("Lightsabers",       RandomizeLightsabers.ToString()),
                new Tuple<string, string>("Masks",             RandomizeMask.ToString()),
                new Tuple<string, string>("Melee Weapons",     RandomizeMelee.ToString()),
                new Tuple<string, string>("Mines",             RandomizeMines.ToString()),
                new Tuple<string, string>("Pazaak Cards",      RandomizePaz.ToString()),
                new Tuple<string, string>("Stims/Medpacs",     RandomizeStims.ToString()),
                new Tuple<string, string>("Upgrades/Crystals", RandomizeUpgrade.ToString()),
                new Tuple<string, string>("Various",           RandomizeVarious.ToString()),
                new Tuple<string, string>("", ""),
                new Tuple<string, string>("Item Omit Preset",  presetName),
            };

            foreach (var setting in settings)
            {
                ws.Cell(i, 1).Value = setting.Item1;
                ws.Cell(i, 2).Value = setting.Item2;
                ws.Cell(i, 1).Style.Font.Italic = true;
                i++;
            }

            i++;    // Skip a row.

            // Omitted Items
            if (isCustomPreset)
            {
                int iMax = i;
                i = 3;  // Restart at the top of the settings list.

                ws.Cell(i, 4).Value = "Omitted Items";
                ws.Cell(i, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Cell(i, 4).Style.Font.Bold = true;
                ws.Range(i, 4, i, 5).Merge();
                i++;

                ws.Cell(i, 4).Value = "ID";
                ws.Cell(i, 5).Value = "Label";
                ws.Cell(i, 4).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                ws.Cell(i, 5).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                ws.Cell(i, 4).Style.Font.Italic = true;
                ws.Cell(i, 5).Style.Font.Italic = true;
                i++;

                var sortedList = OmittedItems.ToList();
                sortedList.Sort();

                foreach (var item in sortedList)
                {
                    ws.Cell(i, 4).Value = item;
                    var origItemName = Globals.ITEM_LIST_FULL.FirstOrDefault(ri => ri.Code == item)?.Label ?? "";

                    //var origItemVre = items.FirstOrDefault(x => x.ResRef == item);
                    //if (origItemVre != null)
                    //{
                    //    GFF origItem = new GFF(origItemVre.EntryData);
                    //    if (origItem.Top_Level.Fields.FirstOrDefault(x => x.Label == "LocalizedName") is GFF.CExoLocString field)
                    //        origItemName = t.String_Data_Table[field.StringRef].StringText;
                    //}

                    ws.Cell(i, 5).Value = origItemName;
                    i++;
                }

                // Handle variable length of omitted items list.
                if (iMax > i) i = iMax; // Return to the bottom of the settings list.
                else i++;      // Skip a row.
            }

            i++;    // Skip an additional 2 rows.

            // Randomized Items
            ws.Cell(i,   1).Value = "Has Changed";
            ws.Cell(i-1, 2).Value = "Original (New Item)";
            ws.Cell(i,   2).Value = "ID";
            ws.Cell(i,   3).Value = "Label";
            ws.Cell(i-1, 4).Value = "Randomized (Old Item)";
            ws.Cell(i,   4).Value = "ID";
            ws.Cell(i,   5).Value = "Label";
            ws.Cell(i,   1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Cell(i-1, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Cell(i-1, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Cell(i,   1).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            ws.Cell(i,   2).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            ws.Cell(i,   3).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            ws.Cell(i,   4).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            ws.Cell(i,   5).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            ws.Cell(i-1, 2).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
            ws.Cell(i,   2).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
            ws.Cell(i-1, 4).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
            ws.Cell(i,   4).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
            ws.Cell(i-1, 6).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
            ws.Cell(i,   6).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
            ws.Cell(i,   1).Style.Font.Bold = true;
            ws.Cell(i-1, 2).Style.Font.Bold = true;
            ws.Cell(i,   2).Style.Font.Italic = true;
            ws.Cell(i,   3).Style.Font.Italic = true;
            ws.Cell(i-1, 4).Style.Font.Bold = true;
            ws.Cell(i,   4).Style.Font.Italic = true;
            ws.Cell(i,   5).Style.Font.Italic = true;
            ws.Range(i-1, 2, i-1, 3).Merge();
            ws.Range(i-1, 4, i-1, 5).Merge();
            i++;

            var sortedLookup = LookupTable.OrderBy(tpl => tpl.Item1);
            foreach (var tpl in sortedLookup)
            {
                var omitted = OmittedItems.Any(x => x == tpl.Item1);
                var changed = tpl.Item1 != tpl.Item2;   // Has the shuffle changed this item?
                string origItemName = Globals.ITEM_LIST_FULL.FirstOrDefault(ri => ri.Code == tpl.Item1)?.Label ?? "";
                string randItemName = changed ? Globals.ITEM_LIST_FULL.FirstOrDefault(ri => ri.Code == tpl.Item2)?.Label ?? "" : origItemName;

                //var origItemVre = items.FirstOrDefault(x => x.ResRef == tpl.Item1);
                //if (origItemVre != null)
                //{
                //    GFF origItem = new GFF(origItemVre.EntryData);
                //    if (origItem.Top_Level.Fields.FirstOrDefault(x => x.Label == "LocalizedName") is GFF.CExoLocString field)
                //        origItemName = t.String_Data_Table[field.StringRef].StringText;
                //}

                //if (changed)
                //{
                //    var randItemVre = items.FirstOrDefault(x => x.ResRef == tpl.Item2);
                //    if (randItemVre != null)
                //    {
                //        GFF randItem = new GFF(randItemVre.EntryData);
                //        if (randItem.Top_Level.Fields.FirstOrDefault(x => x.Label == "LocalizedName") is GFF.CExoLocString field)
                //            randItemName = t.String_Data_Table[field.StringRef].StringText;
                //    }
                //}
                //else
                //{
                //    randItemName = origItemName;
                //}

                ws.Cell(i, 1).Value = omitted ? "OMITTED" : changed.ToString();
                ws.Cell(i, 2).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                ws.Cell(i, 2).Value = tpl.Item1;
                ws.Cell(i, 3).Value = origItemName;
                ws.Cell(i, 4).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                ws.Cell(i, 4).Value = tpl.Item2;
                ws.Cell(i, 5).Value = randItemName;
                ws.Cell(i, 6).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                if (omitted)
                {
                    // Center "OMITTED" text.
                    ws.Cell(i, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                }
                else
                {
                    // Set color of "Has Changed" column. Booleans are automatically centered.
                    if (changed) ws.Cell(i, 1).Style.Font.FontColor = XLColor.Green;
                    else         ws.Cell(i, 1).Style.Font.FontColor = XLColor.Red;
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

        #region Regexes
        //Armor Regexes
        static List<Regex> ArmorRegs = new List<Regex>()
        {
            new Regex("^g1*_a_|^geno_armor", RegexOptions.Compiled | RegexOptions.IgnoreCase),// All Armor

            new Regex("^g1*_a_class4|^geno_armor", RegexOptions.Compiled | RegexOptions.IgnoreCase),//Armor Class 4
            new Regex("^g1*_a_class5", RegexOptions.Compiled | RegexOptions.IgnoreCase),//Armor Class 5
            new Regex("^g1*_a_class6", RegexOptions.Compiled | RegexOptions.IgnoreCase),//Armor Class 6
            new Regex("^g1*_a_class7", RegexOptions.Compiled | RegexOptions.IgnoreCase),//Armor Class 7
            new Regex("^g1*_a_class8", RegexOptions.Compiled | RegexOptions.IgnoreCase),//Armor Class 8
            new Regex("^g1*_a_class9", RegexOptions.Compiled | RegexOptions.IgnoreCase),//Armor Class 9
            new Regex("^g1*_a_clothes", RegexOptions.Compiled | RegexOptions.IgnoreCase),//Clothes
            new Regex("^g1*_a_jedi", RegexOptions.Compiled | RegexOptions.IgnoreCase),//Basic Robes
            new Regex("^g1*_a_kght", RegexOptions.Compiled | RegexOptions.IgnoreCase),//Knight Robes
            new Regex("^g1*_a_mstr", RegexOptions.Compiled | RegexOptions.IgnoreCase),//Master Robes

        };

        //Stims Regexes
        static List<Regex> StimsRegs = new List<Regex>()
        {
            new Regex("^g1*_i_(adrn|cmbt|medeq)", RegexOptions.Compiled | RegexOptions.IgnoreCase),//All Stims/Medpacs

            new Regex("^g1*_i_adrn", RegexOptions.Compiled | RegexOptions.IgnoreCase),//Adrenals
            new Regex("^g1*_i_cmbt", RegexOptions.Compiled | RegexOptions.IgnoreCase),//Battle Stims
            new Regex("^g1*_i_medeq", RegexOptions.Compiled | RegexOptions.IgnoreCase),//Medpacs
        };

        //Belt Regexs
        static List<Regex> BeltsRegs = new List<Regex>()
        {
            new Regex("^g1*_i_belt|^geno_stealth", RegexOptions.Compiled | RegexOptions.IgnoreCase)//All Belts
        };

        ////Various Regexes
        //private static Regex RegexBith { get { return new Regex("^g1*_i_bith", RegexOptions.Compiled | RegexOptions.IgnoreCase); } }//Bith items
        //private static Regex Regexcredits { get { return new Regex("^g1*_i_credit", RegexOptions.Compiled | RegexOptions.IgnoreCase); } }//Credits

        //Creature Hides
        static List<Regex> HidesRegs = new List<Regex>()
        {
            new Regex("^g1*_i_crhide", RegexOptions.Compiled | RegexOptions.IgnoreCase)//Creature Hides
        };

        //Droid equipment 
        static List<Regex> DroidRegs = new List<Regex>()
        {
            new Regex("^g1*_i_drd", RegexOptions.Compiled | RegexOptions.IgnoreCase),//All Droid Equipment

            new Regex("^g1*_i_drd.{0,2}plat", RegexOptions.Compiled | RegexOptions.IgnoreCase),//Droid Plating
            new Regex("^g1*_i_drd(comspk|secspk)", RegexOptions.Compiled | RegexOptions.IgnoreCase),//Droid probes
            new Regex("^g1*_i_drd(mtn|snc)sen", RegexOptions.Compiled | RegexOptions.IgnoreCase),//Droid Sensors
            new Regex("^g1*_i_drdrep", RegexOptions.Compiled | RegexOptions.IgnoreCase),//Droid repair kits
            new Regex("^g1*_i_drdshld", RegexOptions.Compiled | RegexOptions.IgnoreCase),//Droid Shields
            new Regex("^g1*_i_drdsrc", RegexOptions.Compiled | RegexOptions.IgnoreCase),//Droid Equipment
            new Regex("^g1*_i_drdtrgcom", RegexOptions.Compiled | RegexOptions.IgnoreCase),//Droid Computers
            new Regex("^g1*_i_drdutldev", RegexOptions.Compiled | RegexOptions.IgnoreCase),//Droid Devices
        };

        //Armbands
        static List<Regex> ArmbandsRegs = new List<Regex>()
        {
            new Regex("^g1*_i_frarmbnds", RegexOptions.Compiled | RegexOptions.IgnoreCase),//All Armbands

            new Regex("^g1*_i_frarmbnds0", RegexOptions.Compiled | RegexOptions.IgnoreCase),//Shields
            new Regex("^g1*_i_frarmbnds(1|2)", RegexOptions.Compiled | RegexOptions.IgnoreCase),//Stats
        };

        //Gauntlets
        static List<Regex> GlovesRegs = new List<Regex>()
        {
            new Regex("^g1*_i_gauntlet|^geno_gloves", RegexOptions.Compiled | RegexOptions.IgnoreCase)//Gloves
        };

        //Implants
        static List<Regex> ImplantsRegs = new List<Regex>()
        {
            new Regex("^g1*_i_implant", RegexOptions.Compiled | RegexOptions.IgnoreCase),//All Implants

            new Regex("^g1*_i_implant1", RegexOptions.Compiled | RegexOptions.IgnoreCase),//Implant level 1
            new Regex("^g1*_i_implant2", RegexOptions.Compiled | RegexOptions.IgnoreCase),//Implant level 2
            new Regex("^g1*_i_implant3", RegexOptions.Compiled | RegexOptions.IgnoreCase),//Implant level 3
        };

        //Mask
        static List<Regex> MaskRegs = new List<Regex>()
        {
            new Regex("^g1*_i_mask|^geno_visor", RegexOptions.Compiled | RegexOptions.IgnoreCase),//All Masks

            new Regex("^g1*_i_mask(08|09|10|11|13|16|17|18|22|23|24)", RegexOptions.Compiled | RegexOptions.IgnoreCase),//Mask No Armor Prof
            new Regex("^g1*_i_mask(01|02|03|04|05|07|19|20|21)|^geno_visor", RegexOptions.Compiled | RegexOptions.IgnoreCase),//Mask Light
            new Regex("^g1*_i_mask(06|12|15)", RegexOptions.Compiled | RegexOptions.IgnoreCase),//Mask Medium
            new Regex("^g1*_i_mask14", RegexOptions.Compiled | RegexOptions.IgnoreCase),//Mask Heavy
        };

        //Paz
        static List<Regex> PazRegs = new List<Regex>()
        {
            new Regex("^g1*_i_pazcard", RegexOptions.Compiled | RegexOptions.IgnoreCase)//Pazaak Cards
        };

        //Mines
        static List<Regex> MinesRegs = new List<Regex>()
        {
            new Regex("^g1*_i_trapkit", RegexOptions.Compiled | RegexOptions.IgnoreCase)//Mines
        };

        //Upgrades/Crystals
        static List<Regex> UpgradeRegs = new List<Regex>()
        {
            new Regex("^g1*_(i_upgrade|w_sbrcrstl)|^kas25_wookcrysta|^tat18_dragonprl", RegexOptions.Compiled | RegexOptions.IgnoreCase),//All Upgrades

            new Regex("^g1*_i_upgrade", RegexOptions.Compiled | RegexOptions.IgnoreCase),//Normal Upgrades
            new Regex("(^g1*_w_sbrcrstl(0|1([1-3]|9))|^kas25_wookcrysta|^tat18_dragonprl)", RegexOptions.Compiled | RegexOptions.IgnoreCase),//Crystal Upgrades
            new Regex("^g1*_w_sbrcrstl(1[4-8]|2)", RegexOptions.Compiled | RegexOptions.IgnoreCase),//Crystal Colors
        };

        //Blaster
        static List<Regex> BlastersRegs = new List<Regex>()
        {
            new Regex("^g1*_(w_.*(bls*tr*|rfl|pstl|cstr)|i_bithitem)|geno_blaster", RegexOptions.Compiled | RegexOptions.IgnoreCase),//All Blasters

            new Regex("^g1*_w_.*(rptn)", RegexOptions.Compiled | RegexOptions.IgnoreCase),//Heavy Weapons
            new Regex("^g1*_(w_.*(pstl|hldoblst|hvyblstr|ionblstr)|i_bithitem)|geno_blaster", RegexOptions.Compiled | RegexOptions.IgnoreCase),//Blaster Pistols
            new Regex("^g1*_w_.*(crbn|rfl|cstr)", RegexOptions.Compiled | RegexOptions.IgnoreCase),//Blaster Rifles
        };

        //Creature Weapons
        static List<Regex> CreatureRegs = new List<Regex>()
        {
            new Regex("^g1*_w_cr(go|sl)", RegexOptions.Compiled | RegexOptions.IgnoreCase),//All Creature weapons

            new Regex("^g1*_w_crgore", RegexOptions.Compiled | RegexOptions.IgnoreCase),//Piercing Creature Weapons
            new Regex("^g1*_w_crslash", RegexOptions.Compiled | RegexOptions.IgnoreCase),//Slashing Creature Weapons
            new Regex("^g1*_w_crslprc", RegexOptions.Compiled | RegexOptions.IgnoreCase),//Piercing/slashing Creature weapons
        };

        //Lightsabers
        static List<Regex> LightsabersRegs = new List<Regex>()
        {
            new Regex("^g1*_w_.{1,}sbr", RegexOptions.Compiled | RegexOptions.IgnoreCase),//All Lightsabers

            new Regex("^g1*_w_dblsbr", RegexOptions.Compiled | RegexOptions.IgnoreCase),//Double Lightsabers
            new Regex("^g1*_w_(lght|drkjdi)sbr", RegexOptions.Compiled | RegexOptions.IgnoreCase),//Regular Lightsabers
            new Regex("^g1*_w_shortsbr", RegexOptions.Compiled | RegexOptions.IgnoreCase),//Short Lightsabers
        };

        //Grenades
        static List<Regex> GrenadesRegs = new List<Regex>()
        {
            new Regex("^g1*_w_(.*gren|thermldet)", RegexOptions.Compiled | RegexOptions.IgnoreCase)//Grenades
        };

        //Melee
        static List<Regex> MeleeRegs = new List<Regex>()
        {
            new Regex("^g1*_w_(stunbaton|war|.*swr*d|vi*bro|gaffi|qtrstaff)|^geno_blade", RegexOptions.Compiled | RegexOptions.IgnoreCase),//All Melee Weapons

            new Regex("^g1*_w_stunbaton", RegexOptions.Compiled | RegexOptions.IgnoreCase),//Stun Batons
            new Regex("^g1*_w_lngswrd", RegexOptions.Compiled | RegexOptions.IgnoreCase),//Long Swords
            new Regex("^g1*_w_shortswrd", RegexOptions.Compiled | RegexOptions.IgnoreCase),//Short Swords
            new Regex("^g1*_w_vbroshort", RegexOptions.Compiled | RegexOptions.IgnoreCase),//Vibro Shortblades
            new Regex("^g1*_w_vbroswrd|^geno_blade", RegexOptions.Compiled | RegexOptions.IgnoreCase),//Vibroblades
            new Regex("^g1*_w_dblswrd", RegexOptions.Compiled | RegexOptions.IgnoreCase),//Double Swords
            new Regex("^g1*_w_qtrstaff", RegexOptions.Compiled | RegexOptions.IgnoreCase),//Quarter Staves
            new Regex("^g1*_w_vbrdblswd", RegexOptions.Compiled | RegexOptions.IgnoreCase),//Vibro Doubleblades
            new Regex("^g1*_w_war", RegexOptions.Compiled | RegexOptions.IgnoreCase),//War blade/axes
        };
        #endregion
    }
}
