namespace RecruitmentIQ.Candidate.Application.Features.DeduplicateCandidate;

public static class CandidateContactNormalizer
{
    public static string? NormalizeEmail(string? email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return null;

        return email.Trim().ToLowerInvariant();
    }

    public static string? NormalizePhone(string? phone)
    {
        if (string.IsNullOrWhiteSpace(phone))
            return null;

        return new string(
            phone.Where(char.IsDigit).ToArray());
    }
}