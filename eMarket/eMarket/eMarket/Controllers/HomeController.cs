using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using eMarket.Models;
using PagedList;

namespace eMarket.Controllers
{
    /// <summary>
    /// Glowny konroler, sluże do wyświetlenie kategorij
    /// </summary>
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(Market.DbContext.Categories.ToList());
        }
        public ActionResult Browse(string category, int? page, string sortOrder, string pageSize)
        {
            List<Product> products = Market.DbContext.Categories
                .Include("Products").Single(c => c.Name == category)
                .Products;

            int pageNumber = page ?? 1;
            string sorting = string.IsNullOrEmpty(sortOrder) ? ViewBag.Sort ?? "" : sortOrder;
            string pageSizeSet = string.IsNullOrEmpty(pageSize) ? ViewBag.PageSize ?? "5" : pageSize;

            ViewBag.PageSize = pageSizeSet;
            ViewBag.Sort = sorting;
            ViewBag.Category = category;

            TempData["PageSize"] = pageSizeSet;
            TempData["PageNumber"] = pageNumber;
            

            switch (sorting)
            {
                case "What's new":
                    products = products.OrderByDescending(p => p.ProductId).ToList();
                    break;
                case "Price: High to Low":
                    products = products.OrderByDescending(p => p.Price).ToList();
                    break;
                case "Price: Low to High":
                    products = products.OrderBy(p => p.Price).ToList();
                    break;
            }
            return View(products.ToPagedList(pageNumber, int.Parse(pageSizeSet)));
        }

        public ActionResult Details(int id)
        {
            return View(Market.DbContext.Products.Find(id));
        }

        [ChildActionOnly]
        public ActionResult GenreMenu()
        {
            return PartialView(Market.DbContext.Categories.ToList());
        }
    }
}