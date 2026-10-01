using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tuan7.Models
{
    public class Product : IValidatableObject
    {
        [Key]
        [Display(Name = "Mã sản phẩm")]
        public int Id { get; set; }

        [Display(Name = "Tên sản phẩm")]
        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        [StringLength(150, MinimumLength = 6, ErrorMessage = "Tên sản phẩm phải từ 6 đến 150 ký tự")]
        public string Name { get; set; }

        [Display(Name = "Hình ảnh")]
        public string Image { get; set; }

        [Display(Name = "Giá chuẩn")]
        [Required(ErrorMessage = "Giá sản phẩm không được để trống")]
        [Range(100000, float.MaxValue, ErrorMessage = "Giá sản phẩm phải nhỏ nhất là 100.000")]
        public float Price { get; set; }

        [Display(Name = "Giá khuyến mãi")]
        [Required(ErrorMessage = "Giá khuyến mãi không được để trống")]
        public float SalePrice { get; set; }

        [Display(Name = "Danh mục")]
        [Required(ErrorMessage = "Vui lòng chọn danh mục")]
        public int CategoryId { get; set; }

        [Display(Name = "Mô tả sản phẩm")]
        [Required(ErrorMessage = "Mô tả không được để trống")]
        [MaxLength(1500, ErrorMessage = "Mô tả không được vượt quá 1500 ký tự")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (SalePrice < 0)
            {
                yield return new ValidationResult("Giá khuyến mãi không được âm", new[] { nameof(SalePrice) });
            }

            if (SalePrice > Price * 0.9f)
            {
                yield return new ValidationResult($"Giá khuyến mãi phải nhỏ hơn giá chuẩn ít nhất 10% (tối đa {(Price * 0.9f):N0})", new[] { nameof(SalePrice) });
            }

            if (!string.IsNullOrEmpty(Description))
            {
                string[] sensitiveWords = new[] { "die", "admin", "fack", "fuck" };
                foreach (var word in sensitiveWords)
                {
                    if (Description.ToLower().Contains(word.ToLower()))
                    {
                        yield return new ValidationResult($"Mô tả không được chứa từ nhạy cảm '{word}'", new[] { nameof(Description) });
                        break;
                    }
                }
            }
        }
    }
}
