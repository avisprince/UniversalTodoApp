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
    public class MessageController : TableController<Message>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            UniversalTodoAppContext context = new UniversalTodoAppContext();
            DomainManager = new EntityDomainManager<Message>(context, Request, Services);
        }

        // GET tables/Message
        public IQueryable<Message> GetAllMessage()
        {
            return Query(); 
        }

        // GET tables/Message/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Message> GetMessage(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Message/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Message> PatchMessage(string id, Delta<Message> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/Message
        public async Task<IHttpActionResult> PostMessage(MessageDTO item)
        {
            Message current = await InsertAsync(DTOConverter.ConvertFromDTO(item));
            item.Id = current.Id;
            item.CreatedAt = current.CreatedAt;

            return CreatedAtRoute("Tables", item, item);
        }

        // DELETE tables/Message/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteMessage(string id)
        {
             return DeleteAsync(id);
        }
    }
}