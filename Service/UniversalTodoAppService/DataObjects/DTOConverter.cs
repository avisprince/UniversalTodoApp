using Facebook;
using System.Linq;

namespace UniversalTodoAppService.DataObjects
{
    public static class DTOConverter
    {
        public static EditorDTO ConvertToDTO(Editor editor, string accessToken)
        {
            var fbClient = new FacebookClient(accessToken);

            dynamic userInfo = fbClient.Get(editor.FacebookId, new { fields = new[] { "id", "name", "picture" } });
            //?ids=10156371365995408,10103006789027538&fields=id,name,picture

            return new EditorDTO()
            {
                Id = editor.Id,
                FacebookId = editor.FacebookId,
                UserName = (string)userInfo.name,
                PictureUrl = (string)userInfo.picture.data.url,
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
                AssignedTo = todo.AssignedTo != null ? ConvertToDTO(todo.AssignedTo, fbAccessToken) : null,
                LastEditor = ConvertToDTO(todo.LastEditor, fbAccessToken),
                CreatedBy = ConvertToDTO(todo.CreatedBy, fbAccessToken),
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
                CreatedAt = message.CreatedAt,
                TodoItemId = message.TodoItemId,
                Sender = ConvertToDTO(message.Sender, fbAccessToken)
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
                AssignedToId = todoDto.AssignedTo != null ? todoDto.AssignedTo.Id : null,
                LastEditorId = todoDto.LastEditor.Id,
                CreatedById = todoDto.CreatedBy.Id,
            };
        }

        public static Message ConvertFromDTO(MessageDTO messageDto)
        {
            return new Message()
            {
                Id = messageDto.Id,
                Text = messageDto.Text,
                CreatedAt = messageDto.CreatedAt,
                TodoItemId = messageDto.TodoItemId,
                SenderId = messageDto.Sender.Id,
            };
        }
    }
}