using ABC.CustomValidation;
using ABC.Models;
using System.ComponentModel.DataAnnotations;

namespace ABC.Dtos.UserDto
{
    public class CreateUserDto
    {

        [Required]
        public string FirstName { get; set; }

        public string? MiddleName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string Address { get; set; }

        public string Country { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
