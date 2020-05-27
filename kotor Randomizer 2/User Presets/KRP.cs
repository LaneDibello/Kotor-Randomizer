using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace kotor_Randomizer_2
{
    public static class KRP
    {
        #region private properties
        private static string Version = "KRP V2.0";
        private static int CategoryCount = 8;
        #endregion

        #region public methods
        public static void ReadKRP(Stream s) //Need to Finish
        {
            BinaryReader br = new BinaryReader(s);

            if (new string(br.ReadChars(8)) == "KRP V2.0") //Version Check
            {
                //Category Booleans
                Properties.Settings.Default.module_rando_active = br.ReadBoolean();
                Properties.Settings.Default.item_rando_active = br.ReadBoolean();
                Properties.Settings.Default.sound_rando_active = br.ReadBoolean();
                Properties.Settings.Default.model_rando_active = br.ReadBoolean();
                Properties.Settings.Default.texture_rando_active = br.ReadBoolean();
                Properties.Settings.Default.twoda_rando_active = br.ReadBoolean();
                Properties.Settings.Default.text_rando_active = br.ReadBoolean();
                Properties.Settings.Default.other_rando_active = br.ReadBoolean();

                //Categories
                //Modules
                if (Properties.Settings.Default.module_rando_active)
                {
                    //Check that Module data is present
                    if (new string(br.ReadChars(4)) != "MODU") { throw new IOException("Module Randomization is Active, but no such preset data is found. Is the file corrupt?"); }

                    //initialize the BoundModules global if it hasn't been already
                    if (!Properties.Settings.Default.ModulesInitialized)
                    {
                        foreach (string st in Globals.MODULES)
                        {
                            Globals.BoundModules.Add(new Globals.Mod_Entry(st, true));
                        }
                        Properties.Settings.Default.ModulesInitialized = true;
                    }

                    //Pull the list of omitted modules from the save
                    List<string> omit_mods = new List<string>();
                    while (br.PeekChar() != '\n')
                    {
                        StringBuilder sb = new StringBuilder();

                        while(br.PeekChar() != '\0')
                        {
                            sb.Append(br.ReadChar());
                        }

                        omit_mods.Add(sb.ToString());
                    }

                    //Load the omitted preset into BoundModules
                    for (int i = 0; i < Globals.BoundModules.Count; i++)
                    {
                        Globals.BoundModules[i] = new Globals.Mod_Entry(Globals.BoundModules[i].name, false);

                        if (omit_mods.Contains(Globals.BoundModules[i].name))
                        {
                            Globals.BoundModules[i] = new Globals.Mod_Entry(Globals.BoundModules[i].name, true);
                        }
                    }


                }
            }
        }

        public static void WriteKRP(Stream s)
        {
            BinaryWriter bw = new BinaryWriter(s);

            //Version
            bw.Write(Version.ToCharArray());

            //Category Booleans
            bw.Write(Properties.Settings.Default.module_rando_active);
            bw.Write(Properties.Settings.Default.item_rando_active);
            bw.Write(Properties.Settings.Default.sound_rando_active);
            bw.Write(Properties.Settings.Default.model_rando_active);
            bw.Write(Properties.Settings.Default.texture_rando_active);
            bw.Write(Properties.Settings.Default.twoda_rando_active);
            bw.Write(Properties.Settings.Default.text_rando_active);
            bw.Write(Properties.Settings.Default.other_rando_active);

            //Categories
            //Modules
            if (Properties.Settings.Default.module_rando_active)
            {
                bw.Write("MODU".ToCharArray());

                //FIX LATER - Possibility that BoundModules is empty if Module Form never opened.
                foreach (Globals.Mod_Entry me in Globals.BoundModules.Where(x => x.ommitted))
                {
                    bw.Write(me.name.ToCharArray());
                    bw.Write('\0');
                }
                bw.Write('\n');

                bw.Write((Properties.Settings.Default.ModuleSaveStatus & 1) > 0); //Mod Delete
                bw.Write((Properties.Settings.Default.ModuleSaveStatus & 2) > 0); //Mini-Game Save
                bw.Write((Properties.Settings.Default.ModuleSaveStatus & 4) > 0); //All Save
                bw.Write(Properties.Settings.Default.AddOverideFiles.Contains("k_ren_visionland.ncs")); //Fixed Dream
                bw.Write(Properties.Settings.Default.AddOverideFiles.Contains("k_pebn_galaxy.ncs")); //Unlocked Galaxy Map
                bw.Write(Properties.Settings.Default.AddOverideFiles.Contains("MISSIONFILENAME")); //Mission Fix *NOT YET IMPLEMENTED*

            }
            //Items
            if (Properties.Settings.Default.item_rando_active)
            {
                bw.Write("ITEM".ToCharArray());

                bw.Write(Properties.Settings.Default.RandomizeArmor);
                bw.Write(Properties.Settings.Default.RandomizeStims);
                bw.Write(Properties.Settings.Default.RandomizeBelts);
                bw.Write(Properties.Settings.Default.RandomizeVarious);
                bw.Write(Properties.Settings.Default.RandomizeHides);
                bw.Write(Properties.Settings.Default.RandomizeArmbands);
                bw.Write(Properties.Settings.Default.RandomizeDroid);
                bw.Write(Properties.Settings.Default.RandomizeGloves);
                bw.Write(Properties.Settings.Default.RandomizeImplants);
                bw.Write(Properties.Settings.Default.RandomizeMask);
                bw.Write(Properties.Settings.Default.RandomizePaz);
                bw.Write(Properties.Settings.Default.RandomizeMines);
                bw.Write(Properties.Settings.Default.RandomizeUpgrade);
                bw.Write(Properties.Settings.Default.RandomizeBlasters);
                bw.Write(Properties.Settings.Default.RandomizeCreature);
                bw.Write(Properties.Settings.Default.RandomizeLightsabers);
                bw.Write(Properties.Settings.Default.RandomizeGrenades);
                bw.Write(Properties.Settings.Default.RandomizeMelee);

                foreach (string st in Globals.OmitItems)
                {
                    bw.Write(st.ToCharArray());
                    bw.Write('\0');
                }
                bw.Write('\n');
            }
            //Sounds
            if (Properties.Settings.Default.sound_rando_active)
            {
                bw.Write("SOUN".ToCharArray());

                bw.Write(Properties.Settings.Default.RandomizeAreaMusic);
                bw.Write(Properties.Settings.Default.RandomizeBattleMusic);
                bw.Write(Properties.Settings.Default.RandomizeAmbientNoise);
                bw.Write(Properties.Settings.Default.RandomizeCutsceneNoise);
                bw.Write(Properties.Settings.Default.RandomizeNpcSounds);
                bw.Write(Properties.Settings.Default.RandomizePartySounds);

                bw.Write(Properties.Settings.Default.MixNpcAndPartySounds);

            }
            //Models
            if (Properties.Settings.Default.model_rando_active)
            {
                bw.Write("MODE".ToCharArray());

                bw.Write(Properties.Settings.Default.RandomizeCharModels);
                bw.Write(Properties.Settings.Default.RandomizePlaceModels);
                bw.Write(Properties.Settings.Default.RandomizeDoorModels);
            }
            //Textures
            if (Properties.Settings.Default.texture_rando_active)
            {
                bw.Write("TEXU".ToCharArray());

                bw.Write(Properties.Settings.Default.TextureRandomizeCubeMaps);
                bw.Write(Properties.Settings.Default.TextureRandomizeCreatures);
                bw.Write(Properties.Settings.Default.TextureRandomizeEffects);
                bw.Write(Properties.Settings.Default.TextureRandomizeItems);
                bw.Write(Properties.Settings.Default.TextureRandomizePlanetary);
                bw.Write(Properties.Settings.Default.TextureRandomizeNPC);
                bw.Write(Properties.Settings.Default.TextureRandomizePlayHeads);
                bw.Write(Properties.Settings.Default.TextureRandomizePlayBodies);
                bw.Write(Properties.Settings.Default.TextureRandomizePlaceables);
                bw.Write(Properties.Settings.Default.TextureRandomizeParty);
                bw.Write(Properties.Settings.Default.TextureRandomizeStunt);
                bw.Write(Properties.Settings.Default.TextureRandomizeVehicles);
                bw.Write(Properties.Settings.Default.TextureRandomizeWeapons);
                bw.Write(Properties.Settings.Default.TextureRandomizeOther);

                bw.Write(Properties.Settings.Default.TexturePack);

            }
            //2das
            if (Properties.Settings.Default.twoda_rando_active)
            {
                bw.Write("TWDA".ToCharArray());
                //TBD

            }
            //Text
            if (Properties.Settings.Default.text_rando_active)
            {
                bw.Write("TEXT".ToCharArray());
                //TBD

            }
            //Other
            if (Properties.Settings.Default.other_rando_active)
            {
                bw.Write("OTHR".ToCharArray());

                bw.Write(Properties.Settings.Default.NameGenRando);
                if (Properties.Settings.Default.NameGenRando)
                {
                    foreach (string st in Properties.Settings.Default.FirstnamesM)
                    {
                        bw.Write(st.ToCharArray());
                        bw.Write('\0');
                    }
                    bw.Write('\n');
                    foreach (string st in Properties.Settings.Default.FirstnamesF)
                    {
                        bw.Write(st.ToCharArray());
                        bw.Write('\0');
                    }
                    bw.Write('\n');
                    foreach (string st in Properties.Settings.Default.Lastnames)
                    {
                        bw.Write(st.ToCharArray());
                        bw.Write('\0');
                    }
                    bw.Write('\n');
                }

                bw.Write(Properties.Settings.Default.PolymorphMode);


            }
        }
        #endregion
    }
}
