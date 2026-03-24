using Microsoft.AspNetCore.Mvc;
using NovusCapEkosistemHaritasi.Models;
using NovusCapEkosistemHaritasi.Services;
using System.Diagnostics;

namespace NovusCapEkosistemHaritasi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEcosystemPointService _ecosystemPointService;
        private readonly ICategoryService _categoryService;
        private readonly IPageContentService _pageContentService;
        private readonly IContactMessageService _contactMessageService;
        private readonly IMenuService _menuService;

        public HomeController(
            ILogger<HomeController> logger,
            IEcosystemPointService ecosystemPointService,
            ICategoryService categoryService,
            IPageContentService pageContentService,
            IContactMessageService contactMessageService,
            IMenuService menuService)
        {
            _logger = logger;
            _ecosystemPointService = ecosystemPointService;
            _categoryService = categoryService;
            _pageContentService = pageContentService;
            _contactMessageService = contactMessageService;
            _menuService = menuService;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Ana Sayfa";
            
            var ecosystemPoints = await _ecosystemPointService.GetAllEcosystemPointsAsync();
            var categories = await _categoryService.GetAllCategoriesAsync();
            var cities = await _ecosystemPointService.GetAllCitiesAsync();
            
            ViewBag.EcosystemPoints = ecosystemPoints;
            ViewBag.Categories = categories;
            ViewBag.Cities = cities;
            
            return View();
        }

        public async Task<IActionResult> About()
        {
            var pageContent = await _pageContentService.GetPageContentByNameAsync("About");
            
            if (pageContent != null)
            {
                ViewData["Title"] = pageContent.Title;
                ViewData["Subtitle"] = pageContent.Subtitle;
                ViewData["MetaDescription"] = pageContent.MetaDescription;
                
                return View(pageContent);
            }
            
            ViewData["Title"] = "Hakkımızda";
            return View();
        }

        public async Task<IActionResult> Contact()
        {
            var pageContent = await _pageContentService.GetPageContentByNameAsync("Contact");
            
            if (pageContent != null)
            {
                ViewData["Title"] = pageContent.Title;
                ViewData["Subtitle"] = pageContent.Subtitle;
                ViewData["MetaDescription"] = pageContent.MetaDescription;
                
                ViewBag.PageContent = pageContent;
            }
            else
            {
                ViewData["Title"] = "İletişim";
            }
            
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact(ContactMessage contactMessage)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _contactMessageService.CreateContactMessageAsync(contactMessage);
                    TempData["SuccessMessage"] = "Mesajınız başarıyla gönderildi. En kısa sürede size dönüş yapacağız.";
                    return RedirectToAction(nameof(Contact));
                }
                catch (ArgumentException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            
            var pageContent = await _pageContentService.GetPageContentByNameAsync("Contact");
            
            if (pageContent != null)
            {
                ViewData["Title"] = pageContent.Title;
                ViewData["Subtitle"] = pageContent.Subtitle;
                ViewData["MetaDescription"] = pageContent.MetaDescription;
                
                ViewBag.PageContent = pageContent;
            }
            else
            {
                ViewData["Title"] = "İletişim";
            }
            
            return View(contactMessage);
        }

        public async Task<IActionResult> Privacy()
        {
            var pageContent = await _pageContentService.GetPageContentByNameAsync("Privacy");
            
            if (pageContent != null)
            {
                ViewData["Title"] = pageContent.Title;
                ViewData["Subtitle"] = pageContent.Subtitle;
                ViewData["MetaDescription"] = pageContent.MetaDescription;
                
                return View(pageContent);
            }
            
            ViewData["Title"] = "Gizlilik Politikası";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
} 