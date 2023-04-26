using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Drawing;
using System.Xml.Linq;
using WebApplicationEmailDetection.Models;

namespace WebApplicationEmailDetection.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient = new HttpClient();

        public HomeController(ILogger<HomeController> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }


       

        public IActionResult Index()
        {
            var model = new CheckEmail();
            model.Answer = "";
            return View(model);
        }
      
        [HttpPost]
        public async Task<IActionResult> Index(CheckEmail checkEmail)
        {
            var apiUrl = $"http://127.0.0.1:5000/queryparams";
            var url = $"{apiUrl}?name={checkEmail.txtBody}";
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                // Read the response content
                var responseContent = await response.Content.ReadAsStringAsync();

                // Do something with the response content, like update a label
                if (responseContent != null)
                {
                    checkEmail.Answer = responseContent;
                   // ModelState.AddModelError("Error", responseContent);

                }
                else
                    ModelState.AddModelError("Error", "Error ");

            }
            else
            {
                // Handle the error response
                ModelState.AddModelError("Error", "Error ");
            }

            //if (response.IsSuccessStatusCode)
            //{
            //    var responseContent = await response.Content.ReadAsStringAsync();
              
            //    return View();
            //}
            //else
            //{
            //    return BadRequest();
            //}
            return View(checkEmail);
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