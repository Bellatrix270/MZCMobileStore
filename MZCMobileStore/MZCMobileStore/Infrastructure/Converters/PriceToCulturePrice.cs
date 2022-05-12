using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace MZCMobileStore.Infrastructure.Converters
{
    public class PriceToCulturePrice : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType == typeof(string))
            {
                if (culture.Name.Contains("en"))
                    return (System.Convert.ToDouble(value) / 60).ToString("##.#") + "$";

                if(culture.Name.Contains("ru"))
                    return $"{value}₽";

                throw new NotSupportedException("Not supported currency");
            }

            return BindableProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Not supported");
        }
    }
}
