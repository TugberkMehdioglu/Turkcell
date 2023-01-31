using Microsoft.AspNetCore.Razor.TagHelpers;

namespace MyAspNetApp.Web.TagHelpers
{
    /*
     * Class ismimin son eki çok önemli:
     * TagHelper'dan önceki kısım; yani Italic, bizim TagHelper'ımızın ismi olucak.
     */
    public class ItalicTagHelper : TagHelper
    {
        //Inheritance aldıktan sonra override edip, Process'i seçtik (ProcessAsync'da var ama şimdi async method kullanmicaz).
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            //İtalik yapmak için: output parametresi üzerinden işlemimizi gerçekleştiricez.

            /*
             * Şimdi ben <i>ahmet</i> yapmak istiyorum
             * output.PreContent demek: ilk tag'e karşılık geliyor(<i>'ye karşılık geliyor).
             */
            output.PreContent.SetHtmlContent("<i>");

            //Şimdi de son tag'i yakalamam lazım (</i>'yi yakalamam lazım)
            output.PostContent.SetHtmlContent("</i>");
        }
    }
}
