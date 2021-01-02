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

            //Party Rando
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
        }
    }
}
