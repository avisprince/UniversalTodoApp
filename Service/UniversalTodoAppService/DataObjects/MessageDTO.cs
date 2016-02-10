using System;

namespace UniversalTodoAppService.DataObjects
{
    public class MessageDTO
    {
        public string Id { get; set; }

        public string Text { get; set; }

        public DateTimeOffset? CreatedAt { get; set; }

        public string TodoItemId { get; set; }

        public EditorDTO Sender { get; set; }
    }
}