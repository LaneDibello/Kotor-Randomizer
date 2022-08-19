using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;
using ClosedXML.Excel;
using kotor_Randomizer_2.Extensions;
using System.Diagnostics;
using System.Threading.Tasks;
using kotor_Randomizer_2.DTOs;

namespace kotor_Randomizer_2
{
    //This has absically been copied verbatim from what Glasnonck coded in the last rando, could probably be cleaned up, but I cannot be bothered 
    class SoundRando
    {
        private static Dictionary<string, string> MusicLookupTable { get; set; } = new Dictionary<string, string>();
        private static Dictionary<string, string> SoundLookupTable { get; set; } = new Dictionary<string, string>();

        //private static RandomizationLevel RandomizeAmbientNoise { get; set; }
        //private static RandomizationLevel RandomizeAreaMusic { get; set; }
        //private static RandomizationLevel RandomizeBattleMusic { get; set; }
        //private static RandomizationLevel RandomizeCutsceneNoise { get; set; }
        //private static RandomizationLevel RandomizeNpcSounds { get; set; }
        //private static RandomizationLevel RandomizePartySounds { get; set; }
        private static List<AudioRandoCategoryOption> AudioOptions { get; set; }

        private static bool MixKotorGameMusic { get; set; }
        private static bool MixNpcAndPartySounds { get; set; }
        private static bool RemoveDmcaMusic { get; set; }

        /// <summary>
        /// Creates backups for music files modified during this randomization.
        /// </summary>
        /// <param name="paths"></param>
        internal static void CreateMusicBackups(KPaths paths)
        {
            paths.BackUpMusicDirectory();
        }

        /// <summary>
        /// Creates backups for sound files modified during this randomization.
        /// </summary>
        /// <param name="paths"></param>
        internal static void CreateSoundBackups(KPaths paths)
        {
            paths.BackUpSoundDirectory();
        }

