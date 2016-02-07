using System;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace ToDoAppWin10.Utilities.Converters
{
    public class HeaderTabIndexToTextColorConverter : IValueConverter
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
                return new SolidColorBrush(Colors.White);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
