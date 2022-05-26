using System;
using System.Collections.Generic;
using System.Text;
using Acr.UserDialogs;
using MZCMobileStore.Models;
using MZCMobileStore.ViewModels.Base;
using MZCMobileStore.Views;
using Xamarin.Forms;

namespace MZCMobileStore.ViewModels
{
    //nameof(ConfirmPhoneNumberPage)}?{nameof(ConfirmPhoneNumberViewModel.UserPhoneNumber)}={config.Id};
    [QueryProperty(nameof(UserPhoneNumber), nameof(UserPhoneNumber))]
    public  class ConfirmPhoneNumberViewModel : BaseViewModel
    {
        private string _phoneNumberCode;
        private string _userPhoneNumber;

        public string TitleDescription { get; set; }

        public string UserPhoneNumber
        {
            get => _userPhoneNumber;
            set
            {
                _userPhoneNumber = value;
                TitleDescription = TitleDescription = $"на номер {value} отправлен код подтверждения";
                OnPropertyChanged(nameof(TitleDescription));
            }
        }

        public string PhoneNumberCode
        {
            get => _phoneNumberCode;
            set
            {
                _phoneNumberCode = value;
                ConfirmPhoneNumberCommand?.ChangeCanExecute();
            }
        }

        public Command ConfirmPhoneNumberCommand { get; }

        public ConfirmPhoneNumberViewModel()
        {
            Title = "Подтверждения телефона";
            ConfirmPhoneNumberCommand = new Command(OnExecuteConfirmPhoneNumberCommand, CanExecuteConfirmPhoneNumberCommand);
        }

        private async void OnExecuteConfirmPhoneNumberCommand(object parameter)
        {
            using (var progress = UserDialogs.Instance.Loading("Загрузка..."))
            {
                progress.PercentComplete = +15;
                if (await User.Instance.RegistrationConfirmAsync(UserPhoneNumber, PhoneNumberCode))
                    await Shell.Current.GoToAsync("//AboutPage");
                else
                    UserDialogs.Instance.Alert("Неверный код подтверждения", "Ошибка авторизации");
            }
        }

        private bool CanExecuteConfirmPhoneNumberCommand(object parameter)
        {
            return PhoneNumberCode.Length == 6;
        }
    }
}
