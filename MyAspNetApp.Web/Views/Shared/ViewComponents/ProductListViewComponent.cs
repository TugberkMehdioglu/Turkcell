using Microsoft.AspNetCore.Mvc;
using MyAspNetApp.Web.Models;
using MyAspNetApp.Web.ViewModels.WrapperClasses;

namespace MyAspNetApp.Web.Views.Shared.ViewComponents
{
    public class ProductListViewComponent : ViewComponent
    {
        private readonly MyContext _context;

        public ProductListViewComponent(MyContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int type=1)
        {
            HashSet<ProductListComponentViewModel> products = _context.Products.Select(x => new ProductListComponentViewModel()
            {
                Description = x.Description,
                Name = x.Name
            }).ToHashSet();

            if (type==1)
            {
                return View("Default", products);
            }
            else
            {
                return View("Type", products);
            }
        }
    }
}
