using RecruitmentIQ.Candidate.Application.Features.DeduplicateCandidate;

namespace RecruitmentIQ.Candidate.UnitTests.DeduplicateCandidate;

public class CandidateContactNormalizerTests
{
    [Fact]
    public void NormalizeEmail_ShouldTrimAndLowercase()
    {
        var result =
            CandidateContactNormalizer.NormalizeEmail("  John@GMAIL.COM ");

        Assert.Equal("john@gmail.com", result);
    }

    [Fact]
    public void NormalizeEmail_ShouldReturnNull_WhenEmpty()
    {
        var result =
            CandidateContactNormalizer.NormalizeEmail(" ");

        Assert.Null(result);
    }

    [Fact]
    public void NormalizePhone_ShouldKeepOnlyDigits()
    {
        var result =
            CandidateContactNormalizer.NormalizePhone("+91 98765-43210");

        Assert.Equal("919876543210", result);
    }

    [Fact]
    public void NormalizePhone_ShouldReturnNull_WhenEmpty()
    {
        var result =
            CandidateContactNormalizer.NormalizePhone(null);

        Assert.Null(result);
    }
}