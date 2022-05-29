using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Xaml;

namespace MZCMobileStore.Infrastructure.MarkupExtensions
{
    public class GenericType : IMarkupExtension
    {
        public GenericType() { }

        public GenericType(Type baseType, Type innerType)
        {
            BaseType = baseType;
            InnerType = innerType;
        }

        public Type BaseType { get; set; }

        public Type InnerType { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            Type result = BaseType.MakeGenericType(InnerType);
            return result;
        }
    }
}
