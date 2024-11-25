using System.ComponentModel.DataAnnotations;

namespace ABC.Models
{
    public class Bank
    {
        [Key]
        public Guid BankId { get; set; }

        [Required]
        public string BankName { get; set; }

        public virtual ICollection<UserBank> UserBanks { get; set; } = new List<UserBank>();
    }
}
