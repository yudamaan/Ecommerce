using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Data
{
    public class CartRepository
    {
        private string _conStr;
        public CartRepository(string conStr)
        {
            _conStr = conStr;
        }
        public Cart CreateCart()
        {
            using(var context = new ECommerceDbDataContext(_conStr))
            {
                Cart cart = new Cart();
                cart.DateCreated = DateTime.Now;
                context.Carts.InsertOnSubmit(cart);
                context.SubmitChanges();
                return cart;
            }
        }
        public void AddToCart(CartItem items)
        {
            using (var context = new ECommerceDbDataContext(_conStr))
            {
                if(CheckIfItemIsAlreadyInCart(items))
                {
                    var updateItem = context.CartItems.Where(i => i.CartId == items.CartId && i.ProductId == items.ProductId).FirstOrDefault();
                    updateItem.Quantity += items.Quantity;
                }
                else
                {
                    context.CartItems.InsertOnSubmit(items);
                }
                
                context.SubmitChanges();
            }
        }
        private bool CheckIfItemIsAlreadyInCart(CartItem items)
        {
            using (var context = new ECommerceDbDataContext(_conStr))
            {
                return context.CartItems.Any(c => c.CartId == items.CartId && c.ProductId == items.ProductId);
            }
        }
        public int GetCartCount(int cartId)
        {
            using (var context = new ECommerceDbDataContext(_conStr))
            {
                context.Log = new DebugTextWriter();                
                return context.CartItems.Where(c => c.CartId == cartId).Sum(q => q.Quantity);               
            }

        }
        public IEnumerable<CartItem> GetItemsInCart(int? cartId)
        {
            if (cartId == null)
            {
                return null;
            }
            using (var context = new ECommerceDbDataContext(_conStr))
            {
                var loadOptions = new DataLoadOptions();
                loadOptions.LoadWith<CartItem>(c => c.Product);
                context.LoadOptions = loadOptions;
                return context.CartItems.Where(c => c.CartId == cartId).ToList();
            }
        }
        public void DeleteItem(int itemId)
        {
            using (var context = new ECommerceDbDataContext(_conStr))
            {
                context.ExecuteCommand("DELETE FROM CartItems WHERE CartItemsId = {0}", itemId);
                
            }
        }
        public void UpdateQuantity(int quantity, int itemId)
        {
            using (var context = new ECommerceDbDataContext(_conStr))
            {
                var updateItem = context.CartItems.Where(i => i.CartItemsId == itemId).FirstOrDefault();
                updateItem.Quantity = quantity;
                context.SubmitChanges();
            }
        }
    }
}
