﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using ClosedXML.Excel;

namespace kotor_Randomizer_2.Models
{
    public class Kotor1Randomizer : RandomizerBase
    {
        #region Constants
        const ModuleExtras EXTRAS_MASK = ModuleExtras.NoSaveDelete   | ModuleExtras.SaveMiniGames | ModuleExtras.SaveAllModules |
                                         ModuleExtras.FixCoordinates | ModuleExtras.FixDream      | ModuleExtras.FixMindPrison  |
                                         ModuleExtras.VulkarSpiceLZ;
        const ModuleExtras UNLOCKS_MASK = ModuleExtras.UnlockDanRuins   | ModuleExtras.UnlockGalaxyMap    | ModuleExtras.UnlockKorValley |
                                          ModuleExtras.UnlockLevElev    | ModuleExtras.UnlockManEmbassy   | ModuleExtras.UnlockManHangar |
                                          ModuleExtras.UnlockStaBastila | ModuleExtras.UnlockTarUndercity | ModuleExtras.UnlockTarVulkar |
                                          ModuleExtras.UnlockUnkSummit  | ModuleExtras.UnlockUnkTempleExit;

        const string ELEM_SETTINGS = "Settings";

        const string ELEM_OMIT    = "Omit";
        const string ELEM_UNLOCK  = "Unlock";

        const string ELEM_GENERAL = "General";
        const string ELEM_AUDIO   = "Audio";
        const string ELEM_ITEM    = "Item";
        const string ELEM_MODEL   = "Model";
        const string ELEM_MODULE  = "Module";
        const string ELEM_OTHER   = "Other";
        const string ELEM_TABLE   = "Table";
        const string ELEM_TEXT    = "Text";
        const string ELEM_TEXTURE = "Texture";

        const string ELEM_CHAR     = "Character";
        const string ELEM_DOOR     = "Door";
        const string ELEM_PLAC     = "Placeable";
        const string ELEM_GLITCHES = "Glitches";
        const string ELEM_GOALS    = "Goals";
        const string ELEM_LOGIC    = "Logic";

        const string ELEM_NAMES        = "Names";
        const string ELEM_FIRST_NAME_F = "FirstF";
        const string ELEM_FIRST_NAME_M = "FirstM";
        const string ELEM_LAST_NAME    = "Last";
        const string ELEM_COLUMN       = "Column";

        const string ATTR_QOL      = "QoL";
        const string ATTR_TAG      = "Tag";
        const string ATTR_CODE     = "Code";
        const string ATTR_SETTINGS = "Settings";

        const string ATTR_AMBIENT     = "Ambient";
        const string ATTR_AREA        = "Area";
        const string ATTR_BATTLE      = "Battle";
        const string ATTR_CUTSCENE    = "Cutscene";
        const string ATTR_NPC         = "Npc";
        const string ATTR_PARTY       = "Party";
        const string ATTR_MIXNPCPARTY = "MixNpcParty";
        const string ATTR_REMOVE_DMCA = "RemoveDmca";

        const string ATTR_ARMBAND    = "Armband";
        const string ATTR_ARMOR      = "Armor";
        const string ATTR_BELT       = "Belt";
        const string ATTR_BLASTER    = "Blaster";
        const string ATTR_CHIDE      = "CHide";
        const string ATTR_CWEAPON    = "CWeapon";
        const string ATTR_DROID      = "Droid";
        const string ATTR_GLOVE      = "Glove";
        const string ATTR_GRENADE    = "Grenade";
        const string ATTR_IMPLANT    = "Implant";
        const string ATTR_LIGHTSABER = "Lightsaber";
        const string ATTR_MASK       = "Mask";
        const string ATTR_MELEE      = "Melee";
        const string ATTR_MINE       = "Mine";
        const string ATTR_MEDICAL    = "Medical";
        const string ATTR_PAZAAK     = "Pazaak";
        const string ATTR_UPGRADE    = "Upgrade";
        const string ATTR_VARIOUS    = "Various";

        const string ATTR_OMIT_LARGE   = "OmitLarge";
        const string ATTR_OMIT_BROKEN  = "OmitBroken";
        const string ATTR_OMIT_AIRLOCK = "OmitAirlock";
        const string ATTR_EASY_PANELS  = "EasyPanels";

        const string ATTR_CLIP        = "Clip";
        const string ATTR_DLZ         = "DLZ";
        const string ATTR_FLU         = "FLU";
        const string ATTR_GPW         = "GPW";
        const string ATTR_HOTSHOT     = "Hotshot";
        const string ATTR_MALAK       = "Malak";
        const string ATTR_MAPS        = "StarMaps";
        const string ATTR_IGNORE_ONCE = "IgnoreOnce";
        const string ATTR_RULES       = "Rules";
        const string ATTR_REACHABLE   = "Reachable";

        const string ATTR_POLYMORPH      = "Polymorph";
        const string ATTR_SWOOP_BOOSTERS = "SwoopBoosters";
        const string ATTR_SWOOP_OBSTACLE = "SwoopObstacle";

