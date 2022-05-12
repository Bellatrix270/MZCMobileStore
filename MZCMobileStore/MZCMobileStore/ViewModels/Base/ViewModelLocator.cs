using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using MZCMobileStore.Services;

namespace MZCMobileStore.ViewModels.Base
{
    public class ViewModelLocator
    {
        public PcConfigItemsViewModel PcConfigItemsViewModel => App.ContainerHost.Resolve<PcConfigItemsViewModel>();
    }
}
