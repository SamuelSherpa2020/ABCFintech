using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ABC.Models
{
    public class UserBank
    {
        [Key]
        public Guid UserBankUid { get; set; }

        [ForeignKey("User")]
        public string Uid { get; set; }

        [ForeignKey("Bank")]
        public Guid BankId { get; set; }

        public string AccountNumber { get; set; }

        public decimal AvailableAmount { get; set; }

        public virtual User User { get; set; }
        public virtual Bank Bank { get; set; }
    }
}
