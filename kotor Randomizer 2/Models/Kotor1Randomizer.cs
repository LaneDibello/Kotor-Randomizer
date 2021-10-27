using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using ClosedXML.Excel;

namespace kotor_Randomizer_2.Models
{
    /// <summary>
    /// Encapsulates the settings and processes used to randomize Kotor 1.
    /// </summary>
    public class Kotor1Randomizer : RandomizerBase
    {
        #region Constants
        public const ModuleExtras EXTRAS_MASK = ModuleExtras.NoSaveDelete   | ModuleExtras.SaveMiniGames | ModuleExtras.SaveAllModules |
                                                ModuleExtras.FixCoordinates | ModuleExtras.FixDream      | ModuleExtras.FixMindPrison  |
                                                ModuleExtras.FastEnvirosuit | ModuleExtras.EarlyT3       | ModuleExtras.VulkarSpiceLZ  |
                                                ModuleExtras.FixFighterEncounter;
        public const ModuleExtras UNLOCKS_MASK = ModuleExtras.UnlockDanRuins   | ModuleExtras.UnlockGalaxyMap     | ModuleExtras.UnlockKorValley    |
                                                 ModuleExtras.UnlockLevElev    | ModuleExtras.EnableLevHangarElev | ModuleExtras.UnlockManEmbassy   |
                                                 ModuleExtras.UnlockManHangar  | ModuleExtras.UnlockStaBastila    | ModuleExtras.UnlockTarUndercity |
                                                 ModuleExtras.UnlockTarVulkar  | ModuleExtras.UnlockUnkSummit     | ModuleExtras.UnlockUnkTempleExit;

        #region XML Consts
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
        #endregion XML Consts

        #region Category Names
        private const string CATEGORY_OVERRIDES = "Overrides";
        private const string CATEGORY_MODULES   = "Modules and General";
        private const string CATEGORY_ITEMS     = "Items";
        private const string CATEGORY_AUDIO     = "Audio";
        private const string CATEGORY_MODELS    = "Models";
        private const string CATEGORY_TEXTURES  = "Textures";
        private const string CATEGORY_TABLES    = "Tables";
        private const string CATEGORY_TEXT      = "Text";
        private const string CATEGORY_OTHER     = "Other";
        #endregion
        #endregion

        #region Constructors
        /// <summary>
        /// Constructs the randomizer with default settings.
        /// </summary>
        public Kotor1Randomizer() : this(string.Empty) { }

        /// <summary>
        /// Constructs the randomizer with settings read from the given preset file path.
        /// </summary>
        /// <param name="path">Full path to a randomizer preset file.</param>
        public Kotor1Randomizer(string path)
        {
            // Create list of unlockable doors.
            GeneralLockedDoors.Add(new UnlockableDoor()
            {
                Area = "LEV", Label = "Hangar Access",     Tag = ModuleExtras.UnlockLevElev,
                ToolTipMessage = "The Leviathan elevator will not restrict you from going to the Hangar early."
            });
            GeneralLockedDoors.Add(new UnlockableDoor()
            {
                Area = "LEV", Label = "Enable Hangar Elevator", Tag = ModuleExtras.EnableLevHangarElev,
                ToolTipMessage = "The Leviathan Hangar elevator will now be usable."
            });
            GeneralLockedDoors.Add(new UnlockableDoor()
            {
                Area = "MAN", Label = "Republic Embassy",  Tag = ModuleExtras.UnlockManEmbassy,
                ToolTipMessage = "Unlocks the Republic Embassy door and the door to the submersible."
            });
            GeneralLockedDoors.Add(new UnlockableDoor()
            {
                Area = "MAN", Label = "Sith Hangar",       Tag = ModuleExtras.UnlockManHangar,
                ToolTipMessage = "Unlocks the Sith Hangar door before visiting Roland."
            });
            GeneralLockedDoors.Add(new UnlockableDoor()
            {
                Area = "STA", Label = "Door to Bastila",   Tag = ModuleExtras.UnlockStaBastila,
                ToolTipMessage = "Unlocks the door leading to the Bastila fight, allowing it to be opened after fighting her."
            });
            GeneralLockedDoors.Add(new UnlockableDoor()
            {
                Area = "TAR", Label = "Undercity",         Tag = ModuleExtras.UnlockTarUndercity,
                ToolTipMessage = "Unlocks the Undercity door in the Lower City."
            });
            GeneralLockedDoors.Add(new UnlockableDoor()
            {
                Area = "TAR", Label = "Vulkar Base",       Tag = ModuleExtras.UnlockTarVulkar,
                ToolTipMessage = "Unlocks the Vulkar Base in the Lower City."
            });
            GeneralLockedDoors.Add(new UnlockableDoor()
            {
                Area = "UNK", Label = "Summit Exit",       Tag = ModuleExtras.UnlockUnkSummit,
                ToolTipMessage = "Unlocks the exit door from the Temple Summit."
            });
            GeneralLockedDoors.Add(new UnlockableDoor()
            {
                Area = "UNK", Label = "Temple Exit",       Tag = ModuleExtras.UnlockUnkTempleExit,
                ToolTipMessage = "Unlocks the exit door from the Temple Main Floor."
            });
            GeneralLockedDoors.Add(new UnlockableDoor()
            {
                Area = "DAN", Label = "Ruins Door",        Tag = ModuleExtras.UnlockDanRuins,
                ToolTipMessage = "Unlocks the door into the Dantooine Ruins."
            });
            GeneralLockedDoors.Add(new UnlockableDoor()
            {
                Area = "EBO", Label = "Galaxy Map",        Tag = ModuleExtras.UnlockGalaxyMap,
                ToolTipMessage = "Unlock all destinations on the Ebon Hawk galaxy map from the start of the game."
            });
            GeneralLockedDoors.Add(new UnlockableDoor()
            {
                Area = "KOR", Label = "Valley After Tomb", Tag = ModuleExtras.UnlockKorValley,
                ToolTipMessage = "Ensures the Sith Tomb and Sith Academy remain unlocked regardless of the Uthar / Yuthura outcome in Naga Sadow."
            });

            // Create module digraph and get the list of modules.
            ModuleDigraph graph;
            var modulesPath = Path.Combine(Environment.CurrentDirectory, "Xml", "KotorModules.xml");
            if (!File.Exists(modulesPath))
            {
                // Personal debug path... will not work for all developers.
                modulesPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"GitHub\Kotor-Randomizer-2\kotor Randomizer 2\Xml\KotorModules.xml");
            }
            graph = new ModuleDigraph(modulesPath);
            ModuleRandomizedList = new ObservableCollection<ModuleVertex>(graph.Modules);

            // Get list of randomizable items.
            ItemRandomizedList = new ObservableCollection<RandomizableItem>(Globals.ITEM_LIST_FULL);

            // Get list of randomizable tables.
            Table2DAs = new ObservableCollection<RandomizableTable>(Globals.TWODA_COLLUMNS.Select(table => new RandomizableTable(table.Key, table.Value)));

            // Add property changed event hooks.
            foreach (var table in Table2DAs)
                table.PropertyChanged += UpdateRandomizedTableCount;

            // Load settings from file if the path is not empty.
            if (!string.IsNullOrWhiteSpace(path) && File.Exists(path))
                Load(path);
            else
                SettingsFileName = string.Empty;
        }
        #endregion Constructors

        #region Properties
        #region Animation Properties
        private RandomizationLevel _animationAttack;
        public RandomizationLevel AnimationAttack
        {
            get => _animationAttack;
            set => SetField(ref _animationAttack, value);
        }

        private RandomizationLevel _animationDamage;
        public RandomizationLevel AnimationDamage
        {
            get => _animationDamage;
            set => SetField(ref _animationDamage, value);
        }

        private RandomizationLevel _animationFire;
        public RandomizationLevel AnimationFire
        {
            get => _animationFire;
            set => SetField(ref _animationFire, value);
        }

        private RandomizationLevel _animationLoop;
        public RandomizationLevel AnimationLoop
        {
            get => _animationLoop;
            set => SetField(ref _animationLoop, value);
        }

        private RandomizationLevel _animationParry;
        public RandomizationLevel AnimationParry
        {
            get => _animationParry;
            set => SetField(ref _animationParry, value);
        }

        private RandomizationLevel _animationPause;
        public RandomizationLevel AnimationPause
        {
            get => _animationPause;
            set => SetField(ref _animationPause, value);
        }

        private RandomizationLevel _animationMove;
        public RandomizationLevel AnimationMove
        {
            get => _animationMove;
            set => SetField(ref _animationMove, value);
        }
        #endregion

        #region Audio Properties
        private RandomizationLevel _audioAmbientNoise;
        public RandomizationLevel AudioAmbientNoise
        {
            get => _audioAmbientNoise;
            set => SetField(ref _audioAmbientNoise, value);
        }

        private RandomizationLevel _audioAreaMusic;
        public RandomizationLevel AudioAreaMusic
        {
            get => _audioAreaMusic;
            set => SetField(ref _audioAreaMusic, value);
        }

        private RandomizationLevel _audioBattleMusic;
        public RandomizationLevel AudioBattleMusic
        {
            get => _audioBattleMusic;
            set => SetField(ref _audioBattleMusic, value);
        }

        private RandomizationLevel _audioCutsceneNoise;
        public RandomizationLevel AudioCutsceneNoise
        {
            get => _audioCutsceneNoise;
            set => SetField(ref _audioCutsceneNoise, value);
        }

        private RandomizationLevel _audioNpcSounds;
        public RandomizationLevel AudioNpcSounds
        {
            get => _audioNpcSounds;
            set => SetField(ref _audioNpcSounds, value);
        }

        private RandomizationLevel _audioPartySounds;
        public RandomizationLevel AudioPartySounds
        {
            get => _audioPartySounds;
            set => SetField(ref _audioPartySounds, value);
        }

        private bool _audioMixNpcAndPartySounds;
        public bool AudioMixNpcAndPartySounds
        {
            get => _audioMixNpcAndPartySounds;
            set => SetField(ref _audioMixNpcAndPartySounds, value);
        }

        private bool _audioRemoveDmcaMusic;
        public bool AudioRemoveDmcaMusic
        {
            get => _audioRemoveDmcaMusic;
            set => SetField(ref _audioRemoveDmcaMusic, value);
        }
        #endregion Audio Properties

        #region General Properties
        private ModuleExtras _generalModuleExtrasValue;
        public ModuleExtras GeneralModuleExtrasValue
        {
            get => _generalModuleExtrasValue;
            set => SetField(ref _generalModuleExtrasValue, value);
        }

        private ObservableCollection<UnlockableDoor> _generalUnlockedDoors = new ObservableCollection<UnlockableDoor>();
        public ObservableCollection<UnlockableDoor> GeneralUnlockedDoors
        {
            get => _generalUnlockedDoors;
            set => SetField(ref _generalUnlockedDoors, value);
        }

        private ObservableCollection<UnlockableDoor> _generalLockedDoors = new ObservableCollection<UnlockableDoor>();
        public ObservableCollection<UnlockableDoor> GeneralLockedDoors
        {
            get => _generalLockedDoors;
            set => SetField(ref _generalLockedDoors, value);
        }
        #endregion General Properties

        #region Item Properties
        private RandomizationLevel _itemArmbands;
        public RandomizationLevel ItemArmbands
        {
            get => _itemArmbands;
            set => SetField(ref _itemArmbands, value);
        }

        private RandomizationLevel _itemArmor;
        public RandomizationLevel ItemArmor
        {
            get => _itemArmor;
            set => SetField(ref _itemArmor, value);
        }

        private RandomizationLevel _itemBelts;
        public RandomizationLevel ItemBelts
        {
            get => _itemBelts;
            set => SetField(ref _itemBelts, value);
        }

        private RandomizationLevel _itemBlasters;
        public RandomizationLevel ItemBlasters
        {
            get => _itemBlasters;
            set => SetField(ref _itemBlasters, value);
        }

        private RandomizationLevel _itemCreatureHides;
        public RandomizationLevel ItemCreatureHides
        {
            get => _itemCreatureHides;
            set => SetField(ref _itemCreatureHides, value);
        }

        private RandomizationLevel _itemCreatureWeapons;
        public RandomizationLevel ItemCreatureWeapons
        {
            get => _itemCreatureWeapons;
            set => SetField(ref _itemCreatureWeapons, value);
        }

        private RandomizationLevel _itemDroidEquipment;
        public RandomizationLevel ItemDroidEquipment
        {
            get => _itemDroidEquipment;
            set => SetField(ref _itemDroidEquipment, value);
        }

        private RandomizationLevel _itemGloves;
        public RandomizationLevel ItemGloves
        {
            get => _itemGloves;
            set => SetField(ref _itemGloves, value);
        }

        private RandomizationLevel _itemGrenades;
        public RandomizationLevel ItemGrenades
        {
            get => _itemGrenades;
            set => SetField(ref _itemGrenades, value);
        }

