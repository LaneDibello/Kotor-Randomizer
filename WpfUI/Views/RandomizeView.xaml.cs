using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for RandomizeView.xaml
    /// </summary>
    public partial class RandomizeView : UserControl
    {
        #region Members
        private static readonly Regex _decimal = new Regex("[^0-9]+");
        private static readonly Regex _hexidecimal = new Regex("[^0-9A-Fa-f]+");
        private const uint MAX_SEED = 0x7FFFFFFF;   // 2147483647

        private BackgroundWorker bwDoRando = new BackgroundWorker();
        private BackgroundWorker bwUnRando = new BackgroundWorker();

        private bool initialized = false;
        private bool isRandomized = false;
        #endregion

        #region Constructors
        public RandomizeView()
        {
            InitializeComponent();
            initialized = true;

            tbLog.Clear();
            CurrentState = "Ready";

            // Generate a new random seed.
            tbSeed.Tag = new Random().Next();
            tbSeed.Text = tbSeed.Tag.ToString();

            bwDoRando.WorkerReportsProgress = true;
            bwDoRando.ProgressChanged += Worker_ProgressChanged;
            bwDoRando.RunWorkerCompleted += Worker_RunWorkerCompleted;

            bwUnRando.WorkerReportsProgress = true;
            bwUnRando.ProgressChanged += Worker_ProgressChanged;
            bwUnRando.RunWorkerCompleted += Worker_RunWorkerCompleted;
        }
        #endregion

        #region Dependency Properties
        public static readonly DependencyProperty CreateSpoilersProperty  = DependencyProperty.Register("CreateSpoilers",  typeof(bool),   typeof(RandomizeView));
        public static readonly DependencyProperty CurrentProgressProperty = DependencyProperty.Register("CurrentProgress", typeof(double), typeof(RandomizeView));
        public static readonly DependencyProperty CurrentStateProperty    = DependencyProperty.Register("CurrentState",    typeof(string), typeof(RandomizeView));
        public static readonly DependencyProperty IsBusyProperty          = DependencyProperty.Register("IsBusy",          typeof(bool),   typeof(RandomizeView));
        public static readonly DependencyProperty GamePathProperty        = DependencyProperty.Register("GamePath",        typeof(string), typeof(RandomizeView), new PropertyMetadata("", HandleGamePathChanged));
        public static readonly DependencyProperty SpoilerPathProperty     = DependencyProperty.Register("SpoilerPath",     typeof(string), typeof(RandomizeView));
        #endregion

        #region Public Properties
        public bool CreateSpoilers
        {
            get { return (bool)GetValue(CreateSpoilersProperty); }
            set { SetValue(CreateSpoilersProperty, value); }
        }

        public double CurrentProgress
        {
            get { return (double)GetValue(CurrentProgressProperty); }
            set { SetValue(CurrentProgressProperty, value); }
        }

        public string CurrentState
        {
            get { return (string)GetValue(CurrentStateProperty); }
            set { SetValue(CurrentStateProperty, value); }
        }

        public bool IsBusy
        {
            get { return (bool)GetValue(IsBusyProperty); }
            set { SetValue(IsBusyProperty, value); }
        }

        public string GamePath
        {
            get { return (string)GetValue(GamePathProperty); }
            set { SetValue(GamePathProperty, value); }
        }

        public string SpoilerPath
        {
            get { return (string)GetValue(SpoilerPathProperty); }
            set { SetValue(SpoilerPathProperty, value); }
        }
        #endregion

        #region Events
        private void BtnNewSeed_Click(object sender, RoutedEventArgs e)
        {
            var newSeed = new Random().Next();
            tbSeed.Tag = newSeed;
            tbSeed.Text = newSeed.ToString();
            tbSeedHex.Text = newSeed.ToString("X");
        }

        private void BtnRandomize_Click(object sender, RoutedEventArgs e)
        {
            // Don't perform any actions if something is still busy.
            if (bwUnRando.IsBusy || bwDoRando.IsBusy)
            {
                // Note: This is where cancellation could be implemented.
                WriteLineToLog($"Unable to start process while {(bwUnRando.IsBusy ? "unrandomization" : "randomization")} is in progress.");
                return;
            }

            // Check for randomization state mismatch.
            var randomizedFileExists = System.IO.File.Exists(System.IO.Path.Combine(GamePath, kotor_Randomizer_2.KPaths.RANDOMIZED_LOG_FILENAME));
            if (isRandomized != randomizedFileExists)
            {
                WriteLineToLog($"Game path randomization state changed unexpectedly. It is currently {(randomizedFileExists ? "randomized" : "unrandomized")}.");
                isRandomized = randomizedFileExists;
                SetReady();
                return;
            }

            // If game is randomized, use BwUnRando.
            if (isRandomized)
            {
                WriteLineToLog("Starting unrandomization!");
                CurrentProgress = 0;
                CurrentState = $"Unrandomizing ...";

                SetBusy("Unrandomization in Progress ...");

                bwUnRando.RunWorkerAsync(new kotor_Randomizer_2.Models.RandoArgs()
                {
                    Seed = -1,
                    GamePath = GamePath,
                    SpoilersPath = null,
                });
            }
            // If game is not randomized, use BwDoRando.
            else
            {
                CurrentProgress = 0;

                WriteLineToLog("Starting randomization!");
                CurrentState = $"Randomizing ...";

                SetBusy("Randomization in Progress ...");

                // If creating spoilers, make sure the directory exists.
                if (CreateSpoilers && !string.IsNullOrWhiteSpace(SpoilerPath) && !System.IO.Directory.Exists(SpoilerPath))
                {
                    System.IO.Directory.CreateDirectory(SpoilerPath);
                }

                bwDoRando.RunWorkerAsync(new kotor_Randomizer_2.Models.RandoArgs()
                {
                    Seed = (int)tbSeed.Tag,
                    GamePath = GamePath,
                    SpoilersPath = CreateSpoilers ? SpoilerPath : null,  // Null if spoilers shouldn't be created.
                });
            }
        }

        private static void HandleGamePathChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var thisView = d as RandomizeView;
            var newValue = e.NewValue?.ToString() ?? null;

            if (string.IsNullOrWhiteSpace(newValue))
            {
                // Invalid value: game path not provided.
                thisView.btnRandomize.IsEnabled = false;
                thisView.btnRandomize.Content = "Game path not set in General";
                return;
            }
            else if (!System.IO.Directory.Exists(newValue))
            {
                // Invalid value: game path does not exist.
                thisView.btnRandomize.IsEnabled = false;
                thisView.btnRandomize.Content = "Game path does not exist (See General)";
                return;
            }

            // Update isRandomized whenever the game path is changed.
            thisView.isRandomized = System.IO.File.Exists(System.IO.Path.Combine(newValue, kotor_Randomizer_2.KPaths.RANDOMIZED_LOG_FILENAME));

            // If not busy, update the randomization button to reflect the current randomization status.
            if (!thisView.IsBusy)
            {
                thisView.btnRandomize.IsEnabled = true;
                thisView.btnRandomize.Content = thisView.isRandomized
                    ? "Unrandomize Game"    // If randomized, change the button to read "Unrandomize"
                    : "Randomize Game";     // If not randomized, change the button to read "Randomize"
            }
        }

        private void TbSeed_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text, _decimal);
        }

        private void TbSeed_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (initialized)
            {
                initialized = false;
                uint seed = 0;

                try
                {
                    if (!string.IsNullOrWhiteSpace(tbSeed.Text))
                        seed = uint.Parse(tbSeed.Text);
                }
                catch (OverflowException)
                {
                    seed = MAX_SEED + 1;
                }

                if (seed > MAX_SEED)
                {
                    seed = MAX_SEED;
                    tbSeed.Text = seed.ToString();
                }

                tbSeed.Tag = (int)seed;
                tbSeedHex.Text = seed.ToString("X");
                initialized = true;
            }
        }

        private void TbSeedHex_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text, _hexidecimal);
        }

        private void TbSeedHex_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (initialized)
            {
                initialized = false;
                uint seed = 0;

                try
                {
                    if (!string.IsNullOrWhiteSpace(tbSeedHex.Text))
                        seed = uint.Parse(tbSeedHex.Text, System.Globalization.NumberStyles.HexNumber);
                }
                catch (OverflowException)
                {
                    seed = MAX_SEED + 1;
                }

                if (seed > MAX_SEED)
                {
                    seed = MAX_SEED;
                    tbSeedHex.Text = seed.ToString("X");
                }

                tbSeed.Tag = (int)seed;
                tbSeed.Text = seed.ToString();
                initialized = true;
            }
        }

        private void View_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            // Subscribe and unsubscribe from DataContext BackgroundWorker events.
            if (e.OldValue is kotor_Randomizer_2.Models.Kotor1Randomizer dcOld)
            {
                bwDoRando.DoWork -= dcOld.Randomizer_DoWork;
                bwUnRando.DoWork -= dcOld.Unrandomize;
            }
            if (e.NewValue is kotor_Randomizer_2.Models.Kotor1Randomizer dcNew)
            {
                bwDoRando.DoWork += dcNew.Randomizer_DoWork;
                bwUnRando.DoWork += dcNew.Unrandomize;
            }
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState is kotor_Randomizer_2.Models.RandoProgress progress)
            {
                CurrentProgress = progress.PercentComplete;
                if (!string.IsNullOrWhiteSpace(progress.Log))
                    WriteLineToLog(progress.Log);

                if (progress.Status == null)
                    progress.Status = string.Empty;

                string statusFormat;
                switch (progress.State)
                {
                    case kotor_Randomizer_2.Models.BusyState.Randomizing:
                        statusFormat = "Randomizing ... [{0:F1}%] {1}";
                        break;
                    case kotor_Randomizer_2.Models.BusyState.Spoiling:
                        statusFormat = "Spoiling ... [{0:F1}%] {1}";
                        break;
                    case kotor_Randomizer_2.Models.BusyState.Unrandomizing:
                        statusFormat = "Unrandomizing ... [{0:F1}%] {1}";
                        break;
                    case kotor_Randomizer_2.Models.BusyState.BackingUp:
                        statusFormat = "Backing up ... [{0:F1}%] {1}";
                        break;
                    case kotor_Randomizer_2.Models.BusyState.Unknown:
                    default:
                        statusFormat = "Processing ... [{0:F1}%] {1}";
                        break;
                }
                CurrentState = string.Format(statusFormat, progress.PercentComplete, progress.Status);
            }
            else
            {
                CurrentProgress = e.ProgressPercentage;
                CurrentState = $"Processing ... [{e.ProgressPercentage}.0%]";
            }
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var isRandoWorker = sender == bwDoRando;

            // Was the process cancelled? (Note: This isn't currently allowed for the BWs.)
            if (e.Cancelled)
            {
                var msg = isRandoWorker ? "Randomization cancelled." : "Unrandomization cancelled.";
                WriteLineToLog(msg);
                CurrentState = msg;
            }

            // Was there an error?
            else if (e.Error != null)
            {
                WriteLineToLog($"Error during {(isRandoWorker ? "randomization" : "unrandomization")}: {e.Error}");
                CurrentState = $"Error encountered during {(isRandoWorker ? "randomization" : "unrandomization")}. See log for details...";
            }

            // Randomization complete. What was the final result?
            else
            {
                var msg = isRandoWorker ? "Randomization successful!" : "Unrandomization successful!";
                WriteLineToLog(msg);
                CurrentState = msg;
            }

            // Standard final steps.
            isRandomized = System.IO.File.Exists(System.IO.Path.Combine(GamePath, kotor_Randomizer_2.KPaths.RANDOMIZED_LOG_FILENAME));
            SetReady();
            WriteLineToLog();
        }
        #endregion

        #region Private Methods
        private static bool IsTextAllowed(string text, Regex regex)
        {
            return !regex.IsMatch(text);
        }

        private void SetBusy(string buttonContent)
        {
            wpSettings.IsEnabled = false;
            btnRandomize.IsEnabled = false;
            btnRandomize.Content = buttonContent;
            IsBusy = true;
        }

        private void SetReady(string buttonContent = "")
        {
            if (string.IsNullOrWhiteSpace(buttonContent))
                buttonContent = isRandomized ? "Unrandomize!" : "Randomize!";

            wpSettings.IsEnabled = true;
            btnRandomize.IsEnabled = true;
            btnRandomize.Content = buttonContent;
            IsBusy = false;
        }

        public void WriteToLog(string message)
        {
            tbLog.AppendText($"{message}");
            tbLog.ScrollToEnd();
        }

        public void WriteLineToLog(string message = "")
        {
            WriteToLog($"{message}{Environment.NewLine}");
        }
        #endregion
    }
}
