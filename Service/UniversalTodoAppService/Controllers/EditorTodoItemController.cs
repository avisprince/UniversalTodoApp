using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.WindowsAzure.Mobile.Service;
using UniversalTodoAppService.DataObjects;
using UniversalTodoAppService.Models;

namespace UniversalTodoAppService.Controllers
{
    public class EditorTodoItemController : TableController<EditorTodoItem>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            UniversalTodoAppContext context = new UniversalTodoAppContext();
            DomainManager = new EntityDomainManager<EditorTodoItem>(context, Request, Services);
        }

        // GET tables/EditorTodoItem
        public IQueryable<EditorTodoItem> GetAllEditorTodoItem()
        {
            return Query(); 
        }

        // GET tables/EditorTodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<EditorTodoItem> GetEditorTodoItem(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/EditorTodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<EditorTodoItem> PatchEditorTodoItem(string id, Delta<EditorTodoItem> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/EditorTodoItem
        public async Task<IHttpActionResult> PostEditorTodoItem(EditorTodoItem item)
        {
            EditorTodoItem current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/EditorTodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteEditorTodoItem(string id)
        {
             return DeleteAsync(id);
        }
    }
}