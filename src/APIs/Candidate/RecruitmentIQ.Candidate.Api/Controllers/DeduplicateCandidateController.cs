using MediatR;
using Microsoft.AspNetCore.Mvc;
using RecruitmentIQ.Candidate.Application.Features.DeduplicateCandidate;

namespace RecruitmentIQ.Candidate.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DeduplicateCandidateController : ControllerBase
{
    private readonly IMediator _mediator;

    public DeduplicateCandidateController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<DeduplicateCandidateResult>> Deduplicate(
        [FromBody] DeduplicateCandidateCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);

        return Ok(result);
    }
}