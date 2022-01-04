using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace CustomerFeedbackDA.Model
{
    [Table("UserDetail")]
    public partial class UserDetail
    {
        public UserDetail()
        {
            Feedbacks = new HashSet<Feedback>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(10)]
        public string Title { get; set; }
        [Required]
        [StringLength(150)]
        public string FirstName { get; set; }
        [StringLength(10)]
        public string Initial { get; set; }
        [Required]
        [StringLength(100)]
        public string Email { get; set; }
        [Required]
        [StringLength(100)]
        public string StreetAddress { get; set; }
        [StringLength(100)]
        public string StreetAddress2 { get; set; }
        [Required]
        [StringLength(50)]
        public string City { get; set; }
        [Required]
        [StringLength(50)]
        public string Region { get; set; }
        [Required]
        [StringLength(10)]
        public string PostalCode { get; set; }
        [Required]
        [StringLength(50)]
        public string Country { get; set; }

        [InverseProperty(nameof(Feedback.User))]
        public virtual ICollection<Feedback> Feedbacks { get; set; }
    }
}
