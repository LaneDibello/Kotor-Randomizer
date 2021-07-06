using kotor_Randomizer_2;
using kotor_Randomizer_2.Models;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Randomizer_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constants
        public const string SETTINGS_FILENAME = "settings.xml";
        #endregion

        #region Constructors
        public MainWindow()
        {
            InitializeComponent();

            var path = Path.Combine(Environment.CurrentDirectory, SETTINGS_FILENAME);
            SettingsFile file = null;
            if (File.Exists(path))
            {
                try
                {
                    file = new SettingsFile(path);
                }
                catch (Exception)
                {
                    File.Delete(path);  // Delete the bad file.
                }
            }

            if (file == null)
            {
                file = new SettingsFile()
                {
                    Kotor1Path = @"",
                    Kotor2Path = @"",
                    SpoilerPath = @"C:\Users\chilley\Documents\Knights",// TODO: Change to a general folder before release.
                };
                file.WriteFile(path);
            }

            K1Randomizer = new Kotor1Randomizer();
            Kotor1Path = file.Kotor1Path;
            Kotor2Path = file.Kotor2Path;
            SpoilerPath = file.SpoilerPath;

            DataContext = K1Randomizer;
        }
        #endregion

        #region Dependency Properties
        public static readonly DependencyProperty Kotor1PathProperty  = DependencyProperty.Register("Kotor1Path",  typeof(string), typeof(MainWindow));
        public static readonly DependencyProperty Kotor2PathProperty  = DependencyProperty.Register("Kotor2Path",  typeof(string), typeof(MainWindow));
        public static readonly DependencyProperty SpoilerPathProperty = DependencyProperty.Register("SpoilerPath", typeof(string), typeof(MainWindow));
        #endregion

        #region Properties
        public Kotor1Randomizer K1Randomizer { get; set; }

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

        public string SpoilerPath
        {
            get { return (string)GetValue(SpoilerPathProperty); }
            set { SetValue(SpoilerPathProperty, value); }
        }

        public string WindowTitle
        {
            get
            {
                Version v = System.Reflection.Assembly.GetAssembly(typeof(Kotor1Randomizer)).GetName().Version;
                return $"Kotor Randomizer (v{v.Major}.{v.Minor}.{v.Build})";
            }
        }
        #endregion

        #region Events
        private void ModuleTab_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //string message =
            //    $"ModuleLogicRandoRules: {K1Randomizer.ModuleLogicRandoRules}{Environment.NewLine}" +
            //    $"ModuleLogicReachability: {K1Randomizer.ModuleLogicReachability}{Environment.NewLine}" +
            //    $"ModuleLogicIgnoreOnceEdges: {K1Randomizer.ModuleLogicIgnoreOnceEdges}{Environment.NewLine}" +
            //    $"Change the values?";

            MessageBox.Show($"Module Randomization is {(K1Randomizer.DoRandomizeModules ? "active" : "inactive")}.");

            //if (MessageBox.Show(message, "Decide", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            //{
            //    K1Randomizer.ModuleLogicRandoRules = !K1Randomizer.ModuleLogicRandoRules;
            //    K1Randomizer.ModuleLogicReachability = !K1Randomizer.ModuleLogicReachability;
            //    K1Randomizer.ModuleLogicIgnoreOnceEdges = !K1Randomizer.ModuleLogicIgnoreOnceEdges;
            //}
        }

        private void ItemTab_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string message =
                $"ItemArmbands: {K1Randomizer.ItemArmbands}{Environment.NewLine}" +
                $"ItemArmor: {K1Randomizer.ItemArmor}{Environment.NewLine}" +
                $"ItemBelts: {K1Randomizer.ItemBelts}{Environment.NewLine}" +
                $"Change the values?";
            if (MessageBox.Show(message, "Decide", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                K1Randomizer.ItemArmbands = (RandomizationLevel)((((int)K1Randomizer.ItemArmbands) + 1) % 4);
                K1Randomizer.ItemArmor = (RandomizationLevel)((((int)K1Randomizer.ItemArmor) + 1) % 4);
                K1Randomizer.ItemBelts = (RandomizationLevel)((((int)K1Randomizer.ItemBelts) + 1) % 4);
            }
        }

        private void MiOpenSpoilerFolder_Click(object sender, RoutedEventArgs e)
        {
            if (Directory.Exists(SpoilerPath))
            {
                System.Diagnostics.Process.Start(SpoilerPath);
            }
        }

        private void MiHyperlink_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start((sender as FrameworkElement).Tag.ToString());
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            var file = new SettingsFile()
            {
                Kotor1Path  = Kotor1Path,
                Kotor2Path  = Kotor2Path,
                SpoilerPath = SpoilerPath,
            };
            file.WriteFile(Path.Combine(Environment.CurrentDirectory, SETTINGS_FILENAME));
        }
        #endregion

        private void SaveTest_Click(object sender, RoutedEventArgs e)
        {
            K1Randomizer.Save(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"Knights\test.xkrp"));
        }

        private void LoadTest_Click(object sender, RoutedEventArgs e)
        {
            K1Randomizer.Load(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"Knights\test.xkrp"));
        }
    }
}
