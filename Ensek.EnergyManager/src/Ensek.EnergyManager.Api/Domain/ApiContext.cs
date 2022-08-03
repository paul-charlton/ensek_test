using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ensek.EnergyManager.Api.Domain;

internal class ApiContext : DbContext
{
    public ApiContext(DbContextOptions options) : base(options)
    {
    }

    protected ApiContext()
    {
    }

    public virtual DbSet<AccountEntity> Accounts { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder
            .ApplyConfiguration(new AccountConfiguration())
            .ApplyConfiguration(new MeterReadingConfiguration());
    }

    private class AccountConfiguration : IEntityTypeConfiguration<AccountEntity>
    {
        public void Configure(EntityTypeBuilder<AccountEntity> builder)
        {
            builder.ToTable("Accounts");

            builder.HasKey(x => x.AccountId);

            builder.OwnsOne(x => x.Name);

            builder
                .HasMany(x => x.MeterReadings)
                .WithOne(x => x.Account);
        }
    }

    private class MeterReadingConfiguration : IEntityTypeConfiguration<MeterReadingEntity>
    {
        public void Configure(EntityTypeBuilder<MeterReadingEntity> builder)
        {
            builder.ToTable(nameof(AccountEntity.MeterReadings));

            builder.HasKey(x => x.MeterReadingId);

            builder.Property(x => x.MeterReadingValue);

            builder.Property(x => x.ReadingDateTimeOffset);

        }
    }
}
