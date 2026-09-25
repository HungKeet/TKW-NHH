using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tuan6.Models.DataModels
{
    public class Member
    {
        [DisplayName("Mã thành viên")]
        public string MemberId { get; set; } = string.Empty;

        [DisplayName("Tên đăng nhập")]
        public string Username { get; set; } = string.Empty;

        [DisplayName("Họ và tên")]
        public string Fullname { get; set; } = string.Empty;

        [DisplayName("Mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [DisplayName("Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;
    }
}
