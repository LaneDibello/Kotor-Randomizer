using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using KotOR_IO;
using ClosedXML.Excel;

namespace kotor_Randomizer_2
{
    public static class OtherRando
    {
        private const string PAZAAKDECKS_RESREF = "pazaakdecks";
        private const string DECKNAME_COLUMN = "deckname";

        private const string BIF_LAYOUT = "layouts.bif";
        private const string LYT_SWOOP_TARIS    = "m03mg";
        private const string LYT_SWOOP_TATOOINE = "m17mg";
        private const string LYT_SWOOP_MANAAN   = "m26mg";

        private const string NAME_GEN_MALE = "male";
        private const string NAME_GEN_FEMALE = "female";
        private const string NAME_GEN_LAST = "last";

        // The ResRefs of the swoop races.
        private static readonly List<string> swoopLayouts = new List<string>()
        {
            LYT_SWOOP_TARIS,
            LYT_SWOOP_TATOOINE,
            LYT_SWOOP_MANAAN,
        };

        //List of Tuples matching each party member's various identifying data (item1 : dialogue file, item2 : file name/ResRef, item3 : Scripting Tag)
        private static readonly List<Tuple<string, string, string>> Party_IDs = new List<Tuple<string, string, string>>()
        {
            new Tuple<string, string, string>("k_hbas_dialog", "p_bastilla", "Bastila"),
            new Tuple<string, string, string>("k_hcan_dialog", "p_cand", "Cand"),
            new Tuple<string, string, string>("k_hcar_dialog", "p_carth", "Carth"),
            new Tuple<string, string, string>("k_hhkd_dialog", "p_hk47", "HK47"),
            new Tuple<string, string, string>("k_hjol_dialog", "p_jolee", "Jolee"),
            new Tuple<string, string, string>("k_hjuh_dialog", "p_juhani", "Juhani"),
            new Tuple<string, string, string>("k_hmis_dialog", "p_mission", "Mission"),
            new Tuple<string, string, string>("k_htmd_dialog", "p_t3m4", "T3M4"),
            new Tuple<string, string, string>("k_hzaa_dialog", "p_zaalbar", "Zaalbar"),
        };

        /// <summary>
        /// Lookup table for the male, female, and last names used.
        /// Usage: NameGenLookup[Type] = List(Names)
        /// </summary>
        private static Dictionary<string, List<string>> NameGenLookup { get; set; } = new Dictionary<string, List<string>>();

        /// <summary>
        /// Lookup table for how polymorph items are randomized.
        /// Usage: PolymorphLookupTable[ItemName] = Disguise;
        /// </summary>
        private static Dictionary<string, int> PolymorphLookupTable { get; set; } = new Dictionary<string, int>();

        /// <summary>
        /// Lookup table for how NPC pazaak decks are randomized.
        /// Usage: NpcPazaakLookupTable[CardColumn] = List(OriginalCard, RandomizedCard);
        /// </summary>
        private static Dictionary<string, List<Tuple<string, string>>> NpcPazaakLookupTable { get; set; } = new Dictionary<string, List<Tuple<string, string>>>();

