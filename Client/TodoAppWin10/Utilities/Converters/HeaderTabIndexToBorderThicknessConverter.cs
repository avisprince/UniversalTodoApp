using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace ToDoAppWin10.Utilities.Converters
{
    public class HeaderTabIndexToBorderThicknessConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var tabIndex = System.Convert.ToInt32(parameter);
            var selectedIndex = System.Convert.ToInt32(value);

            if (tabIndex == selectedIndex)
            {
                return new Thickness(0, 0, 0, 1);
            }
            else
            {
                return new Thickness(0, 0, 0, 0);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
