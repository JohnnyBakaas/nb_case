using WebApplication1.Contracts.ContactForm.v1;

namespace WebApplication1.Controllers.FormHandeler.v1.Model
{
    public class FormData : IFormData
    {
        public string Applicant { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
    }
}
