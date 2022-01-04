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
        [Required(ErrorMessage = "Product Name is required")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "Category Id is required")]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        [Required(ErrorMessage = "Brand Id is required")]
        public int BrandId { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Stock is required")]
        public int Stock { get; set; }
    }
}
