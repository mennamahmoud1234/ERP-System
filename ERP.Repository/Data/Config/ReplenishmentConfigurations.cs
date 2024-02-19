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
    public class ReplenishmentConfigurations : IEntityTypeConfiguration<Replenishment>
    {
        public void Configure(EntityTypeBuilder<Replenishment> builder)
        {
            // ProductMinquantity  => constrain  ?? ask
            builder.Property(Rep => Rep.ProductMinquantity).IsRequired();

            //  ProductMaxquantity => constrain ?? ask 
             builder.Property(Rep => Rep.ProductMaxquantity).IsRequired();

            //Relationship One to one {product , Replenishment}
            builder.HasOne(R => R.Product)
                .WithOne(P => P.Replenishment)
                .HasForeignKey<Replenishment>(R => R.ProductId);

        }
    }
}
