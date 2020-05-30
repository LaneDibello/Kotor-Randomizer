using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;

namespace kotor_Randomizer_2
{
    //This has absically been copied verbatim from what Glasnonck coded in the last rando, could probably be cleaned up, but I cannot be bothered 
    class SoundRando
    {
        public static void sound_rando(Globals.KPaths paths)
        {
            DirectoryInfo music = new DirectoryInfo(paths.get_backup(paths.music));
            List<FileInfo> musicFiles = music.GetFiles().ToList();
            DirectoryInfo sounds = new DirectoryInfo(paths.get_backup(paths.sounds));
            List<FileInfo> soundFiles = sounds.GetFiles().ToList();

            //ThreadSafeRandom.SetSeed(Convert.ToInt32(seedBox.Text)); // We could restart the seed, or just leave it alone. Either way will give randomness.

            // Get file collections
            List<FileInfo> maxMusic = new List<FileInfo>();
            List<FileInfo> maxSound = new List<FileInfo>();

            // Area Music
            List<FileInfo> areaMusic = new List<FileInfo>();
            foreach (var prefix in PrefixListAreaMusic)
            {
                areaMusic.AddRange(musicFiles.Where(f => f.Name.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)));
            }

            switch ((Globals.RandomizationLevel)Properties.Settings.Default.RandomizeAreaMusic)
            {
                case Globals.RandomizationLevel.Max:
                    maxMusic.AddRange(areaMusic);
                    break;
                case Globals.RandomizationLevel.Type:
                    Randomize.RandomizeFiles(areaMusic, paths.music);
                    break;
                case Globals.RandomizationLevel.Subtype:
                case Globals.RandomizationLevel.None:
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

            switch ((Globals.RandomizationLevel)Properties.Settings.Default.RandomizeAmbientNoise)
            {
                case Globals.RandomizationLevel.Max:
                    maxMusic.AddRange(ambientNoiseMusic);
                    maxSound.AddRange(ambientNoiseSound);
                    break;
                case Globals.RandomizationLevel.Type:
                    Randomize.RandomizeFiles(ambientNoiseMusic, paths.music);
                    Randomize.RandomizeFiles(ambientNoiseSound, paths.sounds);
                    break;
                case Globals.RandomizationLevel.Subtype:
                case Globals.RandomizationLevel.None:
                default:
                    break;
            }

            // Battle Music
            List<FileInfo> battleMusic = new List<FileInfo>(musicFiles.Where(f => RegexBattleMusic.IsMatch(f.Name)));
            List<FileInfo> battleMusicEnd = new List<FileInfo>(soundFiles.Where(f => RegexBattleMusic.IsMatch(f.Name)));

            switch ((Globals.RandomizationLevel)Properties.Settings.Default.RandomizeBattleMusic)
            {
                case Globals.RandomizationLevel.Max:
                    maxMusic.AddRange(battleMusic);
                    maxSound.AddRange(battleMusicEnd);
                    break;
                case Globals.RandomizationLevel.Type:
                    Randomize.RandomizeFiles(battleMusic, paths.music);
                    Randomize.RandomizeFiles(battleMusicEnd, paths.sounds);
                    break;
                case Globals.RandomizationLevel.Subtype:
                case Globals.RandomizationLevel.None:
                default:
                    break;
            }

            // Cutscene Noise
            List<FileInfo> cutsceneNoise = new List<FileInfo>(musicFiles.Where(f => RegexCutscene.IsMatch(f.Name)));
            cutsceneNoise.RemoveAll(f => f.Name.StartsWith("57.")); // Remove specific exception

            switch ((Globals.RandomizationLevel)Properties.Settings.Default.RandomizeCutsceneNoise)
            {
                case Globals.RandomizationLevel.Max:
                    maxMusic.AddRange(cutsceneNoise);
                    break;
                case Globals.RandomizationLevel.Type:
                    Randomize.RandomizeFiles(cutsceneNoise, paths.music);
                    break;
                case Globals.RandomizationLevel.Subtype:
                case Globals.RandomizationLevel.None:
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
                switch ((Globals.RandomizationLevel)Properties.Settings.Default.RandomizePartySounds)
                {
                    case Globals.RandomizationLevel.Max:
                        maxSound.AddRange(partySounds);
                        break;
                    case Globals.RandomizationLevel.Type:
                        Randomize.RandomizeFiles(partySounds, paths.sounds);
                        break;
                    case Globals.RandomizationLevel.Subtype:
                        RandomizeSoundActions(partySounds, paths.sounds);
                        break;
                    case Globals.RandomizationLevel.None:
                    default:
                        break;
                }
            }

            //// NPC Sounds (or both if mixing) // Functionality Disabled
            //switch (RandomizeNpcSounds)
            //{
            //    case Globals.RandomizationLevel.Max:
            //        maxSound.AddRange(npcSounds);
            //        break;
            //    case Globals.RandomizationLevel.Type:
            //        Randomize.RandomizeFiles(npcSounds, SoundsPath);
            //        break;
            //    case Globals.RandomizationLevel.Subtype:
            //        RandomizeSoundActions(npcSounds, SoundsPath);
            //        break;
            //    case Globals.RandomizationLevel.None:
            //    default:
            //        break;
            //}

            // Max Randomizations
            if (maxMusic.Any()) { Randomize.RandomizeFiles(maxMusic, paths.music); }
            if (maxSound.Any()) { Randomize.RandomizeFiles(maxSound, paths.sounds); }
        }

