using System;
using System.Collections.Generic;
using System.Text;
using MZCMobileStore.Views;
using Xamarin.Forms;

namespace MZCMobileStore.ViewModels
{
    public class RegistrationViewModel
    {
        public string UserName { get; set; }
        public string UserLogin { get; set; }
        public string UserNumberPhone { get; set; }
        public string UserPassword { get; set; }
        public string ConfirmUserPassword { get; set; }

        public Command ContinueRegisterCommand { get; }
        public Command ContinueWithoutRegisterCommand { get; }
        public Command GoToLoginPageCommand { get; }

        public RegistrationViewModel()
        {
            ContinueWithoutRegisterCommand = new Command(WithoutRegister);
            string name = App.Current.Properties["name"].ToString();
        }

        private async void WithoutRegister(object parameter)
        {
            await Shell.Current.GoToAsync("//AboutPage");
        }

    }
}
