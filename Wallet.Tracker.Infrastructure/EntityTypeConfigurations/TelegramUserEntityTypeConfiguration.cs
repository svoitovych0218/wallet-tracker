namespace Wallet.Tracker.Infrastructure.EntityTypeConfigurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wallet.Tracker.Domain.Models.Entities;

internal class TelegramUserEntityTypeConfiguration : IEntityTypeConfiguration<TelegramUser>
{
    public void Configure(EntityTypeBuilder<TelegramUser> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(s => s.Id);
        builder.Property(s => s.IsSubscribed);
        builder.Property(s => s.UserName).IsRequired(false);
        builder.Property(e => e.RowVersion).HasColumnName("xmin").HasColumnType("xid").IsRowVersion();
        builder.Property(e => e.CreatedAt).HasColumnType("timestamptz").HasDefaultValueSql("CURRENT_TIMESTAMP").ValueGeneratedOnAdd();
    }
}
