using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using UrlShortener.Data;
using UrlShortener.Models;

namespace UrlShortener.Controllers
{
    [Authorize]
    public class UrlShortenerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UrlShortenerController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var urls = _context.UrlShort.ToList();
            return View(urls);
        }

        [HttpPost]
        public async Task<IActionResult> Create(string originalUrl) 
        { 
            if (string.IsNullOrEmpty(originalUrl)) 
            { 
                ModelState.AddModelError("", "URL cannot be empty.");
                var urls = _context.UrlShort.ToList();
                return View("Index", urls); 
            } 
            var existingUrl = _context.UrlShort.FirstOrDefault(u => u.OriginalUrl == originalUrl); if (existingUrl != null) 
            {
                ModelState.AddModelError("", "This URL is already exists."); 
                var urls = _context.UrlShort.ToList();
                return View("Index", urls); 
            } 

            var shortUrl = GenerateShortUrl(); 
            var userName = User.Identity.Name;

            var urlShortener = new UrlShort 
            { 
                OriginalUrl = originalUrl, 
                ShortUrl = shortUrl, 
                CreatedBy = userName, 
                CreatedDate = DateTime.Now 
            }; 

            _context.UrlShort.Add(urlShortener); 

            await _context.SaveChangesAsync();
            
            return RedirectToAction("Index"); 
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var urlShortener = await _context.UrlShort.FindAsync(id);
            if (urlShortener == null)
            {
                return NotFound();
            }
            return View(urlShortener);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var urlShortener = await _context.UrlShort.FindAsync(id);
            if (urlShortener == null)
            {
                return NotFound();
            }

            _context.UrlShort.Remove(urlShortener);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        private string GenerateShortUrl()
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, 6)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