        public static void sound_rando(KPaths paths, Models.Kotor1Randomizer k1rando = null)
        {
            // Prepare for new randomization.
            Reset();
            AssignSettings(k1rando);

            // Get file collections
            var tasks = new List<Task>();   // List of tasks to run in parallel.
            var maxMusic = new List<FileInfo>();    // List to shuffle all music at max level.
            var maxSound = new List<FileInfo>();    // List to shuffle all sound at max level.
            var musicFiles = new List<FileInfo>();  // List of all files in StreamMusic.
            var soundFiles = new List<FileInfo>();  // List of all files in StreamSounds.
            if (Directory.Exists(paths.music_backup)) musicFiles = paths.FilesInMusicBackup.ToList();
            if (Directory.Exists(paths.sounds_backup)) soundFiles = paths.FilesInSoundsBackup.ToList();

            // DEBUG: Time the randomization process.
            var sw = new Stopwatch();
            sw.Start();

            var music = new Dictionary<AudioRandoCategory, List<FileInfo>>();
            var sound = new Dictionary<AudioRandoCategory, List<FileInfo>>();
            foreach (var op in AudioOptions)
            {
                music[op.Category] = new List<FileInfo>();
                sound[op.Category] = new List<FileInfo>();
            }

            // TODO: Test this code ... not sure if it'll work.
            AudioOptions.AsParallel().ForAll((op) =>
            {
                //music[op.Category] = new List<FileInfo>();
                //sound[op.Category] = new List<FileInfo>();

                //foreach (var prefix in op.Prefixes)
                //    music[op.Category].AddRange(musicFiles.Where(f => f.Name.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)));
                if (op.Folders.HasFlag(AudioFolders.Music)) music[op.Category].AddRange(musicFiles.Where(f => op.Regex.IsMatch(f.Name)));
                if (op.Folders.HasFlag(AudioFolders.Sounds)) sound[op.Category].AddRange(soundFiles.Where(f => op.Regex.IsMatch(f.Name)));

                // Remove DMCA music.
                if (RemoveDmcaMusic && op.Category == AudioRandoCategory.AreaMusic)
                    _ = music[op.Category].RemoveAll(f => DmcaAreaMusic.Contains(f.Name));

                switch (op.Level)
                {
                    // Move to max lists.
                    case RandomizationLevel.Max:
                        if (music[op.Category].Any()) maxMusic.AddRange(music[op.Category]);
                        if (sound[op.Category].Any()) maxSound.AddRange(sound[op.Category]);
                        break;

                    // Randomize category files in a Task.
                    case RandomizationLevel.Type:
                        if (music[op.Category].Any())
                        {
                            var randMusic = Randomize.RandomizeFiles(music[op.Category], paths.music);
                            AddToMusicLookup(music[op.Category], randMusic);
                        }
                        if (sound[op.Category].Any())
                        {
                            var randSound = Randomize.RandomizeFiles(sound[op.Category], paths.sounds);
                            AddToSoundLookup(sound[op.Category], randSound);
                        }
                        break;

                    // Do nothing.
                    case RandomizationLevel.Subtype:
                    case RandomizationLevel.None:
                    default:
                        break;
                }

                Console.WriteLine($"{op.Category.ToLabel()} Done!");
            });

            Console.WriteLine($"Time to randomize categories: {sw.Elapsed}");
            sw.Restart();

            ////foreach (var op in AudioOptions)
            ////{
            ////    music[op.Category] = new List<FileInfo>();
            ////    sound[op.Category] = new List<FileInfo>();

            ////    //foreach (var prefix in op.Prefixes)
            ////    //    music[op.Category].AddRange(musicFiles.Where(f => f.Name.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)));
            ////    if (op.Folders.HasFlag(AudioFolders.Music))  music[op.Category].AddRange(musicFiles.Where(f => op.Regex.IsMatch(f.Name)));
            ////    if (op.Folders.HasFlag(AudioFolders.Sounds)) sound[op.Category].AddRange(soundFiles.Where(f => op.Regex.IsMatch(f.Name)));

            ////    // Remove DMCA music.
            ////    if (op.Category == AudioRandoCategory.AreaMusic)
            ////        _ = music[op.Category].RemoveAll(f => DmcaAreaMusic.Contains(f.Name));

            ////    switch (op.Level)
            ////    {
            ////        // Move to max lists.
            ////        case RandomizationLevel.Max:
            ////            maxMusic.AddRange(music[op.Category]);
            ////            maxSound.AddRange(sound[op.Category]);
            ////            break;

            ////        // Randomize category files in a Task.
            ////        case RandomizationLevel.Type:
            ////            tasks.Add(Task.Run(() =>
            ////            {
            ////                var randList = Randomize.RandomizeFiles(music[op.Category], paths.music);
            ////                AddToMusicLookup(music[op.Category], randList);
            ////            }));
            ////            break;

            ////        // Do nothing.
            ////        case RandomizationLevel.Subtype:
            ////        case RandomizationLevel.None:
            ////        default:
            ////            break;
            ////    }
            ////}

            ///**** Shuffle Area Music ****/
            //// Find all files in StreamMusic that have a prefix found in PrefixListAreaMusic.
            //var areaMusic = new List<FileInfo>();
            //foreach (var prefix in PrefixListAreaMusic)
            //    areaMusic.AddRange(musicFiles.Where(f => f.Name.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)));

            //// If requested, remove any music likely to trigger a DMCA strike.
            //if (RemoveDmcaMusic)
            //    _ = areaMusic.RemoveAll(f => DmcaAreaMusic.Contains(f.Name));

            //// Based on randomization level ...
            //switch (RandomizeAreaMusic)
            //{
            //    // Move to max music list.
            //    case RandomizationLevel.Max:
            //        maxMusic.AddRange(areaMusic);
            //        break;

            //    // Randomize area music files in a Task.
            //    case RandomizationLevel.Type:
            //        tasks.Add(Task.Run(() =>
            //        {
            //            var randList = Randomize.RandomizeFiles(areaMusic, paths.music);
            //            AddToMusicLookup(areaMusic, randList);
            //        }));
            //        break;

            //    // Do nothing.
            //    case RandomizationLevel.Subtype:
            //    case RandomizationLevel.None:
            //    default:
            //        break;
            //}

            //Console.WriteLine($"music randomized in {sw.Elapsed}");
            //sw.Restart();

            ///**** Shuffle Ambient Noise ****/
            //// Find all files in StreamMusic that have a prefix found in PrefixListNoise.
            //var ambientNoiseMusic = new List<FileInfo>();
            //foreach (var prefix in PrefixListNoise)
            //    ambientNoiseMusic.AddRange(musicFiles.Where(f => f.Name.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)));

            //// Find all files in StreamSounds that have a prefix found in PrefixListNoise.
            //var ambientNoiseSound = new List<FileInfo>();
            //foreach (var prefix in PrefixListNoise)
            //    ambientNoiseSound.AddRange(soundFiles.Where(f => f.Name.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)));

            //// Based on randomization level ...
            //switch (RandomizeAmbientNoise)
            //{
            //    // Move files in StreamMusic into max music and move files in StreamSounds into max sound.
            //    case RandomizationLevel.Max:
            //        maxMusic.AddRange(ambientNoiseMusic);
            //        maxSound.AddRange(ambientNoiseSound);
            //        break;

            //    // Randomize noise in music and noise in sounds in a Task.
            //    case RandomizationLevel.Type:
            //        tasks.Add(Task.Run(() =>
            //        {
            //            var randList = Randomize.RandomizeFiles(ambientNoiseMusic, paths.music);
            //            AddToMusicLookup(ambientNoiseMusic, randList);

            //            randList = Randomize.RandomizeFiles(ambientNoiseSound, paths.sounds);
            //            AddToSoundLookup(ambientNoiseSound, randList);
            //        }));
            //        break;

            //    // Do nothing.
            //    case RandomizationLevel.Subtype:
            //    case RandomizationLevel.None:
            //    default:
            //        break;
            //}

            //Console.WriteLine($"ambient noise randomized in {sw.Elapsed}");
            //sw.Restart();

            ///**** Shuffle Battle Music ****/
            //// Find all files in StreamMusic that match RegexBattleMusic.
            //var battleMusic = new List<FileInfo>(musicFiles.Where(f => RegexBattleMusic.IsMatch(f.Name)));
            //// Find all files in StreamSounds that match RegexBattleMusic.
            //var battleMusicEnd = new List<FileInfo>(soundFiles.Where(f => RegexBattleMusic.IsMatch(f.Name)));

            //// Based on randomization level ...
            //switch (RandomizeBattleMusic)
            //{
            //    // Add files in StreamMusic to max music, and files in StreamSounds to max sound.
            //    case RandomizationLevel.Max:
            //        maxMusic.AddRange(battleMusic);
            //        maxSound.AddRange(battleMusicEnd);
            //        break;

            //    // Randomize files separately in their folders in a Task.
            //    case RandomizationLevel.Type:
            //        tasks.Add(Task.Run(() =>
            //        {
            //            var randList = Randomize.RandomizeFiles(battleMusic, paths.music);
            //            AddToMusicLookup(battleMusic, randList);

            //            randList = Randomize.RandomizeFiles(battleMusicEnd, paths.sounds);
            //            AddToSoundLookup(battleMusicEnd, randList);
            //        }));
            //        break;

            //    // Do nothing.
            //    case RandomizationLevel.Subtype:
            //    case RandomizationLevel.None:
            //    default:
            //        break;
            //}

            //Console.WriteLine($"battle music randomized in {sw.Elapsed}");
            //sw.Restart();

            //// Cutscene Noise
            //var cutsceneNoise = new List<FileInfo>(musicFiles.Where(f => RegexCutscene.IsMatch(f.Name)));
            //_ = cutsceneNoise.RemoveAll(f => f.Name.StartsWith("57.")); // Remove specific exception

            //switch (RandomizeCutsceneNoise)
            //{
            //    case RandomizationLevel.Max:
            //        maxMusic.AddRange(cutsceneNoise);
            //        break;
            //    case RandomizationLevel.Type:
            //        tasks.Add(Task.Run(() =>
            //        {
            //            var randList = Randomize.RandomizeFiles(cutsceneNoise, paths.music);
            //            AddToMusicLookup(cutsceneNoise, randList);
            //        }));
            //        break;
            //    case RandomizationLevel.Subtype:
            //    case RandomizationLevel.None:
            //    default:
            //        break;
            //}

            //Console.WriteLine($"cutscene noise randomized in {sw.Elapsed}");
            //sw.Restart();

            //// Check if NPC and Party Sounds are combined
            //var npcSounds = new List<FileInfo>(soundFiles.Where(f => RegexNPCSound.IsMatch(f.Name)));
            //var partySounds = new List<FileInfo>(soundFiles.Where(f => RegexPartySound.IsMatch(f.Name)));

            ////if (MixNpcAndPartySounds) // Functionality Disabled
            ////{
            ////    npcSounds.AddRange(partySounds);
            ////}
            ////else
            //{
            //    // Party Sounds (if not mixing)
            //    switch (RandomizePartySounds)
            //    {
            //        case RandomizationLevel.Max:
            //            maxSound.AddRange(partySounds);
            //            break;
            //        case RandomizationLevel.Type:
            //            tasks.Add(Task.Run(() =>
            //            {
            //                var randList = Randomize.RandomizeFiles(partySounds, paths.sounds);
            //                AddToSoundLookup(partySounds, randList);
            //            }));
            //            break;
            //        case RandomizationLevel.Subtype:
            //            RandomizeSoundActions(partySounds, paths.sounds);
            //            break;
            //        case RandomizationLevel.None:
            //        default:
            //            break;
            //    }
            //}

            //Console.WriteLine($"party sounds randomized in {sw.Elapsed}");
            //sw.Restart();

            ////// NPC Sounds (or both if mixing) // Functionality Disabled
            ////switch (RandomizeNpcSounds)
            ////{
            ////    case RandomizationLevel.Max:
            ////        maxSound.AddRange(npcSounds);
            ////        break;
            ////    case RandomizationLevel.Type:
            ////        Randomize.RandomizeFiles(npcSounds, SoundsPath);
            ////        break;
            ////    case RandomizationLevel.Subtype:
            ////        RandomizeSoundActions(npcSounds, SoundsPath);
            ////        break;
            ////    case RandomizationLevel.None:
            ////    default:
            ////        break;
            ////}

            // Max Randomizations
            if (maxMusic.Any())
            {
                tasks.Add(Task.Run(() =>
                {
                    var randList = Randomize.RandomizeFiles(maxMusic, paths.music);
                    AddToMusicLookup(maxMusic, randList);
                }));
            }
            if (maxSound.Any())
            {
                tasks.Add(Task.Run(() =>
                {
                    var randList = Randomize.RandomizeFiles(maxSound, paths.sounds);
                    AddToSoundLookup(maxSound, randList);
                }));
            }

            Console.WriteLine($"max audio randomized in {sw.Elapsed}");
            sw.Restart();

            // Overwrite DMCA music with alternatives
            //if (RemoveDmcaMusic)
            //{
            //    tasks.Add(Task.Run(() =>
            //    {
            //        var orig = new List<FileInfo>();
            //        var rand = new List<FileInfo>();
            //        foreach (var fi in musicFiles.Where(f => DmcaAreaMusic.Contains(f.Name)))
            //        {
            //            var replacement = areaMusic[Randomize.Rng.Next(areaMusic.Count)];
            //            File.Copy(replacement.FullName, Path.Combine(paths.music, fi.Name), true);

            //            orig.Add(fi);
            //            rand.Add(replacement);
            //        }
            //        AddToMusicLookup(orig, rand);
            //    }));
            //}

            // Overwrite DMCA music with alternatives
            if (RemoveDmcaMusic)
            {
                tasks.Add(Task.Run(() =>
                {
                    var orig = new List<FileInfo>();
                    var rand = new List<FileInfo>();
                    foreach (var fi in musicFiles.Where(f => DmcaAreaMusic.Contains(f.Name)))
                    {
                        var areaMusic = music[AudioRandoCategory.AreaMusic];
                        var replacement = areaMusic[Randomize.Rng.Next(areaMusic.Count)];
                        File.Copy(replacement.FullName, Path.Combine(paths.music, fi.Name), true);

                        orig.Add(fi);
                        rand.Add(replacement);
                    }
                    AddToMusicLookup(orig, rand);
                }));
            }

            Console.WriteLine($"dmca randomized in {sw.Elapsed}");
            sw.Restart();

            // Run all randomization tasks.
            Task.WhenAll(tasks).Wait();

            Console.WriteLine($"tasks randomized in {sw.Elapsed}");
            sw.Restart();
        }

