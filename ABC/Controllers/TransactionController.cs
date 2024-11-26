using ABC.Data.DataServices.Interface;
using ABC.Dtos.BankDto;
using ABC.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

            // Load Banks for Dropdown
            var allBanks = await uow.Repository<Bank>().GetAllAsync();
            var allBankDto = mapper.Map<List<GetBankDto>>(allBanks);
            ViewBag.Banks = new SelectList(allBankDto, "BankId", "BankName");

            return View(transactions);
        }

        public async Task<IActionResult> Filter(string senderName, string receiverName, Guid? bankId, string accountNumber)
        {
            // Load Banks for Dropdown
            var allBanks = await uow.Repository<Bank>().GetAllAsync();
            var allBankDto = mapper.Map<List<GetBankDto>>(allBanks);
            ViewBag.Banks = new SelectList(allBankDto, "BankId", "BankName");

            // Load Transactions with Filters
            var transactions = await uow.Repository<Transaction>()
                .FindAsync(includeProperties: "Sender,Receiver,PaymentDetail.Bank");

            // Apply Filters
            if (!string.IsNullOrEmpty(senderName))
            {
                transactions = transactions.Where(t =>
                    t.Sender.FirstName.Contains(senderName, StringComparison.OrdinalIgnoreCase) ||
                    t.Sender.LastName.Contains(senderName, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(receiverName))
            {
                transactions = transactions.Where(t =>
                    t.Receiver.FirstName.Contains(receiverName, StringComparison.OrdinalIgnoreCase) ||
                    t.Receiver.LastName.Contains(receiverName, StringComparison.OrdinalIgnoreCase));
            }

            if (bankId.HasValue)
            {
                transactions = transactions.Where(t => t.PaymentDetail.BankId == bankId);
            }

            if (!string.IsNullOrEmpty(accountNumber))
            {
                transactions = transactions.Where(t =>
                    t.PaymentDetail.ReceiverAccountNumber.Contains(accountNumber, StringComparison.OrdinalIgnoreCase));
            }

            return View("Index", transactions);
        }

    }
}
