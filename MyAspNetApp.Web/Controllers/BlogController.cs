using Microsoft.AspNetCore.Mvc;

namespace MyAspNetApp.Web.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Article(string name, int id)
        {
            //URL => blog/article/makale-ismi/id
            //blog => Controller'dır
            //article => Action method'un ismi
            //makale-ismi => Action parametresi olan name'e bind olucak
            //id => Action parametresi olan id'ye bind olucak

            return View();
        }
    }
}
