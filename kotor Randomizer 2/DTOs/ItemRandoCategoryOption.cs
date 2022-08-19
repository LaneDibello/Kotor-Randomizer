using kotor_Randomizer_2.Extensions;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace kotor_Randomizer_2.DTOs
{
    public class ItemRandoCategoryOption : RandomizationLevelOption
    {
        #region Constructors
        public ItemRandoCategoryOption() { }

        public ItemRandoCategoryOption(ItemRandoCategory category, List<Regex> regex,
                bool subtypeVisible = true, string subtypeLabel = "Subtype",
                bool typeVisible = true, string typeLabel = "Type",
                bool maxVisible = true, string maxLabel = "Max",
                RandomizationLevel level = RandomizationLevel.None)
            : base(category.ToLabel(), category.ToToolTip(), subtypeVisible, subtypeLabel, typeVisible, typeLabel, maxVisible, maxLabel, level)
        {
            Category = category;
            Regex = regex;
        }
        #endregion

        #region Properties
        private ItemRandoCategory _category;
        public ItemRandoCategory Category
        {
            get => _category;
            set => SetField(ref _category, value);
        }

        private List<Regex> _regex;
        public List<Regex> Regex
        {
            get => _regex;
            set => SetField(ref _regex, value);
        }
        #endregion
    }
}