        /// <summary>
        /// Lookup table for how party members are randomized.
        /// Usage: PartyLookupTable[PartyShortName] = RandomizedResRef;
        /// </summary>
        private static Dictionary<string, string> PartyLookupTable { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// Lookup table for how swoops are randomized.
        /// Usage: SwoopLookupTable[TrackName] = (OriginalLayout, RandomizedLayout);
        /// </summary>
        private static Dictionary<string, Tuple<Layout, Layout>> SwoopLookupTable { get; set; } = new Dictionary<string, Tuple<Layout, Layout>>();

        public static void other_rando(KPaths paths)
        {
            // NameGen
            if (Properties.Settings.Default.RandomizeNameGen)
            {
                List<string> male_names = Properties.Settings.Default.FirstnamesM.Cast<string>().Select(x => x.Trim()).Where(x => x.Length > 2).ToList();
                LTR ltr_male_names = new LTR(male_names);
                List<string> female_names = Properties.Settings.Default.FirstnamesF.Cast<string>().Select(x => x.Trim()).Where(x => x.Length > 2).ToList();
                LTR ltr_female_names = new LTR(female_names);
                List<string> last_names = Properties.Settings.Default.Lastnames.Cast<string>().Select(x => x.Trim()).Where(x => x.Length > 2).ToList();
                LTR ltr_last_names = new LTR(last_names);

                if (male_names.Any())
                {
                    NameGenLookup.Add(NAME_GEN_MALE, male_names);
                    ltr_male_names.WriteToFile(paths.Override + "humanm.ltr");
                }
                if (female_names.Any())
                {
                    NameGenLookup.Add(NAME_GEN_FEMALE, female_names);
                    ltr_female_names.WriteToFile(paths.Override + "humanf.ltr");
                }
                if (last_names.Any())
                {
                    NameGenLookup.Add(NAME_GEN_LAST, last_names);
                    ltr_last_names.WriteToFile(paths.Override + "humanl.ltr");
                }
            }

            // Polymorph
            if (Properties.Settings.Default.PolymorphMode)
            {
                BIF b = new BIF(paths.data + "templates.bif");
                KEY k = new KEY(paths.chitin);
                b.AttachKey(k, "data\\templates.bif");

                

                foreach (var res in b.VariableResourceTable.Where(x => x.ResourceType == ResourceType.UTI))
                {
                    GFF g = new GFF(res.EntryData);
                    int item_basetype = (g.Top_Level.Fields.Where(x => x.Label == "BaseItem").FirstOrDefault() as GFF.INT).value;
                    //ignore items that can't be equipped in the chest slot
                    if ((item_basetype < 35 || item_basetype > 43) && (item_basetype < 66 || item_basetype > 68) && item_basetype != 85 && item_basetype != 89)
                    {
                        continue;
                    }

                    ushort rando_appearance = 0;
                    while (Globals.BROKEN_CHARS.Contains((int)rando_appearance) || Globals.LARGE_CHARS.Contains((int)rando_appearance))
                    {
                        rando_appearance = (ushort)Randomize.Rng.Next(508);
                    }

                    //STRUCT that gives an item the "disguise" property
                    GFF.STRUCT disguise_prop = new GFF.STRUCT("", 0,
                        new List<GFF.FIELD>()
                        {
                        new GFF.BYTE("ChanceAppear", 100),
                        new GFF.BYTE("CostTable", 0),
                        new GFF.WORD("CostValue", 0),
                        new GFF.BYTE("Param1", 255),
                        new GFF.BYTE("Param1Value", 0),
                        new GFF.WORD("PropertyName", 59), //Disguise property
                        new GFF.WORD("Subtype", rando_appearance), //The random appearance value (same values used in model rando)
                        }
                        );

                    //Adds the disguise property to the UTI's property list
                    (g.Top_Level.Fields.Where(x => x.Label == "PropertiesList").FirstOrDefault() as GFF.LIST).Structs.Add(disguise_prop);

                    //GFF.ResRef resref = g.Top_Level.Fields.Where(x => x.Label == "TemplateResRef")?.FirstOrDefault() as GFF.ResRef;
                    //if (resref != null && !PolymorphLookupTable.ContainsKey(resref.Reference)) PolymorphLookupTable.Add(resref.Reference, rando_appearance.ToString());
                    PolymorphLookupTable.Add(res.ResRef, rando_appearance);

                    g.WriteToFile(paths.Override + res.ResRef + ".uti");
                    
                }
            }

            // Random NPC Pazaak Decks
            if (Properties.Settings.Default.RandomizePazaakDecks)
            {
                string ops = "+-*";

                BIF b = new BIF(paths.data + "2da.bif");
                KEY k = new KEY(paths.chitin_backup);
                b.AttachKey(k, "data\\2da.bif");

                var resource = b.VariableResourceTable.Where(x => x.ResRef == PAZAAKDECKS_RESREF).FirstOrDefault();
                if (resource == null)
                {
                    throw new ArgumentOutOfRangeException($"The ResRef \"{PAZAAKDECKS_RESREF}\" could not be found.");
                }

                TwoDA t = new TwoDA(resource.EntryData, PAZAAKDECKS_RESREF);

                foreach (string c in t.Columns)
                {
                    if (c == DECKNAME_COLUMN)
                    {
                        NpcPazaakLookupTable.Add(c, new List<Tuple<string, string>>()
                        {
                            new Tuple<string, string>(t.Data[c][0], t.Data[c][0]),
                            new Tuple<string, string>(t.Data[c][1], t.Data[c][1]),
                            new Tuple<string, string>(t.Data[c][2], t.Data[c][2]),
                            new Tuple<string, string>(t.Data[c][3], t.Data[c][3]),
                        });
                        continue;
                    }
                    NpcPazaakLookupTable.Add(c, new List<Tuple<string, string>>());

                    // [+-*][1-6]
                    // "" + ops[Randomize.Rng.Next(0, 3)] + Convert.ToString(Randomize.Rng.Next(1, 7));
                    var card0 = $"{ops[Randomize.Rng.Next(0, 3)]}{Convert.ToString(Randomize.Rng.Next(1, 7))}";
                    var card1 = $"{ops[Randomize.Rng.Next(0, 3)]}{Convert.ToString(Randomize.Rng.Next(1, 7))}";
                    var card2 = $"{ops[Randomize.Rng.Next(0, 3)]}{Convert.ToString(Randomize.Rng.Next(1, 7))}";
                    var card3 = $"{ops[Randomize.Rng.Next(0, 3)]}{Convert.ToString(Randomize.Rng.Next(1, 7))}";

                    NpcPazaakLookupTable[c].Add(new Tuple<string, string>(t.Data[c][0], card0));
                    NpcPazaakLookupTable[c].Add(new Tuple<string, string>(t.Data[c][1], card1));
                    NpcPazaakLookupTable[c].Add(new Tuple<string, string>(t.Data[c][2], card2));
                    NpcPazaakLookupTable[c].Add(new Tuple<string, string>(t.Data[c][3], card3));

                    t.Data[c][0] = card0;
                    t.Data[c][1] = card1;
                    t.Data[c][2] = card2;
                    t.Data[c][3] = card3;
                }

                t.WriteToDirectory(paths.Override);
            }

            // Party Rando
            if (Properties.Settings.Default.RandomizePartyMembers) //ADD SETTING
            {
                BIF b = new BIF(paths.data + "templates.bif");
                KEY k = new KEY(paths.chitin_backup);
                b.AttachKey(k, "data\\templates.bif");

                foreach (var ID in Party_IDs)
                {
                    //Find a creature that isn't the party member
                    var resource = b.VariableResourceTable.Where(x => x.ResRef != ID.Item2 && x.ResourceType == ResourceType.UTC).ToList()[Randomize.Rng.Next(155)];
                    PartyLookupTable.Add(ID.Item3, resource.ResRef);

                    GFF g = new GFF(resource.EntryData);

                    //Turns Creature File into a playable companion replacing the current party member
                    (g.Top_Level.Fields.Where(x => x.Label == "Conversation").FirstOrDefault() as GFF.ResRef).Reference = ID.Item1;
                    (g.Top_Level.Fields.Where(x => x.Label == "Tag").FirstOrDefault() as GFF.CExoString).CEString = ID.Item3;
                    (g.Top_Level.Fields.Where(x => x.Label == "TemplateResRef").FirstOrDefault() as GFF.ResRef).Reference = ID.Item2;
                    (g.Top_Level.Fields.Where(x => x.Label == "NoPermDeath").FirstOrDefault() as GFF.BYTE).value = 1;
                    (g.Top_Level.Fields.Where(x => x.Label == "FactionID").FirstOrDefault() as GFF.WORD).value = 2;

                    //Henchmen Script suite
                    (g.Top_Level.Fields.Where(x => x.Label == "ScriptHeartbeat").FirstOrDefault() as GFF.ResRef).Reference = "k_hen_heartbt01";
                    (g.Top_Level.Fields.Where(x => x.Label == "ScriptOnNotice").FirstOrDefault() as GFF.ResRef).Reference = "k_hen_percept01";
                    (g.Top_Level.Fields.Where(x => x.Label == "ScriptSpellAt").FirstOrDefault() as GFF.ResRef).Reference = "";
                    (g.Top_Level.Fields.Where(x => x.Label == "ScriptAttacked").FirstOrDefault() as GFF.ResRef).Reference = "k_hen_attacked01";
                    (g.Top_Level.Fields.Where(x => x.Label == "ScriptDamaged").FirstOrDefault() as GFF.ResRef).Reference = "k_hen_damage01";
                    (g.Top_Level.Fields.Where(x => x.Label == "ScriptDisturbed").FirstOrDefault() as GFF.ResRef).Reference = "";
                    (g.Top_Level.Fields.Where(x => x.Label == "ScriptEndRound").FirstOrDefault() as GFF.ResRef).Reference = "k_hen_combend01";
                    (g.Top_Level.Fields.Where(x => x.Label == "ScriptEndDialogu").FirstOrDefault() as GFF.ResRef).Reference = "";
                    (g.Top_Level.Fields.Where(x => x.Label == "ScriptDialogue").FirstOrDefault() as GFF.ResRef).Reference = "k_hen_dialogue01";
                    (g.Top_Level.Fields.Where(x => x.Label == "ScriptSpawn").FirstOrDefault() as GFF.ResRef).Reference = "k_hen_spawn01";
                    (g.Top_Level.Fields.Where(x => x.Label == "ScriptRested").FirstOrDefault() as GFF.ResRef).Reference = "";
                    (g.Top_Level.Fields.Where(x => x.Label == "ScriptDeath").FirstOrDefault() as GFF.ResRef).Reference = "";
                    (g.Top_Level.Fields.Where(x => x.Label == "ScriptOnBlocked").FirstOrDefault() as GFF.ResRef).Reference = "k_hen_blocked01";
                    (g.Top_Level.Fields.Where(x => x.Label == "ScriptUserDefine").FirstOrDefault() as GFF.ResRef).Reference = "";

                    //Add a Dummy Feat to prevent the feats menu from crashing
                    (g.Top_Level.Fields.Where(x => x.Label == "FeatList").FirstOrDefault() as GFF.LIST).Structs.Add(new GFF.STRUCT("", 1, new List<GFF.FIELD>() {new GFF.WORD("Feat", 27) }));

                    g.WriteToFile(paths.Override + ID.Item2 + ".utc");

                }

            }

            // Swoop Rando
            if (Properties.Settings.Default.RandomizeSwoopBoosters ||
                Properties.Settings.Default.RandomizeSwoopObstacles)
            {
                // Get the bif containing layout files.
                BIF b = new BIF(Path.Combine(paths.data, BIF_LAYOUT));
                KEY k = new KEY(paths.chitin_backup);
                b.AttachKey(k, $"data\\{BIF_LAYOUT}");

                // Limit search to just the swoop layouts.
                var VRT = b.VariableResourceTable.Where(x => swoopLayouts.Contains(x.ResRef));

                // Parse each swoop layout, randomize the requested elements, and write to the Override folder.
                foreach (BIF.VariableResourceEntry vre in VRT)
                {
                    Layout lyt = new Layout(vre.EntryData);
                    Layout lytOrig = new Layout(lyt);

                    lyt.RandomizeLayout(
                        doTracks:    Properties.Settings.Default.RandomizeSwoopBoosters,
                        doObstacles: Properties.Settings.Default.RandomizeSwoopObstacles
                    );

                    string trackName = "";
                    switch (vre.ResRef)
                    {
                        default:
                            break;
                        case LYT_SWOOP_TARIS:
                            trackName = "Taris";
                            break;
                        case LYT_SWOOP_TATOOINE:
                            trackName = "Tatooine";
                            break;
                        case LYT_SWOOP_MANAAN:
                            trackName = "Manaan";
                            break;
                    }

                    SwoopLookupTable.Add(trackName, new Tuple<Layout, Layout>(lytOrig, lyt));
                    lyt.WriteToFile(Path.Combine(paths.Override, $"{vre.ResRef}.lyt"));
                }
            }
        }

        internal static void GenerateSpoilerLog(XLWorkbook workbook)
        {
            if (NameGenLookup.Count == 0        &&
                PolymorphLookupTable.Count == 0 &&
                NpcPazaakLookupTable.Count == 0 &&
                PartyLookupTable.Count == 0     &&
                SwoopLookupTable.Count == 0     )
            { return; }
            var ws = workbook.Worksheets.Add("Other");

            var paths = new KPaths(Properties.Settings.Default.Kotor1Path);
            TLK tlk = new TLK(File.Exists(paths.dialog_backup) ? paths.dialog_backup : paths.dialog);
            KEY key = new KEY(paths.chitin_backup);
            BIF bifTmp = new BIF(Path.Combine(paths.data, "templates.bif"));
            bifTmp.AttachKey(key, "data\\templates.bif");

            int i = 1;
            ws.Cell(i, 1).Value = "Seed";
            ws.Cell(i, 2).Value = Properties.Settings.Default.Seed;
            ws.Cell(i, 1).Style.Font.Bold = true;
            i += 2;     // Skip a row.

            int jMax = 2;   // Remember the widest table.

            // Other Randomization Settings
            ws.Cell(i, 1).Value = "Rando Type";
            ws.Cell(i, 2).Value = "Is Enabled";
            ws.Cell(i, 1).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            ws.Cell(i, 1).Style.Font.Bold = true;
            ws.Cell(i, 2).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            ws.Cell(i, 2).Style.Font.Bold = true;
            i++;

            var settings = new List<Tuple<string, bool>>()
            {
                new Tuple<string, bool>("Name Gen Rando",   Properties.Settings.Default.RandomizeNameGen),
                new Tuple<string, bool>("Polymorph Mode",   Properties.Settings.Default.PolymorphMode),
                new Tuple<string, bool>("NPC Pazaak Decks", Properties.Settings.Default.RandomizePazaakDecks),
                new Tuple<string, bool>("Party Members",    Properties.Settings.Default.RandomizePartyMembers),
                new Tuple<string, bool>("Swoop Boosters",   Properties.Settings.Default.RandomizeSwoopBoosters),
                new Tuple<string, bool>("Swoop Obstacles",  Properties.Settings.Default.RandomizeSwoopObstacles),
            };

            foreach (var setting in settings)
            {
                ws.Cell(i, 1).Value = setting.Item1;
                ws.Cell(i, 2).Value = setting.Item2;
                ws.Cell(i, 1).Style.Font.Italic = true;
                i++;
            }

            i++;    // Skip a row.

            // Name Gen Randomization
            if (NameGenLookup.Any())
            {
                ws.Cell(i, 1).Value = "Name Generator Rando";
                ws.Cell(i, 1).Style.Font.Bold = true;
                ws.Cell(i, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Range(i, 1, i, 3).Merge();
                i++;

                int iStart = i;
                int iDone = i;
                int j = 1;

                foreach (var type in NameGenLookup)
                {
                    if (jMax < j) jMax = j + 1;     // Remember the width of the widest table.

                    // Column Header
                    i = iStart;
                    ws.Cell(i, j).Value = type.Key;
                    ws.Cell(i, j).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    ws.Cell(i, j).Style.Font.Italic = true;
                    i++;

                    foreach (var row in type.Value)
                    {
                        ws.Cell(i, j).Value = row;
                        i++;
                    }

                    j++;    // Move to the next column.
                    if (iDone < i) iDone = i;   // Remember the length of this table.
                }

                i = iDone + 1;  // Skip a row.
            }

            // NPC Pazaak Deck Randomization
            if (NpcPazaakLookupTable.Any())
            {
                const string RANDOM = "randomized";

                ws.Cell(i, 1).Value = "NPC Pazaak Deck Rando";
                ws.Cell(i, 1).Style.Font.Bold = true;
                ws.Cell(i, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Range(i, 1, i, 2).Merge();
                i++;

                int iStart = i;
                int iDone = i;
                int j = 1;

                if (NpcPazaakLookupTable.ContainsKey(DECKNAME_COLUMN))
                {
                    ws.Cell(i, 1).Value = DECKNAME_COLUMN;
                    ws.Cell(i, 1).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    ws.Cell(i, 1).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    ws.Cell(i, 1).Style.Font.Italic = true;
                    i++;

                    foreach (var name in NpcPazaakLookupTable[DECKNAME_COLUMN])
                    {
                        ws.Cell(i, 1).Value = name.Item1;
                        ws.Cell(i, 1).Style.Border.RightBorder = XLBorderStyleValues.Thin;

                        ws.Cell(i + 1, j).Value = RANDOM;
                        ws.Cell(i + 1, j).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                        ws.Cell(i + 1, j).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        ws.Cell(i + 1, j).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        ws.Cell(i + 1, j).Style.Font.Italic = true;
                        i += 2;
                    }

                    j++;
                    iDone = i;
                }

                foreach (var col in NpcPazaakLookupTable)
                {
                    if (col.Key == DECKNAME_COLUMN) { continue; }
                    if (jMax < j) jMax = j + 1;     // Remember the width of the widest table.

                    // Column Header
                    i = iStart;
                    ws.Cell(i, j).Value = $"{col.Key}";
                    ws.Cell(i, j).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    ws.Cell(i, j).Style.Font.Italic = true;
                    i++;

                    foreach (var row in col.Value)
                    {
                        // Row Data
                        ws.Cell(i, j).Value = $"'{row.Item1}";
                        ws.Cell(i + 1, j).Value = $"'{row.Item2}";
                        ws.Cell(i + 1, j).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        i += 2;
                    }

                    j++;
                    if (iDone < i) iDone = i;   // Remember the length of this table.
                }

                i = iDone + 1;  // Skip a row.
            }

            // Party Randomization
            if (PartyLookupTable.Any())
            {
                var chars = bifTmp.VariableResourceTable.Where(x => x.ResourceType == ResourceType.UTC);

                ws.Cell(i, 1).Value = "Party Member Rando";
                ws.Cell(i, 1).Style.Font.Bold = true;
                ws.Cell(i, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Range(i, 1, i, 2).Merge();
                i++;

                ws.Cell(i, 1).Value = "Party Member";
                ws.Cell(i, 2).Value = "Rando ResRef";
                ws.Cell(i, 3).Value = "Rando Name";

                ws.Cell(i, 1).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                ws.Cell(i, 2).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                ws.Cell(i, 3).Style.Border.BottomBorder = XLBorderStyleValues.Thin;

                ws.Cell(i, 1).Style.Font.Italic = true;
                ws.Cell(i, 2).Style.Font.Italic = true;
                ws.Cell(i, 3).Style.Font.Italic = true;
                i++;

                foreach (var kvp in PartyLookupTable)
                {
                    string randName = "";
                    var randCharVre = chars.FirstOrDefault(x => x.ResRef == kvp.Value);
                    if (randCharVre != null)
                    {
                        GFF randChar = new GFF(randCharVre.EntryData);
                        if (randChar.Top_Level.Fields.FirstOrDefault(x => x.Label == "FirstName") is GFF.CExoLocString field)
                            randName = tlk.String_Data_Table[field.StringRef].StringText;
                    }

                    ws.Cell(i, 1).Value = kvp.Key;
                    ws.Cell(i, 2).Value = kvp.Value;
                    ws.Cell(i, 3).Value = randName;
                    i++;
                }

                i++;    // Skip a row.
            }

            // Polymorph Equipment
            if (PolymorphLookupTable.Any())
            {
                const string CHAR_2DA = "appearance";
                const string COL_LABEL = "label";

                BIF bif2da = new BIF(Path.Combine(paths.data, "2da.bif"));
                bif2da.AttachKey(key, "data\\2da.bif");

                var items = bifTmp.VariableResourceTable.Where(x => x.ResourceType == ResourceType.UTI);
                var charVRE = bif2da.VariableResourceTable.Where(x => x.ResRef == CHAR_2DA).FirstOrDefault();
                TwoDA char2DA = new TwoDA(charVRE.EntryData, charVRE.ResRef);

                ws.Cell(i, 1).Value = "Equipment Polymorph Mode";
                ws.Cell(i, 1).Style.Font.Bold = true;
                ws.Cell(i, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Range(i, 1, i, 2).Merge();
                i++;

                // Column Headers
                ws.Cell(i, 1).Value = "Item Code";
                ws.Cell(i, 2).Value = "Real Item Name";
                ws.Cell(i, 3).Value = "Model ID";
                ws.Cell(i, 4).Value = "Model Name";
                ws.Cell(i, 1).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                ws.Cell(i, 2).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                ws.Cell(i, 3).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                ws.Cell(i, 4).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                ws.Cell(i, 1).Style.Font.Italic = true;
                ws.Cell(i, 2).Style.Font.Italic = true;
                ws.Cell(i, 3).Style.Font.Italic = true;
                ws.Cell(i, 4).Style.Font.Italic = true;
                i++;

                foreach (var kvp in PolymorphLookupTable)
                {
                    string origItemName = "";
                    var itemCode = ItemRando.LookupTable.FirstOrDefault(x => x.Item2 == kvp.Key)?.Item1 ?? kvp.Key;
                    var randItemVre = items.FirstOrDefault(x => x.ResRef == itemCode);

                    if (randItemVre != null)
                    {
                        GFF randItem = new GFF(randItemVre.EntryData);
                        if (randItem.Top_Level.Fields.FirstOrDefault(x => x.Label == "LocalizedName") is GFF.CExoLocString field)
                            origItemName = tlk.String_Data_Table[field.StringRef].StringText;
                    }

                    ws.Cell(i, 1).Value = kvp.Key;
                    ws.Cell(i, 2).Value = origItemName;
                    ws.Cell(i, 3).Value = kvp.Value;
                    ws.Cell(i, 4).Value = char2DA.Data[COL_LABEL][kvp.Value];
                    i++;
                }

                i++;    // Skip a row.
            }

            // Swoop Layout Randomization
            if (SwoopLookupTable.Any())
            {
                ws.Cell(i, 1).Value = "Swoop Layout Rando";
                ws.Cell(i, 1).Style.Font.Bold = true;
                ws.Cell(i, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Range(i, 1, i, 2).Merge();
                i++;

                foreach (var track in SwoopLookupTable)
                {
                    ws.Cell(i, 1).Value = track.Key;
                    ws.Cell(i, 1).Style.Font.Italic = true;
                    ws.Cell(i, 1).Style.Font.Underline = XLFontUnderlineValues.Single;
                    ws.Cell(i, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Range(i, 1, i, 2).Merge();
                    i++;

                    var iStart = i;
                    int iDone = i;
                    int j = 1;

                    if (Properties.Settings.Default.RandomizeSwoopBoosters)
                    {
                        // Column Headers
                        ws.Cell(i, j).Value = "Boosters";
                        ws.Cell(i, j).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell(i, j).Style.Font.Bold = true;
                        ws.Range(i, j, i, j+4).Merge().Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        i++;

                        ws.Cell(i, j).Value = "Name";
                        ws.Cell(i, j).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        ws.Cell(i, j).Style.Font.Italic = true;

                        ws.Cell(i, j+1).Value = "x Orig";
                        ws.Cell(i, j+1).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        ws.Cell(i, j+1).Style.Font.Italic = true;

                        ws.Cell(i, j+2).Value = "x Rand";
                        ws.Cell(i, j+2).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        ws.Cell(i, j+2).Style.Font.Italic = true;

                        ws.Cell(i, j+3).Value = "y Orig";
                        ws.Cell(i, j+3).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        ws.Cell(i, j+3).Style.Font.Italic = true;

                        ws.Cell(i, j+4).Value = "y Rand";
                        ws.Cell(i, j+4).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        ws.Cell(i, j+4).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        ws.Cell(i, j+4).Style.Font.Italic = true;
                        i++;

                        for (int t = 0; t < track.Value.Item1.Tracks.Count; t++)
                        {
                            ws.Cell(i, j+0).Value = track.Value.Item1.Tracks[t].Item1;
                            ws.Cell(i, j+1).Value = track.Value.Item1.Tracks[t].Item2;
                            ws.Cell(i, j+2).Value = track.Value.Item2.Tracks[t].Item2;
                            ws.Cell(i, j+3).Value = track.Value.Item1.Tracks[t].Item3;
                            ws.Cell(i, j+4).Value = track.Value.Item2.Tracks[t].Item3;
                            ws.Cell(i, j+4).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            i++;
                        }

                        j += 5;     // Move to the next set of columns.
                        if (iDone < i) iDone = i;   // Remember the length of this table.
                    }

                    if (Properties.Settings.Default.RandomizeSwoopObstacles)
                    {
                        i = iStart;

                        // Column Headers
                        ws.Cell(i, j).Value = "Obstacles";
                        ws.Cell(i, j).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell(i, j).Style.Font.Bold = true;
                        ws.Range(i, j, i, j+4).Merge().Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        i++;

                        ws.Cell(i, j).Value = "Name";
                        ws.Cell(i, j).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        ws.Cell(i, j).Style.Font.Italic = true;

                        ws.Cell(i, j+1).Value = "x Orig";
                        ws.Cell(i, j+1).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        ws.Cell(i, j+1).Style.Font.Italic = true;

                        ws.Cell(i, j+2).Value = "x Rand";
                        ws.Cell(i, j+2).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        ws.Cell(i, j+2).Style.Font.Italic = true;

                        ws.Cell(i, j+3).Value = "y Orig";
                        ws.Cell(i, j+3).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        ws.Cell(i, j+3).Style.Font.Italic = true;

                        ws.Cell(i, j+4).Value = "y Rand";
                        ws.Cell(i, j+4).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        ws.Cell(i, j+4).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        ws.Cell(i, j+4).Style.Font.Italic = true;
                        i++;

                        for (int t = 0; t < track.Value.Item1.Obstacles.Count; t++)
                        {
                            ws.Cell(i, j+0).Value = track.Value.Item1.Obstacles[t].Item1;
                            ws.Cell(i, j+1).Value = track.Value.Item1.Obstacles[t].Item2;
                            ws.Cell(i, j+2).Value = track.Value.Item2.Obstacles[t].Item2;
                            ws.Cell(i, j+3).Value = track.Value.Item1.Obstacles[t].Item3;
                            ws.Cell(i, j+4).Value = track.Value.Item2.Obstacles[t].Item3;
                            ws.Cell(i, j+4).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            i++;
                        }

                        if (iDone < i) iDone = i;   // Remember the length of this table.
                    }

                    i = iDone + 1;  // Skip a row.
                    if (jMax < j) jMax = j + 4;     // Remember the width of the widest table.
                    j = 1;          // Reset to column A.
                }
            }

            // Adjust columns.
            for (int c = 1; c <= jMax; c++)
            {
                ws.Column(c).AdjustToContents();
            }
        }

        internal static void Reset()
        {
            NameGenLookup.Clear();
            PolymorphLookupTable.Clear();
            NpcPazaakLookupTable.Clear();
            PartyLookupTable.Clear();
            SwoopLookupTable.Clear();
        }
    }

    public class Layout
    {
        // Parsed elements within the layout file.
        public string FileDependancy { get; set; }
        public List<Tuple<string, double, double, double>> Rooms     { get; set; } = new List<Tuple<string, double, double, double>>();
        public List<Tuple<string, double, double, double>> Tracks    { get; set; } = new List<Tuple<string, double, double, double>>();
        public List<Tuple<string, double, double, double>> Obstacles { get; set; } = new List<Tuple<string, double, double, double>>();
        public List<Tuple<string, double, double, double>> DoorHooks { get; set; } = new List<Tuple<string, double, double, double>>();

        /// <summary>
        /// Parse raw byte data as a layout file.
        /// </summary>
        /// <param name="rawData">Byte array to parse.</param>
        public Layout(byte[] rawData)
            : this(new MemoryStream(rawData))
        { }

        /// <summary>
        /// Parse layout file from stream.
        /// </summary>
        /// <param name="s">Stream to parse.</param>
        public Layout(Stream s)
        {
            using (StreamReader sr = new StreamReader(s))
            {
                // Read stream as plain text, then split it into individual lines of text.
                string content = sr.ReadToEnd();
                var lines = content.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                bool isRoom = false;
                bool isTrack = false;
                bool isObstacle = false;
                bool isDoorHook = false;

                // Parse each line.
                foreach (var line in lines)
                {
                    var split = line.Trim().Split(' ');

                    switch (split[0])
                    {
                        case "#MAXLAYOUT":
                        case "beginlayout":
                        case "donelayout":
                            continue;   // Skip these lines.
                        case "filedependancy":
                            FileDependancy = split.Last();
                            continue;
                        case "roomcount":
                            isRoom  = true;
                            isTrack = false;
                            isObstacle = false;
                            isDoorHook = false;
                            continue;   // Set default action to add to rooms.
                        case "trackcount":
                            isRoom  = false;
                            isTrack = true;
                            isObstacle = false;
                            isDoorHook = false;
                            continue;   // Set default action to add to tracks.
                        case "obstaclecount":
                            isRoom  = false;
                            isTrack = false;
                            isObstacle = true;
                            isDoorHook = false;
                            continue;   // Set default action to add to obstacles.
                        case "doorhookcount":
                            isRoom  = false;
                            isTrack = false;
                            isObstacle = false;
                            isDoorHook = true;
                            continue;   // Set default action to add to doorhooks.
                        default:
                            break;
                    }

                    // Line should consist of a name and 3 doubles representing the object's position in space.
                    if (split.Count() < 4)
                    {
                        continue; // Not enough values, so skip.
                    }

                    // Parse the available doubles.
                    double.TryParse(split[1], out double x);
                    double.TryParse(split[2], out double y);
                    double.TryParse(split[3], out double z);
                    Tuple<string, double, double, double> obj = new Tuple<string, double, double, double>(split[0], x, y, z);

                    // Add to the appropriate list, and continue to the next line.
                    if (isRoom)     { Rooms.Add(obj);     continue; }
                    if (isTrack)    { Tracks.Add(obj);    continue; }
                    if (isObstacle) { Obstacles.Add(obj); continue; }
                    if (isDoorHook) { DoorHooks.Add(obj); continue; }
                }
            }
        }

        /// <summary>
        /// Creates a copy of the given layout.
        /// </summary>
        public Layout(Layout l)
        {
            foreach (var room in l.Rooms)
                Rooms.Add(new Tuple<string, double, double, double>(room.Item1, room.Item2, room.Item3, room.Item4));

            foreach (var track in l.Tracks)
                Tracks.Add(new Tuple<string, double, double, double>(track.Item1, track.Item2, track.Item3, track.Item4));

            foreach (var obstacle in l.Obstacles)
                Obstacles.Add(new Tuple<string, double, double, double>(obstacle.Item1, obstacle.Item2, obstacle.Item3, obstacle.Item4));

            foreach (var doorhook in l.DoorHooks)
                DoorHooks.Add(new Tuple<string, double, double, double>(doorhook.Item1, doorhook.Item2, doorhook.Item3, doorhook.Item4));
        }

        /// <summary>
        /// Randomize the position of the objects in each requested list.
        /// </summary>
        public void RandomizeLayout(bool doRooms = false, bool doTracks = false, bool doObstacles = false, bool doDoorHooks = false)
        {
            if (doRooms)
            {
                // Not yet implemented.
            }

            if (doTracks)
            {
                // Use the standard min and max for all swoop tracks.
                var minimum = new Tuple<double, double, double>( 80.796, 170.042, 0.0);
                var maximum = new Tuple<double, double, double>(123.842, 3778.61, 0.0);

                // Skip the first item, which is the track itself.
                var boosters = Tracks.Where(x => !x.Item1.EndsWith("01")).ToList();
                RandomizeCoordinates(boosters, minimum, maximum);

                // Assign randomized values back to the full list, skipping any whose name ends in 01.
                var trackCount = Tracks.Count;
                int j = 0;
                for (int i = 0; i < trackCount; i++)
                {
                    if (Tracks[i].Item1.EndsWith("01"))
                    {
                        //j--; // Don't change j.
                        continue;
                    }

                    Tracks[i] = boosters[j];
                    j++;
                }
            }

            if (doObstacles)
            {
                // Use the standard min and max for all swoop tracks.
                var minimum = new Tuple<double, double, double>( 80.796, 170.042, 0.0);
                var maximum = new Tuple<double, double, double>(123.842, 3778.61, 0.0);
                RandomizeCoordinates(Obstacles, minimum, maximum);
            }

            if (doDoorHooks)
            {
                // Not yet implemented.
            }
        }

        /// <summary>
        /// Generates new coordinates for each object in the list between the min and max given.
        /// </summary>
        private void RandomizeCoordinates(List<Tuple<string, double, double, double>> objs,
                                          Tuple<double, double, double> min,
                                          Tuple<double, double, double> max)
        {
            var count = objs.Count;
            for (int i = 0; i < count; i++)
            {
                objs[i] = new Tuple<string, double, double, double>(
                    objs[i].Item1,
                    Randomize.GetRandomDouble(min.Item1, max.Item1),
                    Randomize.GetRandomDouble(min.Item2, max.Item2),
                    Randomize.GetRandomDouble(min.Item3, max.Item3));
            }
        }

        /// <summary>
        /// Write layout data to stream.
        /// </summary>
        public void Write(Stream s)
        {
            using (StreamWriter sw = new StreamWriter(s))
            {
                sw.WriteLine("#MAXLAYOUT ASCII");
                sw.WriteLine($"filedependancy {FileDependancy}");
                sw.WriteLine("beginlayout");

                sw.WriteLine($"   roomcount {Rooms.Count}");
                foreach (var room in Rooms)
                    sw.WriteLine($"      {room.Item1} {room.Item2} {room.Item3} {room.Item4}");

                sw.WriteLine($"   trackcount {Tracks.Count}");
                foreach (var track in Tracks)
                    sw.WriteLine($"      {track.Item1} {track.Item2} {track.Item3} {track.Item4}");

                sw.WriteLine($"   obstaclecount {Obstacles.Count}");
                foreach (var obstacle in Obstacles)
                    sw.WriteLine($"      {obstacle.Item1} {obstacle.Item2} {obstacle.Item3} {obstacle.Item4}");

                sw.WriteLine($"   doorhookcount {DoorHooks.Count}");
                foreach (var doorhook in DoorHooks)
                    sw.WriteLine($"      {doorhook.Item1} {doorhook.Item2} {doorhook.Item3} {doorhook.Item4}");

                sw.WriteLine("donelayout");
            }
        }

        /// <summary>
        /// Write layout data to file.
        /// </summary>
        public void WriteToFile(string path)
        {
            Write(File.OpenWrite(path));
        }

        /// <summary>
        /// Get layout data as a byte array.
        /// </summary>
        public byte[] ToRawData()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Write(ms);
                return ms.ToArray();
            }
        }
    }
}
