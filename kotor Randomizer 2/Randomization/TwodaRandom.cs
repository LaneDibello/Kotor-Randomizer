using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using KotOR_IO;
using ClosedXML.Excel;
using kotor_Randomizer_2.Models;

namespace kotor_Randomizer_2
{
    public static class TwodaRandom
    {
        /// <summary>
        /// Lookup table for how 2DAs are randomized.
        /// Usage: LookupTable[2DA][col_name] = (OriginalData, RandomizedData);
        /// </summary>
        private static Dictionary<string, Dictionary<string, List<Tuple<string, string>>>> LookupTable { get; set; } = new Dictionary<string, Dictionary<string, List<Tuple<string, string>>>>();

        public static Dictionary<string, List<string>> Selected2DAs { get; set; }

        /// <summary>
        /// Creates backups for files modified during this randomization.
        /// </summary>
        /// <param name="paths"></param>
        internal static void CreateTwoDABackups(KPaths paths)
        {
            paths.BackUpChitinFile();
            paths.BackUpOverrideDirectory();
        }

        public static void Twoda_rando(KPaths paths, Kotor1Randomizer k1rando = null)
        {
            AssignSettings(k1rando);

            BIF b = new BIF(Path.Combine(paths.data, "2da.bif"));
            KEY k = new KEY(paths.chitin_backup);
            b.AttachKey(k, "data\\2da.bif");

            var filesInOverride = paths.FilesInOverride.ToList();

            foreach (BIF.VariableResourceEntry VRE in b.VariableResourceTable.Where(x => Selected2DAs.Keys.Contains(x.ResRef)))
            {
                // Check to see if this table is already in the override directory.
                TwoDA t;
                if (filesInOverride.Any(fi => fi.Name == $"{VRE.ResRef}.2da"))
                {
                    // Modify the existing table.
                    t = new TwoDA(File.ReadAllBytes(filesInOverride.First(fi => fi.Name == $"{VRE.ResRef}.2da").FullName), VRE.ResRef);
                }
                else
                {
                    // Fetch the table from the 2DA BIF file.
                    t = new TwoDA(VRE.EntryData, VRE.ResRef);
                }

                if (!LookupTable.ContainsKey(VRE.ResRef))
                {
                    // Add 2DA to the table.
                    LookupTable.Add(VRE.ResRef, new Dictionary<string, List<Tuple<string, string>>>());
                }

                foreach (string col in Selected2DAs[VRE.ResRef])
                {
                    if (!LookupTable[VRE.ResRef].ContainsKey(col))
                    {
                        // Add column to the table.
                        LookupTable[VRE.ResRef].Add(col, new List<Tuple<string, string>>());
                    }

                    var old = t.Data[col].ToList();             // Save list of old data.
                    Randomize.FisherYatesShuffle(t.Data[col]);  // Randomize 2DA column data.

                    for (int i = 0; i < old.Count; i++)
                    {
                        // Add old and new data to the table.
                        LookupTable[VRE.ResRef][col].Add(new Tuple<string, string>(old[i], t.Data[col][i]));
                    }
                }

                t.WriteToDirectory(paths.Override); // Write new 2DA data to file.
            }
        }

        private static void AssignSettings(Kotor1Randomizer k1rando)
        {
            if (k1rando == null)
            {
                Selected2DAs = Globals.Selected2DAs;
            }
            else
            {
                Selected2DAs = new Dictionary<string, List<string>>();
                foreach (var table in k1rando.Table2DAs.Where(rt => rt.IsRandomized))
                {
                    Selected2DAs.Add(table.Name, table.Randomized.ToList());
                }
            }
        }

        internal static void Reset()
        {
            // Prepare lists for new randomization.
            LookupTable.Clear();
            Selected2DAs = null;
        }

        internal static void CreateSpoilerLog(XLWorkbook workbook)
        {
            if (LookupTable.Count == 0) { return; }
            var ws = workbook.Worksheets.Add("TwoDA");
            int i = 1;

            // TwoDA Randomization
            const string ORIGINAL = "Orig";
            const string RANDOM   = "Rand";

            int iDone = i;
            int j = 1;
            int jMax = 1;

            foreach (var twoDA in LookupTable)
            {
                // TwoDA Table Header
                ws.Cell(i, 1).Value = twoDA.Key;
                ws.Cell(i, 1).Style.Font.Bold = true;
                ws.Cell(i, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Range(i, 1, i, 2).Merge();
                i++;

                var iStart = i;

                foreach (var col in twoDA.Value)
                {
                    if (jMax < j) jMax = j + 1;     // Remember the width of the widest table.

                    // Column Headers
                    i = iStart;
                    ws.Cell(i, j).Value = $"{col.Key} {ORIGINAL}";
                    ws.Cell(i, j).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    ws.Cell(i, j).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    ws.Cell(i, j).Style.Font.Italic = true;
                    ws.Cell(i, j+1).Value = $"{col.Key} {RANDOM}";
                    ws.Cell(i, j+1).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    ws.Cell(i, j+1).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    ws.Cell(i, j+1).Style.Font.Italic = true;
                    i++;

                    foreach (var row in col.Value)
                    {
                        // Row Data
                        ws.Cell(i, j).Value = row.Item1;
                        ws.Cell(i, j).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        ws.Cell(i, j+1).Value = row.Item2;
                        ws.Cell(i, j+1).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        i++;
                    }

                    j += 2;     // Move to the next pair of columns.
                    if (iDone < i) iDone = i;   // Remember the length of this table.
                }

                i = iDone + 1;  // Skip a row.
                j = 1;          // Reset to column A.
            }

            // Adjust columns.
            for (int c = 1; c <= jMax; c++)
            {
                ws.Column(c).AdjustToContents();
            }
        }
    }
}
