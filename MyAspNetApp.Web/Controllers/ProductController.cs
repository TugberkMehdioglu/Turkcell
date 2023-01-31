using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using MyAspNetApp.Web.Filters;
using MyAspNetApp.Web.Models;
using MyAspNetApp.Web.ViewModels;

namespace MyAspNetApp.Web.Controllers
{
    [Route("[controller]/[action]")]
    public class ProductController : Controller
    {
        private readonly MyContext _context;
        private readonly IMapper _mapper;
        private readonly IFileProvider _fileProvider;
        public ProductController(MyContext context, IMapper mapper, IFileProvider fileProvider)
        {
            _context = context;
            _mapper = mapper;
            _fileProvider = fileProvider;
        }

        [CustomResultFilter("x-version", "2.0")]
        //[CacheResourceFilter]
        public IActionResult Index()
        {
            HashSet<Product> products = _context.Products.Include(x => x.Category).ToHashSet();
             
            return View(_mapper.Map<HashSet<ProductViewModel>>(products));
        }

        public IActionResult Add()
        {
            ViewBag.Expire = new Dictionary<string, int>()
            {
               {"1 Ay", 1},
               {"3 Ay", 3},
               {"6 Ay", 6},
               {"12 Ay", 12}
            };

            ViewBag.ColorSelect = new SelectList(new List<ColorSelectList>()
            {
                new(){Data="Mavi", Value="Mavi"},
                new(){Data="Kırmızı", Value="Kırmızı"},
                new(){Data="Sarı", Value="Sarı"},
            }, nameof(ColorSelectList.Value), nameof(ColorSelectList.Data));

            List<Category> categories = _context.Categories.ToList();
            ViewBag.categorySelect = new SelectList(categories, nameof(Category.Id), nameof(Category.Name));

            return View();
        }

        [HttpPost]
        public IActionResult Add(ProductAddViewModel newProduct)
        {
            IActionResult? result = null;

            if (!string.IsNullOrEmpty(newProduct.Name) && newProduct.Name.StartsWith("A"))
            {
                ModelState.AddModelError(string.Empty, "Ürün ismi A harfi ile başlayamaz");
            }

            if (!ModelState.IsValid)
            {
                result = View();
            }
            else
            {
                try
                {
                    //Product product = _mapper.Map<Product>(newProduct);

                    Product product = new()
                    {
                        Id = newProduct.Id,
                        Name = newProduct.Name,
                        Price = newProduct.Price,
                        Stock = newProduct.Stock,
                        Color=newProduct.Color,
                        İsPublish=newProduct.İsPublish,
                        Expire=newProduct.Expire,
                        Description=newProduct.Description,
                        PublishDate=newProduct.PublishDate,
                        ImagePath=newProduct.ImagePath,
                        CategoryId=newProduct.CategoryId,
                    };

                    if (newProduct.Image!=null && newProduct.Image.Length>0)
                    {
                        var root = _fileProvider.GetDirectoryContents("wwwroot");

                        var images = root.First(x => x.Name == "images");

                        var randomImageName = Guid.NewGuid() + Path.GetExtension(newProduct.Image.FileName);

                        var path = Path.Combine(images.PhysicalPath, randomImageName);

                        using var stream = new FileStream(path, FileMode.Create);

                        newProduct.Image.CopyTo(stream);

                        product.ImagePath = randomImageName;
                    }

                    //Eğer if'e girerse Image'ıda kaydedip Db'ye eklicek, Image yoksa if'e girmeden sadece Product'ı kaydedicek Db'ye(Image'ı null kaydeder).
                 

                    _context.Products.Add(product);
                    _context.SaveChanges();
                    TempData["success"] = "Ürün ekleme başarılı";
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ViewBag.ErrorMessage = $"Veritabanı ile ilgili bir hata oluştu, alınan hata = {exception.Message} - {exception.InnerException}";
                    result = View();
                }
            }

            ViewBag.Expire = new Dictionary<string, int>()
            {
                 {"1 Ay", 1},
                 {"3 Ay", 3},
                 {"6 Ay", 6},
                 {"12 Ay", 12}
            };

            ViewBag.ColorSelect = new SelectList(new List<ColorSelectList>()
            {
                 new(){Data="Mavi", Value="Mavi"},
                 new(){Data="Kırmızı", Value="Kırmızı"},
                 new(){Data="Sarı", Value="Sarı"},
            }, nameof(ColorSelectList.Value), nameof(ColorSelectList.Data));

            List<Category> categories = _context.Categories.ToList();
            ViewBag.categorySelect = new SelectList(categories, nameof(Category.Id), nameof(Category.Name));

            return result;
        }


