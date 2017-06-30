using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using eMarket.Models;
using System.IO;

namespace eMarket.Controllers

{
    /// <summary>
    /// Kontroler sluże do zarządzania kategorijamy
    /// </summary>
    public class CategoriesManagerController : Controller
    {
        public ActionResult Details(int id = 0)
        {
            Category category = Market.DbContext.Categories.Find(id);
            if (category == null)
                return HttpNotFound();
            return View(category);
        }   
        public ActionResult Create()
        {
            return View();
        }
        /// <summary>
        /// Sluże do tworzenia kategorii
        /// </summary>
        /// <param name="category"></param>
        /// <param name="file">Zdięcia kategoriji</param>
        /// <returns></returns>
        /// 
        [HttpPost]
        public ActionResult Create(Category category, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string path = Path.Combine(Server.MapPath("~/Content/Images/"), file.FileName);
                    file.SaveAs(path);
                    category.Image = file.FileName;
                }

                Market.DbContext.Categories.Add(category);

                try
                {
                    Market.DbContext.SaveChanges();
                }
                catch
                {
                    return View("Error");
                }

                return RedirectToAction("Index", "Home");
            }
            return View(category);
        }

        
        public ActionResult Edit(int id = 0)
        {
            Category category = Market.DbContext.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }
        /// <summary>
        /// Edetuwania ketegorii
        /// </summary>
        /// <param name="category"></param>
        /// <param name="file">Zdięcia kategorii</param>
        /// <returns></returns>
        
        [HttpPost]
        public ActionResult Edit(Category category, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string path = Path.Combine(Server.MapPath("~/Content/Images/"), file.FileName);
                    file.SaveAs(path);
                    category.Image = file.FileName;
                }
                Market.DbContext.Entry(category).State = EntityState.Modified;
                try
                {
                    Market.DbContext.SaveChanges();
                }
                catch
                {

                    return View("Error");
                }
                return RedirectToAction("Index", "Home");
            }
            return View(category);
        }
        /// <summary>
        /// Usunięcia kategoriji
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id = 0)
        {
            Category category = Market.DbContext.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }
        
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Market.DbContext.Categories.Remove(Market.DbContext.Categories.Find(id));
            try
            {
                Market.DbContext.SaveChanges();
            }
            catch
            {

                return View("Error");
            }
            return RedirectToAction("Index", "Home");
        }
    }
}