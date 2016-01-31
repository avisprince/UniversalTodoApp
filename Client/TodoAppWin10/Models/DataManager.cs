using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using ToDoAppWin10.Views;

namespace ToDoAppWin10.Models
{
    public static class DataManager
    {
        public static Dictionary<string, User> Users = new Dictionary<string, User>();

        public static ObservableCollection<TodoItem> Todos = new ObservableCollection<TodoItem>();

        public static async Task LoadData()
        {
            var todos = await App.MobileService.GetTable<TodoItem>().ToListAsync();
            Todos = new ObservableCollection<TodoItem>(todos);

            // load users
        }

        public static void DeleteTodo(TodoItem todo)
        {
            var children = Todos.Where(t => t.Parent != null && t.Parent.Id == todo.Id).ToList();
            foreach (var child in children)
            {
                DeleteTodo(child);
            }

            Todos.Remove(todo);
        }
    }
}
