using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using KotOR_IO;

namespace kotor_Randomizer_2
{
    public static class ModuleRando
    {
        private const string AREA_MIND_PRISON = "g_brakatan003";

        // Populates and shuffles the the modules flagged to be randomized. Returns true if override files should be added.
        public static void Module_rando(KPaths paths)
        {
            // Set up the bound module collection if it hasn't been already.
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

            // Split the Bound modules into their respective list.
            List<string> Shuffled_Mods = Globals.BoundModules.Where(x => !x.Omitted).Select(x => x.Name).ToList();
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
            foreach (string M in Globals.BoundModules.Where(x => !x.Omitted).Select(x => x.Name))
            {
                File.Copy(paths.modules_backup + M + ".rim", paths.modules + Shuffled_Mods[k] + ".rim", true);
                File.Copy(paths.modules_backup + M + "_s.rim", paths.modules + Shuffled_Mods[k] + "_s.rim", true);
                File.Copy(paths.lips_backup + M + "_loc.mod", paths.lips + Shuffled_Mods[k] + "_loc.mod", true);
                k++;
            }

            foreach (string M in Globals.BoundModules.Where(x => x.Omitted).Select(x => x.Name))
            {
                File.Copy(paths.modules_backup + M + ".rim", paths.modules + M + ".rim", true);
                File.Copy(paths.modules_backup + M + "_s.rim", paths.modules + M + "_s.rim", true);
                File.Copy(paths.lips_backup + M + "_loc.mod", paths.lips + M + "_loc.mod", true);
            }

            foreach (string L in Globals.lipXtras)
            {
                File.Copy(paths.lips_backup + L, paths.lips + L, true);
            }

            // Fix warp coordinates.
            if (Properties.Settings.Default.FixWarpCoords)
            {
                foreach (FileInfo fi in paths.FilesInModules)  // todo: can we query for the appropriate files before going into a loop?
                {
                    RIM r = new RIM(fi.FullName);

                    if (fi.Name[fi.Name.Length - 5] == 's')
                    {
                        continue;
                    }

                    bool edit_flag = false;

                    // 2014 refers to the IFO type code within the resource tables "Res_Types" and "TypeCodes". It is a GFF type.
                    GFF g = new GFF(r.File_Table.Where(x => x.TypeID == (int)ResourceType.IFO).FirstOrDefault().File_Data);
                    var fieldX = g.Field_Array.Where(x => x.Label == Properties.Resources.ModuleEntryX).FirstOrDefault();
                    var fieldY = g.Field_Array.Where(x => x.Label == Properties.Resources.ModuleEntryY).FirstOrDefault();
                    var fieldZ = g.Field_Array.Where(x => x.Label == Properties.Resources.ModuleEntryZ).FirstOrDefault();

                    // todo: separate the memory stream write into a separate method that can be called at the end of each case
                    switch((g.Field_Array.Where(x => x.Label == Properties.Resources.ModuleEntryArea).FirstOrDefault().Field_Data as GFF.CResRef).Text)
                    {
                        case Globals.AREA_UNDERCITY:    // Undercity
                            fieldX.DataOrDataOffset = Globals.FIXED_COORDINATES[Globals.AREA_UNDERCITY].Item1;
                            fieldY.DataOrDataOffset = Globals.FIXED_COORDINATES[Globals.AREA_UNDERCITY].Item2;
                            fieldZ.DataOrDataOffset = Globals.FIXED_COORDINATES[Globals.AREA_UNDERCITY].Item3;
                            edit_flag = true;
                            break;
                        case Globals.AREA_TOMB_RAGNOS:  // Tomb of Marka Ragnos
                            fieldX.DataOrDataOffset = Globals.FIXED_COORDINATES[Globals.AREA_TOMB_RAGNOS].Item1;
                            fieldY.DataOrDataOffset = Globals.FIXED_COORDINATES[Globals.AREA_TOMB_RAGNOS].Item2;
                            fieldZ.DataOrDataOffset = Globals.FIXED_COORDINATES[Globals.AREA_TOMB_RAGNOS].Item3;
                            edit_flag = true;
                            break;
                        case Globals.AREA_LEVI_HANGAR:  // Leviathan Hangar
                            fieldX.DataOrDataOffset = Globals.FIXED_COORDINATES[Globals.AREA_LEVI_HANGAR].Item1;
                            fieldY.DataOrDataOffset = Globals.FIXED_COORDINATES[Globals.AREA_LEVI_HANGAR].Item2;
                            fieldZ.DataOrDataOffset = Globals.FIXED_COORDINATES[Globals.AREA_LEVI_HANGAR].Item3;
                            edit_flag = true;
                            break;
                        case Globals.AREA_AHTO_WEST:    // Ahto West
                            fieldX.DataOrDataOffset = Globals.FIXED_COORDINATES[Globals.AREA_AHTO_WEST].Item1;
                            fieldY.DataOrDataOffset = Globals.FIXED_COORDINATES[Globals.AREA_AHTO_WEST].Item2;
                            fieldZ.DataOrDataOffset = Globals.FIXED_COORDINATES[Globals.AREA_AHTO_WEST].Item3;
                            edit_flag = true;
                            break;
                        case Globals.AREA_MANAAN_SITH:  // Manaan Sith Base
                            fieldX.DataOrDataOffset = Globals.FIXED_COORDINATES[Globals.AREA_MANAAN_SITH].Item1;
                            fieldY.DataOrDataOffset = Globals.FIXED_COORDINATES[Globals.AREA_MANAAN_SITH].Item2;
                            fieldZ.DataOrDataOffset = Globals.FIXED_COORDINATES[Globals.AREA_MANAAN_SITH].Item3;
                            edit_flag = true;
                            break;
                        case Globals.AREA_RAKA_SETTLE:  // Rakatan Settlement
                            fieldX.DataOrDataOffset = Globals.FIXED_COORDINATES[Globals.AREA_RAKA_SETTLE].Item1;
                            fieldY.DataOrDataOffset = Globals.FIXED_COORDINATES[Globals.AREA_RAKA_SETTLE].Item2;
                            fieldZ.DataOrDataOffset = Globals.FIXED_COORDINATES[Globals.AREA_RAKA_SETTLE].Item3;
                            edit_flag = true;
                            break;
                        case Globals.AREA_TEMPLE_MAIN:  // Temple Main Floor
                            fieldX.DataOrDataOffset = Globals.FIXED_COORDINATES[Globals.AREA_TEMPLE_MAIN].Item1;
                            fieldY.DataOrDataOffset = Globals.FIXED_COORDINATES[Globals.AREA_TEMPLE_MAIN].Item2;
                            fieldZ.DataOrDataOffset = Globals.FIXED_COORDINATES[Globals.AREA_TEMPLE_MAIN].Item3;
                            edit_flag = true;
                            break;
                    }

                    if (edit_flag)
                    {
                        MemoryStream ms = new MemoryStream();
                        r.File_Table.Where(x => x.TypeID == (int)ResourceType.IFO).FirstOrDefault().File_Data = g.ToRawData();
                        r.WriteToFile(fi.FullName);
                    }
                }
            }

            // Fixed Rakata riddle Man in Mind Prison.
            if (Properties.Settings.Default.FixMindPrison)
            {
                foreach (FileInfo fi in paths.FilesInModules)
                {
                    if (fi.Name[fi.Name.Length - 5] != 's')
                    {
                        continue;
                    }

                    RIM r = new RIM(fi.FullName);
                    if (r.File_Table.Where(x => x.Label == AREA_MIND_PRISON).Any())
                    {
                        bool offadjust = false;
                        foreach (RIM.rFile rf in r.File_Table)
                        {
                            if (rf.Label == AREA_MIND_PRISON)
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

                        r.WriteToFile(fi.FullName);
                    }
                }
            }
        }
    }
}
