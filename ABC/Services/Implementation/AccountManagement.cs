using ABC.Data.DataServices.Interface;
using ABC.Dtos.UserDto;
using ABC.Models;
using ABC.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace ABC.Services.Implementation
{
    public class AccountManagement : IAccountManagement
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;

        public AccountManagement(IUnitOfWork unitOfWork, UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        public async Task<bool> SignUpAsync(CreateUserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            user.UserName = userDto.Email;
            user.Email = userDto.Email;

            user.SecurityStamp = Guid.NewGuid().ToString();
            try
            {
                var result = await _userManager.CreateAsync(user, userDto.ConfirmPassword);
                if (result.Succeeded)
                {
                    // Additional operations can be done with _unitOfWork here if needed
                    await _unitOfWork.CompleteAsync();
                }
                else
                {
                    // Log errors for debugging
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine($"Error: {error.Description}");
                    }
                }
                return result.Succeeded;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error during user creation:  { ex.Message }");
            }

        }

        public async Task<bool> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            //var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, lockoutOnFailure: false);
            // only below sigin authenticates for a user to be login
            var result = await _signInManager.PasswordSignInAsync(loginDto.Email, loginDto.Password, isPersistent: true, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                
                return result.Succeeded; 
            }

            return result.Succeeded; // Login failed
        }
        public async Task LogoutAsync() // Implement Logout
        {
            await _signInManager.SignOutAsync();
        }
    }
}

