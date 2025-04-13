using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
    {
        public void Configure(EntityTypeBuilder<SaleItem> builder)
        {
            builder.ToTable("SaleItems");

            builder.HasKey(si => si.Id);
            builder.Property(si => si.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

            builder.Property(s => s.Product)
                .HasConversion<string>()
                .HasMaxLength(50);

            builder.Property(si => si.Quantity).IsRequired();
            builder.Property(si => si.UnitPrice).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(si => si.Discount).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(si => si.IsCancelled).IsRequired().HasDefaultValue(false);

            builder.HasOne<Sale>()
                .WithMany(s => s.Items)
                .HasForeignKey(s => s.SaleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
