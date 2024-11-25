using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ABC.Models
{
    public class Transaction
    {
        [Key]
        public Guid TransactionId { get; set; }

        [ForeignKey("Sender")]
        public string SenderId { get; set; }

        [ForeignKey("Receiver")]
        public string ReceiverId { get; set; }

        [ForeignKey("PaymentDetail")]
        public Guid PaymentDetailId { get; set; }

        public virtual User Sender { get; set; }
        public virtual User Receiver { get; set; }
        public virtual PaymentDetail PaymentDetail { get; set; }
    }
}
