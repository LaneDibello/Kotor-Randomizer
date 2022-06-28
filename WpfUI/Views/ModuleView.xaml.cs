using kotor_Randomizer_2;
using kotor_Randomizer_2.Models;
using Randomizer_WPF.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for ModuleView.xaml
    /// </summary>
    public partial class ModuleView : UserControl
    {
        #region Members
        private ObservableCollection<string> cbbModulePresetOptions;
        private SortAdorner lvOmittedModulesAdorner;
        private GridViewColumnHeader lvOmittedModulesHeader;
        private ObservableCollection<ModuleVertex> lvOmittedModulesSource;
        private SortAdorner lvRandomizedModulesAdorner;
        private GridViewColumnHeader lvRandomizedModulesHeader;
        private ObservableCollection<ModuleVertex> lvRandomizedModulesSource;
        #endregion

        #region Constructors
        public ModuleView()
        {
            InitializeComponent();
            cbbModulePresetOptions = new ObservableCollection<string>(Globals.K1_MODULE_OMIT_PRESETS.Keys);

            //ModuleDigraph graph;
            //var path = System.IO.Path.Combine(Environment.CurrentDirectory, "Xml", "KotorModules.xml");
            //if (System.IO.File.Exists(System.IO.Path.Combine(Environment.CurrentDirectory, "Xml", "KotorModules.xml")))
            //{
            //    graph = new ModuleDigraph(path);
            //}
            //else
            //{
            //    path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"GitHub\Kotor-Randomizer-2\kotor Randomizer 2\Xml\KotorModules.xml");
            //    graph = new ModuleDigraph(path);
            //}
            
            lvRandomizedModulesSource = new ObservableCollection<ModuleVertex>(/*graph.Modules*/);
            lvOmittedModulesSource = new ObservableCollection<ModuleVertex>();
            cbbShufflePreset.ItemsSource = cbbModulePresetOptions;

            //lvShuffleIncluded.ItemsSource = lvRandomizedModulesSource;
        }
        #endregion

        #region Dependency Properties
        public static readonly DependencyProperty AllowGlitchClipProperty    = DependencyProperty.Register("AllowGlitchClip",    typeof(bool), typeof(ModuleView));
        public static readonly DependencyProperty AllowGlitchDLZProperty     = DependencyProperty.Register("AllowGlitchDLZ",     typeof(bool), typeof(ModuleView));
        public static readonly DependencyProperty AllowGlitchFLUProperty     = DependencyProperty.Register("AllowGlitchFLU",     typeof(bool), typeof(ModuleView));
        public static readonly DependencyProperty AllowGlitchGPWProperty     = DependencyProperty.Register("AllowGlitchGPW",     typeof(bool), typeof(ModuleView));
        public static readonly DependencyProperty AllowGlitchHotshotProperty = DependencyProperty.Register("AllowGlitchHotshot", typeof(bool), typeof(ModuleView));

        public static readonly DependencyProperty IgnoreSingleUseTransitionsProperty = DependencyProperty.Register("IgnoreSingleUseTransitions", typeof(bool), typeof(ModuleView));
        public static readonly DependencyProperty UseRandoRulesProperty = DependencyProperty.Register("UseRandoRules", typeof(bool), typeof(ModuleView));
        public static readonly DependencyProperty VerifyReachabilityProperty = DependencyProperty.Register("VerifyReachability", typeof(bool), typeof(ModuleView));
        #endregion

        #region Public Properties
        public bool AllowGlitchClip
        {
            get { return (bool)GetValue(AllowGlitchClipProperty); }
            set { SetValue(AllowGlitchClipProperty, value); }
        }

        public bool AllowGlitchDLZ
        {
            get { return (bool)GetValue(AllowGlitchDLZProperty); }
            set { SetValue(AllowGlitchDLZProperty, value); }
        }

        public bool AllowGlitchFLU
        {
            get { return (bool)GetValue(AllowGlitchFLUProperty); }
            set { SetValue(AllowGlitchFLUProperty, value); }
        }

        public bool AllowGlitchGPW
        {
            get { return (bool)GetValue(AllowGlitchGPWProperty); }
            set { SetValue(AllowGlitchGPWProperty, value); }
        }

        public bool AllowGlitchHotshot
        {
            get { return (bool)GetValue(AllowGlitchHotshotProperty); }
            set { SetValue(AllowGlitchHotshotProperty, value); }
        }

        public bool IgnoreSingleUseTransitions
        {
            get { return (bool)GetValue(IgnoreSingleUseTransitionsProperty); }
            set { SetValue(IgnoreSingleUseTransitionsProperty, value); }
        }

        public bool UseRandoRules
        {
            get { return (bool)GetValue(UseRandoRulesProperty); }
            set { SetValue(UseRandoRulesProperty, value); }
        }

        public bool VerifyReachability
        {
            get { return (bool)GetValue(VerifyReachabilityProperty); }
            set { SetValue(VerifyReachabilityProperty, value); }
        }
        #endregion

        #region Events
        private void BtnOmitAll_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("----> Entering Omit All");
            cbbShufflePreset.SelectedItem = null;
            var view = CollectionViewSource.GetDefaultView(lvShuffleIncluded.ItemsSource);
            var toRemove = new List<ModuleVertex>();

            foreach (ModuleVertex item in view)
            {
                lvOmittedModulesSource.Add(item);
                toRemove.Add(item);
            }

            foreach (var item in toRemove)
            {
                lvRandomizedModulesSource.Remove(item);
            }
        }

        private void BtnOmitSelected_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("----> Entering Omit Selected");
            cbbShufflePreset.SelectedItem = null;
            var selected = lvShuffleIncluded.SelectedItems.OfType<ModuleVertex>().ToList();
            foreach (var item in selected)
            {
                lvOmittedModulesSource.Add(item);
                lvRandomizedModulesSource.Remove(item);
            }
        }

        private void BtnRandomizeAll_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("----> Entering Rando All");
            cbbShufflePreset.SelectedItem = null;
            var view = CollectionViewSource.GetDefaultView(lvShuffleExcluded.ItemsSource);
            var toRemove = new List<ModuleVertex>();

            foreach (ModuleVertex item in view)
            {
                lvRandomizedModulesSource.Add(item);
                toRemove.Add(item);
            }

            foreach (var item in toRemove)
            {
                lvOmittedModulesSource.Remove(item);
            }
        }

        private void BtnRandomizeSelected_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("----> Entering Rando Selected");
            cbbShufflePreset.SelectedItem = null;
            var selected = lvShuffleExcluded.SelectedItems.OfType<ModuleVertex>().ToList();
            foreach (var item in selected)
            {
                lvRandomizedModulesSource.Add(item);
                lvOmittedModulesSource.Remove(item);
            }
        }

        private void CbbShufflePreset_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var randmod = DataContext as IRandomizeModules;
            if (cbbShufflePreset.SelectedItem == null)
            {
                // Do nothing. Custom settings are in use.
            }
            else if (string.Equals("Off", cbbShufflePreset.SelectedItem.ToString(), StringComparison.CurrentCultureIgnoreCase))
            {
                // If "Off", move all to omitted.
                foreach (var item in lvRandomizedModulesSource)
                {
                    lvOmittedModulesSource.Add(item);
                }
                lvRandomizedModulesSource.Clear();
            }
            else if (!randmod.ModuleOmitPresets.ContainsKey(cbbShufflePreset.SelectedItem.ToString()))
            {
                // If key is invalid, set to off. This method will trigger again and run the code above.
                cbbShufflePreset.SelectedItem = "Off";
            }
            else
            {
                // Else, move all to randomized and then move those with matching codes to omitted.
                foreach (var item in lvOmittedModulesSource)
                {
                    lvRandomizedModulesSource.Add(item);
                }
                lvOmittedModulesSource.Clear();

                var codes = randmod.ModuleOmitPresets[cbbShufflePreset.SelectedItem.ToString()];
                var omits = lvRandomizedModulesSource.Where(x => codes.Contains(x.WarpCode)).ToList();

                foreach (var omit in omits)
                {
                    lvOmittedModulesSource.Add(omit);
                    lvRandomizedModulesSource.Remove(omit);
                }
            }
        }

        private void LvShuffleExcluded_ColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            var column = sender as GridViewColumnHeader;
            var sortBy = column.Tag.ToString();
            SortAdorner.SortColumn(lvShuffleExcluded,
                                   ref lvOmittedModulesHeader,
                                   ref lvOmittedModulesAdorner,
                                   column, sortBy);
        }

        private void LvShuffleExcluded_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("----> Entering Omit Dbl Click");
            cbbShufflePreset.SelectedItem = null;
            var item = ((ListViewItem)sender).Content as ModuleVertex;
            lvRandomizedModulesSource.Add(item);
            lvOmittedModulesSource.Remove(item);
        }

        private void LvShuffleIncluded_ColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            var column = sender as GridViewColumnHeader;
            var sortBy = column.Tag.ToString();
            SortAdorner.SortColumn(lvShuffleIncluded,
                                   ref lvRandomizedModulesHeader,
                                   ref lvRandomizedModulesAdorner,
                                   column, sortBy);
        }

        private void LvShuffleIncluded_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("----> Entering Rando Dbl Click");
            cbbShufflePreset.SelectedItem = null;
            var item = ((ListViewItem)sender).Content as ModuleVertex;
            lvOmittedModulesSource.Add(item);
            lvRandomizedModulesSource.Remove(item);
        }

        private void TxtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(lvShuffleExcluded.ItemsSource).Refresh();
            CollectionViewSource.GetDefaultView(lvShuffleIncluded.ItemsSource).Refresh();
        }

        private void View_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext is kotor_Randomizer_2.Models.Kotor1Randomizer k1rand)
            {
                //k1rand.ModuleRandomizedList = new ObservableCollection<ModuleVertex>(lvRandomizedModulesSource);
                lvRandomizedModulesSource = k1rand.ModuleRandomizedList;
                lvOmittedModulesSource = k1rand.ModuleOmittedList;
            }
            if (DataContext is kotor_Randomizer_2.Models.Kotor2Randomizer k2rand)
            {
                lvRandomizedModulesSource = k2rand.ModuleRandomizedList;
                lvOmittedModulesSource = k2rand.ModuleOmittedList;
                cbbShufflePreset.ItemsSource = k2rand.ModulePresetOptions;
                //cbbShufflePreset.SelectedItem = k2rand.ModulePresetOptions.First();
            }
            //cbbShufflePreset.ItemsSource = cbbModulePresetOptions;
            //cbbShufflePreset.SelectedItem = cbbModulePresetOptions.First();
        }

        private void View_Loaded(object sender, RoutedEventArgs e)
        {
            var view = (CollectionView)CollectionViewSource.GetDefaultView(lvShuffleIncluded.ItemsSource);
            if (view != null) view.Filter = HandleListFilter;

            view = (CollectionView)CollectionViewSource.GetDefaultView(lvShuffleExcluded.ItemsSource);
            if (view != null) view.Filter = HandleListFilter;
        }
        #endregion

        #region Methods
        private bool HandleListFilter(object item)
        {
            if (string.IsNullOrEmpty(txtFilter.Text)) return true;
            else return (item as ModuleVertex).Planet.IndexOf(    txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        (item as ModuleVertex).WarpCode.IndexOf(  txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        (item as ModuleVertex).CommonName.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0;
        }
        #endregion
    }
}
