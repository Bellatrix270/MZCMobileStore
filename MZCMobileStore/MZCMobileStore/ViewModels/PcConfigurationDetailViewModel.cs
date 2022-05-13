using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MZCMobileStore.Models;
using MZCMobileStore.Services;
using MZCMobileStore.ViewModels.Base;
using Xamarin.Forms;

namespace MZCMobileStore.ViewModels
{
    [QueryProperty(nameof(PcConfigurationId), nameof(PcConfigurationId))]
    public class PcConfigurationDetailViewModel : BaseViewModel
    {
        private int _pcConfigurationId;
        private PcConfiguration _selectedConfig;

        public PcConfiguration SelectedConfig
        {
            get => _selectedConfig;
            set => Set(ref _selectedConfig, value);
        }

        public int PcConfigurationId
        {
            get => _pcConfigurationId;
            set
            {
                _pcConfigurationId = value;
                LoadPcConfigurationId(value);
            }
        }

        public PcConfigurationDetailViewModel()
        {
            Title = "Подробности сборки";
        }

        public async void LoadPcConfigurationId(int configId)
        {
            try
            {
                var item = await new PcConfigurationWebRepository().GetByIdAsync(configId);
                SelectedConfig = item;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
