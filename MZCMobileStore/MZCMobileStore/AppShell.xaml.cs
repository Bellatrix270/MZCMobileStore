using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MZCMobileStore.Models;
using MZCMobileStore.ViewModels;
using MZCMobileStore.Views;
using MZCMobileStore.Views.DetailsConfiguration;
using Xamarin.Forms;

namespace MZCMobileStore
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(ConfirmPhoneNumberPage), typeof(ConfirmPhoneNumberPage));

            LoadSavedDataAsync();
        }

        private async Task LoadSavedDataAsync()
        {
            if (!Application.Current.Properties.TryGetValue("isFirstOpen", out _))
            {
                CurrentItem = ContentRegistrationPage;
                Application.Current.Properties.Add("isFirstOpen", "false");
                await Application.Current.SavePropertiesAsync().ConfigureAwait(true);
            }
            else if (Application.Current.Properties.TryGetValue("user", out object userData))
            {
                string[] user = (string[])userData;
                await User.Instance.AuthorizationAsync(user[0], user[1]).ConfigureAwait(true);
            }
        }
    }
}
