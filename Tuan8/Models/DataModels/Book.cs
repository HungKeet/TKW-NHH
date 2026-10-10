using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tuan8.Models.DataModels
{
    [Table("Books")]
    public class Book
    {
        [Key]
        [DisplayName("Mã sách")]
        [StringLength(10)]
        [Required(ErrorMessage = "Mã sách không được để trống")]
        public string BookId { get; set; }

        [DisplayName("Tên sách")]
        [StringLength(200)]
        [Required(ErrorMessage = "Tên sách không được để trống")]
        public string Title { get; set; }

        [DisplayName("Tác giả")]
        [StringLength(100)]
        public string Author { get; set; }

        [DisplayName("Năm xuất bản")]
        public int? Release { get; set; }

        [DisplayName("Giá")]
        public double? Price { get; set; }

        [DisplayName("Mô tả")]
        public string Description { get; set; }

        [DisplayName("Hình ảnh")]
        public string Picture { get; set; }

        [DisplayName("Mã nhà xuất bản")]
        public int? PublisherId { get; set; }

        [DisplayName("Mã loại")]
        public int? CategoryId { get; set; }

        // Tạo các quan hệ giữa các thực thể
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        [ForeignKey("PublisherId")]
        public virtual Publisher Publisher { get; set; }
    }
}
