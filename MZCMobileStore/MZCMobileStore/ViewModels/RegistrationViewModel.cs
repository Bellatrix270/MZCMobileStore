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
        #region Fields

        private string _userLogin;
        private string _userName;
        private string _userNumberPhone;
        private string _userPassword;
        private string _confirmUserPassword;

        #endregion

        #region Properties

        public string UserName
        {
            get => _userName;
            set => Set(ref _userName, value);
        }

        public string UserLogin
        {
            get => _userLogin;
            set => Set(ref _userLogin, value);
        }

        public string UserNumberPhone
        {
            get => _userNumberPhone;
            set => Set(ref _userNumberPhone, value);
        }

        public string UserPassword
        {
            get => _userPassword;
            set => Set(ref _userPassword, value);
        }

        public string ConfirmUserPassword
        {
            get => _confirmUserPassword;
            set => Set(ref _confirmUserPassword, value);
        }

        #endregion

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

        protected override bool Set<T>(ref T field, T value, string propertyName = null)
        {
            bool isChange =  base.Set(ref field, value, propertyName);
            ContinueRegisterCommand?.ChangeCanExecute();
            return isChange;
        }

        private async void OnExecuteContinueRegisterCommand(object parameter)
        {
            await User.Instance.RegistrationAsync();
        }

        private bool CanExecuteContinueRegisterCommand(object parameter)
            => UserName.Length >= 2 &&
                   UserNumberPhone.Length > 7 &&
                   (UserPassword == ConfirmUserPassword && UserPassword.Length >= 5) &&
                   User.CheckLoginToUnique(UserLogin).Result;
    }
}
