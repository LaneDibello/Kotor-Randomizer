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
        public static bool ReadKRP(Stream s)
        {
            using (BinaryReader br = new BinaryReader(s))
            {
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
                    #region Modules
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

                            while (br.PeekChar() != '\0')
                            {
                                sb.Append(br.ReadChar());
                            }
                            br.ReadChar();

                            omit_mods.Add(sb.ToString());
                        }
                        br.ReadChar();

                        //Load the omitted preset into BoundModules
                        for (int i = 0; i < Globals.BoundModules.Count; i++)
                        {
                            Globals.BoundModules[i] = new Globals.Mod_Entry(Globals.BoundModules[i].Name, false);

                            if (omit_mods.Contains(Globals.BoundModules[i].Name))
                            {
                                Globals.BoundModules[i] = new Globals.Mod_Entry(Globals.BoundModules[i].Name, true);
                            }
                        }
                        Properties.Settings.Default.ModulesInitialized = true;

                        //Load bool settings
                        Properties.Settings.Default.ModuleSaveStatus = 0;
                        if (br.ReadBoolean()) { Properties.Settings.Default.ModuleSaveStatus ^= 1; }
                        if (br.ReadBoolean()) { Properties.Settings.Default.ModuleSaveStatus ^= 2; }
                        if (br.ReadBoolean()) { Properties.Settings.Default.ModuleSaveStatus ^= 4; }
                        if (br.ReadBoolean()) { Properties.Settings.Default.AddOverideFiles.Add("k_ren_visionland.ncs"); }
                        if (br.ReadBoolean()) { Properties.Settings.Default.AddOverideFiles.Add("k_pebn_galaxy.ncs"); }
                        Properties.Settings.Default.FixWarpCoords = br.ReadBoolean();
                        Properties.Settings.Default.FixMindPrison = br.ReadBoolean();
                    }
                    #endregion
                    #region Items
                    if (Properties.Settings.Default.item_rando_active)
                    {
                        //Check that Item data is present
                        if (new string(br.ReadChars(4)) != "ITEM") { throw new IOException("Item Randomization is Active, but no such preset data is found. Is the file corrupt?"); }

                        //Load in randolevels
                        Properties.Settings.Default.RandomizeArmor = br.ReadInt32();
                        Properties.Settings.Default.RandomizeStims = br.ReadInt32();
                        Properties.Settings.Default.RandomizeBelts = br.ReadInt32();
                        Properties.Settings.Default.RandomizeVarious = br.ReadInt32();
                        Properties.Settings.Default.RandomizeHides = br.ReadInt32();
                        Properties.Settings.Default.RandomizeArmbands = br.ReadInt32();
                        Properties.Settings.Default.RandomizeDroid = br.ReadInt32();
                        Properties.Settings.Default.RandomizeGloves = br.ReadInt32();
                        Properties.Settings.Default.RandomizeImplants = br.ReadInt32();
                        Properties.Settings.Default.RandomizeMask = br.ReadInt32();
                        Properties.Settings.Default.RandomizePaz = br.ReadInt32();
                        Properties.Settings.Default.RandomizeMines = br.ReadInt32();
                        Properties.Settings.Default.RandomizeUpgrade = br.ReadInt32();
                        Properties.Settings.Default.RandomizeBlasters = br.ReadInt32();
                        Properties.Settings.Default.RandomizeCreature = br.ReadInt32();
                        Properties.Settings.Default.RandomizeLightsabers = br.ReadInt32();
                        Properties.Settings.Default.RandomizeGrenades = br.ReadInt32();
                        Properties.Settings.Default.RandomizeMelee = br.ReadInt32();

                        //Load Omit Items
                        Globals.OmitItems.Clear();
                        while (br.PeekChar() != '\n')
                        {
                            StringBuilder sb = new StringBuilder();

                            while (br.PeekChar() != '\0')
                            {
                                sb.Append(br.ReadChar());
                            }
                            br.ReadChar();

                            Globals.OmitItems.Add(sb.ToString());
                        }
                        br.ReadChar();
                    }
                    #endregion
                    #region Sounds
                    if (Properties.Settings.Default.sound_rando_active)
                    {
                        //Check that Sound data is present
                        if (new string(br.ReadChars(4)) != "SOUN") { throw new IOException("Sound Randomization is Active, but no such preset data is found. Is the file corrupt?"); }

                        //Rando Levels
                        Properties.Settings.Default.RandomizeAreaMusic = br.ReadInt32();
                        Properties.Settings.Default.RandomizeBattleMusic = br.ReadInt32();
                        Properties.Settings.Default.RandomizeAmbientNoise = br.ReadInt32();
                        Properties.Settings.Default.RandomizeCutsceneNoise = br.ReadInt32();
                        Properties.Settings.Default.RandomizeNpcSounds = br.ReadInt32();
                        Properties.Settings.Default.RandomizePartySounds = br.ReadInt32();

                        //Bools
                        Properties.Settings.Default.MixNpcAndPartySounds = br.ReadBoolean();
                    }
                    #endregion
                    #region Models
                    if (Properties.Settings.Default.model_rando_active)
                    {
                        //Check that Model data is present
                        if (new string(br.ReadChars(4)) != "MODE") { throw new IOException("Model Randomization is Active, but no such preset data is found. Is the file corrupt?"); }

                        //Settings
                        Properties.Settings.Default.RandomizeCharModels = br.ReadInt32();
                        Properties.Settings.Default.RandomizePlaceModels = br.ReadInt32();
                        Properties.Settings.Default.RandomizeDoorModels = br.ReadInt32();

                    }
                    #endregion
                    #region Textures
                    if (Properties.Settings.Default.texture_rando_active)
                    {
                        //Check that Texture data is present
                        if (new string(br.ReadChars(4)) != "TEXU") { throw new IOException("Texture Randomization is Active, but no such preset data is found. Is the file corrupt?"); }

                        //Settings
                        Properties.Settings.Default.TextureRandomizeCubeMaps = br.ReadInt32();
                        Properties.Settings.Default.TextureRandomizeCreatures = br.ReadInt32();
                        Properties.Settings.Default.TextureRandomizeEffects = br.ReadInt32();
                        Properties.Settings.Default.TextureRandomizeItems = br.ReadInt32();
                        Properties.Settings.Default.TextureRandomizePlanetary = br.ReadInt32();
                        Properties.Settings.Default.TextureRandomizeNPC = br.ReadInt32();
                        Properties.Settings.Default.TextureRandomizePlayHeads = br.ReadInt32();
                        Properties.Settings.Default.TextureRandomizePlayBodies = br.ReadInt32();
                        Properties.Settings.Default.TextureRandomizePlaceables = br.ReadInt32();
                        Properties.Settings.Default.TextureRandomizeParty = br.ReadInt32();
                        Properties.Settings.Default.TextureRandomizeStunt = br.ReadInt32();
                        Properties.Settings.Default.TextureRandomizeVehicles = br.ReadInt32();
                        Properties.Settings.Default.TextureRandomizeWeapons = br.ReadInt32();
                        Properties.Settings.Default.TextureRandomizeOther = br.ReadInt32();

                        //Texture Pack
                        Properties.Settings.Default.TexturePack = br.ReadInt32();
                    }
                    #endregion
                    #region 2DAs
                    if (Properties.Settings.Default.twoda_rando_active)
                    {
                        //Check that 2DA data is present
                        if (new string(br.ReadChars(4)) != "TWDA") { throw new IOException("2-Dimensional Array Randomization is Active, but no such preset data is found. Is the file corrupt?"); }

                        Globals.Selected2DAs.Clear();
                        while (br.PeekChar() != '\x3')
                        {
                            StringBuilder kb = new StringBuilder();
                            while (br.PeekChar() != '\r')
                            {
                                kb.Append(br.ReadChar());
                            }
                            br.ReadChar();
                            string key = kb.ToString();
                            Globals.Selected2DAs.Add(key, new List<string>());
                            while (br.PeekChar() != '\n')
                            {
                                StringBuilder vb = new StringBuilder();
                                while (br.PeekChar() != '\0')
                                {
                                    vb.Append(br.ReadChar());
                                }
                                br.ReadChar();
                                Globals.Selected2DAs[key].Add(vb.ToString());
                            }
                            br.ReadChar();
                        }
                        br.ReadChar();
                    }
                    #endregion
                    #region Text
                    if (Properties.Settings.Default.text_rando_active)
                    {
                        //Check that Text data is present
                        if (new string(br.ReadChars(4)) != "TEXT") { throw new IOException("Text Randomization is Active, but no such preset data is found. Is the file corrupt?"); }

                        //TBD
                    }
                    #endregion
                    #region Other
                    if (Properties.Settings.Default.other_rando_active)
                    {
                        //Check that Other data is present
                        if (new string(br.ReadChars(4)) != "OTHR") { throw new IOException("Other Randomization is Active, but no such preset data is found. Is the file corrupt?"); }

                        //Name Gen
                        Properties.Settings.Default.NameGenRando = br.ReadBoolean();
                        if (Properties.Settings.Default.NameGenRando)
                        {
                            Properties.Settings.Default.FirstnamesM.Clear();
                            while (br.PeekChar() != '\n')
                            {
                                StringBuilder mb = new StringBuilder();
                                while (br.PeekChar() != '\0')
                                {
                                    mb.Append(br.ReadChar());
                                }
                                br.ReadChar();
                                Properties.Settings.Default.FirstnamesM.Add(mb.ToString());
                            }
                            br.ReadChar();

                            Properties.Settings.Default.FirstnamesF.Clear();
                            while (br.PeekChar() != '\n')
                            {
                                StringBuilder mb = new StringBuilder();
                                while (br.PeekChar() != '\0')
                                {
                                    mb.Append(br.ReadChar());
                                }
                                br.ReadChar();
                                Properties.Settings.Default.FirstnamesF.Add(mb.ToString());
                            }
                            br.ReadChar();

                            Properties.Settings.Default.Lastnames.Clear();
                            while (br.PeekChar() != '\n')
                            {
                                StringBuilder mb = new StringBuilder();
                                while (br.PeekChar() != '\0')
                                {
                                    mb.Append(br.ReadChar());
                                }
                                br.ReadChar();
                                Properties.Settings.Default.Lastnames.Add(mb.ToString());
                            }
                            br.ReadChar();
                        }

                        Properties.Settings.Default.PolymorphMode = br.ReadBoolean();
                        Properties.Settings.Default.PazaakDecks = br.ReadBoolean();
                    }
                    #endregion

                    return true;
                }
            }
            return false;
        }

        public static void WriteKRP(Stream s)
        {
            using (BinaryWriter bw = new BinaryWriter(s))
            {
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
                    foreach (Globals.Mod_Entry me in Globals.BoundModules.Where(x => x.Omitted))
                    {
                        bw.Write(me.Name.ToCharArray());
                        bw.Write('\0');
                    }
                    bw.Write('\n');

                    bw.Write((Properties.Settings.Default.ModuleSaveStatus & 1) > 0); //Mod Delete
                    bw.Write((Properties.Settings.Default.ModuleSaveStatus & 2) > 0); //Mini-Game Save
                    bw.Write((Properties.Settings.Default.ModuleSaveStatus & 4) > 0); //All Save
                    bw.Write(Properties.Settings.Default.AddOverideFiles.Contains("k_ren_visionland.ncs")); //Fixed Dream
                    bw.Write(Properties.Settings.Default.AddOverideFiles.Contains("k_pebn_galaxy.ncs")); //Unlocked Galaxy Map
                    bw.Write(Properties.Settings.Default.FixWarpCoords);//Fixed Module Coordinates
                    bw.Write(Properties.Settings.Default.FixMindPrison);//Fixed Rakatan Riddles

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

                    foreach (KeyValuePair<string, List<string>> k in Globals.Selected2DAs)
                    {
                        bw.Write(k.Key.ToCharArray());
                        bw.Write('\r');

                        foreach (string v in k.Value)
                        {
                            bw.Write(v.ToCharArray());
                            bw.Write('\0');
                        }
                        bw.Write('\n');
                    }
                    bw.Write('\x3');

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
                    bw.Write(Properties.Settings.Default.PazaakDecks);
                }
            }
        }
        #endregion
    }
}
