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
            return Visibility.Visible;
            //var displayIfCurrentUser = System.Convert.ToBoolean(parameter);
            //var sender = value as User;

            //if (displayIfCurrentUser)
            //{
            //    return sender.UserId == CurrentUser.Instance.UserId
            //            ? Visibility.Visible
            //            : Visibility.Collapsed;
            //}
            //else
            //{
            //    return sender.UserId != CurrentUser.Instance.UserId
            //            ? Visibility.Visible
            //            : Visibility.Collapsed;
            //}
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
