using WebApplication1.Contracts.ContactForm.v1;

namespace WebApplication1.Controllers.FormHandeler.v1.Model
{
    public class FormDataResponse : IFormDataResponse
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
    }
}
