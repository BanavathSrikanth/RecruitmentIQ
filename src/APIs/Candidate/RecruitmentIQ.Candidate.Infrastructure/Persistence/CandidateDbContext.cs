using Microsoft.EntityFrameworkCore;
using CandidateEntity = RecruitmentIQ.Candidate.Domain.Entities.Candidate;

namespace RecruitmentIQ.Candidate.Infrastructure.Persistence;

public sealed class CandidateDbContext : DbContext
{
    public CandidateDbContext(
        DbContextOptions<CandidateDbContext> options)
        : base(options)
    {
    }

    public DbSet<CandidateEntity> Candidates => Set<CandidateEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CandidateEntity>(entity =>
        {
            entity.ToTable("candidates");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Id)
                .HasColumnName("id");

            entity.Property(x => x.TenantId)
                .HasColumnName("tenant_id")
                .IsRequired();

            entity.Property(x => x.NormalizedEmail)
                .HasColumnName("normalized_email");

            entity.Property(x => x.NormalizedPhone)
                .HasColumnName("normalized_phone");
        });
    }
}