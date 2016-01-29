using System.Collections.Generic;
using System.Linq;
using ToDoAppWin10.Models;
using ToDoAppWin10.Views;
using Windows.UI.Xaml.Data;

namespace ToDoAppWin10.ViewModels
{
    public class SelectToDoViewModel : BaseViewModel
    {
        public ICollection<TodoItem> Todos
        {
            get
            {
                return DataManager.Todos.Where(t => t.Parent == null).ToList();
            }
        }

        public async void AddNewTodo(TodoItem newTodo)
        {
            DataManager.Todos.Add(newTodo);
            this.NotifyPropertyChanged("Todos");

            await App.MobileService.GetTable<TodoItem>().InsertAsync(newTodo);
        }
    }
}
