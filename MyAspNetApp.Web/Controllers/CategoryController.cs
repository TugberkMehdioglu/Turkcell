using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyAspNetApp.Web.Models;
using MyAspNetApp.Web.ViewModels;

namespace MyAspNetApp.Web.Controllers
{
    [Route("[Controller]/[Action]")]
    public class CategoryController : Controller
    {
        private readonly MyContext _context;
        private readonly IMapper _mapper;

        public CategoryController(MyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            List<Category> categories = _context.Categories.ToList();

            return View(_mapper.Map<List<CategoryViewModel>>(categories));
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(CategoryViewModel categoryViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Categories.Add(_mapper.Map<Category>(categoryViewModel));
                    _context.SaveChanges();
                    TempData["success"] = "Kategori eklendi";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception exception)
                {
                    ViewBag.exception = $"Veritabanı ile ilgili bir hata oluştu, alınan hata = {exception.Message}";
                    return View();
                }
            }
            ViewBag.exception = "Girilen değerler istenilen şartlara uymuyor";
            return View();
        }

        
        public IActionResult Update(int id)
        {
            Category? category = _context.Categories.Find(id);

            if (category == null) return RedirectToAction(nameof(Index));

            return View(_mapper.Map<CategoryViewModel>(category));
        }

        [HttpPost]
        public IActionResult Update(CategoryViewModel categoryViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                try
                {
                    _context.Categories.Update(_mapper.Map<Category>(categoryViewModel));
                    _context.SaveChanges();
                    TempData["success"] = "Kategori güncellendi";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception exception)
                {
                    ViewBag.exception = $"Veritabanı ile ilgili bir hata oluştu, alınan hata = {exception.Message}";
                    return View();
                }

            }
        }

        [Route("{id}")]
        public IActionResult Remove(int id)
        {
            Category? category=_context.Categories.Find(id);

            if (category == null) return RedirectToAction(nameof(Index));

            try
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
                TempData["success"] = "Kategori silindi";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception exception)
            {
                ViewBag.exception = $"Veritabanı ile ilgili bir hata oluştu, alınan hata = {exception.Message}";
                return View();
            }
        }


    }
}
