using System.Collections.ObjectModel;

namespace ToDoAppWin10.Models
{
    public class Editor : BaseModel
    {
        private string userName;
        private string pictureUrl;

        public string Id
        {
            get;
            set;
        }

        public string FacebookId
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

        public string PictureUrl
        {
            get
            {
                return this.pictureUrl;
            }
            set
            {
                if (this.pictureUrl != value)
                {
                    this.pictureUrl = value;
                    this.NotifyPropertyChanged("ProfilePicture");
                }
            }
        }
    }
}
