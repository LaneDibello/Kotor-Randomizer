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
                List<string> male_names = Properties.Settings.Default.FirstnamesM.Cast<string>().Select(x => x.Trim()).ToList();
                LTR ltr_male_names = new LTR(male_names);
                List<string> female_names = Properties.Settings.Default.FirstnamesF.Cast<string>().Select(x => x.Trim()).ToList();
                LTR ltr_female_names = new LTR(female_names);
                List<string> last_names = Properties.Settings.Default.Lastnames.Cast<string>().Select(x => x.Trim()).ToList();
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


        }
    }
}
