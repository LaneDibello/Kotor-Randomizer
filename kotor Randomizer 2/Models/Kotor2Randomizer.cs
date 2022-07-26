using ClosedXML.Excel;
using kotor_Randomizer_2.Digraph;
using kotor_Randomizer_2.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace kotor_Randomizer_2.Models
{
    public class Kotor2Randomizer : RandomizerBase, IGeneralSettings, IRandomizeModules, IRandomizeItems
    {
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
        private const string XML_KREIA          = "Kreia";
        private const string XML_LAST_NAME      = "Last";
        private const string XML_LIGHTSABER     = "Lightsaber";
        private const string XML_LOGIC          = "Logic";
        private const string XML_LOOP           = "Loop";
        private const string XML_MALAK          = "Malak";
        private const string XML_MAPS           = "StarMaps";
        private const string XML_MASK           = "Mask";
        private const string XML_MASTER         = "Master";
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
        private const string XML_SAVE_OPS       = "SaveOps";
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

        #region Area Names
        private const string AREA_CIT_ENTERTAIN = "202TEL";
        #endregion

        #region Constructors

        /// <summary>
        /// Constructs the randomizer with default settings.
        /// </summary>
        public Kotor2Randomizer() : this(string.Empty) { }

        /// <summary>
        /// Constructs the randomizer with settings read fromt he given preset file path.
        /// </summary>
        /// <param name="path">Full path to a randomizer preset file.</param>
        public Kotor2Randomizer(string path)
        {
            ModuleGoalList = new ObservableCollection<ReachabilityGoal>
            {
                new ReachabilityGoal { GoalID = 0, Caption = "Defeat Traya" },
                new ReachabilityGoal { GoalID = 1, Caption = "Defeat All Sith Lords" },
                new ReachabilityGoal { GoalID = 2, Caption = "Meet Jedi Masters" },
                new ReachabilityGoal { GoalID = 3, Caption = "Recruit Party Members" },
                new ReachabilityGoal { GoalID = 4, Caption = "Pazaak Champion" },
            };

            GeneralLockedDoors = new ObservableCollection<QualityOfLifeOption>
            {
                new QualityOfLifeOption(QualityOfLife.CO_GalaxyMap),                 // General
                new QualityOfLifeOption(QualityOfLife.K2_PreventDiscipleCrash),
                new QualityOfLifeOption(QualityOfLife.K2_CitResidential_AptDoor),    // Citadel Station
                new QualityOfLifeOption(QualityOfLife.K2_CitResidential_ToExchange),
                new QualityOfLifeOption(QualityOfLife.K2_CitStation_Terminals),
                new QualityOfLifeOption(QualityOfLife.K2_DanCourtyard_ToEnclave),    // Dantooine
                new QualityOfLifeOption(QualityOfLife.K2_DxnCamp_ToIziz),            // Dxun
                new QualityOfLifeOption(QualityOfLife.K2_DxnCamp_Basalisk),
                new QualityOfLifeOption(QualityOfLife.K2_DxnTomb_ToCamp),
                new QualityOfLifeOption(QualityOfLife.K2_KorAcademy_ToValley),       // Korriban
                new QualityOfLifeOption(QualityOfLife.K2_KorCave_ToTomb),
                new QualityOfLifeOption(QualityOfLife.K2_MalSurface_ToHawk),         // Malachor V
                new QualityOfLifeOption(QualityOfLife.K2_MalCore_ToAcademy),
                new QualityOfLifeOption(QualityOfLife.K2_NarDocks_ZezDoor),          // Nar Shaddaa
                new QualityOfLifeOption(QualityOfLife.K2_NarJekk_VipRoom),
                new QualityOfLifeOption(QualityOfLife.K2_NarTunnels_ToJekk),
                new QualityOfLifeOption(QualityOfLife.K2_NarYacht_ToHawk),
                new QualityOfLifeOption(QualityOfLife.K2_OndPort_ToCamp),            // Onderon
                new QualityOfLifeOption(QualityOfLife.K2_PerAdmin_ToDorms),          // Peragus
                new QualityOfLifeOption(QualityOfLife.K2_PerAdmin_ToTunnels),
                new QualityOfLifeOption(QualityOfLife.K2_PerAdmin_ToHarbinger),
                new QualityOfLifeOption(QualityOfLife.K2_PerAdmin_ToDepot),
                new QualityOfLifeOption(QualityOfLife.K2_PerDorms_ToExterior),
                new QualityOfLifeOption(QualityOfLife.K2_PerExterior_ToDorms),
                new QualityOfLifeOption(QualityOfLife.K2_PerDepot_ToTunnels),
                new QualityOfLifeOption(QualityOfLife.K2_PerDepot_ForceFields),
                new QualityOfLifeOption(QualityOfLife.K2_PerHangar_ToHawk),
                new QualityOfLifeOption(QualityOfLife.K2_TelAcademy_ToPlateau),      // Telos
                new QualityOfLifeOption(QualityOfLife.K2_TelAcademy_ToHawk),
                new QualityOfLifeOption(QualityOfLife.K2_TelBaoDurConvo),
                new QualityOfLifeOption(QualityOfLife.K2_WarEntertain_ToRavager),    // Wartime Telos
        };

            ModuleDigraph graph;
            var modulesPath = Path.Combine(Environment.CurrentDirectory, "Xml", "Kotor2Modules.xml");
            if (!File.Exists(modulesPath))
            {
                // Personal debug path... will not work for all developers.
                modulesPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"GitHub\Kotor-Randomizer-2\kotor Randomizer 2\Xml\Kotor2Modules.xml");
            }
            graph = new ModuleDigraph(modulesPath, Game.Kotor2);
            ModuleRandomizedList = new ObservableCollection<ModuleVertex>(graph.Modules);

            // Get list of randomizable items.
            ItemRandomizedList = new ObservableCollection<RandomizableItem>();
            ItemOmittedList = new ObservableCollection<RandomizableItem>();
            ItemOmittedPreset = string.Empty;
        }

        #endregion

        #region Private Methods

        public void ResetSettingsToDefault()
        {
            ResetGeneral();
            //ResetAudio();
            ResetItems();
            //ResetModels();
            ResetModules();
            //ResetOther();
            //ResetTables();
            //ResetText();
            //ResetTextures();
        }

        /// <summary>
        /// Reset general settings to defaults.
        /// </summary>
        private void ResetGeneral()
        {
            GeneralSaveOptions = SavePatchOptions.Default;
            foreach (var door in GeneralUnlockedDoors)
                GeneralLockedDoors.Add(door);
            GeneralUnlockedDoors.Clear();
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

            // Move all items to randomized list and clear omitted.
            foreach (var item in ItemOmittedList) ItemRandomizedList.Add(item);
            ItemOmittedList.Clear();

            //// Grab omitted list from globals.
            //var omitItems = ItemRandomizedList.Where(ri => RandomizableItem.KOTOR1_OMIT_PRESETS.First().Value.Contains(ri.Code)).ToList();
            //foreach (var item in omitItems)
            //{
            //    ItemOmittedList.Add(item);
            //    ItemRandomizedList.Remove(item);
            //}

            //ItemOmittedPreset = RandomizableItem.KOTOR1_OMIT_PRESETS.First().Key;
        }

        private void ResetModules()
        {
            GeneralSaveOptions = SavePatchOptions.Default;

            foreach (var item in GeneralUnlockedDoors) GeneralLockedDoors.Add(item);
            GeneralUnlockedDoors.Clear();

            //ModuleAllowGlitchClip      = false;
            //ModuleAllowGlitchDlz       = false;
            //ModuleAllowGlitchFlu       = false;
            //ModuleAllowGlitchGpw       = false;
            //ModuleLogicStrongGoals     = false;
            //ModuleGoalIsKreia          = true;
            //ModuleGoalIsMasters        = false;
            //ModuleGoalIsPazaak         = false;
            //ModuleGoalIsFullParty      = false;
            //ModuleLogicIgnoreOnceEdges = true;
            //ModuleLogicRandoRules      = true;
            //ModuleLogicReachability    = true;

            foreach (var item in ModuleRandomizedList) ModuleOmittedList.Add(item);
            ModuleRandomizedList.Clear();
            ModuleShufflePreset = Globals.K2_MODULE_OMIT_PRESETS.First().Key;
        }

        #endregion

        #region RandomizerBase Implementation

        public override Game Game => Game.Kotor2;
        public override string Extension => "xk2rp";

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
        /// Counts the number of active randomization categories.
        /// </summary>
        private int CountActiveCategories()
        {
            var count = 0;
            //if (DoRandomizeAudio) count++;
            if (DoRandomizeItems) count++;
            //if (DoRandomizeModels) count++;
            if (DoRandomizeModules) count++;
            //if (DoRandomizeOther) count++;
            //if (DoRandomizeParty    ) count++;
            //if (DoRandomizeText) count++;
            //if (DoRandomizeTextures) count++;
            //if (DoRandomizeTables) count++;
            return count;
        }

        /// <summary>
        /// Reports progress of the current busy state to the BackgroundWorker.
        /// </summary>
        /// <param name="bw">BackgroundWorker to report progress to.</param>
        /// <param name="progress">Percent progress on the current action.</param>
        /// <param name="state">BusyState of the randomizer.</param>
        /// <param name="status">Text to display as the current status.</param>
        /// <param name="message">Text to write to the log.</param>
        private void ReportProgress(BackgroundWorker bw, double progress, BusyState state, string status = null, string message = null)
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
        /// Run randomization methods as needed while reporting progress to the BackgroundWorker.
        /// </summary>
        /// <param name="bw"></param>
        /// <param name="paths"></param>
        private void DoRandomize(BackgroundWorker bw, KPaths paths)
        {
            if (CountActiveCategories() == 0) { throw new InvalidOperationException(Properties.Resources.ErrorNoRandomization); }
            BackUpGame(bw, paths);
            RandomizeGame(bw, paths);
        }

        private void BackUpGame(BackgroundWorker bw, KPaths paths)
        {
            var activeCategories = CountActiveCategories() + 2; // Add one for overrides and one for finishing up.
            var stepSize = 100.0 / activeCategories;  // Calculate step size.
            var progress = 0 - stepSize;

            try
            {
                ReportProgress(bw, progress, BusyState.BackingUp, message: "Backing up game files.");

                // Backup each category being randomized.
                var form = "... backing up {0}.";

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

                //if (DoRandomizeAudio)       // Back up Audio
                //{
                //    ReportProgress(bw, progress += stepSize, BusyState.BackingUp, CATEGORY_AUDIO, string.Format(form, CATEGORY_AUDIO));
                //    if (DoRandomizeMusic) { SoundRando.CreateMusicBackups(paths); }
                //    if (DoRandomizeSound) { SoundRando.CreateSoundBackups(paths); }
                //}

                //if (DoRandomizeModels)      // Back up Models (Cosmetics)
                //{
                //    ReportProgress(bw, progress += stepSize, BusyState.BackingUp, CATEGORY_MODELS, string.Format(form, CATEGORY_MODELS));
                //    ModelRando.CreateModelBackups(paths);
                //}

                //if (DoRandomizeTextures)    // Back up Textures (Cosmetics)
                //{
                //    ReportProgress(bw, progress += stepSize, BusyState.BackingUp, CATEGORY_TEXTURES, string.Format(form, CATEGORY_TEXTURES));
                //    TextureRando.CreateTextureBackups(paths);
                //}

                //if (DoRandomizeTables)      // Back up Tables
                //{
                //    ReportProgress(bw, progress += stepSize, BusyState.BackingUp, CATEGORY_TABLES, string.Format(form, CATEGORY_TABLES));
                //    TwodaRandom.CreateTwoDABackups(paths);
                //}

                //if (DoRandomizeText)        // Back up Text
                //{
                //    ReportProgress(bw, progress += stepSize, BusyState.BackingUp, CATEGORY_TEXT, string.Format(form, CATEGORY_TEXT));
                //    TextRando.CreateTextBackups(paths);
                //}

                //if (DoRandomizeOther)       // Back up Other
                //{
                //    ReportProgress(bw, progress += stepSize, BusyState.BackingUp, CATEGORY_OTHER, string.Format(form, CATEGORY_OTHER));
                //    OtherRando.CreateOtherBackups(paths);
                //}
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

                    //// Write appearance override.
                    //if (GeneralModuleExtrasValue.HasFlag(ModuleExtras.FastEnvirosuit))
                    //{
                    //    File.WriteAllBytes(Path.Combine(paths.Override, "appearance.2da"), Properties.Resources.appearance_speedysuit);
                    //}
                    //else
                    //{
                    //    File.WriteAllBytes(Path.Combine(paths.Override, "appearance.2da"), Properties.Resources.appearance);
                    //}

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

                    //if (DoRandomizeAudio)       // Randomize Audio
                    //{
                    //    Randomize.RestartRng();
                    //    ReportProgress(bw, progress += stepSize, BusyState.Randomizing, CATEGORY_AUDIO, string.Format(form, CATEGORY_AUDIO));
                    //    SoundRando.sound_rando(paths, this);
                    //    sw.WriteLine(Properties.Resources.LogMusicSoundDone);
                    //}

                    //if (DoRandomizeModels)      // Randomize Cosmetics (Models)
                    //{
                    //    Randomize.RestartRng();
                    //    ReportProgress(bw, progress += stepSize, BusyState.Randomizing, CATEGORY_MODELS, string.Format(form, CATEGORY_MODELS));
                    //    ModelRando.model_rando(paths, this);
                    //    sw.WriteLine(Properties.Resources.LogItemsDone);
                    //}

                    //if (DoRandomizeTextures)    // Randomize Cosmetics (Textures)
                    //{
                    //    Randomize.RestartRng();
                    //    ReportProgress(bw, progress += stepSize, BusyState.Randomizing, CATEGORY_TEXTURES, string.Format(form, CATEGORY_TEXTURES));
                    //    TextureRando.texture_rando(paths, this);
                    //    sw.WriteLine(Properties.Resources.LogTexturesDone);
                    //}

                    //if (DoRandomizeTables)      // Randomize Tables
                    //{
                    //    Randomize.RestartRng();
                    //    ReportProgress(bw, progress += stepSize, BusyState.Randomizing, CATEGORY_TABLES, string.Format(form, CATEGORY_TABLES));
                    //    TwodaRandom.Twoda_rando(paths, this);
                    //    sw.WriteLine(Properties.Resources.Log2DADone);
                    //}

                    //if (DoRandomizeText)        // Randomize Text
                    //{
                    //    Randomize.RestartRng();
                    //    ReportProgress(bw, progress += stepSize, BusyState.Randomizing, CATEGORY_TEXT, string.Format(form, CATEGORY_TEXT));
                    //    TextRando.text_rando(paths, this);
                    //    sw.WriteLine(Properties.Resources.LogTextDone);
                    //}

                    //if (DoRandomizeOther)       // Randomize Other
                    //{
                    //    Randomize.RestartRng();
                    //    ReportProgress(bw, progress += stepSize, BusyState.Randomizing, CATEGORY_OTHER, string.Format(form, CATEGORY_OTHER));
                    //    OtherRando.other_rando(paths, this);
                    //    sw.WriteLine(Properties.Resources.LogOtherDone);
                    //}
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error encountered during randomization: {ex.Message}", ex);
                }
                finally
                {
                    ReportProgress(bw, 100, BusyState.Randomizing, message: Properties.Resources.TaskFinishing);
                    sw.WriteLine("\nThe Kotor Randomizer was created by Lane and Glasnonck, with help from the greater Kotor Speedrunning community.");
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

                    //if (DoRandomizeModels)
                    //{
                    //    category = "Models";
                    //    ReportProgress(bw, progress += stepSize, BusyState.Spoiling, category, string.Format(spoilFormat, category));
                    //    ModelRando.CreateSpoilerLog(workbook);
                    //}

                    if (DoRandomizeModules)
                    {
                        category = "Modules";
                        ReportProgress(bw, progress += stepSize, BusyState.Spoiling, category, string.Format(spoilFormat, category));
                        ModuleRando.CreateSpoilerLog(workbook, false);
                    }

                    //if (DoRandomizeAudio)
                    //{
                    //    category = "Audio";
                    //    ReportProgress(bw, progress += stepSize, BusyState.Spoiling, category, string.Format(spoilFormat, category));
                    //    SoundRando.CreateSpoilerLog(workbook);
                    //}

                    //if (DoRandomizeOther)
                    //{
                    //    category = "Other";
                    //    ReportProgress(bw, progress += stepSize, BusyState.Spoiling, category, string.Format(spoilFormat, category));
                    //    OtherRando.CreateSpoilerLog(workbook);
                    //}

                    //if (DoRandomizeText)
                    //{
                    //    category = "Text";
                    //    ReportProgress(bw, progress += stepSize, BusyState.Spoiling, category, string.Format(spoilFormat, category));
                    //    TextRando.CreateSpoilerLog(workbook);
                    //}

                    //if (DoRandomizeTextures)
                    //{
                    //    category = "Textures";
                    //    ReportProgress(bw, progress += stepSize, BusyState.Spoiling, category, string.Format(spoilFormat, category));
                    //    TextureRando.CreateSpoilerLog(workbook);
                    //}

                    //if (DoRandomizeTables)
                    //{
                    //    category = "Tables";
                    //    ReportProgress(bw, progress += stepSize, BusyState.Spoiling, category, string.Format(spoilFormat, category));
                    //    TwodaRandom.CreateSpoilerLog(workbook);
                    //}

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

        protected override void ReadKRP(Stream s)
        {
            throw new NotImplementedException("KRP format is invalid for Kotor 2 Randomization.");
        }

        protected override void ReadFromFile(string path)
        {
            ResetSettingsToDefault();

            var doc = XDocument.Load(path);
            var element = doc.Descendants(XML_GENERAL).FirstOrDefault();
            if (element != null) ReadGeneralSettings(element);

            //element = doc.Descendants(XML_AUDIO).FirstOrDefault();
            //if (element != null) ReadAudioSettings(element);

            element = doc.Descendants(XML_ITEM).FirstOrDefault();
            if (element != null) ReadItemSettings(element);

            //element = doc.Descendants(XML_MODEL).FirstOrDefault();
            //if (element != null) ReadModelSettings(element);

            element = doc.Descendants(XML_MODULE).FirstOrDefault();
            if (element != null) ReadModuleSettings(element);

            //element = doc.Descendants(XML_OTHER).FirstOrDefault();
            //if (element != null) ReadOtherSettings(element);

            //element = doc.Descendants(XML_TABLES).FirstOrDefault();
            //if (element != null) ReadTableSettings(element);

            //element = doc.Descendants(XML_TEXT).FirstOrDefault();
            //if (element != null) ReadTextSettings(element);

            //element = doc.Descendants(XML_TEXTURE).FirstOrDefault();
            //if (element != null) ReadTextureSettings(element);
        }

        /// <summary>
        /// Read general settings from an XML file.
        /// </summary>
        /// <param name="element">XML element containing the general settings.</param>
        private void ReadGeneralSettings(XElement element)
        {
            { if (element.Attribute(XML_SAVE_OPS) is XAttribute attr) GeneralSaveOptions = ParseEnum<SavePatchOptions>(attr.Value); }

            {   // Extra block used to encapsulate the reused variable name "attr".
                if (element.Attribute(XML_QOL) is XAttribute attr)
                {
                    var qols = attr.Value
                        .Split(", ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                        .Select(s => (QualityOfLife)Enum.Parse(typeof(QualityOfLife), s));

                    foreach (var door in GeneralLockedDoors.ToList())   // Use a new list so we can modify this one in the loop.
                    {
                        if (qols.Any(qol => qol == door.QoL))   // If this "door" should be enabled, enable it.
                        {
                            GeneralUnlockedDoors.Add(door);
                            _ = GeneralLockedDoors.Remove(door);
                        }
                    }
                }
            }
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

            //// Read omitted item preset.
            ////var omit = element.Descendants(XML_OMIT).FirstOrDefault();
            //ItemOmittedPreset = element.Attribute(XML_PRESET)?.Value ?? null;

            //// If preset is null, read the list of omitted items.
            //if (ItemOmittedPreset is null)
            //{
            //    foreach (var i in element.Descendants(XML_OMIT))
            //    {
            //        var code = i.Attribute(XML_CODE).Value;
            //        var item = ItemRandomizedList.FirstOrDefault(x => x.Code == code);
            //        if (item != null)
            //        {
            //            ItemOmittedList.Add(item);
            //            ItemRandomizedList.Remove(item);
            //        }
            //    }
            //}
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
                { if (goals.Attribute(XML_KREIA ) is XAttribute attr) ModuleGoalIsKreia     = bool.Parse(attr.Value); }
                { if (goals.Attribute(XML_MASTER) is XAttribute attr) ModuleGoalIsMasters   = bool.Parse(attr.Value); }
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

        protected override void WriteToFile(string path)
        {
            using (var w = new XmlTextWriter(path, null))
            {
                w.WriteStartDocument();
                w.WriteStartElement(XML_SETTINGS); // Begin Settings

                WriteGeneralSettings(w);

                //if (DoRandomizeAudio   ) WriteAudioSettings(w);
                if (DoRandomizeItems   ) WriteItemSettings(w);
                //if (DoRandomizeModels  ) WriteModelSettings(w);
                if (DoRandomizeModules ) WriteModuleSettings(w);
                //if (DoRandomizeOther   ) WriteOtherSettings(w);
                //if (DoRandomizeParty   ) WritePartySettings(w);   // Not yet implemented.
                //if (DoRandomizeTables  ) WriteTableSettings(w);
                //if (DoRandomizeText    ) WriteTextSettings(w);
                //if (DoRandomizeTextures) WriteTextureSettings(w);

                w.WriteEndElement();                // End Settings
                w.WriteEndDocument();
                w.Flush();
            }
        }

        /// <summary>
        /// Write General settings to an XML file.
        /// </summary>
        /// <param name="w"></param>
        private void WriteGeneralSettings(XmlTextWriter w)
        {
            w.WriteStartElement(XML_GENERAL);  // Begin General

            var v = System.Reflection.Assembly.GetAssembly(typeof(Kotor1Randomizer)).GetName().Version;
            w.WriteAttributeString(XML_VERSION, $"v{v.Major}.{v.Minor}.{v.Build}");

            //if (GeneralSaveOptions != SavePatchOptions.Default)
            w.WriteAttributeString(XML_SAVE_OPS, ((int)GeneralSaveOptions).ToString()); // Always write save options.

            var qols = GeneralUnlockedDoors.Select(d => (int)d.QoL).ToList();
            qols.Sort();
            if (qols.Any())
                w.WriteAttributeString(XML_QOL, string.Join(", ", qols));

            w.WriteEndElement();                // End General
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

            //if (!string.IsNullOrWhiteSpace(ItemOmittedPreset))
            //{
            //    // Write omit preset string.
            //    w.WriteAttributeString(XML_PRESET, ItemOmittedPreset);
            //}
            //else
            //{
            //    // Write each omitted item to the file.
            //    foreach (var item in ItemOmittedList)
            //    {
            //        w.WriteStartElement(XML_OMIT);  // Begin Omit
            //        w.WriteAttributeString(XML_CODE, item.Code);
            //        w.WriteEndElement();            // End Omit
            //    }
            //}

            w.WriteEndElement();                // End Item
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
            w.WriteAttributeString(XML_KREIA,   ModuleGoalIsKreia.ToString());
            w.WriteAttributeString(XML_MASTER,  ModuleGoalIsMasters.ToString());
            w.WriteAttributeString(XML_PAZAAK,  ModuleGoalIsPazaak.ToString());
            w.WriteAttributeString(XML_PARTY,   ModuleGoalIsFullParty.ToString());
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

        #endregion

        #region IGeneralSettings Implementation

        #region Backing Fields
        private SavePatchOptions _generalSaveOptions;
        private ObservableCollection<QualityOfLifeOption> _generalUnlockedDoors = new ObservableCollection<QualityOfLifeOption>();
        private ObservableCollection<QualityOfLifeOption> _generalLockedDoors = new ObservableCollection<QualityOfLifeOption>();
        #endregion

        public Dictionary<string, Tuple<float, float, float>> FixedCoordinates => new Dictionary<string, Tuple<float, float, float>>()
        {
            { AREA_CIT_ENTERTAIN, new Tuple<float, float, float>(-13.5f, -63.4f,  11.51f) },
        };

        public SavePatchOptions GeneralSaveOptions
        {
            get => _generalSaveOptions;
            set => SetField(ref _generalSaveOptions, value);
        }

        public ObservableCollection<QualityOfLifeOption> GeneralUnlockedDoors
        {
            get => _generalUnlockedDoors;
            set => SetField(ref _generalUnlockedDoors, value);
        }

        public ObservableCollection<QualityOfLifeOption> GeneralLockedDoors
        {
            get => _generalLockedDoors;
            set => SetField(ref _generalLockedDoors, value);
        }

        #endregion

        #region IRandomizeModules Implementation

        #region Backing Fields
        private bool _moduleAllowGlitchClip;
        private bool _moduleAllowGlitchDlz;
        private bool _moduleAllowGlitchFlu;
        private bool _moduleAllowGlitchGpw;
        private bool _moduleLogicStrongGoals;
        private bool _moduleLogicIgnoreOnceEdges;
        private bool _moduleLogicRandoRules;
        private bool _moduleLogicReachability;
        private ObservableCollection<ReachabilityGoal> _moduleGoalList;
        private bool _moduleGoalIsKreia = true;
        private bool _moduleGoalIsJediMasters;
        private bool _moduleGoalIsPazaak;
        private bool _moduleGoalIsFullParty;
        private ObservableCollection<ModuleVertex> _moduleRandomizedList = new ObservableCollection<ModuleVertex>();
        private ObservableCollection<ModuleVertex> _moduleOmittedList = new ObservableCollection<ModuleVertex>();
        private ObservableCollection<string> _modulePresetOptions = new ObservableCollection<string>(Globals.K2_MODULE_OMIT_PRESETS.Keys);
        private string _moduleShufflePreset = "Default";
        #endregion

        public override bool SupportsModules => true;

        public bool DoRandomizeModules =>   // A couple of general options are handled by module randomization.
            (ModuleRandomizedList?.Count ?? 0) > 1
            || GeneralSaveOptions != SavePatchOptions.Default
            || GeneralUnlockedDoors.Count != 0
            ;

        public bool ModuleAllowGlitchClip
        {
            get => _moduleAllowGlitchClip;
            set => SetField(ref _moduleAllowGlitchClip, value);
        }

        public bool ModuleAllowGlitchDlz
        {
            get => _moduleAllowGlitchDlz;
            set => SetField(ref _moduleAllowGlitchDlz, value);
        }

        public bool ModuleAllowGlitchFlu
        {
            get => _moduleAllowGlitchFlu;
            set => SetField(ref _moduleAllowGlitchFlu, value);
        }

        public bool ModuleAllowGlitchGpw
        {
            get => _moduleAllowGlitchGpw;
            set => SetField(ref _moduleAllowGlitchGpw, value);
        }

        public bool ModuleLogicStrongGoals
        {
            get => _moduleLogicStrongGoals;
            set => SetField(ref _moduleLogicStrongGoals, value);
        }

        public bool ModuleLogicIgnoreOnceEdges
        {
            get => _moduleLogicIgnoreOnceEdges;
            set => SetField(ref _moduleLogicIgnoreOnceEdges, value);
        }

        public bool ModuleLogicRandoRules
        {
            get => _moduleLogicRandoRules;
            set => SetField(ref _moduleLogicRandoRules, value);
        }

        public bool ModuleLogicReachability
        {
            get => _moduleLogicReachability;
            set => SetField(ref _moduleLogicReachability, value);
        }

        public ObservableCollection<ReachabilityGoal> ModuleGoalList
        {
            get => _moduleGoalList;
            set => SetField(ref _moduleGoalList, value);
        }

        public bool ModuleGoalIsKreia
        {
            get => _moduleGoalIsKreia;
            set => SetField(ref _moduleGoalIsKreia, value);
        }

        public bool ModuleGoalIsMasters
        {
            get => _moduleGoalIsJediMasters;
            set => SetField(ref _moduleGoalIsJediMasters, value);
        }

        public bool ModuleGoalIsPazaak
        {
            get => _moduleGoalIsPazaak;
            set => SetField(ref _moduleGoalIsPazaak, value);
        }

        public bool ModuleGoalIsFullParty
        {
            get => _moduleGoalIsFullParty;
            set => SetField(ref _moduleGoalIsFullParty, value);
        }

        public ObservableCollection<ModuleVertex> ModuleRandomizedList
        {
            get => _moduleRandomizedList;
            set => SetField(ref _moduleRandomizedList, value);
        }

        public ObservableCollection<ModuleVertex> ModuleOmittedList
        {
            get => _moduleOmittedList;
            set => SetField(ref _moduleOmittedList, value);
        }

        public string ModuleShufflePreset
        {
            get => _moduleShufflePreset;
            set => SetField(ref _moduleShufflePreset, value);
        }

        public ObservableCollection<string> ModulePresetOptions
        {
            get => _modulePresetOptions;
            set => SetField(ref _modulePresetOptions, value);
        }

        public Dictionary<string, List<string>> ModuleOmitPresets => Globals.K2_MODULE_OMIT_PRESETS;

        #endregion

        #region IRandomizeItems Implementation

        #region Backing Fields
        private RandomizationLevel _itemArmbands;
        private RandomizationLevel _itemArmor;
        private RandomizationLevel _itemBelts;
        private RandomizationLevel _itemBlasters;
        private RandomizationLevel _itemCreatureHides;
        private RandomizationLevel _itemCreatureWeapons;
        private RandomizationLevel _itemDroidEquipment;
        private RandomizationLevel _itemGloves;
        private RandomizationLevel _itemGrenades;
        private RandomizationLevel _itemImplants;
        private RandomizationLevel _itemLightsabers;
        private RandomizationLevel _itemMasks;
        private RandomizationLevel _itemMeleeWeapons;
        private RandomizationLevel _itemMines;
        private RandomizationLevel _itemPazaakCards;
        private RandomizationLevel _itemMedical;
        private RandomizationLevel _itemUpgrades;
        private RandomizationLevel _itemVarious;
        private ObservableCollection<RandomizableItem> _itemOmittedList;
        private ObservableCollection<RandomizableItem> _itemRandomizedList;
        private string _itemOmittedPreset;
        #endregion

        public override bool SupportsItems => false;

        public bool DoRandomizeItems =>
            (ItemArmbands       | ItemArmor           | ItemBelts          | ItemBlasters
            | ItemCreatureHides | ItemCreatureWeapons | ItemDroidEquipment | ItemGloves
            | ItemGrenades      | ItemImplants        | ItemLightsabers    | ItemMasks
            | ItemMedical       | ItemMeleeWeapons    | ItemMines          | ItemPazaakCards
            | ItemUpgrades      | ItemVarious)
            != RandomizationLevel.None;

        public RandomizationLevel ItemArmbands
        {
            get => _itemArmbands;
            set => SetField(ref _itemArmbands, value);
        }

        public RandomizationLevel ItemArmor
        {
            get => _itemArmor;
            set => SetField(ref _itemArmor, value);
        }

        public RandomizationLevel ItemBelts
        {
            get => _itemBelts;
            set => SetField(ref _itemBelts, value);
        }

        public RandomizationLevel ItemBlasters
        {
            get => _itemBlasters;
            set => SetField(ref _itemBlasters, value);
        }

        public RandomizationLevel ItemCreatureHides
        {
            get => _itemCreatureHides;
            set => SetField(ref _itemCreatureHides, value);
        }

        public RandomizationLevel ItemCreatureWeapons
        {
            get => _itemCreatureWeapons;
            set => SetField(ref _itemCreatureWeapons, value);
        }

        public RandomizationLevel ItemDroidEquipment
        {
            get => _itemDroidEquipment;
            set => SetField(ref _itemDroidEquipment, value);
        }

        public RandomizationLevel ItemGloves
        {
            get => _itemGloves;
            set => SetField(ref _itemGloves, value);
        }

        public RandomizationLevel ItemGrenades
        {
            get => _itemGrenades;
            set => SetField(ref _itemGrenades, value);
        }

        public RandomizationLevel ItemImplants
        {
            get => _itemImplants;
            set => SetField(ref _itemImplants, value);
        }

        public RandomizationLevel ItemLightsabers
        {
            get => _itemLightsabers;
            set => SetField(ref _itemLightsabers, value);
        }

        public RandomizationLevel ItemMasks
        {
            get => _itemMasks;
            set => SetField(ref _itemMasks, value);
        }

        public RandomizationLevel ItemMeleeWeapons
        {
            get => _itemMeleeWeapons;
            set => SetField(ref _itemMeleeWeapons, value);
        }

        public RandomizationLevel ItemMines
        {
            get => _itemMines;
            set => SetField(ref _itemMines, value);
        }

        public RandomizationLevel ItemPazaakCards
        {
            get => _itemPazaakCards;
            set => SetField(ref _itemPazaakCards, value);
        }

        public RandomizationLevel ItemMedical
        {
            get => _itemMedical;
            set => SetField(ref _itemMedical, value);
        }

        public RandomizationLevel ItemUpgrades
        {
            get => _itemUpgrades;
            set => SetField(ref _itemUpgrades, value);
        }

        public RandomizationLevel ItemVarious
        {
            get => _itemVarious;
            set => SetField(ref _itemVarious, value);
        }

        public ObservableCollection<RandomizableItem> ItemOmittedList
        {
            get => _itemOmittedList;
            set => SetField(ref _itemOmittedList, value);
        }

        public ObservableCollection<RandomizableItem> ItemRandomizedList
        {
            get => _itemRandomizedList;
            set => SetField(ref _itemRandomizedList, value);
        }

        public string ItemOmittedPreset
        {
            get => _itemOmittedPreset;
            set => SetField(ref _itemOmittedPreset, value);
        }

        #endregion
    }
}
