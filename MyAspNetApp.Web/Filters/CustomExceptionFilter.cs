using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MyAspNetApp.Web.Models;

namespace MyAspNetApp.Web.Filters
{
    public class CustomExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            //Bu Filter'ı hangi Action'a tanımlarsam, orda bir hata fırladığında OnException method'um çalışıcak.
            context.ExceptionHandled= true; //Yani bu hatayı kendimiz ele alıcaz demiş olduk

            var error = context.Exception.Message; //Hata mesajını almış olduk

            context.Result = new RedirectToActionResult("Error", "Home", new ErrorViewModel() { Errors = new List<string>() { $"{error}" } });
        }
    }
}
