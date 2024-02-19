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
    internal class MovesHistoryConfigurations : IEntityTypeConfiguration<MovesHistory>
    {
        public void Configure(EntityTypeBuilder<MovesHistory> builder)
        {
            builder.Property(e => e.Reference)
                .IsRequired(true)
                .HasMaxLength(255);

            builder.Property(e => e.Quantity)
                .IsRequired();

            builder.Property(e => e.Date)
                .IsRequired();

            //Relationship one to Many with product
            builder.HasOne(MH => MH.Product)
                .WithMany(P => P.MovesHistories)
                .HasForeignKey(MH => MH.ProductId);

            //Relationship one to Many with Employee
            builder.HasOne(MH => MH.Employee)
                .WithMany(E => E.MovesHistorris)
                .HasForeignKey(MH => MH.DoneBy)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
