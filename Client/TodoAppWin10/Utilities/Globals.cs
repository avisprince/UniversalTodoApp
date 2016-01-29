using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace ToDoAppWin10.Utilities
{
    public static class Globals
    {
        public static class ColorScheme
        {
            public static readonly SolidColorBrush SystemAccentColor = new SolidColorBrush((Color)Application.Current.Resources["SystemAccentColor"]);

            public static readonly SolidColorBrush HeaderBackgroundColor = new SolidColorBrush(Color.FromArgb(100, 100, 100, 100));
        }
    }
}
