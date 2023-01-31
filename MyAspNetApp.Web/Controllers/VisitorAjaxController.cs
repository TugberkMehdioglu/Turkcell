using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyAspNetApp.Web.Models;
using MyAspNetApp.Web.ViewModels;

namespace MyAspNetApp.Web.Controllers
{
    [Route("[Controller]/[Action]")]
    public class VisitorAjaxController : Controller
    {
        private readonly IMapper _mapper;
        private readonly MyContext _context;

        public VisitorAjaxController(IMapper mapper, MyContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> VisitorCommentList()
        {
            HashSet<Visitor> visitors = _context.Visitors.OrderByDescending(x => x.CreatedDate).ToHashSet();
            HashSet<VisitorViewModel> visitorViewModels = _mapper.Map<HashSet<VisitorViewModel>>(visitors);

            return Json(visitorViewModels);
        }

        [HttpPost]
        public IActionResult SaveVisitorComment(VisitorViewModel visitorViewModel)
        {
            Visitor visitor=_mapper.Map<Visitor>(visitorViewModel);
            visitor.CreatedDate= DateTime.Now;

            _context.Visitors.Add(visitor);
            _context.SaveChanges();

            return Json(new { IsSuccess = "true" });
        }
    }
}
