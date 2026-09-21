namespace RecruitmentIQ.Candidate.Application.Features.DeduplicateCandidate;

public class DeduplicateCandidateHandler
{
    public async Task<DeduplicateCandidateResult> Handle(DeduplicateCandidateCommand command, CancellationToken cancellationToken = default)
    {
        return new DeduplicateCandidateResult();
    }
}