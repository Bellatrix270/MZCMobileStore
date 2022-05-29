using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Acr.UserDialogs;
using MZCMobileStore.Models;
using MZCMobileStore.Services;
using MZCMobileStore.Services.Interfaces;
using MZCMobileStore.ViewModels.Base;
using MZCMobileStore.Views;
using MZCMobileStore.Views.DetailsConfiguration;
using Xamarin.Forms;

namespace MZCMobileStore.ViewModels
{
    public class PcConfigurationsViewModel : BaseViewModel
    {
        private readonly IPcConfigurationRepository _pcConfigurationRepository;
        private PcConfiguration _selectedItem;
        private List<PcConfiguration> _pcConfigurations;
        private IUserDialogs _userDialogs;
        private bool _isBusy;
        private string _searchBarQuery;

        public override bool IsBusy
        {
            get => _isBusy;
            set
            {
                Set(ref _isBusy, value);
                SearchPcConfigCommand?.ChangeCanExecute();
            }
        }

        public IEnumerable<PcConfiguration> PcConfigurations
        {
            get
            {
                if (string.IsNullOrEmpty(_searchBarQuery))
                    return _pcConfigurations.Take(_pcConfigurations.Count);
                return _pcConfigurations.Where(config => config.Name == _searchBarQuery);
            }
        }

        public Command LoadConfigurationsCommand { get; }
        public Command<PcConfiguration> AddToCardCommand { get; }
        public Command<string> SearchPcConfigCommand { get; }
        public Command<PcConfiguration> ConfigurationTapped { get; }

        public PcConfigurationsViewModel(IPcConfigurationRepository pcConfigurationRepository)
        {
            Title = "Конфигурации ПК";

            _userDialogs = UserDialogs.Instance;

            _pcConfigurationRepository = pcConfigurationRepository;
            _pcConfigurations = new List<PcConfiguration>();

            LoadConfigurationsCommand = new Command(async () => await ExecuteLoadConfigurationsCommand());
            AddToCardCommand = new Command<PcConfiguration>(async (config) =>  await OnExecuteAddToCartCommand(config));
            SearchPcConfigCommand = new Command<string>(OnExecuteSearchPcConfigCommand, p => !IsBusy);
            ConfigurationTapped = new Command<PcConfiguration>(OnConfigurationSelected, (sender) => !IsBusy);
        }

        private async Task ExecuteLoadConfigurationsCommand()
        {
            IsBusy = true;

            try
            {
                _pcConfigurations.Clear();

                _pcConfigurations.Add(new PcConfiguration() { Name = "MockNamePc", ShortDescription = "Mock short descriprtion about this pc configuration" });
                _pcConfigurations.Add(new PcConfiguration() { Name = "MockNamePc", ShortDescription = "Mock short descriprtion about this pc configuration" });

                var items = await _pcConfigurationRepository.GeAllAsync();
                _pcConfigurations.Clear();

                foreach (var item in items)
                {
                    _pcConfigurations.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                _userDialogs.Toast("Ошибка загрузки, повторите позже");
            }
            finally
            {
                IsBusy = false;
                OnPropertyChanged(nameof(PcConfigurations));
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public PcConfiguration SelectedItem
        {
            get => _selectedItem;
            set
            {
                Set(ref _selectedItem, value);
                OnConfigurationSelected(value);
            }
        }

        private async void OnConfigurationSelected(PcConfiguration config)
        {
            if (config == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(PcConfigurationDetailViewModel.PcConfigurationId)}={config.Id}");
        }

        private void OnExecuteSearchPcConfigCommand(string parameter)
        {
            _searchBarQuery = parameter;
            OnPropertyChanged(nameof(PcConfigurations));
        }

        private async Task OnExecuteAddToCartCommand(PcConfiguration config)
        {
            if (User.Instance.IsAuth)
            {
                int currentCount = 0;

                if (User.Instance.CartItems.ContainsKey(config.Id))
                {
                    currentCount = User.Instance.CartItems[config.Id];
                    User.Instance.CartItems.Remove(config.Id);
                }

                User.Instance.CartItems.Add(config.Id, ++currentCount);

                _userDialogs.Toast($"Конфигураця {config.Name} добавлена в корзину X{currentCount}", TimeSpan.FromSeconds(0.5));
            }
            else
            {
                bool resultDialog = await _userDialogs.ConfirmAsync("Необходимо зарегистрироваться", 
                    "Добавление конфигурации в корзину", "Зарегистрироваться", "Отмена");

                if (resultDialog)
                    await Shell.Current.GoToAsync("//RegistrationPage");
            }
        }
    }
}