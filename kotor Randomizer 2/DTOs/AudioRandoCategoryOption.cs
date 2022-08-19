using kotor_Randomizer_2.Extensions;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace kotor_Randomizer_2.DTOs
{
    [Flags]
    public enum AudioFolders
    {
        Unknown = 0b0000,
        /// <summary> StreamMusic (KotOR 1 and KotOR 2) </summary>
        Music   = 0b0001,
        /// <summary> StreamSounds (KotOR 1 and KotOR 2) </summary>
        Sounds  = 0b0010,
        /// <summary> StreamVoice (KotOR 2 Only) </summary>
        Voice   = 0b0100,
        /// <summary> StreamWaves (KotOR 1 Only) </summary>
        Waves   = 0b1000,
    }

    public class AudioRandoCategoryOption : RandomizationLevelOption
    {
        #region Constructors
        public AudioRandoCategoryOption() { }

        public AudioRandoCategoryOption(AudioRandoCategory category, AudioFolders folders, Regex regex,
                bool subtypeVisible = true, string subtypeLabel = "Subtype",
                bool typeVisible = true, string typeLabel = "Type",
                bool maxVisible = true, string maxLabel = "Max",
                RandomizationLevel level = RandomizationLevel.None)
            : base(category.ToLabel(), category.ToToolTip(), subtypeVisible, subtypeLabel, typeVisible, typeLabel, maxVisible, maxLabel, level)
        {
            Category = category;
            Folders = folders;
            Regex = regex;
        }

        //public AudioRandoCategoryOption(AudioRandoCategory category, List<string> prefixes)
        //    : this(category)
        //{
        //    Prefixes = prefixes;
        //}
        #endregion

        #region Properties
        private AudioRandoCategory _category;
        public AudioRandoCategory Category
        {
            get => _category;
            set => SetField(ref _category, value);
        }

        private AudioFolders _folders;
        public AudioFolders Folders
        {
            get => _folders;
            set => SetField(ref _folders, value);
        }

        private Regex _regex;
        public Regex Regex
        {
            get => _regex;
            set => SetField(ref _regex, value);
        }

        //private List<string> _prefixes;
        //public List<string> Prefixes
        //{
        //    get => _prefixes;
        //    set => SetField(ref _prefixes, value);
        //}
        #endregion
    }
}
