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
    public class EmployeePrivateInformationConfigurations : IEntityTypeConfiguration<EmployeePrivateInformation>
    {
        public void Configure(EntityTypeBuilder<EmployeePrivateInformation> builder)
        {
            builder.Property(EP => EP.Address)
                .IsRequired()
                .HasMaxLength(255);


            builder.Property(EP => EP.Phone)
                .IsRequired()
                .HasMaxLength(255);


            builder.Property(EP => EP.EmergencyName)
                .IsRequired()
                .HasMaxLength(255);


            builder.Property(EP => EP.EmergencyPhone)
                .IsRequired()
                .HasMaxLength(255);


            builder.Property(EP => EP.Nationality)
                .IsRequired()
                .HasMaxLength(255);


            builder.Property(EP => EP.IdentificationNo)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(EP => EP.PassportNo)
                .IsRequired()
                .HasMaxLength(255);


            builder.Property(EP => EP.Gender)
                .IsRequired();

            // FK [one to one] =>Employee
            builder.HasOne(EPI => EPI.Employee)
                   .WithOne(E => E.EmployeePrivateInformations)
                   .HasForeignKey<EmployeePrivateInformation>(EPI => EPI.EmployeeId);
        }
    }
}
