using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OzelDers.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzelDers.Data.Config
{
    public class BranchConfig : IEntityTypeConfiguration<Branch>
    {
        public void Configure(EntityTypeBuilder<Branch> builder)
        {
            builder.HasKey(b => b.Id);//primary key
            builder.Property(b => b.Id).ValueGeneratedOnAdd();//1 er 1er artması

            builder.Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(b => b.Url)
             .IsRequired()
             .HasMaxLength(250);

            builder.HasData(
              
               new Branch
               {
                   Id = 2,
                   Name = "Matematik",
                   Url = "matematik"
               },
               new Branch
               {
                   Id = 3,
                   Name = "Fizik",
                   Url = "fizik"
               }
               );
        }
    }
}
