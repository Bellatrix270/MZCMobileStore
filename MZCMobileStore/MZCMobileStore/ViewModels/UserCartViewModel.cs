using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MZCMobileStore.Models;
using MZCMobileStore.Services.Interfaces;
using MZCMobileStore.ViewModels.Base;
using Xamarin.Forms;

namespace MZCMobileStore.ViewModels
{
    public class UserCartViewModel : BaseViewModel
    {
        private readonly IPcConfigurationRepository _pcConfigurationRepository;
        public List<PcConfiguration> UserCartItems { get; set; }
        public Command LoadingCartItemsCommand { get; }
        public Command AuthorizationCommand { get; set; }
        public Command RegisterCommand { get; set; }

        public UserCartViewModel(IPcConfigurationRepository pcConfigurationRepository)
        {
            Title = "Корзина";
            _pcConfigurationRepository = pcConfigurationRepository;

            LoadingCartItemsCommand = new Command(async () => await OnExecuteLoadingCartItemsCommand());

            UserCartItems = new List<PcConfiguration>();
        }

        private async Task OnExecuteLoadingCartItemsCommand()
        {
            if (!User.Instance.IsAuth)
            {
                IsBusy = false;
                return;
            }

            IsBusy = true;

            try
            {
                UserCartItems.Clear();

                UserCartItems = new List<PcConfiguration>
                {
                    new PcConfiguration() { Name = "MockNamePc", Processor = "Mock CPU", VideoCard = "Mock Videocart", Ram = "Mock RAM", Price = 100800},
                    new PcConfiguration() { Name = "MockNamePc", Processor = "Mock CPU", VideoCard = "Mock Videocart", Ram = "Mock RAM", Price = 210600}
                };

                foreach (var cartItem in User.Instance.CartItems)
                {
                    var item = await _pcConfigurationRepository.GetByIdAsync(cartItem.Key, false);
                    UserCartItems.Add(item);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
