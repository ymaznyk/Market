using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace eMarket.Models
{
    public partial class Category
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "ErrorMessage.RequiredCategoryName")]
        [StringLength(200)]
        public string Name { get; set; }

        [DisplayName("Image")]
        public string Image { get; set; }

        public List<Product> Products { get; set; }
    }
}
