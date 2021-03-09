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
    public static class TextRando
    {
        private const int TLK_STRING_COUNT = 49264;

        /// <summary>
        /// Lookup table for randomized dialogue entries.
        /// Usage: EntriesLookupTable[Module.Name][RFile.Label] = List(Struct[text].StringRef, rand, Struct[VO_ResRef].Reference, rand, Struct[Sound].Reference, rand);
        /// </summary>
        private static Dictionary<string, Dictionary<string, List<Tuple<int, int, string, string, string, string>>>> EntriesLookupTable { get; set; } = new Dictionary<string, Dictionary<string, List<Tuple<int, int, string, string, string, string>>>>();
        /// <summary>
        /// Lookup table for randomized dialogue replies.
        /// Usage: RepliesLookupTable[Module.Name][RFile.Label] = List(Struct[text].StringRef, rand);
        /// </summary>
        private static Dictionary<string, Dictionary<string, List<Tuple<int, int>>>> RepliesLookupTable { get; set; } = new Dictionary<string, Dictionary<string, List<Tuple<int, int>>>>();
        /// <summary>
        /// Lookup table for the randomized TLK table.
        /// Usage: TlkLookupTable[Index] = (OldValue, RandomValue);
        /// </summary>
        private static Dictionary<int, Tuple<string, string>> TlkLookupTable { get; set; } = new Dictionary<int, Tuple<string, string>>();

        public static void text_rando(KPaths paths)
        {
            var settings = Properties.Settings.Default;
            if (settings.TextSettingsValue.HasFlag(TextSettings.RandoFullTLK)) //Shuffle TLK first, so sound matching still works
            {
                shuffle_TLK(paths, settings.TextSettingsValue.HasFlag(TextSettings.MatchSimLengthStrings));
            }
            if (settings.TextSettingsValue.HasFlag(TextSettings.RandoDialogEntries) || settings.TextSettingsValue.HasFlag(TextSettings.RandoDialogReplies))
            {
                shuffle_dialogue(paths, settings.TextSettingsValue.HasFlag(TextSettings.RandoDialogEntries), settings.TextSettingsValue.HasFlag(TextSettings.RandoDialogReplies), settings.TextSettingsValue.HasFlag(TextSettings.MatchEntrySoundsWText));
            }
        }

        //Randomize Dialogue
        static void shuffle_dialogue(KPaths paths, bool Entries, bool Replies, bool SoundMatching)
        {
            TLK t = new TLK(paths.dialog);

            foreach (FileInfo fi in paths.FilesInModules)
            {
                if (fi.Name[fi.Name.Length - 5] != 's') { continue; }

                RIM r = new RIM(fi.FullName);

                foreach (RIM.rFile RF in r.File_Table.Where(x => x.TypeID == (int)ResourceType.DLG))
                {
                    GFF g = new GFF(RF.File_Data);

                    //Entries
                    if (Entries)
                    {
                        foreach (GFF.STRUCT S in (g.Top_Level.Fields.Where(x => x.Label == "EntryList").FirstOrDefault() as GFF.LIST).Structs)
                        {
                            if ((S.Fields.Where(x => x.Label == "Text").FirstOrDefault() as GFF.CExoLocString).StringRef != -1) // Avoid overwriting dialogue end indicators, and animation nodes
                            {
                                int str_ref = 0; // Find valid string
                                while (t.String_Data_Table[str_ref].SoundResRef == "" || t.String_Data_Table[str_ref].SoundResRef[0] == '_' || t.String_Data_Table[str_ref].SoundResRef.ToLower().Contains("comp")) //Ensure the string we have has a sound to go with it, starting with undescord means it's player dialogue, which doesn't have audio in this game
                                {
                                    str_ref = Randomize.Rng.Next(TLK_STRING_COUNT);
                                }

                                if (!EntriesLookupTable.ContainsKey(fi.Name))
                                    EntriesLookupTable.Add(fi.Name, new Dictionary<string, List<Tuple<int, int, string, string, string, string>>>());
                                if (!EntriesLookupTable[fi.Name].ContainsKey(RF.Label))
                                    EntriesLookupTable[fi.Name].Add(RF.Label, new List<Tuple<int, int, string, string, string, string>>());

                                // Sound and Text Matching
                                if (SoundMatching)
                                {
                                    var text = S.Fields.Where(x => x.Label == "Text").FirstOrDefault() as GFF.CExoLocString;
                                    int textOrig = text.StringRef;
                                    int textRand = str_ref;
                                    text.StringRef = str_ref;

                                    string VORefOrig = "";
                                    string VORefRand = "";
                                    string SoundOrig = "";
                                    string SoundRand = "";

                                    try
                                    {
                                        var voResRef = S.Fields.Where(x => x.Label == "VO_ResRef").FirstOrDefault() as GFF.ResRef;
                                        VORefOrig = voResRef.Reference;
                                        VORefRand = t.String_Data_Table[str_ref].SoundResRef;
                                        voResRef.Reference = VORefRand;
                                    }
                                    catch
                                    {
                                        VORefOrig = "";
                                        VORefRand = "";
                                    }
                                    try
                                    {
                                        var sound = S.Fields.Where(x => x.Label == "Sound").FirstOrDefault() as GFF.ResRef;
                                        SoundOrig = sound.Reference;
                                        SoundRand = t.String_Data_Table[str_ref].SoundResRef;
                                        sound.Reference = SoundRand;
                                    }
                                    catch
                                    {
                                        SoundOrig = "";
                                        SoundRand = "";
                                    } // If both VO_ResRef and Sound Fail we ignore the entry

                                    EntriesLookupTable[fi.Name][RF.Label].Add(new Tuple<int, int, string, string, string, string>(textOrig, textRand, VORefOrig, VORefRand, SoundOrig, SoundRand));
                                }
                                else
                                {
                                    var text = S.Fields.Where(x => x.Label == "Text").FirstOrDefault() as GFF.CExoLocString;
                                    EntriesLookupTable[fi.Name][RF.Label].Add(new Tuple<int, int, string, string, string, string>(text.StringRef, str_ref, "", "", "", ""));
                                    text.StringRef = str_ref;
                                }
                            }
                        }
                    }

                    //Replies
                    if (Replies)
                    {
                        foreach (GFF.STRUCT S in (g.Top_Level.Fields.Where(x => x.Label == "ReplyList").FirstOrDefault() as GFF.LIST).Structs)
                        {
                            if ((S.Fields.Where(x => x.Label == "Text").FirstOrDefault() as GFF.CExoLocString).StringRef != -1) //Avoid overwriting dialogue end indicators, and animation nodes
                            {
                                if (!RepliesLookupTable.ContainsKey(fi.Name))
                                    RepliesLookupTable.Add(fi.Name, new Dictionary<string, List<Tuple<int, int>>>());
                                if (!RepliesLookupTable[fi.Name].ContainsKey(RF.Label))
                                    RepliesLookupTable[fi.Name].Add(RF.Label, new List<Tuple<int, int>>());

                                int str_ref = Randomize.Rng.Next(TLK_STRING_COUNT);
                                while (t.String_Data_Table[str_ref].StringText == "")
                                {
                                    str_ref = Randomize.Rng.Next(TLK_STRING_COUNT);
                                }
                                var text = S.Fields.Where(x => x.Label == "Text").FirstOrDefault() as GFF.CExoLocString;
                                RepliesLookupTable[fi.Name][RF.Label].Add(new Tuple<int, int>(text.StringRef, str_ref));
                                text.StringRef = str_ref;
                            }
                        }
                    }

                    Array.Clear(RF.File_Data, 0, RF.File_Data.Length);
                    RF.File_Data = g.ToRawData();
                }

                r.WriteToFile(fi.FullName);
            }
        }

        //Randomize TLK
        static void shuffle_TLK(KPaths paths, bool LengthMatching)
        {
            TLK t = new TLK(paths.dialog);

            if (LengthMatching)
            {
                TLK t_ordered = new TLK(paths.dialog);

                t_ordered.String_Data_Table = t_ordered.String_Data_Table.OrderBy(x => x.StringText.Length).ToList();

                for (int i = 0; i < t.String_Data_Table.Count; i++)
                {   try
                    {
                        int index_offset = 0;
                        while (index_offset == 0)
                        {
                            index_offset = Randomize.Rng.Next(-5, 5);
                        }

                        // Could get faster execution time by matching the strings by lenght instead of text, but then there would be bias towards strings earlier in each lenght bracket.
                        var randomString = t_ordered.String_Data_Table[t_ordered.String_Data_Table.FindIndex(x => x.StringText == t.String_Data_Table[i].StringText) + index_offset];
                        TlkLookupTable.Add(i, new Tuple<string, string>(t.String_Data_Table[i].StringText, randomString.StringText));
                        t.String_Data_Table[i] = randomString;
                    }
                    catch (Exception ex)
                    {
                        if (ex is IndexOutOfRangeException || ex is ArgumentOutOfRangeException)
                        {
                            continue; //ignoring extreme cases
                        }
                        else
                        {
                            throw;
                        }
                    }
                    
                }
            }
            else
            {
                TLK t_orig = new TLK(paths.dialog);
                Randomize.FisherYatesShuffle(t.String_Data_Table);

                for (int i = 0; i < t.String_Data_Table.Count; i++)
                {
                    TlkLookupTable.Add(i, new Tuple<string, string>(t_orig.String_Data_Table[i].StringText, t.String_Data_Table[i].StringText));
                }
            }

            t.WriteToFile(paths.dialog);
        }

        internal static void Reset()
        {
            TlkLookupTable.Clear();
            EntriesLookupTable.Clear();
            RepliesLookupTable.Clear();
        }

        internal static void GenerateSpoilerLog(XLWorkbook workbook)
        {
            if (TlkLookupTable.Count == 0     &&
                EntriesLookupTable.Count == 0 &&
                RepliesLookupTable.Count == 0 )
            { return; }
            var ws = workbook.Worksheets.Add("Text");

            int i = 1;
            ws.Cell(i, 1).Value = "Seed";
            ws.Cell(i, 2).Value = Properties.Settings.Default.Seed;
            ws.Cell(i, 1).Style.Font.Bold = true;
            i += 2;     // Skip a row.

            // Text Randomization Settings
            ws.Cell(i, 1).Value = "Rando Type";
            ws.Cell(i, 1).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            ws.Cell(i, 1).Style.Font.Bold = true;
            ws.Cell(i, 2).Value = "Is Enabled";
            ws.Cell(i, 2).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            ws.Cell(i, 2).Style.Font.Bold = true;
            i++;

            var textSetting = Properties.Settings.Default.TextSettingsValue;
            var settings = new List<Tuple<string, bool>>()
            {
                new Tuple<string, bool>("Dialogue Randomization", textSetting.HasFlag(TextSettings.RandoDialogEntries) || textSetting.HasFlag(TextSettings.RandoDialogReplies)),
                new Tuple<string, bool>("Randomize Entries", textSetting.HasFlag(TextSettings.RandoDialogEntries)),
                new Tuple<string, bool>("Randomize Replies", textSetting.HasFlag(TextSettings.RandoDialogReplies)),
                new Tuple<string, bool>("Match Entry Sounds", textSetting.HasFlag(TextSettings.MatchEntrySoundsWText)),
                new Tuple<string, bool>("Randomize Additional Text", textSetting.HasFlag(TextSettings.RandoFullTLK)),
                new Tuple<string, bool>("Match Text Length", textSetting.HasFlag(TextSettings.MatchSimLengthStrings)),
            };

            foreach (var setting in settings)
            {
                ws.Cell(i, 1).Value = setting.Item1;
                ws.Cell(i, 2).Value = setting.Item2;
                ws.Cell(i, 1).Style.Font.Italic = true;
                i++;
            }

            i++;    // Skip a row.

            var jMax = 2;

            // Entries
            if (EntriesLookupTable.Any())
            {
                ws.Cell(i, 1).Value = "Entries Randomization";
                ws.Cell(i, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Cell(i, 1).Style.Font.Bold = true;
                ws.Range(i, 1, i, 3).Merge();
                i++;

                // Column Headings
                int j = 0;
                ws.Cell(i, ++j).Value = "Module Name";
                ws.Cell(i, j).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                ws.Cell(i, j).Style.Font.Italic = true;
                ws.Cell(i, ++j).Value = "RFile Label";
                ws.Cell(i, j).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                ws.Cell(i, j).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                ws.Cell(i, j).Style.Font.Italic = true;
                ws.Cell(i, ++j).Value = "Orig StrRef";
                ws.Cell(i, j).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                ws.Cell(i, j).Style.Font.Italic = true;
                ws.Cell(i, ++j).Value = "Rand StrRef";
                ws.Cell(i, j).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                ws.Cell(i, j).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                ws.Cell(i, j).Style.Font.Italic = true;

                if (textSetting.HasFlag(TextSettings.MatchEntrySoundsWText))
                {
                    ws.Cell(i, ++j).Value = "Orig VO_Ref";
                    ws.Cell(i, j).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    ws.Cell(i, j).Style.Font.Italic = true;
                    ws.Cell(i, ++j).Value = "Rand VO_Ref";
                    ws.Cell(i, j).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    ws.Cell(i, j).Style.Font.Italic = true;
                    ws.Cell(i, ++j).Value = "Orig Sound";
                    ws.Cell(i, j).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    ws.Cell(i, j).Style.Font.Italic = true;
                    ws.Cell(i, ++j).Value = "Rand Sound";
                    ws.Cell(i, j).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    ws.Cell(i, j).Style.Font.Italic = true;
                }

                if (jMax < j) jMax = j;
                i++;

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

                foreach (var map in EntriesLookupTable)
                {
                    foreach (var rfile in map.Value)
                    {
                        foreach (var set in rfile.Value)
                        {
                            ws.Cell(i, 1).Value = map.Key;
                            ws.Cell(i, 2).Value = rfile.Key;
                            ws.Cell(i, 3).Value = set.Item1;
                            ws.Cell(i, 4).Value = set.Item2;
                            if (textSetting.HasFlag(TextSettings.MatchEntrySoundsWText))
                            {
                                ws.Cell(i, 5).Value = set.Item3;
                                ws.Cell(i, 6).Value = set.Item4;
                                ws.Cell(i, 7).Value = set.Item5;
                                ws.Cell(i, 8).Value = set.Item6;
                                ws.Cell(i, 6).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                ws.Cell(i, 8).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            }

                            if (prevMap != map.Key)
                            {
                                prevMap = map.Key;
                                c = (c + 1) % colors.Count;
                            }
                            ws.Cell(i, 1).Style.Font.FontColor = colors[c];
                            ws.Cell(i, 2).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            ws.Cell(i, 4).Style.Border.RightBorder = XLBorderStyleValues.Thin;

                            i++;
                        }
                    }
                }

                i++;    // Skip a row.
            }

            // Replies
            if (RepliesLookupTable.Any())
            {
                ws.Cell(i, 1).Value = "Replies Randomization";
                ws.Cell(i, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Cell(i, 1).Style.Font.Bold = true;
                ws.Range(i, 1, i, 3).Merge();
                i++;

                ws.Cell(i, 1).Value = "Module Name";
                ws.Cell(i, 1).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                ws.Cell(i, 1).Style.Font.Italic = true;
                ws.Cell(i, 2).Value = "RFile Label";
                ws.Cell(i, 2).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                ws.Cell(i, 2).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                ws.Cell(i, 2).Style.Font.Italic = true;
                ws.Cell(i, 3).Value = "Orig StrRef";
                ws.Cell(i, 3).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                ws.Cell(i, 3).Style.Font.Italic = true;
                ws.Cell(i, 4).Value = "Rand StrRef";
                ws.Cell(i, 4).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                ws.Cell(i, 4).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                ws.Cell(i, 4).Style.Font.Italic = true;

                if (jMax < 4) jMax = 4;
                i++;

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

                foreach (var map in RepliesLookupTable)
                {
                    foreach (var rfile in map.Value)
                    {
                        foreach (var set in rfile.Value)
                        {
                            ws.Cell(i, 1).Value = map.Key;
                            ws.Cell(i, 2).Value = rfile.Key;
                            ws.Cell(i, 3).Value = set.Item1;
                            ws.Cell(i, 4).Value = set.Item2;

                            if (prevMap != map.Key)
                            {
                                prevMap = map.Key;
                                c = (c + 1) % colors.Count;
                            }
                            ws.Cell(i, 1).Style.Font.FontColor = colors[c];
                            ws.Cell(i, 2).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            ws.Cell(i, 4).Style.Border.RightBorder = XLBorderStyleValues.Thin;

                            i++;
                        }
                    }
                }

                i++;    // Skip a row.
            }

            // TLK File
            if (TlkLookupTable.Any())
            {
                ws.Cell(i, 1).Value = "Additional Randomization";
                ws.Cell(i, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Cell(i, 1).Style.Font.Bold = true;
                ws.Range(i, 1, i, 3).Merge();
                i++;

                ws.Cell(i, 1).Value = "Table Index";
                ws.Cell(i, 1).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                ws.Cell(i, 1).Style.Font.Italic = true;
                ws.Cell(i, 2).Value = "Orig Text";
                ws.Cell(i, 2).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                ws.Cell(i, 2).Style.Font.Italic = true;
                ws.Range(i, 2, i, 3).Merge();
                ws.Cell(i, 4).Value = "Rand Text";
                ws.Cell(i, 4).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                ws.Cell(i, 4).Style.Font.Italic = true;
                ws.Range(i, 4, i, 5).Merge();

                if (jMax < 5) jMax = 5;
                i++;

                foreach (var index in TlkLookupTable)
                {
                    const int MAX_STR_LENGTH = 100;

                    ws.Cell(i, 1).Value = index.Key;

                    if (index.Value.Item1.Length > MAX_STR_LENGTH)
                        ws.Cell(i, 2).Value = $"{index.Value.Item1.Substring(0, MAX_STR_LENGTH)}...";
                    else
                        ws.Cell(i, 2).Value = index.Value.Item1;


                    if (index.Value.Item2.Length > MAX_STR_LENGTH)
                        ws.Cell(i, 4).Value = $"{index.Value.Item2.Substring(0, MAX_STR_LENGTH)}...";
                    else
                        ws.Cell(i, 4).Value = index.Value.Item2;

                    ws.Cell(i, 2).Style.Alignment.WrapText = true;
                    ws.Range(i, 2, i, 3).Merge();
                    ws.Cell(i, 4).Style.Alignment.WrapText = true;
                    ws.Range(i, 4, i, 5).Merge();

                    i++;
                }

                i++;    // Skip a row.
            }

            // Adjust columns.
            for (int c = 1; c <= jMax; c++)
            {
                ws.Column(c).AdjustToContents();
            }
        }
    }
}