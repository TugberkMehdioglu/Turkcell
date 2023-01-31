namespace MyAspNetApp.Web.Models
{
    public class ErrorViewModel
    {
        public List<string> Errors { get; set; } //Hatalari burada tutucam

        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}