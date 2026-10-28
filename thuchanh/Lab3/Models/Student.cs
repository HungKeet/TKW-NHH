using System;
using System.ComponentModel.DataAnnotations;

namespace MyWebApp.Models;

public class Student
{
    [Display(Name = "Mã sinh viên")]
    public int Id { get; set; }

    [Display(Name = "Họ và tên")]
    [Required(ErrorMessage = "Họ và tên bắt buộc phải nhập")]
    [StringLength(100, MinimumLength = 4, ErrorMessage = "Họ tên phải từ 4 đến 100 ký tự")]
    public string? Name { get; set; }

    [Display(Name = "Địa chỉ Email")]
    [Required(ErrorMessage = "Email bắt buộc phải được nhập")]
    [RegularExpression(@"^[A-Za-z0-9._%+-]+@gmail\.com$", ErrorMessage = "Địa chỉ email phải có đuôi @gmail.com")]
    public string? Email { get; set; }

    [Display(Name = "Mật khẩu")]
    [Required(ErrorMessage = "Mật khẩu bắt buộc phải nhập")]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", 
        ErrorMessage = "Mật khẩu từ 8 ký tự trở lên, có ký tự viết hoa, viết thường, chữ số và ký tự đặc biệt (@$!%*?&)")]
    public string? Password { get; set; }

    [Display(Name = "Ngành học")]
    [Required(ErrorMessage = "Ngành học bắt buộc phải chọn")]
    [EnumDataType(typeof(Branch))]
    public Branch? Branch { get; set; }

    [Display(Name = "Giới tính")]
    [Required(ErrorMessage = "Giới tính bắt buộc phải chọn")]
    [EnumDataType(typeof(Gender))]
    public Gender? Gender { get; set; }

    [Display(Name = "Hệ đào tạo")]
    public bool IsRegular { get; set; }

    [Display(Name = "Địa chỉ")]
    [Required(ErrorMessage = "Địa chỉ bắt buộc phải nhập")]
    [DataType(DataType.MultilineText)]
    public string? Address { get; set; }

    [Display(Name = "Ngày sinh")]
    [Required(ErrorMessage = "Ngày sinh bắt buộc phải nhập")]
    [DataType(DataType.Date)]
    [Range(typeof(DateTime), "1/1/1963", "12/31/2005", ErrorMessage = "Ngày sinh phải nằm trong khoảng từ 01/01/1963 đến 31/12/2005")]
    public DateTime DateOfBorth { get; set; }

    [Display(Name = "Điểm")]
    [Required(ErrorMessage = "Điểm bắt buộc phải nhập")]
    [Range(0.0, 10.0, ErrorMessage = "Điểm phải là số thực từ 0.0 đến 10.0")]
    public double? Score { get; set; }

    [Display(Name = "Ảnh đại diện")]
    public string? AvatarPath { get; set; }
}
