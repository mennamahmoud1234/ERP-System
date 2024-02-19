using ERP.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Core.Data.Config
{
    internal class SubCategoryConfigurations : IEntityTypeConfiguration<SubCategory>
    {
        public void Configure(EntityTypeBuilder<SubCategory> builder)
        {
            builder.Property(c => c.SubCategoryName)
                .IsRequired()
                .HasMaxLength(50);


            //foreign key one to many relationship
            builder.HasOne(s => s.ParentCategory)
            .WithMany(p => p.SubCategories)
            .HasForeignKey(s => s.ParentCategoryId)
            .OnDelete(DeleteBehavior.Cascade);

            // foreign key product relation [has]
            builder.HasMany(c => c.Products)
                .WithOne(p => p.SubCategory)
                .HasForeignKey(c => c.SubCategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
