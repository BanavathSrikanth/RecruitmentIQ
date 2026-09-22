using MediatR;
using RecruitmentIQ.Candidate.Application.Abstractions;

namespace RecruitmentIQ.Candidate.Application.Features.DeduplicateCandidate;

public sealed class DeduplicateCandidateHandler
    : IRequestHandler<DeduplicateCandidateCommand, DeduplicateCandidateResult>
{
    private readonly ICandidateDeduplicationRepository _repository;
    private readonly ICurrentTenant _currentTenant;

    public DeduplicateCandidateHandler(
        ICandidateDeduplicationRepository repository,
        ICurrentTenant currentTenant)
    {
        _repository = repository;
        _currentTenant = currentTenant;
    }

    public async Task<DeduplicateCandidateResult> Handle(
        DeduplicateCandidateCommand request,
        CancellationToken cancellationToken)
    {
        var normalizedEmail =
            CandidateContactNormalizer.NormalizeEmail(request.Email);

        var normalizedPhone =
            CandidateContactNormalizer.NormalizePhone(request.Phone);

        var matches = await _repository.FindMatchesAsync(
            _currentTenant.TenantId,
            normalizedEmail,
            normalizedPhone,
            cancellationToken);

        if (matches.Count == 0)
        {
            return new DeduplicateCandidateResult(
                false,
                null,
                []);
        }

        var match = matches[0];

        var matchedFields = new List<string>();

        if (match.EmailMatched)
            matchedFields.Add("Email");

        if (match.PhoneMatched)
            matchedFields.Add("Phone");

        return new DeduplicateCandidateResult(
            true,
            match.CandidateId,
            matchedFields);
    }
}