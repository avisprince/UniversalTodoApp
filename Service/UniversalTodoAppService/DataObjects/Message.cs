using Microsoft.WindowsAzure.Mobile.Service;

namespace UniversalTodoAppService.DataObjects
{
    public class Message : EntityData
    {
        public string Text { get; set; }

        //public string SenderId { get; set; }
        
        public string TodoItemId { get; set; }
        public virtual TodoItem Todo { get; set; }
    }
}