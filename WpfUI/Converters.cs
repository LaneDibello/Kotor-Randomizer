using kotor_Randomizer_2;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Randomizer_WPF
{
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BoolToVisibilityConverter : IValueConverter
    {
        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(Visibility)) throw new InvalidOperationException("The target must be of type Visibility.");

            if ((bool)value) return Visibility.Visible;
            else             return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(bool)) throw new InvalidOperationException("The target must be a boolean.");

            if ((Visibility)value == Visibility.Visible) return true;
            else                                         return false;
        }
        #endregion
    }

    [ValueConversion(typeof(double), typeof(double))]
    public class AddToDoubleConverter : IValueConverter
    {
        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(double))
                throw new InvalidOperationException("The target must be a double.");
            double.TryParse(parameter.ToString(), out double toAdd);
            double.TryParse(value.ToString(), out double result);
            result += toAdd;
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(double))
                throw new InvalidOperationException("The target must be a string.");
            double.TryParse(parameter.ToString(), out double toAdd);
            double.TryParse(value.ToString(), out double result);
            result -= toAdd;
            return result;
        }
        #endregion
    }

    [ValueConversion(typeof(string), typeof(double))]
    public class StringDoubleConverter : IValueConverter
    {
        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            const string DEFAULT = "14";
            if (targetType != typeof(double))
                throw new InvalidOperationException("The target must be a double.");
            double.TryParse((value as ComboBoxItem)?.Content?.ToString() ?? DEFAULT, out double result);
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(string))
                throw new InvalidOperationException("The target must be a string.");
            return value.ToString();
        }
        #endregion
    }

    [ValueConversion(typeof(bool), typeof(bool))]
    public class InverseBooleanConverter : IValueConverter
    {
        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(bool))
                throw new InvalidOperationException("The target must be a boolean.");
            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(bool))
                throw new InvalidOperationException("The target must be a boolean.");
            return !(bool)value;
        }
        #endregion
    }

    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class InverseBoolToVisibilityConverter : IValueConverter
    {
        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(Visibility)) throw new InvalidOperationException("The target must be of type Visibility.");

            if ((bool)value) return Visibility.Collapsed;
            else             return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(bool)) throw new InvalidOperationException("The target must be a boolean.");

            if ((Visibility)value == Visibility.Visible) return false;
            else                                         return true;
        }
        #endregion
    }

    [ValueConversion(typeof(ModuleExtras), typeof(bool))]
    public class ModuleExtrasToBoolConverter : IValueConverter
    {
        private ModuleExtras target;
        public ModuleExtrasToBoolConverter() { }

        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            target = (ModuleExtras)value;
            return target.HasFlag((ModuleExtras)parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (target.HasFlag((ModuleExtras)parameter))
            {
                if ((bool)value)
                    return target;
                else
                    return target ^= (ModuleExtras)parameter;
            }
            else
            {
                if ((bool)value)
                    return target |= (ModuleExtras)parameter;
                else
                    return target;
            }
        }
        #endregion
    }

    [ValueConversion(typeof(TextSettings), typeof(bool))]
    public class TextSettingsToBoolConverter : IValueConverter
    {
        private TextSettings target;
        public TextSettingsToBoolConverter() { }

        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            target = (TextSettings)value;
            return target.HasFlag((TextSettings)parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (target.HasFlag((TextSettings)parameter))
            {
                if ((bool)value)
                    return target;
                else
                    return target ^= (TextSettings)parameter;
            }
            else
            {
                if ((bool)value)
                    return target |= (TextSettings)parameter;
                else
                    return target;
            }
        }
        #endregion
    }

    [ValueConversion(typeof(RandomizationLevel), typeof(Visibility))]
    public class VisibleIfRandoLevelMatchesMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(Visibility)) return Visibility.Collapsed;
            if (!values.Any()) return Visibility.Visible;

            foreach (var value in values)
            {
                try
                { 
                    if ((RandomizationLevel)value == (RandomizationLevel)parameter)
                    {
                        return Visibility.Visible;
                    }
                }
                catch
                {
                    return Visibility.Visible;
                }
            }

            return Visibility.Collapsed;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
