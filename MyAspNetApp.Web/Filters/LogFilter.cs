using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace MyAspNetApp.Web.Filters
{
    public class LogFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //Action Method çalışmadan önce


            //Mesela Dubag'a (Output penceresine(npm'in yanındaki)) Log atalım:
            Debug.WriteLine("Action method çalışmadan önce"); //Debug yazınca (ctrl + .)yap ki referansını al.
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            //Action Method çalıştıktan sonra
            Debug.WriteLine("Action method çalıştıktan sonra");
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            //Sonuç üretilmeden önce
            Debug.WriteLine("Action method sonuç üretilmeden önce");
        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {
            //Sonuç üretildikten sonra
            Debug.WriteLine("Action method sonuç üretildikten sonra");
        }
    }


    //OnActionExecuting ve OnActionExecuted bizim ActionFilter'ımıza karşılık geliyor.
    //OnResultExecuting ve OnResultExecuted bizim ResultFilter'ımıza karşılık geliyor.
    //Yani şuan 2 tane Filter'ın(ActionFilter - ResultFilter) kodlamasını görüyoruz.
}
