﻿using kotor_Randomizer_2;
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

        bool Constructed = false;
        #endregion

        #region Constructors
        public GeneralView()
        {
            InitializeComponent();
        }
        #endregion

        #region Dependency Properties
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
        #endregion

        #region Public Properties
        public string Kotor1Path
        {
            get { return (string)GetValue(Kotor1PathProperty); }
            set { SetValue(Kotor1PathProperty, value); }
        }

        public string Kotor2Path
        {
            get { return (string)GetValue(Kotor2PathProperty); }
            set { SetValue(Kotor2PathProperty, value); }
        }

        public string PresetPath
        {
            get { return (string)GetValue(PresetPathProperty); }
            set { SetValue(PresetPathProperty, value); }
        }

        public string SpoilerPath
        {
            get { return (string)GetValue(SpoilerPathProperty); }
            set { SetValue(SpoilerPathProperty, value); }
        }

        public bool SaveDataDeleteMilestone
        {
            get { return (bool)GetValue(SaveDataDeleteMilestoneProperty); }
            set { SetValue(SaveDataDeleteMilestoneProperty, value); }
        }

        public bool SaveDataIncludeMinigames
        {
            get { return (bool)GetValue(SaveDataIncludeMinigamesProperty); }
            set { SetValue(SaveDataIncludeMinigamesProperty, value); }
        }

        public bool SaveDataIncludeAll
        {
            get { return (bool)GetValue(SaveDataIncludeAllProperty); }
            set { SetValue(SaveDataIncludeAllProperty, value); }
        }

        public bool QolAddSpiceLab
        {
            get { return (bool)GetValue(QolAddSpiceLabProperty); }
            set { SetValue(QolAddSpiceLabProperty, value); }
        }

        public bool QolFastEnvirosuit
        {
            get { return (bool)GetValue(QolFastEnvirosuitProperty); }
            set { SetValue(QolFastEnvirosuitProperty, value); }
        }

        public bool QolEarlyT3
        {
            get { return (bool)GetValue(QolEarlyT3Property); }
            set { SetValue(QolEarlyT3Property, value); }
        }

        public bool QolFixDreamSequence
        {
            get { return (bool)GetValue(QolFixDreamSequenceProperty); }
            set { SetValue(QolFixDreamSequenceProperty, value); }
        }

        public bool QolFixFighterEncounter
        {
            get { return (bool)GetValue(QolFixFighterEncounterProperty); }
            set { SetValue(QolFixFighterEncounterProperty, value); }
        }

        public bool QolFixMindPrison
        {
            get { return (bool)GetValue(QolFixMindPrisonProperty); }
            set { SetValue(QolFixMindPrisonProperty, value); }
        }

        public bool QolFixModuleCoordinates
        {
            get { return (bool)GetValue(QolFixModuleCoordinatesProperty); }
            set { SetValue(QolFixModuleCoordinatesProperty, value); }
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
                lvUnlockedItemSource.Remove(item);
            }
        }

        private void BtnLockSelected_Click(object sender, RoutedEventArgs e)
        {
            var selected = lvUnlocked.SelectedItems.OfType<UnlockableDoor>().ToList();
            foreach (UnlockableDoor item in selected)
            {
                lvLockedItemSource.Add(item);
                lvUnlockedItemSource.Remove(item);
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
                lvLockedItemSource.Remove(item);
            }
        }

        private void BtnUnlockSelected_Click(object sender, RoutedEventArgs e)
        {
            var selected = lvLocked.SelectedItems.OfType<UnlockableDoor>().ToList();
            foreach (UnlockableDoor item in selected)
            {
                lvUnlockedItemSource.Add(item);
                lvLockedItemSource.Remove(item);
            }
        }

        private void BtnAutoFindPaths_Click(object sender, RoutedEventArgs e)
        {
            bool K1PathFound = false;
            List<string> Kotor1Paths = new List<string>()
            {
                @"C:\Program Files (x86)\Steam\steamapps\common\swkotor",
                @"C:\Program Files\Steam\steamapps\common\swkotor",
                @"C:\Program Files (x86)\LucasArts\SWKotOR",
                @"C:\Program Files\LucasArts\SWKotOR",
            };
            foreach (string path in Kotor1Paths)
            {
                DirectoryInfo di = new DirectoryInfo(path);
                if (di.Exists)
                {
                    Kotor1Path = path;
                    K1PathFound = true;
                    break;
                }
            }
            if (K1PathFound == false) Kotor1Path = string.Empty;

            //bool K2PathFound = false;
            //List<string> Kotor2Paths = new List<string>()
            //{
            //    @"C:\Program Files (x86)\Steam\steamapps\common\Knights of the Old Republic II",
            //    @"C:\Program Files\Steam\steamapps\common\Knights of the Old Republic II",
            //    @"C:\Program Files (x86)\LucasArts\Knights of the Old Republic II",
            //    @"C:\Program Files\LucasArts\Knights of the Old Republic II",
            //};
            //foreach (string path in Kotor2Paths)
            //{
            //    DirectoryInfo di = new DirectoryInfo(path);
            //    if (di.Exists)
            //    {
            //        Kotor2Path = path;
            //        K2PathFound = true;
            //        break;
            //    }
            //}
            //if (K2PathFound == false) Kotor2Path = string.Empty;
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
            lvLockedItemSource.Remove(item);
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
            lvUnlockedItemSource.Remove(item);
        }

        private void TxtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(lvLocked.ItemsSource).Refresh();
            CollectionViewSource.GetDefaultView(lvUnlocked.ItemsSource).Refresh();
        }

        private void View_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext is Kotor1Randomizer k1rand)
            {
                lvLockedItemSource = k1rand.GeneralLockedDoors;
                lvUnlockedItemSource = k1rand.GeneralUnlockedDoors;
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