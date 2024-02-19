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
    public class EmployeeWorkInformationConfigurations : IEntityTypeConfiguration<EmployeeWorkInformation>
    {
        public void Configure(EntityTypeBuilder<EmployeeWorkInformation> builder)
        {
            builder.Property(EW => EW.WorkMobile)
                .IsRequired()
                .HasMaxLength(255);


            builder.Property(EW => EW.WorkPhone)
                .IsRequired()
                .HasMaxLength(255);


            builder.Property(EW => EW.WorkEmail)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(EW => EW.BankAccount)
                .IsRequired()
                .HasMaxLength(255);


            builder.Property(EW => EW.WorkPermitNo)
                .IsRequired()
                .HasMaxLength(255);

            // FK [one to one] =>Employee
            builder.HasOne(EWI => EWI.Employee)
                   .WithOne(E => E.EmployeeWorkInformations)
                   .HasForeignKey<EmployeeWorkInformation>(EWI => EWI.EmployeeId);
        }
    }
}
