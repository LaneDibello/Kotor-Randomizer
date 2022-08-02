using kotor_Randomizer_2.Extensions;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace kotor_Randomizer_2.DBOs
{
    public class ItemRandoCategoryOption : RandomizationLevelOption
    {
        #region Constructors
        public ItemRandoCategoryOption() { }

        public ItemRandoCategoryOption(ItemRandoCategory category, List<Regex> regex)
            : base(category.ToLabel(), category.ToToolTip())
        {
            Regex = regex;
            Category = category;
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
