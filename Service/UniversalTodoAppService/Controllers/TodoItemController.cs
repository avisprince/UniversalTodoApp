using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.WindowsAzure.Mobile.Service;
using Microsoft.WindowsAzure.Mobile.Service.Security;
using UniversalTodoAppService.DataObjects;
using UniversalTodoAppService.Models;

namespace UniversalTodoAppService.Controllers
{
    [AuthorizeLevel(AuthorizationLevel.User)]
    public class TodoItemController : TableController<TodoItem>
    {
        private UniversalTodoAppContext context;

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            context = new UniversalTodoAppContext();
            DomainManager = new EntityDomainManager<TodoItem>(context, Request, Services);
        }

        // GET tables/TodoItem
        public async Task<IEnumerable<TodoItemDTO>> GetAllTodoItems()
        {
            var fbAccessToken = await FacebookAuthHelper.GetFacebookAccessToken((ServiceUser)this.User);
            var facebookId = FacebookAuthHelper.GetCurrentUserFacebookId((ServiceUser)this.User, fbAccessToken);

            var currentEditor = this.context.Editors.Where(e => e.FacebookId == facebookId).FirstOrDefault();

            var todos = this.context.TodoItems//.Where(t => t.Editors.Contains(currentEditor))// && t.Parent == null)
                .Include(t => t.Messages).ToList();

            foreach (var todo in todos)
            {
                var editorIds = this.context.EditorTodoItems.Where(e => e.TodoItemId == todo.Id).Select(e => e.EditorId).ToList();
                foreach (var eId in editorIds)
                {
                    var editor = this.context.Editors.Where(e => e.Id == eId).First();
                    todo.Editors.Add(editor);
                }
            }

            return todos.Select(t => DTOConverter.ConvertToDTO(t, fbAccessToken));
        }

        // GET tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public async Task<IEnumerable<TodoItemDTO>> GetTodoItem(string id)
        {
            var fbAccessToken = await FacebookAuthHelper.GetFacebookAccessToken((ServiceUser)this.User);
            
            var todos = this.context.TodoItems.Where(t => t.ParentId == id)
                .Include(t => t.Messages)
                .Include(t => t.Editors).ToList();
            return todos.Select(t => DTOConverter.ConvertToDTO(t, fbAccessToken));
        }

        // PATCH tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public async Task<TodoItemDTO> PatchTodoItem(string id, Delta<TodoItemDTO> patch)
        {
            var fbAccessToken = await FacebookAuthHelper.GetFacebookAccessToken((ServiceUser)this.User);

            var patchedTodo = patch.GetEntity();
            var existingTodo = this.context.TodoItems.First(t => t.Id == id);

            existingTodo.Title = patchedTodo.Title;
            existingTodo.Description = patchedTodo.Description;
            existingTodo.FinishDate = patchedTodo.FinishDate;
            existingTodo.Complete = patchedTodo.Complete;

            var existingEditors = this.context.EditorTodoItems.Where(e => e.TodoItemId == existingTodo.Id).Select(e => e.Id).ToList();
            var patchEditors = patchedTodo.Editors.Select(e => e.Id).ToList();
            var newEditors = existingEditors.Except(patchEditors).Union(patchEditors.Except(existingEditors));
            foreach(var editorId in newEditors)
            {
                this.context.EditorTodoItems.Add(new EditorTodoItem() { EditorId = editorId, TodoItemId = existingTodo.Id });
            }

            await this.context.SaveChangesAsync();

            return DTOConverter.ConvertToDTO(existingTodo, fbAccessToken);
        }

        // POST tables/TodoItem
        public async Task<IHttpActionResult> PostTodoItem(TodoItemDTO item)
        {
            TodoItem current = await InsertAsync(DTOConverter.ConvertFromDTO(item));
            item.Id = current.Id;
            item.CreatedAt = current.CreatedAt;
            item.UpdatedAt = current.UpdatedAt;
            
            this.context.EditorTodoItems.Add(new EditorTodoItem() { Id = Guid.NewGuid().ToString(), EditorId = item.Editors.First().Id, TodoItemId = current.Id });
            await this.context.SaveChangesAsync();

            return CreatedAtRoute("Tables", item, item);
        }

        // DELETE tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public async Task DeleteTodoItem(string id)
        {
            var todo = this.context.TodoItems.First(t => t.Id == id);
            await DeleteTodoItemHelper(todo);

            return;
        }

        private async Task DeleteTodoItemHelper(TodoItem todo)
        {
            // Delete children
            var children = this.context.TodoItems.Where(t => t.ParentId == todo.Id).ToList();
            foreach (var child in children)
            {
                await DeleteTodoItemHelper(child);
            }

            // Delete messages
            var messages = this.context.Messages.Where(m => m.TodoItemId == todo.Id).ToList();
            this.context.Messages.RemoveRange(messages);

            // Delete Editor references
            var editors = this.context.EditorTodoItems.Where(e => e.TodoItemId == todo.Id).ToList();
            this.context.EditorTodoItems.RemoveRange(editors);

            // Delete todo
            this.context.TodoItems.Remove(todo);

            await this.context.SaveChangesAsync();
        }
    }
}