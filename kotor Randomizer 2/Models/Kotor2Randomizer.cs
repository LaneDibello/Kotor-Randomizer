using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kotor_Randomizer_2.Models
{
    public class Kotor2Randomizer : RandomizerBase, IGeneralSettings, IRandomizeModules, IRandomizeItems
    {
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
                new ReachabilityGoal { GoalID = 1, Caption = "Meet Jedi Masters" },
                new ReachabilityGoal { GoalID = 2, Caption = "Recruit Party Members" },
                new ReachabilityGoal { GoalID = 3, Caption = "Pazaak Champion" },
            };

            GeneralLockedDoors = new ObservableCollection<UnlockableDoor>
            {
                new UnlockableDoor
                {
                    Area = "PER", Label = "Admin to Dorms", Tag = ModuleExtras.K2Door_PerAdmin_ToDorms,
                    ToolTipMessage = "Unlocks the door leading from Administration to the Dormitories."
                },
                new UnlockableDoor
                {
                    Area = "PER", Label = "Admin to Tunnels", Tag = ModuleExtras.K2Door_PerAdmin_ToTunnels,
                    ToolTipMessage = "Unlocks the door leading from Administration to the Mining Tunnels."
                },
                new UnlockableDoor
                {
                    Area = "PER", Label = "Admin to Harbinger", Tag = ModuleExtras.K2Door_PerAdmin_ToHarbinger,
                    ToolTipMessage = "Unlocks the door leading from Administration to the Harbinger Command Deck."
                },
                new UnlockableDoor
                {
                    Area = "PER", Label = "Admin to Depot", Tag = ModuleExtras.K2Door_PerAdmin_ToDepot,
                    ToolTipMessage = "Unlocks the door leading from Administration to the Fuel Depot."
                },
                new UnlockableDoor
                {
                    Area = "PER", Label = "Dorms to Exterior", Tag = ModuleExtras.K2Door_PerDorms_ToAsteroid,
                    ToolTipMessage = "Unlocks the door leading from the Dormitories to the Asteroid Exterior."
                },
                new UnlockableDoor
                {
                    Area = "PER", Label = "Exterior to Dorms", Tag = ModuleExtras.K2Patch_PerAsteroid_ToTunnels,
                    ToolTipMessage = "Adds a loading zone leading from the Asteroid Exterior to the Dormitories."
                },
                new UnlockableDoor
                {
                    Area = "PER", Label = "Depot to Tunnels", Tag = ModuleExtras.K2Door_PerDepot_ToTunnels,
                    ToolTipMessage = "Unlocks the door leading from the Fuel Depot to the Mining Tunnels."
                },
                new UnlockableDoor
                {
                    Area = "PER", Label = "Depot Force Fields", Tag = ModuleExtras.K2Door_PerDepot_ForceFields,
                    ToolTipMessage = "Unlocks the force fields inside the fuel depot."
                },
                new UnlockableDoor
                {
                    Area = "PER", Label = "Hangar to Ebon Hawk", Tag = ModuleExtras.K2Door_PerHangar_ToHawk,
                    ToolTipMessage = "Unlocks the door leading from the Hangar to the Ebon Hawk."
                },
                new UnlockableDoor
                {
                    Area = "CIT", Label = "Residential Apartment Door", Tag = ModuleExtras.K2Door_CitResidential_AptDoor,
                    ToolTipMessage = "Unlocks the door preventing you from leaving your apartment in the Citadel Station Residential District."
                },
                new UnlockableDoor
                {
                    Area = "CIT", Label = "Residential to Exchange Corp", Tag = ModuleExtras.K2Door_CitResidential_ToExchange,
                    ToolTipMessage = "Unlocks the door leading from the Residential District to the Bumani Exchange Corp."
                },
                new UnlockableDoor
                {
                    Area = "CIT", Label = "Unlock Info Terminals", Tag = ModuleExtras.K2Patch_CitTerminals,
                    ToolTipMessage = "Unlocks access to all destinations of the info terminals."
                },
                new UnlockableDoor
                {
                    Area = "TEL", Label = "Academy to Plateau", Tag = ModuleExtras.K2Door_TelAcademy_ToPlateau,
                    ToolTipMessage = "Unlocks the door leading from Secret Academy to the Polar Plateau."
                },
                new UnlockableDoor
                {
                    Area = "TEL", Label = "Academy to Ebon Hawk", Tag = ModuleExtras.K2Patch_TelAcademy_ToHawk,
                    ToolTipMessage = "Unlocks the door leading from the Polar Academy to the Ebon Hawk."
                },
                new UnlockableDoor
                {
                    Area = "TEL", Label = "Patch Bao Dur Conversation", Tag = ModuleExtras.K2Patch_TelBaoDurConvo,
                    ToolTipMessage = "Patches the Bao Dur intro convo when landing on Telos."
                },
                new UnlockableDoor
                {
                    Area = "NAR", Label = "Docks Zez Kai El's Door", Tag = ModuleExtras.K2Door_NarDocks_ZezDoor,
                    ToolTipMessage = "Unlocks the door of Zez Kai El's apartment."
                },
                new UnlockableDoor
                {
                    Area = "NAR", Label = "Jekk'Jekk VIP Room", Tag = ModuleExtras.K2Door_NarJekk_VipRoom,
                    ToolTipMessage = "Unlocks the door to the VIP room of the Jekk'Jekk Tarr."
                },
                new UnlockableDoor
                {
                    Area = "NAR", Label = "Jekk Tunnels to Jekk'Jekk", Tag = ModuleExtras.K2Door_NarTunnels_ToJekk,
                    ToolTipMessage = "Unlocks the door leading from the Jekk'Jekk Tarr Tunnels to the Jekk'Jekk Tarr."
                },
                new UnlockableDoor
                {
                    Area = "NAR", Label = "G0T0's Yacht to Broken Hawk", Tag = ModuleExtras.K2Door_NarYacht_ToHawk,
                    ToolTipMessage = "Unlocks the door leading from G0T0's Yacht to a broken version of the Ebon Hawk."
                },
                new UnlockableDoor
                {
                    Area = "DXN", Label = "Camp to Onderon", Tag = ModuleExtras.K2Patch_DxnCamp_ToIziz,
                    ToolTipMessage = "Unlocks the door leading from the Mandalorian Camp to the Onderon Docking Bay."
                },
                new UnlockableDoor
                {
                    Area = "DXN", Label = "Camp to Wartime Onderon", Tag = ModuleExtras.K2Door_DxnMando_Basalisk,
                    ToolTipMessage = "Unlocks the door leading from the Mandalorian Camp to the Wartime Onderon."
                },
                new UnlockableDoor
                {
                    Area = "DXN", Label = "Tomb to Camp", Tag = ModuleExtras.K2_DxnTomb_ToMando,
                    ToolTipMessage = "Enables the loading zone from Freedon Nadd's Tomb to the Mandalorian Camp."
                },
                new UnlockableDoor
                {
                    Area = "DAN", Label = "Courtyard to Rebuilt Enclave", Tag = ModuleExtras.K2Door_DanCourtyard_ToEnclave,
                    ToolTipMessage = "Unlocks the door leading from the Courtyard to the Rebuilt Enclave."
                },
                new UnlockableDoor
                {
                    Area = "KOR", Label = "Academy to Valley", Tag = ModuleExtras.K2Door_KorAcademy_ToValley,
                    ToolTipMessage = "Unlocks the door leading from the Sith Academy to the Valley of the Dark Lords."
                },
                new UnlockableDoor
                {
                    Area = "KOR", Label = "Cave to Tomb", Tag = ModuleExtras.K2Door_KorCave_ToTomb,
                    ToolTipMessage = "Deactivates the trigger preventing entry into the Secret Tomb within the Shyrack Cave."
                },
                new UnlockableDoor
                {
                    Area = "WAR", Label = "Entertainment to Ravager", Tag = ModuleExtras.K2Door_WarEntertain_ToRavager,
                    ToolTipMessage = "Unlocks the door leading from the Wartime Entertainment Module to the Ravager."
                },
                new UnlockableDoor
                {
                    Area = "MAL", Label = "Surface to Ebon Hawk", Tag = ModuleExtras.K2Patch_MalSurface_ToHawk,
                    ToolTipMessage = "Adds an elevator and a loading zone from Malachor V Surface to the Ebon Hawk."
                },
                new UnlockableDoor
                {
                    Area = "MAL", Label = "Core to Academy", Tag = ModuleExtras.K2_MalCore_ToAcademy,
                    ToolTipMessage = "Adds a transition from the Trayus Core to the Trayus Academy."
                },
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

        #region RandomizerBase Implementation

        public override Game Game => Game.Kotor2;

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
            throw new NotImplementedException();
        }

        protected override void ReadFromFile(string path)
        {
            throw new NotImplementedException();
        }

        protected override void WriteToFile(string path)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IRandomizeGeneral Implementation

        #region Backing Fields
        private ModuleExtras _generalModuleExtrasValue;
        private ObservableCollection<UnlockableDoor> _generalUnlockedDoors = new ObservableCollection<UnlockableDoor>();
        private ObservableCollection<UnlockableDoor> _generalLockedDoors = new ObservableCollection<UnlockableDoor>();
        #endregion

        public Dictionary<string, Tuple<float, float, float>> FixedCoordinates => new Dictionary<string, Tuple<float, float, float>>()
        {
            { AREA_CIT_ENTERTAIN, new Tuple<float, float, float>(-13.5f, -63.4f,  11.51f) },
        };

        public ModuleExtras GeneralModuleExtrasValue
        {
            get => _generalModuleExtrasValue;
            set => SetField(ref _generalModuleExtrasValue, value);
        }

        public ObservableCollection<UnlockableDoor> GeneralUnlockedDoors
        {
            get => _generalUnlockedDoors;
            set => SetField(ref _generalUnlockedDoors, value);
        }

        public ObservableCollection<UnlockableDoor> GeneralLockedDoors
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
            || GeneralModuleExtrasValue   != ModuleExtras.Default
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

        public override bool SupportsItems => true;

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
