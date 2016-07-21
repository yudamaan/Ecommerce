using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ecommerce.Data;


namespace ECommerce
{
    public class CartCountAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var repo = new CartRepository(Properties.Settings.Default.ConStr);
            if(filterContext.HttpContext.Session["cart"] != null)
            {
                var cartId = (int)filterContext.HttpContext.Session["cart"];
                filterContext.Controller.ViewBag.CartCount = repo.GetCartCount(cartId);
                filterContext.Controller.ViewBag.CartId = cartId;
            }
            
            base.OnActionExecuting(filterContext);
        }
    }
}