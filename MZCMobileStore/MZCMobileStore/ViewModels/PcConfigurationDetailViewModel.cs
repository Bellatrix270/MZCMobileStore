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
        private int _idSelectedImage;

        public Command ChangeImageCommand { get; }

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

        public byte[] SelectedImage =>
            (SelectedConfig != null) ? SelectedConfig.AdditionalImages[_idSelectedImage] : Array.Empty<byte>();

        public PcConfigurationDetailViewModel()
        {
            Title = "Подробности сборки";
            ChangeImageCommand = new Command(ExecuteChangeImageCommand, (sender) => !IsBusy);
        }

        public async void LoadPcConfigurationId(int configId)
        {
            IsBusy = true;

            try
            {
                SelectedConfig = new PcConfiguration() { Name = "MockName" };
                var item = await new PcConfigurationWebRepository().GetByIdAsync(configId, true);
                SelectedConfig = item;
                OnPropertyChanged(nameof(SelectedImage));
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void ExecuteChangeImageCommand(object parameter)
        {
            int newIdSelectedImage = _idSelectedImage + Convert.ToInt32(parameter);

            if (newIdSelectedImage < _selectedConfig.AdditionalImages.Length && newIdSelectedImage >= 0)
            {
                _idSelectedImage = newIdSelectedImage;
                OnPropertyChanged(nameof(SelectedImage));
            }
        }
    }
}
