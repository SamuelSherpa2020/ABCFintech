using ABC.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ABC.Dtos.UserBankDto
{
    public class GetUserBankDto
    {
       
        public Guid UserBankUid { get; set; }

        public string Uid { get; set; }

     
        public Guid BankId { get; set; }

        public string AccountNumber { get; set; }

        public decimal AvailableAmount { get; set; }

    }
}
