using ABC.Data.DataServices.Interface;
using ABC.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace ABC.Controllers
{
    public class TransactionController : Controller
    {
        private readonly UserManager<User> _userManager;
        IMapper mapper;
        IUnitOfWork uow;

        public TransactionController(IMapper _mapper, IUnitOfWork _unitOfWork, UserManager<User> userManager)
        {
            mapper = _mapper;
            uow = _unitOfWork;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            //var transactions = uow.Repository<Transaction>().GetAllAsync();
            // Now Below code is for Transaction:
            ClaimsPrincipal principal = User;
            var user = await _userManager.GetUserAsync(principal);
            var currentUserId = user.Id;


            var transactions = await uow.Repository<Transaction>()
                .FindAsync(includeProperties: "Sender,Receiver,PaymentDetail.Bank");

            var bankName = transactions.FirstOrDefault()!.PaymentDetail.BankId;

            return View(transactions);
        }
    }
}
