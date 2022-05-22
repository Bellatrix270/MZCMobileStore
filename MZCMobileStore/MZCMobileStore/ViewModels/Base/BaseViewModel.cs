using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MZCMobileStore.Infrastructure;

namespace MZCMobileStore.ViewModels.Base
{
    public class BaseViewModel : OnPropertyChangedClass
    {
        private bool _isBusy = false;
        private string _title = string.Empty;

        public virtual bool IsBusy
        {
            get => _isBusy;
            set => Set(ref _isBusy, value);
        }

        public virtual string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }
    }
}
