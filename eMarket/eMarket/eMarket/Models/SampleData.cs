using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace eMarket.Models
{
    public class SampleData : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context) 
        {
            var categories = new List<Category>
            {
                new Category {Name = "Category1"},
            };
            new List<Product>
            {
                new Product {Title = "Apple iPhone 5", Category = categories.Single(c => c.Name == "Category1"),  Price = 595.90M , Company = "Apple", Img = ResMan.PathImg + "Product.png"},
            }.ForEach(p=>context.Products.Add(p));
        }
    }
}
