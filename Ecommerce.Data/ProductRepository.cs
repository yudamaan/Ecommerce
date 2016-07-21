using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Data
{
    public class ProductRepository
    {
        private string _conStr;
        public ProductRepository(string conStr)
        {
            _conStr = conStr;
        }
        public IEnumerable<Product> GetProductsWithImage(int categoryId, int page)
        {
            
            int skip = (page - 1) * 3;
            using(var context = new ECommerceDbDataContext(_conStr))
            {
                var loadOptions = new DataLoadOptions();
                loadOptions.LoadWith<Product>(p => p.Images);
                context.LoadOptions = loadOptions;
                return context.Products.Skip(skip).Take(3).Where(p => p.CategoryId == categoryId).ToList();
            }
        }
        public Product GetProductWithImages(int productId)
        {
            using(var context = new ECommerceDbDataContext(_conStr))
            {
                var loadOptions = new DataLoadOptions();
                loadOptions.LoadWith<Product>(p => p.Images);
                context.LoadOptions = loadOptions;
                return context.Products.Where(p => p.ProductId == productId).FirstOrDefault();
            }
        }
        public void AddProduct(Product product)
        {
            using(var context = new ECommerceDbDataContext(_conStr))
            {
                context.Products.InsertOnSubmit(product);
                context.SubmitChanges();
            }
        }
        public int GetProductCountPerCategory(int categoryId)
        {
            using (var context = new ECommerceDbDataContext(_conStr))
            {
                return context.Products.Where(p => p.CategoryId == categoryId).Count();
            }
        }
        
    }
}
