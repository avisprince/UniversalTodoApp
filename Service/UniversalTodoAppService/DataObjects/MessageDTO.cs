using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversalTodoAppService.DataObjects
{
    public class MessageDTO
    {
        public string Id { get; set; }

        public string Text { get; set; }

        public DateTimeOffset? CreatedAt { get; set; }

        public string TodoItemId { get; set; }
    }
}