        private RandomizationLevel _itemImplants;
        public RandomizationLevel ItemImplants
        {
            get => _itemImplants;
            set => SetField(ref _itemImplants, value);
        }

        private RandomizationLevel _itemLightsabers;
        public RandomizationLevel ItemLightsabers
        {
            get => _itemLightsabers;
            set => SetField(ref _itemLightsabers, value);
        }

        private RandomizationLevel _itemMasks;
        public RandomizationLevel ItemMasks
        {
            get => _itemMasks;
            set => SetField(ref _itemMasks, value);
        }

        private RandomizationLevel _itemMeleeWeapons;
        public RandomizationLevel ItemMeleeWeapons
        {
            get => _itemMeleeWeapons;
            set => SetField(ref _itemMeleeWeapons, value);
        }

        private RandomizationLevel _itemMines;
        public RandomizationLevel ItemMines
        {
            get => _itemMines;
            set => SetField(ref _itemMines, value);
        }

        private RandomizationLevel _itemPazaakCards;
        public RandomizationLevel ItemPazaakCards
        {
            get => _itemPazaakCards;
            set => SetField(ref _itemPazaakCards, value);
        }

        private RandomizationLevel _itemMedical;
        public RandomizationLevel ItemMedical
        {
            get => _itemMedical;
            set => SetField(ref _itemMedical, value);
        }

        private RandomizationLevel _itemUpgrades;
        public RandomizationLevel ItemUpgrades
        {
            get => _itemUpgrades;
            set => SetField(ref _itemUpgrades, value);
        }

        private RandomizationLevel _itemVarious;
        public RandomizationLevel ItemVarious
        {
            get => _itemVarious;
            set => SetField(ref _itemVarious, value);
        }

        private ObservableCollection<RandomizableItem> _itemOmittedList = new ObservableCollection<RandomizableItem>();
        public ObservableCollection<RandomizableItem> ItemOmittedList
        {
            get => _itemOmittedList;
            set => SetField(ref _itemOmittedList, value);
        }

        private ObservableCollection<RandomizableItem> _itemRandomizedList = new ObservableCollection<RandomizableItem>();
        public ObservableCollection<RandomizableItem> ItemRandomizedList
        {
            get => _itemRandomizedList;
            set => SetField(ref _itemRandomizedList, value);
        }

        private string _itemOmittedPreset = Globals.OMIT_ITEM_PRESETS.First().Key;
        public string ItemOmittedPreset
        {
            get => _itemOmittedPreset;
            set => SetField(ref _itemOmittedPreset, value);
        }
        #endregion Item Properties

        #region Model Properties
        private bool _modelCharacterRando;
        public bool ModelCharacterRando
        {
            get => _modelCharacterRando;
            set => SetField(ref _modelCharacterRando, value);
        }

        private bool _modelCharacterOmitLarge = true;
        public bool ModelCharacterOmitLarge
        {
            get => _modelCharacterOmitLarge;
            set => SetField(ref _modelCharacterOmitLarge, value);
        }

        private bool _modelCharacterOmitBroken = true;
        public bool ModelCharacterOmitBroken
        {
            get => _modelCharacterOmitBroken;
            set => SetField(ref _modelCharacterOmitBroken, value);
        }

        private bool _modelDoorRando;
        public bool ModelDoorRando
        {
            get => _modelDoorRando;
            set => SetField(ref _modelDoorRando, value);
        }

        private bool _modelDoorOmitAirlock = true;
        public bool ModelDoorOmitAirlock
        {
            get => _modelDoorOmitAirlock;
            set => SetField(ref _modelDoorOmitAirlock, value);
        }

        private bool _modelDoorOmitBroken = true;
        public bool ModelDoorOmitBroken
        {
            get => _modelDoorOmitBroken;
            set => SetField(ref _modelDoorOmitBroken, value);
        }

        private bool _modelPlaceableRando;
        public bool ModelPlaceableRando
        {
            get => _modelPlaceableRando;
            set => SetField(ref _modelPlaceableRando, value);
        }

        private bool _modelPlaceableOmitLarge = true;
        public bool ModelPlaceableOmitLarge
        {
            get => _modelPlaceableOmitLarge;
            set => SetField(ref _modelPlaceableOmitLarge, value);
        }

        private bool _modelPlaceableOmitBroken = true;
        public bool ModelPlaceableOmitBroken
        {
            get => _modelPlaceableOmitBroken;
            set => SetField(ref _modelPlaceableOmitBroken, value);
        }

        private bool _modelPlaceableEasyPanels;
        public bool ModelPlaceableEasyPanels
        {
            get => _modelPlaceableEasyPanels;
            set => SetField(ref _modelPlaceableEasyPanels, value);
        }
        #endregion Model Properties

        #region Module Properties
        private bool _moduleAllowGlitchClip;
        public bool ModuleAllowGlitchClip
        {
            get => _moduleAllowGlitchClip;
            set => SetField(ref _moduleAllowGlitchClip, value);
        }

        private bool _moduleAllowGlitchDlz;
        public bool ModuleAllowGlitchDlz
        {
            get => _moduleAllowGlitchDlz;
            set => SetField(ref _moduleAllowGlitchDlz, value);
        }

        private bool _moduleAllowGlitchFlu;
        public bool ModuleAllowGlitchFlu
        {
            get => _moduleAllowGlitchFlu;
            set => SetField(ref _moduleAllowGlitchFlu, value);
        }

        private bool _moduleAllowGlitchGpw;
        public bool ModuleAllowGlitchGpw
        {
            get => _moduleAllowGlitchGpw;
            set => SetField(ref _moduleAllowGlitchGpw, value);
        }

        private bool _moduleLogicStrongGoals;
        public bool ModuleLogicStrongGoals
        {
            get => _moduleLogicStrongGoals;
            set => SetField(ref _moduleLogicStrongGoals, value);
        }

        private bool _moduleGoalIsMalak = true;
        public bool ModuleGoalIsMalak
        {
            get => _moduleGoalIsMalak;
            set => SetField(ref _moduleGoalIsMalak, value);
        }

        private bool _moduleGoalIsPazaak;
        public bool ModuleGoalIsPazaak
        {
            get => _moduleGoalIsPazaak;
            set => SetField(ref _moduleGoalIsPazaak, value);
        }

        private bool _moduleGoalIsStarMap;
        public bool ModuleGoalIsStarMap
        {
            get => _moduleGoalIsStarMap;
            set => SetField(ref _moduleGoalIsStarMap, value);
        }

        private bool _moduleGoalIsFullParty;
        public bool ModuleGoalIsFullParty
        {
            get => _moduleGoalIsFullParty;
            set => SetField(ref _moduleGoalIsFullParty, value);
        }

        private bool _moduleLogicIgnoreOnceEdges = true;
        public bool ModuleLogicIgnoreOnceEdges
        {
            get => _moduleLogicIgnoreOnceEdges;
            set => SetField(ref _moduleLogicIgnoreOnceEdges, value);
        }

        private bool _moduleLogicRandoRules = true;
        public bool ModuleLogicRandoRules
        {
            get => _moduleLogicRandoRules;
            set => SetField(ref _moduleLogicRandoRules, value);
        }

        private bool _moduleLogicReachability = true;
        public bool ModuleLogicReachability
        {
            get => _moduleLogicReachability;
            set => SetField(ref _moduleLogicReachability, value);
        }

        private ObservableCollection<ModuleVertex> _moduleRandomizedList = new ObservableCollection<ModuleVertex>();
        public ObservableCollection<ModuleVertex> ModuleRandomizedList
        {
            get => _moduleRandomizedList;
            set => SetField(ref _moduleRandomizedList, value);
        }

        private ObservableCollection<ModuleVertex> _moduleOmittedList = new ObservableCollection<ModuleVertex>();
        public ObservableCollection<ModuleVertex> ModuleOmittedList
        {
            get => _moduleOmittedList;
            set => SetField(ref _moduleOmittedList, value);
        }

        private string _moduleShufflePreset = Globals.OMIT_PRESETS.First().Key;
        public string ModuleShufflePreset
        {
            get => _moduleShufflePreset;
            set => SetField(ref _moduleShufflePreset, value);
        }
        #endregion Module Properties

        #region Other Properties
        private string _otherFirstNamesF;
        public string OtherFirstNamesF
        {
            get => _otherFirstNamesF;
            set => SetField(ref _otherFirstNamesF, value);
        }

        private string _otherFirstNamesM;
        public string OtherFirstNamesM
        {
            get => _otherFirstNamesM;
            set => SetField(ref _otherFirstNamesM, value);
        }

        private string _otherLastNames;
        public string OtherLastNames
        {
            get => _otherLastNames;
            set => SetField(ref _otherLastNames, value);
        }

        private bool _otherNameGeneration;
        public bool OtherNameGeneration
        {
            get => _otherNameGeneration;
            set => SetField(ref _otherNameGeneration, value);
        }

        private bool _otherPartyMembers;
        public bool OtherPartyMembers
        {
            get => _otherPartyMembers;
            set => SetField(ref _otherPartyMembers, value);
        }

        private bool _otherPazaakDecks;
        public bool OtherPazaakDecks
        {
            get => _otherPazaakDecks;
            set => SetField(ref _otherPazaakDecks, value);
        }

        private bool _otherPolymorphMode;
        public bool OtherPolymorphMode
        {
            get => _otherPolymorphMode;
            set => SetField(ref _otherPolymorphMode, value);
        }

        private bool _otherSwoopBoosters;
        public bool OtherSwoopBoosters
        {
            get => _otherSwoopBoosters;
            set => SetField(ref _otherSwoopBoosters, value);
        }

        private bool _otherSwoopObstacles;
        public bool OtherSwoopObstacles
        {
            get => _otherSwoopObstacles;
            set => SetField(ref _otherSwoopObstacles, value);
        }
        #endregion Other Properties

        #region Table Properties
        private ObservableCollection<RandomizableTable> _table2DAs;
        public ObservableCollection<RandomizableTable> Table2DAs
        {
            get => _table2DAs;
            set => SetField(ref _table2DAs, value);
        }

        private int _tableRandomizedCount;
        public int TableRandomizedCount
        {
            get => _tableRandomizedCount;
            set => SetField(ref _tableRandomizedCount, value);
        }
        #endregion

        #region Text Properties
        private TextSettings _textSettingsValue;
        public TextSettings TextSettingsValue
        {
            get => _textSettingsValue;
            set => SetField(ref _textSettingsValue, value);
        }
        #endregion Text Properties

        #region Texture Properties
        private RandomizationLevel _textureCreatures;
        public RandomizationLevel TextureCreatures
        {
            get => _textureCreatures;
            set => SetField(ref _textureCreatures, value);
        }

        private RandomizationLevel _textureCubeMaps;
        public RandomizationLevel TextureCubeMaps
        {
            get => _textureCubeMaps;
            set => SetField(ref _textureCubeMaps, value);
        }

        private RandomizationLevel _textureEffects;
        public RandomizationLevel TextureEffects
        {
            get => _textureEffects;
            set => SetField(ref _textureEffects, value);
        }

        private RandomizationLevel _textureItems;
        public RandomizationLevel TextureItems
        {
            get => _textureItems;
            set => SetField(ref _textureItems, value);
        }

        private RandomizationLevel _textureNPC;
        public RandomizationLevel TextureNPC
        {
            get => _textureNPC;
            set => SetField(ref _textureNPC, value);
        }

        private RandomizationLevel _textureOther;
        public RandomizationLevel TextureOther
        {
            get => _textureOther;
            set => SetField(ref _textureOther, value);
        }

        private RandomizationLevel _textureParty;
        public RandomizationLevel TextureParty
        {
            get => _textureParty;
            set => SetField(ref _textureParty, value);
        }

        private RandomizationLevel _texturePlaceables;
        public RandomizationLevel TexturePlaceables
        {
            get => _texturePlaceables;
            set => SetField(ref _texturePlaceables, value);
        }

        private RandomizationLevel _texturePlanetary;
        public RandomizationLevel TexturePlanetary
        {
            get => _texturePlanetary;
            set => SetField(ref _texturePlanetary, value);
        }

        private RandomizationLevel _texturePlayerBodies;
        public RandomizationLevel TexturePlayerBodies
        {
            get => _texturePlayerBodies;
            set => SetField(ref _texturePlayerBodies, value);
        }

        private RandomizationLevel _texturePlayerHeads;
        public RandomizationLevel TexturePlayerHeads
        {
            get => _texturePlayerHeads;
            set => SetField(ref _texturePlayerHeads, value);
        }

        private RandomizationLevel _textureStunt;
        public RandomizationLevel TextureStunt
        {
            get => _textureStunt;
            set => SetField(ref _textureStunt, value);
        }

        private RandomizationLevel _textureVehicles;
        public RandomizationLevel TextureVehicles
        {
            get => _textureVehicles;
            set => SetField(ref _textureVehicles, value);
        }

