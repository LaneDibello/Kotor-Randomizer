using kotor_Randomizer_2.DTOs;
using kotor_Randomizer_2.Models;
using System.Collections.ObjectModel;

namespace kotor_Randomizer_2.Interfaces
{
    public interface IRandomizeItems
    {
        bool DoRandomizeItems { get; }
        ObservableCollection<ItemRandoCategoryOption> ItemCategoryOptions { get; set; }
        ObservableCollection<RandomizableItem> ItemOmittedList { get; set; }
        ObservableCollection<RandomizableItem> ItemRandomizedList { get; set; }
        string ItemOmittedPreset { get; set; }
    }
}
