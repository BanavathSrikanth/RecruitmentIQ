namespace RecruitmentIQ.Candidate.Application.Features.DeduplicateCandidate;

public interface ICandidateDeduplicationRepository
{
    Task<IReadOnlyList<CandidateDuplicateMatch>> FindMatchesAsync(
        Guid tenantId,
        string? normalizedEmail,
        string? normalizedPhone,
        CancellationToken cancellationToken = default);
}

public sealed record CandidateDuplicateMatch(
    Guid CandidateId,
    bool EmailMatched,
    bool PhoneMatched);