using RecruitmentIQ.Candidate.Application.Features.DeduplicateCandidate;

namespace RecruitmentIQ.Candidate.Infrastructure.Repositories;

public sealed class CandidateDeduplicationRepository
    : ICandidateDeduplicationRepository
{
    public Task<IReadOnlyList<CandidateDuplicateMatch>> FindMatchesAsync(
        Guid tenantId,
        string? normalizedEmail,
        string? normalizedPhone,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}