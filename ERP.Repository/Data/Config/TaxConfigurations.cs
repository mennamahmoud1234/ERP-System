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
    internal class TaxConfigurations : IEntityTypeConfiguration<Tax>
    {
        public void Configure(EntityTypeBuilder<Tax> builder)
        {
            builder.Property(P => P.TaxValue)
                   .IsRequired();
            builder.Property(P => P.TaxType)
                  .IsRequired();
        }
    }
}
