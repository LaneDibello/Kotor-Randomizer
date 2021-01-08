using System.Linq;
using System.IO;
using KotOR_IO;
using System.Collections.Generic;
using System;

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
    }
}