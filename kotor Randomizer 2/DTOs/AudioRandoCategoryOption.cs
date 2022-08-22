using kotor_Randomizer_2.Extensions;
using System;
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

        public AudioRandoCategoryOption(AudioRandoCategory category, AudioFolders folders, Regex audioRegex, Regex stingRegex = null,
                bool subtypeVisible = true, string subtypeLabel = "Subtype",
                bool typeVisible = true, string typeLabel = "Type",
                bool maxVisible = true, string maxLabel = "Max",
                RandomizationLevel level = RandomizationLevel.None)
            : base(category.ToLabel(), category.ToToolTip(), subtypeVisible, subtypeLabel, typeVisible, typeLabel, maxVisible, maxLabel, level)
        {
            Category = category;
            Folders = folders;
            AudioRegex = audioRegex;
            StingRegex = stingRegex;
        }
        #endregion

        #region Properties

        #region Backing Fields
        private AudioRandoCategory _category;
        private AudioFolders _folders;
        private Regex _audioRegex;
        private Regex _stingRegex;
        #endregion

        /// <summary>
        /// Category of audio file to randomize.
        /// </summary>
        public AudioRandoCategory Category
        {
            get => _category;
            set => SetField(ref _category, value);
        }

        /// <summary>
        /// Folders in which the files are found.
        /// </summary>
        public AudioFolders Folders
        {
            get => _folders;
            set => SetField(ref _folders, value);
        }

        /// <summary>
        /// Regex describing the name of the files to randomize.
        /// </summary>
        public Regex AudioRegex
        {
            get => _audioRegex;
            set => SetField(ref _audioRegex, value);
        }

        /// <summary>
        /// Regex describing the name of associated audio stings to randomize.
        /// </summary>
        public Regex StingRegex
        {
            get => _stingRegex;
            set => SetField(ref _stingRegex, value);
        }
        #endregion
    }
}
