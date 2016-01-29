using Facebook;
using ToDoAppWin10.Models;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace ToDoAppWin10.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Login : Page
    {
        public Login()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var loginSuccessful = AuthenticationHelper.LoginWithCookie();
            if (loginSuccessful)
            {
                #pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => this.LoadDataAndNavigateToApp());
                #pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            }
        }

        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            var loginSuccessful = await AuthenticationHelper.LoginAsync();
            if (loginSuccessful)
            {
                this.LoadDataAndNavigateToApp();
            }
        }

        private async void LoadDataAndNavigateToApp()
        {
            // load data here
            await DataManager.LoadData();

            // navigate to home page
            Frame.Navigate(typeof(SelectToDo));
        }
    }
}
