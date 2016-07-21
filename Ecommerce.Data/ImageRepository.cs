using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Ecommerce.Data
{
    public class ImageRepository
    {
        private string _conStr;
        public ImageRepository(string conStr)
        {
            _conStr = conStr;
        }
        public void AddImage(List<Image> images)
        {
            using(var context = new ECommerceDbDataContext(_conStr))
            {
                context.Images.InsertAllOnSubmit(images);
                context.SubmitChanges();
            }
        }
    }
}