        [ServiceFilter(typeof(NotFoundFilter))]
        [Route("urunler/urun/{productid}", Name = "product")]
        public IActionResult GetById(int productid)
        {
            Product? product = _context.Products.Find(productid);

            return View(_mapper.Map<ProductViewModel>(product));
        }

        [ServiceFilter(typeof(NotFoundFilter))]
        public IActionResult Update(int id)
        {
            Product? toBeUpdated = _context.Products.FirstOrDefault(x => x.Id == id);

            if (toBeUpdated == null) return RedirectToAction("Index", "Product");

            ViewBag.ExpireValue = toBeUpdated.Expire;
            ViewBag.Expire = new Dictionary<string, int>()
            {
               {"1 Ay", 1},
               {"3 Ay", 3},
               {"6 Ay", 6},
               {"12 Ay", 12}
            };

            ViewBag.ColorSelect = new SelectList(new List<ColorSelectList>()
            {
                new(){Data="Mavi", Value="Mavi"},
                new(){Data="Kırmızı", Value="Kırmızı"},
                new(){Data="Sarı", Value="Sarı"},
            }, nameof(ColorSelectList.Value), nameof(ColorSelectList.Data), toBeUpdated.Color);

            List<Category> categories = _context.Categories.ToList();
            ViewBag.categorySelect = new SelectList(categories, nameof(Category.Id), nameof(Category.Name), nameof(toBeUpdated.CategoryId));

            return View(_mapper.Map<ProductUpdateViewModel>(toBeUpdated));
        }

        [HttpPost]
        public IActionResult Update(ProductUpdateViewModel toBeUpdated)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ExpireValue = toBeUpdated.Expire;
                ViewBag.Expire = new Dictionary<string, int>()
                {
                    {"1 Ay", 1},
                    {"3 Ay", 3},
                    {"6 Ay", 6},
                    {"12 Ay", 12}
                };

                ViewBag.ColorSelect = new SelectList(new List<ColorSelectList>()
                {
                    new(){Data="Mavi", Value="Mavi"},
                    new(){Data="Kırmızı", Value="Kırmızı"},
                    new(){Data="Sarı", Value="Sarı"},
                }, nameof(ColorSelectList.Value), nameof(ColorSelectList.Data), toBeUpdated.Color);

                List<Category> categories = _context.Categories.ToList();
                ViewBag.categorySelect = new SelectList(categories, nameof(Category.Id), nameof(Category.Name), toBeUpdated.CategoryId);

                return View(toBeUpdated);
            }

            if (toBeUpdated.Image != null && toBeUpdated.Image.Length > 0)
            {
                var root = _fileProvider.GetDirectoryContents("wwwroot");

                var images = root.First(x => x.Name == "images");

                var randomImageName = Guid.NewGuid() + Path.GetExtension(toBeUpdated.Image.FileName);

                var path = Path.Combine(images.PhysicalPath, randomImageName);

                using var stream = new FileStream(path, FileMode.Create);

                toBeUpdated.Image.CopyTo(stream);

                toBeUpdated.ImagePath = randomImageName;
            }

            Product product = _mapper.Map<Product>(toBeUpdated);


            _context.Products.Update(product);
            _context.SaveChanges();
            TempData["success"] = "Ürün güncellendi";
            return RedirectToAction("Index", "Product");
        }

        [ServiceFilter(typeof(NotFoundFilter))]
        [HttpGet("{id}")]
        public IActionResult Delete(int id)
        {
            Product toBeDeleted = _context.Products.FirstOrDefault(x => x.Id == id);

            if (toBeDeleted == null) return RedirectToAction("Index", "Product");

            _context.Products.Remove(toBeDeleted);
            _context.SaveChanges();
            TempData["success"] = "Ürün silindi";
            return RedirectToAction("Index", "Product");
        }


        [Route("[controller]/[action]/{page}/{pageSize}", Name = "productpage")]
        public IActionResult Pages(int page, int pageSize)
        {
            var routes = Request.RouteValues;

            HashSet<Product> products = _context.Products.Skip((page - 1) * pageSize).Take(pageSize).ToHashSet();

            return View(_mapper.Map<HashSet<ProductViewModel>>(products));
        }


        [AcceptVerbs("GET", "POST")]
        public IActionResult HasProductName(string Name)
        {
            bool anyProducts = _context.Products.Any(x => x.Name.ToLower() == Name.ToLower());

            return anyProducts ? Json("Kaydetmeye çalıştığınız ürün ismi veritabanında bulunmaktadır") : Json(true);
        }

    }
}
