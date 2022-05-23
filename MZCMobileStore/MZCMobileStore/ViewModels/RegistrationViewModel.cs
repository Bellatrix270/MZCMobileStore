using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using MZCMobileStore.Infrastructure;
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

        public Command<string> ValidatePasswordCommand { get; }
        public Func<string, LevelsPasswordSecurityEnum> ValidatePasswordPredicate { get; }

        #region Regex

        private readonly Regex _hasNumber = new Regex(@"[0-9]+");
        private readonly Regex _hasUpperChar = new Regex(@"[A-Z]+");
        private readonly Regex _hasMiniMaxChars = new Regex(@".{6,21}");
        private readonly Regex _hasLowerChar = new Regex(@"[a-z]+");
        private readonly Regex _hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

        #endregion

        public RegistrationViewModel()
        {
            Title = "Регистрация";

            ContinueWithoutRegisterCommand = new Command(async () => await Shell.Current.GoToAsync("//AboutPage"));
            GoToLoginPageCommand = new Command(async () => await Shell.Current.GoToAsync($"//{nameof(LoginPage)}"));

            ContinueRegisterCommand = new Command(OnExecuteContinueRegisterCommand, CanExecuteContinueRegisterCommand);

            ValidatePasswordPredicate = PasswordValidate;
        }

        protected override bool Set<T>(ref T field, T value, string propertyName = null)
        {
            bool isChange =  base.Set(ref field, value, propertyName);
            ContinueRegisterCommand?.ChangeCanExecute();
            return isChange;
        }

        private async void OnExecuteContinueRegisterCommand(object parameter)
        {
            //TODO: Check Login to Unique here.
            await User.Instance.RegistrationAsync(UserName, UserNumberPhone, UserPassword, UserLogin);
        }

        private bool CanExecuteContinueRegisterCommand(object parameter)
            => UserName.Length >= 2 &&
               UserNumberPhone.Length > 7 &&
               PasswordValidate(UserPassword) >= LevelsPasswordSecurityEnum.Medium &&
               UserPassword == ConfirmUserPassword;

        public LevelsPasswordSecurityEnum PasswordValidate(string password)
        {
            LevelsPasswordSecurityEnum passwordLevelsSecurity = LevelsPasswordSecurityEnum.Indefinite;

            if (!_hasMiniMaxChars.IsMatch(password))
                passwordLevelsSecurity = LevelsPasswordSecurityEnum.Low;

            if (_hasNumber.IsMatch(password) && _hasLowerChar.IsMatch(password) &&
                _hasUpperChar.IsMatch(password))
            {
                passwordLevelsSecurity = LevelsPasswordSecurityEnum.Medium;

                if (_hasSymbols.IsMatch(password))
                    passwordLevelsSecurity = LevelsPasswordSecurityEnum.High;
            }
            else
                passwordLevelsSecurity = LevelsPasswordSecurityEnum.Low;

            return passwordLevelsSecurity;
        }
    }
}