        const string ATTR_PACK      = "Pack";
        const string ATTR_CREATURE  = "Creature";
        const string ATTR_CUBE_MAP  = "CubeMap";
        const string ATTR_EFFECT    = "Effect";
        const string ATTR_ITEM      = "Item";
        const string ATTR_OTHER     = "Other";
        const string ATTR_PLACE     = "Placeable";
        const string ATTR_PLANETARY = "Planetary";
        const string ATTR_BODY      = "PBody";
        const string ATTR_HEAD      = "PHead";
        const string ATTR_STUNT     = "Stunt";
        const string ATTR_VEHICLE   = "Vehicle";
        const string ATTR_WEAPON    = "Weapon";

        const string ATTR_          = "";
        #endregion

        #region Constructors
        public Kotor1Randomizer() : this(string.Empty) { }

        public Kotor1Randomizer(string path)
        {
            // Create list of unlockable doors.
            GeneralLockedDoors.Add(new UnlockableDoor() { Area = "LEV", Label = "Elevators",         Tag = ModuleExtras.UnlockLevElev });
            GeneralLockedDoors.Add(new UnlockableDoor() { Area = "MAN", Label = "Republic Embassy",  Tag = ModuleExtras.UnlockManEmbassy });
            GeneralLockedDoors.Add(new UnlockableDoor() { Area = "MAN", Label = "Sith Hangar",       Tag = ModuleExtras.UnlockManHangar });
            GeneralLockedDoors.Add(new UnlockableDoor() { Area = "STA", Label = "Door to Bastila",   Tag = ModuleExtras.UnlockStaBastila });
            GeneralLockedDoors.Add(new UnlockableDoor() { Area = "TAR", Label = "Undercity",         Tag = ModuleExtras.UnlockTarUndercity });
            GeneralLockedDoors.Add(new UnlockableDoor() { Area = "TAR", Label = "Vulkar Base",       Tag = ModuleExtras.UnlockTarVulkar });
            GeneralLockedDoors.Add(new UnlockableDoor() { Area = "UNK", Label = "Summit Exit",       Tag = ModuleExtras.UnlockUnkSummit });
            GeneralLockedDoors.Add(new UnlockableDoor() { Area = "UNK", Label = "Temple Exit",       Tag = ModuleExtras.UnlockUnkTempleExit });
            GeneralLockedDoors.Add(new UnlockableDoor() { Area = "DAN", Label = "Ruins Door",        Tag = ModuleExtras.UnlockDanRuins });
            GeneralLockedDoors.Add(new UnlockableDoor() { Area = "EBO", Label = "Galaxy Map",        Tag = ModuleExtras.UnlockGalaxyMap });
            GeneralLockedDoors.Add(new UnlockableDoor() { Area = "KOR", Label = "Valley After Tomb", Tag = ModuleExtras.UnlockKorValley });

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
            Table2DAs = new ObservableCollection<RandomizableTable>();
            foreach (var table in Globals.TWODA_COLLUMNS)
            {
                Table2DAs.Add(new RandomizableTable(table.Key, table.Value));
            }

            // Load settings from file if the path is not empty.
            if (!string.IsNullOrWhiteSpace(path) && File.Exists(path))
                Load(path);
            else
                SettingsFileName = string.Empty;
        }
        #endregion Constructors

        #region Properties
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