        private RandomizationLevel _textureWeapons;
        public RandomizationLevel TextureWeapons
        {
            get => _textureWeapons;
            set => SetField(ref _textureWeapons, value);
        }

        private TexturePack _textureSelectedPack = TexturePack.HighQuality;
        public TexturePack TextureSelectedPack
        {
            get => _textureSelectedPack;
            set => SetField(ref _textureSelectedPack, value);
        }
        #endregion Texture Properties

        #region Active Rando Properties
        public bool DoRandomizeAnimation =>
            (AnimationAttack | AnimationDamage | AnimationFire | AnimationLoop
            | AnimationParry | AnimationPause  | AnimationMove)
            != RandomizationLevel.None;

        public bool DoRandomizeAudio => DoRandomizeMusic || DoRandomizeSound;

        public bool DoRandomizeMusic =>
            (AudioAreaMusic | AudioAmbientNoise | AudioBattleMusic | AudioCutsceneNoise)
            != RandomizationLevel.None
            || AudioRemoveDmcaMusic;

        public bool DoRandomizeSound =>
            (AudioAmbientNoise | AudioBattleMusic | AudioNpcSounds | AudioPartySounds)
            != RandomizationLevel.None;

        public bool DoRandomizeItems =>
            (ItemArmbands       | ItemArmor           | ItemBelts          | ItemBlasters
            | ItemCreatureHides | ItemCreatureWeapons | ItemDroidEquipment | ItemGloves
            | ItemGrenades      | ItemImplants        | ItemLightsabers    | ItemMasks
            | ItemMedical       | ItemMeleeWeapons    | ItemMines          | ItemPazaakCards
            | ItemUpgrades      | ItemVarious)
            != RandomizationLevel.None;

        public bool DoRandomizeModels =>
            ModelCharacterRando ||
            ModelDoorRando      ||
            ModelPlaceableRando;

        public bool DoRandomizeModules =>   // A couple of general options are handled by module randomization.
            (_moduleRandomizedList?.Count ?? 0) > 1
            || GeneralModuleExtrasValue   != ModuleExtras.Default
            || GeneralUnlockedDoors.Count != 0;

        public bool DoRandomizeOther =>
            OtherNameGeneration ||
            OtherPartyMembers   ||
            OtherPazaakDecks    ||
            OtherPolymorphMode  ||
            OtherSwoopBoosters  ||
            OtherSwoopObstacles;

        //public bool DoRandomizeParty
        //{
        //    get
        //    {
        //        return false;   // Not yet implemented.
        //    }
        //}

        public bool DoRandomizeTables =>
            Table2DAs.Any(rt => rt.IsRandomized) ||
            DoRandomizeAnimation;

        public bool DoRandomizeText =>
            TextSettingsValue.HasFlag(TextSettings.RandoDialogEntries) ||
            TextSettingsValue.HasFlag(TextSettings.RandoDialogReplies) ||
            TextSettingsValue.HasFlag(TextSettings.RandoFullTLK);

        public bool DoRandomizeTextures =>
            (TextureCreatures  | TextureCubeMaps     | TextureEffects     | TextureItems
            | TextureNPC       | TextureOther        | TextureParty       | TexturePlaceables
            | TexturePlanetary | TexturePlayerBodies | TexturePlayerHeads | TextureStunt
            | TextureVehicles  | TextureWeapons)
            != RandomizationLevel.None;
        #endregion Active Rando Properties
        #endregion Properties

        #region Public/Protected Methods
        /// <summary>
        /// Resets all randomization settings to the default value.
        /// </summary>
        public void ResetSettingsToDefault()
        {
            ResetGeneral();
            ResetAudio();
            ResetItems();
            ResetModels();
            ResetModules();
            ResetOther();
            ResetTables();
            ResetText();
            ResetTextures();
        }

        /// <summary>
        /// Randomization and spoiler creation delegate for BackgroundWorker DoWork event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void Randomizer_DoWork(object sender, DoWorkEventArgs e)
        {
            var bw = sender as BackgroundWorker;

            if (e.Argument is RandoArgs args)
            {
                // GamePath can't be empty.
                if (string.IsNullOrWhiteSpace(args.GamePath))
                {
                    throw new ArgumentException("Game path not given.", "RandoArgs.GamePath");
                }

                // Final check for already randomized game before randomizing.
                var paths = new KPaths(args.GamePath);
                if (File.Exists(paths.RANDOMIZED_LOG))
                {
                    throw new InvalidOperationException(Properties.Resources.AlreadyRandomized);
                }

                if (args.Seed < 0) args.Seed *= -1; // Seed must be non-negative.
                Randomize.SetSeed(args.Seed);       // Set the seed.

                // Perform randomization.
                DoRandomize(bw, paths);

                // If SpoilersPath is given, create spoiler logs.
                if (!string.IsNullOrWhiteSpace(args.SpoilersPath))
                {
                    DoSpoil(bw, args.SpoilersPath, args.Seed);
                }
            }
            else
            {
                // Invalid use of this method. Please define randomization arguments.
                throw new ArgumentNullException("argument", "RandoArgs must be given when randomizing.");
            }
        }

        /// <summary>
        /// Unrandomization delegate for BackgroundWorker DoWork event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void Unrandomize(object sender, DoWorkEventArgs e)
        {
            var bw = sender as BackgroundWorker;

            if (e.Argument is RandoArgs args)
            {
                // GamePath can't be empty.
                if (string.IsNullOrWhiteSpace(args.GamePath))
                    throw new ArgumentException("Game path not given.", "RandoArgs.GamePath");

                // Final check for already unrandomized game before unrandomizing.
                var paths = new KPaths(args.GamePath);
                if (!File.Exists(paths.RANDOMIZED_LOG))
                    throw new InvalidOperationException(Properties.Resources.ErrorNotRandomized);

                var stepSize = 100.0 / 8.0;  // Calculation: 100 / (# of things to restore)
                double progress = 0;

                try
                {
                    ReportProgress(bw, progress += stepSize, BusyState.Unrandomizing, message: $"... {Properties.Resources.UnrandomizingModules}");
                    paths.RestoreModulesDirectory();

                    ReportProgress(bw, progress += stepSize, BusyState.Unrandomizing, message: $"... {Properties.Resources.UnrandomizingLips}");
                    paths.RestoreLipsDirectory();

                    ReportProgress(bw, progress += stepSize, BusyState.Unrandomizing, message: $"... {Properties.Resources.UnrandomizingOverrides}");
                    paths.RestoreOverrideDirectory();

                    ReportProgress(bw, progress += stepSize, BusyState.Unrandomizing, message: $"... {Properties.Resources.UnrandomizingMusic}");
                    paths.RestoreMusicDirectory();

                    ReportProgress(bw, progress += stepSize, BusyState.Unrandomizing, message: $"... {Properties.Resources.UnrandomizingSounds}");
                    paths.RestoreSoundsDirectory();

                    ReportProgress(bw, progress += stepSize, BusyState.Unrandomizing, message: $"... {Properties.Resources.UnrandomizingTextures}");
                    paths.RestoreTexturePacksDirectory();

                    ReportProgress(bw, progress += stepSize, BusyState.Unrandomizing, message: $"... {Properties.Resources.UnrandomizingKeyTable}");
                    paths.RestoreChitinFile();

                    ReportProgress(bw, progress += stepSize, BusyState.Unrandomizing, message: $"... {Properties.Resources.UnrandomizingTLKFile}");
                    paths.RestoreDialogFile();

                    ReportProgress(bw, progress += stepSize, BusyState.Unrandomizing, message: $"... {Properties.Resources.TaskFinishing}");
                    File.Delete(paths.RANDOMIZED_LOG);

                    ResetStaticRandomizationClasses();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Exception caught during unrandomization. {ex.Message}", ex);
                }
            }
            else
            {
                // Invalid use of this method. Please define randomization arguments.
                throw new ArgumentNullException("argument", "RandoArgs must be given when unrandomizing.");
            }
        }

        /// <summary>
        /// Reads an xml preset file. This provides backwards compatibility.
        /// </summary>
        /// <param name="s"></param>
        protected override void ReadFromFile(string path)
        {
            ResetSettingsToDefault();

            var doc = XDocument.Load(path);
            var element = doc.Descendants(XML_GENERAL).FirstOrDefault();
            if (element != null) ReadGeneralSettings(element);

            element = doc.Descendants(XML_AUDIO).FirstOrDefault();
            if (element != null) ReadAudioSettings(element);

            element = doc.Descendants(XML_ITEM).FirstOrDefault();
            if (element != null) ReadItemSettings(element);

            element = doc.Descendants(XML_MODEL).FirstOrDefault();
            if (element != null) ReadModelSettings(element);

            element = doc.Descendants(XML_MODULE).FirstOrDefault();
            if (element != null) ReadModuleSettings(element);

            element = doc.Descendants(XML_OTHER).FirstOrDefault();
            if (element != null) ReadOtherSettings(element);

            element = doc.Descendants(XML_TABLES).FirstOrDefault();
            if (element != null) ReadTableSettings(element);

            element = doc.Descendants(XML_TEXT).FirstOrDefault();
            if (element != null) ReadTextSettings(element);

            element = doc.Descendants(XML_TEXTURE).FirstOrDefault();
            if (element != null) ReadTextureSettings(element);
        }

        /// <summary>
        /// Writes an xml preset file. This provides backwards compatibility.
        /// </summary>
        /// <param name="s"></param>
        protected override void WriteToFile(string path)
        {
            using (var w = new XmlTextWriter(path, null))
            {
                w.WriteStartDocument();
                w.WriteStartElement(XML_SETTINGS); // Begin Settings

                WriteGeneralSettings(w);

                if (DoRandomizeAudio   ) WriteAudioSettings(w);
                if (DoRandomizeItems   ) WriteItemSettings(w);
                if (DoRandomizeModels  ) WriteModelSettings(w);
                if (DoRandomizeModules ) WriteModuleSettings(w);
                if (DoRandomizeOther   ) WriteOtherSettings(w);
                //if (DoRandomizeParty   ) WritePartySettings(w);   // Not yet implemented.
                if (DoRandomizeTables  ) WriteTableSettings(w);
                if (DoRandomizeText    ) WriteTextSettings(w);
                if (DoRandomizeTextures) WriteTextureSettings(w);

                w.WriteEndElement();                // End Settings
                w.WriteEndDocument();
                w.Flush();
            }
        }

        /// <summary>
        /// Reads a KRP file using the old, compact format.
        /// </summary>
        /// <param name="s"></param>
        protected override void ReadKRP(Stream s)
        {
            if (KRP.ReadKRP(s)) // If read KRP is successful ...
            {
                var doRandoModule  = Properties.Settings.Default.DoRandomization_Module;
                var doRandoItem    = Properties.Settings.Default.DoRandomization_Item;
                var doRandoAudio   = Properties.Settings.Default.DoRandomization_Sound;
                var doRandoModel   = Properties.Settings.Default.DoRandomization_Model;
                var doRandoTexture = Properties.Settings.Default.DoRandomization_Texture;
                var doRandoTwoDA   = Properties.Settings.Default.DoRandomization_TwoDA;
                var doRandoText    = Properties.Settings.Default.DoRandomization_Text;
                var doRandoOther   = Properties.Settings.Default.DoRandomization_Other;

                // Categories
                if (doRandoModule)  ReadKRPModules();
                else                ResetModules();

                if (doRandoItem)    ReadKRPItems();
                else                ResetItems();

                if (doRandoAudio)   ReadKRPAudio();
                else                ResetAudio();

                if (doRandoModel)   ReadKRPModels();
                else                ResetModels();

                if (doRandoTexture) ReadKRPTextures();
                else                ResetTextures();

                if (doRandoTwoDA)   ReadKRPTwoDAs();
                else                ResetTables();

                if (doRandoText)    ReadKRPText();
                else                ResetText();

                if (doRandoOther)   ReadKRPOther();
                else                ResetOther();
            }
            else
            {
                // KRP version didn't match the file. Exception thrown will be caught above.
            }
        }
        #endregion Public/Protected Methods

        #region Private Methods
        /// <summary>
        /// Counts the number of active randomization categories.
        /// </summary>
        private int CountActiveCategories()
        {
            int count = 0;
            if (DoRandomizeAudio    ) count++;
            if (DoRandomizeItems    ) count++;
            if (DoRandomizeModels   ) count++;
            if (DoRandomizeModules  ) count++;
            if (DoRandomizeOther    ) count++;
            //if (DoRandomizeParty    ) count++;
            if (DoRandomizeText     ) count++;
            if (DoRandomizeTextures ) count++;
            if (DoRandomizeTables   ) count++;
            return count;
        }

        /// <summary>
        /// Run randomization methods as needed while reporting progress to the BackgroundWorker.
        /// </summary>
        /// <param name="bw"></param>
        /// <param name="paths"></param>
        private void DoRandomize(BackgroundWorker bw, KPaths paths)
        {
            // Determine step size and throw error if no categories are selected.
            if (CountActiveCategories() == 0) { throw new InvalidOperationException(Properties.Resources.ErrorNoRandomization); }
            BackUpGame(bw, paths);
            RandomizeGame(bw, paths);
        }

