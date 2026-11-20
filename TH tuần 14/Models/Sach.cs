using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace NguyenHuyHung_241230739.Models;

public partial class Sach
{
    public int MaSach { get; set; }

    [Display(Name = "MaTL")]
    public int MaTl { get; set; }

    [Required(ErrorMessage = "TenSach không được để trống")]
    [Display(Name = "TenSach")]
    public string TenSach { get; set; } = null!;

    [Required(ErrorMessage = "Gia phải là số và nằm trong giới hạn từ 50 đến 1000")]
    [Range(50, 1000, ErrorMessage = "Gia phải là số và nằm trong giới hạn từ 50 đến 1000")]
    [Display(Name = "Gia")]
    public decimal? Gia { get; set; }

    [Required(ErrorMessage = "Anh không được để trống")]
    [RegularExpression(@".*\.(jpg|jpeg|png|webp)$", ErrorMessage = "Tên file ảnh (Anh) phải có đuôi: .jpg | .jpeg | .png | .webp")]
    [Display(Name = "Anh")]
    public string? Anh { get; set; }

    [ValidateNever]
    public virtual TheLoai? MaTlNavigation { get; set; }
}
