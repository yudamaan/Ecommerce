using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Data
{
    public class CategoryRepository
    {
        private string _conStr;
        public CategoryRepository(string conStr)
        {
            _conStr = conStr;
        }
        public void AddCategory(Category category)
        {
            using(var context = new ECommerceDbDataContext(_conStr))
            {
                context.Categories.InsertOnSubmit(category);
                context.SubmitChanges();
            }
        }
        public IEnumerable<Category> GetCategories()
        {
            using (var context = new ECommerceDbDataContext(_conStr))
            {
                return context.Categories.ToList();
            }
        }
    }
}
