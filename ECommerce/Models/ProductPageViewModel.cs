using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ecommerce.Data;

namespace ECommerce.Models
{
    public class ProductPageViewModel
    {
        public Product Product { get; set; }
        public IEnumerable<Category> Catergories { get; set; }

    }
}