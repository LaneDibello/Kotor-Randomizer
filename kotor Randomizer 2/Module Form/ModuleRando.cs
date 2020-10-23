using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using KotOR_IO;

namespace kotor_Randomizer_2
{
    public partial class ModuleForm
    {
        //Populates and shuffles the the modules flagged to be randomized. Returns true if override files should be added.
        public static void Module_rando(Globals.KPaths paths)
        {
            //Set up the bound module collection if it hasn't been already
            if (!Properties.Settings.Default.ModulesInitialized)
            {
                Globals.BoundModules.Clear();
                foreach (string s in Globals.MODULES)
                {
                    Globals.BoundModules.Add(new Globals.Mod_Entry(s, true));
                }
                Properties.Settings.Default.ModulesInitialized = true;
            }

            //if (!Properties.Settings.Default.ModulePresetSelected)
            //{
            //    //Figure something out here
            //}





            //Split the Bound modules into their respective list
            List<string> Shuffled_Mods = Globals.BoundModules.Where(x => !x.ommitted).Select(x => x.name).ToList();
            Randomize.FisherYatesShuffle(Shuffled_Mods);

            // todo: create a readonly constant for paths.Override + "modulesave.2da"
            if (Properties.Settings.Default.AddOverideFiles.Count > 0)
            {
                switch (Properties.Settings.Default.ModuleSaveStatus)
                {
                    case 0: // 0000
                        File.WriteAllBytes(paths.Override + "modulesave.2da", Properties.Resources.NODELETE_modulesave);
                        break;
                    default:
                    case 1: // 0001
                        //This is kotor's default configuration
                        break;
                    case 2: // 0010
                        File.WriteAllBytes(paths.Override + "modulesave.2da", Properties.Resources.NODELETE_MGINCLUDED_modulesave);
                        break;
                    case 3: // 0011
                        File.WriteAllBytes(paths.Override + "modulesave.2da", Properties.Resources.MGINCLUDED_modulesave);
                        break;
                    case 6: // 0110
                        File.WriteAllBytes(paths.Override + "modulesave.2da", Properties.Resources.NODELETE_ALLINCLUDED_modulesave);
                        break;
                    case 7: // 0111
                        File.WriteAllBytes(paths.Override + "modulesave.2da", Properties.Resources.ALLINCLUDED_modulesave);
                        break;
                }

                if (Properties.Settings.Default.AddOverideFiles.Contains("k_ren_visionland.ncs"))
                {
                    File.WriteAllBytes(paths.Override + "k_ren_visionland.ncs", Properties.Resources.k_ren_visionland);
                }

                if (Properties.Settings.Default.AddOverideFiles.Contains("k_pebn_galaxy.ncs"))
                {
                    File.WriteAllBytes(paths.Override + "k_pebn_galaxy.ncs", Properties.Resources.k_pebn_galaxy);
                }
            }

            int k = 0;
            foreach (string M in Globals.BoundModules.Where(x => !x.ommitted).Select(x => x.name))
            {
                File.Copy(paths.get_backup(paths.modules) + M + ".rim", paths.modules + Shuffled_Mods[k] + ".rim", true);
                File.Copy(paths.get_backup(paths.modules) + M + "_s.rim", paths.modules + Shuffled_Mods[k] + "_s.rim", true);
                File.Copy(paths.get_backup(paths.lips) + M + "_loc.mod", paths.lips + Shuffled_Mods[k] + "_loc.mod", true);
                k++;
            }

            foreach (string M in Globals.BoundModules.Where(x => x.ommitted).Select(x => x.name))
            {
                File.Copy(paths.get_backup(paths.modules) + M + ".rim", paths.modules + M + ".rim", true);
                File.Copy(paths.get_backup(paths.modules) + M + "_s.rim", paths.modules + M + "_s.rim", true);
                File.Copy(paths.get_backup(paths.lips) + M + "_loc.mod", paths.lips + M + "_loc.mod", true);
            }

            foreach (string L in Globals.lipXtras)
            {
                File.Copy(paths.get_backup(paths.lips) + L, paths.lips + L, true);
            }

            //Fix warp coordinates
            if (Properties.Settings.Default.FixWarpCoords)
            {
                DirectoryInfo di = new DirectoryInfo(paths.modules);
                foreach (FileInfo fi in di.GetFiles())  // todo: can we query for the appropriate files before going into a loop?
                {
                    RIM r = KReader.ReadRIM(fi.OpenRead());

                    if (fi.Name[fi.Name.Length - 5] == 's')
                    {
                        continue;
                    }

                    bool edit_flag = false;

                    // 2014 refers to the IFO type code within the resource tables "Res_Types" and "TypeCodes". It is a GFF type
                    GFF g = new GFF(r.File_Table.Where(x => x.TypeID == 2014).FirstOrDefault().File_Data);  // todo: fix usage of undefined constants

                    // todo: update switch cases with readonly constants for both the case and the XYZ tuple
                    // todo: separate the memory stream write into a separate method that can be called at the end of each case
                    switch((g.Field_Array.Where(x => x.Label == "Mod_Entry_Area").FirstOrDefault().Field_Data as GFF.CResRef).Text)
                    {
                        case "m04aa":   // Undercity
                            (g.Field_Array.Where(x => x.Label == "Mod_Entry_X").FirstOrDefault().DataOrDataOffset) = BitConverter.ToInt32(BitConverter.GetBytes(183.5f), 0);
                            (g.Field_Array.Where(x => x.Label == "Mod_Entry_Y").FirstOrDefault().DataOrDataOffset) = BitConverter.ToInt32(BitConverter.GetBytes(167.4f), 0);
                            (g.Field_Array.Where(x => x.Label == "Mod_Entry_Z").FirstOrDefault().DataOrDataOffset) = BitConverter.ToInt32(BitConverter.GetBytes(1.5f), 0);
                            edit_flag = true;
                            break;
                        case "m38aa":   // Tomb of Marka Ragnos
                            (g.Field_Array.Where(x => x.Label == "Mod_Entry_X").FirstOrDefault().DataOrDataOffset) = BitConverter.ToInt32(BitConverter.GetBytes(15.8f), 0);
                            (g.Field_Array.Where(x => x.Label == "Mod_Entry_Y").FirstOrDefault().DataOrDataOffset) = BitConverter.ToInt32(BitConverter.GetBytes(55.6f), 0);
                            (g.Field_Array.Where(x => x.Label == "Mod_Entry_Z").FirstOrDefault().DataOrDataOffset) = BitConverter.ToInt32(BitConverter.GetBytes(0.75f), 0);
                            edit_flag = true;
                            break;
                        case "m40ac":   // Leviathan Hangar
                            (g.Field_Array.Where(x => x.Label == "Mod_Entry_X").FirstOrDefault().DataOrDataOffset) = BitConverter.ToInt32(BitConverter.GetBytes(12.5f), 0);
                            (g.Field_Array.Where(x => x.Label == "Mod_Entry_Y").FirstOrDefault().DataOrDataOffset) = BitConverter.ToInt32(BitConverter.GetBytes(155.2f), 0);
                            (g.Field_Array.Where(x => x.Label == "Mod_Entry_Z").FirstOrDefault().DataOrDataOffset) = BitConverter.ToInt32(BitConverter.GetBytes(3.0f), 0);
                            edit_flag = true;
                            break;
                        case "m26aa":   // Ahto West
                            (g.Field_Array.Where(x => x.Label == "Mod_Entry_X").FirstOrDefault().DataOrDataOffset) = BitConverter.ToInt32(BitConverter.GetBytes(5.7f), 0);
                            (g.Field_Array.Where(x => x.Label == "Mod_Entry_Y").FirstOrDefault().DataOrDataOffset) = BitConverter.ToInt32(BitConverter.GetBytes(-10.7f), 0);
                            (g.Field_Array.Where(x => x.Label == "Mod_Entry_Z").FirstOrDefault().DataOrDataOffset) = BitConverter.ToInt32(BitConverter.GetBytes(59.2f), 0);
                            edit_flag = true;
                            break;
                        case "m27aa":   // Manaan Sith Base
                            (g.Field_Array.Where(x => x.Label == "Mod_Entry_X").FirstOrDefault().DataOrDataOffset) = BitConverter.ToInt32(BitConverter.GetBytes(112.8f), 0);
                            (g.Field_Array.Where(x => x.Label == "Mod_Entry_Y").FirstOrDefault().DataOrDataOffset) = BitConverter.ToInt32(BitConverter.GetBytes(2.4f), 0);
                            (g.Field_Array.Where(x => x.Label == "Mod_Entry_Z").FirstOrDefault().DataOrDataOffset) = BitConverter.ToInt32(BitConverter.GetBytes(0f), 0);
                            edit_flag = true;
                            break;
                        case "m43aa":   // Rakatan Settlement
                            (g.Field_Array.Where(x => x.Label == "Mod_Entry_X").FirstOrDefault().DataOrDataOffset) = BitConverter.ToInt32(BitConverter.GetBytes(202.2f), 0);
                            (g.Field_Array.Where(x => x.Label == "Mod_Entry_Y").FirstOrDefault().DataOrDataOffset) = BitConverter.ToInt32(BitConverter.GetBytes(31.5f), 0);
                            (g.Field_Array.Where(x => x.Label == "Mod_Entry_Z").FirstOrDefault().DataOrDataOffset) = BitConverter.ToInt32(BitConverter.GetBytes(40.7f), 0);
                            edit_flag = true;
                            break;
                        case "m44aa":   // Temple Main Floor
                            (g.Field_Array.Where(x => x.Label == "Mod_Entry_X").FirstOrDefault().DataOrDataOffset) = BitConverter.ToInt32(BitConverter.GetBytes(95.3f), 0);
                            (g.Field_Array.Where(x => x.Label == "Mod_Entry_Y").FirstOrDefault().DataOrDataOffset) = BitConverter.ToInt32(BitConverter.GetBytes(42.0f), 0);
                            (g.Field_Array.Where(x => x.Label == "Mod_Entry_Z").FirstOrDefault().DataOrDataOffset) = BitConverter.ToInt32(BitConverter.GetBytes(0.44f), 0);
                            edit_flag = true;
                            break;
                    }
                    if (edit_flag)
                    {
                        MemoryStream ms = new MemoryStream();

                        kWriter.Write(g, ms);

                        r.File_Table.Where(x => x.TypeID == 2014).FirstOrDefault().File_Data = ms.ToArray();

                        kWriter.Write(r, fi.OpenWrite());
                    }

                }
            }

            //Fixed Rakata riddle Man in Mind Prison
            if (Properties.Settings.Default.FixMindPrison)
            {
                DirectoryInfo di = new DirectoryInfo(paths.modules);
                foreach (FileInfo fi in di.GetFiles())
                {
                    if (fi.Name[fi.Name.Length - 5] != 's')
                    {
                        continue;
                    }

                    RIM r = KReader.ReadRIM(fi.OpenRead());
                    if (r.File_Table.Where(x => x.Label == "g_brakatan003").Any())
                    {
                        bool offadjust = false;
                        foreach (RIM.rFile rf in r.File_Table)
                        {
                            if (rf.Label == "g_brakatan003")
                            {
                                rf.File_Data = Properties.Resources.g_brakatan003;
                                rf.DataSize += 192;
                                offadjust = true;
                                continue;
                            }
                            if (offadjust)
                            {
                                rf.DataOffset += 192;
                            }

                        }

                        kWriter.Write(r, fi.OpenWrite());
                    }
                }

            }

        }
    }
}