using System.Linq;
using System.Threading.Tasks;
using Facebook;
using Microsoft.WindowsAzure.Mobile.Service.Security;

namespace UniversalTodoAppService.Controllers
{
    public static class FacebookAuthHelper
    {
        public static async Task<string> GetFacebookAccessToken(ServiceUser user)
        {
            return "CAACEdEose0cBAPstNHajP3ZAd1hAi6iPFhTUAF83Or9lpUtKm3NpbGNpjTZCZBCjc0N1a9Qzbwau67IS8NEqECts6ftVpuZBSdbWJoD3zjZCoYVI6jTFhvbrZAZBZC8gUj7mpaVgJZCoQOIiCXxp5bNPJSlpiRBhZAwuFYLJEnMJ8632rpVcFQAHvq5nGzHy3bXButbOO9zqHkYHzkR2XE7Ige";

            //var fb = (await user.GetIdentitiesAsync()).OfType<FacebookCredentials>().FirstOrDefault();
            //return fb.AccessToken;
        }

        public static string GetCurrentUserFacebookId(ServiceUser user, string fbAccessToken)
        {
            var fbClient = new FacebookClient(fbAccessToken);

            dynamic userInfo = fbClient.Get("me");
            return (string)userInfo.id;
        }
    }
}
