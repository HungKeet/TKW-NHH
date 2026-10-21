using System.ComponentModel.DataAnnotations;

namespace MyWebApp.Models;

public class Student
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập họ tên.")]
    [StringLength(100)]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập email.")]
    [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập mật khẩu.")]
    [DataType(DataType.Password)]
    public string? Password { get; set; }

    [Required(ErrorMessage = "Vui lòng chọn ngành học.")]
    [EnumDataType(typeof(Branch))]
    public Branch? Branch { get; set; }

    [Required(ErrorMessage = "Vui lòng chọn giới tính.")]
    [EnumDataType(typeof(Gender))]
    public Gender? Gender { get; set; }

    public bool IsRegular { get; set; }

    [StringLength(200)]
    public string? Address { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime DateOfBorth { get; set; }
    public string? AvatarPath { get; set; }
}
