using Microsoft.AspNetCore.Mvc;

namespace MyAspNetApp.Web.Controllers
{
    [Route("[Controller]/[Action]")]
    public class CookieController : Controller
    {
        public IActionResult CookieCreate()
        {
            //HttpContext uygulamamızın kalbidir, tüm Request'e ve Response'a burdan erişebiliriz:
            //                                         key        , value,             CookieOptions
            HttpContext.Response.Cookies.Append("background-color", "red", new CookieOptions() { Expires = DateTime.Now.AddDays(2)});

            //Append method'u ile birlikte Key-Value şeklinde bir Cookie ekliyebiliyorum
            //3. parametre olan CookieOptions ile beraber, Cookie'yi daha da detaylandırabilirim (Ör. bu Cookie'nin ne zaman geçersiz olucağını belirleyebiliriz)
            //Expires = DateTime.Now.AddDays(2) ile bu Cookie'nin ömrünün 2 gün olmasını sağladık
            //3. parametrede Expires haricinde başak prop'larda var, mesela: HttpOnly ile script(JavaScript) tarafından bu Cookie okunmasın demiş olursun
            //IsEssential ile beraberde: bu Cookie'nin çok temel ve önemli bir Cookie olduğunuda belirtebilirsin.

            return View();
        }

        public IActionResult CookieRead()
        {
            //Cookie'yi okumak içinde Request'in Cookie'lerine gidiyoruz.
            var bgColor = HttpContext.Request.Cookies["background-color"];

            ViewBag.bgColor = bgColor;
            return View();
        }
    }
}
