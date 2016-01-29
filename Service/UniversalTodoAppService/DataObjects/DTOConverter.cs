using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversalTodoAppService.DataObjects
{
    public static class DTOConverter
    {
        public static TodoItemDTO ConvertToDTO(TodoItem todo)
        {
            return new TodoItemDTO
            {
                Id = todo.Id,
                Title = todo.Title,
                Description = todo.Description,
                CreatedAt = todo.CreatedAt,
                UpdatedAt = todo.UpdatedAt,
                FinishDate = todo.FinishDate,
                Parent = todo.Parent != null ? ConvertToDTO(todo.Parent) : null,
                Messages = todo.Messages.Select(m => ConvertToDTO(m)).ToList(),
            };
        }

        public static MessageDTO ConvertToDTO(Message message)
        {
            return new MessageDTO()
            {
                Id = message.Id,
                Text = message.Text,
                TodoItemId = message.TodoItemId,
                CreatedAt = message.CreatedAt,
            };
        }

        public static TodoItem ConvertFromDTO(TodoItemDTO todo)
        {
            return new TodoItem
            {
                Id = todo.Id,
                Title = todo.Title,
                Description = todo.Description,
                CreatedAt = todo.CreatedAt,
                UpdatedAt = todo.UpdatedAt,
                FinishDate = todo.FinishDate,
                ParentId = todo.Parent != null ? todo.Parent.Id : null,
            };
        }

        public static Message ConvertFromDTO(MessageDTO message)
        {
            return new Message()
            {
                Id = message.Id,
                Text = message.Text,
                TodoItemId = message.TodoItemId,
                CreatedAt = message.CreatedAt,
            };
        }
    }
}