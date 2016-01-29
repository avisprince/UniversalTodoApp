using System;

namespace ToDoAppWin10.Models
{
    public class Message : BaseModel
    {
        public Message() { }

        public Message(string text, string todoItemId)
        {
            this.Text = text;
            this.CreatedAt = DateTime.Now;
            this.Sender = CurrentUser.Instance;
            this.TodoItemId = todoItemId;
        }

        public string Id { get; set; }
        public string Text { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public User Sender { get; set; }
        public string TodoItemId { get; set; }

        public string Signature
        {
            get
            {
                return CurrentUser.Instance.UserName + ": " + this.CreatedAt.DateTime.ToString();
            }
        }
    }
}