        private static void AssignSettings(Models.Kotor1Randomizer rando)
        {
            // If rando is null, pull from settings.
            if (null == rando)
            {
                //RandomizeAmbientNoise  = Properties.Settings.Default.RandomizeAmbientNoise;
                //RandomizeAreaMusic     = Properties.Settings.Default.RandomizeAreaMusic;
                //RandomizeBattleMusic   = Properties.Settings.Default.RandomizeBattleMusic;
                //RandomizeCutsceneNoise = Properties.Settings.Default.RandomizeCutsceneNoise;
                //RandomizeNpcSounds     = Properties.Settings.Default.RandomizeNpcSounds;
                //RandomizePartySounds   = Properties.Settings.Default.RandomizePartySounds;

                AudioOptions = Models.Kotor1Randomizer.ConstructAudioOptionsList().ToList();
                AudioOptions.First(arco => arco.Category == AudioRandoCategory.AmbientNoise).Level = Properties.Settings.Default.RandomizeAmbientNoise;
                AudioOptions.First(arco => arco.Category == AudioRandoCategory.AreaMusic).Level = Properties.Settings.Default.RandomizeAreaMusic;
                AudioOptions.First(arco => arco.Category == AudioRandoCategory.BattleMusic).Level = Properties.Settings.Default.RandomizeBattleMusic;
                AudioOptions.First(arco => arco.Category == AudioRandoCategory.CutsceneNoise).Level = Properties.Settings.Default.RandomizeCutsceneNoise;
                AudioOptions.First(arco => arco.Category == AudioRandoCategory.NpcSounds).Level = Properties.Settings.Default.RandomizeNpcSounds;
                AudioOptions.First(arco => arco.Category == AudioRandoCategory.PartySounds).Level = Properties.Settings.Default.RandomizePartySounds;

                MixKotorGameMusic      = false;
                MixNpcAndPartySounds   = Properties.Settings.Default.MixNpcAndPartySounds;
                RemoveDmcaMusic        = Properties.Settings.Default.RemoveDmcaMusic;
            }
            // Otherwise, pull from the Kotor1Randomizer object.
            else
            {
                //RandomizeAmbientNoise  = rando.AudioAmbientNoise;
                //RandomizeAreaMusic     = rando.AudioAreaMusic;
                //RandomizeBattleMusic   = rando.AudioBattleMusic;
                //RandomizeCutsceneNoise = rando.AudioCutsceneNoise;
                //RandomizeNpcSounds     = rando.AudioNpcSounds;
                //RandomizePartySounds   = rando.AudioPartySounds;

                AudioOptions         = rando.AudioCategoryOptions.ToList();
                MixKotorGameMusic    = rando.AudioMixKotorGameMusic;
                MixNpcAndPartySounds = rando.AudioMixNpcAndPartySounds;
                RemoveDmcaMusic      = rando.AudioRemoveDmcaMusic;
            }
        }

