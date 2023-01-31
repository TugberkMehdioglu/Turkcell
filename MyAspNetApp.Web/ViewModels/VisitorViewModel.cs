namespace MyAspNetApp.Web.ViewModels
{
    public class VisitorViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedDate { get; set; }

        //Lambda ile girdiğinde sadece get'i olur prop'un
        public string Date => CreatedDate.ToShortDateString();
        //Sadece tarih bilgisini aldık, saat bilgisi gelmez bu sayede

    }
}
