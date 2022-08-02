using kotor_Randomizer_2.DBOs;
using System.Collections.ObjectModel;

namespace kotor_Randomizer_2.Models
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
