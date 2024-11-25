using ABC.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ABC.Dtos.TransactionDto
{
    public class GetTransactionDto
    {

        public Guid TransactionId { get; set; }


        public string SenderId { get; set; }

        public string ReceiverId { get; set; }

        public Guid PaymentDetailId { get; set; }
    }
}
