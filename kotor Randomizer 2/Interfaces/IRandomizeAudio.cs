using kotor_Randomizer_2.DTOs;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace kotor_Randomizer_2.Interfaces
{
    public interface IRandomizeAudio
    {
        bool DoRandomizeAudio { get; }

        RandomizationLevel AudioAmbientNoise { get; set; }
        RandomizationLevel AudioAreaMusic { get; set; }
        RandomizationLevel AudioBattleMusic { get; set; }
        RandomizationLevel AudioCutsceneNoise { get; set; }
        RandomizationLevel AudioNpcSounds { get; set; }
        RandomizationLevel AudioPartySounds { get; set; }
        ObservableCollection<AudioRandoCategoryOption> AudioCategoryOptions { get; set; }
        bool AudioMixKotorGameMusic { get; set; }
        bool AudioMixNpcAndPartySounds { get; set; }
        bool AudioRemoveDmcaMusic { get; set; }
        Regex AudioDmcaMusicRegex { get; }
    }
}
