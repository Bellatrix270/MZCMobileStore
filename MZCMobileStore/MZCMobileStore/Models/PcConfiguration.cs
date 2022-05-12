using System;
using System.Collections.Generic;
using System.Text;

namespace MZCMobileStore.Models
{
    public class PcConfiguration
    { 
        public int Id { get; set; }
        public string Name { get; set; }
        public string Processor { get; set; }
        public string VideoCard { get; set; }
        public string Ram { get; set; }
        public string MotherBoard { get; set; }
        public byte[] MainImage { get; set; }
        public string[] AdditionalImageFtpPaths { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }

        public string CardDescription
        {
            get
            {
                if (ShortDescription != null)
                    return ShortDescription;

                string cardDesc = $"{Processor}\n{VideoCard}\n{Ram}";

                return cardDesc;
            }
        }
        public double Price { get; set; }
    }
}
