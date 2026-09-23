using RecruitmentIQ.Candidate.Application.Abstractions;
using RecruitmentIQ.Candidate.Application.Features.DeduplicateCandidate;

namespace RecruitmentIQ.Candidate.UnitTests.DeduplicateCandidate;

public class DeduplicateCandidateHandlerTests
{
    private static readonly Guid TenantId =
        Guid.Parse("11111111-1111-1111-1111-111111111111");

    private static readonly Guid CandidateId =
        Guid.Parse("22222222-2222-2222-2222-222222222222");

    [Fact]
    public async Task Handle_ShouldReturnNoDuplicate_WhenNoMatchesFound()
    {
        var repository = new FakeCandidateDeduplicationRepository([]);
        var tenant = new FakeCurrentTenant(TenantId);

        var handler = new DeduplicateCandidateHandler(
            repository,
            tenant);

        var command = new DeduplicateCandidateCommand(
            "John@Gmail.com",
            "+91 98765-43210");

        var result = await handler.Handle(
            command,
            CancellationToken.None);

        Assert.False(result.IsDuplicate);
        Assert.Null(result.CandidateId);
        Assert.Empty(result.MatchedFields);
    }

    [Fact]
    public async Task Handle_ShouldReturnEmailDuplicate_WhenEmailMatches()
    {
        var repository = new FakeCandidateDeduplicationRepository(
        [
            new CandidateDuplicateMatch(
                CandidateId,
                EmailMatched: true,
                PhoneMatched: false)
        ]);

        var tenant = new FakeCurrentTenant(TenantId);

        var handler = new DeduplicateCandidateHandler(
            repository,
            tenant);

        var command = new DeduplicateCandidateCommand(
            "John@Gmail.com",
            null);

        var result = await handler.Handle(
            command,
            CancellationToken.None);

        Assert.True(result.IsDuplicate);
        Assert.Equal(CandidateId, result.CandidateId);
        Assert.Contains("Email", result.MatchedFields);
    }

    [Fact]
    public async Task Handle_ShouldReturnPhoneDuplicate_WhenPhoneMatches()
    {
        var repository = new FakeCandidateDeduplicationRepository(
        [
            new CandidateDuplicateMatch(
                CandidateId,
                EmailMatched: false,
                PhoneMatched: true)
        ]);

        var tenant = new FakeCurrentTenant(TenantId);

        var handler = new DeduplicateCandidateHandler(
            repository,
            tenant);

        var command = new DeduplicateCandidateCommand(
            null,
            "+91 98765-43210");

        var result = await handler.Handle(
            command,
            CancellationToken.None);

        Assert.True(result.IsDuplicate);
        Assert.Equal(CandidateId, result.CandidateId);
        Assert.Contains("Phone", result.MatchedFields);
    }

    [Fact]
    public async Task Handle_ShouldReturnBothFields_WhenEmailAndPhoneMatch()
    {
        var repository = new FakeCandidateDeduplicationRepository(
        [
            new CandidateDuplicateMatch(
                CandidateId,
                EmailMatched: true,
                PhoneMatched: true)
        ]);

        var tenant = new FakeCurrentTenant(TenantId);

        var handler = new DeduplicateCandidateHandler(
            repository,
            tenant);

        var command = new DeduplicateCandidateCommand(
            "John@Gmail.com",
            "+91 98765-43210");

        var result = await handler.Handle(
            command,
            CancellationToken.None);

        Assert.True(result.IsDuplicate);
        Assert.Equal(CandidateId, result.CandidateId);
        Assert.Contains("Email", result.MatchedFields);
        Assert.Contains("Phone", result.MatchedFields);
    }

    private sealed class FakeCurrentTenant : ICurrentTenant
    {
        public FakeCurrentTenant(Guid tenantId)
        {
            TenantId = tenantId;
        }

        public Guid TenantId { get; }
    }

    private sealed class FakeCandidateDeduplicationRepository
        : ICandidateDeduplicationRepository
    {
        private readonly IReadOnlyList<CandidateDuplicateMatch> _matches;

        public FakeCandidateDeduplicationRepository(
            IReadOnlyList<CandidateDuplicateMatch> matches)
        {
            _matches = matches;
        }

        public Task<IReadOnlyList<CandidateDuplicateMatch>> FindMatchesAsync(
            Guid tenantId,
            string? normalizedEmail,
            string? normalizedPhone,
            CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_matches);
        }
    }
}