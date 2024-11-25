using ABC.Dtos.UserDto;
using ABC.Models;

namespace ABC.Services.Interfaces
{
    public interface IAccountManagement
    {
        Task<bool> SignUpAsync(CreateUserDto user);
        Task<bool> LoginAsync(LoginDto loginDto);

        Task LogoutAsync();
    }
}
