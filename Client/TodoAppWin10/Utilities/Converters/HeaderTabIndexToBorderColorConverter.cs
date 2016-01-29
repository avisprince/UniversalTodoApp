using System;
using Windows.UI.Xaml.Data;

namespace ToDoAppWin10.Utilities.Converters
{
    public class HeaderTabIndexToBorderColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var tabIndex = System.Convert.ToInt32(parameter);
            var selectedIndex = System.Convert.ToInt32(value);

            if (tabIndex == selectedIndex)
            {
                return Globals.ColorScheme.SystemAccentColor;
            }
            else
            {
                return Globals.ColorScheme.HeaderBackgroundColor;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}

