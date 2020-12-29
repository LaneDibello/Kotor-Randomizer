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
    public static class TextRando
    {
        private const int TLK_STRING_COUNT = 49264;

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

                foreach (RIM.rFile RF in r.File_Table.Where(x => x.TypeID == Reference_Tables.TypeCodes["DLG "]))
                {
                    GFF g = new GFF(RF.File_Data);

                    //Entries
                    if (Entries)
                    {
                        foreach (GFF.STRUCT S in (g.Top_Level.Fields.Where(x => x.Label == "EntryList").FirstOrDefault() as GFF.LIST).Structs)
                        {
                            if ((S.Fields.Where(x => x.Label == "Text").FirstOrDefault() as GFF.CExoLocString).StringRef != -1) //Avoid averwriting dialogue end indicators, and animation nodes
                            {
                                int str_ref = 0; //Find valid string
                                while (t.String_Data_Table[str_ref].SoundResRef == "") //Ensure the string we have has a sound to go with it
                                {
                                    str_ref = Randomize.Rng.Next(TLK_STRING_COUNT);
                                }

                                // Sound and Text Matching
                                if (SoundMatching)
                                {
                                    (S.Fields.Where(x => x.Label == "Text").FirstOrDefault() as GFF.CExoLocString).StringRef = str_ref;
                                    try
                                    {
                                        (S.Fields.Where(x => x.Label == "VO_ResRef").FirstOrDefault() as GFF.ResRef).Reference = t.String_Data_Table[str_ref].SoundResRef;
                                    }
                                    catch { }
                                    try
                                    {
                                        (S.Fields.Where(x => x.Label == "Sound").FirstOrDefault() as GFF.ResRef).Reference = t.String_Data_Table[str_ref].SoundResRef;
                                    }
                                    catch { } //If both VO_ResRef and Sound Fail we ignore the entry
                                }
                                else
                                {
                                    (S.Fields.Where(x => x.Label == "Text").FirstOrDefault() as GFF.CExoLocString).StringRef = str_ref;
                                }
                            }
                        }
                    }

                    //Replies
                    if (Replies)
                    {
                        foreach (GFF.STRUCT S in (g.Top_Level.Fields.Where(x => x.Label == "ReplyList").FirstOrDefault() as GFF.LIST).Structs)
                        {
                            if ((S.Fields.Where(x => x.Label == "Text").FirstOrDefault() as GFF.CExoLocString).StringRef != -1) //Avoid averwriting dialogue end indicators, and animation nodes
                            {
                                int str_ref = Randomize.Rng.Next(49264);
                                (S.Fields.Where(x => x.Label == "Text").FirstOrDefault() as GFF.CExoLocString).StringRef = str_ref;
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
        static void shuffle_TLK(KPaths paths, bool LengthMatching, int Length_Margin = 5)
        {
            TLK t = new TLK(paths.dialog);

            if (LengthMatching)
            {
                for (int i = 0; i < t.String_Data_Table.Count; i++)
                {
                    int margin = Length_Margin;

                    List<TLK.String_Data> marginalized_strings = t.String_Data_Table.Where(x => Math.Abs(x.StringText.Length - t.String_Data_Table[i].StringText.Length) < margin).ToList();
                    while (marginalized_strings.Count < 2)
                    {
                        margin++;
                        marginalized_strings = t.String_Data_Table.Where(x => Math.Abs(x.StringText.Length - t.String_Data_Table[i].StringText.Length) < margin).ToList();
                    }

                    t.String_Data_Table[i] = marginalized_strings[Randomize.Rng.Next(marginalized_strings.Count)];
                }
            }
            else
            {
                Randomize.FisherYatesShuffle(t.String_Data_Table);
            }

            t.WriteToFile(paths.dialog);
        }

    }
}