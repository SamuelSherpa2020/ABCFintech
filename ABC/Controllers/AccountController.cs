using ABC.Dtos.UserDto;
using ABC.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ABC.Controllers
{
    public class AccountController : Controller
    {
        private IAccountManagement iaccountManagement;
        private IMapper mapper;
        public AccountController(IAccountManagement _iAccountManagement, IMapper _mapper)
        {
            iaccountManagement = _iAccountManagement;
            mapper = _mapper;
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(CreateUserDto createUserDto)
        {

            if (!ModelState.IsValid)
            {
                return View(createUserDto);
            }
            var result = await iaccountManagement.SignUpAsync(createUserDto);

            if (!result)
            {
                ModelState.AddModelError("", "Failed to create user. Please check the detail");
                return View(createUserDto);
            }
            return RedirectToAction("Login");

        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {

            if (!ModelState.IsValid)
            {
                return View(loginDto);
            }

            var result = await iaccountManagement.LoginAsync(loginDto);
            //return RedirectToAction("Index", "Home");

            if (result)
            {
                return RedirectToAction("Index", "Home"); // Redirect to HomeController's Index action
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(loginDto);
        }

        //[HttpPost]
        public async Task<IActionResult> Logout()
        {
            await iaccountManagement.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }


    }
}
