using ABC.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ABC.Dtos.UserBankDto
{
    public class CreateUserBankDto
    {
             
        //public string Uid { get; set; }
     
        //public Guid BankId { get; set; }

        //public string AccountNumber { get; set; }

        //public decimal AvailableAmount { get; set; }

        //public string Uid { get; set; }

        [Required(ErrorMessage = "Please select a bank.")]
        public Guid BankId { get; set; }

        [Required(ErrorMessage = "Account number is required.")]
        [StringLength(20, ErrorMessage = "Account number cannot exceed 20 characters.")]
        public string AccountNumber { get; set; }

        [Required(ErrorMessage = "Please enter an available amount.")]
        [Range(0, double.MaxValue, ErrorMessage = "Amount must be a positive number.")]
        public decimal AvailableAmount { get; set; }

    }
}
