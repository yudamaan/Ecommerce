using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ecommerce.Data;

namespace ECommerce.Models
{
    public class CartPagePropertis
    {
        public CartItem Item { get; set; }
        public decimal TotalPerItem { get; set; }
    }
}