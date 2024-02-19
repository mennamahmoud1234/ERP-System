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
    public class ScmOrderProductConfigurations : IEntityTypeConfiguration<ScmOrderProduct>
    {
        public void Configure(EntityTypeBuilder<ScmOrderProduct> builder)
        {
            builder.HasKey(IOP => new { IOP.ProductId, IOP.ScmId });
            //FK [one to many] => Products
            builder.HasOne(SOP => SOP.Product)
                   .WithMany(P => P.ScmProductOrders)
                    .HasForeignKey(SOP => SOP.ProductId);


            //FK [one to many] => ScmOrder
            builder.HasOne(SOP => SOP.ScmOrder)
                   .WithMany(SP => SP.ScmOrderProducts)
                    .HasForeignKey(SOP => SOP.ScmId);
        
        }
    }
}