        private static void RandomizeSoundActions(List<FileInfo> files, string outPath)
        {
            Randomize.RandomizeFiles(files.Where(f => SuffixSoundAttack.IsMatch(f.Name)), outPath);       // ATK
            Randomize.RandomizeFiles(files.Where(f => SuffixSoundBattle.IsMatch(f.Name)), outPath);       // BAT
            Randomize.RandomizeFiles(files.Where(f => SuffixSoundLockAttempt.IsMatch(f.Name)), outPath);  // BLOCK
            Randomize.RandomizeFiles(files.Where(f => SuffixSoundCriticalHit.IsMatch(f.Name)), outPath);  // CRIT
            Randomize.RandomizeFiles(files.Where(f => SuffixSoundDead.IsMatch(f.Name)), outPath);         // DEAD
            Randomize.RandomizeFiles(files.Where(f => SuffixSoundMineFound.IsMatch(f.Name)), outPath);    // DMIN
            Randomize.RandomizeFiles(files.Where(f => SuffixSoundLockFailure.IsMatch(f.Name)), outPath);  // FLOCK
            Randomize.RandomizeFiles(files.Where(f => SuffixSoundHit.IsMatch(f.Name)), outPath);          // HIT
            Randomize.RandomizeFiles(files.Where(f => SuffixSoundMineSet.IsMatch(f.Name)), outPath);      // LMIN
            Randomize.RandomizeFiles(files.Where(f => SuffixSoundLowHealth.IsMatch(f.Name)), outPath);    // LOW
            Randomize.RandomizeFiles(files.Where(f => SuffixSoundMedicine.IsMatch(f.Name)), outPath);     // MED
            Randomize.RandomizeFiles(files.Where(f => SuffixSoundPoison.IsMatch(f.Name)), outPath);       // POIS
            Randomize.RandomizeFiles(files.Where(f => SuffixSoundSoloOff.IsMatch(f.Name)), outPath);      // RPRTY
            Randomize.RandomizeFiles(files.Where(f => SuffixSoundSelect.IsMatch(f.Name)), outPath);       // SLCT
            Randomize.RandomizeFiles(files.Where(f => SuffixSoundLockSuccess.IsMatch(f.Name)), outPath);  // SLOCK
            Randomize.RandomizeFiles(files.Where(f => SuffixSoundSoloOn.IsMatch(f.Name)), outPath);       // SPRTY
            Randomize.RandomizeFiles(files.Where(f => SuffixSoundSearch.IsMatch(f.Name)), outPath);       // SRCH
            Randomize.RandomizeFiles(files.Where(f => SuffixSoundStealth.IsMatch(f.Name)), outPath);      // STLH
            Randomize.RandomizeFiles(files.Where(f => SuffixSoundIneffective.IsMatch(f.Name)), outPath);  // TIA
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
                    "57.",
                    "credits.",
                    "evil_ending.",
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
