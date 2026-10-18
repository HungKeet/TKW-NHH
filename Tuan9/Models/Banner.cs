using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tuan9.Models;

[Table("Banners")]
public class Banner
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Tên banner không được để trống")]
    [StringLength(100, ErrorMessage = "Tên banner tối đa 100 ký tự")]
    [Display(Name = "Tên banner")]
    public string Name { get; set; }

    [Display(Name = "Trạng thái")]
    public byte Status { get; set; } = 1;

    [Display(Name = "Thứ tự ưu tiên")]
    public int Prioty { get; set; } = 0;

    [StringLength(100)]
    [Display(Name = "Hình ảnh")]
    public string Image { get; set; }

    [StringLength(350)]
    [Display(Name = "Mô tả")]
    public string Description { get; set; }
}
