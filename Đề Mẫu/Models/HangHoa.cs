using System.ComponentModel.DataAnnotations;

namespace NguyenHuyHung_241230739.Models;

public partial class HangHoa
{
    public int MaHang { get; set; }

    [Display(Name = "Loại hàng")]
    public int MaLoai { get; set; }

    [Display(Name = "Tên hàng")]
    [Required(ErrorMessage = "Tên hàng không được để trống")]
    [StringLength(50, ErrorMessage = "Tên hàng tối đa 50 ký tự")]
    public string TenHang { get; set; } = null!;

    [Display(Name = "Giá")]
    [Required(ErrorMessage = "Giá không được để trống")]
    [Range(100, 5000, ErrorMessage = "Giá phải là số và nằm trong giới hạn 100 đến 5000")]
    public decimal? Gia { get; set; }

    [Display(Name = "Ảnh đại diện")]
    [Required(ErrorMessage = "Tên file ảnh không được để trống")]
    [RegularExpression(@"^.*\.(jpg|png|gif|tiff)$", ErrorMessage = "Tên file ảnh phải có đuôi: .jpg | .png | .gif | .tiff")]
    public string? Anh { get; set; }

    public virtual LoaiHang? MaLoaiNavigation { get; set; }
}