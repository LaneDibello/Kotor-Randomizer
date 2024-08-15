using System;
using System.Collections.Generic;
using System.Linq;
using KotOR_IO;
using System.Text.RegularExpressions;
using ClosedXML.Excel;
using kotor_Randomizer_2.Models;
using kotor_Randomizer_2.Extensions;
using kotor_Randomizer_2.DTOs;
using kotor_Randomizer_2.Interfaces;

namespace kotor_Randomizer_2
{
    public static class ItemRando
    {
        #region Private Properties
        private static List<ItemRandoCategoryOption> ItemCategories { get; set; } = new List<ItemRandoCategoryOption>();
        private static List<string> OmittedItems { get; set; }
        private static string OmitPreset { get; set; }
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
        /// <param name="rando">Object that contains item rando settings to use.</param>
        public static void item_rando(KPaths paths, IRandomizeItems rando = null)
        {
            // Prepare for new randomization.
            Reset();
            AssignSettings(rando);

            // Load KEY file.
            var k = new KEY(paths.chitin);

            // Handle categories
            foreach (var option in ItemCategories)
            {
                if (option.Category == ItemRandoCategory.Various) continue;   // Handle various separately.
                HandleCategory(k, option.Regex, option.Level);
            }

            // Handle Various
            if (ItemCategories.First(irco => irco.Category == ItemRandoCategory.Various) is ItemRandoCategoryOption various)
            {
                switch (various.Level)
                {
                    default:
                    case RandomizationLevel.None:
                    case RandomizationLevel.Subtype:
                        break;
                    case RandomizationLevel.Type:
                        var type = new List<string>(k.KeyTable.Where(x => Matches_None(x.ResRef) && !Is_Forbidden(x.ResRef) && x.ResourceType == (short)ResourceType.UTI).Select(x => x.ResRef));
                        Type_Lists.Add(type);
                        break;
                    case RandomizationLevel.Max:
                        Max_Rando.AddRange(k.KeyTable.Where(x => Matches_None(x.ResRef) && !Is_Forbidden(x.ResRef) && x.ResourceType == (short)ResourceType.UTI).Select(x => x.ResRef));
                        break;
                }
            }

            // Omitted Items
            foreach (var item in OmittedItems)
            {
                LookupTable.Add(new Tuple<string, string>(item, item));
            }

            // Max Rando
            var Max_Rando_Iterator = new List<string>(Max_Rando);
            Randomize.FisherYatesShuffle(Max_Rando);
            var j = 0;
            foreach (var ke in k.KeyTable.Where(x => Max_Rando_Iterator.Contains(x.ResRef)))
            {
                LookupTable.Add(new Tuple<string, string>(ke.ResRef, Max_Rando[j]));
                ke.ResRef = Max_Rando[j];
                j++;
            }

            // Type Rando
            foreach (var li in Type_Lists)
            {
                var type_copy = new List<string>(li);
                Randomize.FisherYatesShuffle(type_copy);
                j = 0;
                foreach (var ke in k.KeyTable.Where(x => li.Contains(x.ResRef)))
                {
                    LookupTable.Add(new Tuple<string, string>(ke.ResRef, type_copy[j]));
                    ke.ResRef = type_copy[j];
                    j++;
                }
            }

            k.WriteToFile(paths.chitin);
        }

