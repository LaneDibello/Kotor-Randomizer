using kotor_Randomizer_2;
using kotor_Randomizer_2.Models;
using Randomizer_WPF.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Randomizer_WPF.Views
{
    /// <summary>
    /// Interaction logic for ItemView.xaml
    /// </summary>
    public partial class ItemView : UserControl
    {
        #region Members
        private ObservableCollection<string> cbbItemPresetOptions;
        private ObservableCollection<RandomizableItem> lvRandomizedItemSource;
        private SortAdorner lvRandomizedSortAdorner;
        private GridViewColumnHeader lvRandomizedSortCol;
        private ObservableCollection<RandomizableItem> lvOmittedItemSource;
        private SortAdorner lvOmittedSortAdorner;
        private GridViewColumnHeader lvOmittedSortCol;

        private List<RandomizableItem> _fullItemList = new List<RandomizableItem>();

        private List<RandomizationLevelUserControl> ItemControls = new List<RandomizationLevelUserControl>();

        bool Constructed = false;
        bool DelaySort = false;
        #endregion

        #region Constructor
        public ItemView()
        {
            InitializeComponent();
            ItemControls.AddRange(new List<RandomizationLevelUserControl>()
            {
                rlucArmbands,
                rlucArmor,
                rlucBelts,
                rlucBlaster,
                rlucCreatureHides,
                rlucCreatureWeapons,
                rlucDroidEquipment,
                rlucGauntlets,
                rlucGrenades,
                rlucImplants,
                rlucLightsabers,
                rlucMasks,
                rlucMedical,
                rlucMelee,
                rlucMines,
                rlucPazaak,
                rlucUpgrades,
                rlucVarious,
            });
            cbbItemPresetOptions = new ObservableCollection<string>(Globals.OMIT_ITEM_PRESETS.Keys);
            cbbOmitPreset.ItemsSource = cbbItemPresetOptions;
        }
        #endregion

        #region Dependency Properties
        public static readonly DependencyProperty ItemLevelArmbandsProperty         = DependencyProperty.Register("ItemLevelArmbands", typeof(RandomizationLevel), typeof(ItemView)/*, null, UpdateLists*/);
        public static readonly DependencyProperty ItemLevelArmorProperty            = DependencyProperty.Register("ItemLevelArmor", typeof(RandomizationLevel), typeof(ItemView));
        public static readonly DependencyProperty ItemLevelBeltsProperty            = DependencyProperty.Register("ItemLevelBelts", typeof(RandomizationLevel), typeof(ItemView));
        public static readonly DependencyProperty ItemLevelBlastersProperty         = DependencyProperty.Register("ItemLevelBlasters", typeof(RandomizationLevel), typeof(ItemView));
        public static readonly DependencyProperty ItemLevelCreatureHidesProperty    = DependencyProperty.Register("ItemLevelCreatureHides", typeof(RandomizationLevel), typeof(ItemView));
        public static readonly DependencyProperty ItemLevelCreatureWeaponsProperty  = DependencyProperty.Register("ItemLevelCreatureWeapons", typeof(RandomizationLevel), typeof(ItemView));
        public static readonly DependencyProperty ItemLevelDroidEquipmentProperty   = DependencyProperty.Register("ItemLevelDroidEquipment", typeof(RandomizationLevel), typeof(ItemView));
        public static readonly DependencyProperty ItemLevelGauntletsProperty        = DependencyProperty.Register("ItemLevelGauntlets", typeof(RandomizationLevel), typeof(ItemView));
        public static readonly DependencyProperty ItemLevelGrenadesProperty         = DependencyProperty.Register("ItemLevelGrenades", typeof(RandomizationLevel), typeof(ItemView));
        public static readonly DependencyProperty ItemLevelImplantsProperty         = DependencyProperty.Register("ItemLevelImplants", typeof(RandomizationLevel), typeof(ItemView));
        public static readonly DependencyProperty ItemLevelLightsabersProperty      = DependencyProperty.Register("ItemLevelLightsabers", typeof(RandomizationLevel), typeof(ItemView));
        public static readonly DependencyProperty ItemLevelMasksProperty            = DependencyProperty.Register("ItemLevelMasks", typeof(RandomizationLevel), typeof(ItemView));
        public static readonly DependencyProperty ItemLevelMeleeProperty            = DependencyProperty.Register("ItemLevelMelee", typeof(RandomizationLevel), typeof(ItemView));
        public static readonly DependencyProperty ItemLevelMinesProperty            = DependencyProperty.Register("ItemLevelMines", typeof(RandomizationLevel), typeof(ItemView));
        public static readonly DependencyProperty ItemLevelPazaakProperty           = DependencyProperty.Register("ItemLevelPazaak", typeof(RandomizationLevel), typeof(ItemView));
        public static readonly DependencyProperty ItemLevelMedicalProperty          = DependencyProperty.Register("ItemLevelMedical", typeof(RandomizationLevel), typeof(ItemView));
        public static readonly DependencyProperty ItemLevelUpgradesProperty         = DependencyProperty.Register("ItemLevelUpgrades", typeof(RandomizationLevel), typeof(ItemView));
        public static readonly DependencyProperty ItemLevelVariousProperty          = DependencyProperty.Register("ItemLevelVarious", typeof(RandomizationLevel), typeof(ItemView));
        protected static readonly DependencyProperty ExpanderHeightProperty         = DependencyProperty.Register("ExpanderHeight", typeof(double), typeof(ItemView), new PropertyMetadata(24.0));
        #endregion

        #region Properties
        protected double ExpanderHeight
        {
            get { return (double)GetValue(ExpanderHeightProperty); }
            set { SetValue(ExpanderHeightProperty, value); }
        }

        public RandomizationLevel ItemLevelArmbands
        {
            get { return (RandomizationLevel)GetValue(ItemLevelArmbandsProperty); }
            set { SetValue(ItemLevelArmbandsProperty, value); }
        }

        public RandomizationLevel ItemLevelArmor
        {
            get { return (RandomizationLevel)GetValue(ItemLevelArmorProperty); }
            set { SetValue(ItemLevelArmorProperty, value); }
        }

        public RandomizationLevel ItemLevelBelts
        {
            get { return (RandomizationLevel)GetValue(ItemLevelBeltsProperty); }
            set { SetValue(ItemLevelBeltsProperty, value); }
        }

        public RandomizationLevel ItemLevelBlasters
        {
            get { return (RandomizationLevel)GetValue(ItemLevelBlastersProperty); }
            set { SetValue(ItemLevelBlastersProperty, value); }
        }

        public RandomizationLevel ItemLevelCreatureHides
        {
            get { return (RandomizationLevel)GetValue(ItemLevelCreatureHidesProperty); }
            set { SetValue(ItemLevelCreatureHidesProperty, value); }
        }

        public RandomizationLevel ItemLevelCreatureWeapons
        {
            get { return (RandomizationLevel)GetValue(ItemLevelCreatureWeaponsProperty); }
            set { SetValue(ItemLevelCreatureWeaponsProperty, value); }
        }

        public RandomizationLevel ItemLevelDroidEquipment
        {
            get { return (RandomizationLevel)GetValue(ItemLevelDroidEquipmentProperty); }
            set { SetValue(ItemLevelDroidEquipmentProperty, value); }
        }

        public RandomizationLevel ItemLevelGauntlets
        {
            get { return (RandomizationLevel)GetValue(ItemLevelGauntletsProperty); }
            set { SetValue(ItemLevelGauntletsProperty, value); }
        }

        public RandomizationLevel ItemLevelGrenades
        {
            get { return (RandomizationLevel)GetValue(ItemLevelGrenadesProperty); }
            set { SetValue(ItemLevelGrenadesProperty, value); }
        }

        public RandomizationLevel ItemLevelImplants
        {
            get { return (RandomizationLevel)GetValue(ItemLevelImplantsProperty); }
            set { SetValue(ItemLevelImplantsProperty, value); }
        }

        public RandomizationLevel ItemLevelLightsabers
        {
            get { return (RandomizationLevel)GetValue(ItemLevelLightsabersProperty); }
            set { SetValue(ItemLevelLightsabersProperty, value); }
        }

        public RandomizationLevel ItemLevelMasks
        {
            get { return (RandomizationLevel)GetValue(ItemLevelMasksProperty); }
            set { SetValue(ItemLevelMasksProperty, value); }
        }

        public RandomizationLevel ItemLevelMelee
        {
            get { return (RandomizationLevel)GetValue(ItemLevelMeleeProperty); }
            set { SetValue(ItemLevelMeleeProperty, value); }
        }

        public RandomizationLevel ItemLevelMines
        {
            get { return (RandomizationLevel)GetValue(ItemLevelMinesProperty); }
            set { SetValue(ItemLevelMinesProperty, value); }
        }

        public RandomizationLevel ItemLevelPazaak
        {
            get { return (RandomizationLevel)GetValue(ItemLevelPazaakProperty); }
            set { SetValue(ItemLevelPazaakProperty, value); }
        }

        public RandomizationLevel ItemLevelMedical
        {
            get { return (RandomizationLevel)GetValue(ItemLevelMedicalProperty); }
            set { SetValue(ItemLevelMedicalProperty, value); }
        }

        public RandomizationLevel ItemLevelUpgrades
        {
            get { return (RandomizationLevel)GetValue(ItemLevelUpgradesProperty); }
            set { SetValue(ItemLevelUpgradesProperty, value); }
        }

        public RandomizationLevel ItemLevelVarious
        {
            get { return (RandomizationLevel)GetValue(ItemLevelVariousProperty); }
            set { SetValue(ItemLevelVariousProperty, value); }
        }
        #endregion

        #region Events
        private void BtnToggleAll_Click(object sender, RoutedEventArgs e)
        {
            DelaySort = true;
            bool CheckAllBoxes = ItemControls.Any(rluc => !rluc.IsChecked);

            foreach (var item in ItemControls)
            {
                item.IsChecked = CheckAllBoxes;
            }

            DelaySort = false;
            CbbOmitPreset_SelectionChanged(this, null);
        }

        private void BtnSubtype_Click(object sender, RoutedEventArgs e)
        {
            DelaySort = true;
            foreach (var item in ItemControls)
            {
                item.SelectedLevel = RandomizationLevel.Subtype;
            }

            DelaySort = false;
            CbbOmitPreset_SelectionChanged(this, null);
        }

        private void BtnType_Click(object sender, RoutedEventArgs e)
        {
            DelaySort = true;
            foreach (var item in ItemControls)
            {
                item.SelectedLevel = RandomizationLevel.Type;
            }

            DelaySort = false;
            CbbOmitPreset_SelectionChanged(this, null);
        }

        private void BtnMax_Click(object sender, RoutedEventArgs e)
        {
            DelaySort = true;
            foreach (var item in ItemControls)
            {
                item.SelectedLevel = RandomizationLevel.Max;
            }

            DelaySort = false;
            CbbOmitPreset_SelectionChanged(this, null);
        }

        private void BtnRandomizeAll_Click(object sender, RoutedEventArgs e)
        {
            cbbOmitPreset.SelectedItem = null;
            var view = CollectionViewSource.GetDefaultView(lvOmitted.ItemsSource);
            var toRemove = new List<RandomizableItem>();

            foreach (RandomizableItem item in view)
            {
                lvRandomizedItemSource.Add(item);
                toRemove.Add(item);
            }

            foreach (var item in toRemove)
            {
                lvOmittedItemSource.Remove(item);
            }
        }

        private void BtnRandomizeSelected_Click(object sender, RoutedEventArgs e)
        {
            cbbOmitPreset.SelectedItem = null;
            var selected = lvOmitted.SelectedItems.OfType<RandomizableItem>().ToList();
            foreach (RandomizableItem item in selected)
            {
                lvRandomizedItemSource.Add(item);
                lvOmittedItemSource.Remove(item);
            }
        }

        private void BtnOmitSelected_Click(object sender, RoutedEventArgs e)
        {
            cbbOmitPreset.SelectedItem = null;
            var selected = lvRandomized.SelectedItems.OfType<RandomizableItem>().ToList();
            foreach (RandomizableItem item in selected)
            {
                lvOmittedItemSource.Add(item);
                lvRandomizedItemSource.Remove(item);
            }
        }

        private void BtnOmitAll_Click(object sender, RoutedEventArgs e)
        {
            cbbOmitPreset.SelectedItem = null;
            var view = CollectionViewSource.GetDefaultView(lvRandomized.ItemsSource);
            var toRemove = new List<RandomizableItem>();

            foreach (RandomizableItem item in view)
            {
                lvOmittedItemSource.Add(item);
                toRemove.Add(item);
            }

            foreach (var item in toRemove)
            {
                lvRandomizedItemSource.Remove(item);
            }
        }

        private void CbbOmitPreset_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbbOmitPreset?.SelectedItem == null)
            {
                // Do nothing.
            }
            else if (!Globals.OMIT_ITEM_PRESETS.ContainsKey(cbbOmitPreset.SelectedItem.ToString()))
            {
                // If key is invalid, set to default. This method will trigger again and run the code below.
                cbbOmitPreset.SelectedItem = Globals.OMIT_ITEM_PRESETS.Keys.FirstOrDefault();
            }
            else
            {
                // Else, move all to randomized and then move those with matching codes to omitted.
                foreach (var item in lvOmittedItemSource)
                {
                    lvRandomizedItemSource.Add(item);
                }
                lvOmittedItemSource.Clear();

                var codes = Globals.OMIT_ITEM_PRESETS[cbbOmitPreset.SelectedItem.ToString()];
                var omits = lvRandomizedItemSource.Where(x => codes.Contains(x.Code)).ToList();

                foreach (var omit in omits)
                {
                    lvOmittedItemSource.Add(omit);
                    lvRandomizedItemSource.Remove(omit);
                }
            }
        }

        private void LvRandomized_ColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            var column = sender as GridViewColumnHeader;
            var sortBy = column.Tag.ToString();

            SortAdorner.SortColumn(lvRandomized,
                                   ref lvRandomizedSortCol,
                                   ref lvRandomizedSortAdorner,
                                   column,
                                   sortBy);
        }

        private void LvRandomized_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Randomized item clicked ... move it to the Omitted list.
            cbbOmitPreset.SelectedItem = null;
            var item = ((ListViewItem)sender).Content as RandomizableItem;
            lvOmittedItemSource.Add(item);
            lvRandomizedItemSource.Remove(item);
        }

        private void LvOmitted_ColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            var column = sender as GridViewColumnHeader;
            var sortBy = column.Tag.ToString();

            SortAdorner.SortColumn(lvOmitted,
                                   ref lvOmittedSortCol,
                                   ref lvOmittedSortAdorner,
                                   column,
                                   sortBy);
        }

        private void LvOmitted_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Omitted item clicked ... move it to the Randomized list.
            cbbOmitPreset.SelectedItem = null;
            var item = ((ListViewItem)sender).Content as RandomizableItem;
            lvRandomizedItemSource.Add(item);
            lvOmittedItemSource.Remove(item);
        }

        private void TxtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(lvRandomized.ItemsSource).Refresh();
            CollectionViewSource.GetDefaultView(lvOmitted.ItemsSource).Refresh();
        }

        private void View_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext is Kotor1Randomizer k1rand)
            {
                lvRandomizedItemSource = k1rand.ItemRandomizedList;
                lvOmittedItemSource = k1rand.ItemOmittedList;
            }

            _fullItemList.Clear();
            _fullItemList.AddRange(lvRandomizedItemSource);
            _fullItemList.AddRange(lvOmittedItemSource);

            lvRandomizedItemSource.Clear();
            lvOmittedItemSource.Clear();

            cbbOmitPreset.SelectedItem = cbbItemPresetOptions.First();
        }

        private void View_Loaded(object sender, RoutedEventArgs e)
        {
            var view = (CollectionView)CollectionViewSource.GetDefaultView(lvRandomized.ItemsSource);
            if (view != null) view.Filter = HandleListFilter;

            view = (CollectionView)CollectionViewSource.GetDefaultView(lvOmitted.ItemsSource);
            if (view != null) view.Filter = HandleListFilter;

            // Set up the default sort for item omitted list views.
            if (!Constructed)
            {
                SortAdorner.SortColumn(lvRandomized,
                                       ref lvRandomizedSortCol,
                                       ref lvRandomizedSortAdorner,
                                       gvchRandomizedLabel,
                                       gvchRandomizedLabel.Tag.ToString());
                SortAdorner.SortColumn(lvRandomized,
                                       ref lvRandomizedSortCol,
                                       ref lvRandomizedSortAdorner,
                                       gvchRandomizedCategory,
                                       gvchRandomizedCategory.Tag.ToString());
                SortAdorner.SortColumn(lvOmitted,
                                       ref lvOmittedSortCol,
                                       ref lvOmittedSortAdorner,
                                       gvchOmittedCategory,
                                       gvchOmittedCategory.Tag.ToString());
                Constructed = true;
            }
        }

        private void RandomizationLevelChanged(string category, RandomizationLevel oldValue, RandomizationLevel newValue)
        {
            var changed = false;

            // Ignore if nothing has changed.
            if (oldValue == newValue) return;

            // Add items if enabling a randomization.
            if (oldValue == RandomizationLevel.None)
            {
                foreach (var item in _fullItemList.Where(i => i.Category == category))
                {
                    // Only add it if it's not already in the list.
                    if (!lvRandomizedItemSource.Contains(item))
                        lvRandomizedItemSource.Add(item);
                }
                changed = true;
            }

            // Remove items if disabling a randomization.
            if (newValue == RandomizationLevel.None)
            {
                foreach (var item in _fullItemList.Where(i => i.Category == category))
                {
                    lvOmittedItemSource.Remove(item);
                    lvRandomizedItemSource.Remove(item);
                }
                changed = true;
            }

            // Sort items based on category selection.
            if (changed && !DelaySort) CbbOmitPreset_SelectionChanged(this, null);
        }
        #endregion

        #region Methods
        private bool HandleListFilter(object item)
        {
            if (string.IsNullOrEmpty(txtFilter.Text)) return true;
            else return (item as RandomizableItem).Code.IndexOf( txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        (item as RandomizableItem).Label.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0;
        }
        #endregion
    }
}
