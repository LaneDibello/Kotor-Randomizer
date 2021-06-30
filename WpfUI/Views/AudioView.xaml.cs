using kotor_Randomizer_2;
using Randomizer_WPF.UserControls;
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
    /// Interaction logic for AudioView.xaml
    /// </summary>
    public partial class AudioView : UserControl
    {
        #region Members
        private List<RandomizationLevelUserControl> MusicSoundControls;
        #endregion

        #region Constructor
        public AudioView()
        {
            InitializeComponent();
            MusicSoundControls = new List<RandomizationLevelUserControl>()
            {
                rlucAmbientNoise,
                rlucAreaMusic,
                rlucBattleMusic,
                rlucCutsceneNoise,
                //rlucNpcSounds,    // Not yet implemented.
                rlucPartySounds,
            };
        }
        #endregion

        #region Dependency Properties
        public static readonly DependencyProperty AreaMusicProperty = DependencyProperty.Register("AreaMusic", typeof(RandomizationLevel), typeof(AudioView));
        public static readonly DependencyProperty BattleMusicProperty = DependencyProperty.Register("BattleMusic", typeof(RandomizationLevel), typeof(AudioView));
        public static readonly DependencyProperty AmbientMusicProperty = DependencyProperty.Register("AmbientMusic", typeof(RandomizationLevel), typeof(AudioView));
        public static readonly DependencyProperty CutsceneNoiseProperty = DependencyProperty.Register("CutsceneNoise", typeof(RandomizationLevel), typeof(AudioView));
        public static readonly DependencyProperty NpcSoundsProperty = DependencyProperty.Register("NpcSounds", typeof(RandomizationLevel), typeof(AudioView));
        public static readonly DependencyProperty PartySoundsProperty = DependencyProperty.Register("PartySounds", typeof(RandomizationLevel), typeof(AudioView));
        public static readonly DependencyProperty OverwriteDmcaMusicProperty = DependencyProperty.Register("OverwriteDmcaMusic", typeof(bool), typeof(AudioView), new PropertyMetadata(false));
        public static readonly DependencyProperty MixKotorGameMusicProperty = DependencyProperty.Register("MixKotorGameMusic", typeof(bool), typeof(AudioView), new PropertyMetadata(false));
        public static readonly DependencyProperty MixNpcAndPartySoundsProperty = DependencyProperty.Register("MixNpcAndPartySounds", typeof(bool), typeof(AudioView), new PropertyMetadata(false));
        #endregion

        #region Public Properties
        public RandomizationLevel AreaMusic
        {
            get { return (RandomizationLevel)GetValue(AreaMusicProperty); }
            set { SetValue(AreaMusicProperty, value); }
        }

        public RandomizationLevel BattleMusic
        {
            get { return (RandomizationLevel)GetValue(BattleMusicProperty); }
            set { SetValue(BattleMusicProperty, value); }
        }

        public RandomizationLevel AmbientMusic
        {
            get { return (RandomizationLevel)GetValue(AmbientMusicProperty); }
            set { SetValue(AmbientMusicProperty, value); }
        }

        public RandomizationLevel CutsceneNoise
        {
            get { return (RandomizationLevel)GetValue(CutsceneNoiseProperty); }
            set { SetValue(CutsceneNoiseProperty, value); }
        }

        public RandomizationLevel NpcSounds
        {
            get { return (RandomizationLevel)GetValue(NpcSoundsProperty); }
            set { SetValue(NpcSoundsProperty, value); }
        }

        public RandomizationLevel PartySounds
        {
            get { return (RandomizationLevel)GetValue(PartySoundsProperty); }
            set { SetValue(PartySoundsProperty, value); }
        }

        public bool OverwriteDmcaMusic
        {
            get { return (bool)GetValue(OverwriteDmcaMusicProperty); }
            set { SetValue(OverwriteDmcaMusicProperty, value); }
        }

        public bool MixKotorGameMusic
        {
            get { return (bool)GetValue(MixKotorGameMusicProperty); }
            set { SetValue(MixKotorGameMusicProperty, value); }
        }

        public bool MixNpcAndPartySounds
        {
            get { return (bool)GetValue(MixNpcAndPartySoundsProperty); }
            set { SetValue(MixNpcAndPartySoundsProperty, value); }
        }
        #endregion

        #region Events
        private void BtnToggleAll_Click(object sender, RoutedEventArgs e)
        {
            bool CheckAllBoxes = MusicSoundControls.Any(rluc => !rluc.IsChecked);
            foreach (var item in MusicSoundControls)
            {
                item.IsChecked = CheckAllBoxes;
            }
        }

        private void BtnType_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in MusicSoundControls)
            {
                item.SelectedLevel = RandomizationLevel.Type;
            }
        }

        private void BtnMax_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in MusicSoundControls)
            {
                item.SelectedLevel = RandomizationLevel.Max;
            }
        }
        #endregion
    }
}
