using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace eMarket.Models
{
    public class Product
    {
        [ScaffoldColumn(false)]
        public int ProductId { get; set; }

        [DisplayName("Category")]
        [Required(ErrorMessage = "ErrorMessage.RequiredCategoryId")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "ErrorMessage.RequiredTitle")]
        [StringLength(200)]
        public string Title { get; set; }

        [Required(ErrorMessage = "ErrorMessage.RequiredPrice")]
        [Range(0.01, Double.PositiveInfinity, ErrorMessage = "ErrorMessage.RangePrice")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "ErrorMessage.RequiredItem")]
        public uint Item { get; set; }

        [Required(ErrorMessage = "ErrorMessage.RequiredCompany")]
        [StringLength(200)]
        public string Company { get; set; }

        [DisplayName("Image")]
        [StringLength(1000)]
        public string Img { get; set; }

        public string Availability { get; set; }
        public virtual Category Category { get; set; }
    }
}
