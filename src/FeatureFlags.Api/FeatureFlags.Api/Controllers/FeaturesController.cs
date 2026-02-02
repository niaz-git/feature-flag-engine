using FeatureFlags.Application.Services;
using FeatureFlags.Domain.context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace FeatureFlags.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeaturesController : ControllerBase
    {

        private readonly FeatureEvaluationService _service;
        private readonly FeatureMutationService _featureMutationServie;

        public FeaturesController(FeatureEvaluationService service, FeatureMutationService featureMutationServie)
        {
            _service = service;
            _featureMutationServie = featureMutationServie;
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

        [HttpPost("{key}/create")]
        public IActionResult CreateFeature(
            string key,
            bool defaultEnabled,
            string? description)
        {
            try
            {
                _featureMutationServie.CreateFeature(key, defaultEnabled, description);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
           
        }
        [HttpPut("{key}/overrides")]
        public IActionResult UpsertOverride(
            string key,
            string scope,
            string targetId,
            bool enabled)
        {
            try
            {
                if (!Enum.TryParse<Domain.Enums.OverrideScope>(scope, true, out var overrideScope))
                {
                    return BadRequest("Invalid scope");
                }
                _featureMutationServie.UpsertOverride(
                    key,
                    overrideScope,
                    targetId,
                    enabled);
                return Ok();

            }catch(Exception ex)
            {
                return BadRequest();
            }
        }



    }
}
