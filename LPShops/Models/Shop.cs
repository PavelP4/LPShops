
using LPShops.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LPShops.Models
{    
    public class Shop
    {
        public Shop() 
        {
            Products = new HashSet<Product>();
        }
        
        public int ShopId { get; set; }
        [Required(ErrorMessage = "The field Name is required")]
        public string Name { get; set; }        
        public string Address { get; set; }        
        public string Mode { get; set; }        
        public virtual ICollection<Product> Products { get; set; }
    }
}