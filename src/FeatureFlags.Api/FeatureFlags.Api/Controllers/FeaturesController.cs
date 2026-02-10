using FeatureFlags.Api.Framework;
using FeatureFlags.Application.Interfaces;
using FeatureFlags.Application.Services;
using FeatureFlags.Domain.context;
using FeatureFlags.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FeatureFlags.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeaturesController : BaseController
    {
        private readonly IFeatureEvaluationService _evaluationService;
        private readonly IFeatureMutationService _mutationService;

        public FeaturesController(
            IFeatureEvaluationService evaluationService,
            IFeatureMutationService mutationService)
        {
            _evaluationService = evaluationService;
            _mutationService = mutationService;
        }

        [HttpGet("evaluate")]
        public async Task<IActionResult> Evaluate(
            string key,
            string? userId,
            string? groupId,
            string? region)
        {
            var enabled = await _evaluationService.IsEnabled(
                key,
                new FeatureContext(userId, groupId, region));

            return Ok(new { feature = key, enabled });
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateFeature(
            string key,
            bool defaultEnabled,
            string? description)
        {
            try
            {
                await _mutationService.CreateFeature(key, defaultEnabled, description);
                return Response(CommonResponse.Ok($"{key} data created"));
            }
            catch
            {
                return Response(CommonResponse.Invalid("Data not inserted"));
            }
        }

        [HttpPut("overrides")]
        public async Task<IActionResult> UpsertOverride(
            string key,
            string scope,
            string targetId,
            bool enabled)
        {
            try
            {
                if (!Enum.TryParse<OverrideScope>(scope, true, out var overrideScope))
                {
                    return Response(CommonResponse.NoContent($"{key} data created"));
                }
                await _mutationService.UpsertOverride(
                    key,
                    overrideScope,
                    targetId,
                    enabled);
                return Response(CommonResponse.Ok($"{key} data created"));
            }
            catch (KeyNotFoundException)
            {
                return Response(CommonResponse.NotFound("Data not inserted"));
            }
            catch
            {
                return Response(CommonResponse.Invalid("Data not inserted"));
            }
        }
    }
}
