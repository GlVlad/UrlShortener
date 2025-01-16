using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Models;

namespace UrlShortener.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        
        private const string InitialDescription = "���� URL Shortener ���������� �������� ��������� ��������� ����� ��� �������� ���������� �������� URL. ��� ������ ���������� ������ URL ������������ ���������� ������, ������� �������� � ���� ������.";

        [HttpGet]
        public IActionResult About()
        {
             ViewData["Description"] = InitialDescription;
             return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult About(string description)
        {
             ViewData["Description"] = description;
             return View();
        }
   
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
