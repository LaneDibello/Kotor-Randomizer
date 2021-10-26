using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;
using ClosedXML.Excel;
using kotor_Randomizer_2.Extensions;

namespace kotor_Randomizer_2
{
    //This has absically been copied verbatim from what Glasnonck coded in the last rando, could probably be cleaned up, but I cannot be bothered 
    class SoundRando
    {
        private static Dictionary<string, string> MusicLookupTable { get; set; } = new Dictionary<string, string>();
        private static Dictionary<string, string> SoundLookupTable { get; set; } = new Dictionary<string, string>();

        private static RandomizationLevel RandomizeAmbientNoise { get; set; }
        private static RandomizationLevel RandomizeAreaMusic { get; set; }
        private static RandomizationLevel RandomizeBattleMusic { get; set; }
        private static RandomizationLevel RandomizeCutsceneNoise { get; set; }
        private static RandomizationLevel RandomizeNpcSounds { get; set; }
        private static RandomizationLevel RandomizePartySounds { get; set; }
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
            List<FileInfo> maxMusic = new List<FileInfo>();
            List<FileInfo> maxSound = new List<FileInfo>();
            List<FileInfo> musicFiles = new List<FileInfo>();
            List<FileInfo> soundFiles = new List<FileInfo>();
            if (Directory.Exists(paths.music_backup)) musicFiles = paths.FilesInMusicBackup.ToList();
            if (Directory.Exists(paths.sounds_backup)) soundFiles = paths.FilesInSoundsBackup.ToList();

            // Area Music
            List<FileInfo> areaMusic = new List<FileInfo>();
            foreach (var prefix in PrefixListAreaMusic)
            {
                areaMusic.AddRange(musicFiles.Where(f => f.Name.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)));
            }

            if (RemoveDmcaMusic)
            {
                areaMusic.RemoveAll(f => DmcaAreaMusic.Contains(f.Name));   // Remove DMCA music from the area list.
            }

            switch (RandomizeAreaMusic)
            {
                case RandomizationLevel.Max:
                    maxMusic.AddRange(areaMusic);
                    break;
                case RandomizationLevel.Type:
                    var randList = Randomize.RandomizeFiles(areaMusic, paths.music);
                    AddToMusicLookup(areaMusic, randList);
                    break;
                case RandomizationLevel.Subtype:
                case RandomizationLevel.None:
                default:
                    break;
            }

