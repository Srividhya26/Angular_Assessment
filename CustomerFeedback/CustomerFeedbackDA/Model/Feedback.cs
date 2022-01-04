using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace CustomerFeedbackDA.Model
{
    [Table("Feedback")]
    public partial class Feedback
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(10)]
        public string FeedbackNumber { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int ReviewId { get; set; }
        [StringLength(50)]
        public string LastPurchasedItem { get; set; }
        [Required]
        [StringLength(200)]
        public string ProductFeedback { get; set; }
        [StringLength(200)]
        public string ReasonForUnsatisfaction { get; set; }
        [StringLength(200)]
        public string FileUpload { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedAt { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedAt { get; set; }

        [ForeignKey(nameof(ProductId))]
        [InverseProperty("Feedbacks")]
        public virtual Product Product { get; set; }
        [ForeignKey(nameof(ReviewId))]
        [InverseProperty("Feedbacks")]
        public virtual Review Review { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(UserDetail.Feedbacks))]
        public virtual UserDetail User { get; set; }
    }
}
