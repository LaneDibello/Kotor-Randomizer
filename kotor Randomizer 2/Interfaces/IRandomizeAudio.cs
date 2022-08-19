using kotor_Randomizer_2.DTOs;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace kotor_Randomizer_2.Interfaces
{
    public interface IRandomizeAudio
    {
        bool DoRandomizeAudio { get; }

        ObservableCollection<AudioRandoCategoryOption> AudioCategoryOptions { get; set; }
        bool AudioMixKotorGameMusic { get; set; }
        bool AudioMixNpcAndPartySounds { get; set; }
        bool AudioRemoveDmcaMusic { get; set; }
        Regex AudioDmcaMusicRegex { get; }
    }
}