            // Ambient Noise
            List<FileInfo> ambientNoiseMusic = new List<FileInfo>();
            foreach (var prefix in PrefixListNoise)
            {
                ambientNoiseMusic.AddRange(musicFiles.Where(f => f.Name.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)));
            }

            List<FileInfo> ambientNoiseSound = new List<FileInfo>();
            foreach (var prefix in PrefixListNoise)
            {
                ambientNoiseSound.AddRange(soundFiles.Where(f => f.Name.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)));
            }

            switch (RandomizeAmbientNoise)
            {
                case RandomizationLevel.Max:
                    maxMusic.AddRange(ambientNoiseMusic);
                    maxSound.AddRange(ambientNoiseSound);
                    break;
                case RandomizationLevel.Type:
                    var randList = Randomize.RandomizeFiles(ambientNoiseMusic, paths.music);
                    AddToMusicLookup(ambientNoiseMusic, randList);

                    randList = Randomize.RandomizeFiles(ambientNoiseSound, paths.sounds);
                    AddToSoundLookup(ambientNoiseSound, randList);
                    break;
                case RandomizationLevel.Subtype:
                case RandomizationLevel.None:
                default:
                    break;
            }

            // Battle Music
            List<FileInfo> battleMusic = new List<FileInfo>(musicFiles.Where(f => RegexBattleMusic.IsMatch(f.Name)));
            List<FileInfo> battleMusicEnd = new List<FileInfo>(soundFiles.Where(f => RegexBattleMusic.IsMatch(f.Name)));

            switch (RandomizeBattleMusic)
            {
                case RandomizationLevel.Max:
                    maxMusic.AddRange(battleMusic);
                    maxSound.AddRange(battleMusicEnd);
                    break;
                case RandomizationLevel.Type:
                    var randList = Randomize.RandomizeFiles(battleMusic, paths.music);
                    AddToMusicLookup(battleMusic, randList);

                    randList = Randomize.RandomizeFiles(battleMusicEnd, paths.sounds);
                    AddToSoundLookup(battleMusicEnd, randList);
                    break;
                case RandomizationLevel.Subtype:
                case RandomizationLevel.None:
                default:
                    break;
            }

            // Cutscene Noise
            List<FileInfo> cutsceneNoise = new List<FileInfo>(musicFiles.Where(f => RegexCutscene.IsMatch(f.Name)));
            cutsceneNoise.RemoveAll(f => f.Name.StartsWith("57.")); // Remove specific exception

            switch (RandomizeCutsceneNoise)
            {
                case RandomizationLevel.Max:
                    maxMusic.AddRange(cutsceneNoise);
                    break;
                case RandomizationLevel.Type:
                    var randList = Randomize.RandomizeFiles(cutsceneNoise, paths.music);
                    AddToMusicLookup(cutsceneNoise, randList);
                    break;
                case RandomizationLevel.Subtype:
                case RandomizationLevel.None:
                default:
                    break;
            }

            // Check if NPC and Party Sounds are combined
            List<FileInfo> npcSounds = new List<FileInfo>(soundFiles.Where(f => RegexNPCSound.IsMatch(f.Name)));
            List<FileInfo> partySounds = new List<FileInfo>(soundFiles.Where(f => RegexPartySound.IsMatch(f.Name)));

            //if (MixNpcAndPartySounds) // Functionality Disabled
            //{
            //    npcSounds.AddRange(partySounds);
            //}
            //else
            {
                // Party Sounds (if not mixing)
                switch (RandomizePartySounds)
                {
                    case RandomizationLevel.Max:
                        maxSound.AddRange(partySounds);
                        break;
                    case RandomizationLevel.Type:
                        var randList = Randomize.RandomizeFiles(partySounds, paths.sounds);
                        AddToSoundLookup(partySounds, randList);
                        break;
                    case RandomizationLevel.Subtype:
                        RandomizeSoundActions(partySounds, paths.sounds);
                        break;
                    case RandomizationLevel.None:
                    default:
                        break;
                }
            }

            //// NPC Sounds (or both if mixing) // Functionality Disabled
            //switch (RandomizeNpcSounds)
            //{
            //    case RandomizationLevel.Max:
            //        maxSound.AddRange(npcSounds);
            //        break;
            //    case RandomizationLevel.Type:
            //        Randomize.RandomizeFiles(npcSounds, SoundsPath);
            //        break;
            //    case RandomizationLevel.Subtype:
            //        RandomizeSoundActions(npcSounds, SoundsPath);
            //        break;
            //    case RandomizationLevel.None:
            //    default:
            //        break;
            //}

            // Max Randomizations
            if (maxMusic.Any())
            {
                var randList = Randomize.RandomizeFiles(maxMusic, paths.music);
                AddToMusicLookup(maxMusic, randList);
            }
            if (maxSound.Any())
            {
                var randList = Randomize.RandomizeFiles(maxSound, paths.sounds);
                AddToSoundLookup(maxSound, randList);
            }

            // Overwrite DMCA music with alternatives
            if (RemoveDmcaMusic)
            {
                var orig = new List<FileInfo>();
                var rand = new List<FileInfo>();
                foreach (var fi in musicFiles.Where(f => DmcaAreaMusic.Contains(f.Name)))
                {
                    var replacement = areaMusic[Randomize.Rng.Next(areaMusic.Count)];
                    File.Copy(replacement.FullName, Path.Combine(paths.music, fi.Name), true);

                    orig.Add(fi);
                    rand.Add(replacement);
                }
                AddToMusicLookup(orig, rand);
            }
        }

        private static void AssignSettings(Models.Kotor1Randomizer k1rando)
        {
            // If k1rando is null, pull from settings.
            if (null == k1rando)
            {
                RandomizeAmbientNoise  = Properties.Settings.Default.RandomizeAmbientNoise;
                RandomizeAreaMusic     = Properties.Settings.Default.RandomizeAreaMusic;
                RandomizeBattleMusic   = Properties.Settings.Default.RandomizeBattleMusic;
                RandomizeCutsceneNoise = Properties.Settings.Default.RandomizeCutsceneNoise;
                RandomizeNpcSounds     = Properties.Settings.Default.RandomizeNpcSounds;
                RandomizePartySounds   = Properties.Settings.Default.RandomizePartySounds;
                MixNpcAndPartySounds   = Properties.Settings.Default.MixNpcAndPartySounds;
                RemoveDmcaMusic        = Properties.Settings.Default.RemoveDmcaMusic;
            }
            // Otherwise, pull from the Kotor1Randomizer object.
            else
            {
                RandomizeAmbientNoise  = k1rando.AudioAmbientNoise;
                RandomizeAreaMusic     = k1rando.AudioAreaMusic;
                RandomizeBattleMusic   = k1rando.AudioBattleMusic;
                RandomizeCutsceneNoise = k1rando.AudioCutsceneNoise;
                RandomizeNpcSounds     = k1rando.AudioNpcSounds;
                RandomizePartySounds   = k1rando.AudioPartySounds;
                MixNpcAndPartySounds   = k1rando.AudioMixNpcAndPartySounds;
                RemoveDmcaMusic        = k1rando.AudioRemoveDmcaMusic;
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

            var settings = new List<Tuple<string, string>>()
            {
                new Tuple<string, string>("Area Music",        RandomizeAreaMusic.ToString()),
                new Tuple<string, string>("Battle Music",      RandomizeBattleMusic.ToString()),
                new Tuple<string, string>("Ambient Noise",     RandomizeAmbientNoise.ToString()),
                new Tuple<string, string>("Cutscene Noise",    RandomizeCutsceneNoise.ToString()),
                new Tuple<string, string>("NPC Sounds",        RandomizeNpcSounds.ToString()),
                new Tuple<string, string>("Party Sounds",      RandomizePartySounds.ToString()),
                new Tuple<string, string>("Remove DMCA",       RemoveDmcaMusic.ToEnabledDisabled()),
                new Tuple<string, string>("Mix NPC and Party", MixNpcAndPartySounds.ToEnabledDisabled()),
                new Tuple<string, string>("", ""),  // Skip a row.
            };

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

        //public static Regex PrefixAreaMusic { get { return new Regex("", RegexOptions.Compiled | RegexOptions.IgnoreCase); } }
        public static List<string> PrefixListAreaMusic
        {
            get
            {
                return new List<string>()
                {
                    "mus_area_",
                    "mus_theme_",
                    "57.",          // LS ending BGM, Star Wars main theme
                    "credits.",     // Star Wars main theme
                    "evil_ending.", // DS ending BGM, Star Wars main theme
                };
            }
        }

        public static List<string> DmcaAreaMusic
        {
            get
            {
                return new List<string>()
                {
                    "57.wav",
                    "credits.wav",
                    "evil_ending.wav",
                };
            }
        }

        public static Regex RegexBattleMusic { get { return new Regex("mus_s?bat_", RegexOptions.Compiled | RegexOptions.IgnoreCase); } }

        public static List<string> PrefixListBattleMusic
        {
            get
            {
                return new List<string>()
                {
                    "mus_bat_",
                    "mus_sbat_",
                };
            }
        }
        
        //public static Regex PrefixNoise { get { return new Regex("", RegexOptions.Compiled | RegexOptions.IgnoreCase); } }

        public static List<string> PrefixListNoise
        {
            get
            {
                return new List<string>()
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
            }
        }
        
        public static Regex RegexCutscene { get { return new Regex(@"^\d{2}[abc]?\.wav$", RegexOptions.Compiled | RegexOptions.IgnoreCase); } }
        
        public static Regex RegexNPCSound { get { return new Regex("^n_", RegexOptions.Compiled | RegexOptions.IgnoreCase); } }

        public List<string> PrefixListNPCSound
        {
            get
            {
                return new List<string>()
                {
                    "n_",
                };
            }
        }
        
        public static Regex RegexPartySound { get { return new Regex("^p_", RegexOptions.Compiled | RegexOptions.IgnoreCase); } }

        public List<string> PrefixListPartySound
        {
            get
            {
                return new List<string>()
                {
                    "p_",
                };
            }
        }
        
        public static Regex SuffixSoundAttack { get { return new Regex(@"_ATK\d?.wav$", RegexOptions.Compiled | RegexOptions.IgnoreCase); } }
        
        public static Regex SuffixSoundBattle { get { return new Regex(@"_BAT\d?.wav$", RegexOptions.Compiled | RegexOptions.IgnoreCase); } }
        
        public static Regex SuffixSoundCriticalHit { get { return new Regex(@"_CRIT\d?.wav$", RegexOptions.Compiled | RegexOptions.IgnoreCase); } }
        
        public static Regex SuffixSoundDead { get { return new Regex(@"_DEAD\d?.wav$", RegexOptions.Compiled | RegexOptions.IgnoreCase); } }
        
        public static Regex SuffixSoundHit { get { return new Regex(@"_HIT\d?.wav$", RegexOptions.Compiled | RegexOptions.IgnoreCase); } }
        
        public static Regex SuffixSoundIneffective { get { return new Regex(@"_TIA\d?.wav$", RegexOptions.Compiled | RegexOptions.IgnoreCase); } }
        
        public static Regex SuffixSoundLockAttempt { get { return new Regex(@"_BLOCK\d?.wav$", RegexOptions.Compiled | RegexOptions.IgnoreCase); } }
        
        public static Regex SuffixSoundLockFailure { get { return new Regex(@"_FLOCK\d?.wav$", RegexOptions.Compiled | RegexOptions.IgnoreCase); } }
        
        public static Regex SuffixSoundLockSuccess { get { return new Regex(@"_SLOCK\d?.wav$", RegexOptions.Compiled | RegexOptions.IgnoreCase); } }

        public static Regex SuffixSoundLowHealth { get { return new Regex(@"_LOW\d?.wav$", RegexOptions.Compiled | RegexOptions.IgnoreCase); } }
        
        public static Regex SuffixSoundMedicine { get { return new Regex(@"_MED\d?.wav$", RegexOptions.Compiled | RegexOptions.IgnoreCase); } }
        
        public static Regex SuffixSoundMineFound { get { return new Regex(@"_DMIN\d?.wav$", RegexOptions.Compiled | RegexOptions.IgnoreCase); } }
        
        public static Regex SuffixSoundMineSet { get { return new Regex(@"_LMIN\d?.wav$", RegexOptions.Compiled | RegexOptions.IgnoreCase); } }
        
        public static Regex SuffixSoundPoison { get { return new Regex(@"_POIS\d?.wav$", RegexOptions.Compiled | RegexOptions.IgnoreCase); } }
        
        public static Regex SuffixSoundSelect { get { return new Regex(@"_SLCT\d?.wav$", RegexOptions.Compiled | RegexOptions.IgnoreCase); } }
        
        public static Regex SuffixSoundSearch { get { return new Regex(@"_SRCH\d?.wav$", RegexOptions.Compiled | RegexOptions.IgnoreCase); } }
        
        public static Regex SuffixSoundSoloOff { get { return new Regex(@"_RPRTY\d?.wav$", RegexOptions.Compiled | RegexOptions.IgnoreCase); } }
        
        public static Regex SuffixSoundSoloOn { get { return new Regex(@"_SPRTY\d?.wav$", RegexOptions.Compiled | RegexOptions.IgnoreCase); } }
        
        public static Regex SuffixSoundStealth { get { return new Regex(@"_STLH\d?.wav$", RegexOptions.Compiled | RegexOptions.IgnoreCase); } }
    }
}
