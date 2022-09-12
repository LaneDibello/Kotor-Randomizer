using kotor_Randomizer_2.DTOs;
using kotor_Randomizer_2.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace kotor_Randomizer_2.Interfaces
{
    /// <summary>
    /// Settings related to randomizing items.
    /// </summary>
    public interface IRandomizeItems
    {
        bool DoRandomizeItems { get; }
        ObservableCollection<ItemRandoCategoryOption> ItemCategoryOptions { get; set; }
        ObservableCollection<RandomizableItem> ItemOmittedList { get; set; }
        ObservableCollection<RandomizableItem> ItemRandomizedList { get; set; }
        Dictionary<string, List<string>> ItemOmitPresets { get; }
        string ItemOmittedPreset { get; set; }
    }
}
