using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ShoppingWebsiteDA.Model
{
    [Table("Image")]
    public partial class Image
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Version { get; set; }
        [StringLength(250)]
        public string ImageUrl { get; set; }
        [Required]
        public byte[] UploadedAt { get; set; }

        [ForeignKey(nameof(ProductId))]
        [InverseProperty("Images")]
        public virtual Product Product { get; set; }
    }
}
