using Facebook;
using System.Linq;

namespace UniversalTodoAppService.DataObjects
{
    public static class DTOConverter
    {
        public static EditorDTO ConvertToDTO(Editor editor, string accessToken)
        {
            var fbClient = new FacebookClient(accessToken);

            dynamic userInfo = fbClient.Get(editor.FacebookId);
            dynamic picture = fbClient.Get(editor.FacebookId, new { fields = new[] { "picture" } });

            return new EditorDTO()
            {
                Id = editor.Id,
                FacebookId = editor.FacebookId,
                UserName = (string)userInfo.name,
                PictureUrl = (string)picture.picture.data.url,
            };
        }

        public static TodoItemDTO ConvertToDTO(TodoItem todo, string fbAccessToken)
        {
            return new TodoItemDTO
            {
                Id = todo.Id,
                Title = todo.Title,
                Description = todo.Description,
                Complete = todo.Complete,
                CreatedAt = todo.CreatedAt,
                UpdatedAt = todo.UpdatedAt,
                FinishDate = todo.FinishDate,
                Parent = todo.Parent != null ? ConvertToDTO(todo.Parent, fbAccessToken) : null,
                Messages = todo.Messages.Select(m => ConvertToDTO(m, fbAccessToken)).ToList(),
                Editors = todo.Editors.Select(e => ConvertToDTO(e, fbAccessToken)).ToList(),
            };
        }

        public static MessageDTO ConvertToDTO(Message message, string fbAccessToken)
        {
            return new MessageDTO()
            {
                Id = message.Id,
                Text = message.Text,
                TodoItemId = message.TodoItemId,
                CreatedAt = message.CreatedAt,
            };
        }

        public static Editor ConvertFromDTO(EditorDTO editorDto)
        {
            return new Editor()
            {
                Id = editorDto.Id,
                FacebookId = editorDto.FacebookId,
            };
        }

        public static TodoItem ConvertFromDTO(TodoItemDTO todoDto)
        {
            return new TodoItem
            {
                Id = todoDto.Id,
                Title = todoDto.Title,
                Description = todoDto.Description,
                Complete = todoDto.Complete,
                CreatedAt = todoDto.CreatedAt,
                UpdatedAt = todoDto.UpdatedAt,
                FinishDate = todoDto.FinishDate,
                ParentId = todoDto.Parent != null ? todoDto.Parent.Id : null,
            };
        }

        public static Message ConvertFromDTO(MessageDTO messageDto)
        {
            return new Message()
            {
                Id = messageDto.Id,
                Text = messageDto.Text,
                TodoItemId = messageDto.TodoItemId,
                CreatedAt = messageDto.CreatedAt,
            };
        }
    }
}