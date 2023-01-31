using AutoMapper;
using MyAspNetApp.Web.Models;
using MyAspNetApp.Web.ViewModels;
using MyAspNetApp.Web.ViewModels.WrapperClasses;

namespace MyAspNetApp.Web.Mapping
{
    public class ModelViewMapping : Profile
    {
        public ModelViewMapping()
        {
            CreateMap<Product, ProductViewModel>().ReverseMap();
            CreateMap<Visitor, VisitorViewModel>().ReverseMap();
            CreateMap<Product, ProductUpdateViewModel>().ReverseMap();
            CreateMap<Category, CategoryViewModel>().ReverseMap();
        }
    }
}
