using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MZCMobileStore.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserCartPage : ContentPage
    {
        public UserCartPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //TODO: Auto update list in viewModel
        }
    }
}