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
        public static readonly string PAZAAKDECKS_RESREF = "pazaakdecks";
        public static readonly string DECKNAME_COLUMN = "deckname";

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
        }
    }
}
