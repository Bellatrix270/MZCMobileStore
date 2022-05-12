using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace MZCMobileStore.Infrastructure
{
    public class OnPropertyChangedClass : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #region Methods of calling the event PropertyChanged

        /// <summary>/Method for calling notification event about a change one property</summary>
        /// <param name="propertyName">changed property</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        /// <summary>Method for calling notification event about a change list of property</summary>
        /// <param name="propList">List of property names</param>
        protected virtual void OnPropertyChanged(IEnumerable<string> propList)
        {
            foreach (string propName in propList.Where(name => !string.IsNullOrWhiteSpace(name)))
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        /// <summary>Method for calling notification event about a change enumeration of property</summary>
        /// <param name="propList">List of property names</param>
        protected virtual void OnPropertyChanged(params string[] propList)
        {
            foreach (string propName in propList.Where(name => !string.IsNullOrEmpty(name)))
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        protected void OnAllPropertyChanged()
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));

        #endregion

        #region Methods for change value in property

        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;

            field = value;
            OnPropertyChanged(propertyName);

            return true;
        }

        #endregion
    }
}
