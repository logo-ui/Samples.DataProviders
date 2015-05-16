using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace LogoUI.Samples.Client.Gui.Shared.Views.Converters
{
    public class TextLengthToVisibilityConbverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value.ToString() != string.Empty)
                return Visibility.Collapsed;
            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}