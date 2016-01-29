using System.Collections.ObjectModel;

namespace ToDoAppWin10.Models
{
    public class CurrentUser : User
    {
        private static CurrentUser instance;
        
        private ObservableCollection<TodoItem> todos;

        private CurrentUser()
        {
            this.UserName = "Avi";
        }

        public static CurrentUser Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CurrentUser();
                }

                return instance;
            }
        }
    }
}
