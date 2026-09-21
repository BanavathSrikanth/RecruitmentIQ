using Microsoft.AspNetCore.Mvc;

namespace RecruitmentIQ.Candidate.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DeduplicateCandidateController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Deduplicate()
    {
        return Ok();
    }
}