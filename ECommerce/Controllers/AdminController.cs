using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Ecommerce.Data;
using ECommerce.Models;
using System.IO;

namespace ECommerce.Controllers
{
    
    public class AdminController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
              
        [Authorize]
        public ActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCategory(Category category)
        {
            var repo = new CategoryRepository(Properties.Settings.Default.ConStr);
            repo.AddCategory(category);
            return Redirect("index");
        }
         
        [Authorize]
        public ActionResult AddProduct()
        {
            var categoryRepo = new CategoryRepository(Properties.Settings.Default.ConStr);
            AddProductModelView model = new AddProductModelView();
            model.Categories = categoryRepo.GetCategories();
            return View(model);
        }
        [HttpPost]
        public ActionResult AddProduct(Product product, List<ImageUpload> image)
        {
            var productRepo = new ProductRepository(Properties.Settings.Default.ConStr);
            var imageRepo = new ImageRepository(Properties.Settings.Default.ConStr);
            
            productRepo.AddProduct(product);
            List<Image> images = new List<Image>();
            foreach (ImageUpload i in image)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(i.Image.FileName);
                i.Image.SaveAs(Server.MapPath("~/Images/") + fileName);
                images.Add(new Image
                {
                    ProductId = product.ProductId,
                    FileName = fileName,
                });
            }
            imageRepo.AddImage(images);
            return Redirect("index");
        }
        
        public ActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LogIn(string userName, string password)
        {
            var repo = new UserRepository(Properties.Settings.Default.ConStr);
            var user = repo.Login(userName, password);
            if(user == null)
            {
                return Redirect("login");
            }
            FormsAuthentication.SetAuthCookie(userName, true);
            return Redirect("index");
        }

    }
}
