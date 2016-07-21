using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerce.Models;
using Ecommerce.Data;
using AttributeRouting.Web.Mvc;

namespace ECommerce.Controllers
{
    public class HomeController : Controller
    {
        [Route("Home/Category/{Category}")]
        public ActionResult Index(int? categoryId, int? page)
        {
            IndexPageViewModel model = new IndexPageViewModel();
            if (categoryId == null)
            {
                categoryId = 1;
            }
            if (page == null)
            {
                page = 1;
            }

            var productRepo = new ProductRepository(Properties.Settings.Default.ConStr);
            var categoryRepo = new CategoryRepository(Properties.Settings.Default.ConStr);
            model.PageNumber = page.Value;
            model.CurrentCategory = categoryId.Value;
            model.Catergories = categoryRepo.GetCategories();
            model.Products = productRepo.GetProductsWithImage(categoryId.Value, page.Value);
            model.SetShowNextPage(productRepo.GetProductCountPerCategory(model.CurrentCategory));
            return View(model);
        }
        [Route("Home/Product/{product}")]
        public ActionResult Product(int productId)
        {
            var productRepo = new ProductRepository(Properties.Settings.Default.ConStr);
            ProductPageViewModel model = new ProductPageViewModel();
            var categoryRepo = new CategoryRepository(Properties.Settings.Default.ConStr);
            model.Catergories = categoryRepo.GetCategories();
            model.Product = productRepo.GetProductWithImages(productId);
            return View(model);
        }
        
        [HttpPost]
        public ActionResult AddToCart(CartItem items, int quantity)
        {
            var cartRepo = new CartRepository(Properties.Settings.Default.ConStr);
            if (Session["cart"] == null)
            {
                Cart cart = cartRepo.CreateCart();
                Session["cart"] = cart.CartId;
            }
            items.CartId = (int)Session["cart"];
            items.Quantity = quantity;
            cartRepo.AddToCart(items);
            return Json(new { CartCount = cartRepo.GetCartCount((int)Session["cart"]), CartId = (int)Session["cart"] }, JsonRequestBehavior.AllowGet);
        }
        [Route("/Home/Cart/{cartId}")]
        public ActionResult Cart(int? cartId)
        {
            var cartRepo = new CartRepository(Properties.Settings.Default.ConStr);
            CartPageViewModel model = new CartPageViewModel();
            if (cartId != null)
            {
                model.ItemsInCart = cartRepo.GetItemsInCart(cartId).Select(i => new CartPagePropertis
                {
                    Item = i,
                    TotalPerItem = (decimal)i.Quantity * i.Product.Price,
                });
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateQuantity(int quantity, int itemId)
        {
            var cartRepo = new CartRepository(Properties.Settings.Default.ConStr);
            cartRepo.UpdateQuantity(quantity, itemId);
            return Redirect("cart?cartid=" + Session["cart"]);
        }

        [HttpPost]
        public void DeleteItem(int itemId)
        {
            var cartRepo = new CartRepository(Properties.Settings.Default.ConStr);
            cartRepo.DeleteItem(itemId);
        }
    }
}
