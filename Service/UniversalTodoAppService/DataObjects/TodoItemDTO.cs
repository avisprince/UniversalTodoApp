using System;
using System.Collections.Generic;
using Microsoft.WindowsAzure.Mobile.Service;

namespace UniversalTodoAppService.DataObjects
{
    public class TodoItemDTO
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public bool Complete { get; set; }

        public DateTimeOffset? CreatedAt { get; set; }

        public DateTimeOffset? UpdatedAt { get; set; }

        public DateTimeOffset? FinishDate { get; set; }

        //public UserInfo LastEditor { get; set; }
        
        public TodoItemDTO Parent { get; set; }

        //public UserInfo AssignedTo { get; set; }

        //public ICollection<TodoItemDTO> Todos { get; set; }

        //public ICollection<UserInfo> Editors { get; set; }

        public ICollection<MessageDTO> Messages { get; set; }
    }
}