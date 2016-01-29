using System;
using System.Collections.ObjectModel;
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
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TodoDetails : Page
    {
        private TodoDetailsViewModel todoDetails;

        public TodoDetails()
        {
            this.InitializeComponent();
            this.Header.Background = Globals.ColorScheme.HeaderBackgroundColor;
            this.HeaderTabs.Background = Globals.ColorScheme.HeaderBackgroundColor;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is TodoItem)
            {
                this.NavigateToTodo((TodoItem)e.Parameter);
            }

            base.OnNavigatedTo(e);
        }

        /* Header */

        private void HeaderTab_Click(object sender, RoutedEventArgs e)
        {
            var panel = sender as AppBarButton;
            if (panel != null && panel.Tag != null)
            {
                this.TodoDetailsPivot.SelectedIndex = Int32.Parse(panel.Tag.ToString());
            }
        }

        private void HeaderBackButton_Click(object sender, RoutedEventArgs e)
        {
            var parent = this.todoDetails.CurrentTodo.Parent;

            if (parent != null)
            {
                this.NavigateToTodo(parent);
            }
            else
            {
                Frame.GoBack();
            }
        }

        /* Items tab */

        private void Todo_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var todo = (TodoItem)((FrameworkElement)sender).Tag;

            this.todoDetails = new TodoDetailsViewModel(todo);
            this.DataContext = this.todoDetails;
        }

        private void AddTodo_Click(object sender, RoutedEventArgs e)
        {
            var newTodo = new TodoItem(this.AddTodoTextBox.Text, this.todoDetails.CurrentTodo);
            this.AddTodoTextBox.Text = string.Empty;

            this.todoDetails.AddNewTodo(newTodo);
        }

        /* Details tab */

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.CheckIfTodoIsEdited();
        }

        private void ShowDateTimePickerCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            this.CheckIfTodoIsEdited();
        }

        private void DatePicker_DateChanged(object sender, DatePickerValueChangedEventArgs e)
        {
            this.CheckIfTodoIsEdited();
        }

        private void TimePicker_TimeChanged(object sender, TimePickerValueChangedEventArgs e)
        {
            if (this.todoDetails != null)
            {
                this.CheckIfTodoIsEdited();
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.CheckIfTodoIsEdited();
        }

        private void CheckIfTodoIsEdited()
        {
            var currentTodo = this.todoDetails.CurrentTodo;

            var todoIsEdited =
                this.TitleTextBox.Text != currentTodo.Title ||
                this.DescriptionTextBox.Text != currentTodo.Description ||
                ((User)this.AssignedToComboBox.SelectedItem) != currentTodo.AssignedTo;
            
            var dateIsChanged =
                this.ShowDateTimePickerCheckBox.IsChecked != this.todoDetails.ShowFinishByDetails ||
                (this.ShowDateTimePickerCheckBox.IsChecked == true &&
                (this.FinishByDatePicker.Date.Date != currentTodo.FinishDate.Date ||
                this.FinishByTimePicker.Time != currentTodo.FinishDate.TimeOfDay));

            this.DetailsTabEnableButtons(todoIsEdited || dateIsChanged);

            if (this.TitleTextBox.Text == string.Empty)
            {
                this.DetailsTabSaveButton.IsEnabled = false;
            }
        }

        private void DetailsTabSaveButton_Click(object sender, RoutedEventArgs e)
        {
            var currentTodo = this.todoDetails.CurrentTodo;
            currentTodo.Title = this.TitleTextBox.Text;
            currentTodo.Description = this.DescriptionTextBox.Text;
            this.todoDetails.ShowFinishByDetails = this.ShowDateTimePickerCheckBox.IsChecked == true;

            if (this.todoDetails.ShowFinishByDetails)
            {
                var dateTime = this.FinishByDatePicker.Date.DateTime;
                var timeSpan = this.FinishByTimePicker.Time;

                currentTodo.FinishDate = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, timeSpan.Hours, timeSpan.Seconds, timeSpan.Milliseconds);
            }
            else
            {
                currentTodo.FinishDate = new DateTime();
            }

            currentTodo.AssignedTo = this.AssignedToComboBox.SelectedItem as User;

            currentTodo.UpdatedAt = DateTime.Now;
            currentTodo.LastEditor = CurrentUser.Instance;

            this.DetailsTabEnableButtons(false);

            this.todoDetails.EditTodo();
        }

        private void DetailsTabCancelButton_Click(object sender, RoutedEventArgs e)
        {
            var currentTodo = this.todoDetails.CurrentTodo;

            this.TitleTextBox.Text = currentTodo.Title;
            this.DescriptionTextBox.Text = currentTodo.Description;
            this.AssignedToComboBox.SelectedItem = currentTodo.AssignedTo;

            this.ShowDateTimePickerCheckBox.IsChecked = this.todoDetails.ShowFinishByDetails;
            this.FinishByDatePicker.Date = this.todoDetails.CurrentTodo.FinishDate;
            this.FinishByTimePicker.Time = this.todoDetails.CurrentTodo.FinishDate.TimeOfDay;

            this.DetailsTabEnableButtons(false);
        }

        private void DetailsTabDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var currentTodo = this.todoDetails.CurrentTodo;
            var parent = currentTodo.Parent;

            if (parent != null)
            {
                //parent.Todos.Remove(currentTodo);
                this.NavigateToTodo(parent);
            }
            else
            {
                //CurrentUser.Instance.Todos.Remove(currentTodo);
                Frame.GoBack();
            }
        }

        private void DetailsTabEnableButtons(bool enable)
        {
            this.DetailsTabSaveButton.IsEnabled = enable;
            this.DetailsTabCancelButton.IsEnabled = enable;
        }

        /* Messages tab */

        private async void SendMessage_Click(object sender, RoutedEventArgs e)
        {
            var newMessage = new Message(this.SendMessageTextBox.Text, this.todoDetails.CurrentTodo.Id);
            this.todoDetails.CurrentTodo.Messages.Add(newMessage);
            this.SendMessageTextBox.Text = string.Empty;

            await App.MobileService.GetTable<Message>().InsertAsync(newMessage);
        }

        /* Editors tab */

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {

        }

        /* Helper Functions */

        private void NavigateToTodo(TodoItem todo)
        {
            this.TodoDetailsPivot.SelectedIndex = 0;
            this.todoDetails = new TodoDetailsViewModel(todo);
            this.DataContext = this.todoDetails;
        }
    }
}
