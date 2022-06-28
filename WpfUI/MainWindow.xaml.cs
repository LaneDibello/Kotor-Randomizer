using kotor_Randomizer_2.Models;
using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Randomizer_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constants
        public const string SETTINGS_FILENAME = "settings.xml";
        public const double DEFAULT_HEIGHT = 620.0;
        public const double DEFAULT_WIDTH  = 820.0;
        #endregion Constants

        #region Constructors
        public MainWindow(string startupFilePath = "")
        {
            InitializeComponent();

            // Create objects and pull settings from file.
            K1Randomizer = new Kotor1Randomizer();
            K2Randomizer = new Kotor2Randomizer();

            CurrentSettings = GetSettingsFile();
            miCreateSpoilers.IsChecked = CurrentSettings.CreateSpoilers;
            Kotor1Path = CurrentSettings.Kotor1Path;
            Kotor2Path = CurrentSettings.Kotor2Path;
            PresetPath = CurrentSettings.PresetPath;
            SpoilerPath = CurrentSettings.SpoilerPath;
            SelectedFontIndex = CurrentSettings.FontSizeIndex;
            CurrentHeight = CurrentSettings.Height;
            CurrentWidth = CurrentSettings.Width;

            IsKotor2Selected = CurrentSettings.IsKotor2Selected;
            if (IsKotor2Selected == false)
                DataContext = K1Randomizer; // Set window data context.

            // If startup file path given, load it -- primarily used for debugging.
            if (!string.IsNullOrEmpty(startupFilePath)) LoadPresetFile(startupFilePath);
            else GetLastUsedPreset();       // Always load the last settings.

            WriteLineToLog($"{Environment.NewLine}Once you are satisfied, click the button below to randomize your game.{Environment.NewLine}");
        }
        #endregion Constructors

        #region Dependency Properties
        public static readonly DependencyProperty Kotor1PathProperty  = DependencyProperty.Register("Kotor1Path",  typeof(string), typeof(MainWindow));
        public static readonly DependencyProperty Kotor2PathProperty  = DependencyProperty.Register("Kotor2Path",  typeof(string), typeof(MainWindow));
        public static readonly DependencyProperty PresetPathProperty  = DependencyProperty.Register("PresetPath",  typeof(string), typeof(MainWindow));
        public static readonly DependencyProperty SpoilerPathProperty = DependencyProperty.Register("SpoilerPath", typeof(string), typeof(MainWindow));
        public static readonly DependencyProperty SelectedFontIndexProperty = DependencyProperty.Register("SelectedFontIndex", typeof(int), typeof(MainWindow), new PropertyMetadata(1));
        public static readonly DependencyProperty CurrentHeightProperty     = DependencyProperty.Register("CurrentHeight",  typeof(double), typeof(MainWindow), new PropertyMetadata(DEFAULT_HEIGHT));
        public static readonly DependencyProperty CurrentWidthProperty      = DependencyProperty.Register("CurrentWidth",   typeof(double), typeof(MainWindow), new PropertyMetadata(DEFAULT_WIDTH));
        #endregion Dependency Properties

        #region Properties
        public Kotor1Randomizer K1Randomizer { get; set; }
        public Kotor2Randomizer K2Randomizer { get; set; }

        public SettingsFile CurrentSettings { get; set; }



        public bool IsKotor2Selected
        {
            get => (bool)GetValue(IsKotor2SelectedProperty);
            set => SetValue(IsKotor2SelectedProperty, value);
        }

        public static readonly DependencyProperty IsKotor2SelectedProperty = DependencyProperty.Register("IsKotor2Selected", typeof(bool), typeof(MainWindow), new PropertyMetadata(OnGameChanged));

        private static void OnGameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var win = d as MainWindow;
            if ((bool)e.NewValue)
            {
                // Kotor 2 is now the selected game.
                win.DataContext = win.K2Randomizer;
            }
            else
            {
                // Kotor 1 is now the selected game.
                win.DataContext = win.K1Randomizer;
            }
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

        public int SelectedFontIndex
        {
            get => (int)GetValue(SelectedFontIndexProperty);
            set => SetValue(SelectedFontIndexProperty, value);
        }

        public double CurrentHeight
        {
            get => (double)GetValue(CurrentHeightProperty);
            set => SetValue(CurrentHeightProperty, value);
        }

        public double CurrentWidth
        {
            get => (double)GetValue(CurrentWidthProperty);
            set => SetValue(CurrentWidthProperty, value);
        }

        public bool IsRandomizeViewBusy => (RandomizeView != null) && RandomizeView.IsBusy;

        public string WindowTitle
        {
            get
            {
                var v = System.Reflection.Assembly.GetAssembly(typeof(Kotor1Randomizer)).GetName().Version;
                return $"Kotor Randomizer (v{v.Major}.{v.Minor}.{v.Build})";
            }
        }
        #endregion Properties

        #region Events
        private void MiOpenSpoilerFolder_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SpoilerPath))
            {
                SpoilerPath = SettingsFile.DEFAULT_SPOILER_PATH;
            }
            if (Directory.Exists(SpoilerPath))
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
            WriteToLog("Saving current settings ... ");
            if (RandomizeView != null) RandomizeView.CurrentState = "Saving ...";

            if (string.IsNullOrWhiteSpace(PresetPath))
                PresetPath = SettingsFile.DEFAULT_PRESET_PATH;

            if (string.IsNullOrWhiteSpace(SpoilerPath))
                SpoilerPath = SettingsFile.DEFAULT_SPOILER_PATH;

            var file = new SettingsFile()
            {
                CreateSpoilers   = miCreateSpoilers.IsChecked,
                IsKotor2Selected = IsKotor2Selected,
                Kotor1Path       = Kotor1Path,
                Kotor2Path       = Kotor2Path,
                PresetPath       = PresetPath,
                SpoilerPath      = SpoilerPath,
                FontSizeIndex    = SelectedFontIndex,
                Height           = CurrentHeight,
                Width            = CurrentWidth,
            };
            file.WriteFile(Path.Combine(Environment.CurrentDirectory, SETTINGS_FILENAME));

            K1Randomizer.Save(Path.Combine(Environment.CurrentDirectory, "last.xkrp"));
            WriteToLog("done.");
        }

        private void Window_DragOver(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.None;

            // If the data is a file drop, verify that it is in a usable format.
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var files = (string[])e.Data.GetData(DataFormats.FileDrop);
                var info = new FileInfo(files.First());
                if (info.Extension.ToLower() == ".xkrp" || info.Extension.ToLower() == ".krp")
                {
                    e.Effects = DragDropEffects.Copy | DragDropEffects.Move;
                }
            }

            e.Handled = true;
        }

        private void Window_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var files = (string[])e.Data.GetData(DataFormats.FileDrop);
                var path = files.First();
                if (MessageBox.Show(this, $"Do you wish to open this file?{Environment.NewLine}\"{path}\"{Environment.NewLine}Current randomization settings will be lost.",
                    "Load Dragged File?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    WriteLineToLog($"Opening settings file: \"{path}\"");
                    K1Randomizer.Load(path);
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CurrentHeight = CurrentSettings.Height;
            CurrentWidth = CurrentSettings.Width;
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
            {
                WriteLineToLog("Clearing randomization settings.");
                K1Randomizer.ResetSettingsToDefault();
            }
        }

        private void OpenCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !IsRandomizeViewBusy;
        }

        private void OpenCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // Reset to default if path is empty.
            if (string.IsNullOrWhiteSpace(PresetPath))
                PresetPath = SettingsFile.DEFAULT_PRESET_PATH;

            // Create the directory if it doesn't exist.
            if (!Directory.Exists(PresetPath))
                Directory.CreateDirectory(PresetPath);

            // Set dialog options.
            var dialog = new OpenFileDialog
            {
                Filter = "XML Kotor Rando Preset (*.xkrp)|*.xkrp|Kotor Rando Preset (*.krp)|*.krp",
                InitialDirectory = PresetPath,
            };

            // Show open file dialog.
            if (dialog.ShowDialog() == true)
            {
                WriteLineToLog($"Opening settings file: \"{dialog.FileName}\"");
                K1Randomizer.Load(dialog.FileName);
            }
        }

        private void SaveCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !IsRandomizeViewBusy;
        }

        private void SaveCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // Reset to default if path is empty.
            if (string.IsNullOrWhiteSpace(PresetPath))
                PresetPath = SettingsFile.DEFAULT_PRESET_PATH;

            // Create the directory if it doesn't exist.
            if (!Directory.Exists(PresetPath))
                Directory.CreateDirectory(PresetPath);

            // Set dialog options.
            var dialog = new SaveFileDialog
            {
                Filter = "XML Kotor Rando Preset (*.xkrp)|*.xkrp",
                InitialDirectory = PresetPath,
            };

            // Show open file dialog.
            if (dialog.ShowDialog() == true)
            {
                K1Randomizer.Save(dialog.FileName);
                WriteLineToLog($"Settings saved to file: \"{dialog.FileName}\"");
            }
        }

        private void ExitCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !IsRandomizeViewBusy;
        }

        private void ExitCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }
        #endregion Commands

        #region Private Methods
        private SettingsFile GetSettingsFile()
        {
            // Check for a settings file.
            var path = Path.Combine(Environment.CurrentDirectory, SETTINGS_FILENAME);
            SettingsFile file = null;

            if (File.Exists(path))
            {
                try
                {
                    WriteToLog("Reading settings file ... ");
                    file = new SettingsFile(path);
                    WriteLineToLog("done.");
                }
                catch (Exception)
                {
                    WriteLineToLog("error.");
                    WriteLineToLog("Deleting the bad file.");
                    File.Delete(path);  // Delete the bad file.
                    file = null;
                }
            }

            // If file doesn't exist, create one with default values.
            if (file == null)
            {
                WriteToLog("Creating settings file with default values ... ");
                file = new SettingsFile()
                {
                    CreateSpoilers = false,
                    Kotor1Path = @"",
                    Kotor2Path = @"",
                    PresetPath = Path.Combine(Environment.CurrentDirectory, "Presets"),
                    SpoilerPath = Path.Combine(Environment.CurrentDirectory, "Spoilers"),
                    FontSizeIndex = 1,
                    Height = DEFAULT_HEIGHT,
                    Width  = DEFAULT_WIDTH,
                };
                file.WriteFile(path);
                WriteLineToLog("done.");
            }

            if (file.Height == 0) file.Height = DEFAULT_HEIGHT;
            if (file.Width  == 0) file.Width  = DEFAULT_WIDTH;

            return file;
        }

        private void WriteToLog(string message)
        {
            RandomizeView?.WriteToLog(message);
        }

        private void WriteLineToLog(string message = "")
        {
            RandomizeView?.WriteLineToLog(message);
        }

        private void GetLastUsedPreset()
        {
            var lastPath = Path.Combine(Environment.CurrentDirectory, "last.xkrp");
            if (File.Exists(lastPath))
            {
                try
                {
                    // Write message to log about loading startup file.
                    WriteToLog("Reading last used preset ... ");

                    // Load the file that was requested.
                    K1Randomizer.Load(lastPath);

                    // Make a note that the file was loaded successfully.
                    WriteLineToLog("done.");
                }
                catch (Exception e)
                {
                    // Write message to log about failure.
                    WriteLineToLog("error.");
                    WriteLineToLog($"Failed to read last used preset: {e.Message}");
                    MessageBox.Show($"Failed to read last used preset: {e.Message}", "Read Error");
                }
            }
        }

        private void LoadPresetFile(string presetFilePath)
        {
            try
            {
                // Write message to log about loading startup file.
                WriteLineToLog($"Reading preset from file: {presetFilePath}");

                // Load the file that was requested.
                K1Randomizer.Load(presetFilePath);

                // Make a note that the file was loaded successfully.
                WriteLineToLog("Preset loaded sucessfully.");
            }
            catch (Exception e)
            {
                // Write message to log about failure.
                WriteLineToLog($"Failed to read preset from file: {e.Message}");
                MessageBox.Show($"Failed to read preset from file: {e.Message}", "Read Error");
            }
        }
        #endregion Private Methods
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
