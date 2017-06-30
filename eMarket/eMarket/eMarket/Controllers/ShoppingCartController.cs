using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using eMarket.Models;

namespace eMarket.Controllers
{
    /// <summary>
    /// Odpowiada za kosz
    /// </summary>
    [Authorize]
    public class ShoppingCartController : Controller
    {
        /// <summary>
        /// Wyświetlenie produktów z kosza
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            List<Cart> carts = Market.DbContext.Carts.Where(c => c.CartId == 
                                                this.HttpContext.User.Identity.Name).ToList();
            foreach (Cart cartItem in carts)
            {
                cartItem.Product = Market.DbContext.Products.Find(cartItem.ProductId);
            }
            decimal? total = (from cartItems in Market.DbContext.Carts
                              where cartItems.CartId == this.HttpContext.User.Identity.Name
                              select (int?)cartItems.Quantity *
                              cartItems.Product.Price).Sum();
            ViewBag.Total = total ?? Decimal.Zero;

            return View(carts);
        }
        /// <summary>
        /// Dodawanie produktów do kosza
        /// </summary>
        /// <param name="id">Numer produkta</param>
        /// <returns></returns>
        public ActionResult AddToCart(int id)
        {
            var addedProduct = Market.DbContext.Products.Single(product => product.ProductId == id);
            var cartItem = Market.DbContext.Carts.SingleOrDefault(c => c.ProductId == addedProduct.ProductId 
                                                           && c.CartId == this.HttpContext.User.Identity.Name);
            if (cartItem == null)
            {
                cartItem = new Cart
                {
                    ProductId = addedProduct.ProductId,
                    CartId = this.HttpContext.User.Identity.Name,
                    Quantity = 1,
                    DateCreated = DateTime.Now
                };
                Market.DbContext.Carts.Add(cartItem);
            }
            else cartItem.Quantity++;

            try
            {
                Market.DbContext.SaveChanges();
            }
            catch
            {

                return View("Error");
            }
            return RedirectToAction("Index");
        }
       /// <summary>
       /// Usunięcia produktu z kosza
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
        public ActionResult Delete(int id)
        {
            Market.DbContext.Carts.Remove(Market.DbContext.Carts.Find(id));
            try
            {
                Market.DbContext.SaveChanges();
            }
            catch
            {

                return View("Error");
            }
            return RedirectToAction("Index");
        }
    }
}
