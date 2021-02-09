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
        private const string VERSION = "KRP V2.1";
        //private static int CategoryCount = 8;

        #region public methods
        public static bool ReadKRP(Stream s)
        {
            using (BinaryReader br = new BinaryReader(s))
            {
                string versionText = new string(br.ReadChars(VERSION.Length));


                if (versionText == VERSION)    // Version Check
                {
                    // Category Booleans
                    Properties.Settings.Default.DoRandomization_Module = br.ReadBoolean();
                    Properties.Settings.Default.DoRandomization_Item = br.ReadBoolean();
                    Properties.Settings.Default.DoRandomization_Sound = br.ReadBoolean();
                    Properties.Settings.Default.DoRandomization_Model = br.ReadBoolean();
                    Properties.Settings.Default.DoRandomization_Texture = br.ReadBoolean();
                    Properties.Settings.Default.DoRandomization_TwoDA = br.ReadBoolean();
                    Properties.Settings.Default.DoRandomization_Text = br.ReadBoolean();
                    Properties.Settings.Default.DoRandomization_Other = br.ReadBoolean();

                    // Categories
                    #region Modules
                    if (Properties.Settings.Default.DoRandomization_Module)
                    {
                        // Check that Module data is present
                        if (new string(br.ReadChars(4)) != "MODU") { throw new IOException("Module Randomization is Active, but no such preset data is found. Is the file corrupt?"); }

                        // Initialize the BoundModules global if it hasn't been already
                        if (!Properties.Settings.Default.ModulesInitialized)
                        {
                            foreach (string st in Globals.MODULES)
                            {
                                Globals.BoundModules.Add(new Globals.Mod_Entry(st, true));
                            }
                            Properties.Settings.Default.ModulesInitialized = true;
                        }

                        // Pull the list of omitted modules from the save
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

                        // Load the omitted preset into BoundModules
                        for (int i = 0; i < Globals.BoundModules.Count; i++)
                        {
                            Globals.BoundModules[i] = new Globals.Mod_Entry(Globals.BoundModules[i].Code, false);

                            if (omit_mods.Contains(Globals.BoundModules[i].Code))
                            {
                                Globals.BoundModules[i] = new Globals.Mod_Entry(Globals.BoundModules[i].Code, true);
                            }
                        }
                        Properties.Settings.Default.ModulesInitialized = true;

                        // Load bool settings
                        Properties.Settings.Default.ModuleExtrasValue = 0;
                        if (!br.ReadBoolean()) { Properties.Settings.Default.ModuleExtrasValue |= ModuleExtras.NoSaveDelete; }
                        if (br.ReadBoolean()) { Properties.Settings.Default.ModuleExtrasValue |= ModuleExtras.SaveMiniGames; }
                        if (br.ReadBoolean()) { Properties.Settings.Default.ModuleExtrasValue |= ModuleExtras.SaveAllModules; }
                        if (br.ReadBoolean()) { Properties.Settings.Default.ModuleExtrasValue |= ModuleExtras.FixDream; }
                        if (br.ReadBoolean()) { Properties.Settings.Default.ModuleExtrasValue |= ModuleExtras.UnlockGalaxyMap; }
                        if (br.ReadBoolean()) { Properties.Settings.Default.ModuleExtrasValue |= ModuleExtras.FixCoordinates; }
                        if (br.ReadBoolean()) { Properties.Settings.Default.ModuleExtrasValue |= ModuleExtras.FixMindPrison; }
                        if (br.ReadBoolean()) { Properties.Settings.Default.ModuleExtrasValue |= ModuleExtras.UnlockVarDoors; }
                        if (br.ReadBoolean()) { Properties.Settings.Default.ModuleExtrasValue |= ModuleExtras.FixLevElevators; }
                        if (br.ReadBoolean()) { Properties.Settings.Default.ModuleExtrasValue |= ModuleExtras.VulkarSpiceLZ; }

                        // Load reachability settings
                        Properties.Settings.Default.VerifyReachability = br.ReadBoolean();
                        Properties.Settings.Default.IgnoreOnceEdges = br.ReadBoolean();
                        Properties.Settings.Default.AllowGlitchClip = br.ReadBoolean();
                        Properties.Settings.Default.AllowGlitchDlz = br.ReadBoolean();
                        Properties.Settings.Default.AllowGlitchFlu = br.ReadBoolean();
                        Properties.Settings.Default.AllowGlitchGpw = br.ReadBoolean();
                        Properties.Settings.Default.GoalIsMalak = br.ReadBoolean();
                        Properties.Settings.Default.GoalIsPazaak = br.ReadBoolean();
                        Properties.Settings.Default.GoalIsStarMaps = br.ReadBoolean();

                        Properties.Settings.Default.LastPresetComboIndex = -2;
                        Properties.Settings.Default.ModulePresetSelected = false;
                    }
                    #endregion
                    #region Items
                    if (Properties.Settings.Default.DoRandomization_Item)
                    {
                        // Check that Item data is present
                        if (new string(br.ReadChars(4)) != "ITEM") { throw new IOException("Item Randomization is Active, but no such preset data is found. Is the file corrupt?"); }

                        // Load in randolevels
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

                        // Load Omit Items
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
                    if (Properties.Settings.Default.DoRandomization_Sound)
                    {
                        // Check that Sound data is present
                        if (new string(br.ReadChars(4)) != "SOUN") { throw new IOException("Sound Randomization is Active, but no such preset data is found. Is the file corrupt?"); }

                        // Rando Levels
                        Properties.Settings.Default.RandomizeAreaMusic = br.ReadInt32();
                        Properties.Settings.Default.RandomizeBattleMusic = br.ReadInt32();
                        Properties.Settings.Default.RandomizeAmbientNoise = br.ReadInt32();
                        Properties.Settings.Default.RandomizeCutsceneNoise = br.ReadInt32();
                        Properties.Settings.Default.RandomizeNpcSounds = br.ReadInt32();
                        Properties.Settings.Default.RandomizePartySounds = br.ReadInt32();

                        // Bools
                        Properties.Settings.Default.RemoveDmcaMusic = br.ReadBoolean();
                        Properties.Settings.Default.MixNpcAndPartySounds = br.ReadBoolean();
                    }
                    #endregion
                    #region Models
                    if (Properties.Settings.Default.DoRandomization_Model)
                    {
                        // Check that Model data is present
                        if (new string(br.ReadChars(4)) != "MODE") { throw new IOException("Model Randomization is Active, but no such preset data is found. Is the file corrupt?"); }

                        // Settings
                        Properties.Settings.Default.RandomizeCharModels = br.ReadInt32();
                        Properties.Settings.Default.RandomizePlaceModels = br.ReadInt32();
                        Properties.Settings.Default.RandomizeDoorModels = br.ReadInt32();

                    }
                    #endregion
                    #region Textures
                    if (Properties.Settings.Default.DoRandomization_Texture)
                    {
                        // Check that Texture data is present
                        if (new string(br.ReadChars(4)) != "TEXU") { throw new IOException("Texture Randomization is Active, but no such preset data is found. Is the file corrupt?"); }

                        // Settings
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

                        // Texture Pack
                        Properties.Settings.Default.TexturePack = br.ReadInt32();
                    }
                    #endregion
                    #region 2DAs
                    if (Properties.Settings.Default.DoRandomization_TwoDA)
                    {
                        // Check that 2DA data is present
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
                    if (Properties.Settings.Default.DoRandomization_Text)
                    {
                        // Check that Text data is present
                        if (new string(br.ReadChars(4)) != "TEXT") { throw new IOException("Text Randomization is Active, but no such preset data is found. Is the file corrupt?"); }

                        // Text Settings
                        Properties.Settings.Default.TextSettingsValue = (TextSettings)br.ReadInt32();
                    }
                    #endregion
                    #region Other
                    if (Properties.Settings.Default.DoRandomization_Other)
                    {
                        // Check that Other data is present
                        if (new string(br.ReadChars(4)) != "OTHR") { throw new IOException("Other Randomization is Active, but no such preset data is found. Is the file corrupt?"); }

                        // Name Gen
                        Properties.Settings.Default.RandomizeNameGen = br.ReadBoolean();
                        if (Properties.Settings.Default.RandomizeNameGen)
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
                        Properties.Settings.Default.RandomizePazaakDecks = br.ReadBoolean();
                        Properties.Settings.Default.RandomizePartyMembers = br.ReadBoolean();
                        Properties.Settings.Default.RandomizeSwoopBoosters = br.ReadBoolean();
                        Properties.Settings.Default.RandomizeSwoopObstacles = br.ReadBoolean();
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
                // Version
                bw.Write(VERSION.ToCharArray());

                // Category Booleans
                bw.Write(Properties.Settings.Default.DoRandomization_Module);
                bw.Write(Properties.Settings.Default.DoRandomization_Item);
                bw.Write(Properties.Settings.Default.DoRandomization_Sound);
                bw.Write(Properties.Settings.Default.DoRandomization_Model);
                bw.Write(Properties.Settings.Default.DoRandomization_Texture);
                bw.Write(Properties.Settings.Default.DoRandomization_TwoDA);
                bw.Write(Properties.Settings.Default.DoRandomization_Text);
                bw.Write(Properties.Settings.Default.DoRandomization_Other);

                // Categories
                #region Modules
                if (Properties.Settings.Default.DoRandomization_Module)
                {
                    bw.Write("MODU".ToCharArray());

                    // FIX LATER - Possibility that BoundModules is empty if Module Form never opened.
                    foreach (Globals.Mod_Entry me in Globals.BoundModules.Where(x => x.Omitted))
                    {
                        bw.Write(me.Code.ToCharArray());
                        bw.Write('\0');
                    }
                    bw.Write('\n');

                    bw.Write(!Properties.Settings.Default.ModuleExtrasValue.HasFlag(ModuleExtras.NoSaveDelete));    // Milestone Delete
                    bw.Write(Properties.Settings.Default.ModuleExtrasValue.HasFlag(ModuleExtras.SaveMiniGames));    // Mini-Game Save
                    bw.Write(Properties.Settings.Default.ModuleExtrasValue.HasFlag(ModuleExtras.SaveAllModules));   // All Save
                    bw.Write(Properties.Settings.Default.ModuleExtrasValue.HasFlag(ModuleExtras.FixDream));         // Fixed Dream
                    bw.Write(Properties.Settings.Default.ModuleExtrasValue.HasFlag(ModuleExtras.UnlockGalaxyMap));  // Unlocked Galaxy Map
                    bw.Write(Properties.Settings.Default.ModuleExtrasValue.HasFlag(ModuleExtras.FixCoordinates));   // Fixed Module Coordinates
                    bw.Write(Properties.Settings.Default.ModuleExtrasValue.HasFlag(ModuleExtras.FixMindPrison));    // Fixed Rakatan Riddles
                    bw.Write(Properties.Settings.Default.ModuleExtrasValue.HasFlag(ModuleExtras.UnlockVarDoors));   // Unlock Problem Doors
                    bw.Write(Properties.Settings.Default.ModuleExtrasValue.HasFlag(ModuleExtras.FixLevElevators));  // Fixed Leviathan Elevators
                    bw.Write(Properties.Settings.Default.ModuleExtrasValue.HasFlag(ModuleExtras.VulkarSpiceLZ));    // Add Vulkar Spice Loading Zone

                }
                #endregion
                #region Items
                if (Properties.Settings.Default.DoRandomization_Item)
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
                #endregion
                #region Sounds
                if (Properties.Settings.Default.DoRandomization_Sound)
                {
                    bw.Write("SOUN".ToCharArray());

                    bw.Write(Properties.Settings.Default.RandomizeAreaMusic);
                    bw.Write(Properties.Settings.Default.RandomizeBattleMusic);
                    bw.Write(Properties.Settings.Default.RandomizeAmbientNoise);
                    bw.Write(Properties.Settings.Default.RandomizeCutsceneNoise);
                    bw.Write(Properties.Settings.Default.RandomizeNpcSounds);
                    bw.Write(Properties.Settings.Default.RandomizePartySounds);

                    bw.Write(Properties.Settings.Default.RemoveDmcaMusic);
                    bw.Write(Properties.Settings.Default.MixNpcAndPartySounds);

                }
                #endregion
                #region Models
                if (Properties.Settings.Default.DoRandomization_Model)
                {
                    bw.Write("MODE".ToCharArray());

                    bw.Write(Properties.Settings.Default.RandomizeCharModels);
                    bw.Write(Properties.Settings.Default.RandomizePlaceModels);
                    bw.Write(Properties.Settings.Default.RandomizeDoorModels);
                }
                #endregion
                #region Textures
                if (Properties.Settings.Default.DoRandomization_Texture)
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
                #endregion
                #region 2DAs
                if (Properties.Settings.Default.DoRandomization_TwoDA)
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
                #endregion
                #region Text
                if (Properties.Settings.Default.DoRandomization_Text)
                {
                    bw.Write("TEXT".ToCharArray());

                    // Text Settings
                    bw.Write((int)Properties.Settings.Default.TextSettingsValue);

                }
                #endregion
                #region Other
                if (Properties.Settings.Default.DoRandomization_Other)
                {
                    bw.Write("OTHR".ToCharArray());

                    bw.Write(Properties.Settings.Default.RandomizeNameGen);
                    if (Properties.Settings.Default.RandomizeNameGen)
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
                    bw.Write(Properties.Settings.Default.RandomizePazaakDecks);
                    bw.Write(Properties.Settings.Default.RandomizePartyMembers);
                    bw.Write(Properties.Settings.Default.RandomizeSwoopBoosters);
                    bw.Write(Properties.Settings.Default.RandomizeSwoopObstacles);
                }
                #endregion
            }
        }
        #endregion
    }
}
