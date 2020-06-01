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
        public static void other_rando(Globals.KPaths paths)
        {
            //NameGen
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
                    kWriter.Write(ltr_male_names, File.OpenWrite(paths.Override + "humanm.ltr"));
                }
                if (female_names.Any())
                {
                    kWriter.Write(ltr_female_names, File.OpenWrite(paths.Override + "humanf.ltr"));
                }
                if (last_names.Any())
                {
                    kWriter.Write(ltr_last_names, File.OpenWrite(paths.Override + "humanl.ltr"));
                }
            }

            //Polymorph

            //Random NPC Pazaak Decks
            if (Properties.Settings.Default.PazaakDecks)
            {
                string ops = "+-*";

                BIF b = KReader.ReadBIF(File.OpenRead(paths.data + "\\2da.bif"));
                KEY k = KReader.ReadKEY(File.OpenRead(paths.get_backup(paths.chitin)));
                b.attachKey(k, "data\\2da.bif");

                MemoryStream ms = new MemoryStream(b.Variable_Resource_Table.Where(x => x.ResRef == "pazaakdecks").FirstOrDefault().Entry_Data);

                TwoDA_REFRACTOR t = new TwoDA_REFRACTOR(ms, "pazaakdecks");

                foreach (string c in t.Columns)
                {
                    if (c == "deckname") { continue; }

                    t.Data[c][0] = "" + ops[Randomize.Rng.Next(0, 3)] + Convert.ToString(Randomize.Rng.Next(1, 7));
                    t.Data[c][1] = "" + ops[Randomize.Rng.Next(0, 3)] + Convert.ToString(Randomize.Rng.Next(1, 7));
                    t.Data[c][2] = "" + ops[Randomize.Rng.Next(0, 3)] + Convert.ToString(Randomize.Rng.Next(1, 7));
                    t.Data[c][3] = "" + ops[Randomize.Rng.Next(0, 3)] + Convert.ToString(Randomize.Rng.Next(1, 7));
                }

                t.write(File.OpenWrite(paths.Override + "pazaakdecks.2da"));

            }


        }
    }
}
