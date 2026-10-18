using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tuan9.Models;

[Table("Blogs")]
public class Blog
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Tiêu đề blog không được để trống")]
    [StringLength(100, ErrorMessage = "Tiêu đề blog tối đa 100 ký tự")]
    [Display(Name = "Tiêu đề")]
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
}
