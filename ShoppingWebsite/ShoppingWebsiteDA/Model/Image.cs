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
        public int? Version { get; set; }
        [StringLength(200)]
        public string ImageName { get; set; }
        [StringLength(200)]
        public string FileName { get; set; }

        [ForeignKey(nameof(ProductId))]
        [InverseProperty("Images")]
        public virtual Product Product { get; set; }
    }
}
