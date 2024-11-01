using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerceWebApi.Domain.Entities;

namespace E_commerceWebApi.Infustracture.Configuration
{
    public class ProductConfiquration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(c => c.Brand)
               .WithMany()
               .HasForeignKey(c => c.BrandId);
}
    }
}
