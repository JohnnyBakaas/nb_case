namespace WebApplication1.Contracts.ContactForm.v1
{
    public interface IContactForm
    {
        IFormDataResponse SubmitForm(IFormData data);
    }
}
