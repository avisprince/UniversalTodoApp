using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Facebook;
using Microsoft.WindowsAzure.Mobile.Service;
using Microsoft.WindowsAzure.Mobile.Service.Security;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        public IEnumerable<TodoItemDTO> GetAllTodoItems()
        {
            var todos = Query().Include(t => t.Parent).Include(t => t.Messages).ToList();
            return todos.Select(t => DTOConverter.ConvertToDTO(t));
        }

        // GET tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public IEnumerable<TodoItemDTO> GetTodoItem(string id)
        {
            var todos = Query().Where(t => t.Parent.Id == id).Include(t => t.Messages).ToList();
            return todos.Select(t => DTOConverter.ConvertToDTO(t));
        }

        // PATCH tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public async Task<TodoItemDTO> PatchTodoItem(string id, Delta<TodoItemDTO> patch)
        {
            var patchedTodo = patch.GetEntity();
            var existingTodo = this.context.TodoItems.First(t => t.Id == id);

            existingTodo.Title = patchedTodo.Title;
            existingTodo.Description = patchedTodo.Description;
            existingTodo.FinishDate = patchedTodo.FinishDate;
            existingTodo.Complete = patchedTodo.Complete;

            await this.context.SaveChangesAsync();

            return DTOConverter.ConvertToDTO(existingTodo);
        }

        // POST tables/TodoItem
        public async Task<IHttpActionResult> PostTodoItem(TodoItemDTO item)
        {
            TodoItem current = await InsertAsync(DTOConverter.ConvertFromDTO(item));
            item.Id = current.Id;
            item.CreatedAt = current.CreatedAt;
            item.UpdatedAt = current.UpdatedAt;

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

            // Delete todo
            this.context.TodoItems.Remove(todo);

            await this.context.SaveChangesAsync();
        }

        //var user = this.User as ServiceUser;
        //var creds = (await user.GetIdentitiesAsync()).OfType<FacebookCredentials>().FirstOrDefault();
        //string accessToken = creds.AccessToken;

        //var user = this.User as ServiceUser;
        //return Query().Where(t => t.Editors.Contains(user.Id)).Include(t => t.Messages);

        //var identities = await user.GetIdentitiesAsync();
        //var result = new JObject();
        //var fb = identities.OfType<FacebookCredentials>().FirstOrDefault();
        //if (fb != null)
        //{
        //    var accessToken = fb.AccessToken;

        //    var client = new FacebookClient(accessToken);
        //    return client.Get("/me?fields=id,name,picture").ToString();
        //    //return JsonConvert.DeserializeObject<UserInfo>(userInfo);

        //    //result.Add("facebook", await GetProviderInfo("https://graph.facebook.com/me?access_token=" + accessToken));
        //}

        //return null;
    }
}