        internal static void Reset()
        {
            // Prepare lists for new randomization.
            MusicLookupTable.Clear();
            SoundLookupTable.Clear();
        }

        private static void AddToMusicLookup(List<FileInfo> original, List<FileInfo> randomized)
        {
            for (int i = 0; i < original.Count; i++)
            {
                if (MusicLookupTable.ContainsKey(original[i].Name))
                {
                    MusicLookupTable[original[i].Name] = randomized[i].Name;
                }
                else
                {
                    MusicLookupTable.Add(original[i].Name, randomized[i].Name);
                }
            }
        }

        private static void AddToSoundLookup(List<FileInfo> original, List<FileInfo> randomized)
        {
            for (int i = 0; i < original.Count; i++)
            {
                if (SoundLookupTable.ContainsKey(original[i].Name))
                {
                    SoundLookupTable[original[i].Name] = randomized[i].Name;
                }
                else
                {
                    SoundLookupTable.Add(original[i].Name, randomized[i].Name);
                }
            }
        }

        private static void RandomizeSoundActions(List<FileInfo> files, string outPath)
        {
            var actionList = files.Where(f => SuffixSoundAttack.IsMatch(f.Name)).ToList();
            var randList = Randomize.RandomizeFiles(actionList, outPath);   // ATK
            AddToSoundLookup(actionList, randList);

            actionList = files.Where(f => SuffixSoundBattle.IsMatch(f.Name)).ToList();
            randList = Randomize.RandomizeFiles(actionList, outPath);       // BAT
            AddToSoundLookup(actionList, randList);

            actionList = files.Where(f => SuffixSoundLockAttempt.IsMatch(f.Name)).ToList();
            randList = Randomize.RandomizeFiles(actionList, outPath);       // BLOCK
            AddToSoundLookup(actionList, randList);

            actionList = files.Where(f => SuffixSoundCriticalHit.IsMatch(f.Name)).ToList();
            randList = Randomize.RandomizeFiles(actionList, outPath);       // CRIT
            AddToSoundLookup(actionList, randList);

            actionList = files.Where(f => SuffixSoundDead.IsMatch(f.Name)).ToList();
            randList = Randomize.RandomizeFiles(actionList, outPath);       // DEAD
            AddToSoundLookup(actionList, randList);

            actionList = files.Where(f => SuffixSoundMineFound.IsMatch(f.Name)).ToList();
            randList = Randomize.RandomizeFiles(actionList, outPath);       // DMIN
            AddToSoundLookup(actionList, randList);

            actionList = files.Where(f => SuffixSoundLockFailure.IsMatch(f.Name)).ToList();
            randList = Randomize.RandomizeFiles(actionList, outPath);       // FLOCK
            AddToSoundLookup(actionList, randList);

            actionList = files.Where(f => SuffixSoundHit.IsMatch(f.Name)).ToList();
            randList = Randomize.RandomizeFiles(actionList, outPath);       // HIT
            AddToSoundLookup(actionList, randList);

            actionList = files.Where(f => SuffixSoundMineSet.IsMatch(f.Name)).ToList();
            randList = Randomize.RandomizeFiles(actionList, outPath);       // LMIN
            AddToSoundLookup(actionList, randList);

            actionList = files.Where(f => SuffixSoundLowHealth.IsMatch(f.Name)).ToList();
            randList = Randomize.RandomizeFiles(actionList, outPath);       // LOW
            AddToSoundLookup(actionList, randList);

            actionList = files.Where(f => SuffixSoundMedicine.IsMatch(f.Name)).ToList();
            randList = Randomize.RandomizeFiles(actionList, outPath);       // MED
            AddToSoundLookup(actionList, randList);

            actionList = files.Where(f => SuffixSoundPoison.IsMatch(f.Name)).ToList();
            randList = Randomize.RandomizeFiles(actionList, outPath);       // POIS
            AddToSoundLookup(actionList, randList);

            actionList = files.Where(f => SuffixSoundSoloOff.IsMatch(f.Name)).ToList();
            randList = Randomize.RandomizeFiles(actionList, outPath);       // RPRTY
            AddToSoundLookup(actionList, randList);

            actionList = files.Where(f => SuffixSoundSelect.IsMatch(f.Name)).ToList();
            randList = Randomize.RandomizeFiles(actionList, outPath);       // SLCT
            AddToSoundLookup(actionList, randList);

            actionList = files.Where(f => SuffixSoundLockSuccess.IsMatch(f.Name)).ToList();
            randList = Randomize.RandomizeFiles(actionList, outPath);       // SLOCK
            AddToSoundLookup(actionList, randList);

            actionList = files.Where(f => SuffixSoundSoloOn.IsMatch(f.Name)).ToList();
            randList = Randomize.RandomizeFiles(actionList, outPath);       // SPRTY
            AddToSoundLookup(actionList, randList);

            actionList = files.Where(f => SuffixSoundSearch.IsMatch(f.Name)).ToList();
            randList = Randomize.RandomizeFiles(actionList, outPath);       // SRCH
            AddToSoundLookup(actionList, randList);

            actionList = files.Where(f => SuffixSoundStealth.IsMatch(f.Name)).ToList();
            randList = Randomize.RandomizeFiles(actionList, outPath);       // STLH
            AddToSoundLookup(actionList, randList);

            actionList = files.Where(f => SuffixSoundIneffective.IsMatch(f.Name)).ToList();
            randList = Randomize.RandomizeFiles(actionList, outPath);       // TIA
            AddToSoundLookup(actionList, randList);
        }

