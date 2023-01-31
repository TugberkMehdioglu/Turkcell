using Microsoft.AspNetCore.Mvc;

namespace MyAspNetApp.Web.Controllers
{
    [Route("[Controller]/[Action]")]
    public class AppSettingController : Controller
    {
        //Appsetting.json'da data okumak için
        private readonly IConfiguration _configuration;

        public AppSettingController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            //1. okuma biçimi: (tek değer varsa bu şekil)
            ViewBag.baseUrl = _configuration["baseUrl"];

            //2. okuma biçimi: (Keys içindeki Sms'i çektik)
            ViewBag.smsKey = _configuration["Keys:Sms"];

            //3. okuma biçimi: (Keys içindeki email'i çektik)
            ViewBag.emailKey = _configuration.GetSection("Keys")["email"];

            return View();
        }
    }
}
