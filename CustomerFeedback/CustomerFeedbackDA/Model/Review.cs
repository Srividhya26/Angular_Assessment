using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace CustomerFeedbackDA.Model
{
    [Table("Review")]
    public partial class Review
    {
        public Review()
        {
            Feedbacks = new HashSet<Feedback>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedAt { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedAt { get; set; }

        [InverseProperty(nameof(Feedback.Review))]
        public virtual ICollection<Feedback> Feedbacks { get; set; }
    }
}
