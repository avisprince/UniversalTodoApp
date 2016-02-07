using Microsoft.WindowsAzure.Mobile.Service;

namespace UniversalTodoAppService.DataObjects
{
    public class EditorTodoItem : EntityData
    {
        public string EditorId { get; set; }

        public string TodoItemId { get; set; }
    }
}
