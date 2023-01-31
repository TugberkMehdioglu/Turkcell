using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyAspNetApp.Web.Filters;
using MyAspNetApp.Web.Models;
using MyAspNetApp.Web.ViewModels;
using MyAspNetApp.Web.ViewModels.WrapperClasses;
using System.Diagnostics;

namespace MyAspNetApp.Web.Controllers
{
    [LogFilter]
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MyContext _context;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, MyContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        [Route("/")] //Controller'ın üstündeki Route'u yazınca, burdaki Route'ların içine / koyduk ki, burdaki Route'lar yukardaki Route'dan etkilenmesin.
        [Route("/Home")]
        [Route("/Home/Index")]
        public IActionResult Index()
        {
            HashSet<ProductPartialViewModel> products = _context.Products.Select(x => new ProductPartialViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                Stock = x.Stock
            }).ToHashSet();

            ViewBag.productsListPartialViewModel=new ProductListPartialViewModel() { Products= products };

            return View();
        }


        [CustomExceptionFilter]
        public IActionResult Privacy()
        {
            throw new Exception("Veritabanı ile ilgili bir hata meydana geldi");

            HashSet<ProductPartialViewModel> products = _context.Products.Select(x => new ProductPartialViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                Stock = x.Stock
            }).ToHashSet();

            ViewBag.productsListPartialViewModel = new ProductListPartialViewModel() { Products = products };

            return View();
        }

        public IActionResult Visitor()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SaveVisitorComment(VisitorViewModel visitorViewModel)
        {
            try
            {
                Visitor visitor = _mapper.Map<Visitor>(visitorViewModel);
                visitor.CreatedDate = DateTime.Now;
                _context.Visitors.Add(visitor);
                _context.SaveChanges();
                TempData["result"] = "Yorum kaydedilmiştir";
                return RedirectToAction(nameof(Visitor));
            }
            catch (Exception)
            {
                TempData["result"] = "Yorum kaydedilirken bir hata oluştu, lütfen daha sonra tekrar deneyiniz";
                return RedirectToAction(nameof(Visitor));
            }


        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(ErrorViewModel errorViewModel)
        {
            //Current.Id yada TraceIdentifier : RequestId'ye uniqe bir id değeri atar, istersen Guid değerde verebilirsin sana kalmış.
            errorViewModel.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;

            return View(errorViewModel);
            //Bunun View'su neden Home'da değilde Shared'da?
            //Çünkü Error sadece HomeController'a özgü bir sayfa değil, uygulamanın genelinde hata meydana geldiğinde gözükceğinden Shared'da.
            //Bir ASP.NET Frametowk'ü bir Action method'un View'sunu: İlk olarak ilgili Controller'ın klasöründe arar, bulamazsa Shared klasörüne bakar.
        }
    }
}