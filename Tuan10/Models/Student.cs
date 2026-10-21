using System;
using System.ComponentModel.DataAnnotations;

namespace Tuan10.Models;

public class Student
{
    [Display(Name = "Mã")]
    public int Id { get; set; }

    [Display(Name = "Họ tên")]
    public string Name { get; set; }

    [Display(Name = "Email")]
    public string Email { get; set; }

    [Display(Name = "Mật khẩu")]
    public string Password { get; set; }

    [Display(Name = "Ngành học")]
    public Branch? Branch { get; set; }

    [Display(Name = "Giới tính")]
    public Gender? Gender { get; set; }

    [Display(Name = "Hệ đào tạo")]
    public bool IsRegular { get; set; }

    [Display(Name = "Địa chỉ")]
    public string Address { get; set; }

    [Display(Name = "Ngày sinh")]
    [DataType(DataType.Date)]
    public DateTime DateOfBorth { get; set; }

    [Display(Name = "Ảnh đại diện")]
    public string Avatar { get; set; }
}
