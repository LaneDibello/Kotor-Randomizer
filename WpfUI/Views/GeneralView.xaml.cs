using kotor_Randomizer_2;
using kotor_Randomizer_2.Models;
using Randomizer_WPF.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Randomizer_WPF.Views
{
    /// <summary>
    /// Interaction logic for GeneralView.xaml
    /// </summary>
    public partial class GeneralView : UserControl
    {
        #region Members

        private ObservableCollection<UnlockableDoor> lvLockedItemSource = new ObservableCollection<UnlockableDoor>();
        private SortAdorner lvLockedSortAdorner = null;
        private GridViewColumnHeader lvLockedSortCol = null;
        private ObservableCollection<UnlockableDoor> lvUnlockedItemSource = new ObservableCollection<UnlockableDoor>();
        private SortAdorner lvUnlockedSortAdorner = null;
        private GridViewColumnHeader lvUnlockedSortCol = null;

        private bool Constructed = false;

        #endregion

        #region Constructors

        public GeneralView()
        {
            InitializeComponent();
        }

        #endregion

        #region Dependency Properties

        public static readonly DependencyProperty IsKotor2SelectedProperty = DependencyProperty.Register("IsKotor2Selected", typeof(bool), typeof(GeneralView), new PropertyMetadata(false));
        public static readonly DependencyProperty Kotor1PathProperty  = DependencyProperty.Register("Kotor1Path",  typeof(string), typeof(GeneralView));
        public static readonly DependencyProperty Kotor2PathProperty  = DependencyProperty.Register("Kotor2Path",  typeof(string), typeof(GeneralView));
        public static readonly DependencyProperty PresetPathProperty  = DependencyProperty.Register("PresetPath",  typeof(string), typeof(GeneralView));
        public static readonly DependencyProperty SpoilerPathProperty = DependencyProperty.Register("SpoilerPath", typeof(string), typeof(GeneralView));

        public static readonly DependencyProperty SaveDataDeleteMilestoneProperty   = DependencyProperty.Register("SaveDataDeleteMilestone",  typeof(bool), typeof(GeneralView));
        public static readonly DependencyProperty SaveDataIncludeMinigamesProperty  = DependencyProperty.Register("SaveDataIncludeMinigames", typeof(bool), typeof(GeneralView));
        public static readonly DependencyProperty SaveDataIncludeAllProperty        = DependencyProperty.Register("SaveDataIncludeAll",       typeof(bool), typeof(GeneralView));
        public static readonly DependencyProperty QolAddSpiceLabProperty            = DependencyProperty.Register("QolAddSpiceLab",           typeof(bool), typeof(GeneralView));
        public static readonly DependencyProperty QolFastEnvirosuitProperty         = DependencyProperty.Register("QolFastEnvirosuit",        typeof(bool), typeof(GeneralView));
        public static readonly DependencyProperty QolEarlyT3Property                = DependencyProperty.Register("QolEarlyT3",               typeof(bool), typeof(GeneralView));
        public static readonly DependencyProperty QolFixDreamSequenceProperty       = DependencyProperty.Register("QolFixDreamSequence",      typeof(bool), typeof(GeneralView));
        public static readonly DependencyProperty QolFixFighterEncounterProperty    = DependencyProperty.Register("QolFixFighterEncounter",   typeof(bool), typeof(GeneralView));
        public static readonly DependencyProperty QolFixMindPrisonProperty          = DependencyProperty.Register("QolFixMindPrison",         typeof(bool), typeof(GeneralView));
        public static readonly DependencyProperty QolFixModuleCoordinatesProperty   = DependencyProperty.Register("QolFixModuleCoordinates",  typeof(bool), typeof(GeneralView));

        public static readonly DependencyProperty K2SavePatchProperty     = DependencyProperty.Register(nameof(K2SavePatch),     typeof(bool), typeof(GeneralView));
        public static readonly DependencyProperty K2DisciplePatchProperty = DependencyProperty.Register(nameof(K2DisciplePatch), typeof(bool), typeof(GeneralView));

        #endregion

        #region Public Properties

        public bool IsKotor2Selected
        {
            get => (bool)GetValue(IsKotor2SelectedProperty);
            set => SetValue(IsKotor2SelectedProperty, value);
        }

        public string Kotor1Path
        {
            get => (string)GetValue(Kotor1PathProperty);
            set => SetValue(Kotor1PathProperty, value);
        }

        public string Kotor2Path
        {
            get => (string)GetValue(Kotor2PathProperty);
            set => SetValue(Kotor2PathProperty, value);
        }

        public string PresetPath
        {
            get => (string)GetValue(PresetPathProperty);
            set => SetValue(PresetPathProperty, value);
        }

        public string SpoilerPath
        {
            get => (string)GetValue(SpoilerPathProperty);
            set => SetValue(SpoilerPathProperty, value);
        }

        public bool SaveDataDeleteMilestone
        {
            get => (bool)GetValue(SaveDataDeleteMilestoneProperty);
            set => SetValue(SaveDataDeleteMilestoneProperty, value);
        }

        public bool SaveDataIncludeMinigames
        {
            get => (bool)GetValue(SaveDataIncludeMinigamesProperty);
            set => SetValue(SaveDataIncludeMinigamesProperty, value);
        }

        public bool SaveDataIncludeAll
        {
            get => (bool)GetValue(SaveDataIncludeAllProperty);
            set => SetValue(SaveDataIncludeAllProperty, value);
        }

        public bool QolAddSpiceLab
        {
            get => (bool)GetValue(QolAddSpiceLabProperty);
            set => SetValue(QolAddSpiceLabProperty, value);
        }

        public bool QolFastEnvirosuit
        {
            get => (bool)GetValue(QolFastEnvirosuitProperty);
            set => SetValue(QolFastEnvirosuitProperty, value);
        }

        public bool QolEarlyT3
        {
            get => (bool)GetValue(QolEarlyT3Property);
            set => SetValue(QolEarlyT3Property, value);
        }

        public bool QolFixDreamSequence
        {
            get => (bool)GetValue(QolFixDreamSequenceProperty);
            set => SetValue(QolFixDreamSequenceProperty, value);
        }

        public bool QolFixFighterEncounter
        {
            get => (bool)GetValue(QolFixFighterEncounterProperty);
            set => SetValue(QolFixFighterEncounterProperty, value);
        }

        public bool QolFixMindPrison
        {
            get => (bool)GetValue(QolFixMindPrisonProperty);
            set => SetValue(QolFixMindPrisonProperty, value);
        }

        public bool QolFixModuleCoordinates
        {
            get => (bool)GetValue(QolFixModuleCoordinatesProperty);
            set => SetValue(QolFixModuleCoordinatesProperty, value);
        }

        public bool K2SavePatch
        {
            get => (bool)GetValue(K2SavePatchProperty);
            set => SetValue(K2SavePatchProperty, value);
        }

        public bool K2DisciplePatch
        {
            get => (bool)GetValue(K2DisciplePatchProperty);
            set => SetValue(K2DisciplePatchProperty, value);
        }

        #endregion

        #region Events

        private void BtnLockAll_Click(object sender, RoutedEventArgs e)
        {
            var view = CollectionViewSource.GetDefaultView(lvUnlocked.ItemsSource);
            var toRemove = new List<UnlockableDoor>();

            foreach (UnlockableDoor item in view)
            {
                lvLockedItemSource.Add(item);
                toRemove.Add(item);
            }

            foreach (var item in toRemove)
            {
                _ = lvUnlockedItemSource.Remove(item);
            }
        }

        private void BtnLockSelected_Click(object sender, RoutedEventArgs e)
        {
            var selected = lvUnlocked.SelectedItems.OfType<UnlockableDoor>().ToList();
            foreach (var item in selected)
            {
                lvLockedItemSource.Add(item);
                _ = lvUnlockedItemSource.Remove(item);
            }
        }

        private void BtnUnlockAll_Click(object sender, RoutedEventArgs e)
        {
            var view = CollectionViewSource.GetDefaultView(lvLocked.ItemsSource);
            var toRemove = new List<UnlockableDoor>();

            foreach (UnlockableDoor item in view)
            {
                lvUnlockedItemSource.Add(item);
                toRemove.Add(item);
            }

            foreach (var item in toRemove)
            {
                _ = lvLockedItemSource.Remove(item);
            }
        }

        private void BtnUnlockSelected_Click(object sender, RoutedEventArgs e)
        {
            var selected = lvLocked.SelectedItems.OfType<UnlockableDoor>().ToList();
            foreach (var item in selected)
            {
                lvUnlockedItemSource.Add(item);
                _ = lvLockedItemSource.Remove(item);
            }
        }

        private void BtnAutoFindPaths_Click(object sender, RoutedEventArgs e)
        {
            // Find K1 Path
            Kotor1Path = FindGoodPathWithFile("swkotor.exe",
                @"C:\Program Files (x86)\Steam\steamapps\common\swkotor",
                @"C:\Program Files\Steam\steamapps\common\swkotor",
                @"C:\Program Files (x86)\LucasArts\SWKotOR",
                @"C:\Program Files\LucasArts\SWKotOR"
            );

            // Find K2 Path
            Kotor2Path = FindGoodPathWithFile("swkotor2.exe",
                @"C:\Program Files (x86)\Steam\steamapps\common\Knights of the Old Republic II",
                @"C:\Program Files\Steam\steamapps\common\Knights of the Old Republic II",
                @"C:\Program Files (x86)\LucasArts\Knights of the Old Republic II",
                @"C:\Program Files\LucasArts\Knights of the Old Republic II"
            );
        }

        /// <summary>
        /// Searchs through a sequence of paths and returns the first one that contains a file with the specified name.
        /// </summary>
        /// <param name="filename">Name of the file to search for.</param>
        /// <param name="paths">Sequence of paths to search.</param>
        /// <returns>First path that contains the specified filename.</returns>
        private string FindGoodPathWithFile(string filename, params string[] paths)
        {
            if (paths.Any())
            {
                foreach (var path in paths)
                {
                    var di = new DirectoryInfo(path);
                    if (di.Exists && di.EnumerateFiles().Any(fi => fi.Name.Equals(filename, StringComparison.CurrentCultureIgnoreCase)))
                    {
                        return path;
                    }
                }
            }

            return string.Empty;
        }

        private void CbSaveAllModules_Checked(object sender, RoutedEventArgs e)
        {
            cbSaveMinigames.IsChecked = true;
        }

        private void LvLockedColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            var column = sender as GridViewColumnHeader;
            var sortBy = column.Tag.ToString();

            SortAdorner.SortColumn(lvLocked,
                                   ref lvLockedSortCol,
                                   ref lvLockedSortAdorner,
                                   column,
                                   sortBy);
        }

        private void LvLockedItem_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Locked item clicked... move it to the Unlocked list.
            var item = ((ListViewItem)sender).Content as UnlockableDoor;
            lvUnlockedItemSource.Add(item);
            _ = lvLockedItemSource.Remove(item);
        }

        private void LvUnlockedColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            var column = sender as GridViewColumnHeader;
            var sortBy = column.Tag.ToString();

            SortAdorner.SortColumn(lvUnlocked,
                                   ref lvUnlockedSortCol,
                                   ref lvUnlockedSortAdorner,
                                   column,
                                   sortBy);
        }

        private void LvUnlockedItem_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Unlocked item clicked... move it to the Locked list.
            var item = ((ListViewItem)sender).Content as UnlockableDoor;
            lvLockedItemSource.Add(item);
            _ = lvUnlockedItemSource.Remove(item);
        }

        private void TxtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(lvLocked.ItemsSource).Refresh();
            CollectionViewSource.GetDefaultView(lvUnlocked.ItemsSource).Refresh();
        }

        private void View_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //if (DataContext is Kotor1Randomizer k1rand)
            //{
            //    lvLockedItemSource = k1rand.GeneralLockedDoors;
            //    lvUnlockedItemSource = k1rand.GeneralUnlockedDoors;
            //}
            if (DataContext is IGeneralSettings settings)
            {
                lvLockedItemSource = settings.GeneralLockedDoors;
                lvUnlockedItemSource = settings.GeneralUnlockedDoors;
            }
        }

        private void View_Loaded(object sender, RoutedEventArgs e)
        {
            var view = (CollectionView)CollectionViewSource.GetDefaultView(lvLocked.ItemsSource);
            if (view != null) view.Filter = UnlockFilter;

            view = (CollectionView)CollectionViewSource.GetDefaultView(lvUnlocked.ItemsSource);
            if (view != null) view.Filter = UnlockFilter;

            if (!Constructed)
            {
                try
                {
                    SortAdorner.SortColumn(lvUnlocked,
                                           ref lvUnlockedSortCol,
                                           ref lvUnlockedSortAdorner,
                                           gvchUnlockedLabel,
                                           gvchUnlockedLabel.Tag.ToString());
                    SortAdorner.SortColumn(lvUnlocked,
                                           ref lvUnlockedSortCol,
                                           ref lvUnlockedSortAdorner,
                                           gvchUnlockedArea,
                                           gvchUnlockedArea.Tag.ToString());
                    SortAdorner.SortColumn(lvLocked,
                                           ref lvLockedSortCol,
                                           ref lvLockedSortAdorner,
                                           gvchLockedArea,
                                           gvchLockedArea.Tag.ToString());
                    Constructed = true;
                }
                catch (Exception)
                {
                    // Ignore the exception.
                }
            }
        }
        #endregion

        #region Methods

        private bool UnlockFilter(object item)
        {
            if (string.IsNullOrEmpty(txtFilter.Text)) return true;
            else return (item as UnlockableDoor).Label.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        (item as UnlockableDoor).Area.IndexOf( txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        #endregion
    }
}