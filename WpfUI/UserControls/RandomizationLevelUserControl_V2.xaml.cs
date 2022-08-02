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
        #region Fields

        //private bool IgnoreChanges;

        #endregion

        #region Constructors

        public RandomizationLevelUserControl_V2()
        {
            InitializeComponent();

            //var active = cbIsActive.IsChecked;
            //SelectedLevel = RandomizationLevel.Subtype;

            //if (active == false)
            //{
            //    SelectedLevel = RandomizationLevel.None;
            //}
        }

        #endregion

        #region Dependency Properties

        //public static readonly DependencyProperty SelectedLevelProperty = DependencyProperty.Register(nameof(SelectedLevel), typeof(RandomizationLevel), typeof(RandomizationLevelUserControl_V2)/*, new PropertyMetadata(new PropertyChangedCallback(OnSelectedLevelChanged))*/);
        public static readonly DependencyProperty LevelsBoxMinWidthProperty = DependencyProperty.Register(nameof(LevelsBoxMinWidth), typeof(double), typeof(RandomizationLevelUserControl_V2));

        #endregion

        #region Properties

        //public RandomizationLevel SelectedLevel
        //{
        //    get => (RandomizationLevel)GetValue(SelectedLevelProperty);
        //    set
        //    {
        //        // If the requested level is not visible, ignore the request.
        //        if (value == RandomizationLevel.Subtype && rbSubtype.Visibility != Visibility.Visible) return;
        //        if (value == RandomizationLevel.Type    && rbType.Visibility    != Visibility.Visible) return;
        //        if (value == RandomizationLevel.Max     && rbMax.Visibility     != Visibility.Visible) return;

        //        // If the new level is not different, ignore the request.
        //        if (value == SelectedLevel) return;

        //        var oldValue = SelectedLevel;

        //        // If the buttons don't reflect this new value, update them.
        //        if (GetDisplayedValue() != value) UpdateButtons(value);

        //        // Set the new value.
        //        SetValue(SelectedLevelProperty, value);

        //        // Notify additional listeners of the change.
        //        RandomizationLevelChanged?.Invoke(Tag, oldValue, value);
        //    }
        //}

        //private RandomizationLevel GetDisplayedValue()
        //{
        //    if (cbIsActive.IsChecked == false) return RandomizationLevel.None;
        //    if (rbSubtype.IsChecked ?? false) return RandomizationLevel.Subtype;
        //    if (rbType.IsChecked ?? false) return RandomizationLevel.Type;
        //    return RandomizationLevel.Max;
        //}

        //private void UpdateButtons(RandomizationLevel level)
        //{
        //    IgnoreChanges = true;

        //    switch (level)
        //    {
        //        case RandomizationLevel.None:
        //            cbIsActive.IsChecked = false;
        //            break;
        //        case RandomizationLevel.Subtype:
        //            cbIsActive.IsChecked = true;
        //            rbSubtype.IsChecked = true;
        //            rbType.IsChecked = false;
        //            rbMax.IsChecked = false;
        //            break;
        //        case RandomizationLevel.Type:
        //            cbIsActive.IsChecked = true;
        //            rbSubtype.IsChecked = false;
        //            rbType.IsChecked = true;
        //            rbMax.IsChecked = false;
        //            break;
        //        case RandomizationLevel.Max:
        //            cbIsActive.IsChecked = true;
        //            rbSubtype.IsChecked = false;
        //            rbType.IsChecked = false;
        //            rbMax.IsChecked = true;
        //            break;
        //        default:
        //            break;
        //    }

        //    IgnoreChanges = false;
        //}

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

        //private static void OnSelectedLevelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    var control = d as RandomizationLevelUserControl_V2;
        //    control.OnSelectedLevelChanged(e);
        //}

        ///// <summary>
        ///// Notify additional listeners whenever the selected level changes.
        ///// </summary>
        //private void OnSelectedLevelChanged(DependencyPropertyChangedEventArgs e)
        //{
        //    RandomizationLevelChanged?.Invoke(Tag, (RandomizationLevel)e.OldValue, (RandomizationLevel)e.NewValue);
        //}

        //private void RbSubtype_Checked(object sender, RoutedEventArgs e)
        //{
        //    if (IgnoreChanges) return;
        //    SelectedLevel = RandomizationLevel.Subtype;
        //}

        //private void RbType_Checked(object sender, RoutedEventArgs e)
        //{
        //    if (IgnoreChanges) return;
        //    SelectedLevel = RandomizationLevel.Type;
        //}

        //private void RbMax_Checked(object sender, RoutedEventArgs e)
        //{
        //    if (IgnoreChanges) return;
        //    SelectedLevel = RandomizationLevel.Max;
        //}

        //private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        //{
        //    if (IgnoreChanges) return;
        //    SelectedLevel = RandomizationLevel.None;
        //}

        //private void CheckBox_Checked(object sender, RoutedEventArgs e)
        //{
        //    if (IgnoreChanges) return;
        //    if (rbSubtype.IsChecked ?? false) { SelectedLevel = RandomizationLevel.Subtype; return; }
        //    if (rbType.IsChecked    ?? false) { SelectedLevel = RandomizationLevel.Type; return; }
        //    if (rbMax.IsChecked     ?? false) { SelectedLevel = RandomizationLevel.Max; return; }
        //    SelectedLevel = RandomizationLevel.None;
        //}

        #endregion

        private void RandoLevelFlagsChanged(RandoLevelFlags oldValue, RandoLevelFlags newValue)
        {
            var oldLevel = oldValue.ToRandomizationLevel();
            var newLevel = newValue.ToRandomizationLevel();
            if (oldLevel != newLevel) RandomizationLevelChanged?.Invoke(Tag, oldLevel, newLevel);
        }
    }
}
