using ABC.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ABC.Dtos.TransactionDto
{
    public class CreateTransactionDto
    {

        [ForeignKey("Sender")]
        public string SenderId { get; set; }

        [ForeignKey("Receiver")]
        public string ReceiverId { get; set; }

        [ForeignKey("PaymentDetail")]
        public Guid PaymentDetailId { get; set; }

    }
}
