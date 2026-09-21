namespace RecruitmentIQ.Candidate.Application.Features.DeduplicateCandidate;

public sealed record DeduplicateCandidateResult(
    bool IsDuplicate,
    Guid? CandidateId,
    IReadOnlyList<string> MatchedFields);