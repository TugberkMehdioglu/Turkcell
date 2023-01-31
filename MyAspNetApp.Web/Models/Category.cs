namespace MyAspNetApp.Web.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //Navigation Properties
        public List<Product> Products { get; set; }
    }
}
