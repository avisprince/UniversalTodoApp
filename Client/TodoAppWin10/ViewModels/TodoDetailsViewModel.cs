using System;
using System.Collections.Generic;
using System.Linq;
using ToDoAppWin10.Models;
using ToDoAppWin10.Views;

namespace ToDoAppWin10.ViewModels
{
    public class TodoDetailsViewModel : BaseViewModel
    {
        private bool showFinishByDetails;

        public TodoDetailsViewModel(TodoItem todo)
        {
            this.CurrentTodo = todo;
            this.ShowFinishByDetails = this.CurrentTodo.FinishDate != new DateTime();
        }

        public TodoItem CurrentTodo
        {
            get;
            private set;
        }

        public ICollection<TodoItem> Todos
        {
            get
            {
                return DataManager.Todos.Where(t => t.Parent != null && t.Parent.Id == CurrentTodo.Id).ToList();
            }
        }

        public bool ShowFinishByDetails
        {
            get
            {
                return this.showFinishByDetails;
            }
            set
            {
                if (this.showFinishByDetails != value)
                {
                    this.showFinishByDetails = value;
                    this.NotifyPropertyChanged("ShowFinishByDetails");
                }
            }
        }

        public async void AddNewTodo(TodoItem newTodo)
        {
            DataManager.Todos.Add(newTodo);
            this.NotifyPropertyChanged("Todos");

            await App.MobileService.GetTable<TodoItem>().InsertAsync(newTodo);
        }

        public async void EditTodo()
        {
            await App.MobileService.GetTable<TodoItem>().UpdateAsync(this.CurrentTodo);
        }
    }
}
