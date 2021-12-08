using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingWebsiteBL.ViewModel
{
    public class ProductVM
    {
        [Required]
        public string ProductName { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        [Required]
        public int BrandId { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
    }
}
