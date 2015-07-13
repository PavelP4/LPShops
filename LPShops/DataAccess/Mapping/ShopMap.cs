using LPShops.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;


namespace LPShops.DataAccess.Mapping
{
    public class ShopMap: EntityTypeConfiguration<Shop>
    {
        public ShopMap()
        {
            HasKey(t => t.ShopId);
            
            Property(t => t.ShopId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Name).IsRequired().HasMaxLength(50);
            Property(t => t.Address).HasMaxLength(100);
            Property(t => t.Mode).HasMaxLength(50);

            HasMany(t => t.Products).WithRequired(t => t.Shop).WillCascadeOnDelete(true);

            ToTable("Shops");
        }        
    }
}