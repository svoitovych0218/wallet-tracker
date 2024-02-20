namespace Wallet.Tracker.Infrastructure.EntityTypeConfigurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wallet.Tracker.Domain.Models.Entities;

public class WalletEntityTypeConfiguration : IEntityTypeConfiguration<WalletData>
{
    public void Configure(EntityTypeBuilder<WalletData> builder)
    {
        builder.HasKey(s => s.Address);

        builder.Property(s => s.Address).IsRequired();
        builder.Property(s => s.Title).IsRequired();
        builder.Property(s => s.CreatedAt).HasColumnType("timestamptz");
        builder.Property(s => s.IsDeleted).HasDefaultValue(false);

        builder.HasMany(s => s.Erc20Transactions).WithOne().HasForeignKey(s => s.WalletAddress);
    }
}
