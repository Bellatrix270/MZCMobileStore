using System;
using Autofac;
using MZCMobileStore.Services;
using MZCMobileStore.Services.Interfaces;
using MZCMobileStore.ViewModels;
using MZCMobileStore.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MZCMobileStore
{
    public partial class App : Application
    {
        private static IContainer _containerHost;
        public static IContainer ContainerHost => _containerHost ?? (_containerHost = ConfigureContainer());

        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {

        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        private static IContainer ConfigureContainer()
        {
            ContainerBuilder builder = new ContainerBuilder();

            builder.RegisterType<PcConfigurationWebRepository>().As<IPcConfigurationRepository>();

            builder.RegisterType<PcConfigurationsViewModel>();
            builder.RegisterType<PcConfigurationDetailViewModel>();
            //builder.RegisterType<PcConfigurationDetailViewModel>().AsSelf().SingleInstance();

            return builder.Build();
        }
    }
}
