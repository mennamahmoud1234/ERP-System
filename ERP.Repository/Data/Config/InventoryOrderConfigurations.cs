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
    internal class InventoryOrderConfigurations : IEntityTypeConfiguration<InventoryOrder>
    {
        public void Configure(EntityTypeBuilder<InventoryOrder> builder)
        {
            builder.Property(IO => IO.Reference)
                .IsRequired()
                .HasMaxLength(255);

            //Relationship one to many {Employee , InventoryOrder}
            builder.HasOne(IO => IO.InventoryEmp)
                .WithMany(E => E.InventoryEmp)
                .HasForeignKey(IO => IO.InventoryEmployee)
                .OnDelete(DeleteBehavior.NoAction); ;

            //Relationship one to many {AcmEmployee , InventoryOrder}
            builder.HasOne(IO => IO.AccEmp)
                .WithMany(E => E.InventoryOrder)
                .HasForeignKey(IO => IO.AccEmployee).
                OnDelete(DeleteBehavior.NoAction);
            //Relationship one to many {product , InventoryOrder}
            builder.HasOne(IOP => IOP.Product)
                .WithMany(P => P.inventoryOrder)
                .HasForeignKey(IOP => IOP.ProductId)
                .OnDelete(DeleteBehavior.SetNull);

        }
    }
}
