namespace WebApplication1.Contracts.ContactForm.v1
{
    public interface IFormData
    {
        string Applicant { get; set; }
        string Email { get; set; }
        string Name { get; set; }
        string Phone { get; set; }
    }
}
