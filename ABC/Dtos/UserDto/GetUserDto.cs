using ABC.Models;
using System.ComponentModel.DataAnnotations;

namespace ABC.Dtos.UserDto
{
    public class GetUserDto
    {
        public Guid Uid { get; set; }

        public string FirstName { get; set; }

        public string? MiddleName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string Country { get; set; }

    }
}
