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
        private static Dictionary<string, Dictionary<string, Dictionary<string, Tuple<int, string, int, string>>>> LookupTable { get; set; } = new Dictionary<string, Dictionary<string, Dictionary<string, Tuple<int, string, int, string>>>>();

        private const string CHARACTER = "Character";
        private const string DOOR = "Door";
        private const string PLACEABLE = "Placeable";

        private const string AREA_UNK_CATACOMBS = "unk_m44ab";
        private const string LABEL_UNK_FLPNL = "flpnl";
        private const string LABEL_UNK_RESETPANEL = "panelreset";

        public const string LBL_LOC_NAME = "LocName";
        public const string LBL_GENERIC_TYPE = "GenericType";
        public const string LBL_APPEARANCE = "Appearance";
        public const string LBL_APPEARANCE_TYPE = "Appearance_Type";

        public static void model_rando(KPaths paths)
        {
            const int MAX_CHAR_INDEX = 509;
            const int MIN_DOOR_INDEX_BROKEN = 13;
            const int MAX_DOOR_INDEX = 65;
            const int MAX_PLAC_INDEX = 232;

            const string CHAR_2DA = "appearance";
            const string DOOR_2DA = "genericdoors";
            const string PLAC_2DA = "placeables";

            const string COL_LABEL = "label";

            LookupTable.Clear();

            BIF bif = new BIF(Path.Combine(paths.data, "2da.bif"));
            KEY key = new KEY(paths.chitin);
            bif.AttachKey(key, "data\\2da.bif");

            var charVRE = bif.VariableResourceTable.Where(x => x.ResRef == CHAR_2DA).FirstOrDefault();
            var doorVRE = bif.VariableResourceTable.Where(x => x.ResRef == DOOR_2DA).FirstOrDefault();
            var placVRE = bif.VariableResourceTable.Where(x => x.ResRef == PLAC_2DA).FirstOrDefault();

            TwoDA char2DA = new TwoDA(charVRE.EntryData, charVRE.ResRef);
            TwoDA door2DA = new TwoDA(doorVRE.EntryData, doorVRE.ResRef);
            TwoDA plac2DA = new TwoDA(placVRE.EntryData, placVRE.ResRef);

            // Check if the floor panel fix is enabled.
            bool isFloorPanelActive = (Properties.Settings.Default.RandomizePlaceModels & 8) > 0;
            var catacombsFile = AREA_UNK_CATACOMBS;

            // Check if modules have been randomized.
            var moduleFiles = paths.FilesInModules.ToList();
          
            if (ModuleRando.LookupTable.Any())
            {
                // If randomized, ensure module files are processed in the same order every time.
                var sortedLookup = ModuleRando.LookupTable.OrderBy(kvp => kvp.Key);
                var newList = new List<FileInfo>();

                foreach (var kvp in sortedLookup)
                {
                    // Find the file that has replaced the catacombs for the floor panel fix.
                    if (kvp.Key == AREA_UNK_CATACOMBS) catacombsFile = kvp.Value;

                    // Add the files to modify to the new list in proper order.
                    var filesToAdd = moduleFiles.Where(fi => fi.Name.Contains(kvp.Value));
                    newList.AddRange(filesToAdd);
                }

                moduleFiles = newList;
            }

            // Loop through each file and randomize the requested model types.
            foreach (FileInfo fi in moduleFiles)
            {
                RIM r = new RIM(fi.FullName);
                LookupTable.Add(fi.Name, new Dictionary<string, Dictionary<string, Tuple<int, string, int, string>>>());

                // Doors
                if ((Properties.Settings.Default.RandomizeDoorModels & 1) > 0)
                {
                    LookupTable[fi.Name].Add(DOOR, new Dictionary<string, Tuple<int, string, int, string>>());

                    foreach (RIM.rFile rf in r.File_Table.Where(x => x.TypeID == (int)ResourceType.UTD))
                    {

                        GFF g = new GFF(rf.File_Data);

                        int randAppear = 0; //The randomly generated Appearance ID

                        //Generate the random appearacne values before ommitting airlock, to create more seed consistancy
                        if ((Properties.Settings.Default.RandomizeDoorModels & 4) > 0) // Broken Doors
                        {
                            randAppear = Randomize.Rng.Next(MIN_DOOR_INDEX_BROKEN, MAX_DOOR_INDEX); // First 12 doors are open so this is easier
                        }
                        else
                        {
                            randAppear = Randomize.Rng.Next(0, MAX_DOOR_INDEX);
                        }

                        // Airlock
                        if ((Properties.Settings.Default.RandomizeDoorModels & 2) > 0 &&
                            (g.Top_Level.Fields.Where(f => f.Label == LBL_LOC_NAME).FirstOrDefault() as GFF.CExoLocString).StringRef == 21080)
                        {
                            continue;
                        }

                        //Get Info from Door2DA for the Spoiler Log
                        var field = g.Top_Level.Fields.Where(f => f.Label == LBL_GENERIC_TYPE).FirstOrDefault() as GFF.BYTE;
                        int id = (int)field.Value;

                        var label_old = door2DA.Data[COL_LABEL][id];
                        var label_new = door2DA.Data[COL_LABEL][randAppear];

                        LookupTable[fi.Name][DOOR].Add(rf.Label, new Tuple<int, string, int, string>(id, label_old, randAppear, label_new));

                        //Change the appearance value
                        (g.Top_Level.Fields.Where(f => f.Label == LBL_GENERIC_TYPE).FirstOrDefault() as GFF.BYTE).Value = (byte)randAppear;

                        rf.File_Data = g.ToRawData();
                    }
                }

                // Placeables
                if ((Properties.Settings.Default.RandomizePlaceModels & 1) > 0)
                {
                    LookupTable[fi.Name].Add(PLACEABLE, new Dictionary<string, Tuple<int, string, int, string>>());

                    // Check if floor panels should be replaced with valid placeables.
                    bool useValidFloorPanels = isFloorPanelActive && fi.Name.Contains(catacombsFile);

                    foreach (RIM.rFile rf in r.File_Table.Where(k => k.TypeID == (int)ResourceType.UTP))
                    {
                        GFF g = new GFF(rf.File_Data);

                        // If this is a broken placeable, skip it.
                        if (Globals.BROKEN_PLACE.Contains((int)(g.Top_Level.Fields.Where(f => f.Label == LBL_APPEARANCE).FirstOrDefault() as GFF.DWORD).Value)) { continue; }

                        int randAppear = 0;

                        // Randomly generate a valid replacement for the "Lights Out" panels.
                        if (useValidFloorPanels && (rf.Label.StartsWith(LABEL_UNK_FLPNL) || rf.Label == LABEL_UNK_RESETPANEL))
                        {
                            randAppear = Globals.PANEL_PLACE[Randomize.Rng.Next(0, Globals.PANEL_PLACE.Count)];
                        }
                        else
                        {
                            // Generate a random appearance for this placeable.
                            bool isBroken = false;
                            bool isLarge = false;

                            do
                            {
                                randAppear = Randomize.Rng.Next(0, MAX_PLAC_INDEX);
                                isBroken = ((Properties.Settings.Default.RandomizePlaceModels & 4) > 0) && Globals.BROKEN_PLACE.Contains(randAppear); // Always Satisfied if Broken omission disbaled
                                isLarge  = ((Properties.Settings.Default.RandomizePlaceModels & 2) > 0) && Globals.LARGE_PLACE.Contains(randAppear);  // Always satisifed if Large omission disabled
                            }
                            while (isBroken || isLarge);
                        }

                        var field = g.Top_Level.Fields.Where(f => f.Label == LBL_APPEARANCE).FirstOrDefault() as GFF.DWORD;
                        int id = (int)field.Value;

                        var label_old = plac2DA.Data[COL_LABEL][id];
                        var label_new = plac2DA.Data[COL_LABEL][randAppear];

                        LookupTable[fi.Name][PLACEABLE].Add(rf.Label, new Tuple<int, string, int, string>(id, label_old, randAppear, label_new));

                        // Change the appearance value.
                        (g.Top_Level.Fields.Where(f => f.Label == LBL_APPEARANCE).FirstOrDefault() as GFF.DWORD).Value = (uint)randAppear;

                        rf.File_Data = g.ToRawData();
                    }
                }

                // Characters
                if ((Properties.Settings.Default.RandomizeCharModels & 1) > 0)
                {
                    LookupTable[fi.Name].Add(CHARACTER, new Dictionary<string, Tuple<int, string, int, string>>());

                    foreach (RIM.rFile rf in r.File_Table.Where(k => k.TypeID == (int)ResourceType.UTC))
                    {
                        GFF g = new GFF(rf.File_Data);

                        int randAppear = 0;
                        bool isBroken = false;
                        bool isLarge = false;

                        do
                        {
                            randAppear = Randomize.Rng.Next(0, MAX_CHAR_INDEX);
                            isBroken = ((Properties.Settings.Default.RandomizeCharModels & 4) > 0) && Globals.BROKEN_CHARS.Contains(randAppear);  // Always Satisfied if Broken omission disabled
                            isLarge  = ((Properties.Settings.Default.RandomizeCharModels & 2) > 0) && Globals.LARGE_CHARS.Contains(randAppear);   // Always satisifed if Large omission disabled
                        }
                        while (isBroken || isLarge);

                        var field = g.Top_Level.Fields.Where(f => f.Label == LBL_APPEARANCE_TYPE).FirstOrDefault() as GFF.WORD;
                        int id = (int)field.Value;

                        var label_old = char2DA.Data[COL_LABEL][id];
                        var label_new = char2DA.Data[COL_LABEL][randAppear];

                        LookupTable[fi.Name][CHARACTER].Add(rf.Label, new Tuple<int, string, int, string>(id, label_old, randAppear, label_new));

                        (g.Top_Level.Fields.Where(f => f.Label == LBL_APPEARANCE_TYPE).FirstOrDefault() as GFF.WORD).Value = (ushort)randAppear;

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

        public static void CreateSpoilerLog(XLWorkbook workbook)
        {
            if (LookupTable.Count == 0) { return; }
            var ws = workbook.Worksheets.Add("Model");

            int i = 1;
            ws.Cell(i, 1).Value = "Seed";
            ws.Cell(i, 2).Value = Properties.Settings.Default.Seed;
            ws.Cell(i, 1).Style.Font.Bold = true;
            i++;

            Version version = typeof(StartForm).Assembly.GetName().Version;
            ws.Cell(i, 1).Value = "Version";
            ws.Cell(i, 1).Style.Font.Bold = true;
            ws.Cell(i, 2).Value = $"v{version.Major}.{version.Minor}.{version.Build}";
            ws.Cell(i, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            i += 2;     // Skip a row.

            // Character Randomization Settings
            ws.Cell(i, 1).Value = "Character Models";
            ws.Cell(i, 1).Style.Font.Bold = true;
            ws.Cell(i, 2).Value = ((Properties.Settings.Default.RandomizeCharModels & 1) > 0).ToString();
            i++;

            ws.Cell(i, 1).Value = "Omit Large";
            ws.Cell(i, 1).Style.Font.Italic = true;
            ws.Cell(i, 2).Value = ((Properties.Settings.Default.RandomizeCharModels & 2) > 0).ToString();
            i++;

            ws.Cell(i, 1).Value = "Omit Broken";
            ws.Cell(i, 1).Style.Font.Italic = true;
            ws.Cell(i, 2).Value = ((Properties.Settings.Default.RandomizeCharModels & 4) > 0).ToString();
            i += 2;     // Skip a row.

            // Placeable Randomization Settings
            ws.Cell(i, 1).Value = "Placeable Models";
            ws.Cell(i, 1).Style.Font.Bold = true;
            ws.Cell(i, 2).Value = ((Properties.Settings.Default.RandomizePlaceModels & 1) > 0).ToString();
            i++;

            ws.Cell(i, 1).Value = "Omit Large";
            ws.Cell(i, 1).Style.Font.Italic = true;
            ws.Cell(i, 2).Value = ((Properties.Settings.Default.RandomizePlaceModels & 2) > 0).ToString();
            i++;

            ws.Cell(i, 1).Value = "Omit Broken";
            ws.Cell(i, 1).Style.Font.Italic = true;
            ws.Cell(i, 2).Value = ((Properties.Settings.Default.RandomizePlaceModels & 4) > 0).ToString();
            i++;

            ws.Cell(i, 1).Value = "Easy Panels";
            ws.Cell(i, 1).Style.Font.Italic = true;
            ws.Cell(i, 2).Value = ((Properties.Settings.Default.RandomizePlaceModels & 8) > 0).ToString();
            i += 2;     // Skip a row.

            // Door Randomization Settings
            ws.Cell(i, 1).Value = "Door Models";
            ws.Cell(i, 1).Style.Font.Bold = true;
            ws.Cell(i, 2).Value = ((Properties.Settings.Default.RandomizeDoorModels & 1) > 0).ToString();
            i++;

            ws.Cell(i, 1).Value = "Omit Airlocks";
            ws.Cell(i, 1).Style.Font.Italic = true;
            ws.Cell(i, 2).Value = ((Properties.Settings.Default.RandomizeDoorModels & 2) > 0).ToString();
            i++;

            ws.Cell(i, 1).Value = "Omit Broken";
            ws.Cell(i, 1).Style.Font.Italic = true;
            ws.Cell(i, 2).Value = ((Properties.Settings.Default.RandomizeDoorModels & 4) > 0).ToString();
            i += 3;     // Skip two rows.

            int j = 1;  // Start at column A.
            var jMax = 1;   // Remember max table width.
            var areModulesRandomized = ModuleRando.LookupTable.Any();

            // Model Shuffle Headings
            ws.Cell(i, j).Value = "Map Filename";
            ws.Cell(i, j).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            ws.Cell(i, j).Style.Font.Bold = true;
            j++;

            if (areModulesRandomized)
            {
                ws.Cell(i, j).Value = "Randomized Map";
                ws.Cell(i, j).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                ws.Cell(i, j).Style.Font.Bold = true;
                j++;
            }

            ws.Cell(i, j).Value = "Model Type";
            ws.Cell(i, j).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            ws.Cell(i, j).Style.Font.Bold = true;
            j++;

            ws.Cell(i, j).Value = "Model Label";
            ws.Cell(i, j).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            ws.Cell(i, j).Style.Font.Bold = true;
            j++;

            ws.Cell(i-1, j).Value = "Original";
            ws.Cell(i-1, j).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Cell(i-1, j).Style.Font.Bold = true;
            ws.Range(i-1, j, i-1, j+1).Merge().Style.Border.LeftBorder = XLBorderStyleValues.Thin;

            ws.Cell(i, j).Value = "ID";
            ws.Cell(i, j).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            ws.Cell(i, j).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
            ws.Cell(i, j).Style.Font.Italic = true;
            j++;

            ws.Cell(i, j).Value = "Label";
            ws.Cell(i, j).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            ws.Cell(i, j).Style.Border.RightBorder = XLBorderStyleValues.Thin;
            ws.Cell(i, j).Style.Font.Italic = true;
            j++;

            ws.Cell(i, j).Value = "Changed";
            ws.Cell(i, j).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            ws.Cell(i, j).Style.Border.RightBorder = XLBorderStyleValues.Thin;
            ws.Cell(i, j).Style.Font.Bold = true;
            j++;

            ws.Cell(i-1, j).Value = "Randomized";
            ws.Cell(i-1, j).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Cell(i-1, j).Style.Font.Bold = true;
            ws.Range(i-1, j, i-1, j+1).Merge().Style.Border.RightBorder = XLBorderStyleValues.Thin;

            ws.Cell(i, j).Value = "ID";
            ws.Cell(i, j).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            ws.Cell(i, j).Style.Font.Italic = true;
            j++;

            ws.Cell(i, j).Value = "Label";
            ws.Cell(i, j).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            ws.Cell(i, j).Style.Border.RightBorder = XLBorderStyleValues.Thin;
            ws.Cell(i, j).Style.Font.Italic = true;

            if (jMax < j) jMax = j;
            i++;

            // Set up color rotation.
            List<XLColor> colors = new List<XLColor>()
            {
                XLColor.Red,
                XLColor.OrangePeel,
                XLColor.Green,
                XLColor.DeepSkyBlue,
                XLColor.Blue,
                XLColor.Purple,
            };
            string prevMap = string.Empty;
            int c = colors.Count - 1;

            // Model Shuffle Contents
            foreach (var map in LookupTable)
            {
                foreach (var type in map.Value)
                {
                    foreach (var kvp in type.Value)
                    {
                        j = 1;  // Reset column counter.
                        var hasChanged = kvp.Value.Item1 != kvp.Value.Item3;

                        ws.Cell(i, j).Value = map.Key;
                        if (prevMap != map.Key) { prevMap = map.Key; c = (c + 1) % colors.Count; }
                        ws.Cell(i, j++).Style.Font.FontColor = colors[c];

                        // If modules are randomized, look up the shuffle.
                        if (areModulesRandomized)
                        {
                            ws.Cell(i, j++).Value = ModuleRando.LookupTable.FirstOrDefault(x => x.Value == map.Key.Remove(map.Key.LastIndexOf('_'))).Key;
                        }

                        ws.Cell(i, j).Value = type.Key;
                        if (type.Key == DOOR)      ws.Cell(i, j++).Style.Font.FontColor = XLColor.Blue;
                        if (type.Key == CHARACTER) ws.Cell(i, j++).Style.Font.FontColor = XLColor.Green;
                        if (type.Key == PLACEABLE) ws.Cell(i, j++).Style.Font.FontColor = XLColor.Red;

                        ws.Cell(i, j++).Value = kvp.Key;

                        ws.Cell(i, j).Value = kvp.Value.Item1;
                        ws.Cell(i, j++).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        ws.Cell(i, j++).Value = kvp.Value.Item2;

                        ws.Cell(i, j).Value = hasChanged;
                        ws.Cell(i, j).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        ws.Cell(i, j).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        if (hasChanged) ws.Cell(i, j++).Style.Font.FontColor = XLColor.Green;
                        else            ws.Cell(i, j++).Style.Font.FontColor = XLColor.Red;

                        ws.Cell(i, j++).Value = kvp.Value.Item3;
                        ws.Cell(i, j).Value = kvp.Value.Item4;
                        ws.Cell(i, j++).Style.Border.RightBorder = XLBorderStyleValues.Thin;

                        i++;
                    }
                }
            }

            // Resize columns.
            for (int col = 1; col <= jMax; col++)
            {
                ws.Column(col).AdjustToContents();
            }
        }
    }
}