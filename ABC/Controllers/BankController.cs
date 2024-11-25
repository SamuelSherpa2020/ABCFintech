using ABC.Data.DataServices.Interface;
using ABC.Dtos.BankDto;
using ABC.Dtos.UserBankDto;
using ABC.Models;
using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using Microsoft.AspNetCore.Components.Forms.Mapping;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace ABC.Controllers
{
    public class BankController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        public BankController(IUnitOfWork _uow, IMapper _mapper, UserManager<User> userManager)
        {
            uow = _uow;
            mapper = _mapper;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var allBank = await uow.Repository<Bank>().GetAllAsync();
            var getBankDto = mapper.Map<List<GetBankDto>>(allBank);

            return View(getBankDto);
        }

        public async Task<IActionResult> BankDetails(Guid id)
        {
            var bank = await uow.Repository<Bank>().GetByIdAsync(id);
            var bankDetails = mapper.Map<GetBankDto>(bank);
            if (bank == null)
            {
                return NotFound();
            }
            return View(bankDetails);
        }

        #region CreateBank
        // GET: Bank/Create
        public IActionResult CreateBank()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBank(CreateBankDto createBankDto)
        {
            if (ModelState.IsValid)
            {
                var bank = new Bank
                {
                    BankId = Guid.NewGuid(),
                    BankName = createBankDto.BankName
                };

                await uow.Repository<Bank>().AddAsync(bank);
                await uow.CompleteAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(createBankDto);
        }

        #endregion

        #region Edit Bank
        public async Task<IActionResult> EditBank(Guid id)
        {
            var bank = await uow.Repository<Bank>().GetByIdAsync(id);
            var bankDetail = mapper.Map<GetBankDto>(bank);
            if (bank == null)
            {
                return NotFound();
            }

            return View(bankDetail);
        }

        [HttpPost]
        public async Task<IActionResult> EditBank(GetBankDto getBankDto)
        {
            if (ModelState.IsValid)
            {
                var editBank = mapper.Map<Bank>(getBankDto);
                uow.Repository<Bank>().Update(editBank!);
                await uow.CompleteAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(getBankDto);
        }

        #endregion

        #region Delete Bank

        // GET: Bank/Delete/5
        public async Task<IActionResult> DeleteBank(Guid id)
        {
            var bank = await uow.Repository<Bank>().GetByIdAsync(id);
            var bankDetail = mapper.Map<GetBankDto>(bank);
            if (bank == null)
            {
                return NotFound();
            }
            return View(bankDetail);
        }

        // POST: Bank/Delete/5
        [HttpPost]
        public async Task<IActionResult> DeleteBank(GetBankDto getBankDto)
        {
            var bank = await uow.Repository<Bank>().GetByIdAsync(getBankDto.BankId);
            if (bank == null)
            {
                return NotFound();
            }

            uow.Repository<Bank>().Remove(bank);
            await uow.CompleteAsync();

            return RedirectToAction(nameof(Index));
        }
        #endregion

        [HttpGet]
        public async Task<IActionResult> RegisterUserBank()
        {
            var allBanks = await uow.Repository<Bank>().GetAllAsync();
            var allbankDto = mapper.Map<List<GetBankDto>>(allBanks);

            ViewBag.Banks = new SelectList(allbankDto, "BankId", "BankName");
            ClaimsPrincipal principal = User;
            var user = await _userManager.GetUserAsync(principal);
            if (user == null)
                throw new Exception("User not found");

            TempData["Uid"] = user.Id.ToString();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUserBank(CreateUserBankDto createUserBankDto)
        {
            if (!ModelState.IsValid)
            {
                var allBanks = await uow.Repository<Bank>().GetAllAsync();
                var allbankDto = mapper.Map<List<GetBankDto>>(allBanks);

                ViewBag.Banks = new SelectList(allbankDto, "BankId", "BankName");
                ClaimsPrincipal principal = User;
                var user = await _userManager.GetUserAsync(principal);

                return View(createUserBankDto);
                TempData.Keep();
            }

            TempData.Keep();

            var userBank = mapper.Map<UserBank>(createUserBankDto);
            userBank.Uid = TempData["Uid"].ToString();

            await uow.Repository<UserBank>().AddAsync(userBank);
            await uow.CompleteAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}
