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
    public class TransferProductConfigurations : IEntityTypeConfiguration<TransferProduct>
    {
        public void Configure(EntityTypeBuilder<TransferProduct> builder)
        {
            builder.HasKey(TP => new { TP.Id, TP.TransferId, TP.ProductId });
            // Quantity => constrain
            builder.Property(Trans => Trans.Quantity).IsRequired();

            //Relationship One to Many {TransferProduct , Product}
            builder.HasOne(TP => TP.Product)
                .WithMany(P => P.TransferProducts)
                .HasForeignKey(TP => TP.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            //Relationship One to Many {Transfer , TransferProduct}
            builder.HasOne(TP => TP.Transfer)
                .WithMany(T => T.TransferProducts)
                .HasForeignKey(TP => TP.TransferId);
        }
    }
}
