using System.Linq;
using System.IO;
using KotOR_IO;
using System.Collections.Generic;
using System;
using ClosedXML.Excel;

namespace kotor_Randomizer_2
{
    public static class ModelRando
    {
        /// <summary>
        /// Lookup table for how models are randomized.
        /// Usage: LookupTable[MapName][ModelType][Label] = (OriginalID, RandomizedID);
        /// </summary>
        private static Dictionary<string, Dictionary<string, Dictionary<string, Tuple<int, int>>>> LookupTable { get; set; } = new Dictionary<string, Dictionary<string, Dictionary<string, Tuple<int, int>>>>();

        public static void model_rando(KPaths paths)
        {
            const string CHARACTER = "Character";
            const string DOOR = "Door";
            const string PLACEABLE = "Placeable";
            LookupTable.Clear();

            foreach (FileInfo fi in paths.FilesInModules)
            {
                RIM r = new RIM(fi.FullName);
                LookupTable.Add(fi.Name, new Dictionary<string, Dictionary<string, Tuple<int, int>>>());

                // Doors
                if ((Properties.Settings.Default.RandomizeDoorModels & 1) > 0)
                {
                    LookupTable[fi.Name].Add(DOOR, new Dictionary<string, Tuple<int, int>>());

                    foreach (RIM.rFile rf in r.File_Table.Where(x => x.TypeID == (int)ResourceType.UTD))
                    {
                        GFF_old g = new GFF_old(rf.File_Data);

                        int temp = 0;

                        if ((Properties.Settings.Default.RandomizeDoorModels & 4) > 0) // Broken Doors
                        {
                            temp = Randomize.Rng.Next(13, 64); // First 12 doors are open so this is easier
                        }
                        else
                        {
                            temp = Randomize.Rng.Next(0, 64);
                        }

                        // Airlock
                        if ((Properties.Settings.Default.RandomizeDoorModels & 2) > 0 &&
                            (g.Field_Array.Where(x => x.Label == "LocName").FirstOrDefault().Field_Data as GFF_old.CExoLocString).StringRef == 21080)
                        {
                            continue;
                        }

                        var id = BitConverter.ToInt32((byte[])g.Field_Array.Where(k => k.Label == "GenericType").FirstOrDefault().Field_Data, 0);
                        LookupTable[fi.Name][DOOR].Add(rf.Label, new Tuple<int, int>(id, temp));

                        g.Field_Array.Where(k => k.Label == "GenericType").FirstOrDefault().Field_Data = temp;
                        g.Field_Array.Where(k => k.Label == "GenericType").FirstOrDefault().DataOrDataOffset = temp;

                        rf.File_Data = g.ToRawData();
                    }
                }

                // Placeables
                if ((Properties.Settings.Default.RandomizePlaceModels & 1) > 0)
                {
                    LookupTable[fi.Name].Add(PLACEABLE, new Dictionary<string, Tuple<int, int>>());

                    foreach (RIM.rFile rf in r.File_Table.Where(k => k.TypeID == (int)ResourceType.UTP))
                    {
                        GFF_old g = new GFF_old(rf.File_Data);

                        if (Globals.BROKEN_PLACE.Contains(g.Field_Array.Where(k => k.Label == "Appearance").FirstOrDefault().DataOrDataOffset)) { continue; }

                        int temp = Randomize.Rng.Next(0, 231);

                        bool broken_satisfied = !((Properties.Settings.Default.RandomizePlaceModels & 4) > 0) || !Globals.BROKEN_PLACE.Contains(temp);//Always Satisfied if Broken omission disbaled
                        bool large_satisfied = !((Properties.Settings.Default.RandomizePlaceModels & 2) > 0) || !Globals.LARGE_PLACE.Contains(temp);//Always satisifed if Large omission disabled

                        while (!(broken_satisfied && large_satisfied))
                        {
                            temp = Randomize.Rng.Next(0, 231);
                            broken_satisfied = !((Properties.Settings.Default.RandomizePlaceModels & 4) > 0) || !Globals.BROKEN_PLACE.Contains(temp);//Always Satisfied if Broken omission disbaled
                            large_satisfied = !((Properties.Settings.Default.RandomizePlaceModels & 2) > 0) || !Globals.LARGE_PLACE.Contains(temp);//Always satisifed if Large omission disabled

                        }

                        var id = Convert.ToInt32(g.Field_Array.Where(k => k.Label == "Appearance").FirstOrDefault().Field_Data);
                        LookupTable[fi.Name][PLACEABLE].Add(rf.Label, new Tuple<int, int>(id, temp));

                        g.Field_Array.Where(k => k.Label == "Appearance").FirstOrDefault().Field_Data = temp;
                        g.Field_Array.Where(k => k.Label == "Appearance").FirstOrDefault().DataOrDataOffset = temp;

                        rf.File_Data = g.ToRawData();
                    }
                }

                // Characters
                if ((Properties.Settings.Default.RandomizeCharModels & 1) > 0)
                {
                    LookupTable[fi.Name].Add(CHARACTER, new Dictionary<string, Tuple<int, int>>());

                    foreach (RIM.rFile rf in r.File_Table.Where(k => k.TypeID == (int)ResourceType.UTC))
                    {
                        GFF_old g = new GFF_old(rf.File_Data);

                        int temp = Randomize.Rng.Next(0, 508);

                        bool broken_satisfied = !((Properties.Settings.Default.RandomizeCharModels & 4) > 0) || !Globals.BROKEN_CHARS.Contains(temp);//Always Satisfied if Broken omission disbaled
                        bool large_satisfied = !((Properties.Settings.Default.RandomizeCharModels & 2) > 0) || !Globals.LARGE_CHARS.Contains(temp);//Always satisifed if Large omission disabled

                        while(!(broken_satisfied && large_satisfied))
                        {
                            temp = Randomize.Rng.Next(0, 508);
                            broken_satisfied = !((Properties.Settings.Default.RandomizeCharModels & 4) > 0) || !Globals.BROKEN_CHARS.Contains(temp);//Always Satisfied if Broken omission disbaled
                            large_satisfied = !((Properties.Settings.Default.RandomizeCharModels & 2) > 0) || !Globals.LARGE_CHARS.Contains(temp);//Always satisifed if Large omission disabled
                        }

                        var id = Convert.ToInt32(g.Field_Array.Where(k => k.Label == "Appearance_Type").FirstOrDefault().Field_Data);
                        LookupTable[fi.Name][CHARACTER].Add(rf.Label, new Tuple<int, int>(id, temp));

                        g.Field_Array.Where(k => k.Label == "Appearance_Type").FirstOrDefault().Field_Data = temp;
                        g.Field_Array.Where(k => k.Label == "Appearance_Type").FirstOrDefault().DataOrDataOffset = temp;

                        rf.File_Data = g.ToRawData();
                    }
                }

                r.WriteToFile(fi.FullName);
            }
        }

