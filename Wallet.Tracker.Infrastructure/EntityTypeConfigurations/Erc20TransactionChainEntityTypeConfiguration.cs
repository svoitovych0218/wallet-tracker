namespace Wallet.Tracker.Infrastructure.EntityTypeConfigurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using Wallet.Tracker.Domain.Models.Entities;

public class Erc20TransactionChainEntityTypeConfiguration : IEntityTypeConfiguration<Erc20TransactionChain>
{
    public void Configure(EntityTypeBuilder<Erc20TransactionChain> builder)
    {
        builder.HasKey(s => new { s.Erc20TransactionId, s.ChainId });
        builder.Property(s => s.Erc20TransactionId);
        builder.Property(s => s.ChainId);

        builder.HasOne(s => s.Chain).WithMany().HasForeignKey(s => s.ChainId);
        builder.HasOne(s => s.Erc20Transaction).WithOne(s => s.TransactionChain).HasForeignKey<Erc20TransactionChain>(s => s.Erc20TransactionId);
    }
}
