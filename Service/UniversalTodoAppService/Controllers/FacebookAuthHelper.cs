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
            return "CAAGztpWeEdMBAOZAwXo32eH2sNmc97gKaiX6UENcwORH99bP4QgjhREU6H0ZAFyt9D6aPyTnhgw9b1QdN2q3rlmeCESPqqNoHxFYGyREsJRIoRTyQxG8IRH2aXPoD3OxPZAsp2MNyldQvxPVTKQ1D21fPvt6ZBwVO0ZAx4nFrWQybvH1UvFpWc1Jkle436FfdqGdJIpZCxRwZDZD";

            //var fb = (await user.GetIdentitiesAsync()).OfType<FacebookCredentials>().FirstOrDefault();
            //return fb.AccessToken;
        }

        public static string GetCurrentUserFacebookId(ServiceUser user, string fbAccessToken)
        {
            var fbClient = new FacebookClient(fbAccessToken);

            dynamic userInfo = fbClient.Get("me");
            return (string)userInfo.id;
        }

        public static string GetCurrentUserFacebookFriends(ServiceUser user, string fbAccessToken)
        {
            var fbClient = new FacebookClient(fbAccessToken);

            dynamic userInfo = fbClient.Get("me/friends");
            return string.Empty;
        }
    }
}
