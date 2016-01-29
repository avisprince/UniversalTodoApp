using Parse;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ToDoAppWin10.Models
{
    public class MasterTodoList
    {
        private static MasterTodoList instance;
        
        private ObservableCollection<Todo> todos;

        private MasterTodoList()
        {
            var loginSuccess = this.Login().Result;
        }

        public static MasterTodoList Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MasterTodoList();
                }

                return instance;
            }
        }

        public ObservableCollection<Todo> Todos
        {
            get
            {
                if (this.todos == null)
                {
                    this.todos = new ObservableCollection<Todo>();
                }

                return this.todos;
            }
            set
            {
                if (this.todos != value)
                {
                    this.todos = value;
                    //this.NotifyPropertyChanged("Todos");
                }
            }
        }

        private async Task<bool> Login()
        {
            var user = await ParseUser.LogInAsync("avi", "123");
            return user != null;
        }
    }
}
