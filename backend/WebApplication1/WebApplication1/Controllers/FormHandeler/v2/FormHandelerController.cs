using Microsoft.AspNetCore.Mvc;
using WebApplication1.Contracts.ContactForm.v1;

namespace WebApplication1.Controllers.FormHandeler.v2
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class FormHandelerController : ControllerBase
    {
        private readonly IContactForm _formHandeler;

        public FormHandelerController(IContactForm formHandeler)
        {
            _formHandeler = formHandeler;
        }

        /*
        [HttpPost("Submit")]
        public IActionResult Submit([FromBody] FormData data)
        {
        }
         */
    }
}
