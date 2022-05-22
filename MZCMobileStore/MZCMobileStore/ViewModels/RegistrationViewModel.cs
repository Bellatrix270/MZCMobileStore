using System;
using System.Collections.Generic;
using System.Text;
using MZCMobileStore.Models;
using MZCMobileStore.ViewModels.Base;
using MZCMobileStore.Views;
using Xamarin.Forms;

namespace MZCMobileStore.ViewModels
{
    public class RegistrationViewModel : BaseViewModel
    {
        private string _userLogin;

        public string UserName
        {
            get; 
            set;
        }

        public string UserLogin
        {
            get => _userLogin;
            set
            {
                _userLogin = value;
                ContinueRegisterCommand?.ChangeCanExecute();
            }
        }

        public string UserNumberPhone
        {
            get; 
            set;
        }

        public string UserPassword
        {
            get; 
            set;
        }

        public string ConfirmUserPassword
        {
            get; 
            set;
        }

        public Command ContinueRegisterCommand { get; }
        public Command ContinueWithoutRegisterCommand { get; }
        public Command GoToLoginPageCommand { get; }

        public RegistrationViewModel()
        {
            Title = "Регистрация";

            UserName = string.Empty;
            UserLogin = string.Empty;
            UserNumberPhone = string.Empty;
            UserPassword = string.Empty;
            ConfirmUserPassword = string.Empty;

            ContinueWithoutRegisterCommand = new Command(async () => await Shell.Current.GoToAsync("//AboutPage"));
            GoToLoginPageCommand = new Command(async () => await Shell.Current.GoToAsync($"//{nameof(LoginPage)}"));

            ContinueRegisterCommand = new Command(OnExecuteContinueRegisterCommand, CanExecuteContinueRegisterCommand);
        }

        private async void OnExecuteContinueRegisterCommand(object parameter)
        {
            await User.Instance.RegistrationAsync();
        }

        private bool CanExecuteContinueRegisterCommand(object parameter)
        {
            return UserName.Length >= 2 &&
                   UserNumberPhone.Length > 7 &&
                   UserPassword == ConfirmUserPassword &&
                   User.CheckLoginToUnique(UserLogin).Result;
        }
    }
}
