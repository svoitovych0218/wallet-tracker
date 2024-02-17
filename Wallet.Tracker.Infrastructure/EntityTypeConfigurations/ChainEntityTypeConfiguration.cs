namespace Wallet.Tracker.Infrastructure.EntityTypeConfigurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wallet.Tracker.Domain.Models.Entities;

public class ChainEntityTypeConfiguration : IEntityTypeConfiguration<Chain>
{
    public void Configure(EntityTypeBuilder<Chain> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id);
        builder.Property(s => s.Name);
    }
}
