using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ABC.Models
{
    public class User : IdentityUser
    {
        
        [Required]
        public string FirstName { get; set; }

        public string? MiddleName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string Address { get; set; }

        public string Country { get; set; }
        public virtual ICollection<UserBank> UserBanks { get; set; } = new List<UserBank>();
    }
}
