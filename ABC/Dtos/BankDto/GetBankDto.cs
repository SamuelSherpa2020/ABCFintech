using ABC.Models;
using System.ComponentModel.DataAnnotations;

namespace ABC.Dtos.BankDto
{
    public class GetBankDto
    {
      
        public Guid BankId { get; set; }

        public string BankName { get; set; }
    }
}
