namespace Wallet.Tracker.Infrastructure.EntityTypeConfigurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using Wallet.Tracker.Domain.Models.Entities;

public class Erc20TransactionEntityTypeConfiguration : IEntityTypeConfiguration<Erc20Transaction>
{
    public void Configure(EntityTypeBuilder<Erc20Transaction> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(s => s.TxHash);
        builder.Property(s => s.WalletAddress);
        builder.Property(s => s.TransferType).HasConversion<int>();
        builder.Property(s => s.At).HasColumnType("timestamptz");
        builder.Property(s => s.Symbol);
        builder.Property(s => s.ContractAddress);
        builder.Property(s => s.Amount);
        builder.Property(s => s.NativeValue);
        builder.Property(s => s.TokenName);
    }
}
