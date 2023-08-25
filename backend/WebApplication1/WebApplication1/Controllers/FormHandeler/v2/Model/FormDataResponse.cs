using WebApplication1.Contracts.ContactForm.v2;

namespace WebApplication1.Controllers.FormHandeler.v2.Model
{
    public class FormDataResponse : IFormDataResponse
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
    }
}
