using System;
using System.Collections.Generic;
using System.Text;
using MZCMobileStore.Models;
using MZCMobileStore.Services.Interfaces;
using MZCMobileStore.ViewModels.Base;
using Xamarin.Forms;

namespace MZCMobileStore.ViewModels
{
    public class UserCartViewModel : BaseViewModel
    {
        private readonly IPcConfigurationRepository _pcConfigurationRepository;
        public List<PcConfiguration> CartItems { get; set; }
        public Command LoadCartItemsCommand { get; }
        public Command AuthorizationCommand { get; set; }
        public Command RegisterCommand { get; set; }

        public UserCartViewModel(IPcConfigurationRepository pcConfigurationRepository)
        {
            Title = "Корзина";
            _pcConfigurationRepository = pcConfigurationRepository;

            //CartItems = new List<PcConfiguration>
            //{
            //    new PcConfiguration() { Name = "MockNamePc", Processor = "Mock CPU", VideoCard = "Mock Videocart", Ram = "Mock RAM", Price = 100800},
            //    new PcConfiguration() { Name = "MockNamePc", Processor = "Mock CPU", VideoCard = "Mock Videocart", Ram = "Mock RAM", Price = 210600}
            //};
        }
    }
}
