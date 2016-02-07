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
    public class EditorController : TableController<Editor>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            var context = new UniversalTodoAppContext();
            DomainManager = new EntityDomainManager<Editor>(context, Request, Services);
        }

        // GET tables/Editor
        public IQueryable<Editor> GetAllEditor()
        {
            return Query();
        }

        // GET tables/Editor/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public async Task<EditorDTO> GetEditor(string id)
        {
            var fbAccessToken = await FacebookAuthHelper.GetFacebookAccessToken((ServiceUser)this.User);

            var currentEditor = Query().Where(e => e.Id == id).FirstOrDefault();

            return DTOConverter.ConvertToDTO(currentEditor, fbAccessToken);
        }

        // PATCH tables/Editor/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Editor> PatchEditor(string id, Delta<Editor> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/Editor
        public async Task<IHttpActionResult> PostEditor(Editor item)
        {
            var fbAccessToken = await FacebookAuthHelper.GetFacebookAccessToken((ServiceUser)this.User);
            var facebookId = FacebookAuthHelper.GetCurrentUserFacebookId((ServiceUser)this.User, fbAccessToken);

            var currentEditor = Query().Where(e => e.FacebookId == facebookId).FirstOrDefault();
            if (currentEditor == null)
            {
                currentEditor = await InsertAsync(new Editor() { FacebookId = facebookId });
            }

            var currentEditorDTO = DTOConverter.ConvertToDTO(currentEditor, fbAccessToken);
            return CreatedAtRoute("Tables", new { id = currentEditorDTO.Id }, currentEditorDTO);
        }

        // DELETE tables/Editor/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteEditor(string id)
        {
             return DeleteAsync(id);
        }
    }
}