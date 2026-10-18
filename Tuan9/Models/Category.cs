using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tuan9.Models;

[Table("Categories")]
public class Category
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Tên danh mục không được để trống")]
    [StringLength(100, ErrorMessage = "Tên danh mục tối đa 100 ký tự")]
    [Display(Name = "Tên danh mục")]
    public string Name { get; set; }

    [Display(Name = "Trạng thái")]
    public byte Status { get; set; } = 1;

    [DataType(DataType.Date)]
    [Display(Name = "Ngày tạo")]
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    [StringLength(100)]
    [Display(Name = "Hình ảnh")]
    public string Image { get; set; }

    [StringLength(350)]
    [Display(Name = "Mô tả")]
    public string Description { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
