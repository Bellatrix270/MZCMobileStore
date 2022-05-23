using System;
using System.Collections.Generic;
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

            if (!Application.Current.Properties.TryGetValue("isFirstOpen", out _))
            {
                CurrentItem.CurrentItem = ContentRegistrationPage;
                Application.Current.Properties.Add("isFirstOpen", "false");
                Application.Current.SavePropertiesAsync();
            }
        }
    }
}
