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
    internal class TransferConfigurations : IEntityTypeConfiguration<Transfer>
    {
        public void Configure(EntityTypeBuilder<Transfer> builder)
        {
            builder.Property(t => t.Reference)
                .IsRequired(true)
                .HasMaxLength(255);

            builder.Property(t => t.Status)
                .IsRequired();

            builder.Property(t => t.Date)
                .IsRequired();

            //Relationship one to Many with Employee
            builder.HasOne(MH => MH.Employee)
                .WithMany(E => E.Transfers)
                .HasForeignKey(MH => MH.DoneBy);
        }
    }
}
