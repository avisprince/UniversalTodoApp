﻿using System;
using System.Collections.ObjectModel;

namespace ToDoAppWin10.Models
{
    public class TodoItem : BaseModel
    {
        private string title;
        private string description;
        private bool complete;
        private DateTimeOffset createdAt;
        private DateTimeOffset updatedAt;
        private DateTimeOffset finishDate;
        private TodoItem parent;
        private Editor createdBy;
        private Editor lastEditor; 
        private Editor assignedTo;

        public TodoItem() { }

        public TodoItem(string title, TodoItem parent)
        {
            this.Title = title;
            this.Description = string.Empty;
            this.CreatedAt = DateTime.Now;
            this.UpdatedAt = DateTime.Now;
            this.Parent = parent;
            this.CreatedBy = CurrentUser.Instance;
            this.LastEditor = CurrentUser.Instance;

            //this.Todos = new ObservableCollection<TodoItem>();
            this.Editors = new ObservableCollection<Editor>() { CurrentUser.Instance };
            this.Messages = new ObservableCollection<Message>();
        }

        public string Id
        {
            get;
            set;
        }

        public string Title
        {
            get
            {
                return this.title;
            }
            set
            {
                if (this.title != value)
                {
                    this.title = value;
                    this.NotifyPropertyChanged("Title");
                }
            }
        }

        public bool Complete
        {
            get
            {
                return this.complete;
            }
            set
            {
                if (this.complete != value)
                {
                    this.complete = value;
                    this.NotifyPropertyChanged("Complete");
                }
            }
        }

        public string Description
        {
            get
            {
                return this.description;
            }
            set
            {
                if (this.description != value)
                {
                    this.description = value;
                    this.NotifyPropertyChanged("Description");
                }
            }
        }

        public DateTimeOffset CreatedAt
        {
            get
            {
                return this.createdAt;
            }
            set
            {
                if (this.createdAt != value)
                {
                    this.createdAt = value;
                    this.NotifyPropertyChanged("CreatedAt");
                }
            }
        }

        public DateTimeOffset UpdatedAt
        {
            get
            {
                return this.updatedAt;
            }
            set
            {
                if (this.updatedAt != value)
                {
                    this.updatedAt = value;
                    this.NotifyPropertyChanged("UpdatedAt");
                }
            }
        }

        public DateTimeOffset FinishDate
        {
            get
            {
                return this.finishDate;
            }
            set
            {
                if (this.finishDate != value)
                {
                    this.finishDate = value;
                    this.NotifyPropertyChanged("FinishDate");
                }
            }
        }

        public TodoItem Parent
        {
            get
            {
                return this.parent;
            }
            set
            {
                if (this.parent != value)
                {
                    this.parent = value;
                    this.NotifyPropertyChanged("Parent");
                }
            }
        }

        public Editor CreatedBy
        {
            get
            {
                return this.createdBy;
            }
            set
            {
                if (this.createdBy != value)
                {
                    this.createdBy = value;
                    this.NotifyPropertyChanged("CreatedBy");
                }
            }
        }

        public Editor LastEditor
        {
            get
            {
                return this.lastEditor;
            }
            set
            {
                if (this.lastEditor != value)
                {
                    this.lastEditor = value;
                    this.NotifyPropertyChanged("LastEditor");
                }
            }
        }

        public Editor AssignedTo
        {
            get
            {
                return this.assignedTo;
            }
            set
            {
                if (this.assignedTo != value)
                {
                    this.assignedTo = value;
                    this.NotifyPropertyChanged("AssignedTo");
                }
            }
        }

        //public ObservableCollection<TodoItem> Todos
        //{
        //    get;
        //    set;
        //}

        public ObservableCollection<Editor> Editors
        {
            get;
            set;
        }

        public ObservableCollection<Message> Messages
        {
            get;
            set;
        }
    }
}
