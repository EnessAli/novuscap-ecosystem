using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NovusCapEkosistemHaritasi.Models;
using NovusCapEkosistemHaritasi.Services;

namespace NovusCapEkosistemHaritasi.Controllers
{
    public class EcosystemPointsController : Controller
    {
        private readonly IEcosystemPointService _ecosystemPointService;
        private readonly ICategoryService _categoryService;

        public EcosystemPointsController(
            IEcosystemPointService ecosystemPointService,
            ICategoryService categoryService)
        {
            _ecosystemPointService = ecosystemPointService;
            _categoryService = categoryService;
        }

        // GET: EcosystemPoints
        public async Task<IActionResult> Index(string searchTerm = "", int? categoryId = null)
        {
            ViewData["Title"] = "Ekosistem Noktaları";
            
            IEnumerable<EcosystemPoint> ecosystemPoints;
            
            if (!string.IsNullOrEmpty(searchTerm))
            {
                ecosystemPoints = await _ecosystemPointService.SearchEcosystemPointsAsync(searchTerm);
                ViewData["SearchTerm"] = searchTerm;
            }
            else if (categoryId.HasValue)
            {
                ecosystemPoints = await _ecosystemPointService.GetEcosystemPointsByCategoryAsync(categoryId.Value);
                ViewData["CategoryId"] = categoryId;
            }
            else
            {
                ecosystemPoints = await _ecosystemPointService.GetAllEcosystemPointsAsync();
            }
            
            // Get categories for filter dropdown
            var categories = await _categoryService.GetAllCategoriesAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            
            return View(ecosystemPoints);
        }

        // GET: EcosystemPoints/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var ecosystemPoint = await _ecosystemPointService.GetEcosystemPointByIdAsync(id);
            if (ecosystemPoint == null)
            {
                return NotFound();
            }

            return View(ecosystemPoint);
        }

        // GET: EcosystemPoints/Create
        public async Task<IActionResult> Create()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            
            return View();
        }

        // POST: EcosystemPoints/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EcosystemPoint ecosystemPoint)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _ecosystemPointService.CreateEcosystemPointAsync(ecosystemPoint);
                    return RedirectToAction(nameof(Index));
                }
                catch (ArgumentException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            var categories = await _categoryService.GetAllCategoriesAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name", ecosystemPoint.CategoryId);
            
            return View(ecosystemPoint);
        }

        // GET: EcosystemPoints/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var ecosystemPoint = await _ecosystemPointService.GetEcosystemPointByIdAsync(id);
            if (ecosystemPoint == null)
            {
                return NotFound();
            }

            var categories = await _categoryService.GetAllCategoriesAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name", ecosystemPoint.CategoryId);
            
            return View(ecosystemPoint);
        }

        // POST: EcosystemPoints/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EcosystemPoint ecosystemPoint)
        {
            if (id != ecosystemPoint.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _ecosystemPointService.UpdateEcosystemPointAsync(ecosystemPoint);
                    return RedirectToAction(nameof(Index));
                }
                catch (ArgumentException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                catch (Exception)
                {
                    if (await _ecosystemPointService.GetEcosystemPointByIdAsync(id) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            var categories = await _categoryService.GetAllCategoriesAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name", ecosystemPoint.CategoryId);
            
            return View(ecosystemPoint);
        }

        // GET: EcosystemPoints/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var ecosystemPoint = await _ecosystemPointService.GetEcosystemPointByIdAsync(id);
            if (ecosystemPoint == null)
            {
                return NotFound();
            }

            return View(ecosystemPoint);
        }

        // POST: EcosystemPoints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _ecosystemPointService.DeleteEcosystemPointAsync(id);
            return RedirectToAction(nameof(Index));
        }
        
        // GET: EcosystemPoints/Map
        public async Task<IActionResult> Map()
        {
            ViewData["Title"] = "Ekosistem Haritası";
            
            var ecosystemPoints = await _ecosystemPointService.GetAllEcosystemPointsAsync();
            var categories = await _categoryService.GetAllCategoriesAsync();
            
            ViewBag.Categories = categories;
            
            return View(ecosystemPoints);
        }
        
        // API: EcosystemPoints/GetAllPoints
        [HttpGet]
        public async Task<JsonResult> GetAllPoints()
        {
            var ecosystemPoints = await _ecosystemPointService.GetAllEcosystemPointsAsync();
            return Json(ecosystemPoints);
        }
        
        // API: EcosystemPoints/GetPointsByCategory/5
        [HttpGet]
        public async Task<JsonResult> GetPointsByCategory(int id)
        {
            var ecosystemPoints = await _ecosystemPointService.GetEcosystemPointsByCategoryAsync(id);
            return Json(ecosystemPoints);
        }
        
        // API: EcosystemPoints/GetPointsByCity/Istanbul
        [HttpGet]
        public async Task<JsonResult> GetPointsByCity(string city)
        {
            var ecosystemPoints = await _ecosystemPointService.GetEcosystemPointsByCityAsync(city);
            return Json(ecosystemPoints);
        }
        
        // API: EcosystemPoints/Search?term=42
        [HttpGet]
        public async Task<JsonResult> Search(string term)
        {
            var ecosystemPoints = await _ecosystemPointService.SearchEcosystemPointsAsync(term);
            return Json(ecosystemPoints);
        }
    }
} 