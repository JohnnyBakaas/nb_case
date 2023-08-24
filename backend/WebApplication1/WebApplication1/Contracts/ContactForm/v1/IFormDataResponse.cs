namespace WebApplication1.Contracts.ContactForm.v1
{
    public interface IFormDataResponse
    {
        bool Success { get; set; }
        List<string> Errors { get; set; }
    }
}
