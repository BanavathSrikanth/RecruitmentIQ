namespace RecruitmentIQ.Candidate.Application.Features.DeduplicateCandidate;

public sealed record DeduplicateCandidateCommand(
    string? Email,
    string? Phone);