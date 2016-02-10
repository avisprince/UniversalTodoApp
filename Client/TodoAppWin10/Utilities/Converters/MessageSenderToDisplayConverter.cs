using System;
using ToDoAppWin10.Models;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace ToDoAppWin10.Utilities.Converters
{
    public class MessageSenderToDisplayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            //return Visibility.Visible;
            var displayIfCurrentUser = System.Convert.ToBoolean(parameter);
            var sender = value as Editor;

            if (displayIfCurrentUser)
            {
                return sender.Id == CurrentUser.Instance.Id
                        ? Visibility.Visible
                        : Visibility.Collapsed;
            }
            else
            {
                return sender.Id != CurrentUser.Instance.Id
                        ? Visibility.Visible
                        : Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
