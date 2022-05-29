using System;
using System.Collections.Generic;
using System.Text;

namespace MZCMobileStore.Models.Interfaces
{
    public interface IStoreItem
    {
        public string Name { get; set; }
        public string CardDescription { get; }
        public byte[] MainImage { get; set; }
        public double Price { get; set; }
    }
}
