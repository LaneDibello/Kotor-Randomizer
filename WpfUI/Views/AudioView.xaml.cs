using Randomizer_WPF.UserControls;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Randomizer_WPF.Views
{
    /// <summary>
    /// Interaction logic for AudioView.xaml
    /// </summary>
    public partial class AudioView : UserControl
    {
        #region Members
        //private List<RandomizationLevelUserControl> MusicSoundControls;
        #endregion

        #region Constructor
        public AudioView()
        {
            InitializeComponent();
            //MusicSoundControls = new List<RandomizationLevelUserControl>()
            //{
            //    rlucAmbientNoise,
            //    rlucAreaMusic,
            //    rlucBattleMusic,
            //    rlucCutsceneNoise,
            //    //rlucNpcSounds,    // Not yet implemented.
            //    rlucPartySounds,
            //};
        }
        #endregion

        #region Dependency Properties
        //public static readonly DependencyProperty AreaMusicProperty = DependencyProperty.Register("AreaMusic", typeof(RandomizationLevel), typeof(AudioView));
        //public static readonly DependencyProperty BattleMusicProperty = DependencyProperty.Register("BattleMusic", typeof(RandomizationLevel), typeof(AudioView));
        //public static readonly DependencyProperty AmbientMusicProperty = DependencyProperty.Register("AmbientMusic", typeof(RandomizationLevel), typeof(AudioView));
        //public static readonly DependencyProperty CutsceneNoiseProperty = DependencyProperty.Register("CutsceneNoise", typeof(RandomizationLevel), typeof(AudioView));
        //public static readonly DependencyProperty NpcSoundsProperty = DependencyProperty.Register("NpcSounds", typeof(RandomizationLevel), typeof(AudioView));
        //public static readonly DependencyProperty PartySoundsProperty = DependencyProperty.Register("PartySounds", typeof(RandomizationLevel), typeof(AudioView));
        public static readonly DependencyProperty OverwriteDmcaMusicProperty = DependencyProperty.Register("OverwriteDmcaMusic", typeof(bool), typeof(AudioView), new PropertyMetadata(false));
        public static readonly DependencyProperty MixKotorGameMusicProperty = DependencyProperty.Register("MixKotorGameMusic", typeof(bool), typeof(AudioView), new PropertyMetadata(false));
        public static readonly DependencyProperty MixNpcAndPartySoundsProperty = DependencyProperty.Register("MixNpcAndPartySounds", typeof(bool), typeof(AudioView), new PropertyMetadata(false));
        #endregion

        #region Methods
        private static List<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
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
        #endregion

        #region Public Properties
        //public RandomizationLevel AreaMusic
        //{
        //    get => (RandomizationLevel)GetValue(AreaMusicProperty);
        //    set => SetValue(AreaMusicProperty, value);
        //}

        //public RandomizationLevel BattleMusic
        //{
        //    get => (RandomizationLevel)GetValue(BattleMusicProperty);
        //    set => SetValue(BattleMusicProperty, value);
        //}

        //public RandomizationLevel AmbientMusic
        //{
        //    get => (RandomizationLevel)GetValue(AmbientMusicProperty);
        //    set => SetValue(AmbientMusicProperty, value);
        //}

        //public RandomizationLevel CutsceneNoise
        //{
        //    get => (RandomizationLevel)GetValue(CutsceneNoiseProperty);
        //    set => SetValue(CutsceneNoiseProperty, value);
        //}

        //public RandomizationLevel NpcSounds
        //{
        //    get => (RandomizationLevel)GetValue(NpcSoundsProperty);
        //    set => SetValue(NpcSoundsProperty, value);
        //}

        //public RandomizationLevel PartySounds
        //{
        //    get => (RandomizationLevel)GetValue(PartySoundsProperty);
        //    set => SetValue(PartySoundsProperty, value);
        //}

        public bool OverwriteDmcaMusic
        {
            get => (bool)GetValue(OverwriteDmcaMusicProperty);
            set => SetValue(OverwriteDmcaMusicProperty, value);
        }

        public bool MixKotorGameMusic
        {
            get => (bool)GetValue(MixKotorGameMusicProperty);
            set => SetValue(MixKotorGameMusicProperty, value);
        }

        public bool MixNpcAndPartySounds
        {
            get => (bool)GetValue(MixNpcAndPartySoundsProperty);
            set => SetValue(MixNpcAndPartySoundsProperty, value);
        }
        #endregion

        #region Events
        private void BtnToggleAll_Click(object sender, RoutedEventArgs e)
        {
            var rlucs = GetRandomizationLevelUserControls();
            var CheckAllBoxes = rlucs.Any(rluc => !rluc.IsActive);
            foreach (var item in rlucs)
            {
                item.IsActive = CheckAllBoxes;
            }
            //bool CheckAllBoxes = MusicSoundControls.Any(rluc => !rluc.IsChecked);
            //foreach (var item in MusicSoundControls)
            //{
            //    item.IsChecked = CheckAllBoxes;
            //}
        }

        private void BtnType_Click(object sender, RoutedEventArgs e)
        {
            var rlucs = GetRandomizationLevelUserControls();
            foreach (var item in rlucs)
            {
                if (item.TypeVisible) item.IsType = true;
            }
            //foreach (var item in MusicSoundControls)
            //{
            //    item.SelectedLevel = RandomizationLevel.Type;
            //}
        }

        private void BtnMax_Click(object sender, RoutedEventArgs e)
        {
            var rlucs = GetRandomizationLevelUserControls();
            foreach (var item in rlucs)
            {
                if (item.MaxVisible) item.IsMax = true;
            }
            //foreach (var item in MusicSoundControls)
            //{
            //    item.SelectedLevel = RandomizationLevel.Max;
            //}
        }
        #endregion
    }
}
