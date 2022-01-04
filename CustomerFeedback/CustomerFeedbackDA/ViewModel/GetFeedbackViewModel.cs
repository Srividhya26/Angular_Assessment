using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerFeedbackDA.ViewModel
{
    public class GetFeedbackViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
  
        public string FirstName { get; set; }

        public string Initial { get; set; }

        public string Email { get; set; }

        public string StreetAddress { get; set; }
        public string StreetAddress2 { get; set; }
   
        public string City { get; set; }
 
        public string Region { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }

        public int ProductId { get; set; }

        public int ReviewId { get; set; }
        public string LastPurchasedItem { get; set; }

        public string ProductFeedback { get; set; }
        public string ReasonForUnSatisfaction { get; set; }
        public string FileUpload { get; set; }
    }
}
