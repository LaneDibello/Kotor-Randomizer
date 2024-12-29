using kotor_Randomizer_2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for RandomizationLevelUserControl.xaml
    /// </summary>
    public partial class RandomizationLevelUserControl : UserControl
    {
        public RandomizationLevelUserControl()
        {
            InitializeComponent();

            var active = cbIsActive.IsChecked;

            if (rbSubtype.Visibility == Visibility.Visible)
            {
                SelectedLevel = RandomizationLevel.Subtype;
            }
            else
            {
                SelectedLevel = RandomizationLevel.Type;
            }

            if (active == false)
            {
                SelectedLevel = RandomizationLevel.None;
            }
        }

        #region Dependency Properties

        public static readonly DependencyProperty SelectedLevelProperty = DependencyProperty.Register("SelectedLevel", typeof(RandomizationLevel),
            typeof(RandomizationLevelUserControl), new PropertyMetadata(RandomizationLevel.None, new PropertyChangedCallback(OnSelectedLevelChanged)));

        public static readonly DependencyProperty CheckboxLabelProperty = DependencyProperty.Register("CheckboxLabel", typeof(string), typeof(RandomizationLevelUserControl), new PropertyMetadata("Hello World"));
        public static readonly DependencyProperty CheckboxToolTipProperty = DependencyProperty.Register("CheckboxToolTip", typeof(string), typeof(RandomizationLevelUserControl));

        public static readonly DependencyProperty SubtypeLabelProperty = DependencyProperty.Register("SubtypeLabel", typeof(string), typeof(RandomizationLevelUserControl), new PropertyMetadata("Subtype"));
        public static readonly DependencyProperty TypeLabelProperty = DependencyProperty.Register("TypeLabel", typeof(string), typeof(RandomizationLevelUserControl), new PropertyMetadata("Type"));
        public static readonly DependencyProperty MaxLabelProperty = DependencyProperty.Register("MaxLabel", typeof(string), typeof(RandomizationLevelUserControl), new PropertyMetadata("Max"));

        public static readonly DependencyProperty SubtypeVisibleProperty = DependencyProperty.Register("SubtypeVisible", typeof(Visibility),
            typeof(RandomizationLevelUserControl), new PropertyMetadata(Visibility.Visible, new PropertyChangedCallback(OnSubtypeVisibilityChanged)));
        public static readonly DependencyProperty TypeVisibleProperty = DependencyProperty.Register("TypeVisible", typeof(Visibility),
            typeof(RandomizationLevelUserControl), new PropertyMetadata(Visibility.Visible));
        public static readonly DependencyProperty MaxVisibleProperty = DependencyProperty.Register("MaxVisible", typeof(Visibility),
            typeof(RandomizationLevelUserControl), new PropertyMetadata(Visibility.Visible));


        public static readonly DependencyProperty LevelsBoxMinWidthProperty = DependencyProperty.Register("LevelsBoxMinWidth", typeof(double), typeof(RandomizationLevelUserControl));

        #endregion Dependency Properties

        #region Public Properties

        public RandomizationLevel SelectedLevel
        {
            get => (RandomizationLevel)GetValue(SelectedLevelProperty);
            set
            {
                // If the requested level is not visible, ignore the request.
                if (value == RandomizationLevel.Subtype && SubtypeVisible != Visibility.Visible) return;
                if (value == RandomizationLevel.Type    && TypeVisible    != Visibility.Visible) return;
                if (value == RandomizationLevel.Max     && MaxVisible     != Visibility.Visible) return;

                SetValue(SelectedLevelProperty, value);
            }
        }

        public string CheckboxLabel
        {
            get => (string)GetValue(CheckboxLabelProperty);
            set => SetValue(CheckboxLabelProperty, value);
        }

        public string CheckboxToolTip
        {
            get => (string)GetValue(CheckboxToolTipProperty);
            set => SetValue(CheckboxToolTipProperty, value);
        }

        public string SubtypeLabel
        {
            get => (string)GetValue(SubtypeLabelProperty);
            set => SetValue(SubtypeLabelProperty, value);
        }

        public string TypeLabel
        {
            get => (string)GetValue(TypeLabelProperty);
            set => SetValue(TypeLabelProperty, value);
        }

        public string MaxLabel
        {
            get => (string)GetValue(MaxLabelProperty);
            set => SetValue(MaxLabelProperty, value);
        }

        public Visibility SubtypeVisible
        {
            get => (Visibility)GetValue(SubtypeVisibleProperty);
            set => SetValue(SubtypeVisibleProperty, value);
        }

        public Visibility TypeVisible
        {
            get => (Visibility)GetValue(TypeVisibleProperty);
            set => SetValue(TypeVisibleProperty, value);
        }

        public Visibility MaxVisible
        {
            get => (Visibility)GetValue(MaxVisibleProperty);
            set => SetValue(MaxVisibleProperty, value);
        }

        public double LevelsBoxMinWidth
        {
            get => (double)GetValue(LevelsBoxMinWidthProperty);
            set => SetValue(LevelsBoxMinWidthProperty, value);
        }

        public bool IsChecked
        {
            get => cbIsActive.IsChecked ?? false;
            set => cbIsActive.IsChecked = value;
        }

        #endregion Public Properties

        #region Events

        public delegate void Control_RandomizationLevelChanged(object tag, RandomizationLevel oldValue, RandomizationLevel newValue);
        public event Control_RandomizationLevelChanged RandomizationLevelChanged;

        private static void OnSelectedLevelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as RandomizationLevelUserControl;
            control.OnSelectedLevelChanged(e);
        }

        /// <summary>
        /// Whever the selection level is changed, ensure the checkbox and radio buttons reflect the change. The discrepancy would appear
        /// when a rando preset is loaded, chainging the settings without going through the buttons themselves.
        /// </summary>
        private void OnSelectedLevelChanged(DependencyPropertyChangedEventArgs e)
        {
            if ((RandomizationLevel)e.NewValue == RandomizationLevel.None)
            {
                cbIsActive.IsChecked = false;
            }
            else
            {
                rbSubtype.IsChecked = (RandomizationLevel)e.NewValue == RandomizationLevel.Subtype;
                rbType.IsChecked = (RandomizationLevel)e.NewValue == RandomizationLevel.Type;
                rbMax.IsChecked = (RandomizationLevel)e.NewValue == RandomizationLevel.Max;
                cbIsActive.IsChecked = true;
            }
            RandomizationLevelChanged?.Invoke(Tag, (RandomizationLevel)e.OldValue, (RandomizationLevel)e.NewValue);
        }

        private static void OnSubtypeVisibilityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RandomizationLevelUserControl control = d as RandomizationLevelUserControl;
            control.OnSubtypeVisibilityChanged(e);
        }

        private void OnSubtypeVisibilityChanged(DependencyPropertyChangedEventArgs e)
        {
            if ((Visibility)e.NewValue != Visibility.Visible)
            {
                var isOff = SelectedLevel == RandomizationLevel.None;

                if (rbSubtype.IsChecked == true)
                {
                    if (TypeVisible == Visibility.Visible) rbType.IsChecked = true;
                    else rbMax.IsChecked = true;

                    if (isOff) SelectedLevel = RandomizationLevel.None;
                }
            }
            else
            {
                var isOff = SelectedLevel == RandomizationLevel.None;

                if (TypeVisible == Visibility.Visible && rbType.IsChecked == true)
                {
                    rbSubtype.IsChecked = true;
                }
                else if (MaxVisible == Visibility.Visible && rbMax.IsChecked == true)
                {
                    rbSubtype.IsChecked = true;
                }
            }
        }

        private void RbSubtype_Checked(object sender, RoutedEventArgs e)
        {
            SelectedLevel = RandomizationLevel.Subtype;
        }

        private void RbType_Checked(object sender, RoutedEventArgs e)
        {
            SelectedLevel = RandomizationLevel.Type;
        }

        private void RbMax_Checked(object sender, RoutedEventArgs e)
        {
            SelectedLevel = RandomizationLevel.Max;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            SelectedLevel = RandomizationLevel.None;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (rbSubtype.IsChecked ?? false) { SelectedLevel = RandomizationLevel.Subtype; return; }
            if (rbType.IsChecked    ?? false) { SelectedLevel = RandomizationLevel.Type; return; }
            if (rbMax.IsChecked     ?? false) { SelectedLevel = RandomizationLevel.Max; return; }
            SelectedLevel = RandomizationLevel.None;
        }

        #endregion Events

        public override string ToString()
        {
            return $"{CheckboxLabel}: {SelectedLevel}";
        }
    }
}
