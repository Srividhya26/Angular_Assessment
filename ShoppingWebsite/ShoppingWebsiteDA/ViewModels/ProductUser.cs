﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingWebsiteDA.ViewModels
{
    public class ProductUser
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public string BrandName { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; }
    }
}
