using Microsoft.AspNetCore.Mvc;
using WebApplication1.Contracts;

namespace WebApplication1.Controllers.FormHandeler.v2
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class FormHandelerController : ControllerBase
    {
        private readonly IFormHandeler _formHandeler;

        public FormHandelerController(IFormHandeler formHandeler)
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