        /// <summary>
        /// Creates a CSV file containing a list of the changes made during randomization.
        /// If the file already exists, this method will append the data.
        /// If no randomization has been performed, no file will be created.
        /// </summary>
        /// <param name="path">Path to desired output file.</param>
        public static void GenerateSpoilerLog(string path)
        {
            if (LookupTable.Count == 0)
            {
                return;
            }

            using (StreamWriter sw = new StreamWriter(path))
            {
                //sw.WriteLine("Model,");
                sw.WriteLine($"Seed,{Properties.Settings.Default.Seed}");
                sw.WriteLine();

                bool IsActive = (Properties.Settings.Default.RandomizeCharModels & 1) > 0;
                bool OmitFirst = (Properties.Settings.Default.RandomizeCharModels & 2) > 0;
                bool OmitSecond = (Properties.Settings.Default.RandomizeCharModels & 4) > 0;
                sw.WriteLine("Model Type,Is Active,Omit Large Models,Omit Broken Models");
                sw.WriteLine($"Character Models,{IsActive},{OmitFirst},{OmitSecond}");

                IsActive = (Properties.Settings.Default.RandomizePlaceModels & 1) > 0;
                OmitFirst = (Properties.Settings.Default.RandomizePlaceModels & 2) > 0;
                OmitSecond = (Properties.Settings.Default.RandomizePlaceModels & 4) > 0;
                sw.WriteLine("Model Type,Is Active,Omit Large Models,Omit Broken Models");
                sw.WriteLine($"Placeable Models,{IsActive},{OmitFirst},{OmitSecond}");

                IsActive = (Properties.Settings.Default.RandomizeDoorModels & 1) > 0;
                OmitFirst = (Properties.Settings.Default.RandomizeDoorModels & 2) > 0;
                OmitSecond = (Properties.Settings.Default.RandomizeDoorModels & 4) > 0;
                sw.WriteLine("Model Type,Is Active,Omit Airlocks,Omit Broken Models");
                sw.WriteLine($"Door Models,{IsActive},{OmitFirst},{OmitSecond}");
                sw.WriteLine();

                sw.WriteLine("Map Filename,Model Type,Model Label,Original ID,Randomized ID");
                foreach (var map in LookupTable)
                {
                    foreach (var type in map.Value)
                    {
                        foreach (var kvp in type.Value)
                        {
                            sw.WriteLine($"{map.Key},{type.Key},{kvp.Key},{kvp.Value.Item1},{kvp.Value.Item2}");
                        }
                    }
                }
                sw.WriteLine();
            }
        }

