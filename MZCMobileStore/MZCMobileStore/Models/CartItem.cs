using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using MZCMobileStore.Infrastructure;
using MZCMobileStore.Models.Interfaces;

namespace MZCMobileStore.Models
{
    //public class CartItem
    //{
    //    public IStoreItem Item { get; set; }
    //    public int Count { get; set; }
    //    public double TotalPrice => Item.Price * Count;
    //    public bool IsSelected { get; set; }
    //}

    public class CartItem<TItem> : OnPropertyChangedClass where TItem : IStoreItem, IEntity
    {
        private int _count;
        public TItem Item { get; set; }

        public int Count
        {
            get => _count;
            set
            {
                _count = value;

                //if (value < 1)
                //    return;

                //int currentCount = User.Instance.CartItems[Item.Id];
                //User.Instance.CartItems.Remove(Item.Id);
                //User.Instance.CartItems.Add(Item.Id, ++currentCount);

                OnPropertyChanged(nameof(TotalPrice));
            }
        }

        public double TotalPrice => Item.Price * Count;
        public bool IsSelected { get; set; }
    }
}
