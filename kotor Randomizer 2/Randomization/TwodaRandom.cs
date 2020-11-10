using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using KotOR_IO;

namespace kotor_Randomizer_2
{
    public static class TwodaRandom
    {
        public static void Twoda_rando(Globals.KPaths paths)
        {
            BIF b = new BIF(Path.Combine(paths.data, "2da.bif"));
            KEY k = new KEY(paths.chitin_backup);
            b.AttachKey(k, "data\\2da.bif");

            foreach (BIF.VariableResourceEntry VRE in b.VariableResourceTable.Where(x => Globals.Selected2DAs.Keys.Contains(x.ResRef)))
            {
                TwoDA t = new TwoDA(VRE.EntryData, VRE.ResRef);

                foreach (string col in Globals.Selected2DAs[VRE.ResRef])
                {
                    Randomize.FisherYatesShuffle(t.Data[col]);
                }

                t.WriteToDirectory(paths.Override);
            }
        }
    }
}
