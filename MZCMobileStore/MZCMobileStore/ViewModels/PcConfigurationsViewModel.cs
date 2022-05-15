using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
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

        public ObservableCollection<PcConfiguration> PcConfigurations { get; }
        public Command LoadConfigurationsCommand { get; }
        public Command<PcConfiguration> ConfigurationTapped { get; }

        public PcConfigurationsViewModel(IPcConfigurationRepository pcConfigurationRepository)
        {
            Title = "Конфигурации ПК";

            _pcConfigurationRepository = pcConfigurationRepository;
            PcConfigurations = new ObservableCollection<PcConfiguration>();

            LoadConfigurationsCommand = new Command(async () => await ExecuteLoadConfigurationsCommand());
            ConfigurationTapped = new Command<PcConfiguration>(OnConfigurationSelected, (sender) => !IsBusy);
        }

        private async Task ExecuteLoadConfigurationsCommand()
        {
            IsBusy = true;

            try
            {
                PcConfigurations.Clear();
                PcConfigurations.Add(new PcConfiguration() { Name = "MockNamePc", ShortDescription = "Mock short descriprtion about this pc configuration" });
                PcConfigurations.Add(new PcConfiguration() { Name = "MockNamePc", ShortDescription = "Mock short descriprtion about this pc configuration" });
                var items = await _pcConfigurationRepository.GeAllAsync();
                PcConfigurations.Clear();
                foreach (var item in items)
                {
                    PcConfigurations.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
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

        async void OnConfigurationSelected(PcConfiguration config)
        {
            if (config == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(PcConfigurationDetailViewModel.PcConfigurationId)}={config.Id}");
        }
    }
}