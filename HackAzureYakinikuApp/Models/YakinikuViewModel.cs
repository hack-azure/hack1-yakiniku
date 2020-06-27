using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackAzureYakinikuApp.Models
{
    public class YakinikuViewModel
    {
        public long Balance { get; set; }

        public long PriceH
        {
            get
            {
                return Balance;
            }
        }
        public long PriceL
        {
            get
            {
                return (long)(Balance * 0.7);
            }
        }
    }
}
