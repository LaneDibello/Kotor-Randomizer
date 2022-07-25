using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kotor_Randomizer_2.Models
{
    public interface IGeneralSettings
    {
        Dictionary<string, Tuple<float, float, float>> FixedCoordinates { get; }
        SavePatchOptions GeneralSaveOptions { get; set; }
        ObservableCollection<UnlockableDoor> GeneralUnlockedDoors { get; set; }
        ObservableCollection<UnlockableDoor> GeneralLockedDoors { get; set; }
    }
}
