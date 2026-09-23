using Microsoft.EntityFrameworkCore;
using RecruitmentIQ.Candidate.Application.Features.DeduplicateCandidate;
using RecruitmentIQ.Candidate.Infrastructure.Persistence;

namespace RecruitmentIQ.Candidate.Infrastructure.Repositories;

public sealed class CandidateDeduplicationRepository
    : ICandidateDeduplicationRepository
{
    private readonly CandidateDbContext _dbContext;

    public CandidateDeduplicationRepository(
        CandidateDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<CandidateDuplicateMatch>> FindMatchesAsync(
        Guid tenantId,
        string? normalizedEmail,
        string? normalizedPhone,
        CancellationToken cancellationToken = default)
    {
        var candidates = await _dbContext.Candidates
            .AsNoTracking()
            .Where(candidate =>
                candidate.TenantId == tenantId &&
                (
                    (normalizedEmail != null &&
                     candidate.NormalizedEmail == normalizedEmail)
                    ||
                    (normalizedPhone != null &&
                     candidate.NormalizedPhone == normalizedPhone)
                ))
            .Select(candidate => new CandidateDuplicateMatch(
                candidate.Id,
                normalizedEmail != null &&
                candidate.NormalizedEmail == normalizedEmail,
                normalizedPhone != null &&
                candidate.NormalizedPhone == normalizedPhone))
            .ToListAsync(cancellationToken);

        return candidates;
    }
}