        private void BackUpGame(BackgroundWorker bw, KPaths paths)
        {
            int activeCategories = CountActiveCategories() + 2; // Add one for overrides and one for finishing up.
            double stepSize = 100.0 / activeCategories;  // Calculate step size.
            double progress = 0 - stepSize;

            try
            {
                ReportProgress(bw, progress, BusyState.BackingUp, message: "Backing up game files.");

                // Backup each category being randomized.
                string form = "... backing up {0}.";

                // Back up current override files.
                ReportProgress(bw, progress += stepSize, BusyState.BackingUp, CATEGORY_OVERRIDES, string.Format(form, CATEGORY_OVERRIDES));
                paths.BackUpOverrideDirectory();

                if (DoRandomizeModules)     // Back up Modules
                {
                    ReportProgress(bw, progress += stepSize, BusyState.BackingUp, CATEGORY_MODULES, string.Format(form, CATEGORY_MODULES));
                    ModuleRando.CreateBackups(paths);
                }

                if (DoRandomizeItems)       // Back up Items
                {
                    ReportProgress(bw, progress += stepSize, BusyState.BackingUp, CATEGORY_ITEMS, string.Format(form, CATEGORY_ITEMS));
                    ItemRando.CreateItemBackups(paths);
                }

                if (DoRandomizeAudio)       // Back up Audio
                {
                    ReportProgress(bw, progress += stepSize, BusyState.BackingUp, CATEGORY_AUDIO, string.Format(form, CATEGORY_AUDIO));
                    if (DoRandomizeMusic) { SoundRando.CreateMusicBackups(paths); }
                    if (DoRandomizeSound) { SoundRando.CreateSoundBackups(paths); }
                }

                if (DoRandomizeModels)      // Back up Models (Cosmetics)
                {
                    ReportProgress(bw, progress += stepSize, BusyState.BackingUp, CATEGORY_MODELS, string.Format(form, CATEGORY_MODELS));
                    ModelRando.CreateModelBackups(paths);
                }

                if (DoRandomizeTextures)    // Back up Textures (Cosmetics)
                {
                    ReportProgress(bw, progress += stepSize, BusyState.BackingUp, CATEGORY_TEXTURES, string.Format(form, CATEGORY_TEXTURES));
                    TextureRando.CreateTextureBackups(paths);
                }

                if (DoRandomizeTables)      // Back up Tables
                {
                    ReportProgress(bw, progress += stepSize, BusyState.BackingUp, CATEGORY_TABLES, string.Format(form, CATEGORY_TABLES));
                    TwodaRandom.CreateTwoDABackups(paths);
                }

                if (DoRandomizeText)        // Back up Text
                {
                    ReportProgress(bw, progress += stepSize, BusyState.BackingUp, CATEGORY_TEXT, string.Format(form, CATEGORY_TEXT));
                    TextRando.CreateTextBackups(paths);
                }

                if (DoRandomizeOther)       // Back up Other
                {
                    ReportProgress(bw, progress += stepSize, BusyState.BackingUp, CATEGORY_OTHER, string.Format(form, CATEGORY_OTHER));
                    OtherRando.CreateOtherBackups(paths);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error encountered during backup: {ex.Message}", ex);
            }
            finally
            {
                ReportProgress(bw, 100, BusyState.BackingUp);
            }
        }

        private void RandomizeGame(BackgroundWorker bw, KPaths paths)
        {
            var activeCategories = CountActiveCategories() + 2; // Add one for overrides and one for finishing up.
            var stepSize = 100.0 / activeCategories;  // Calculate step size.
            var progress = 0.0 - stepSize;

            // Begin randomization process.
            using (var sw = new StreamWriter(paths.RANDOMIZED_LOG))
            {
                sw.WriteLine(DateTime.Now.ToString());
                sw.WriteLine(Properties.Resources.LogHeader);
                ResetStaticRandomizationClasses();

                try
                {
                    ReportProgress(bw, progress, BusyState.Randomizing, message: "Randomizing game files.");

                    // Write general override files.
                    ReportProgress(bw, progress += stepSize, BusyState.Randomizing, CATEGORY_OVERRIDES, "... writing Override files.");
                    File.WriteAllBytes(Path.Combine(paths.Override, "k_pdan_13_area.ncs"), Properties.Resources.k_pdan_13_area);

                    // Write appearance override.
                    if (GeneralModuleExtrasValue.HasFlag(ModuleExtras.FastEnvirosuit))
                    {
                        File.WriteAllBytes(Path.Combine(paths.Override, "appearance.2da"), Properties.Resources.appearance_speedysuit);
                    }
                    else
                    {
                        File.WriteAllBytes(Path.Combine(paths.Override, "appearance.2da"), Properties.Resources.appearance);
                    }

                    // Write early T3 override.
                    if (GeneralModuleExtrasValue.HasFlag(ModuleExtras.EarlyT3))
                    {
                        File.WriteAllBytes(Path.Combine(paths.Override, "tar02_janice021.dlg"), Properties.Resources.tar02_janice021);
                    }

                    // Perform category-based randomization.
                    var form = "... randomizing {0}.";

                    if (DoRandomizeModules)     // Randomize Modules
                    {
                        Randomize.RestartRng();
                        ReportProgress(bw, progress += stepSize, BusyState.Randomizing, CATEGORY_MODULES, string.Format(form, CATEGORY_MODULES));
                        ModuleRando.Module_rando(paths, this);
                        sw.WriteLine(Properties.Resources.LogModulesDone);
                    }

                    if (DoRandomizeItems)       // Randomize Items
                    {
                        Randomize.RestartRng();
                        ReportProgress(bw, progress += stepSize, BusyState.Randomizing, CATEGORY_ITEMS, string.Format(form, CATEGORY_ITEMS));
                        ItemRando.item_rando(paths, this);
                        sw.WriteLine(Properties.Resources.LogItemsDone);
                    }

                    if (DoRandomizeAudio)       // Randomize Audio
                    {
                        Randomize.RestartRng();
                        ReportProgress(bw, progress += stepSize, BusyState.Randomizing, CATEGORY_AUDIO, string.Format(form, CATEGORY_AUDIO));
                        SoundRando.sound_rando(paths, this);
                        sw.WriteLine(Properties.Resources.LogMusicSoundDone);
                    }

                    if (DoRandomizeModels)      // Randomize Cosmetics (Models)
                    {
                        Randomize.RestartRng();
                        ReportProgress(bw, progress += stepSize, BusyState.Randomizing, CATEGORY_MODELS, string.Format(form, CATEGORY_MODELS));
                        ModelRando.model_rando(paths, this);
                        sw.WriteLine(Properties.Resources.LogItemsDone);
                    }

                    if (DoRandomizeTextures)    // Randomize Cosmetics (Textures)
                    {
                        Randomize.RestartRng();
                        ReportProgress(bw, progress += stepSize, BusyState.Randomizing, CATEGORY_TEXTURES, string.Format(form, CATEGORY_TEXTURES));
                        TextureRando.texture_rando(paths, this);
                        sw.WriteLine(Properties.Resources.LogTexturesDone);
                    }

                    if (DoRandomizeTables)      // Randomize Tables
                    {
                        Randomize.RestartRng();
                        ReportProgress(bw, progress += stepSize, BusyState.Randomizing, CATEGORY_TABLES, string.Format(form, CATEGORY_TABLES));
                        TwodaRandom.Twoda_rando(paths, this);
                        sw.WriteLine(Properties.Resources.Log2DADone);
                    }

                    if (DoRandomizeText)        // Randomize Text
                    {
                        Randomize.RestartRng();
                        ReportProgress(bw, progress += stepSize, BusyState.Randomizing, CATEGORY_TEXT, string.Format(form, CATEGORY_TEXT));
                        TextRando.text_rando(paths, this);
                        sw.WriteLine(Properties.Resources.LogTextDone);
                    }

                    if (DoRandomizeOther)       // Randomize Other
                    {
                        Randomize.RestartRng();
                        ReportProgress(bw, progress += stepSize, BusyState.Randomizing, CATEGORY_OTHER, string.Format(form, CATEGORY_OTHER));
                        OtherRando.other_rando(paths, this);
                        sw.WriteLine(Properties.Resources.LogOtherDone);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error encountered during randomization: {ex.Message}", ex);
                }
                finally
                {
                    ReportProgress(bw, 100, BusyState.Randomizing, message: Properties.Resources.TaskFinishing);
                    sw.WriteLine("\nThe Kotor Randomizer was created by Lane Dibello and Glasnonck, with help from the greater Kotor Speedrunning community.");
                    sw.WriteLine("If you encounter any issues please try to contact me @Lane#5847 on Discord");
                }
            }
        }

        /// <summary>
        /// Run spoiler creation methods as needed while reporting progress to the BackgroundWorker.
        /// </summary>
        /// <param name="bw"></param>
        /// <param name="spoilersDirectory"></param>
        private void DoSpoil(BackgroundWorker bw, string spoilersDirectory, int seed)
        {
            double progress = 0;
            var stepSize = 100.0 / (CountActiveCategories() + 2);    // Active categories + General + Saving

            var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");
            var filename = $"{timestamp}, Seed {Randomize.Seed}.xlsx";

            if (!Directory.Exists(spoilersDirectory))
            {
                var message = $"Spoilers directory \"{spoilersDirectory}\" doesn't exist. Spoiler will be saved at \"{Path.Combine(Environment.CurrentDirectory, filename)}\".";
                ReportProgress(bw, progress, BusyState.Spoiling, message: message);
                spoilersDirectory = Environment.CurrentDirectory;
            }

            var path = Path.Combine(spoilersDirectory, filename);
            if (File.Exists(path)) File.Delete(path);

            try
            {
                using (var workbook = new XLWorkbook())
                {
                    ReportProgress(bw, progress, BusyState.Spoiling, message: "Starting to write spoilers to log.");

                    var category = "General";
                    var spoilFormat = "... spoiling {0}.";

                    ReportProgress(bw, 0, BusyState.Spoiling, category, string.Format(spoilFormat, category));
                    ModuleRando.CreateGeneralSpoilerLog(workbook, seed);

                    if (DoRandomizeItems)
                    {
                        category = "Items";
                        ReportProgress(bw, progress += stepSize, BusyState.Spoiling, category, string.Format(spoilFormat, category));
                        ItemRando.CreateSpoilerLog(workbook);
                    }

                    if (DoRandomizeModels)
                    {
                        category = "Models";
                        ReportProgress(bw, progress += stepSize, BusyState.Spoiling, category, string.Format(spoilFormat, category));
                        ModelRando.CreateSpoilerLog(workbook);
                    }

                    if (DoRandomizeModules)
                    {
                        category = "Modules";
                        ReportProgress(bw, progress += stepSize, BusyState.Spoiling, category, string.Format(spoilFormat, category));
                        ModuleRando.CreateSpoilerLog(workbook, false);
                    }

                    if (DoRandomizeAudio)
                    {
                        category = "Audio";
                        ReportProgress(bw, progress += stepSize, BusyState.Spoiling, category, string.Format(spoilFormat, category));
                        SoundRando.CreateSpoilerLog(workbook);
                    }

                    if (DoRandomizeOther)
                    {
                        category = "Other";
                        ReportProgress(bw, progress += stepSize, BusyState.Spoiling, category, string.Format(spoilFormat, category));
                        OtherRando.CreateSpoilerLog(workbook);
                    }

                    if (DoRandomizeText)
                    {
                        category = "Text";
                        ReportProgress(bw, progress += stepSize, BusyState.Spoiling, category, string.Format(spoilFormat, category));
                        TextRando.CreateSpoilerLog(workbook);
                    }

                    if (DoRandomizeTextures)
                    {
                        category = "Textures";
                        ReportProgress(bw, progress += stepSize, BusyState.Spoiling, category, string.Format(spoilFormat, category));
                        TextureRando.CreateSpoilerLog(workbook);
                    }

                    if (DoRandomizeTables)
                    {
                        category = "Tables";
                        ReportProgress(bw, progress += stepSize, BusyState.Spoiling, category, string.Format(spoilFormat, category));
                        TwodaRandom.CreateSpoilerLog(workbook);
                    }

                    // If any worksheets have been added, save the spoiler log.
                    if (workbook.Worksheets.Count > 0)
                    {
                        ReportProgress(bw, progress += stepSize, BusyState.Spoiling, "Saving File", "... saving the spoiler log.");
                        workbook.SaveAs(path);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception caught while creating spoilers: {ex.Message}", ex);
            }
            finally
            {
                ReportProgress(bw, 100, BusyState.Spoiling, message: "Spoiler log created.");
            }
        }

        /// <summary>
        /// Updates the count of randomized tables whenever one of the tables is modified.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateRandomizedTableCount(object sender, PropertyChangedEventArgs e)
        {
            TableRandomizedCount = Table2DAs.Count(rt => rt.IsRandomized);
        }

        /// <summary>
        /// Reports progress of the current busy state to the BackgroundWorker.
        /// </summary>
        /// <param name="bw">BackgroundWorker to report progress to.</param>
        /// <param name="progress">Percent progress on the current action.</param>
        /// <param name="state">BusyState of the randomizer.</param>
        /// <param name="status">Text to display as the current status.</param>
        /// <param name="message">Text to write to the log.</param>
        private void ReportProgress(
            BackgroundWorker bw,
            double progress,
            BusyState state,
            string status = null,
            string message = null)
        {
            bw.ReportProgress(0, new RandoProgress()
            {
                State           = state,
                PercentComplete = progress,
                Status          = status,
                Log             = message,
            });
        }

        /// <summary>
        /// Converts the string representation of the name or numeric value of enumerated constants
        /// to an equivalent enumerated object of the provided enumeration type.
        /// </summary>
        /// <typeparam name="T">An enumeration type.</typeparam>
        /// <param name="value">A string containing the name or value to convert.</param>
        /// <returns>An object of type T whose value is represented by value.</returns>
        private static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value);
        }

        /// <summary>
        /// Reset static randomization classes for a new randomization.
        /// </summary>
        private void ResetStaticRandomizationClasses()
        {
            ModuleRando.Reset(this);
            ItemRando.Reset();
            SoundRando.Reset();
            ModelRando.Reset();
            TextureRando.Reset();
            TwodaRandom.Reset();
            TextRando.Reset();
            OtherRando.Reset();
        }

        /// <summary>
        /// Reset General settings to default.
        /// </summary>
        private void ResetGeneral()
        {
            GeneralModuleExtrasValue = ModuleExtras.Default;
            foreach (var door in GeneralUnlockedDoors)
                GeneralLockedDoors.Add(door);
            GeneralUnlockedDoors.Clear();
        }

        /// <summary>
        /// Reset Audio settings to default.
        /// </summary>
        private void ResetAudio()
        {
            AudioAmbientNoise         = RandomizationLevel.None;
            AudioAreaMusic            = RandomizationLevel.None;
            AudioBattleMusic          = RandomizationLevel.None;
            AudioCutsceneNoise        = RandomizationLevel.None;
            AudioNpcSounds            = RandomizationLevel.None;
            AudioPartySounds          = RandomizationLevel.None;
            AudioRemoveDmcaMusic      = false;
            AudioMixNpcAndPartySounds = false;
        }

        /// <summary>
        /// Reset Item settings to default.
        /// </summary>
        private void ResetItems()
        {
            ItemArmbands        = RandomizationLevel.None;
            ItemArmor           = RandomizationLevel.None;
            ItemBelts           = RandomizationLevel.None;
            ItemBlasters        = RandomizationLevel.None;
            ItemCreatureHides   = RandomizationLevel.None;
            ItemCreatureWeapons = RandomizationLevel.None;
            ItemDroidEquipment  = RandomizationLevel.None;
            ItemGloves          = RandomizationLevel.None;
            ItemGrenades        = RandomizationLevel.None;
            ItemImplants        = RandomizationLevel.None;
            ItemLightsabers     = RandomizationLevel.None;
            ItemMasks           = RandomizationLevel.None;
            ItemMeleeWeapons    = RandomizationLevel.None;
            ItemMines           = RandomizationLevel.None;
            ItemPazaakCards     = RandomizationLevel.None;
            ItemMedical         = RandomizationLevel.None;
            ItemUpgrades        = RandomizationLevel.None;
            ItemVarious         = RandomizationLevel.None;

            // Move all items to randomized list.
            foreach (var item in ItemOmittedList) ItemRandomizedList.Add(item);
            ItemOmittedList.Clear();

            // Grab omitted list from globals.
            var omitItems = ItemRandomizedList.Where(ri => Globals.OMIT_ITEM_PRESETS.First().Value.Contains(ri.Code)).ToList();
            foreach (var item in omitItems)
            {
                ItemOmittedList.Add(item);
                ItemRandomizedList.Remove(item);
            }

            ItemOmittedPreset = Globals.OMIT_ITEM_PRESETS.First().Key;
        }

        /// <summary>
        /// Reset Model settings to default.
        /// </summary>
        private void ResetModels()
        {
            ModelCharacterRando      = false;
            ModelCharacterOmitLarge  = true;
            ModelCharacterOmitBroken = true;

            ModelDoorRando           = false;
            ModelDoorOmitAirlock     = true;
            ModelDoorOmitBroken      = true;

            ModelPlaceableRando      = false;
            ModelPlaceableOmitLarge  = true;
            ModelPlaceableOmitBroken = true;
            ModelPlaceableEasyPanels = false;
        }

        /// <summary>
        /// Reset Module settings to default.
        /// </summary>
        private void ResetModules()
        {
            GeneralModuleExtrasValue = ModuleExtras.Default;

            foreach (var item in GeneralUnlockedDoors) GeneralLockedDoors.Add(item);
            GeneralUnlockedDoors.Clear();

            ModuleAllowGlitchClip      = false;
            ModuleAllowGlitchDlz       = false;
            ModuleAllowGlitchFlu       = false;
            ModuleAllowGlitchGpw       = false;
            ModuleLogicStrongGoals     = false;
            ModuleGoalIsMalak          = true;
            ModuleGoalIsPazaak         = false;
            ModuleGoalIsStarMap        = false;
            ModuleGoalIsFullParty      = false;
            ModuleLogicIgnoreOnceEdges = true;
            ModuleLogicRandoRules      = true;
            ModuleLogicReachability    = true;

            foreach (var item in ModuleRandomizedList) ModuleOmittedList.Add(item);
            ModuleRandomizedList.Clear();
            ModuleShufflePreset = Globals.OMIT_PRESETS.First().Key;
        }

        /// <summary>
        /// Reset Other settings to default.
        /// </summary>
        private void ResetOther()
        {
            OtherNameGeneration = false;
            OtherFirstNamesF    = string.Empty;
            OtherFirstNamesM    = string.Empty;
            OtherLastNames      = string.Empty;
            OtherPartyMembers   = false;
            OtherPazaakDecks    = false;
            OtherPolymorphMode  = false;
            OtherSwoopBoosters  = false;
            OtherSwoopObstacles = false;
        }

        /// <summary>
        /// Reset 2DA settings to default.
        /// </summary>
        private void ResetTables()
        {
            // Reset tables.
            foreach (var table in Table2DAs) table.Reset();

            // Reset animations.
            AnimationAttack = RandomizationLevel.None;
            AnimationDamage = RandomizationLevel.None;
            AnimationFire   = RandomizationLevel.None;
            AnimationLoop   = RandomizationLevel.None;
            AnimationMove   = RandomizationLevel.None;
            AnimationParry  = RandomizationLevel.None;
            AnimationPause  = RandomizationLevel.None;
        }

        /// <summary>
        /// Reset Text settings to default.
        /// </summary>
        private void ResetText()
        {
            TextSettingsValue = TextSettings.Default;
        }

        /// <summary>
        /// Reset Texture settings to default.
        /// </summary>
        private void ResetTextures()
        {
            TextureSelectedPack = TexturePack.HighQuality;

            TextureCreatures    = RandomizationLevel.None;
            TextureCubeMaps     = RandomizationLevel.None;
            TextureEffects      = RandomizationLevel.None;
            TextureItems        = RandomizationLevel.None;
            TextureNPC          = RandomizationLevel.None;
            TextureOther        = RandomizationLevel.None;
            TextureParty        = RandomizationLevel.None;
            TexturePlaceables   = RandomizationLevel.None;
            TexturePlanetary    = RandomizationLevel.None;
            TexturePlayerBodies = RandomizationLevel.None;
            TexturePlayerHeads  = RandomizationLevel.None;
            TextureStunt        = RandomizationLevel.None;
            TextureVehicles     = RandomizationLevel.None;
            TextureWeapons      = RandomizationLevel.None;
        }

        /// <summary>
        /// Read Module and General settings from newly read KRP.
        /// </summary>
        private void ReadKRPModules()
        {
            // Grab QoL from module extras.
            GeneralModuleExtrasValue = Properties.Settings.Default.ModuleExtrasValue & EXTRAS_MASK;

            // Move unlocks into the locked list.
            foreach (var item in GeneralUnlockedDoors) GeneralLockedDoors.Add(item);
            GeneralUnlockedDoors.Clear();

            // Grab unlocks from module extras.
            ModuleExtras unlocks = Properties.Settings.Default.ModuleExtrasValue & UNLOCKS_MASK;
            var toUnlock = GeneralLockedDoors.Where(du => unlocks.HasFlag(du.Tag)).ToList();
            foreach (var item in toUnlock)
            {
                GeneralUnlockedDoors.Add(item);
                GeneralLockedDoors.Remove(item);
            }

            // Read module settings.
            ModuleAllowGlitchClip      = Properties.Settings.Default.AllowGlitchClip;
            ModuleAllowGlitchDlz       = Properties.Settings.Default.AllowGlitchDlz;
            ModuleAllowGlitchFlu       = Properties.Settings.Default.AllowGlitchFlu;
            ModuleAllowGlitchGpw       = Properties.Settings.Default.AllowGlitchGpw;
            ModuleLogicStrongGoals     = false;
            ModuleGoalIsMalak          = Properties.Settings.Default.GoalIsMalak;
            ModuleGoalIsPazaak         = Properties.Settings.Default.GoalIsPazaak;
            ModuleGoalIsStarMap        = Properties.Settings.Default.GoalIsStarMaps;
            ModuleGoalIsFullParty      = false;
            ModuleLogicIgnoreOnceEdges = Properties.Settings.Default.IgnoreOnceEdges;
            ModuleLogicRandoRules      = Properties.Settings.Default.UseRandoRules;
            ModuleLogicReachability    = Properties.Settings.Default.VerifyReachability;

            ModuleShufflePreset = null;

            // Move all modules to randomized.
            foreach (var item in ModuleOmittedList) ModuleRandomizedList.Add(item);
            ModuleOmittedList.Clear();

            // Find omitted modules and move them to omitted list.
            var omittedCodes = Globals.BoundModules.Where(me => me.Omitted == true).Select(me => me.Code);
            var omittedMods = ModuleRandomizedList.Where(mv => omittedCodes.Contains(mv.WarpCode));
            foreach (var item in omittedMods)
            {
                ModuleOmittedList.Add(item);
                ModuleRandomizedList.Remove(item);
            }
        }

        /// <summary>
        /// Read Item settings from newly read KRP.
        /// </summary>
        private void ReadKRPItems()
        {
            ItemOmittedPreset = null;

            ItemArmbands        = Properties.Settings.Default.RandomizeArmbands;
            ItemArmor           = Properties.Settings.Default.RandomizeArmor;
            ItemBelts           = Properties.Settings.Default.RandomizeBelts;
            ItemBlasters        = Properties.Settings.Default.RandomizeBlasters;
            ItemCreatureHides   = Properties.Settings.Default.RandomizeHides;
            ItemCreatureWeapons = Properties.Settings.Default.RandomizeCreature;
            ItemDroidEquipment  = Properties.Settings.Default.RandomizeDroid;
            ItemGloves          = Properties.Settings.Default.RandomizeGloves;
            ItemGrenades        = Properties.Settings.Default.RandomizeGrenades;
            ItemImplants        = Properties.Settings.Default.RandomizeImplants;
            ItemLightsabers     = Properties.Settings.Default.RandomizeLightsabers;
            ItemMasks           = Properties.Settings.Default.RandomizeMask;
            ItemMeleeWeapons    = Properties.Settings.Default.RandomizeMelee;
            ItemMines           = Properties.Settings.Default.RandomizeMines;
            ItemPazaakCards     = Properties.Settings.Default.RandomizePaz;
            ItemMedical         = Properties.Settings.Default.RandomizeStims;
            ItemUpgrades        = Properties.Settings.Default.RandomizeUpgrade;
            ItemVarious         = Properties.Settings.Default.RandomizeVarious;

            // Move all items to randomized list.
            foreach (var item in ItemOmittedList) ItemRandomizedList.Add(item);
            ItemOmittedList.Clear();

            // Grab omitted list from globals.
            var omitItems = ItemRandomizedList.Where(ri => Globals.OmitItems.Contains(ri.Code)).ToList();
            foreach (var item in omitItems)
            {
                ItemOmittedList.Add(item);
                ItemRandomizedList.Remove(item);
            }
        }

        /// <summary>
        /// Read Audio settings from newly read KRP.
        /// </summary>
        private void ReadKRPAudio()
        {
            AudioAmbientNoise         = Properties.Settings.Default.RandomizeAmbientNoise;
            AudioAreaMusic            = Properties.Settings.Default.RandomizeAreaMusic;
            AudioBattleMusic          = Properties.Settings.Default.RandomizeBattleMusic;
            AudioCutsceneNoise        = Properties.Settings.Default.RandomizeCutsceneNoise;
            AudioNpcSounds            = Properties.Settings.Default.RandomizeNpcSounds;
            AudioPartySounds          = Properties.Settings.Default.RandomizePartySounds;
            AudioRemoveDmcaMusic      = Properties.Settings.Default.RemoveDmcaMusic;
            AudioMixNpcAndPartySounds = Properties.Settings.Default.MixNpcAndPartySounds;
        }

        /// <summary>
        /// Read Model settings from newly read KRP.
        /// </summary>
        private void ReadKRPModels()
        {
            ModelCharacterRando      = (Properties.Settings.Default.RandomizeCharModels & 1) > 0;
            ModelCharacterOmitLarge  = (Properties.Settings.Default.RandomizeCharModels & 2) > 0;
            ModelCharacterOmitBroken = (Properties.Settings.Default.RandomizeCharModels & 4) > 0;

            ModelDoorRando           = (Properties.Settings.Default.RandomizeDoorModels & 1) > 0;
            ModelDoorOmitAirlock     = (Properties.Settings.Default.RandomizeDoorModels & 2) > 0;
            ModelDoorOmitBroken      = (Properties.Settings.Default.RandomizeDoorModels & 4) > 0;

            ModelPlaceableRando      = (Properties.Settings.Default.RandomizePlaceModels & 1) > 0;
            ModelPlaceableOmitLarge  = (Properties.Settings.Default.RandomizePlaceModels & 2) > 0;
            ModelPlaceableOmitBroken = (Properties.Settings.Default.RandomizePlaceModels & 4) > 0;
            ModelPlaceableEasyPanels = (Properties.Settings.Default.RandomizePlaceModels & 8) > 0;
        }

        /// <summary>
        /// Read Texture settings from newly read KRP.
        /// </summary>
        private void ReadKRPTextures()
        {
            TextureSelectedPack = Properties.Settings.Default.TexturePack;

            TextureCreatures    = Properties.Settings.Default.TextureRandomizeCreatures;
            TextureCubeMaps     = Properties.Settings.Default.TextureRandomizeCubeMaps;
            TextureEffects      = Properties.Settings.Default.TextureRandomizeEffects;
            TextureItems        = Properties.Settings.Default.TextureRandomizeItems;
            TextureNPC          = Properties.Settings.Default.TextureRandomizeNPC;
            TextureOther        = Properties.Settings.Default.TextureRandomizeOther;
            TextureParty        = Properties.Settings.Default.TextureRandomizeParty;
            TexturePlaceables   = Properties.Settings.Default.TextureRandomizePlaceables;
            TexturePlanetary    = Properties.Settings.Default.TextureRandomizePlanetary;
            TexturePlayerBodies = Properties.Settings.Default.TextureRandomizePlayBodies;
            TexturePlayerHeads  = Properties.Settings.Default.TextureRandomizePlayHeads;
            TextureStunt        = Properties.Settings.Default.TextureRandomizeStunt;
            TextureVehicles     = Properties.Settings.Default.TextureRandomizeVehicles;
            TextureWeapons      = Properties.Settings.Default.TextureRandomizeWeapons;
        }

        /// <summary>
        /// Read 2DA settings from newly read KRP.
        /// </summary>
        private void ReadKRPTwoDAs()
        {
            ResetTables();  // Reset columns to default prior to reading new settings.
            foreach (var kvp in Globals.Selected2DAs)
            {
                // Find the selected table.
                var table = Table2DAs.First(rt => rt.Name == kvp.Key);

                // For each selected column, set it to be randomized.
                foreach (var col in kvp.Value)
                {
                    table.Randomized.Add(col);
                    table.Columns.Remove(col);
                }
            }
        }

        /// <summary>
        /// Read Text settings from newly read KRP.
        /// </summary>
        private void ReadKRPText()
        {
            TextSettingsValue = Properties.Settings.Default.TextSettingsValue;
        }

        /// <summary>
        /// Read Other settings from newly read KRP.
        /// </summary>
        private void ReadKRPOther()
        {
            OtherNameGeneration = Properties.Settings.Default.RandomizeNameGen;
            if (OtherNameGeneration)
            {
                OtherFirstNamesF = string.Join(Environment.NewLine, Properties.Settings.Default.FirstnamesF.OfType<string>());
                OtherFirstNamesM = string.Join(Environment.NewLine, Properties.Settings.Default.FirstnamesM.OfType<string>());
                OtherLastNames   = string.Join(Environment.NewLine, Properties.Settings.Default.Lastnames.OfType<string>());
            }
            else
            {
                OtherFirstNamesF = string.Empty;
                OtherFirstNamesM = string.Empty;
                OtherLastNames   = string.Empty;
            }

            OtherPartyMembers   = Properties.Settings.Default.RandomizePartyMembers;
            OtherPazaakDecks    = Properties.Settings.Default.RandomizePazaakDecks;
            OtherPolymorphMode  = Properties.Settings.Default.PolymorphMode;
            OtherSwoopBoosters  = Properties.Settings.Default.RandomizeSwoopBoosters;
            OtherSwoopObstacles = Properties.Settings.Default.RandomizeSwoopObstacles;
        }

        /// <summary>
        /// Read general settings from an XML file.
        /// </summary>
        /// <param name="element">XML element containing the general settings.</param>
        private void ReadGeneralSettings(XElement element)
        {   // Inside General
            { if (element.Attribute(XML_QOL    ) is XAttribute attr) GeneralModuleExtrasValue = ParseEnum<ModuleExtras>(attr.Value); }

            var unlocks = ModuleExtras.Default;
            { if (element.Attribute(XML_UNLOCKS) is XAttribute attr) unlocks = ParseEnum<ModuleExtras>(attr.Value); }

            foreach (var door in GeneralLockedDoors.ToList())
            {
                if (unlocks.HasFlag(door.Tag))
                {
                    GeneralUnlockedDoors.Add(door);
                    GeneralLockedDoors.Remove(door);
                }
            }
        }

        /// <summary>
        /// Read audio settings from an XML file.
        /// </summary>
        /// <param name="element">XML element containing the audio settings.</param>
        private void ReadAudioSettings(XElement element)
        {
            // Read audio settings.
            { if (element.Attribute(XML_AMBIENT    ) is XAttribute attr) AudioAmbientNoise         = ParseEnum<RandomizationLevel>(attr.Value); }
            { if (element.Attribute(XML_AREA       ) is XAttribute attr) AudioAreaMusic            = ParseEnum<RandomizationLevel>(attr.Value); }
            { if (element.Attribute(XML_BATTLE     ) is XAttribute attr) AudioBattleMusic          = ParseEnum<RandomizationLevel>(attr.Value); }
            { if (element.Attribute(XML_CUTSCENE   ) is XAttribute attr) AudioCutsceneNoise        = ParseEnum<RandomizationLevel>(attr.Value); }
            { if (element.Attribute(XML_NPC        ) is XAttribute attr) AudioNpcSounds            = ParseEnum<RandomizationLevel>(attr.Value); }
            { if (element.Attribute(XML_PARTY      ) is XAttribute attr) AudioPartySounds          = ParseEnum<RandomizationLevel>(attr.Value); }
            { if (element.Attribute(XML_MIXNPCPARTY) is XAttribute attr) AudioMixNpcAndPartySounds = bool.Parse(attr.Value); }
            { if (element.Attribute(XML_REMOVE_DMCA) is XAttribute attr) AudioRemoveDmcaMusic      = bool.Parse(attr.Value); }
        }

        /// <summary>
        /// Read item settings from an XML file.
        /// </summary>
        /// <param name="element">XML element containing the item settings.</param>
        private void ReadItemSettings(XElement element)
        {
            // Read item settings. Note: we can assume element is not null.
            { if (element.Attribute(XML_ARMBAND   ) is XAttribute attr) ItemArmbands        = ParseEnum<RandomizationLevel>(attr.Value); }
            { if (element.Attribute(XML_ARMOR     ) is XAttribute attr) ItemArmor           = ParseEnum<RandomizationLevel>(attr.Value); }
            { if (element.Attribute(XML_BELT      ) is XAttribute attr) ItemBelts           = ParseEnum<RandomizationLevel>(attr.Value); }
            { if (element.Attribute(XML_BLASTER   ) is XAttribute attr) ItemBlasters        = ParseEnum<RandomizationLevel>(attr.Value); }
            { if (element.Attribute(XML_CHIDE     ) is XAttribute attr) ItemCreatureHides   = ParseEnum<RandomizationLevel>(attr.Value); }
            { if (element.Attribute(XML_CWEAPON   ) is XAttribute attr) ItemCreatureWeapons = ParseEnum<RandomizationLevel>(attr.Value); }
            { if (element.Attribute(XML_DROID     ) is XAttribute attr) ItemDroidEquipment  = ParseEnum<RandomizationLevel>(attr.Value); }
            { if (element.Attribute(XML_GLOVE     ) is XAttribute attr) ItemGloves          = ParseEnum<RandomizationLevel>(attr.Value); }
            { if (element.Attribute(XML_GRENADE   ) is XAttribute attr) ItemGrenades        = ParseEnum<RandomizationLevel>(attr.Value); }
            { if (element.Attribute(XML_IMPLANT   ) is XAttribute attr) ItemImplants        = ParseEnum<RandomizationLevel>(attr.Value); }
            { if (element.Attribute(XML_LIGHTSABER) is XAttribute attr) ItemLightsabers     = ParseEnum<RandomizationLevel>(attr.Value); }
            { if (element.Attribute(XML_MASK      ) is XAttribute attr) ItemMasks           = ParseEnum<RandomizationLevel>(attr.Value); }
            { if (element.Attribute(XML_MELEE     ) is XAttribute attr) ItemMeleeWeapons    = ParseEnum<RandomizationLevel>(attr.Value); }
            { if (element.Attribute(XML_MINE      ) is XAttribute attr) ItemMines           = ParseEnum<RandomizationLevel>(attr.Value); }
            { if (element.Attribute(XML_MEDICAL   ) is XAttribute attr) ItemMedical         = ParseEnum<RandomizationLevel>(attr.Value); }
            { if (element.Attribute(XML_PAZAAK    ) is XAttribute attr) ItemPazaakCards     = ParseEnum<RandomizationLevel>(attr.Value); }
            { if (element.Attribute(XML_UPGRADE   ) is XAttribute attr) ItemUpgrades        = ParseEnum<RandomizationLevel>(attr.Value); }
            { if (element.Attribute(XML_VARIOUS   ) is XAttribute attr) ItemVarious         = ParseEnum<RandomizationLevel>(attr.Value); }

            // Reset omitted items list. -- May no longer be necessary.
            foreach (var item in ItemOmittedList)
                ItemRandomizedList.Add(item);
            ItemOmittedList.Clear();

            // Read omitted item preset.
            //var omit = element.Descendants(XML_OMIT).FirstOrDefault();
            ItemOmittedPreset = element.Attribute(XML_PRESET)?.Value ?? null;

            // If preset is null, read the list of omitted items.
            if (ItemOmittedPreset is null)
            {
                foreach (var i in element.Descendants(XML_OMIT))
                {
                    var code = i.Attribute(XML_CODE).Value;
                    var item = ItemRandomizedList.FirstOrDefault(x => x.Code == code);
                    if (item != null)
                    {
                        ItemOmittedList.Add(item);
                        ItemRandomizedList.Remove(item);
                    }
                }
            }
        }

        /// <summary>
        /// Read model settings from an XML file.
        /// </summary>
        /// <param name="element">XML element containing the model settings.</param>
        private void ReadModelSettings(XElement element)
        {
            // Read character settings.
            if (element.Descendants(XML_CHAR).FirstOrDefault() is XElement charElement)
            {
                ModelCharacterRando = true;
                { if (charElement.Attribute(XML_OMIT_LARGE ) is XAttribute attr) ModelCharacterOmitLarge  = bool.Parse(attr.Value); }
                { if (charElement.Attribute(XML_OMIT_BROKEN) is XAttribute attr) ModelCharacterOmitBroken = bool.Parse(attr.Value); }
            }

            // Read door settings.
            if (element.Descendants(XML_DOOR).FirstOrDefault() is XElement doorElement)
            {
                ModelDoorRando = true;
                { if (doorElement.Attribute(XML_OMIT_AIRLOCK) is XAttribute attr) ModelDoorOmitAirlock = bool.Parse(attr.Value); }
                { if (doorElement.Attribute(XML_OMIT_BROKEN ) is XAttribute attr) ModelDoorOmitBroken  = bool.Parse(attr.Value); }
            }

            // Read placeable settings.
            if (element.Descendants(XML_PLAC).FirstOrDefault() is XElement placElement)
            {
                ModelPlaceableRando = true;
                { if (placElement.Attribute(XML_OMIT_LARGE ) is XAttribute attr) ModelPlaceableOmitLarge  = bool.Parse(attr.Value); }
                { if (placElement.Attribute(XML_OMIT_BROKEN) is XAttribute attr) ModelPlaceableOmitBroken = bool.Parse(attr.Value); }
                { if (placElement.Attribute(XML_EASY_PANELS) is XAttribute attr) ModelPlaceableEasyPanels = bool.Parse(attr.Value); }
            }
        }

        /// <summary>
        /// Read module settings from an XML file.
        /// </summary>
        /// <param name="element">XML element containing the module settings.</param>
        private void ReadModuleSettings(XElement element)
        {
            // Read glitch settings.
            if (element.Descendants(XML_GLITCHES).FirstOrDefault() is XElement glitches)
            {
                { if (glitches.Attribute(XML_CLIP) is XAttribute attr) ModuleAllowGlitchClip = bool.Parse(attr.Value); }
                { if (glitches.Attribute(XML_DLZ ) is XAttribute attr) ModuleAllowGlitchDlz  = bool.Parse(attr.Value); }
                { if (glitches.Attribute(XML_FLU ) is XAttribute attr) ModuleAllowGlitchFlu  = bool.Parse(attr.Value); }
                { if (glitches.Attribute(XML_GPW ) is XAttribute attr) ModuleAllowGlitchGpw  = bool.Parse(attr.Value); }
            }

            // Read goal settings.
            if (element.Descendants(XML_GOALS).FirstOrDefault() is XElement goals)
            {
                { if (goals.Attribute(XML_MALAK ) is XAttribute attr) ModuleGoalIsMalak     = bool.Parse(attr.Value); }
                { if (goals.Attribute(XML_MAPS  ) is XAttribute attr) ModuleGoalIsStarMap   = bool.Parse(attr.Value); }
                { if (goals.Attribute(XML_PAZAAK) is XAttribute attr) ModuleGoalIsPazaak    = bool.Parse(attr.Value); }
                { if (goals.Attribute(XML_PARTY ) is XAttribute attr) ModuleGoalIsFullParty = bool.Parse(attr.Value); }
            }

            // Read logic settings.
            if (element.Descendants(XML_LOGIC).FirstOrDefault() is XElement logic)
            {
                { if (logic.Attribute(XML_RULES       ) is XAttribute attr) ModuleLogicRandoRules      = bool.Parse(attr.Value); }
                { if (logic.Attribute(XML_REACHABLE   ) is XAttribute attr) ModuleLogicReachability    = bool.Parse(attr.Value); }
                { if (logic.Attribute(XML_IGNORE_ONCE ) is XAttribute attr) ModuleLogicIgnoreOnceEdges = bool.Parse(attr.Value); }
                { if (logic.Attribute(XML_STRONG_GOALS) is XAttribute attr) ModuleLogicStrongGoals     = bool.Parse(attr.Value); }
            }

            // Reset omitted module list.
            foreach (var module in ModuleOmittedList)
                ModuleRandomizedList.Add(module);
            ModuleOmittedList.Clear();

            // Read shuffle preset.
            var omit = element.Descendants(XML_OMIT).FirstOrDefault();
            ModuleShufflePreset = omit.Attribute(XML_PRESET)?.Value ?? null;

            // If no shuffle preset, read omitted modules list.
            if (ModuleShufflePreset is null)
            {
                foreach (var mod in element.Descendants(XML_MODULE))
                {
                    var module = ModuleRandomizedList.FirstOrDefault(x => x.WarpCode == mod.Attribute(XML_CODE)?.Value);
                    if (module != null)
                    {
                        ModuleOmittedList.Add(module);
                        ModuleRandomizedList.Remove(module);
                    }
                }
            }
        }

        /// <summary>
        /// Read other settings from an XML file.
        /// </summary>
        /// <param name="element">XML element containing the other settings.</param>
        private void ReadOtherSettings(XElement element)
        {
            // If these boolean attributes exist, their settings are enabled (true).
            OtherPartyMembers   = element.Attribute(XML_PARTY         ) != null;
            OtherPazaakDecks    = element.Attribute(XML_PAZAAK        ) != null;
            OtherPolymorphMode  = element.Attribute(XML_POLYMORPH     ) != null;
            OtherSwoopBoosters  = element.Attribute(XML_SWOOP_BOOSTERS) != null;
            OtherSwoopObstacles = element.Attribute(XML_SWOOP_OBSTACLE) != null;

            var names = element.Descendants(XML_NAMES).FirstOrDefault();
            if (names != null)
            {
                OtherNameGeneration = true;

                // Female First Names
                StringBuilder sb = new StringBuilder();
                foreach (var name in names.Descendants(XML_FIRST_NAME_F))
                {
                    sb.AppendLine(name.Value);
                }
                OtherFirstNamesF = sb.ToString().TrimEnd("\r\n".ToCharArray());

                // Male First Names
                sb.Clear();
                foreach (var name in names.Descendants(XML_FIRST_NAME_M))
                {
                    sb.AppendLine(name.Value);
                }
                OtherFirstNamesM = sb.ToString().TrimEnd("\r\n".ToCharArray());

                // Last Names
                sb.Clear();
                foreach (var name in names.Descendants(XML_LAST_NAME))
                {
                    sb.AppendLine(name.Value);
                }
                OtherLastNames = sb.ToString().TrimEnd("\r\n".ToCharArray());
            }
            else
            {
                OtherNameGeneration = false;
            }
        }

        /// <summary>
        /// Read table settings from an XML file.
        /// </summary>
        /// <param name="element">XML element containing the table settings.</param>
        private void ReadTableSettings(XElement element)
        {
            { if (element.Attribute(XML_ATTACK) is XAttribute attr) AnimationAttack = ParseEnum<RandomizationLevel>(attr.Value); }
            { if (element.Attribute(XML_DAMAGE) is XAttribute attr) AnimationDamage = ParseEnum<RandomizationLevel>(attr.Value); }
            { if (element.Attribute(XML_FIRE  ) is XAttribute attr) AnimationFire   = ParseEnum<RandomizationLevel>(attr.Value); }
            { if (element.Attribute(XML_LOOP  ) is XAttribute attr) AnimationLoop   = ParseEnum<RandomizationLevel>(attr.Value); }
            { if (element.Attribute(XML_PARRY ) is XAttribute attr) AnimationParry  = ParseEnum<RandomizationLevel>(attr.Value); }
            { if (element.Attribute(XML_PAUSE ) is XAttribute attr) AnimationPause  = ParseEnum<RandomizationLevel>(attr.Value); }
            { if (element.Attribute(XML_MOVE  ) is XAttribute attr) AnimationMove   = ParseEnum<RandomizationLevel>(attr.Value); }

            foreach (var tbl in element.Descendants(XML_TABLE))
            {
                var name = tbl.Attribute(XML_NAME).Value;
                var twoDA = Table2DAs.FirstOrDefault(x => x.Name == name);
                if (twoDA != null)
                {
                    var columns = tbl.Attribute(XML_COLUMNS).Value.Split(' ');
                    foreach (var col in columns)
                    {
                        twoDA.Randomized.Add(col);
                        twoDA.Columns.Remove(col);
                    }
                }
            }
        }

        /// <summary>
        /// Read text settings from an XML file.
        /// </summary>
        /// <param name="element">XML element containing the text settings.</param>
        private void ReadTextSettings(XElement element)
        {
            var attr = element.Attribute(XML_SETTINGS);
            if (attr != null) TextSettingsValue = ParseEnum<TextSettings>(attr.Value);
        }

        /// <summary>
        /// Read texture settings from an XML file.
        /// </summary>
        /// <param name="element">XML element containing the texture settings.</param>
        private void ReadTextureSettings(XElement element)
        {
            { if (element.Attribute(XML_PACK) is XAttribute attr) TextureSelectedPack = ParseEnum<TexturePack>(attr.Value); }

            { if (element.Attribute(XML_CREATURE ) is XAttribute attr) TextureCreatures    = ParseEnum<RandomizationLevel>(attr.Value); }
            { if (element.Attribute(XML_CUBE_MAP ) is XAttribute attr) TextureCubeMaps     = ParseEnum<RandomizationLevel>(attr.Value); }
            { if (element.Attribute(XML_EFFECT   ) is XAttribute attr) TextureEffects      = ParseEnum<RandomizationLevel>(attr.Value); }
            { if (element.Attribute(XML_ITEM     ) is XAttribute attr) TextureItems        = ParseEnum<RandomizationLevel>(attr.Value); }
            { if (element.Attribute(XML_NPC      ) is XAttribute attr) TextureNPC          = ParseEnum<RandomizationLevel>(attr.Value); }
            { if (element.Attribute(XML_OTHER    ) is XAttribute attr) TextureOther        = ParseEnum<RandomizationLevel>(attr.Value); }
            { if (element.Attribute(XML_PARTY    ) is XAttribute attr) TextureParty        = ParseEnum<RandomizationLevel>(attr.Value); }
            { if (element.Attribute(XML_PLACE    ) is XAttribute attr) TexturePlaceables   = ParseEnum<RandomizationLevel>(attr.Value); }
            { if (element.Attribute(XML_PLANETARY) is XAttribute attr) TexturePlanetary    = ParseEnum<RandomizationLevel>(attr.Value); }
            { if (element.Attribute(XML_BODY     ) is XAttribute attr) TexturePlayerBodies = ParseEnum<RandomizationLevel>(attr.Value); }
            { if (element.Attribute(XML_HEAD     ) is XAttribute attr) TexturePlayerHeads  = ParseEnum<RandomizationLevel>(attr.Value); }
            { if (element.Attribute(XML_STUNT    ) is XAttribute attr) TextureStunt        = ParseEnum<RandomizationLevel>(attr.Value); }
            { if (element.Attribute(XML_VEHICLE  ) is XAttribute attr) TextureVehicles     = ParseEnum<RandomizationLevel>(attr.Value); }
            { if (element.Attribute(XML_WEAPON   ) is XAttribute attr) TextureWeapons      = ParseEnum<RandomizationLevel>(attr.Value); }
        }

        /// <summary>
        /// Write General settings to an XML file.
        /// </summary>
        /// <param name="w"></param>
        private void WriteGeneralSettings(XmlTextWriter w)
        {
            w.WriteStartElement(XML_GENERAL);  // Begin General

            Version v = System.Reflection.Assembly.GetAssembly(typeof(Kotor1Randomizer)).GetName().Version;
            w.WriteAttributeString(XML_VERSION, $"v{v.Major}.{v.Minor}.{v.Build}");

            var activeUnlocks = ModuleExtras.Default;
            foreach (var item in GeneralUnlockedDoors)
                activeUnlocks |= item.Tag;

            if (GeneralModuleExtrasValue != ModuleExtras.Default)
                w.WriteAttributeString(XML_QOL, ((int)GeneralModuleExtrasValue).ToString());

            if (activeUnlocks != ModuleExtras.Default)
                w.WriteAttributeString(XML_UNLOCKS, ((int)activeUnlocks).ToString());

            w.WriteEndElement();                // End General
        }

        /// <summary>
        /// Write Audio settings to an XML file.
        /// </summary>
        /// <param name="w"></param>
        private void WriteAudioSettings(XmlTextWriter w)
        {
            w.WriteStartElement(XML_AUDIO);     // Begin Audio
            if (AudioAmbientNoise  != RandomizationLevel.None) w.WriteAttributeString(XML_AMBIENT,  AudioAmbientNoise.ToString());
            if (AudioAreaMusic     != RandomizationLevel.None) w.WriteAttributeString(XML_AREA,     AudioAreaMusic.ToString());
            if (AudioBattleMusic   != RandomizationLevel.None) w.WriteAttributeString(XML_BATTLE,   AudioBattleMusic.ToString());
            if (AudioCutsceneNoise != RandomizationLevel.None) w.WriteAttributeString(XML_CUTSCENE, AudioCutsceneNoise.ToString());
            if (AudioNpcSounds     != RandomizationLevel.None) w.WriteAttributeString(XML_NPC,      AudioNpcSounds.ToString());
            if (AudioPartySounds   != RandomizationLevel.None) w.WriteAttributeString(XML_PARTY,    AudioPartySounds.ToString());
            if (AudioMixNpcAndPartySounds) w.WriteAttributeString(XML_MIXNPCPARTY, AudioMixNpcAndPartySounds.ToString());
            if (AudioRemoveDmcaMusic     ) w.WriteAttributeString(XML_REMOVE_DMCA, AudioRemoveDmcaMusic.ToString());
            w.WriteEndElement();                // End Audio
        }

        /// <summary>
        /// Write Item settings to an XML file.
        /// </summary>
        /// <param name="w"></param>
        private void WriteItemSettings(XmlTextWriter w)
        {
            w.WriteStartElement(XML_ITEM);      // Begin Item
            if (ItemArmbands        != RandomizationLevel.None) w.WriteAttributeString(XML_ARMBAND,    ItemArmbands.ToString());
            if (ItemArmor           != RandomizationLevel.None) w.WriteAttributeString(XML_ARMOR,      ItemArmor.ToString());
            if (ItemBelts           != RandomizationLevel.None) w.WriteAttributeString(XML_BELT,       ItemBelts.ToString());
            if (ItemBlasters        != RandomizationLevel.None) w.WriteAttributeString(XML_BLASTER,    ItemBlasters.ToString());
            if (ItemCreatureHides   != RandomizationLevel.None) w.WriteAttributeString(XML_CHIDE,      ItemCreatureHides.ToString());
            if (ItemCreatureWeapons != RandomizationLevel.None) w.WriteAttributeString(XML_CWEAPON,    ItemCreatureWeapons.ToString());
            if (ItemDroidEquipment  != RandomizationLevel.None) w.WriteAttributeString(XML_DROID,      ItemDroidEquipment.ToString());
            if (ItemGloves          != RandomizationLevel.None) w.WriteAttributeString(XML_GLOVE,      ItemGloves.ToString());
            if (ItemGrenades        != RandomizationLevel.None) w.WriteAttributeString(XML_GRENADE,    ItemGrenades.ToString());
            if (ItemImplants        != RandomizationLevel.None) w.WriteAttributeString(XML_IMPLANT,    ItemImplants.ToString());
            if (ItemLightsabers     != RandomizationLevel.None) w.WriteAttributeString(XML_LIGHTSABER, ItemLightsabers.ToString());
            if (ItemMasks           != RandomizationLevel.None) w.WriteAttributeString(XML_MASK,       ItemMasks.ToString());
            if (ItemMeleeWeapons    != RandomizationLevel.None) w.WriteAttributeString(XML_MELEE,      ItemMeleeWeapons.ToString());
            if (ItemMines           != RandomizationLevel.None) w.WriteAttributeString(XML_MINE,       ItemMines.ToString());
            if (ItemMedical         != RandomizationLevel.None) w.WriteAttributeString(XML_MEDICAL,    ItemMedical.ToString());
            if (ItemPazaakCards     != RandomizationLevel.None) w.WriteAttributeString(XML_PAZAAK,     ItemPazaakCards.ToString());
            if (ItemUpgrades        != RandomizationLevel.None) w.WriteAttributeString(XML_UPGRADE,    ItemUpgrades.ToString());
            if (ItemVarious         != RandomizationLevel.None) w.WriteAttributeString(XML_VARIOUS,    ItemVarious.ToString());

            if (!string.IsNullOrWhiteSpace(ItemOmittedPreset))
            {
                // Write omit preset string.
                w.WriteAttributeString(XML_PRESET, ItemOmittedPreset);
            }
            else
            {
                // Write each omitted item to the file.
                foreach (var item in ItemOmittedList)
                {
                    w.WriteStartElement(XML_OMIT);  // Begin Omit
                    w.WriteAttributeString(XML_CODE, item.Code);
                    w.WriteEndElement();            // End Omit
                }
            }

            w.WriteEndElement();                // End Item
        }

        /// <summary>
        /// Write Model settings to an XML file.
        /// </summary>
        /// <param name="w"></param>
        private void WriteModelSettings(XmlTextWriter w)
        {
            w.WriteStartElement(XML_MODEL);    // Begin Model
            if (ModelCharacterRando)
            {
                w.WriteStartElement(XML_CHAR); // Begin Character
                w.WriteAttributeString(XML_OMIT_LARGE,  ModelCharacterOmitLarge.ToString());
                w.WriteAttributeString(XML_OMIT_BROKEN, ModelCharacterOmitBroken.ToString());
                w.WriteEndElement();            // End Character
            }
            if (ModelDoorRando)
            {
                w.WriteStartElement(XML_DOOR); // Begin Door
                w.WriteAttributeString(XML_OMIT_AIRLOCK, ModelDoorOmitAirlock.ToString());
                w.WriteAttributeString(XML_OMIT_BROKEN,  ModelDoorOmitBroken.ToString());
                w.WriteEndElement();            // End Door
            }
            if (ModelPlaceableRando)
            {
                w.WriteStartElement(XML_PLAC); // Begin Placeable
                w.WriteAttributeString(XML_OMIT_LARGE,  ModelPlaceableOmitLarge.ToString());
                w.WriteAttributeString(XML_OMIT_BROKEN, ModelPlaceableOmitBroken.ToString());
                w.WriteAttributeString(XML_EASY_PANELS, ModelPlaceableEasyPanels.ToString());
                w.WriteEndElement();            // End Placeable
            }
            w.WriteEndElement();                // End Model
        }

        /// <summary>
        /// Write Module settings to an XML file.
        /// </summary>
        /// <param name="w"></param>
        private void WriteModuleSettings(XmlTextWriter w)
        {
            w.WriteStartElement(XML_MODULE);   // Begin Module
            w.WriteStartElement(XML_GLITCHES); // Begin Glitches
            w.WriteAttributeString(XML_CLIP,    ModuleAllowGlitchClip.ToString());
            w.WriteAttributeString(XML_DLZ,     ModuleAllowGlitchDlz.ToString());
            w.WriteAttributeString(XML_FLU,     ModuleAllowGlitchFlu.ToString());
            w.WriteAttributeString(XML_GPW,     ModuleAllowGlitchGpw.ToString());
            w.WriteEndElement();                // End Glitches

            w.WriteStartElement(XML_GOALS);    // Begin Goals
            w.WriteAttributeString(XML_MALAK,  ModuleGoalIsMalak.ToString());
            w.WriteAttributeString(XML_MAPS,   ModuleGoalIsStarMap.ToString());
            w.WriteAttributeString(XML_PAZAAK, ModuleGoalIsPazaak.ToString());
            w.WriteAttributeString(XML_PARTY,  ModuleGoalIsFullParty.ToString());
            w.WriteEndElement();                // End Goals

            w.WriteStartElement(XML_LOGIC);    // Begin Logic
            w.WriteAttributeString(XML_RULES,        ModuleLogicRandoRules.ToString());
            w.WriteAttributeString(XML_REACHABLE,    ModuleLogicReachability.ToString());
            w.WriteAttributeString(XML_IGNORE_ONCE,  ModuleLogicIgnoreOnceEdges.ToString());
            w.WriteAttributeString(XML_STRONG_GOALS, ModuleLogicStrongGoals.ToString());
            w.WriteEndElement();                // End Logic

            w.WriteStartElement(XML_OMIT);     // Begin Omit
            if (!string.IsNullOrWhiteSpace(ModuleShufflePreset))
            {
                // Write the module omit preset to file.
                w.WriteAttributeString(XML_PRESET, ModuleShufflePreset);
            }
            else
            {
                // Write each omitted module to file.
                foreach (var item in ModuleOmittedList)
                {
                    w.WriteStartElement(XML_MODULE);   // Begin Module
                    w.WriteAttributeString(XML_CODE, item.WarpCode);
                    w.WriteEndElement();                // End Module;
                }
            }
            w.WriteEndElement();                // End Omit
            w.WriteEndElement();                // End Module
        }

        /// <summary>
        /// Write Other settings to an XML file.
        /// </summary>
        /// <param name="w"></param>
        private void WriteOtherSettings(XmlTextWriter w)
        {
            w.WriteStartElement(XML_OTHER);    // Start Other
            if (OtherPartyMembers  ) w.WriteAttributeString(XML_PARTY,          OtherPartyMembers.ToString());
            if (OtherPazaakDecks   ) w.WriteAttributeString(XML_PAZAAK,         OtherPazaakDecks.ToString());
            if (OtherPolymorphMode ) w.WriteAttributeString(XML_POLYMORPH,      OtherPolymorphMode.ToString());
            if (OtherSwoopBoosters ) w.WriteAttributeString(XML_SWOOP_BOOSTERS, OtherSwoopBoosters.ToString());
            if (OtherSwoopObstacles) w.WriteAttributeString(XML_SWOOP_OBSTACLE, OtherSwoopObstacles.ToString());

            if (OtherNameGeneration)
            {
                w.WriteStartElement(XML_NAMES);    // Start Names
                foreach (var name in OtherFirstNamesF.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)) w.WriteElementString(XML_FIRST_NAME_F, name);
                foreach (var name in OtherFirstNamesM.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)) w.WriteElementString(XML_FIRST_NAME_M, name);
                foreach (var name in OtherLastNames.Split(  "\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)) w.WriteElementString(XML_LAST_NAME,    name);
                w.WriteEndElement();                // End   Names
            }
            w.WriteEndElement();                // End Other
        }

        /// <summary>
        /// Write Table settings to an XML file.
        /// </summary>
        /// <param name="w"></param>
        private void WriteTableSettings(XmlTextWriter w)
        {
            w.WriteStartElement(XML_TABLES);    // Start Tables

            if (AnimationAttack != RandomizationLevel.None) w.WriteAttributeString(XML_ATTACK, AnimationAttack.ToString());
            if (AnimationDamage != RandomizationLevel.None) w.WriteAttributeString(XML_DAMAGE, AnimationDamage.ToString());
            if (AnimationFire   != RandomizationLevel.None) w.WriteAttributeString(XML_FIRE,   AnimationFire.ToString());
            if (AnimationLoop   != RandomizationLevel.None) w.WriteAttributeString(XML_LOOP,   AnimationLoop.ToString());
            if (AnimationParry  != RandomizationLevel.None) w.WriteAttributeString(XML_PARRY,  AnimationParry.ToString());
            if (AnimationPause  != RandomizationLevel.None) w.WriteAttributeString(XML_PAUSE,  AnimationPause.ToString());
            if (AnimationMove   != RandomizationLevel.None) w.WriteAttributeString(XML_MOVE,   AnimationMove.ToString());

            foreach (var table in Table2DAs.Where(rt => rt.IsRandomized))
            {
                w.WriteStartElement(XML_TABLE);    // Start Table

                w.WriteAttributeString(XML_NAME, table.Name);
                w.WriteAttributeString(XML_COLUMNS, string.Join(" ", table.Randomized));

                w.WriteEndElement();                // End   Table
            }
            w.WriteEndElement();                // End   Tables
        }

        /// <summary>
        /// Write Text settings to an XML file.
        /// </summary>
        /// <param name="w"></param>
        private void WriteTextSettings(XmlTextWriter w)
        {
            w.WriteStartElement(XML_TEXT); // Start Text
            w.WriteAttributeString(XML_SETTINGS, ((int)TextSettingsValue).ToString());
            w.WriteEndElement();            // End   Text
        }

        /// <summary>
        /// Write Texture settings to an XML file.
        /// </summary>
        /// <param name="w"></param>
        private void WriteTextureSettings(XmlTextWriter w)
        {
            w.WriteStartElement(XML_TEXTURE);  // Start Texture
            w.WriteAttributeString(XML_PACK, TextureSelectedPack.ToString());
            if (TextureCreatures    != RandomizationLevel.None) w.WriteAttributeString(XML_CREATURE,  TextureCreatures.ToString());
            if (TextureCubeMaps     != RandomizationLevel.None) w.WriteAttributeString(XML_CUBE_MAP,  TextureCubeMaps.ToString());
            if (TextureEffects      != RandomizationLevel.None) w.WriteAttributeString(XML_EFFECT,    TextureEffects.ToString());
            if (TextureItems        != RandomizationLevel.None) w.WriteAttributeString(XML_ITEM,      TextureItems.ToString());
            if (TextureNPC          != RandomizationLevel.None) w.WriteAttributeString(XML_NPC,       TextureNPC.ToString());
            if (TextureOther        != RandomizationLevel.None) w.WriteAttributeString(XML_OTHER,     TextureOther.ToString());
            if (TextureParty        != RandomizationLevel.None) w.WriteAttributeString(XML_PARTY,     TextureParty.ToString());
            if (TexturePlaceables   != RandomizationLevel.None) w.WriteAttributeString(XML_PLACE,     TexturePlaceables.ToString());
            if (TexturePlanetary    != RandomizationLevel.None) w.WriteAttributeString(XML_PLANETARY, TexturePlanetary.ToString());
            if (TexturePlayerBodies != RandomizationLevel.None) w.WriteAttributeString(XML_BODY,      TexturePlayerBodies.ToString());
            if (TexturePlayerHeads  != RandomizationLevel.None) w.WriteAttributeString(XML_HEAD,      TexturePlayerHeads.ToString());
            if (TextureStunt        != RandomizationLevel.None) w.WriteAttributeString(XML_STUNT,     TextureStunt.ToString());
            if (TextureVehicles     != RandomizationLevel.None) w.WriteAttributeString(XML_VEHICLE,   TextureVehicles.ToString());
            if (TextureWeapons      != RandomizationLevel.None) w.WriteAttributeString(XML_WEAPON,    TextureWeapons.ToString());
            w.WriteEndElement();                // End   Texture
        }
        #endregion Private Methods
    }
}
