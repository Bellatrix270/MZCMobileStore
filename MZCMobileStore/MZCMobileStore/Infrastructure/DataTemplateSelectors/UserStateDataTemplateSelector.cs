using System;
using System.Collections.Generic;
using System.Text;
using MZCMobileStore.Models;
using Xamarin.Forms;

namespace MZCMobileStore.Infrastructure.DataTemplateSelectors
{
    public class UserStateDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate EmptyUserCart { get; set; }
        public DataTemplate UnRegisterUser { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container) 
        {
            if (!User.Instance.IsAuth)
                return UnRegisterUser;
            return EmptyUserCart;
        }
    }
}
