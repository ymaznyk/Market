using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using eMarket.Models;
using System.IO;

namespace eMarket.Controllers
{    
    /// <summary>
    /// Odpowiada za zarządzania produktamy
    /// </summary>
    [Authorize(Roles = "admin")]
    public class StoreManagerController : Controller
    {

        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(Market.DbContext.Categories, "CategoryId", "Name");
           
            return View();
        }
        
        /// <summary>
        /// Odpowiada za tworzenia prodktów
        /// </summary>
        /// <param name="product"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(Product product, HttpPostedFileBase file)
        {
            
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string path = Path.Combine(Server.MapPath("~/Content/Images/"), file.FileName);
                    file.SaveAs(path);
                    product.Img = file.FileName; 
                }
                Market.DbContext.Products.Add(product);
                try
                {
                    Market.DbContext.SaveChanges();
                }
                catch
                {

                    return View("Error");
                }
                return RedirectToAction("Browse", "Home", 
                    new { Category = Market.DbContext.Categories.Find(product.CategoryId).Name,
                        page = GetPageNumber(product), pageSize = TempData["PageSize"].ToString() });
            }
            ViewBag.CategoryId = new SelectList(Market.DbContext.Categories, "CategoryId", "Name", product.CategoryId);
            return View(product);
        }
        
        public ActionResult Edit(int id)
        {
            Product product = Market.DbContext.Products.Find(id);
            ViewBag.CategoryId = new SelectList(Market.DbContext.Categories, "CategoryId", "Name", product.CategoryId);
            return View(product);
        }
        
        /// <summary>
        /// Odpowiada za edetówanie produktu
        /// </summary>
        /// <param name="product"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(Product product, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string path = Path.Combine(Server.MapPath("~/Content/Images/"), file.FileName);
                    file.SaveAs(path);
                    product.Img = file.FileName; 
                }
                Market.DbContext.Entry(product).State = EntityState.Modified;
                try
                {
                    Market.DbContext.SaveChanges();
                }
                catch
                {

                    return View("Error");
                }
                return RedirectToAction("Browse", "Home", 
                    new { Category = Market.DbContext.Categories.Find(product.CategoryId).Name,
                        page = GetPageNumber(product), pageSize = TempData["PageSize"] });
            }
            ViewBag.CategoryId = new SelectList(Market.DbContext.Categories, "CategoryId", "Name", product.CategoryId);
            return View(product);
        }
        

        public ActionResult Delete(int id)
        {
            return View(Market.DbContext.Products.Find(id));
        }
        
        /// <summary>
        /// Odpowiada za usunięcia produktu z bazy
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        
        public ActionResult DeleteConfirmed(int id)
        {
            string categoryName = Market.DbContext.Products.Find(id).Category.Name;
            Market.DbContext.Products.Remove(Market.DbContext.Products.Find(id));
            try
            {
                Market.DbContext.SaveChanges();
            }
            catch
            {

                return View("Error");
            }
            return RedirectToAction("Browse", "Home", new { Category = categoryName ,
                page = GetPageNumber(categoryName), pageSize = TempData["PageSize"].ToString() });
        }
        #region Helpers
            public decimal GetPageNumber(Product product)
            {
                string categoryName = Market.DbContext.Categories.Find(product.CategoryId).Name;
                List<Product> products = Market.DbContext.Products.Where(c => c.Category.Name == categoryName).ToList();
                decimal indexProduct = products.IndexOf(product) + 1;
                decimal pageNumber = Math.Ceiling(indexProduct / int.Parse(TempData["PageSize"].ToString()));
                return pageNumber;
            }
            public decimal GetPageNumber(string categoryName)
            {
                decimal pageNumber = 1;
                decimal productsCount = Market.DbContext.Products.Where(c => c.Category.Name == categoryName).Count();
                if(productsCount != 0)
                {
                    decimal lastPage = Math.Ceiling(productsCount / int.Parse(TempData["PageSize"].ToString()));
                    int currentPage = int.Parse(TempData["PageNumber"].ToString());
                    if (currentPage <= lastPage)
                    {
                        pageNumber = currentPage;
                    }
                    else
                    {
                        pageNumber = lastPage;
                    }
                }
                return pageNumber;
            }
        #endregion
    }
}
