using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MyAspNetApp.Web.Models;

namespace MyAspNetApp.Web.Filters
{
    public class NotFoundFilter : ActionFilterAttribute
    {
        private readonly MyContext _context;

        public NotFoundFilter(MyContext context)
        {
            _context = context;
        }

        //Bu ezdiğimiz method'ların Async olanlarıda var
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //Action Method çalışmadan önce

            //Action argümanlarından Values'larına git ve ilk değeri al demiş olduk(yani ilk argümanı al demiş olduk).
            //Values = argümana atanan değer.
            var idValue = context.ActionArguments.Values.First(); //FirstOrDefault'da var aöa olmasını beklediğimizden First yaptık.

            var id = Convert.ToInt32(idValue);
            //id'yi Db'den kontrol etmem lazım o yüzden context'e ihtiyacım var.
            var hasProduct = _context.Products.Any(x => x.Id == id);
            //true ise yoluna devam edicek, önemli olan false ise ne olucağı:
            if (hasProduct == false)
            {
                //Burda istediğim result'ı dönebilirim yada istediğim sayfaya yönlendirebilirim.
                context.Result = new RedirectToActionResult("Error", "Home", new ErrorViewModel { Errors = new List<string> { $"Id({id})'ye sahip ürün veritabanında bulunamamıştır" } });
            }
        }
    }
}
