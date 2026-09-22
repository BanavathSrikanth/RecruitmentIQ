namespace RecruitmentIQ.Candidate.Application.Abstractions;

public interface ICurrentTenant
{
    Guid TenantId { get; }
}