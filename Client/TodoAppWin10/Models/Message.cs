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
        public Editor Sender { get; set; }
        public string TodoItemId { get; set; }

        public string Signature
        {
            get
            {
                //return this.CreatedAt.DateTime.ToString();
                return this.GetFormattedDate();
            }
        }

        private string GetFormattedDate()
        {
            var messageDate = this.CreatedAt.Date;

            // if < same day

            // if same week
            var weekday = this.CreatedAt.DayOfWeek;
            var local = this.CreatedAt.LocalDateTime;
            return "";

            // if same year

            // else
            //return 
        }
    }
}
