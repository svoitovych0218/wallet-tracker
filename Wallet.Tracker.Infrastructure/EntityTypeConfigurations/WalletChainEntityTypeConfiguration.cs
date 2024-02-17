namespace Wallet.Tracker.Infrastructure.EntityTypeConfigurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wallet.Tracker.Domain.Models.Entities;

public class WalletChainEntityTypeConfiguration : IEntityTypeConfiguration<WalletChain>
{
    public void Configure(EntityTypeBuilder<WalletChain> builder)
    {
        builder.HasKey(s => new { s.WalletAddress, s.ChainId });

        builder.Property(s => s.WalletAddress).IsRequired();
        builder.Property(s => s.ChainId).IsRequired();

        builder.HasOne(s => s.Wallet).WithMany(s => s.TrackingChains).HasForeignKey(s => s.WalletAddress);
        builder.HasOne(s => s.Chain).WithMany().HasForeignKey(s => s.ChainId);
    }
}
