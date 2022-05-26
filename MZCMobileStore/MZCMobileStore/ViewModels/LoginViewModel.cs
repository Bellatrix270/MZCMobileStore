using System;
using System.Collections.Generic;
using System.Text;
using Acr.UserDialogs;
using MZCMobileStore.Models;
using MZCMobileStore.ViewModels.Base;
using MZCMobileStore.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MZCMobileStore.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IUserDialogs _userDialogs;

        public string UserPassword { get; set; }
        public string UserLogin { get; set; }
        public Command LoginCommand { get; }
        public Command GoToRegisterPageCommand { get; set; }

        public LoginViewModel()
        {
            _userDialogs = UserDialogs.Instance;

            LoginCommand = new Command(OnLoginClicked);
        }

        private async void OnLoginClicked(object obj)
        {
            bool isValid = await User.Instance.AuthorizationAsync(UserPassword, UserLogin);

            if (isValid)
                await Shell.Current.GoToAsync($"//{nameof(UserProfilePage)}");
            else
                _userDialogs.Alert("Неверный логин или пароль", "Авторизация");
        }
    }
}
