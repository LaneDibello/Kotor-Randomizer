using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace kotor_Randomizer_2.Models
{
    /// <summary>
    /// eXtensible Kotor Randomizer Preset
    /// </summary>
    class XKRP
    {
        #region Constants
        private const string XML_AMBIENT        = "Ambient";
        private const string XML_AREA           = "Area";
        private const string XML_ARMBAND        = "Armband";
        private const string XML_ARMOR          = "Armor";
        private const string XML_ATTACK         = "Attack";
        private const string XML_AUDIO          = "Audio";
        private const string XML_BATTLE         = "Battle";
        private const string XML_BELT           = "Belt";
        private const string XML_BLASTER        = "Blaster";
        private const string XML_BODY           = "PBody";
        private const string XML_CHAR           = "Character";
        private const string XML_CHIDE          = "CHide";
        private const string XML_CLIP           = "Clip";
        private const string XML_CODE           = "Code";
        private const string XML_COLUMNS        = "Columns";
        private const string XML_CREATURE       = "Creature";
        private const string XML_CUBE_MAP       = "CubeMap";
        private const string XML_CUTSCENE       = "Cutscene";
        private const string XML_CWEAPON        = "CWeapon";
        private const string XML_DAMAGE         = "Damage";
        private const string XML_DLZ            = "DLZ";
        private const string XML_DOOR           = "Door";
        private const string XML_DROID          = "Droid";
        private const string XML_EASY_PANELS    = "EasyPanels";
        private const string XML_EFFECT         = "Effect";
        private const string XML_FIRE           = "Fire";
        private const string XML_FIRST_NAME_F   = "FirstF";
        private const string XML_FIRST_NAME_M   = "FirstM";
        private const string XML_FLU            = "FLU";
        private const string XML_GENERAL        = "General";
        private const string XML_GLITCHES       = "Glitches";
        private const string XML_GLOVE          = "Glove";
        private const string XML_GOALS          = "Goals";
        private const string XML_GPW            = "GPW";
        private const string XML_GRENADE        = "Grenade";
        private const string XML_HEAD           = "PHead";
        private const string XML_IGNORE_ONCE    = "IgnoreOnce";
        private const string XML_IMPLANT        = "Implant";
        private const string XML_ITEM           = "Item";
        private const string XML_LAST_NAME      = "Last";
        private const string XML_LIGHTSABER     = "Lightsaber";
        private const string XML_LOGIC          = "Logic";
        private const string XML_LOOP           = "Loop";
        private const string XML_MALAK          = "Malak";
        private const string XML_MAPS           = "StarMaps";
        private const string XML_MASK           = "Mask";
        private const string XML_MEDICAL        = "Medical";
        private const string XML_MELEE          = "Melee";
        private const string XML_MINE           = "Mine";
        private const string XML_MIXNPCPARTY    = "MixNpcParty";
        private const string XML_MODEL          = "Model";
        private const string XML_MODULE         = "Module";
        private const string XML_MOVE           = "Move";
        private const string XML_NAME           = "Name";
        private const string XML_NAMES          = "Names";
        private const string XML_NPC            = "Npc";
        private const string XML_OMIT           = "Omit";
        private const string XML_OMIT_AIRLOCK   = "OmitAirlock";
        private const string XML_OMIT_BROKEN    = "OmitBroken";
        private const string XML_OMIT_LARGE     = "OmitLarge";
        private const string XML_OTHER          = "Other";
        private const string XML_PACK           = "Pack";
        private const string XML_PARRY          = "Parry";
        private const string XML_PARTY          = "Party";
        private const string XML_PAUSE          = "Pause";
        private const string XML_PAZAAK         = "Pazaak";
        private const string XML_PLAC           = "Placeable";
        private const string XML_PLACE          = "Placeable";
        private const string XML_PLANETARY      = "Planetary";
        private const string XML_POLYMORPH      = "Polymorph";
        private const string XML_PRESET         = "Preset";
        private const string XML_QOL            = "QoL";
        private const string XML_REACHABLE      = "Reachable";
        private const string XML_REMOVE_DMCA    = "RemoveDmca";
        private const string XML_RULES          = "Rules";
        private const string XML_SETTINGS       = "Settings";
        private const string XML_STRONG_GOALS   = "StrongGoals";
        private const string XML_STUNT          = "Stunt";
        private const string XML_SWOOP_BOOSTERS = "SwoopBoosters";
        private const string XML_SWOOP_OBSTACLE = "SwoopObstacle";
        private const string XML_TABLE          = "Table";
        private const string XML_TABLES         = "Tables";
        private const string XML_TEXT           = "Text";
        private const string XML_TEXTURE        = "Texture";
        private const string XML_UNLOCKS        = "Unlocks";
        private const string XML_UPGRADE        = "Upgrade";
        private const string XML_VARIOUS        = "Various";
        private const string XML_VERSION        = "Version";
        private const string XML_VEHICLE        = "Vehicle";
        private const string XML_WEAPON         = "Weapon";
        #endregion

        #region Constructors

        public XKRP()
        {

        }

        public XKRP(RandomizerBase rando)
        {
            Randomizer = rando;
        }

        public XKRP(Kotor1Randomizer k1rando) : this(k1rando as RandomizerBase)
        {

        }

        public XKRP(Kotor2Randomizer k2rando) : this(k2rando as RandomizerBase)
        {

        }

        #endregion

        #region Properties

        public RandomizerBase Randomizer { get; set; }

        public Game Game => Randomizer?.Game ?? Game.Unsupported;
        public bool DoRandomizeModules => Randomizer.SupportsModules && ((IRandomizeModules)Randomizer).DoRandomizeModules;
        public bool DoRandomizeItems => Randomizer.SupportsItems && ((IRandomizeItems)Randomizer).DoRandomizeItems;


        #endregion

        #region Read Methods

        public static XKRP ReadFromFile(string path)
        {
            return new XKRP();
        }

        #endregion

        #region Write Methods

        public void WriteToFile(string path)
        {
            using (var w = new XmlTextWriter(path, null))
            {
                w.WriteStartDocument();
                w.WriteStartElement(XML_SETTINGS);  // Begin Settings

                WriteGeneralSettings(w);
                //if (DoRandomizeAudio   ) WriteAudioSettings(w);
                if (DoRandomizeItems   ) WriteItemSettings(w);
                //if (DoRandomizeModels  ) WriteModelSettings(w);
                if (DoRandomizeModules ) WriteModuleSettings(w);
                //if (DoRandomizeOther   ) WriteOtherSettings(w);
                //if (DoRandomizeTables  ) WriteTableSettings(w);
                //if (DoRandomizeText    ) WriteTextSettings(w);
                //if (DoRandomizeTextures) WriteTextureSettings(w);

                w.WriteEndElement();    // End Settings
                w.WriteEndDocument();
                w.Flush();
            }
        }

        private void WriteGeneralSettings(XmlTextWriter w)
        {
            throw new NotImplementedException();
        }

        private void WriteItemSettings(XmlTextWriter w)
        {
            throw new NotImplementedException();
        }

        private void WriteModuleSettings(XmlTextWriter w)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
