using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using MZCMobileStore.Models;
using MZCMobileStore.Models.ExtensionGenerics;
using MZCMobileStore.Services.Interfaces;
using MZCMobileStore.ViewModels.Base;
using Xamarin.Forms;

namespace MZCMobileStore.ViewModels
{
    public class UserCartViewModel : BaseViewModel
    {
        private readonly IPcConfigurationRepository _pcConfigurationRepository;
        private readonly IUserDialogs _userDialogs;

        public ObservableCollection<PcConfigCartItem> UserCartItems { get; set; }

        public Command LoadingCartItemsCommand { get; }
        public Command AuthorizationCommand { get; set; }
        public Command RegisterCommand { get; set; }
        public Command ItemCountChangedCommand { get; set; }

        public UserCartViewModel(IPcConfigurationRepository pcConfigurationRepository)
        {
            Title = "Корзина";
            _pcConfigurationRepository = pcConfigurationRepository;
            _userDialogs = UserDialogs.Instance;

            LoadingCartItemsCommand = new Command(async () => await OnExecuteLoadingCartItemsCommand());

            ItemCountChangedCommand = new Command(ExecuteItemCountChangedCommand);

            UserCartItems = new ObservableCollection<PcConfigCartItem>();
        }

        //Parameter is object[]. CurrentCount passed from stepper control
        private void ExecuteItemCountChangedCommand(object parameter)
        {
            var values = (object[])parameter;
            var currentItem = (PcConfiguration)values[0];
            var currentCount = Convert.ToInt32(values[1]);

            User.Instance.CartItems.Remove(currentItem.Id);
            User.Instance.CartItems.Add(currentItem.Id, currentCount);
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

                #region MockData
                var cartItemFirst = new PcConfigCartItem()
                {
                    Item = new PcConfiguration()
                    {
                        Name = "MockNamePc", Processor = "Mock CPU", VideoCard = "Mock Videocart", Ram = "Mock RAM",
                        Price = 210600
                    },
                };

                var cartItemSecond = new PcConfigCartItem()
                {
                    Item = new PcConfiguration()
                    {
                        Name = "MockNamePc", Processor = "Mock CPU", VideoCard = "Mock Videocart", Ram = "Mock RAM",
                        Price = 210600
                    },
                };

                UserCartItems.Add(cartItemFirst);
                UserCartItems.Add(cartItemSecond);
                #endregion

                var items = new List<PcConfigCartItem>();

                foreach (var cartItem in User.Instance.CartItems)
                {
                    var item = await _pcConfigurationRepository.GetByIdAsync(cartItem.Key, false);
                    items.Add(new PcConfigCartItem(){Item = item, Count = cartItem.Value});
                }

                UserCartItems.Clear();

                foreach (var item in items)
                {
                    UserCartItems.Add(item);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                _userDialogs.Toast("Ошибка загрузки, повторите позже");
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
