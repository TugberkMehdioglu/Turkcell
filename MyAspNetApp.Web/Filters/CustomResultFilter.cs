using Microsoft.AspNetCore.Mvc.Filters;

namespace MyAspNetApp.Web.Filters
{
    public class CustomResultFilter : ResultFilterAttribute
    {
        private readonly string _name;
        private readonly string _value;
        //ctor'ında almış olduğu parametreler birer prop, yani IMappper yada DbContext gibi bir nesne geçmiyorum, öyle olsaydı ServiceFilter ile çözerdik işi.
        //ctor'da parametre olarak basit tipler geçtiğimden CustomResultFilter'ı istediğin yerde direkt kullanabilirsin.
        public CustomResultFilter(string name, string value)
        {
            _name = name;
            _value = value;
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            //Sonuç(Result) üretilirken

            //Sonuç üretilirken Response'un Header'ına bir data eklicez
            context.HttpContext.Response.Headers.Add(_name, _value); //HttpContext: bütün Request'lere ve Response'lara eriştiğim yer
        }
    }
}
