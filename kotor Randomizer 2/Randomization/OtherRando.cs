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
            if (Properties.Settings.Default.NameGenRando)
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

            // Random NPC Pazaak Decks
            if (Properties.Settings.Default.PazaakDecks)
            {
                string ops = "+-*";

                BIF b = new BIF(paths.data + "\\2da.bif");
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
