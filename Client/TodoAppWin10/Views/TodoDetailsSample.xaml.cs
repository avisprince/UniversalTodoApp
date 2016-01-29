using System;
using ToDoAppWin10.Utilities;
using Windows.UI.Xaml.Controls;

namespace ToDoAppWin10.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ToDoItemDetails : Page
    {
        public ToDoItemDetails()
        {
            this.InitializeComponent();

            this.Header.Background = Globals.ColorScheme.HeaderBackgroundColor;
        }

        private void HeaderTab_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            var panel = sender as StackPanel;
            if (panel != null && panel.Tag != null)
            {
                this.ToDoDetailsSamplePivot.SelectedIndex = Int32.Parse(panel.Tag.ToString());
            }
        }
    }
}
