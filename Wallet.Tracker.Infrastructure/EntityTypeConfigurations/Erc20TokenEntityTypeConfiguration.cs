namespace Wallet.Tracker.Infrastructure.EntityTypeConfigurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wallet.Tracker.Domain.Models.Entities;

public class Erc20TokenEntityTypeConfiguration : IEntityTypeConfiguration<Erc20Token>
{
    public void Configure(EntityTypeBuilder<Erc20Token> builder)
    {
        builder.HasKey(s => new { s.Address, s.ChainId });

        builder.Property(s => s.Address);
        builder.Property(s => s.ChainId);
        builder.Property(s => s.Name);
        builder.Property(s => s.Symbol);
        builder.Property(s => s.ExistAtCoinmarketCap);
        builder.Property(s => s.ContractCodePublished);

        builder.HasOne(s => s.Chain).WithMany().HasForeignKey(s => s.ChainId);
    }
}
