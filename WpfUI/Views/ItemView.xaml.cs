using kotor_Randomizer_2;
using kotor_Randomizer_2.DTOs;
using kotor_Randomizer_2.Models;
using kotor_Randomizer_2.Interfaces;
using Randomizer_WPF.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

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
        private ObservableCollection<ItemRandoCategoryOption> lvCategoriesSource;

        private List<RandomizableItem> fullItemList = new List<RandomizableItem>();
        private List<RandomizableItem> initialOmitList = new List<RandomizableItem>();

        private bool constructed = false;
        private bool delaySort = false;
        private bool initializeOmits = true;
        #endregion

        #region Constructor
        public ItemView()
        {
            InitializeComponent();
            cbbItemPresetOptions = new ObservableCollection<string>(RandomizableItem.KOTOR1_OMIT_PRESETS.Keys);
            cbbOmitPreset.ItemsSource = cbbItemPresetOptions;
        }
        #endregion

        #region Properties
        protected static readonly DependencyProperty ExpanderHeightProperty = DependencyProperty.Register(nameof(ExpanderHeight), typeof(double), typeof(ItemView), new PropertyMetadata(24.0));

        protected double ExpanderHeight
        {
            get => (double)GetValue(ExpanderHeightProperty);
            set => SetValue(ExpanderHeightProperty, value);
        }
        #endregion

        #region Events
        public static List<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            var children = new List<T>();
            if (depObj != null)
            {
                for (var i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    var child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T t) children.Add(t);

                    var childItems = FindVisualChildren<T>(child);
                    if (childItems != null && childItems.Any()) children.AddRange(childItems);
                }
            }
            return children;
        }

        private List<RandomizationLevelUserControl_V2> GetRandomizationLevelUserControls()
        {
            var rlucs = new List<RandomizationLevelUserControl_V2>();
            for (var i = 0; i < lvCategories.Items.Count; i++)
            {
                var cp = lvCategories.ItemContainerGenerator.ContainerFromIndex(i) as ContentPresenter;
                rlucs.AddRange(FindVisualChildren<RandomizationLevelUserControl_V2>(cp));
            }
            return rlucs;
        }

        private void BtnToggleAll_Click(object sender, RoutedEventArgs e)
        {
            delaySort = true;
            var rlucs = GetRandomizationLevelUserControls();

            var CheckAllBoxes = rlucs.Any(rluc => !rluc.IsActive);
            foreach (var item in rlucs)
            {
                item.IsActive = CheckAllBoxes;
            }

            delaySort = false;
            CbbOmitPreset_SelectionChanged(this, null);
        }

        private void BtnSubtype_Click(object sender, RoutedEventArgs e)
        {
            delaySort = true;
            var rlucs = GetRandomizationLevelUserControls();
            foreach (var item in rlucs)
            {
                if (item.SubtypeVisible) item.IsSubtype = true;
            }

            delaySort = false;
            CbbOmitPreset_SelectionChanged(this, null);
        }

        private void BtnType_Click(object sender, RoutedEventArgs e)
        {
            delaySort = true;
            var rlucs = GetRandomizationLevelUserControls();
            foreach (var item in rlucs)
            {
                if (item.TypeVisible) item.IsType = true;
            }

            delaySort = false;
            CbbOmitPreset_SelectionChanged(this, null);
        }

        private void BtnMax_Click(object sender, RoutedEventArgs e)
        {
            delaySort = true;
            var rlucs = GetRandomizationLevelUserControls();
            foreach (var item in rlucs)
            {
                if (item.MaxVisible) item.IsMax = true;
            }

            delaySort = false;
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
                _ = lvOmittedItemSource.Remove(item);
            }
        }

        private void BtnRandomizeSelected_Click(object sender, RoutedEventArgs e)
        {
            cbbOmitPreset.SelectedItem = null;
            var selected = lvOmitted.SelectedItems.OfType<RandomizableItem>().ToList();
            foreach (var item in selected)
            {
                lvRandomizedItemSource.Add(item);
                _ = lvOmittedItemSource.Remove(item);
            }
        }

        private void BtnOmitSelected_Click(object sender, RoutedEventArgs e)
        {
            cbbOmitPreset.SelectedItem = null;
            var selected = lvRandomized.SelectedItems.OfType<RandomizableItem>().ToList();
            foreach (var item in selected)
            {
                lvOmittedItemSource.Add(item);
                _ = lvRandomizedItemSource.Remove(item);
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
                _ = lvRandomizedItemSource.Remove(item);
            }
        }

        private void CbbOmitPreset_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbbOmitPreset?.SelectedItem == null)
            {
                // If no preset is selected (custom item omits), do nothing.
            }
            else if (!RandomizableItem.KOTOR1_OMIT_PRESETS.ContainsKey(cbbOmitPreset.SelectedItem.ToString()))
            {
                // If key is invalid, set to default. This method will trigger again and run the code below.
                cbbOmitPreset.SelectedItem = RandomizableItem.KOTOR1_OMIT_PRESETS.Keys.FirstOrDefault();
            }
            else
            {
                // Else, move all to randomized and then move those with matching codes to omitted.
                foreach (var item in lvOmittedItemSource)
                {
                    lvRandomizedItemSource.Add(item);
                }
                lvOmittedItemSource.Clear();

                var codes = RandomizableItem.KOTOR1_OMIT_PRESETS[cbbOmitPreset.SelectedItem.ToString()];
                var omits = lvRandomizedItemSource.Where(x => codes.Contains(x.Code)).ToList();

                foreach (var omit in omits)
                {
                    lvOmittedItemSource.Add(omit);
                    _ = lvRandomizedItemSource.Remove(omit);
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
            _ = lvRandomizedItemSource.Remove(item);
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
            _ = lvOmittedItemSource.Remove(item);
        }

        private void TxtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(lvRandomized.ItemsSource).Refresh();
            CollectionViewSource.GetDefaultView(lvOmitted.ItemsSource).Refresh();
        }

        private void View_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext is IRandomizeItems itemRando)
            {
                lvRandomizedItemSource = itemRando.ItemRandomizedList;
                lvOmittedItemSource = itemRando.ItemOmittedList;
                lvCategoriesSource = itemRando.ItemCategoryOptions;
                //((RandomizerBase)e.OldValue).PropertyChanged -= View_ContextPropertyChanged;
                //((RandomizerBase)e.NewValue).PropertyChanged += View_ContextPropertyChanged;
            }

            // Until K2 rando has item omission, use all space in the item view for category selection.
            if (DataContext is Kotor2Randomizer)
                Grid.SetRowSpan(svCategories, 3);
            else
                Grid.SetRowSpan(svCategories, 1);

            if (lvRandomizedItemSource is null || lvOmittedItemSource is null) return;

            fullItemList.Clear();
            fullItemList.AddRange(lvRandomizedItemSource);
            fullItemList.AddRange(lvOmittedItemSource);

            //initialOmitList = lvOmittedItemSource.ToList();

            //lvRandomizedItemSource.Clear();
            //lvOmittedItemSource.Clear();

            cbbOmitPreset.SelectedItem = cbbItemPresetOptions.First();
        }

        //private void View_ContextPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        //{
        //    if (DataContext is RandomizerBase)
        //    {
        //        RandomizerBase rando;
        //        if (e.PropertyName == nameof(rando.SettingsFilePath))
        //        {
        //            initializeOmits = true;
        //            PopulateOmitList();
        //        }
        //    }
        //}

        private void View_Loaded(object sender, RoutedEventArgs e)
        {
            var view = (CollectionView)CollectionViewSource.GetDefaultView(lvRandomized.ItemsSource);
            if (view != null) view.Filter = HandleListFilter;

            view = (CollectionView)CollectionViewSource.GetDefaultView(lvOmitted.ItemsSource);
            if (view != null) view.Filter = HandleListFilter;

            // Set up the default sort for item omitted list views.
            if (!constructed)
            {
                try
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
                    constructed = true;
                }
                catch (Exception)
                {
                    // Ignore the exception.
                }
            }

            //PopulateOmitList();
        }

        private void PopulateOmitList()
        {
            // TODO: Work on this... might be able to set visible omit items on initial load.
            // It is currently adding duplicates when a settings file is saved or loaded.
            // I think it might only be considering when an omit preset is selected.

            if (initializeOmits && lvCategoriesSource != null)
            {
                var rlucs = GetRandomizationLevelUserControls();
                if (rlucs.Any())
                {
                    lvRandomizedItemSource.Clear();
                    lvOmittedItemSource.Clear();

                    foreach (var rluc in rlucs)
                    {
                        // Don't add this category of items if it's not active.
                        if (!rluc.IsActive) continue;

                        // Add this category of items to the correct list.
                        foreach (var item in fullItemList.Where(i => i.CategoryEnum == (ItemRandoCategory)rluc.Tag))
                        {
                            // If no preset is selected, sort based on IRandomizeItem's lists.
                            if (cbbOmitPreset?.SelectedItem == null)
                            {
                                if (initialOmitList.Contains(item))
                                    lvOmittedItemSource.Add(item);
                                else
                                    lvRandomizedItemSource.Add(item);
                            }
                            else    // Otherwise, sort based on the selected preset.
                            {
                                var preset = RandomizableItem.KOTOR1_OMIT_PRESETS[cbbOmitPreset.SelectedItem.ToString()];
                                if (preset.Contains(item.Code))
                                    lvOmittedItemSource.Add(item);
                                else
                                    lvRandomizedItemSource.Add(item);
                            }
                        }
                    }

                    CbbOmitPreset_SelectionChanged(this, null);

                    initializeOmits = false;
                }
            }
        }

        protected void RandomizationLevelChanged(object tag, RandomizationLevel oldValue, RandomizationLevel newValue)
        {
            var changed = false;

            // Ignore if nothing has changed.
            if (oldValue == newValue) return;
            var category = (ItemRandoCategory)tag;

            // Add items if enabling a randomization.
            if (oldValue == RandomizationLevel.None)
            {
                foreach (var item in fullItemList.Where(i => i.CategoryEnum == category))
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
                foreach (var item in fullItemList.Where(i => i.CategoryEnum == category))
                {
                    _ = lvOmittedItemSource.Remove(item);
                    _ = lvRandomizedItemSource.Remove(item);
                }
                changed = true;
            }

            //if (changed)
            //{
            //    // TODO: Fix databinding --- data binding is not linking the IRCO level to the RLUC level. This is a workaround.
            //    lvCategoriesSource.First(op => op.Category == (ItemRandoCategory)tag).Level = newValue;
            //}

            // Sort items based on category selection.
            if (changed && !delaySort) CbbOmitPreset_SelectionChanged(this, null);
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
