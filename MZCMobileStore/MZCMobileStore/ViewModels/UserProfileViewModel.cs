using System;
using System.Windows.Input;
using MZCMobileStore.ViewModels.Base;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MZCMobileStore.ViewModels
{
    public class UserProfileViewModel : BaseViewModel
    {
        public UserProfileViewModel()
        {
            Title = "Профиль";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
        }

        public ICommand OpenWebCommand { get; }
    }
}