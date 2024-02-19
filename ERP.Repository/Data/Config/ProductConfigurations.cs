using ERP.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Data.Config
{
    public class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(P => P.AddedBy)
                .IsRequired(false);

            builder.Property(P => P.ProductSellPrice)
                .HasColumnType("decimal(10, 2)");

            builder.Property(P => P.ProductCostPrice)
                .HasColumnType("decimal(10, 2)");

            builder.Property(P => P.ProductName)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(P => P.ProductBarcode)
                .IsRequired()
                .HasMaxLength(255);

            builder.HasOne(P => P.Employee)
                .WithMany(E => E.Products)
                .HasForeignKey(P => P.AddedBy);

            
        }
    }
}