        private bool _moduleAllowGlitchHotshot = true;
        public bool ModuleAllowGlitchHotshot
        {
            get => _moduleAllowGlitchHotshot;
            set => SetField(ref _moduleAllowGlitchHotshot, value);
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
        public bool DoRandomizeAudio
        {
            get
            {
                return
                    AudioAmbientNoise  != RandomizationLevel.None ||
                    AudioAreaMusic     != RandomizationLevel.None ||
                    AudioBattleMusic   != RandomizationLevel.None ||
                    AudioCutsceneNoise != RandomizationLevel.None ||
                    AudioNpcSounds     != RandomizationLevel.None ||
                    AudioPartySounds   != RandomizationLevel.None ||
                    AudioRemoveDmcaMusic;
            }
        }
        public bool DoRandomizeItems
        {
            get
            {
                return
                    ItemArmbands        != RandomizationLevel.None ||
                    ItemArmor           != RandomizationLevel.None ||
                    ItemBelts           != RandomizationLevel.None ||
                    ItemBlasters        != RandomizationLevel.None ||
                    ItemCreatureHides   != RandomizationLevel.None ||
                    ItemCreatureWeapons != RandomizationLevel.None ||
                    ItemDroidEquipment  != RandomizationLevel.None ||
                    ItemGloves          != RandomizationLevel.None ||
                    ItemGrenades        != RandomizationLevel.None ||
                    ItemImplants        != RandomizationLevel.None ||
                    ItemLightsabers     != RandomizationLevel.None ||
                    ItemMasks           != RandomizationLevel.None ||
                    ItemMedical         != RandomizationLevel.None ||
                    ItemMeleeWeapons    != RandomizationLevel.None ||
                    ItemMines           != RandomizationLevel.None ||
                    ItemPazaakCards     != RandomizationLevel.None ||
                    ItemUpgrades        != RandomizationLevel.None ||
                    ItemVarious         != RandomizationLevel.None;
            }
        }
        public bool DoRandomizeModels
        {
            get
            {
                return
                    ModelCharacterRando ||
                    ModelDoorRando      ||
                    ModelPlaceableRando;
            }
        }
        public bool DoRandomizeModules
        {
            get
            {
                // A couple of general options are handled by module randomization.
                return (_moduleRandomizedList?.Count ?? 0) > 1 ||
                        GeneralModuleExtrasValue   != ModuleExtras.Default ||
                        GeneralUnlockedDoors.Count != 0;
            }
        }
        public bool DoRandomizeOther
        {
            get
            {
                return
                    OtherNameGeneration ||
                    OtherPartyMembers ||
                    OtherPazaakDecks ||
                    OtherPolymorphMode ||
                    OtherSwoopBoosters ||
                    OtherSwoopObstacles;
            }
        }
        //public bool DoRandomizeParty
        //{
        //    get
        //    {
        //        return false;   // Not yet implemented.
        //    }
        //}
        public bool DoRandomizeTables
        {
            get
            {
                return Table2DAs.Any(rt => rt.IsRandomized);
            }
        }
        public bool DoRandomizeText
        {
            get
            {
                return TextSettingsValue != TextSettings.Default;
            }
        }
        public bool DoRandomizeTextures
        {
            get
            {
                return
                    TextureCreatures    != RandomizationLevel.None ||
                    TextureCubeMaps     != RandomizationLevel.None ||
                    TextureEffects      != RandomizationLevel.None ||
                    TextureItems        != RandomizationLevel.None ||
                    TextureNPC          != RandomizationLevel.None ||
                    TextureOther        != RandomizationLevel.None ||
                    TextureParty        != RandomizationLevel.None ||
                    TexturePlaceables   != RandomizationLevel.None ||
                    TexturePlanetary    != RandomizationLevel.None ||
                    TexturePlayerBodies != RandomizationLevel.None ||
                    TexturePlayerHeads  != RandomizationLevel.None ||
                    TextureStunt        != RandomizationLevel.None ||
                    TextureVehicles     != RandomizationLevel.None ||
                    TextureWeapons      != RandomizationLevel.None;
            }
        }
        #endregion Active Rando Properties
        #endregion Properties

        #region Methods
        public override void Randomizer_DoWork(object sender, DoWorkEventArgs e)
        {
            var bw = sender as BackgroundWorker;

            if (e.Argument is RandoArgs args)
            {
                // GamePath can't be empty.
                if (string.IsNullOrWhiteSpace(args.GamePath))
                    throw new ArgumentException("Game path not given.", "RandoArgs.GamePath");

                // Final check for already randomized game before randomizing.
                var paths = new KPaths(args.GamePath);
                if (File.Exists(paths.RANDOMIZED_LOG))
                    throw new InvalidOperationException(Properties.Resources.AlreadyRandomized);

                if (args.Seed < 0) args.Seed *= -1; // Seed must be non-negative.
                Randomize.SetSeed(args.Seed);       // Set the seed.

                // Perform randomization.
                DoRandomize(bw, paths);

                // If SpoilersPath is given, create spoiler logs.
                if (!string.IsNullOrWhiteSpace(args.SpoilersPath))
                    DoSpoil(bw, args.SpoilersPath);
            }
            else
            {
                // Invalid use of this method. Please define randomization arguments.
                throw new ArgumentNullException("argument", "RandoArgs must be given when randomizing.");
            }
        }

        public override void Unrandomize(object sender, DoWorkEventArgs e)
        {
            var bw = sender as BackgroundWorker;

            if (e.Argument is RandoArgs args)
            {
                // GamePath can't be empty.
                if (string.IsNullOrWhiteSpace(args.GamePath))
                    throw new ArgumentException("Game path not given.", "RandoArgs.GamePath");

                // Final check for already unrandomized game before unrandomizing.
                KPaths paths = new KPaths(args.GamePath);
                if (!File.Exists(paths.RANDOMIZED_LOG))
                    throw new InvalidOperationException(Properties.Resources.ErrorNotRandomized);

                double stepSize = 100.0 / 8.0;  // Calculation: 100 / (# of things to restore)
                double progress = 0;

                try
                {
                    ReportProgress(bw, progress += stepSize, Properties.Resources.UnrandomizingModules, BusyState.Unrandomizing);
                    paths.RestoreModulesDirectory();

                    ReportProgress(bw, progress += stepSize, Properties.Resources.UnrandomizingLips, BusyState.Unrandomizing);
                    paths.RestoreLipsDirectory();

                    ReportProgress(bw, progress += stepSize, Properties.Resources.UnrandomizingOverrides, BusyState.Unrandomizing);
                    paths.RestoreOverrideDirectory();

                    ReportProgress(bw, progress += stepSize, Properties.Resources.UnrandomizingMusic, BusyState.Unrandomizing);
                    paths.RestoreMusicDirectory();

                    ReportProgress(bw, progress += stepSize, Properties.Resources.UnrandomizingSounds, BusyState.Unrandomizing);
                    paths.RestoreSoundsDirectory();

                    ReportProgress(bw, progress += stepSize, Properties.Resources.UnrandomizingTextures, BusyState.Unrandomizing);
                    paths.RestoreTexturePacksDirectory();

                    ReportProgress(bw, progress += stepSize, Properties.Resources.UnrandomizingKeyTable, BusyState.Unrandomizing);
                    paths.RestoreChitinFile();

                    ReportProgress(bw, progress += stepSize, Properties.Resources.UnrandomizingTLKFile, BusyState.Unrandomizing);
                    paths.RestoreDialogFile();

                    ReportProgress(bw, progress += stepSize, Properties.Resources.TaskFinishing, BusyState.Unrandomizing);
                    File.Delete(paths.RANDOMIZED_LOG);

                    ResetRandomizationCategories();
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
            throw new NotImplementedException();
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
                w.WriteStartElement(ELEM_SETTINGS); // Begin Settings

                {
                    w.WriteStartElement(ELEM_GENERAL);  // Begin General
                    w.WriteAttributeString(ATTR_QOL, GeneralModuleExtrasValue.ToString());

                    foreach (var item in GeneralUnlockedDoors)
                    {
                        w.WriteStartElement(ELEM_UNLOCK);   // Begin Unlock
                        w.WriteAttributeString(ATTR_TAG, item.Tag.ToString());
                        w.WriteEndElement();                // End Unlock
                    }
                    w.WriteEndElement();                // End General
                }   // General Settings

                if (DoRandomizeAudio)
                {
                    w.WriteStartElement(ELEM_AUDIO);    // Begin Audio
                    w.WriteAttributeString(ATTR_AMBIENT,     AudioAmbientNoise.ToString());
                    w.WriteAttributeString(ATTR_AREA,        AudioAmbientNoise.ToString());
                    w.WriteAttributeString(ATTR_BATTLE,      AudioAmbientNoise.ToString());
                    w.WriteAttributeString(ATTR_CUTSCENE,    AudioAmbientNoise.ToString());
                    w.WriteAttributeString(ATTR_NPC,         AudioAmbientNoise.ToString());
                    w.WriteAttributeString(ATTR_PARTY,       AudioAmbientNoise.ToString());
                    w.WriteAttributeString(ATTR_MIXNPCPARTY, AudioAmbientNoise.ToString());
                    w.WriteAttributeString(ATTR_REMOVE_DMCA, AudioAmbientNoise.ToString());
                    w.WriteEndElement();                // End Audio
                }   // Audio Settings

                if (DoRandomizeItems)
                {
                    w.WriteStartElement(ELEM_ITEM);     // Begin Item
                    w.WriteAttributeString(ATTR_ARMBAND,    ItemArmbands.ToString());
                    w.WriteAttributeString(ATTR_ARMOR,      ItemArmor.ToString());
                    w.WriteAttributeString(ATTR_BELT,       ItemBelts.ToString());
                    w.WriteAttributeString(ATTR_BLASTER,    ItemBlasters.ToString());
                    w.WriteAttributeString(ATTR_CHIDE,      ItemCreatureHides.ToString());
                    w.WriteAttributeString(ATTR_CWEAPON,    ItemCreatureWeapons.ToString());
                    w.WriteAttributeString(ATTR_DROID,      ItemDroidEquipment.ToString());
                    w.WriteAttributeString(ATTR_GLOVE,      ItemGloves.ToString());
                    w.WriteAttributeString(ATTR_GRENADE,    ItemGrenades.ToString());
                    w.WriteAttributeString(ATTR_IMPLANT,    ItemImplants.ToString());
                    w.WriteAttributeString(ATTR_LIGHTSABER, ItemLightsabers.ToString());
                    w.WriteAttributeString(ATTR_MASK,       ItemMasks.ToString());
                    w.WriteAttributeString(ATTR_MELEE,      ItemMeleeWeapons.ToString());
                    w.WriteAttributeString(ATTR_MINE,       ItemMines.ToString());
                    w.WriteAttributeString(ATTR_MEDICAL,    ItemMedical.ToString());
                    w.WriteAttributeString(ATTR_PAZAAK,     ItemPazaakCards.ToString());
                    w.WriteAttributeString(ATTR_UPGRADE,    ItemUpgrades.ToString());
                    w.WriteAttributeString(ATTR_VARIOUS,    ItemVarious.ToString());

                    w.WriteStartElement(ELEM_OMIT);     // Begin Omit
                    foreach (var item in ItemOmittedList)
                    {
                        w.WriteStartElement(ELEM_ITEM); // Begin Item
                        w.WriteAttributeString(ATTR_CODE, item.Code);
                        w.WriteEndElement();            // End Item
                    }
                    w.WriteEndElement();                // End Omit
                    w.WriteEndElement();                // End Item
                }   // Item Settings

                if (DoRandomizeModels)
                {
                    w.WriteStartElement(ELEM_MODEL);    // Begin Model
                    if (ModelCharacterRando)
                    {
                        w.WriteStartElement(ELEM_CHAR); // Begin Character
                        w.WriteAttributeString(ATTR_OMIT_LARGE,  ModelCharacterOmitLarge.ToString());
                        w.WriteAttributeString(ATTR_OMIT_BROKEN, ModelCharacterOmitBroken.ToString());
                        w.WriteEndElement();            // End Character
                    }
                    if (ModelDoorRando)
                    {
                        w.WriteStartElement(ELEM_DOOR); // Begin Door
                        w.WriteAttributeString(ATTR_OMIT_AIRLOCK, ModelDoorOmitAirlock.ToString());
                        w.WriteAttributeString(ATTR_OMIT_BROKEN,  ModelDoorOmitBroken.ToString());
                        w.WriteEndElement();            // End Door
                    }
                    if (ModelPlaceableRando)
                    {
                        w.WriteStartElement(ELEM_PLAC); // Begin Placeable
                        w.WriteAttributeString(ATTR_OMIT_LARGE,  ModelPlaceableOmitLarge.ToString());
                        w.WriteAttributeString(ATTR_OMIT_BROKEN, ModelPlaceableOmitBroken.ToString());
                        w.WriteAttributeString(ATTR_EASY_PANELS, ModelPlaceableEasyPanels.ToString());
                        w.WriteEndElement();            // End Placeable
                    }
                    w.WriteEndElement();                // End Model
                }   // Model Settings

                if (DoRandomizeModules)
                {
                    w.WriteStartElement(ELEM_MODULE);   // Begin Module
                    w.WriteStartElement(ELEM_GLITCHES); // Begin Glitches
                    w.WriteAttributeString(ATTR_CLIP,    ModuleAllowGlitchClip.ToString());
                    w.WriteAttributeString(ATTR_DLZ,     ModuleAllowGlitchDlz.ToString());
                    w.WriteAttributeString(ATTR_FLU,     ModuleAllowGlitchFlu.ToString());
                    w.WriteAttributeString(ATTR_GPW,     ModuleAllowGlitchGpw.ToString());
                    w.WriteAttributeString(ATTR_HOTSHOT, ModuleAllowGlitchHotshot.ToString());
                    w.WriteEndElement();                // End Glitches

                    w.WriteStartElement(ELEM_GOALS);    // Begin Goals
                    w.WriteAttributeString(ATTR_MALAK,  ModuleGoalIsMalak.ToString());
                    w.WriteAttributeString(ATTR_MAPS,   ModuleGoalIsStarMap.ToString());
                    w.WriteAttributeString(ATTR_PAZAAK, ModuleGoalIsPazaak.ToString());
                    w.WriteAttributeString(ATTR_PARTY,  ModuleGoalIsFullParty.ToString());
                    w.WriteEndElement();                // End Goals

                    w.WriteStartElement(ELEM_LOGIC);    // Begin Logic
                    w.WriteAttributeString(ATTR_IGNORE_ONCE, ModuleLogicIgnoreOnceEdges.ToString());
                    w.WriteAttributeString(ATTR_RULES,       ModuleLogicRandoRules.ToString());
                    w.WriteAttributeString(ATTR_REACHABLE,   ModuleLogicReachability.ToString());
                    w.WriteEndElement();                // End Logic

                    w.WriteStartElement(ELEM_OMIT);     // Begin Omit
                    foreach (var item in ModuleOmittedList)
                    {
                        w.WriteStartElement(ELEM_MODULE);   // Begin Module
                        w.WriteAttributeString(ATTR_CODE, item.WarpCode);
                        w.WriteEndElement();                // End Module;
                    }
                    w.WriteEndElement();                // End Omit
                    w.WriteEndElement();                // End Module
                }   // Module Settings

                if (DoRandomizeOther)
                {
                    w.WriteStartElement(ELEM_OTHER);    // Start Other
                    if (OtherPartyMembers)   w.WriteAttributeString(ATTR_PARTY,          OtherPartyMembers.ToString());
                    if (OtherPazaakDecks)    w.WriteAttributeString(ATTR_PAZAAK,         OtherPazaakDecks.ToString());
                    if (OtherPolymorphMode)  w.WriteAttributeString(ATTR_POLYMORPH,      OtherPolymorphMode.ToString());
                    if (OtherSwoopBoosters)  w.WriteAttributeString(ATTR_SWOOP_BOOSTERS, OtherSwoopBoosters.ToString());
                    if (OtherSwoopObstacles) w.WriteAttributeString(ATTR_SWOOP_OBSTACLE, OtherSwoopObstacles.ToString());

                    if (OtherNameGeneration)
                    {
                        w.WriteStartElement(ELEM_NAMES);    // Start Names
                        foreach (var name in OtherFirstNamesF.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)) w.WriteElementString(ELEM_FIRST_NAME_F, name);
                        foreach (var name in OtherFirstNamesM.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)) w.WriteElementString(ELEM_FIRST_NAME_M, name);
                        foreach (var name in OtherLastNames.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries))   w.WriteElementString(ELEM_LAST_NAME,    name);
                        w.WriteEndElement();                // End   Names
                    }
                    w.WriteEndElement();                // End Other
                }   // Other Settings

                //if (DoRandomizeParty) { }

                if (DoRandomizeTables)
                {
                    w.WriteStartElement(ELEM_TABLE);    // Start Table
                    foreach (var table in Table2DAs.Where(rt => rt.IsRandomized))
                    {
                        w.WriteStartElement(ELEM_TABLE);    // Start Table
                        foreach (var column in table.Randomized)
                            w.WriteElementString(ELEM_COLUMN, column);
                        w.WriteEndElement();                // End   Table
                    }
                    w.WriteEndElement();                // End   Table
                }   // Table Settings

                if (DoRandomizeText)
                {
                    w.WriteStartElement(ELEM_TEXT); // Start Text
                    w.WriteAttributeString(ATTR_SETTINGS,   TextSettingsValue.ToString());
                    w.WriteEndElement();            // End   Text
                }   // Text Settings

                if (DoRandomizeTextures)
                {
                    w.WriteStartElement(ELEM_TEXTURE);  // Start Texture
                    w.WriteAttributeString(ATTR_PACK,       TextureSelectedPack.ToString());
                    w.WriteAttributeString(ATTR_CREATURE,   TextureCreatures.ToString());
                    w.WriteAttributeString(ATTR_CUBE_MAP,   TextureCubeMaps.ToString());
                    w.WriteAttributeString(ATTR_EFFECT,     TextureEffects.ToString());
                    w.WriteAttributeString(ATTR_ITEM,       TextureItems.ToString());
                    w.WriteAttributeString(ATTR_NPC,        TextureNPC.ToString());
                    w.WriteAttributeString(ATTR_OTHER,      TextureOther.ToString());
                    w.WriteAttributeString(ATTR_PARTY,      TextureParty.ToString());
                    w.WriteAttributeString(ATTR_PLACE,      TexturePlaceables.ToString());
                    w.WriteAttributeString(ATTR_PLANETARY,  TexturePlanetary.ToString());
                    w.WriteAttributeString(ATTR_BODY,       TexturePlayerBodies.ToString());
                    w.WriteAttributeString(ATTR_HEAD,       TexturePlayerHeads.ToString());
                    w.WriteAttributeString(ATTR_STUNT,      TextureStunt.ToString());
                    w.WriteAttributeString(ATTR_VEHICLE,    TextureVehicles.ToString());
                    w.WriteAttributeString(ATTR_WEAPON,     TextureWeapons.ToString());
                    w.WriteEndElement();                // End   Texture
                }   // Texture Settings

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
            if (KRP.ReadKRP(s) == true)
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
                else                ResetTwoDAs();

                if (doRandoText)    ReadKRPText();
                else                ResetText();

                if (doRandoOther)   ReadKRPOther();
                else                ResetOther();
            }
            else
            {
                // Expected KRP version didn't match the file.
            }
        }

