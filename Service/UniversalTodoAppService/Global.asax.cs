using System.Web;

namespace UniversalTodoAppService
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            WebApiConfig.Register();
        }
    }
}