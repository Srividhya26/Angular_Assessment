using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerFeedbackBL.ViewModels
{
    public class FeedbackVM
    {
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Firstame is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Initial is required")]
        public string Initial { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Street Address is required")]
        public string StreetAddress { get; set; }
        public string StreetAddress2 { get; set; }
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }
        [Required(ErrorMessage = "Region is required")]
        public string Region { get; set; }
        [Required(ErrorMessage = "Postal code is required")]
        public string PostalCode { get; set; }
        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; }
        [Required(ErrorMessage = "Product id is required")]
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Review id is required")]
        public int ReviewId { get; set; }
        public string LastPurchasedItem { get; set; }
        [Required(ErrorMessage = "Product feedback is required")]
        public string ProductFeedback { get; set; }
        public string ReasonForUnSatisfaction { get; set; }
        public IFormFile FileUpload { get; set; }
    }
}
