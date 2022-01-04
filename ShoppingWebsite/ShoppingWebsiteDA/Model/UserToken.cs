using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ShoppingWebsiteDA.Model
{
    [Table("UserToken")]
    public partial class UserToken
    {
        [Key]
        public Guid UserId { get; set; }
        [Key]
        public string LoginProvider { get; set; }
        [Required]
        [StringLength(450)]
        public string Name { get; set; }
        [Key]
        public string Value { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty("UserTokens")]
        public virtual User User { get; set; }
    }
}
