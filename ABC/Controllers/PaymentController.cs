using ABC.Data.DataServices.Interface;
using ABC.Dtos;
using ABC.Dtos.BankDto;
using ABC.Dtos.PaymentDto;
using ABC.Dtos.TransactionDto;
using ABC.Dtos.UserBankDto;
using ABC.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Security.Claims;

namespace ABC.Controllers
{
    public class PaymentController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<PaymentController> _logger;
        private readonly IMapper mapper;
        private readonly IUnitOfWork uow;

        public PaymentController(IMapper _mapper, IUnitOfWork _uow, ILogger<PaymentController> logger, UserManager<User> userManager)
        {
            mapper = _mapper;
            uow = _uow;
            _logger = logger;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> MakePayment()
        {
            var allBanks = await uow.Repository<Bank>().GetAllAsync();
            var allBankDto = mapper.Map<List<GetBankDto>>(allBanks);

            ViewBag.Banks = new SelectList(allBankDto, "BankId", "BankName");


            // Fetch exchange rates from API
            string apiUrl = "https://www.nrb.org.np/api/forex/v1/rates?page=1&per_page=5&from=2024-06-12&to=2024-06-12";
            List<SelectListItem> currencies = new();

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        var apiResponse = JsonConvert.DeserializeObject<ExchangeRateResponseDto>(jsonResponse);

                        if (apiResponse?.Data?.Payload != null && apiResponse.Data.Payload.Count > 0)
                        {
                            var rates = apiResponse.Data.Payload.SelectMany(p => p.Rates).ToList();

                            currencies = rates
                                .Select(rate => new SelectListItem
                                {
                                    Text = $"{rate.Currency.Name} ({rate.Currency.Iso3})",
                                    Value = $"{rate.Currency.Iso3}|{rate.Currency.Unit}|{rate.Sell ?? 0}"
                                })
                                .ToList();
                        }
                    }
                    else
                    {
                        _logger.LogError($"API Error: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Exception occurred: {ex.Message}");
                }
            }

            ViewBag.Currencies = currencies;


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> MakePayment(CreatePaymentDto createPaymentDto)
        {
            if (!ModelState.IsValid)
            {
                // If validation fails, re-populate the ViewBag for the dropdown
                var allBanks = await uow.Repository<Bank>().GetAllAsync();
                var allBankDto = mapper.Map<List<GetBankDto>>(allBanks);

                ViewBag.Banks = new SelectList(allBankDto, "BankId", "BankName");

                return View(createPaymentDto); // Adjust if you have another action to redirect
            }

            var paymentDetail = mapper.Map<PaymentDetail>(createPaymentDto);

            await uow.Repository<PaymentDetail>().AddAsync(paymentDetail);
            await uow.CompleteAsync();

            // Now Below code is for Transaction:
            ClaimsPrincipal principal = User;
            var user = await _userManager.GetUserAsync(principal);
            var senderId = user.Id;

            var paymentMadeto = await uow.Repository<PaymentDetail>().GetByIdAsync(paymentDetail.PaymentDetailId);

            if (paymentMadeto == null)
            {
                throw new Exception("Payment detail not found.");
            }

            // Fetch UserBank where ReceiverAccountNumber matches the AccountNumber in UserBank
            var userBank = (await uow.Repository<UserBank>()
                                    .FindAsync(ub => ub.AccountNumber == paymentMadeto.ReceiverAccountNumber))
                                    .FirstOrDefault();

            var receiverId = userBank?.Uid;

            CreateTransactionDto createTransactionDto = new CreateTransactionDto()
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                PaymentDetailId = paymentDetail.PaymentDetailId,
            };

            var transaction = mapper.Map<Transaction>(createTransactionDto);
            await uow.Repository<Transaction>().AddAsync(transaction);
            await uow.CompleteAsync();

            return RedirectToAction("Index", "Transaction");
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
