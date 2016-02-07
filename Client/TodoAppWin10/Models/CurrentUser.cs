using System.Collections.ObjectModel;

namespace ToDoAppWin10.Models
{
    public class CurrentUser : Editor
    {
        private static CurrentUser instance;

        private CurrentUser()
        {
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
