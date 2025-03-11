using FocusXp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FocusXp.Infrastructure.Mapping;

public class XpTransactionMapping : IEntityTypeConfiguration<XpTransaction>
{
    public void Configure(EntityTypeBuilder<XpTransaction> builder)
    {
        builder.Property(transaction => transaction.Id)
            .ValueGeneratedNever();

        builder.HasOne(transaction => transaction.User)
            .WithMany(user => user.XpTransactions)
            .HasForeignKey(transaction => transaction.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(transaction => transaction.UserId)
            .HasDatabaseName("ix_xp_transactions_user_id");

        builder.HasIndex(transaction => transaction.ReferenceId)
            .HasDatabaseName("ix_xp_transactions_reference_id");

        builder.HasIndex(transaction => transaction.ReferenceType)
            .HasDatabaseName("ix_xp_transactions_reference_type");
    }
}