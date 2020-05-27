using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace kotor_Randomizer_2
{
    public partial class ModuleForm
    {
        //Populates and shuffles the the modules flagged to be randomized. Returns true if override files should be added.
        public static bool Module_rando(out List<string> Shuffled_Mods)
        {
            //Split the Bound modules into their respective list
            Shuffled_Mods = Globals.BoundModules.Where(x => !x.ommitted).Select(x => x.name).ToList();

            Randomize.FisherYatesShuffle(Shuffled_Mods);

            return Properties.Settings.Default.AddOverideFiles.Count > 0;
        }
    }
}