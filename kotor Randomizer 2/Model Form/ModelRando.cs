using System.Linq;
using System.IO;
using KotOR_IO;

namespace kotor_Randomizer_2
{
    public static class ModelRando
    {
        public static void model_rando(Globals.KPaths paths)
        {
            DirectoryInfo di = new DirectoryInfo(paths.modules);
            
            foreach (FileInfo fi in di.GetFiles())
            {
                RIM r = KReader.ReadRIM(fi.OpenRead());

                //Doors
                if ((Properties.Settings.Default.RandomizeDoorModels & 1) > 0)
                {
                    foreach (RIM.rFile rf in r.File_Table.Where(x => x.TypeID == 2042))
                    {
                        GFF g = new GFF(rf.File_Data);

                        int temp = 0;

                        if ((Properties.Settings.Default.RandomizeDoorModels & 4) > 0)//Broken Doors
                        {
                            temp = Randomize.Rng.Next(13, 64); //Fisrt 12 doors are open so this is easier
                        }
                        else
                        {
                            temp = Randomize.Rng.Next(0, 64);
                        }

                        if ((Properties.Settings.Default.RandomizeDoorModels & 2) > 0 && (g.Field_Array.Where(x => x.Label == "LocName").FirstOrDefault().Field_Data as GFF.CExoLocString).StringRef == 21080) { continue; }//Airlock
                        g.Field_Array.Where(k => k.Label == "GenericType").FirstOrDefault().Field_Data = temp;
                        g.Field_Array.Where(k => k.Label == "GenericType").FirstOrDefault().DataOrDataOffset = temp;

                        MemoryStream ms = new MemoryStream();

                        kWriter.Write(g, ms);

                        rf.File_Data = ms.ToArray();
                    }
                }

                //Placeables
                if ((Properties.Settings.Default.RandomizePlaceModels & 1) > 0)
                {
                    foreach (RIM.rFile rf in r.File_Table.Where(k => k.TypeID == 2044))
                    {
                        GFF g = new GFF(rf.File_Data);

                        int temp = Randomize.Rng.Next(0, 231);

                        bool broken_satisfied = !((Properties.Settings.Default.RandomizePlaceModels & 4) > 0) || !Globals.BROKEN_PLACE.Contains(temp);//Always Satisfied if Broken omission disbaled
                        bool large_satisfied = !((Properties.Settings.Default.RandomizePlaceModels & 2) > 0) || !Globals.LARGE_PLACE.Contains(temp);//Always satisifed if Large omission disabled

                        while (!(broken_satisfied && large_satisfied))
                        {
                            temp = Randomize.Rng.Next(0, 231);
                            broken_satisfied = !((Properties.Settings.Default.RandomizePlaceModels & 4) > 0) || !Globals.BROKEN_PLACE.Contains(temp);//Always Satisfied if Broken omission disbaled
                            large_satisfied = !((Properties.Settings.Default.RandomizePlaceModels & 2) > 0) || !Globals.LARGE_PLACE.Contains(temp);//Always satisifed if Large omission disabled

                        }

                        g.Field_Array.Where(k => k.Label == "Appearance").FirstOrDefault().Field_Data = temp;
                        g.Field_Array.Where(k => k.Label == "Appearance").FirstOrDefault().DataOrDataOffset = temp;

                        MemoryStream ms = new MemoryStream();

                        kWriter.Write(g, ms);

                        rf.File_Data = ms.ToArray();
                    }
                }

                //Characters
                if ((Properties.Settings.Default.RandomizeCharModels & 1) > 0)
                {
                    foreach (RIM.rFile rf in r.File_Table.Where(k => k.TypeID == 2027))
                    {
                        GFF g = new GFF(rf.File_Data);

                        int temp = Randomize.Rng.Next(0, 508);

                        bool broken_satisfied = !((Properties.Settings.Default.RandomizeCharModels & 4) > 0) || !Globals.BROKEN_CHARS.Contains(temp);//Always Satisfied if Broken omission disbaled
                        bool large_satisfied = !((Properties.Settings.Default.RandomizeCharModels & 2) > 0) || !Globals.LARGE_CHARS.Contains(temp);//Always satisifed if Large omission disabled

                        while(!(broken_satisfied && large_satisfied))
                        {
                            temp = Randomize.Rng.Next(0, 508);
                            broken_satisfied = !((Properties.Settings.Default.RandomizeCharModels & 4) > 0) || !Globals.BROKEN_CHARS.Contains(temp);//Always Satisfied if Broken omission disbaled
                            large_satisfied = !((Properties.Settings.Default.RandomizeCharModels & 2) > 0) || !Globals.LARGE_CHARS.Contains(temp);//Always satisifed if Large omission disabled
                        }

                        g.Field_Array.Where(k => k.Label == "Appearance_Type").FirstOrDefault().Field_Data = temp;
                        g.Field_Array.Where(k => k.Label == "Appearance_Type").FirstOrDefault().DataOrDataOffset = temp;

                        MemoryStream ms = new MemoryStream();

                        kWriter.Write(g, ms);

                        rf.File_Data = ms.ToArray();
                    }
                }

                kWriter.Write(r, fi.OpenWrite());
            }
        }
    }
}