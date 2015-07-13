
using LPShops.DataAccess;
using System;
using System.ComponentModel.DataAnnotations;

namespace LPShops.Models
{
    public class Product
    {       
        public int ProductId { get; set; }
        [Required(ErrorMessage = "The field Name is required")]
        public string Name { get; set; }        
        public string Description { get; set; }        
        public int ShopId { get; set; }        
        public virtual Shop Shop { get; set; }
    }
}