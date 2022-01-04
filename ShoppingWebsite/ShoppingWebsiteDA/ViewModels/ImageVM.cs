using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingWebsiteDA.ViewModels
{
    public class ImageVM
    {
        //[Required(ErrorMessage = "Product Id is required")]
        public int ProductId { get; set; }
        //[Required(ErrorMessage = "File Name is required")]
        public IFormFile FileName { get; set; }
    }
}