        internal static void Reset()
        {
            // Prepare lists for new randomization.
            LookupTable.Clear();
        }

        public static void GenerateSpoilerLog(XLWorkbook workbook)
        {
            if (LookupTable.Count == 0) { return; }
            var ws = workbook.Worksheets.Add("Models");

            int i = 1;
            ws.Cell(i, 1).Value = "Seed";
            ws.Cell(i, 2).Value = Properties.Settings.Default.Seed;
            ws.Cell(i, 1).Style.Font.Bold = true;
            i += 2;     // Skip a row.

            // Model Randomization Settings
            //   Settings A
            ws.Cell(i, 1).Value = "Model Type";
            ws.Cell(i, 2).Value = "Is Active";
            ws.Cell(i, 3).Value = "Omit Large";
            ws.Cell(i, 4).Value = "Omit Broken";
            ws.Cell(i, 1).Style.Font.Bold = true;
            ws.Cell(i, 2).Style.Font.Bold = true;
            ws.Cell(i, 3).Style.Font.Bold = true;
            ws.Cell(i, 4).Style.Font.Bold = true;
            i++;

            string charIsActive = ((Properties.Settings.Default.RandomizeCharModels & 1) > 0).ToString();
            string charOmitFirst = ((Properties.Settings.Default.RandomizeCharModels & 2) > 0).ToString();
            string charOmitSecond = ((Properties.Settings.Default.RandomizeCharModels & 4) > 0).ToString();

            string modelIsActive = ((Properties.Settings.Default.RandomizePlaceModels & 1) > 0).ToString();
            string modelOmitFirst = ((Properties.Settings.Default.RandomizePlaceModels & 2) > 0).ToString();
            string modelOmitSecond = ((Properties.Settings.Default.RandomizePlaceModels & 4) > 0).ToString();

            var settings = new List<Tuple<string, string, string, string>>()
            {
                new Tuple<string, string, string, string>("Character Models", charIsActive, charOmitFirst, charOmitSecond),
                new Tuple<string, string, string, string>("Placeable Models", modelIsActive, modelOmitFirst, modelOmitSecond),
            };

            foreach (var setting in settings)
            {
                ws.Cell(i, 1).Value = setting.Item1;
                ws.Cell(i, 2).Value = setting.Item2;
                ws.Cell(i, 3).Value = setting.Item3;
                ws.Cell(i, 4).Value = setting.Item4;
                ws.Cell(i, 1).Style.Font.Italic = true;
                i++;
            }

            i++;    // Skip a row.

            //   Settings B
            ws.Cell(i, 1).Value = "Model Type";
            ws.Cell(i, 2).Value = "Is Active";
            ws.Cell(i, 3).Value = "Omit Airlocks";
            ws.Cell(i, 4).Value = "Omit Broken";
            ws.Cell(i, 1).Style.Font.Bold = true;
            ws.Cell(i, 2).Style.Font.Bold = true;
            ws.Cell(i, 3).Style.Font.Bold = true;
            ws.Cell(i, 4).Style.Font.Bold = true;
            i++;

            string doorIsActive = ((Properties.Settings.Default.RandomizeDoorModels & 1) > 0).ToString();
            string doorOmitFirst = ((Properties.Settings.Default.RandomizeDoorModels & 2) > 0).ToString();
            string doorOmitSecond = ((Properties.Settings.Default.RandomizeDoorModels & 4) > 0).ToString();

            settings = new List<Tuple<string, string, string, string>>()
            {
                new Tuple<string, string, string, string>("Door Models", doorIsActive, doorOmitFirst, doorOmitSecond),
            };

            foreach (var setting in settings)
            {
                ws.Cell(i, 1).Value = setting.Item1;
                ws.Cell(i, 2).Value = setting.Item2;
                ws.Cell(i, 3).Value = setting.Item3;
                ws.Cell(i, 4).Value = setting.Item4;
                ws.Cell(i, 1).Style.Font.Italic = true;
                i++;
            }

            i++;    // Skip a row.

            // Model Shuffle
            ws.Cell(i, 1).Value = "Map Filename";
            ws.Cell(i, 2).Value = "Model Type";
            ws.Cell(i, 3).Value = "Model Label";
            ws.Cell(i, 4).Value = "Has Changed";
            ws.Cell(i, 5).Value = "Original ID";
            ws.Cell(i, 6).Value = "Randomized ID";
            ws.Cell(i, 1).Style.Font.Bold = true;
            ws.Cell(i, 2).Style.Font.Bold = true;
            ws.Cell(i, 3).Style.Font.Bold = true;
            ws.Cell(i, 4).Style.Font.Bold = true;
            ws.Cell(i, 5).Style.Font.Bold = true;
            ws.Cell(i, 6).Style.Font.Bold = true;
            i++;

            string prevMap = string.Empty;
            int j = 0;
            List<XLColor> colors = new List<XLColor>()
            {
                XLColor.Red,
                XLColor.OrangePeel,
                XLColor.Green,
                XLColor.DeepSkyBlue,
                XLColor.Blue,
                XLColor.Purple,
            };

            foreach (var map in LookupTable)
            {
                foreach (var type in map.Value)
                {
                    foreach (var kvp in type.Value)
                    {
                        var hasChanged = kvp.Value.Item1 != kvp.Value.Item2;
                        ws.Cell(i, 1).Value = map.Key;
                        ws.Cell(i, 2).Value = type.Key;
                        ws.Cell(i, 3).Value = kvp.Key;
                        ws.Cell(i, 4).Value = hasChanged;
                        ws.Cell(i, 5).Value = kvp.Value.Item1;
                        ws.Cell(i, 6).Value = kvp.Value.Item2;

                        if (prevMap != map.Key)
                        {
                            prevMap = map.Key;
                            j = (j + 1) % colors.Count;
                        }
                        ws.Cell(i, 1).Style.Font.FontColor = colors[j];

                        if (type.Key == "Door")      ws.Cell(i, 2).Style.Font.FontColor = XLColor.Blue;
                        if (type.Key == "Character") ws.Cell(i, 2).Style.Font.FontColor = XLColor.Green;
                        if (type.Key == "Placeable") ws.Cell(i, 2).Style.Font.FontColor = XLColor.Red;

                        if (hasChanged) ws.Cell(i, 4).Style.Font.FontColor = XLColor.Green;
                        else            ws.Cell(i, 4).Style.Font.FontColor = XLColor.Red;

                        i++;
                    }
                }
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