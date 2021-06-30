using kotor_Randomizer_2;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for TextView.xaml
    /// </summary>
    public partial class TextView : UserControl
    {
        #region Constructor
        public TextView()
        {
            InitializeComponent();
        }
        #endregion

        #region Dependency Properties
        public static readonly DependencyProperty RandomizeDialogueEntriesProperty = DependencyProperty.Register("RandomizeDialogueEntries", typeof(bool), typeof(TextView), new PropertyMetadata(HandleTextSettingsChanged));
        public static readonly DependencyProperty RandomizeDialogueRepliesProperty = DependencyProperty.Register("RandomizeDialogueReplies", typeof(bool), typeof(TextView), new PropertyMetadata(HandleTextSettingsChanged));
        public static readonly DependencyProperty MatchEntrySoundsProperty         = DependencyProperty.Register("MatchEntrySounds",         typeof(bool), typeof(TextView), new PropertyMetadata(HandleTextSettingsChanged));
        public static readonly DependencyProperty RandomizeAdditionalTextProperty  = DependencyProperty.Register("RandomizeAdditionalText",  typeof(bool), typeof(TextView), new PropertyMetadata(HandleTextSettingsChanged));
        public static readonly DependencyProperty MatchSimilarStringLengthProperty = DependencyProperty.Register("MatchSimilarStringLength", typeof(bool), typeof(TextView), new PropertyMetadata(HandleTextSettingsChanged));

        public static readonly DependencyProperty TextSettingsValueProperty = DependencyProperty.Register("TextSettingsValue", typeof(TextSettings), typeof(TextView), new PropertyMetadata(TextSettings.Default));
        #endregion

        #region Public Properties
        public bool RandomizeDialogueEntries
        {
            get { return (bool)GetValue(RandomizeDialogueEntriesProperty); }
            set { SetValue(RandomizeDialogueEntriesProperty, value); }
        }

        public bool RandomizeDialogueReplies
        {
            get { return (bool)GetValue(RandomizeDialogueRepliesProperty); }
            set { SetValue(RandomizeDialogueRepliesProperty, value); }
        }

        public bool MatchEntrySounds
        {
            get { return (bool)GetValue(MatchEntrySoundsProperty); }
            set { SetValue(MatchEntrySoundsProperty, value); }
        }

        public bool RandomizeAdditionalText
        {
            get { return (bool)GetValue(RandomizeAdditionalTextProperty); }
            set { SetValue(RandomizeAdditionalTextProperty, value); }
        }

        public bool MatchSimilarStringLength
        {
            get { return (bool)GetValue(MatchSimilarStringLengthProperty); }
            set { SetValue(MatchSimilarStringLengthProperty, value); }
        }

        public TextSettings TextSettingsValue
        {
            get { return (TextSettings)GetValue(TextSettingsValueProperty); }
            set { SetValue(TextSettingsValueProperty, value); }
        }
        #endregion

        #region Events
        private static void HandleTextSettingsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var view = d as TextView;
            var settings = TextSettings.Default;

            if (view.RandomizeDialogueEntries)  settings |= TextSettings.RandoDialogEntries;
            if (view.RandomizeDialogueReplies)  settings |= TextSettings.RandoDialogReplies;
            if (view.MatchEntrySounds        )  settings |= TextSettings.MatchEntrySoundsWText;
            if (view.RandomizeAdditionalText )  settings |= TextSettings.RandoFullTLK;
            if (view.MatchSimilarStringLength)  settings |= TextSettings.MatchSimLengthStrings;

            view.TextSettingsValue = settings;
        }
        #endregion
    }
}
