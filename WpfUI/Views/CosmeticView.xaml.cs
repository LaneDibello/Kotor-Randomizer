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
    /// Interaction logic for CosmeticView.xaml
    /// </summary>
    public partial class CosmeticView : UserControl
    {
        #region Members
        private List<RandomizationLevelUserControl> AnimationControls;
        private List<RandomizationLevelUserControl> TextureControls;
        #endregion

        #region Constructor
        public CosmeticView()
        {
            InitializeComponent();
            AnimationControls = new List<RandomizationLevelUserControl>()
            {
                rlucAnimationAttack,
                rlucAnimationDamage,
                rlucAnimationForget,
                rlucAnimationLoop,
                rlucAnimationParry,
                rlucAnimationPause,
                rlucAnimationMove,
            };
            TextureControls = new List<RandomizationLevelUserControl>()
            {
                rlucCreatures,
                rlucCubeMaps,
                rlucEffects,
                rlucItems,
                rlucNPC,
                rlucOther,
                rlucParty,
                rlucPlaceables,
                rlucPlanetary,
                rlucPlayerBodies,
                rlucPlayerHeads,
                rlucStunt,
                rlucVehicles,
                rlucWeapons,
            };
        }
        #endregion

        #region Dependency Properties
        public static readonly DependencyProperty AnimationAttackProperty = DependencyProperty.Register("AnimationAttack", typeof(RandomizationLevel), typeof(CosmeticView));
        public static readonly DependencyProperty AnimationDamageProperty = DependencyProperty.Register("AnimationDamage", typeof(RandomizationLevel), typeof(CosmeticView));
        public static readonly DependencyProperty AnimationFireProperty   = DependencyProperty.Register("AnimationFire",   typeof(RandomizationLevel), typeof(CosmeticView));
        public static readonly DependencyProperty AnimationLoopProperty   = DependencyProperty.Register("AnimationLoop",   typeof(RandomizationLevel), typeof(CosmeticView));
        public static readonly DependencyProperty AnimationParryProperty  = DependencyProperty.Register("AnimationParry",  typeof(RandomizationLevel), typeof(CosmeticView));
        public static readonly DependencyProperty AnimationPauseProperty  = DependencyProperty.Register("AnimationPause",  typeof(RandomizationLevel), typeof(CosmeticView));
        public static readonly DependencyProperty AnimationMoveProperty   = DependencyProperty.Register("AnimationMove",   typeof(RandomizationLevel), typeof(CosmeticView));

        public static readonly DependencyProperty TexturePackProperty = DependencyProperty.Register("TexturePack", typeof(TexturePack), typeof(CosmeticView), new PropertyMetadata(TexturePack.LowQuality, HandleTexturePackChanged));

        public static readonly DependencyProperty TextureCubeMapsProperty     = DependencyProperty.Register("TextureCubeMaps",     typeof(RandomizationLevel), typeof(CosmeticView));
        public static readonly DependencyProperty TextureCreaturesProperty    = DependencyProperty.Register("TextureCreatures",    typeof(RandomizationLevel), typeof(CosmeticView));
        public static readonly DependencyProperty TextureEffectsProperty      = DependencyProperty.Register("TextureEffects",      typeof(RandomizationLevel), typeof(CosmeticView));
        public static readonly DependencyProperty TextureItemsProperty        = DependencyProperty.Register("TextureItems",        typeof(RandomizationLevel), typeof(CosmeticView));
        public static readonly DependencyProperty TexturePlanetaryProperty    = DependencyProperty.Register("TexturePlanetary",    typeof(RandomizationLevel), typeof(CosmeticView));
        public static readonly DependencyProperty TextureNPCProperty          = DependencyProperty.Register("TextureNPC",          typeof(RandomizationLevel), typeof(CosmeticView));
        public static readonly DependencyProperty TexturePlayerHeadsProperty  = DependencyProperty.Register("TexturePlayerHeads",  typeof(RandomizationLevel), typeof(CosmeticView));
        public static readonly DependencyProperty TexturePlayerBodiesProperty = DependencyProperty.Register("TexturePlayerBodies", typeof(RandomizationLevel), typeof(CosmeticView));
        public static readonly DependencyProperty TexturePlaceablesProperty   = DependencyProperty.Register("TexturePlaceables",   typeof(RandomizationLevel), typeof(CosmeticView));
        public static readonly DependencyProperty TexturePartyProperty        = DependencyProperty.Register("TextureParty",        typeof(RandomizationLevel), typeof(CosmeticView));
        public static readonly DependencyProperty TextureStuntProperty        = DependencyProperty.Register("TextureStunt",        typeof(RandomizationLevel), typeof(CosmeticView));
        public static readonly DependencyProperty TextureVehiclesProperty     = DependencyProperty.Register("TextureVehicles",     typeof(RandomizationLevel), typeof(CosmeticView));
        public static readonly DependencyProperty TextureWeaponsProperty      = DependencyProperty.Register("TextureWeapons",      typeof(RandomizationLevel), typeof(CosmeticView));
        public static readonly DependencyProperty TextureOtherProperty        = DependencyProperty.Register("TextureOther",        typeof(RandomizationLevel), typeof(CosmeticView));
        #endregion

        #region Properties

        public RandomizationLevel AnimationAttack
        {
            get { return (RandomizationLevel)GetValue(AnimationAttackProperty); }
            set { SetValue(AnimationAttackProperty, value); }
        }

        public RandomizationLevel AnimationDamage
        {
            get { return (RandomizationLevel)GetValue(AnimationDamageProperty); }
            set { SetValue(AnimationDamageProperty, value); }
        }

        public RandomizationLevel AnimationFire
        {
            get { return (RandomizationLevel)GetValue(AnimationFireProperty); }
            set { SetValue(AnimationFireProperty, value); }
        }

        public RandomizationLevel AnimationLoop
        {
            get { return (RandomizationLevel)GetValue(AnimationLoopProperty); }
            set { SetValue(AnimationLoopProperty, value); }
        }

        public RandomizationLevel AnimationParry
        {
            get { return (RandomizationLevel)GetValue(AnimationParryProperty); }
            set { SetValue(AnimationParryProperty, value); }
        }

        public RandomizationLevel AnimationPause
        {
            get { return (RandomizationLevel)GetValue(AnimationPauseProperty); }
            set { SetValue(AnimationPauseProperty, value); }
        }

        public RandomizationLevel AnimationMove
        {
            get { return (RandomizationLevel)GetValue(AnimationMoveProperty); }
            set { SetValue(AnimationMoveProperty, value); }
        }

        public TexturePack TexturePack
        {
            get { return (TexturePack)GetValue(TexturePackProperty); }
            set { SetValue(TexturePackProperty, value); }
        }

        public RandomizationLevel TextureCubeMaps
        {
            get { return (RandomizationLevel)GetValue(TextureCubeMapsProperty); }
            set { SetValue(TextureCubeMapsProperty, value); }
        }

        public RandomizationLevel TextureCreatures
        {
            get { return (RandomizationLevel)GetValue(TextureCreaturesProperty); }
            set { SetValue(TextureCreaturesProperty, value); }
        }

        public RandomizationLevel TextureEffects
        {
            get { return (RandomizationLevel)GetValue(TextureEffectsProperty); }
            set { SetValue(TextureEffectsProperty, value); }
        }

        public RandomizationLevel TextureItems
        {
            get { return (RandomizationLevel)GetValue(TextureItemsProperty); }
            set { SetValue(TextureItemsProperty, value); }
        }

        public RandomizationLevel TexturePlanetary
        {
            get { return (RandomizationLevel)GetValue(TexturePlanetaryProperty); }
            set { SetValue(TexturePlanetaryProperty, value); }
        }

        public RandomizationLevel TextureNPC
        {
            get { return (RandomizationLevel)GetValue(TextureNPCProperty); }
            set { SetValue(TextureNPCProperty, value); }
        }

        public RandomizationLevel TexturePlayerHeads
        {
            get { return (RandomizationLevel)GetValue(TexturePlayerHeadsProperty); }
            set { SetValue(TexturePlayerHeadsProperty, value); }
        }

        public RandomizationLevel TexturePlayerBodies
        {
            get { return (RandomizationLevel)GetValue(TexturePlayerBodiesProperty); }
            set { SetValue(TexturePlayerBodiesProperty, value); }
        }

        public RandomizationLevel TexturePlaceables
        {
            get { return (RandomizationLevel)GetValue(TexturePlaceablesProperty); }
            set { SetValue(TexturePlaceablesProperty, value); }
        }

        public RandomizationLevel TextureParty
        {
            get { return (RandomizationLevel)GetValue(TexturePartyProperty); }
            set { SetValue(TexturePartyProperty, value); }
        }

        public RandomizationLevel TextureStunt
        {
            get { return (RandomizationLevel)GetValue(TextureStuntProperty); }
            set { SetValue(TextureStuntProperty, value); }
        }

        public RandomizationLevel TextureVehicles
        {
            get { return (RandomizationLevel)GetValue(TextureVehiclesProperty); }
            set { SetValue(TextureVehiclesProperty, value); }
        }

        public RandomizationLevel TextureWeapons
        {
            get { return (RandomizationLevel)GetValue(TextureWeaponsProperty); }
            set { SetValue(TextureWeaponsProperty, value); }
        }

        public RandomizationLevel TextureOther
        {
            get { return (RandomizationLevel)GetValue(TextureOtherProperty); }
            set { SetValue(TextureOtherProperty, value); }
        }
        #endregion

        #region Events
        private static void HandleTexturePackChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var view = d as CosmeticView;
            switch ((TexturePack)e.NewValue)
            {
                case TexturePack.LowQuality:
                    view.rbLowQuality.IsChecked = true;
                    break;
                case TexturePack.MedQuality:
                    view.rbMedQuality.IsChecked = true;
                    break;
                case TexturePack.HighQuality:
                default:
                    view.rbHighQuality.IsChecked = true;
                    break;
            }
        }

        private void BtnToggleAll_Click(object sender, RoutedEventArgs e)
        {
            bool CheckAllBoxes = TextureControls.Any(rluc => !rluc.IsChecked);
            foreach (var item in TextureControls)
            {
                item.IsChecked = CheckAllBoxes;
            }
        }

        private void BtnType_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in TextureControls)
            {
                item.SelectedLevel = RandomizationLevel.Type;
            }
        }

        private void BtnMax_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in TextureControls)
            {
                item.SelectedLevel = RandomizationLevel.Max;
            }
        }

        private void BtnToggleAllAnimation_Click(object sender, RoutedEventArgs e)
        {
            bool CheckAllBoxes = AnimationControls.Any(rluc => !rluc.IsChecked);
            foreach (var item in AnimationControls)
            {
                item.IsChecked = CheckAllBoxes;
            }
        }

        private void BtnTypeAnimation_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in AnimationControls)
            {
                item.SelectedLevel = RandomizationLevel.Type;
            }
        }

        private void BtnMaxAnimation_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in AnimationControls)
            {
                item.SelectedLevel = RandomizationLevel.Max;
            }
        }

        private void RbHQ_Click(object sender, RoutedEventArgs e)
        {
            TexturePack = TexturePack.HighQuality;
            Console.WriteLine(thisView.DataContext);
        }

        private void RbMQ_Click(object sender, RoutedEventArgs e)
        {
            TexturePack = TexturePack.MedQuality;
        }

        private void RbLQ_Click(object sender, RoutedEventArgs e)
        {
            TexturePack = TexturePack.LowQuality;
        }

        private void View_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Console.WriteLine();
        }

        private void View_Loaded(object sender, RoutedEventArgs e)
        {

        }
        #endregion
    }
}
