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
    public class JobPositionConfigurations : IEntityTypeConfiguration<JobPosition>
    {
        public void Configure(EntityTypeBuilder<JobPosition> builder)
        {
            
            builder.Property(J => J.JobName)
                  .HasMaxLength(255)
                  .IsRequired();
            builder.HasOne(j => j.Department)
                   .WithMany(d => d.JobPositions)
                   .HasForeignKey(j => j.DepartmentId)
                   .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
