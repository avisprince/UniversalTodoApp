using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using ToDoAppWin10.Views;
using Windows.Security.Credentials;

namespace ToDoAppWin10.Models
{
    public static class AuthenticationHelper
    {
        private static MobileServiceUser user;

        private static readonly string provider = "Facebook";

        private static PasswordVault vault = new PasswordVault();

        public static async Task<bool> LoginAsync()
        {
            try
            {
                user = await App.MobileService.LoginAsync(provider);

                // Create and store the user credentials.
                var credential = new PasswordCredential(provider, user.UserId, user.MobileServiceAuthenticationToken);

                vault.Add(credential);

                return true;
            }
            catch (Exception e)
            {
            }

            return false;
        }

        public static bool LoginWithCookie()
        {
            PasswordCredential credential = null;

            try
            {
                // Try to get an existing credential from the vault.
                credential = vault.FindAllByResource(provider).FirstOrDefault();
            }
            catch (Exception)
            {
                // When there is no matching resource an error occurs, which we ignore.
            }

            if (credential != null)
            {
                // Create a user from the stored credentials.
                user = new MobileServiceUser(credential.UserName);
                credential.RetrievePassword();
                user.MobileServiceAuthenticationToken = credential.Password;

                // Set the user from the stored credentials.
                App.MobileService.CurrentUser = user;

                // Consider adding a check to determine if the token is 
                // expired, as shown in this post: http://aka.ms/jww5vp.

                return true;
            }

            return false;
        }

        public static void Logout()
        {
            App.MobileService.Logout();
            App.MobileService.CurrentUser = null;

            // Try to get an existing credential from the vault.
            var credential = vault.FindAllByResource(provider).FirstOrDefault();

            vault.Remove(credential);
        }
    }
}
