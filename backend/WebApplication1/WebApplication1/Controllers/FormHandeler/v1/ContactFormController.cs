using Microsoft.AspNetCore.Mvc;
using WebApplication1.Contracts.ContactForm.v1;
using WebApplication1.Controllers.FormHandeler.v1.Model;

namespace WebApplication1.Controllers.FormHandeler.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ContactFormController : ControllerBase
    {
        private readonly IContactForm _formHandeler;

        public ContactFormController(IContactForm formHandeler)
        {
            _formHandeler = formHandeler;
        }

        [HttpPost("Submit")]
        public IActionResult Submit([FromBody] FormData data)
        {
            var res = (FormDataResponse)_formHandeler.SubmitForm(data);

            if (res.Errors.Any())
            {
                return BadRequest(res);
            }

            Console.WriteLine("Vi magler send greia");

            return Ok(res);
        }
    }
}
