using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ecommerce.Data;

namespace ECommerce.Models
{
    public class AddProductModelView
    {
        public IEnumerable<Category> Categories { get; set; } 
    }
}