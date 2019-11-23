using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace digitalsign.persistence.Models
{
    public class RefreshToken
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Token { get; set; }

        public string JwtId { get; set; }

        public DateTime CreationDate { get; set; }
 
        public DateTime ExpiryDate { get; set; }

        public bool IsUsed { get; set; }

        public bool Invalidated { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
