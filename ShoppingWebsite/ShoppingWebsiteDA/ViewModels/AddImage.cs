using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingWebsiteDA.ViewModels
{
    public class AddImage
    {
        public int ProductId { get; set; }
        public int Version { get; set; }
        public string ImageUrl { get; set; }
        public string FileName { get; set; }
    }
}
