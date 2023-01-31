using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;
using MyAspNetApp.Web.Models;

namespace MyAspNetApp.Web.TagHelpers
{
    //Nasıl bir TagHelper vericek bana? TagHelper kısmını atıcak ve ProductShow'da küçük harf olucak ve iki kelime arasına - koyucak.
    public class ProductShowTagHelper : TagHelper
    {

        //Burda bir Product göstermek için bir prop tanımlıyorum.
        public Product Product { get; set; } = null!;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            //Product nesnesini div içinde ul tag'lerinde göstermek istiyorum
            output.TagName = "div"; //<div></div> böyle bir div açıldı şuan bana
            //şuan <div><ul></ul></div> yapmak istiyorum, böylece ul tag'larıyla birlikte Product'ı göstermek istiyorum
            //Burda istersen listelemede yapabilirsin, herşeyi yapabilirsin.

            //Content.SetHtmlContent() sayesinde: div'in içeriğini set edicem(<div>Burayı set edicem</div>)
            //@ koydumki alt satıra geçe geçe rahatça yazıyım diye
            output.Content.SetHtmlContent(@$"<ul class='list-group'>
            <li class='list-group-item'>{Product.Id}</li>
            <li class='list-group-item'>{Product.Name}</li>
            <li class='list-group-item'>{Product.Price}</li>
            <li class='list-group-item'>{Product.Stock}</li>
            </ul>"); //içeride Bootstrap'de kullandık.

            //Buranın içerisinde istediğin html'i yazabilirsin, ister ComboBox ister DropdownList TagHelper'ı yap sana kalmış.
        }
    }
}
