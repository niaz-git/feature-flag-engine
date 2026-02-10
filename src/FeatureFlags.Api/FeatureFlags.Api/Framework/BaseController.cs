using FeatureFlags.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FeatureFlags.Api.Framework
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {

        protected new IActionResult Response(CommonResponse response)
        {
            var returnResult = response.IsValid ? response.Data : new { Error = response.Exception.Message };
            return new ObjectResult(returnResult) { StatusCode = response.HttpCode };
        }
    }
}
