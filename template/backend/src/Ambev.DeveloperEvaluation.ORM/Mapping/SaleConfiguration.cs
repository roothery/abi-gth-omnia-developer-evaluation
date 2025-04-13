using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.ToTable("Sales");

            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

            builder.Property(s => s.SaleNumber).IsRequired().HasMaxLength(50);
            builder.HasIndex(s => s.SaleNumber).IsUnique();
            builder.Property(s => s.SaleDate).IsRequired();

            builder.Property(s => s.Customer)
                .HasConversion<string>()
                .HasMaxLength(50);

            builder.Property(s => s.Branch)
                .HasConversion<string>()
                .HasMaxLength(50);

            builder.HasMany(s => s.Items)
                .WithOne()
                .HasForeignKey(s => s.SaleId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(s => s.IsCancelled).IsRequired();
        }
    }
}