        public static void CreateSpoilerLog(XLWorkbook workbook)
        {
            if (MusicLookupTable.Count == 0 &&
                SoundLookupTable.Count == 0)
            { return; }
            var ws = workbook.Worksheets.Add("MusicSound");
            int i = 1;

            // Music and Sound Randomization Settings
            ws.Cell(i, 1).Value = "Music/Sound Type";
            ws.Cell(i, 2).Value = "Rando Level";
            ws.Cell(i, 1).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            ws.Cell(i, 2).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            ws.Cell(i, 1).Style.Font.Bold = true;
            ws.Cell(i, 2).Style.Font.Bold = true;
            i++;

            var settings = new List<Tuple<string, string>>();
            foreach (var arco in AudioOptions)
            {
                settings.Add(new Tuple<string, string>(arco.Category.ToLabel(), arco.Level.ToString()));
            }
            //{
            //    new Tuple<string, string>("Area Music",        RandomizeAreaMusic.ToString()),
            //    new Tuple<string, string>("Battle Music",      RandomizeBattleMusic.ToString()),
            //    new Tuple<string, string>("Ambient Noise",     RandomizeAmbientNoise.ToString()),
            //    new Tuple<string, string>("Cutscene Noise",    RandomizeCutsceneNoise.ToString()),
            //    new Tuple<string, string>("NPC Sounds",        RandomizeNpcSounds.ToString()),
            //    new Tuple<string, string>("Party Sounds",      RandomizePartySounds.ToString()),
            //    new Tuple<string, string>("Remove DMCA",       RemoveDmcaMusic.ToEnabledDisabled()),
            //    new Tuple<string, string>("Mix NPC and Party", MixNpcAndPartySounds.ToEnabledDisabled()),
            //    new Tuple<string, string>("", ""),  // Skip a row.
            //};
            settings.Add(new Tuple<string, string>("", ""));    // Skip a row.
            settings.Add(new Tuple<string, string>("Mix Kotor Game Music", MixKotorGameMusic.ToEnabledDisabled()));
            settings.Add(new Tuple<string, string>("Mix NPC and Party", MixNpcAndPartySounds.ToEnabledDisabled()));
            settings.Add(new Tuple<string, string>("Remove DMCA", RemoveDmcaMusic.ToEnabledDisabled()));
            settings.Add(new Tuple<string, string>("", ""));    // Skip a row.

            foreach (var setting in settings)
            {
                ws.Cell(i, 1).Value = setting.Item1;
                ws.Cell(i, 2).Value = setting.Item2;
                ws.Cell(i, 1).Style.Font.Italic = true;
                i++;
            }

            // Music Shuffle
            if (MusicLookupTable.Any())
            {
                var sortedLookup = MusicLookupTable.OrderBy(kvp => kvp.Key);
                ws.Cell(i, 1).Value = "Music";
                ws.Cell(i, 1).Style.Font.Bold = true;
                ws.Cell(i, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Range(i, 1, i, 3).Merge();
                i++;

                ws.Cell(i, 1).Value = "Has Changed";
                ws.Cell(i, 2).Value = "Original";
                ws.Cell(i, 3).Value = "Randomized";
                ws.Cell(i, 1).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                ws.Cell(i, 2).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                ws.Cell(i, 3).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                ws.Cell(i, 1).Style.Font.Bold = true;
                ws.Cell(i, 2).Style.Font.Bold = true;
                ws.Cell(i, 3).Style.Font.Bold = true;
                i++;

                foreach (var kvp in sortedLookup)
                {
                    var hasChanged = kvp.Key != kvp.Value;
                    ws.Cell(i, 1).Value = hasChanged.ToString().ToUpper();
                    ws.Cell(i, 1).DataType = XLDataType.Text;
                    ws.Cell(i, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Cell(i, 2).Value = kvp.Key;
                    ws.Cell(i, 3).Value = kvp.Value;
                    if (hasChanged) ws.Cell(i, 1).Style.Font.FontColor = XLColor.Green;
                    else            ws.Cell(i, 1).Style.Font.FontColor = XLColor.Red;
                    i++;
                }

                i++;    // Skip a row.
            }

            // Sound Shuffle
            if (SoundLookupTable.Any())
            {
                var sortedLookup = SoundLookupTable.OrderBy(kvp => kvp.Key);
                ws.Cell(i, 1).Value = "Sound";
                ws.Cell(i, 1).Style.Font.Bold = true;
                ws.Cell(i, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Range(i, 1, i, 3).Merge();
                i++;

                ws.Cell(i, 1).Value = "Has Changed";
                ws.Cell(i, 2).Value = "Original";
                ws.Cell(i, 3).Value = "Randomized";
                ws.Cell(i, 1).Style.Font.Bold = true;
                ws.Cell(i, 2).Style.Font.Bold = true;
                ws.Cell(i, 3).Style.Font.Bold = true;
                i++;

                foreach (var kvp in sortedLookup)
                {
                    var hasChanged = kvp.Key != kvp.Value;
                    ws.Cell(i, 1).Value = hasChanged.ToString().ToUpper();
                    ws.Cell(i, 1).DataType = XLDataType.Text;
                    ws.Cell(i, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Cell(i, 2).Value = kvp.Key;
                    ws.Cell(i, 3).Value = kvp.Value;
                    if (hasChanged) ws.Cell(i, 1).Style.Font.FontColor = XLColor.Green;
                    else            ws.Cell(i, 1).Style.Font.FontColor = XLColor.Red;
                    i++;
                }
            }

            // Resize Columns
            ws.Column(1).AdjustToContents();
            ws.Column(2).AdjustToContents();
            ws.Column(3).AdjustToContents();
        }

        #region K2 Regexes
        public static Regex K2Music_Bed => new Regex("^bed_", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        public static Regex K2Music_Area => new Regex("^mus_a_", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        public static Regex K2Music_Battle => new Regex("^mus_bat_", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        public static Regex K2Music_BattleEnd => new Regex("^mus_sbat_", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        public static Regex K2Music_SFX => new Regex("^mus_s_", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        public static Regex K2Music_Theme => new Regex("^mus_[ajkmnst][aeirt][adehiort][ehiny]", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        #endregion

        //public static Regex PrefixAreaMusic { get { return new Regex("", RegexOptions.Compiled | RegexOptions.IgnoreCase); } }
        public static List<string> PrefixListAreaMusic => new List<string>()
        {
            "mus_area_",
            "mus_theme_",
            "57.",          // LS ending BGM, Star Wars main theme
            "credits.",     // Star Wars main theme
            "evil_ending.", // DS ending BGM, Star Wars main theme
        };

        Regex K1Music_Area => new Regex(@"^mus_(area_|theme_)|^57\.|^credits\.|^evil_ending\.", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public static List<string> DmcaAreaMusic => new List<string>()
        {
            "57.wav",
            "credits.wav",
            "evil_ending.wav",
        };

        Regex K1Music_DMCA => new Regex(@"^57\.wav$|^credits\.wav$|^evil_ending\.wav$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        Regex K1Music_Battle => new Regex("^mus_bat", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        Regex K1Music_BattleEnd => new Regex("^mus_sbat", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public static Regex RegexBattleMusic => new Regex("mus_s?bat_", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public static List<string> PrefixListBattleMusic => new List<string>()
        {
            "mus_bat_",
            "mus_sbat_",
        };

        //public static Regex PrefixNoise { get { return new Regex("", RegexOptions.Compiled | RegexOptions.IgnoreCase); } }

        Regex K1Sounds_Noise => new Regex(@"^(al|as)_(an|el|en|me|nt|ot|vx|el)_|^cs_|^mgs_|^mus_loadscreen\.|^pl_", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public static List<string> PrefixListNoise => new List<string>()
        {
            "al_an_",
            "al_el_",
            "al_en_",
            "al_me_",
            "al_nt_",
            "al_ot_",
            "al_vx_",
            "as_el_",
            "cs_",
            "mgs_",
            "mus_loadscreen.",
            "pl_",
        };

        /// <summary> Cutscene files begin with 2 digits and optionally a single letter (a, b, or c). </summary>
        public static Regex RegexCutscene => new Regex(@"^\d{2}[abc]?\.wav$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public static Regex RegexNPCSound => new Regex("^n_", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public List<string> PrefixListNPCSound => new List<string>()
        {
            "n_",
        };

        public static Regex RegexPartySound => new Regex("^p_", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public List<string> PrefixListPartySound => new List<string>()
        {
            "p_",
        };

        public static Regex SuffixSoundAttack => new Regex(@"_ATK\d?.wav$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public static Regex SuffixSoundBattle => new Regex(@"_BAT\d?.wav$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public static Regex SuffixSoundCriticalHit => new Regex(@"_CRIT\d?.wav$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public static Regex SuffixSoundDead => new Regex(@"_DEAD\d?.wav$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public static Regex SuffixSoundHit => new Regex(@"_HIT\d?.wav$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public static Regex SuffixSoundIneffective => new Regex(@"_TIA\d?.wav$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public static Regex SuffixSoundLockAttempt => new Regex(@"_BLOCK\d?.wav$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public static Regex SuffixSoundLockFailure => new Regex(@"_FLOCK\d?.wav$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public static Regex SuffixSoundLockSuccess => new Regex(@"_SLOCK\d?.wav$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public static Regex SuffixSoundLowHealth => new Regex(@"_LOW\d?.wav$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public static Regex SuffixSoundMedicine => new Regex(@"_MED\d?.wav$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public static Regex SuffixSoundMineFound => new Regex(@"_DMIN\d?.wav$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public static Regex SuffixSoundMineSet => new Regex(@"_LMIN\d?.wav$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public static Regex SuffixSoundPoison => new Regex(@"_POIS\d?.wav$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public static Regex SuffixSoundSelect => new Regex(@"_SLCT\d?.wav$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public static Regex SuffixSoundSearch => new Regex(@"_SRCH\d?.wav$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public static Regex SuffixSoundSoloOff => new Regex(@"_RPRTY\d?.wav$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public static Regex SuffixSoundSoloOn => new Regex(@"_SPRTY\d?.wav$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public static Regex SuffixSoundStealth => new Regex(@"_STLH\d?.wav$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
    }
}
