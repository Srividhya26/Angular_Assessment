using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace CustomerFeedbackDA.Model
{
    [Table("Product")]
    public partial class Product
    {
        public Product()
        {
            Feedbacks = new HashSet<Feedback>();
        }

        [Key]
        public int Id { get; set; }
        public int CategoryId { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedAt { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedAt { get; set; }

        [ForeignKey(nameof(CategoryId))]
        [InverseProperty("Products")]
        public virtual Category Category { get; set; }
        [InverseProperty(nameof(Feedback.Product))]
        public virtual ICollection<Feedback> Feedbacks { get; set; }
    }
}
