using System.ComponentModel;
using MZCMobileStore.ViewModels;
using Xamarin.Forms;

namespace MZCMobileStore.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new PcConfigItemDetailViewModel();
        }
    }
}