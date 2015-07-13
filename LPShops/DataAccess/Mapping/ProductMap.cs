using LPShops.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;


namespace LPShops.DataAccess.Mapping
{
    public class ProductMap : EntityTypeConfiguration<Product>
    {        
        public ProductMap()
        {
            HasKey(t => t.ProductId);

            Property(t => t.ProductId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Name).IsRequired().HasMaxLength(50);
            Property(t => t.Description).HasMaxLength(100);

            ToTable("Products");
        }  
    }
}