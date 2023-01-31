using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyAspNetApp.Web.Models;
using MyAspNetApp.Web.ViewModels;

namespace MyAspNetApp.Web.Views.Shared.ViewComponents
{
    public class VisitorViewComponent : ViewComponent
    {
        private readonly MyContext _context;
        private readonly IMapper _mapper;

        public VisitorViewComponent(MyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            HashSet<Visitor> visitors = _context.Visitors.ToHashSet();

            HashSet<VisitorViewModel> visitorViewModels = _mapper.Map<HashSet<VisitorViewModel>>(visitors);

            ViewBag.visitorViewModels= visitorViewModels;

            return View();
        }
    }
}
