using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using KotOR_IO;

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
                    ltr_male_names.WriteToFile(paths.Override + "humanm.ltr");
                }
                if (female_names.Any())
                {
                    ltr_female_names.WriteToFile(paths.Override + "humanf.ltr");
                }
                if (last_names.Any())
                {
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
                    if (c == DECKNAME_COLUMN) { continue; }

                    // [+-*][1-6]
                    // "" + ops[Randomize.Rng.Next(0, 3)] + Convert.ToString(Randomize.Rng.Next(1, 7));
                    t.Data[c][0] = $"{ops[Randomize.Rng.Next(0, 3)]}{Convert.ToString(Randomize.Rng.Next(1, 7))}";
                    t.Data[c][1] = $"{ops[Randomize.Rng.Next(0, 3)]}{Convert.ToString(Randomize.Rng.Next(1, 7))}";
                    t.Data[c][2] = $"{ops[Randomize.Rng.Next(0, 3)]}{Convert.ToString(Randomize.Rng.Next(1, 7))}";
                    t.Data[c][3] = $"{ops[Randomize.Rng.Next(0, 3)]}{Convert.ToString(Randomize.Rng.Next(1, 7))}";
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
                    lyt.RandomizeLayout(
                        doTracks:    Properties.Settings.Default.RandomizeSwoopBoosters,
                        doObstacles: Properties.Settings.Default.RandomizeSwoopObstacles
                    );
                    lyt.WriteToFile(Path.Combine(paths.Override, $"{vre.ResRef}.lyt"));
                }
            }
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