        /// <summary>
        /// Writes a KRP file using the old, compact format.
        /// </summary>
        /// <param name="s"></param>
        protected override void WriteKRP(Stream s)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Private Methods
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

        private void DoRandomize(BackgroundWorker bw, KPaths paths)
        {
            // Determine step size and throw error if no categories are selected.
            int activeCategories = CountActiveCategories();
            if (activeCategories == 0)
                throw new InvalidOperationException(Properties.Resources.ErrorNoRandomization);

            activeCategories++; // Increase steps by 1.
            double stepSize = 100.0 / activeCategories;  // Calculate step size.
            double progress = 0;

            // Begin randomization process.
            using (StreamWriter sw = new StreamWriter(paths.RANDOMIZED_LOG))
            {
                sw.WriteLine(DateTime.Now.ToString());
                sw.WriteLine(Properties.Resources.LogHeader);
                ResetRandomizationCategories();

                try
                {
                    paths.BackUpOverrideDirectory();
                    File.WriteAllBytes(Path.Combine(paths.Override, "appearance.2da"), Properties.Resources.appearance);
                    File.WriteAllBytes(Path.Combine(paths.Override, "k_pdan_13_area.ncs"), Properties.Resources.k_pdan_13_area);

                    if (DoRandomizeModules)     // Randomize Modules
                    {
                        ReportProgress(bw, progress += stepSize, Properties.Resources.RandomizingModules, BusyState.Randomizing);
                        Randomize.RestartRng();
                        ModuleRando.CreateBackups(paths);
                        ModuleRando.Module_rando(paths, this);
                        sw.WriteLine(Properties.Resources.LogModulesDone);
                    }

                    if (DoRandomizeItems)       // Randomize Items
                    {
                        ReportProgress(bw, progress += stepSize, Properties.Resources.RandomizingItems, BusyState.Randomizing);
                        Randomize.RestartRng();
                        ItemRando.CreateItemBackups(paths);
                        ItemRando.item_rando(paths, this);
                        sw.WriteLine(Properties.Resources.LogItemsDone);
                    }

                    if (DoRandomizeAudio)       // Randomize Audio
                    {
                        ReportProgress(bw, progress += stepSize, Properties.Resources.RandomizingMusicSound, BusyState.Randomizing);
                        Randomize.RestartRng();

                        // If music files are to be randomized, create backups.
                        if (AudioAreaMusic != RandomizationLevel.None ||
                            AudioAmbientNoise != RandomizationLevel.None ||
                            AudioBattleMusic != RandomizationLevel.None ||
                            AudioCutsceneNoise != RandomizationLevel.None ||
                            AudioRemoveDmcaMusic)
                        {
                            SoundRando.CreateMusicBackups(paths);
                        }

                        // If sound files are to be randomized, create backups.
                        if (AudioAmbientNoise != RandomizationLevel.None ||
                            AudioBattleMusic != RandomizationLevel.None ||
                            AudioNpcSounds != RandomizationLevel.None ||
                            AudioPartySounds != RandomizationLevel.None)
                        {
                            SoundRando.CreateSoundBackups(paths);
                        }

                        SoundRando.sound_rando(paths, this);
                        sw.WriteLine(Properties.Resources.LogMusicSoundDone);
                    }

                    if (DoRandomizeModels)      // Randomize Cosmetics (Models)
                    {
                        ReportProgress(bw, progress += stepSize, Properties.Resources.RandomizingModels, BusyState.Randomizing);
                        Randomize.RestartRng();
                        ModelRando.CreateModelBackups(paths);
                        ModelRando.model_rando(paths, this);
                        sw.WriteLine(Properties.Resources.LogItemsDone);
                    }

                    if (DoRandomizeTextures)    // Randomize Cosmetics (Textures)
                    {
                        ReportProgress(bw, progress += stepSize, Properties.Resources.RandomizingTextures, BusyState.Randomizing);
                        Randomize.RestartRng();
                        TextureRando.CreateTextureBackups(paths);
                        TextureRando.texture_rando(paths, this);
                        sw.WriteLine(Properties.Resources.LogTexturesDone);
                    }

                    if (DoRandomizeTables)      // Randomize Tables
                    {
                        ReportProgress(bw, progress += stepSize, Properties.Resources.Randomizing2DA, BusyState.Randomizing);
                        Randomize.RestartRng();
                        TwodaRandom.CreateTwoDABackups(paths);
                        TwodaRandom.Twoda_rando(paths, this);
                        sw.WriteLine(Properties.Resources.Log2DADone);
                    }

                    if (DoRandomizeText)        // Randomize Text
                    {
                        ReportProgress(bw, progress += stepSize, Properties.Resources.RandomizingText, BusyState.Randomizing);
                        Randomize.RestartRng();
                        TextRando.CreateTextBackups(paths);
                        TextRando.text_rando(paths, this);
                        sw.WriteLine(Properties.Resources.LogTextDone);
                    }

                    if (DoRandomizeOther)       // Randomize Other
                    {
                        ReportProgress(bw, progress += stepSize, Properties.Resources.RandomizingOther, BusyState.Randomizing);
                        Randomize.RestartRng();
                        OtherRando.CreateOtherBackups(paths);
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
                    ReportProgress(bw, 100, Properties.Resources.TaskFinishing, BusyState.Randomizing);
                    sw.WriteLine("\nThe Kotor Randomizer was created by Lane Dibello, with help from Glasnonck, and the greater Kotor Speedrunning community.");
                    sw.WriteLine("If you encounter any issues please try to contact me @Lane#5847 on Discord");
                }
            }
        }

        private void DoSpoil(BackgroundWorker bw, string spoilersDirectory)
        {
            double progress = 0;
            double stepSize = 100.0 / (CountActiveCategories() + 1);

            var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");
            var filename = $"{timestamp}, Seed {Randomize.Seed}.xlsx";

            if (!Directory.Exists(spoilersDirectory))
            {
                var message = $"Spoilers directory \"{spoilersDirectory}\" doesn't exist. Spoiler will be saved at \"{Path.Combine(Environment.CurrentDirectory, filename)}\".";
                ReportProgress(bw, progress, message, BusyState.Spoiling);
                spoilersDirectory = Environment.CurrentDirectory;
            }

            var path = Path.Combine(spoilersDirectory, filename);
            if (File.Exists(path)) File.Delete(path);

            try
            {
                using (var workbook = new XLWorkbook())
                {
                    ReportProgress(bw, progress, "Starting to write spoiler logs.", BusyState.Spoiling);

                    if (DoRandomizeItems)
                    {
                        ReportProgress(bw, progress, "Item", BusyState.Spoiling, false);
                        ItemRando.CreateSpoilerLog(workbook);
                        progress += stepSize;
                    }

                    if (DoRandomizeModels)
                    {
                        ReportProgress(bw, progress, "Model", BusyState.Spoiling, false);
                        ModelRando.CreateSpoilerLog(workbook);
                        progress += stepSize;
                    }

                    if (DoRandomizeModules)
                    {
                        ReportProgress(bw, progress, "Module", BusyState.Spoiling, false);
                        ModuleRando.CreateSpoilerLog(workbook);
                        progress += stepSize;
                    }

                    if (DoRandomizeAudio)
                    {
                        ReportProgress(bw, progress, "Audio", BusyState.Spoiling, false);
                        SoundRando.CreateSpoilerLog(workbook);
                        progress += stepSize;
                    }

                    if (DoRandomizeOther)
                    {
                        ReportProgress(bw, progress, "Other", BusyState.Spoiling, false);
                        OtherRando.CreateSpoilerLog(workbook);
                        progress += stepSize;
                    }

                    if (DoRandomizeText)
                    {
                        ReportProgress(bw, progress, "Text", BusyState.Spoiling, false);
                        TextRando.CreateSpoilerLog(workbook);
                        progress += stepSize;
                    }

                    if (DoRandomizeTextures)
                    {
                        ReportProgress(bw, progress, "Texture", BusyState.Spoiling, false);
                        TextureRando.CreateSpoilerLog(workbook);
                        progress += stepSize;
                    }

                    if (DoRandomizeTables)
                    {
                        ReportProgress(bw, progress, "2DA", BusyState.Spoiling, false);
                        TwodaRandom.CreateSpoilerLog(workbook);
                        progress += stepSize;
                    }

                    ReportProgress(bw, progress, "Saving File", BusyState.Spoiling, false);

                    // If any worksheets have been added, save the spoiler log.
                    if (workbook.Worksheets.Count > 0) workbook.SaveAs(path);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception caught while creating spoilers: {ex.Message}", ex);
            }
            finally
            {
                ReportProgress(bw, 100, Properties.Resources.TaskFinishing, BusyState.Spoiling);
            }
        }

        private void ReportProgress(BackgroundWorker bw, double progress, string message, BusyState state, bool logMessage = true)
        {
            bw.ReportProgress(0, new RandoProgress()
            {
                IsRandomizing   = state == BusyState.Randomizing,
                IsSpoiling      = state == BusyState.Spoiling,
                IsUnrandomizing = state == BusyState.Unrandomizing,
                PercentComplete = progress,
                Status         = message,
                Log             = logMessage ? message : null,
            });
        }

        private void ResetRandomizationCategories()
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

        private void ReadKRPModules()
        {
            // Grab QoL from module extras.
            GeneralModuleExtrasValue = Properties.Settings.Default.ModuleExtrasValue & EXTRAS_MASK;

            // Move unlocks into the locked list.
            foreach (var item in GeneralUnlockedDoors) GeneralLockedDoors.Add(item);
            GeneralUnlockedDoors.Clear();

            // Grab unlocks from module extras.
            ModuleExtras unlocks = Properties.Settings.Default.ModuleExtrasValue & UNLOCKS_MASK;
            var toUnlock = GeneralLockedDoors.Where(du => unlocks.HasFlag(du.Tag));
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
            ModuleAllowGlitchHotshot   = true;
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
            var omitItems = ItemRandomizedList.Where(ri => Globals.OmitItems.Contains(ri.Code));
            foreach (var item in omitItems)
            {
                ItemOmittedList.Add(item);
                ItemRandomizedList.Remove(item);
            }
        }

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

        private void ReadKRPTwoDAs()
        {
            throw new NotImplementedException();
        }

        private void ReadKRPText()
        {
            TextSettingsValue = Properties.Settings.Default.TextSettingsValue;
        }

        private void ReadKRPOther()
        {
            throw new NotImplementedException();
        }

        private void ResetModules()
        {
            ModuleAllowGlitchClip      = false;
            ModuleAllowGlitchDlz       = false;
            ModuleAllowGlitchFlu       = false;
            ModuleAllowGlitchGpw       = false;
            ModuleAllowGlitchHotshot   = true;
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
            var omitItems = ItemRandomizedList.Where(ri => Globals.OMIT_ITEM_PRESETS.First().Value.Contains(ri.Code));
            foreach (var item in omitItems)
            {
                ItemOmittedList.Add(item);
                ItemRandomizedList.Remove(item);
            }

            ItemOmittedPreset = Globals.OMIT_ITEM_PRESETS.First().Key;
        }

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

        private void ResetModels()
        {
            ModelCharacterRando      = false;
            ModelCharacterOmitLarge  = false;
            ModelCharacterOmitBroken = false;

            ModelDoorRando           = false;
            ModelDoorOmitAirlock     = false;
            ModelDoorOmitBroken      = false;

            ModelPlaceableRando      = false;
            ModelPlaceableOmitLarge  = false;
            ModelPlaceableOmitBroken = false;
            ModelPlaceableEasyPanels = false;
        }

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

        private void ResetTwoDAs()
        {
            throw new NotImplementedException();
        }

        private void ResetText()
        {
            TextSettingsValue = TextSettings.Default;
        }

        private void ResetOther()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}