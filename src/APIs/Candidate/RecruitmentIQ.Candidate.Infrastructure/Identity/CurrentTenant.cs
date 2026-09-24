using Company.Common.Authentication.Tenant;
using RecruitmentIQ.Candidate.Application.Abstractions;

namespace RecruitmentIQ.Candidate.Infrastructure.Identity;

public sealed class CurrentTenant : ICurrentTenant
{
    private readonly ITenantContext _tenantContext;

    public CurrentTenant(ITenantContext tenantContext)
    {
        _tenantContext = tenantContext;
    }

    public Guid TenantId => _tenantContext.TenantId;
}