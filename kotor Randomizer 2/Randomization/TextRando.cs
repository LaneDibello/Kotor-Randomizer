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

        //Randomize Dialogue
        static void shuffle_dialogue(KPaths paths)
        {
            foreach (FileInfo fi in paths.FilesInModules)
            {
                if (fi.Name[fi.Name.Length - 5] != 's') { continue; }

                TLK t = new TLK(Path.Combine(paths.swkotor, "dialog.TLK"));
                RIM r = new RIM(fi.FullName);
            }
        }

    }
}