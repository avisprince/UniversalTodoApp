using System.Collections.ObjectModel;

namespace ToDoAppWin10.Models
{
    public class User : BaseModel
    {
        private string userName;

        public int UserId
        {
            get;
            set;
        }

        public string UserName
        {
            get
            {
                return this.userName;
            }
            set
            {
                if (this.userName != value)
                {
                    this.userName = value;
                    this.NotifyPropertyChanged("UserName");
                }
            }
        }
    }
}
