using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tuan8.Models.DataModels
{
    [Table("Categories")]
    public class Category
    {
        [Key]
        [DisplayName("Mã loại")]
        public int CategoryId { get; set; }

        [DisplayName("Tên loại")]
        [StringLength(100)]
        [Required(ErrorMessage = "Tên loại không được để trống")]
        public string CategoryName { get; set; }

        // Thuộc tính quan hệ
        public ICollection<Book> Books { get; set; }
    }
}
