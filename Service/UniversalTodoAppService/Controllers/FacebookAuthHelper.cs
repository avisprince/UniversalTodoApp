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
            //return "CAACEdEose0cBAOIivZApQn02AGbqnFZCYNAn4CwASATSfS48sgFN6A3LNW8R9wQp8FxCjpr6U6tntkva5Bsu23QgQI4vc0JSOp1vYKm7rLoDZB5iZAezIZBWhz38zSZBkwASN7NpfLcvy6uzqRcIkZAZBxvzyeIKhisQtqZAwZCRIkf33Kcb8kGV2S4Alsf1FyPi0AAZBlqPigEnYHk4ZBp93z8g";

            var fb = (await user.GetIdentitiesAsync()).OfType<FacebookCredentials>().FirstOrDefault();
            return fb.AccessToken;
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
