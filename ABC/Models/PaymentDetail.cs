using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ABC.Models
{
    public class PaymentDetail
    {
        [Key]
        public Guid PaymentDetailId { get; set; }

        [ForeignKey("Bank")]
        public Guid BankId { get; set; }

        public string ReceiverAccountNumber { get; set; }

        public decimal TransferedAmount { get; set; }

        public decimal ExchangeRate { get; set; }

        public decimal PayoutAmount { get; set; }

        public decimal Unit {  get; set; }

        public virtual Bank Bank { get; set; }
    }
}
