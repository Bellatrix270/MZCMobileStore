using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MZCMobileStore.Models;
using Xamarin.Forms;

namespace MZCMobileStore.Views.SearchHandlers
{
    class PcConfigurationSearchHandler : SearchHandler
    {
        public static readonly BindableProperty InputPcConfigurationProperty =
            BindableProperty.Create(nameof(InputPcConfiguration), typeof(IList<PcConfiguration>),
                typeof(PcConfigurationSearchHandler), default);
        public IList<PcConfiguration> InputPcConfiguration { get; set; }
        protected override void OnQueryChanged(string oldValue, string newValue)
        {
            base.OnQueryChanged(oldValue, newValue);

            if (string.IsNullOrWhiteSpace(newValue))
            {
                ItemsSource = null;
            }
            else
            {
                ItemsSource = InputPcConfiguration
                    .Where(config => config.Name.ToLower().Contains(newValue.ToLower()))
                    .ToList<PcConfiguration>();
            }
        }
    }
}
