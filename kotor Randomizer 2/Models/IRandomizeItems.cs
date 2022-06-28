using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kotor_Randomizer_2.Models
{
    public interface IRandomizeItems
    {
        bool DoRandomizeItems { get; }

        RandomizationLevel ItemArmbands { get; set; }
        RandomizationLevel ItemArmor { get; set; }
        RandomizationLevel ItemBelts { get; set; }
        RandomizationLevel ItemBlasters { get; set; }
        RandomizationLevel ItemCreatureHides { get; set; }
        RandomizationLevel ItemCreatureWeapons { get; set; }
        RandomizationLevel ItemDroidEquipment { get; set; }
        RandomizationLevel ItemGloves { get; set; }
        RandomizationLevel ItemGrenades { get; set; }
        RandomizationLevel ItemImplants { get; set; }
        RandomizationLevel ItemLightsabers { get; set; }
        RandomizationLevel ItemMasks { get; set; }
        RandomizationLevel ItemMeleeWeapons { get; set; }
        RandomizationLevel ItemMines { get; set; }
        RandomizationLevel ItemPazaakCards { get; set; }
        RandomizationLevel ItemMedical { get; set; }
        RandomizationLevel ItemUpgrades { get; set; }
        RandomizationLevel ItemVarious { get; set; }

        ObservableCollection<RandomizableItem> ItemOmittedList { get; set; }
        ObservableCollection<RandomizableItem> ItemRandomizedList { get; set; }
        string ItemOmittedPreset { get; set; }
    }
}
