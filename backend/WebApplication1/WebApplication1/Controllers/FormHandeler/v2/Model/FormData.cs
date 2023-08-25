using WebApplication1.Contracts.ContactForm.v2;

namespace WebApplication1.Controllers.FormHandeler.v2.Model
{
    public class FormData : IFormData
    {
        public string FirstName { get; set; }
        public string MidleName { get; set; }
        public string LastName { get; set; }
        public string Applicant { get; set; }
        public string Job { get; set; }
        public DateTime Date { get; set; }
        public string AboutTheJob { get; set; }
        public string Email { get; set; }
        public int PhoneNumberPrefix { get; set; }
        public int PhoneNumber { get; set; }
    }
}