        private static void AssignSettings(IRandomizeItems rando)
        {
            if (rando == null)
            {
                ItemCategories = Kotor1Randomizer.ConstructItemOptionsList().ToList();
                ItemCategories.First(irco => irco.Category == ItemRandoCategory.Armbands).Level = Properties.Settings.Default.RandomizeArmbands;
                ItemCategories.First(irco => irco.Category == ItemRandoCategory.Armor).Level = Properties.Settings.Default.RandomizeArmor;
                ItemCategories.First(irco => irco.Category == ItemRandoCategory.Belts).Level = Properties.Settings.Default.RandomizeBelts;
                ItemCategories.First(irco => irco.Category == ItemRandoCategory.Blasters).Level = Properties.Settings.Default.RandomizeBlasters;
                ItemCategories.First(irco => irco.Category == ItemRandoCategory.CreatureHides).Level = Properties.Settings.Default.RandomizeHides;
                ItemCategories.First(irco => irco.Category == ItemRandoCategory.CreatureWeapons).Level = Properties.Settings.Default.RandomizeCreature;
                ItemCategories.First(irco => irco.Category == ItemRandoCategory.DroidEquipment).Level = Properties.Settings.Default.RandomizeDroid;
                ItemCategories.First(irco => irco.Category == ItemRandoCategory.Gloves).Level = Properties.Settings.Default.RandomizeGloves;
                ItemCategories.First(irco => irco.Category == ItemRandoCategory.Grenades).Level = Properties.Settings.Default.RandomizeGrenades;
                ItemCategories.First(irco => irco.Category == ItemRandoCategory.Implants).Level = Properties.Settings.Default.RandomizeImplants;
                ItemCategories.First(irco => irco.Category == ItemRandoCategory.Lightsabers).Level = Properties.Settings.Default.RandomizeLightsabers;
                ItemCategories.First(irco => irco.Category == ItemRandoCategory.Masks).Level = Properties.Settings.Default.RandomizeMask;
                ItemCategories.First(irco => irco.Category == ItemRandoCategory.MeleeWeapons).Level = Properties.Settings.Default.RandomizeMelee;
                ItemCategories.First(irco => irco.Category == ItemRandoCategory.Mines).Level = Properties.Settings.Default.RandomizeMines;
                ItemCategories.First(irco => irco.Category == ItemRandoCategory.PazaakCards).Level = Properties.Settings.Default.RandomizePaz;
                ItemCategories.First(irco => irco.Category == ItemRandoCategory.Medical).Level = Properties.Settings.Default.RandomizeStims;
                ItemCategories.First(irco => irco.Category == ItemRandoCategory.Upgrades).Level = Properties.Settings.Default.RandomizeUpgrade;
                ItemCategories.First(irco => irco.Category == ItemRandoCategory.Various).Level = Properties.Settings.Default.RandomizeVarious;
                OmittedItems = Globals.OmitItems.ToList();
                OmitPreset = "";
            }
            else
            {
                ItemCategories = rando.ItemCategoryOptions.ToList();
                OmittedItems = rando.ItemOmittedList.Select(x => x.Code).ToList();
                OmitPreset = rando.ItemOmittedPreset;
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
                    for (var i = 1; i < r.Count; i++)
                    {
                        var temp = new List<string>(k.KeyTable.Where(x => r[i].IsMatch(x.ResRef) && !Is_Forbidden(x.ResRef) && x.ResourceType == (short)ResourceType.UTI).Select(x => x.ResRef));
                        Type_Lists.Add(temp);
                    }
                    break;
                case RandomizationLevel.Type:
                    var type = new List<string>(k.KeyTable.Where(x => r[0].IsMatch(x.ResRef) && !Is_Forbidden(x.ResRef) && x.ResourceType == (short)ResourceType.UTI).Select(x => x.ResRef));
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
            return !ItemCategories.Any(irco => irco.Category != ItemRandoCategory.Various && irco.Regex[0].IsMatch(s));
        }

        private static List<string> Max_Rando = new List<string>();

        private static List<List<string>> Type_Lists = new List<List<string>>();

        internal static void Reset()
        {
            // Prepare lists for new randomization.
            Max_Rando.Clear();
            Type_Lists.Clear();
            LookupTable.Clear();
            ItemCategories.Clear();
        }

        public static void CreateSpoilerLog(XLWorkbook workbook)
        {
            if (LookupTable.Count == 0) { return; }
            var ws = workbook.Worksheets.Add("Item");
            var i = 1;

            // Item Randomization Settings
            ws.Cell(i, 1).Value = "Item Type";
            ws.Cell(i, 2).Value = "Rando Level";
            ws.Cell(i, 1).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            ws.Cell(i, 2).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            ws.Cell(i, 1).Style.Font.Bold = true;
            ws.Cell(i, 2).Style.Font.Bold = true;
            i++;

            var presetName = OmitPreset;
            var isCustomPreset = false;
            if (string.IsNullOrWhiteSpace(OmitPreset))
            {
                presetName = "Custom";
                isCustomPreset = true;
            }

            var settings = new List<Tuple<string, string>>();
            foreach (var irco in ItemCategories)
            {
                settings.Add(new Tuple<string, string>(irco.Category.ToLabel(), irco.Level.ToString()));
            }
            settings.Add(new Tuple<string, string>("", ""));
            settings.Add(new Tuple<string, string>("Item Omit Preset", presetName));

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
                var iMax = i;
                i = 1;  // Restart at the top of the settings list.

                ws.Cell(i, 4).Value = "Omitted Items";
                ws.Cell(i, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Cell(i, 4).Style.Font.Bold = true;
                _ = ws.Range(i, 4, i, 5).Merge();
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
                    var origItemName = RandomizableItem.KOTOR1_ITEMS.FirstOrDefault(ri => ri.Code == item)?.Label ?? "";

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
            _ = ws.Range(i-1, 2, i-1, 3).Merge();
            _ = ws.Range(i-1, 4, i-1, 5).Merge();
            i++;

            var sortedLookup = LookupTable.OrderBy(tpl => tpl.Item1);
            foreach (var tpl in sortedLookup)
            {
                var omitted = OmittedItems.Any(x => x == tpl.Item1);
                var changed = tpl.Item1 != tpl.Item2;   // Has the shuffle changed this item?
                var origItemName = RandomizableItem.KOTOR1_ITEMS.FirstOrDefault(ri => ri.Code == tpl.Item1)?.Label ?? "";
                var randItemName = changed ? RandomizableItem.KOTOR1_ITEMS.FirstOrDefault(ri => ri.Code == tpl.Item2)?.Label ?? "" : origItemName;

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

                ws.Cell(i, 1).Value = omitted ? "OMITTED" : changed.ToString().ToUpper();
                ws.Cell(i, 1).DataType = XLDataType.Text;
                ws.Cell(i, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
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
            _ = ws.Column(1).AdjustToContents();
            _ = ws.Column(2).AdjustToContents();
            _ = ws.Column(3).AdjustToContents();
            _ = ws.Column(4).AdjustToContents();
            _ = ws.Column(5).AdjustToContents();
        }
    }
}
