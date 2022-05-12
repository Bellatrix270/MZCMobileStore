using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace MZCMobileStore.Infrastructure.Converters
{
    public class ByteArrayToImageSourсe : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(ImageSource))
                throw new ArgumentException("");

            if (value is byte[] byteArray)
            {
                return ImageSource.FromStream(() =>
                {
                    return new MemoryStream(byteArray);
                });
            }

            return BindableProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Not supported");
        }
    }
}
