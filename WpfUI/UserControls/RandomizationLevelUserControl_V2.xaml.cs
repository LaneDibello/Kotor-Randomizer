using kotor_Randomizer_2;
using kotor_Randomizer_2.Extensions;
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

namespace Randomizer_WPF.UserControls
{
    /// <summary>
    /// Interaction logic for RandomizationLevelUserControl_V2.xaml
    /// </summary>
    public partial class RandomizationLevelUserControl_V2 : UserControl
    {
        #region Constructors
        public RandomizationLevelUserControl_V2()
        {
            InitializeComponent();
        }
        #endregion

        #region Dependency Properties
        public static readonly DependencyProperty LevelsBoxMinWidthProperty = DependencyProperty.Register(nameof(LevelsBoxMinWidth), typeof(double), typeof(RandomizationLevelUserControl_V2));
        #endregion

        #region Properties
        public double LevelsBoxMinWidth
        {
            get => (double)GetValue(LevelsBoxMinWidthProperty);
            set => SetValue(LevelsBoxMinWidthProperty, value);
        }

        public bool IsActive
        {
            get => cbIsActive.IsChecked ?? false;
            set => cbIsActive.IsChecked = value;
        }

        public bool IsSubtype
        {
            get => rbSubtype.IsChecked ?? false;
            set => rbSubtype.IsChecked = value;
        }

        public bool IsType
        {
            get => rbType.IsChecked ?? false;
            set => rbType.IsChecked = value;
        }

        public bool IsMax
        {
            get => rbMax.IsChecked ?? false;
            set => rbMax.IsChecked = value;
        }

        public bool SubtypeVisible => rbSubtype.Visibility == Visibility.Visible;

        public bool TypeVisible => rbType.Visibility == Visibility.Visible;

        public bool MaxVisible => rbMax.Visibility == Visibility.Visible;
        #endregion

        #region Event Methods
        public delegate void Control_RandomizationLevelChanged(object tag, RandomizationLevel oldValue, RandomizationLevel newValue);
        public event Control_RandomizationLevelChanged RandomizationLevelChanged;

        /// <summary>
        /// Used by RandoLevelFlagsToBoolConverter in xaml.
        /// </summary>
        private void RandoLevelFlagsChanged(RandoLevelFlags oldValue, RandoLevelFlags newValue)
        {
            var oldLevel = oldValue.ToRandomizationLevel();
            var newLevel = newValue.ToRandomizationLevel();
            if (oldLevel != newLevel) RandomizationLevelChanged?.Invoke(Tag, oldLevel, newLevel);
        }
        #endregion
    }
}
