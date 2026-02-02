using FeatureFlags.Application.Services;
using FeatureFlags.Domain.context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FeatureFlags.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeatureFlagsController : ControllerBase
    {
        private readonly FeatureEvaluationService _service;

        public FeatureFlagsController(FeatureEvaluationService service)
        {
            _service = service;
        }

        [HttpGet("{key}/evaluate")]
        public IActionResult Evaluate(
            string key,
            string? userId,
            string? groupId,
            string? region)
        {
            var enabled = _service.IsEnabled(
                key,
                new FeatureContext(userId, groupId, region));

            return Ok(new { feature = key, enabled });
        }
    }
}