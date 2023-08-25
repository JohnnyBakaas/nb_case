namespace WebApplication1.Contracts.ContactForm.v2
{
    public interface IContactForm
    {
        IFormDataResponse SubmitForm(IFormData data);
    }
}
