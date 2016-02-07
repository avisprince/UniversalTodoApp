using System;
using Windows.UI.Xaml.Data;

namespace ToDoAppWin10.Utilities.Converters
{
    public class TextLengthToIsEnabledConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return System.Convert.ToInt32(value) > 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
