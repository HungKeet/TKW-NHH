using System.ComponentModel.DataAnnotations;

namespace Tuan7.Models
{
    public class Category
    {
        [Key]
        [Display(Name = "Mã danh mục")]
        public int Id { get; set; }

        [Display(Name = "Tên danh mục")]
        [Required(ErrorMessage = "Tên danh mục không được để trống")]
        public string Name { get; set; }
    }
}
