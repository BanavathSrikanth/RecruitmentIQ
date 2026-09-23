namespace RecruitmentIQ.Candidate.Domain.Entities;

public sealed class Candidate
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public string? NormalizedEmail { get; set; }

    public string? NormalizedPhone { get; set; }
}