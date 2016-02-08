using Microsoft.WindowsAzure.Mobile.Service;
using System.Collections;
using System.Collections.ObjectModel;

namespace UniversalTodoAppService.DataObjects
{
    public class Editor : EntityData
    {
        public Editor()
        {
            this.Todos = new Collection<TodoItem>();
            this.Messages = new Collection<Message>();
        }

        public string FacebookId { get; set; }

        public virtual ICollection Todos { get; set; }
        public virtual ICollection Messages { get; set; }
    }
}