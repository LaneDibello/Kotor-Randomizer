using kotor_Randomizer_2;
using kotor_Randomizer_2.Models;
using Microsoft.Win32;
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
        #endregion Constants

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
                    CreateSpoilers = false,
                    Kotor1Path  = @"",
                    Kotor2Path  = @"",
                    PresetPath  = Path.Combine(Environment.CurrentDirectory, "Presets"),
                    SpoilerPath = Path.Combine(Environment.CurrentDirectory, "Spoilers"),
                };
                file.WriteFile(path);
            }

            K1Randomizer = new Kotor1Randomizer();
            miCreateSpoilers.IsChecked = file.CreateSpoilers;
            Kotor1Path  = file.Kotor1Path;
            Kotor2Path  = file.Kotor2Path;
            PresetPath  = file.PresetPath;
            SpoilerPath = file.SpoilerPath;

            DataContext = K1Randomizer;
        }
        #endregion Constructors

        #region Dependency Properties
        public static readonly DependencyProperty Kotor1PathProperty  = DependencyProperty.Register("Kotor1Path",  typeof(string), typeof(MainWindow));
        public static readonly DependencyProperty Kotor2PathProperty  = DependencyProperty.Register("Kotor2Path",  typeof(string), typeof(MainWindow));
        public static readonly DependencyProperty PresetPathProperty  = DependencyProperty.Register("PresetPath",  typeof(string), typeof(MainWindow));
        public static readonly DependencyProperty SpoilerPathProperty = DependencyProperty.Register("SpoilerPath", typeof(string), typeof(MainWindow));
        #endregion Dependency Properties

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

        public bool IsRandomizeViewBusy
        {
            get
            {
                return (RandomizeView != null) && (RandomizeView.IsBusy);
            }
        }

        public string WindowTitle
        {
            get
            {
                Version v = System.Reflection.Assembly.GetAssembly(typeof(Kotor1Randomizer)).GetName().Version;
                return $"Kotor Randomizer (v{v.Major}.{v.Minor}.{v.Build})";
            }
        }
        #endregion Properties

        #region Events
        private void MiOpenSpoilerFolder_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SpoilerPath))
            {
                MessageBox.Show(this, "Spoiler path is empty. Please enter a valid directory path on the General tab.", "Spoilers Directory Error", MessageBoxButton.OK);
            }
            else if (Directory.Exists(SpoilerPath))
            {
                System.Diagnostics.Process.Start(SpoilerPath);
            }
            else if (MessageBox.Show(this,
                        $"Spoilers directory \"{SpoilerPath}\" does not exist. Would you like to create this directory?",
                        "Spoilers Directory Missing", MessageBoxButton.YesNo, MessageBoxImage.Question
                        ) == MessageBoxResult.Yes)
            {
                Directory.CreateDirectory(SpoilerPath);
                System.Diagnostics.Process.Start(SpoilerPath);
            }
        }

        private void MiHyperlink_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start((sender as FrameworkElement).Tag.ToString());
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsRandomizeViewBusy) e.Cancel = true;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            var file = new SettingsFile()
            {
                CreateSpoilers = miCreateSpoilers.IsChecked,
                Kotor1Path  = Kotor1Path,
                Kotor2Path  = Kotor2Path,
                PresetPath  = PresetPath,
                SpoilerPath = SpoilerPath,
            };
            file.WriteFile(Path.Combine(Environment.CurrentDirectory, SETTINGS_FILENAME));
        }
        #endregion Events

        #region Commands
        private void NewCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !IsRandomizeViewBusy;
        }

        private void NewCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show(this, "Are you sure you wish to clear the current settings? This cannot be undone.", "Clear Settings?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                K1Randomizer.ResetSettingsToDefault();
        }

        private void OpenCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !IsRandomizeViewBusy;
        }

        private void OpenCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (!Directory.Exists(PresetPath)) Directory.CreateDirectory(PresetPath);
            var dialog = new OpenFileDialog
            {
                Filter = "XML Kotor Rando Preset (*.xkrp)|*.xkrp|Kotor Rando Preset (*.krp)|*.krp|All files (*.*)|*.*",
                InitialDirectory = PresetPath,
            };
            if (dialog.ShowDialog() == true) K1Randomizer.Load(dialog.FileName);
        }

        private void SaveCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !IsRandomizeViewBusy;
        }

        private void SaveCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (!Directory.Exists(PresetPath)) Directory.CreateDirectory(PresetPath);
            var dialog = new SaveFileDialog
            {
                Filter = "XML Kotor Rando Preset (*.xkrp)|*.xkrp",
                InitialDirectory = PresetPath,
            };
            if (dialog.ShowDialog() == true) K1Randomizer.Save(dialog.FileName);
        }

        private void ExitCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !IsRandomizeViewBusy;
        }

        private void ExitCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }
        #endregion Commands
    }

    public static class CustomCommands
    {
        public static readonly RoutedUICommand Exit = new RoutedUICommand
            (
                "Exit",
                "Exit",
                typeof(CustomCommands),
                new InputGestureCollection()
                {
                    new KeyGesture(Key.F4, ModifierKeys.Alt)
                }
            );
    }
}
