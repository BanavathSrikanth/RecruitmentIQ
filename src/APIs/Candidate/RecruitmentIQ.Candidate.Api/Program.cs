using Company.Common.Authentication.Extensions;
using Microsoft.EntityFrameworkCore;
using RecruitmentIQ.Candidate.Application.Abstractions;
using RecruitmentIQ.Candidate.Application.Features.DeduplicateCandidate;
using RecruitmentIQ.Candidate.Infrastructure.Identity;
using RecruitmentIQ.Candidate.Infrastructure.Persistence;
using RecruitmentIQ.Candidate.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblyContaining<DeduplicateCandidateHandler>());

builder.Services.AddDbContext<CandidateDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("CandidateDatabase")));

builder.Services.AddScoped<
    ICandidateDeduplicationRepository,
    CandidateDeduplicationRepository>();

builder.Services.AddScoped<ICurrentTenant, CurrentTenant>();

builder.Services.AddCommonEntraAuthentication(builder.Configuration);

builder.Services.AddCommonTenantResolution();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCommonEntraAuthentication();

app.UseCommonTenantResolution();

app.MapControllers();

app.Run();