using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using MZCMobileStore.Services;

namespace MZCMobileStore.ViewModels.Base
{
    public class ViewModelLocator
    {
        public PcConfigurationsViewModel PcConfigurationsViewModel
        {
            get
            {
                PcConfigurationsViewModel viewModel = App.ContainerHost.Resolve<PcConfigurationsViewModel>();
                viewModel.OnAppearing();

                return viewModel;
            }
        }

        public PcConfigurationDetailViewModel PcConfigurationDetailViewModel =>
            App.ContainerHost.Resolve<PcConfigurationDetailViewModel>();
    }
}
