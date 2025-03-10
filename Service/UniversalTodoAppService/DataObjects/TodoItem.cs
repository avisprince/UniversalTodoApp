﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.WindowsAzure.Mobile.Service;

namespace UniversalTodoAppService.DataObjects
{
    public class TodoItem : EntityData
    {
        public TodoItem()
        {
            this.Todos = new Collection<TodoItem>();
            this.Messages = new Collection<Message>();
            this.Editors = new Collection<Editor>();
        }

        public string Title { get; set; }

        public string Description { get; set; }

        public bool Complete { get; set; }

        public DateTimeOffset? FinishDate { get; set; }

        public string LastEditorId { get; set; }
        [ForeignKey("LastEditorId")]
        public Editor LastEditor { get; set; }

        public string ParentId { get; set; }
        [ForeignKey("ParentId")]
        public TodoItem Parent { get; set; }

        public string AssignedToId { get; set; }
        [ForeignKey("AssignedToId")]
        public Editor AssignedTo { get; set; }

        public string CreatedById { get; set; }
        [ForeignKey("CreatedById")]
        public Editor CreatedBy { get; set; }

        public virtual ICollection<TodoItem> Todos { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<Editor> Editors { get; set; }
    }
}