using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NguyenHuyHung_241230739.Models;

public partial class TaiSan
{
    public int MaTs { get; set; }

    [Display(Name = "Loại tài sản")]
    public int MaLoai { get; set; }

    [Display(Name = "Tên tài sản")]
    [Required(ErrorMessage = "Tên tài sản không được để trống")]
    [StringLength(100, ErrorMessage = "Tên tài sản tối đa 100 ký tự")]
    public string TenTs { get; set; } = null!;

    [Display(Name = "Nguyên giá (triệu đồng)")]
    [Required(ErrorMessage = "Nguyên giá không được để trống")]
    [Range(1, 2000, ErrorMessage = "Nguyên giá phải là số và nằm trong giới hạn từ 1 đến 2000")]
    public decimal? NguyenGia { get; set; }

    [Display(Name = "Ảnh minh họa")]
    [Required(ErrorMessage = "Tên file ảnh không được để trống")]
    [RegularExpression(@"^.*\.(jpg|jpeg|png|webp)$", ErrorMessage = "Tên file ảnh phải có đuôi: .jpg | .jpeg | .png | .webp")]
    public string? Anh { get; set; }

    public virtual LoaiTaiSan? MaLoaiNavigation { get; set; }
}
