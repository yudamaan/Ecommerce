using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ecommerce.Data;
using ECommerce.Models;

namespace ECommerce.Models
{
    public class CartPageViewModel
    {
        public IEnumerable<CartPagePropertis> ItemsInCart { get; set; }
        public double CartTotal { get; set; }
    }
}