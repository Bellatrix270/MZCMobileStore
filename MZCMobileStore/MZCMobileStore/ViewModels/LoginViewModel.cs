using System;
using System.Collections.Generic;
using System.Text;
using MZCMobileStore.Models;
using MZCMobileStore.ViewModels.Base;
using MZCMobileStore.Views;
using Xamarin.Forms;

namespace MZCMobileStore.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public string UserPassword { get; set; }
        public string UserLogin { get; set; }
        public Command LoginCommand { get; }
        public Command GoToRegisterPageCommand { get; set; }

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
        }

        private async void OnLoginClicked(object obj)
        {
            await User.Instance.AuthorizationAsync(UserPassword, UserLogin);
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
        }
    }
}
