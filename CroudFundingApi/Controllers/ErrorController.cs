using CroudFundingApi.Errors;
using CroudFundingApi.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("Error/{code}")]
    [ApiController]
    public class ErrorController : BaseController
    {
        [HttpGet]
        public IActionResult error(int code)
        {
            return new   ObjectResult(new ApiResponse(code));
        }
       
    }
}
