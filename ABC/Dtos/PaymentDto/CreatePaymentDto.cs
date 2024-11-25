using ABC.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ABC.Dtos.PaymentDto
{
    public class CreatePaymentDto
    {
        public Guid BankId { get; set; }

        public string ReceiverAccountNumber { get; set; }

        public decimal TransferedAmount { get; set; }

        public decimal ExchangeRate { get; set; }

        public decimal PayoutAmount { get; set; }

        public decimal Unit { get; set; }

    }
}
