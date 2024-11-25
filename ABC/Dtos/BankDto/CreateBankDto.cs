using ABC.Models;
using System.ComponentModel.DataAnnotations;

namespace ABC.Dtos.BankDto
{
    public class CreateBankDto
    {
     
        [Required]
        public string BankName { get; set; }

    }
}
