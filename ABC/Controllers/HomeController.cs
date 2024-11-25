
using ABC.Dtos;
using ABC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Extensions;
using Newtonsoft.Json;
using System.Diagnostics;

namespace ABC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task <IActionResult> Index()
        {
            string apiUrl = "https://www.nrb.org.np/api/forex/v1/rates?page=1&per_page=5&from=2024-06-12&to=2024-06-12";
            List<ExchangeRateDto> rates = new();

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    if(response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        var apiResponse = JsonConvert.DeserializeObject<ExchangeRateResponseDto>(jsonResponse);

                        // Extracting rates from the payload

                        if(apiResponse?.Data?.Payload !=null && apiResponse.Data.Payload.Count > 0)
                        {
                            rates = apiResponse.Data.Payload.SelectMany(p => p.Rates).ToList();
                        }
                    }
                    else
                    {
                        _logger.LogError($"API Error: {response.StatusCode}");
                    }
                }
                catch (Exception EX)
                {

                    _logger.LogError($"Exception occured: {EX.Message}");
                    throw new Exception($"The following error have arrived: {EX.Message}");
                }
            }

                return View(rates);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
