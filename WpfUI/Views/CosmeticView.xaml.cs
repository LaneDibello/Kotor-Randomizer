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
        private List<RandomizationLevelUserControl> TextureControls;
        #endregion

        #region Constructor
        public CosmeticView()
        {
            InitializeComponent();
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
        public static readonly DependencyProperty TexturePackProperty = DependencyProperty.Register("TexturePack", typeof(TexturePack), typeof(CosmeticView), new PropertyMetadata(TexturePack.LowQuality, HandleTexturePackChanged));
        #endregion

        #region Properties
        public TexturePack TexturePack
        {
            get { return (TexturePack)GetValue(TexturePackProperty); }
            set { SetValue(TexturePackProperty, value); }
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
