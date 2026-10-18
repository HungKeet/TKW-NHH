using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tuan9.Models;

[Table("Products")]
public class Product
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
    [StringLength(100, ErrorMessage = "Tên sản phẩm tối đa 100 ký tự")]
    [Display(Name = "Tên sản phẩm")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Giá không được để trống")]
    [Range(0, double.MaxValue, ErrorMessage = "Giá phải lớn hơn hoặc bằng 0")]
    [Display(Name = "Giá niêm yết")]
    public double Price { get; set; }

    [Display(Name = "Giá khuyến mãi")]
    public double SalePrice { get; set; } = 0;

    [Display(Name = "Trạng thái")]
    public byte Status { get; set; } = 1;

    [Required(ErrorMessage = "Vui lòng chọn danh mục")]
    [Display(Name = "Danh mục")]
    public int CategoryId { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Ngày tạo")]
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    [StringLength(100)]
    [Display(Name = "Hình ảnh")]
    public string Image { get; set; }

    [StringLength(350)]
    [Display(Name = "Mô tả")]
    public string Description { get; set; }

    [ForeignKey("CategoryId")]
    public virtual Category Category { get; set; }
}
