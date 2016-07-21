using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ecommerce.Data;

namespace ECommerce.Models
{
    public class IndexPageViewModel
    {
        public IEnumerable<Category> Catergories { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public int PageNumber { get; set; }
        public int CurrentCategory { get; set; }
        public bool ShowNextPage { get; private set; }

        public void SetShowNextPage(int total)
        {
            ShowNextPage = PageNumber * 3 < total;
        }
    }
}