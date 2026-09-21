using FluentValidation;

namespace RecruitmentIQ.Candidate.Application.Features.DeduplicateCandidate;

public sealed class DeduplicateCandidateValidator
    : AbstractValidator<DeduplicateCandidateCommand>
{
    public DeduplicateCandidateValidator()
    {
        RuleFor(x => x)
            .Must(x => !string.IsNullOrWhiteSpace(x.Email) ||
                       !string.IsNullOrWhiteSpace(x.Phone))
            .WithMessage("Email or phone number is required.");
    }
}