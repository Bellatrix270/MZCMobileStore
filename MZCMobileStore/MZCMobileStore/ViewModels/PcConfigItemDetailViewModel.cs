using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MZCMobileStore.Models;
using MZCMobileStore.Services;
using MZCMobileStore.ViewModels.Base;
using Xamarin.Forms;

namespace MZCMobileStore.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class PcConfigItemDetailViewModel : BaseViewModel
    {
        private string itemId;
        private string text;
        private string description;
        public string Id { get; set; }

        public string Text
        {
            get => text;
            set => Set(ref text, value);
        }

        public string Description
        {
            get => description;
            set => Set(ref description, value);
        }

        public string ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItemId(value);
            }
        }

        public async void LoadItemId(string itemId)
        {
            try
            {
                var item = await new PcConfigurationWebRepository().GetByIdAsync(1);
                Description = item.Description;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
