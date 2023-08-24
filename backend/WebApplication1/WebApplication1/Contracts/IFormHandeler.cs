namespace WebApplication1.Contracts
{
    public interface IFormHandeler
    {
        IFormDataResponse SubmitForm(IFormData data);
    }
}
