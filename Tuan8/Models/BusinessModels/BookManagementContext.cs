using Microsoft.EntityFrameworkCore;
using Tuan8.Models.DataModels;

namespace Tuan8.Models.BusinessModels
{
    public class BookManagementContext : DbContext
    {
        public BookManagementContext(DbContextOptions<BookManagementContext> options) : base(options)
        {
        }

        // Khai báo các thuộc tính biểu diễn các tập thực thể
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Publisher> Publishers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Dữ liệu mẫu (seeding)
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, CategoryName = "Công nghệ thông tin" },
                new Category { CategoryId = 2, CategoryName = "Kinh tế & Quản trị" },
                new Category { CategoryId = 3, CategoryName = "Văn học nghệ thuật" }
            );

            modelBuilder.Entity<Publisher>().HasData(
                new Publisher { PublisherId = 1, PublisherName = "NXB Giáo Dục", Phone = "0243822080", Address = "81 Trần Hưng Đạo, Hà Nội" },
                new Publisher { PublisherId = 2, PublisherName = "NXB Kim Đồng", Phone = "0243943473", Address = "55 Quang Trung, Hà Nội" },
                new Publisher { PublisherId = 3, PublisherName = "NXB Trẻ", Phone = "0283931628", Address = "161B Lý Chính Thắng, TP.HCM" }
            );

            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    BookId = "B001",
                    Title = "Lập trình C# và .NET Toàn tập",
                    Author = "Phạm Công Hùng",
                    Release = 2024,
                    Price = 150000,
                    Description = "Giáo trình hướng dẫn lập trình C# từ cơ bản đến nâng cao",
                    Picture = "book1.jpg",
                    CategoryId = 1,
                    PublisherId = 1
                },
                new Book
                {
                    BookId = "B002",
                    Title = "Entity Framework Core Thực chiến",
                    Author = "Nguyễn Văn Hưng",
                    Release = 2024,
                    Price = 180000,
                    Description = "Hướng dẫn chi tiết Code-First và Database-First với EF Core",
                    Picture = "book2.jpg",
                    CategoryId = 1,
                    PublisherId = 1
                },
                new Book
                {
                    BookId = "B003",
                    Title = "Quản trị kinh doanh hiện đại",
                    Author = "Lê Thị Lan",
                    Release = 2023,
                    Price = 120000,
                    Description = "Kiến thức quản trị doanh nghiệp trong thời đại số",
                    Picture = "book3.jpg",
                    CategoryId = 2,
                    PublisherId = 3
                }
            );
        }
    }
}
