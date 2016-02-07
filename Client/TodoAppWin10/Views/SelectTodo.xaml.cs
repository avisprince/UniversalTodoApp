using System;
using ToDoAppWin10.Models;
using ToDoAppWin10.Utilities;
using ToDoAppWin10.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace ToDoAppWin10.Views
{
    /// <summary>
    /// Home page. User can view todo lists from this page
    /// </summary>
    public sealed partial class SelectToDo : Page
    {
        private SelectToDoViewModel selectTodoViewModel;

        public SelectToDo()
        {
            this.selectTodoViewModel = new SelectToDoViewModel();
            this.DataContext = this.selectTodoViewModel;

            this.InitializeComponent();
            this.Header.Background = Globals.ColorScheme.HeaderBackgroundColor;
        }

        private void Todo_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var todo = (TodoItem)((FrameworkElement)sender).Tag;
            Frame.Navigate(typeof(TodoDetails), todo);
        }

        private void AddTodo_Click(object sender, RoutedEventArgs e)
        {
            var newTodo = new TodoItem(this.AddTodoTextBox.Text, null);
            this.AddTodoTextBox.Text = string.Empty;

            this.selectTodoViewModel.AddNewTodo(newTodo);
        }

        private void SignoutButton_Click(object sender, RoutedEventArgs e)
        {
            AuthenticationHelper.Logout();

            Frame.Navigate(typeof(Login));
        }
    }
}
