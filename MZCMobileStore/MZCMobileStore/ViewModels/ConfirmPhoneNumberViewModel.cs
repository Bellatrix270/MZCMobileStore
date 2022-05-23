using System;
using System.Collections.Generic;
using System.Text;
using MZCMobileStore.ViewModels.Base;
using Xamarin.Forms;

namespace MZCMobileStore.ViewModels
{
    //nameof(ConfirmPhoneNumberPage)}?{nameof(ConfirmPhoneNumberViewModel.UserPhoneNumber)}={config.Id};
    [QueryProperty(nameof(UserPhoneNumber), nameof(UserPhoneNumber))]
    public  class ConfirmPhoneNumberViewModel : BaseViewModel
    {
        public string UserPhoneNumber { get; set; }
    }
}
