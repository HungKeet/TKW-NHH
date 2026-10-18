using System;
using Microsoft.EntityFrameworkCore;

namespace Tuan9.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Banner> Banners { get; set; }
    public DbSet<Blog> Blogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Category>()
            .HasIndex(c => c.Name)
            .IsUnique();

        modelBuilder.Entity<Product>()
            .HasIndex(p => p.Name)
            .IsUnique();

        modelBuilder.Entity<Banner>()
            .HasIndex(b => b.Name)
            .IsUnique();

        modelBuilder.Entity<Blog>()
            .HasIndex(b => b.Name)
            .IsUnique();

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Thời trang nam", Status = 1, CreatedDate = new DateTime(2026, 1, 1), Image = "cat-1.jpg", Description = "Quần áo và phụ kiện nam" },
            new Category { Id = 2, Name = "Thời trang nữ", Status = 1, CreatedDate = new DateTime(2026, 1, 2), Image = "cat-2.jpg", Description = "Váy đầm và thời trang nữ hiện đại" },
            new Category { Id = 3, Name = "Phụ kiện công nghệ", Status = 1, CreatedDate = new DateTime(2026, 1, 3), Image = "cat-3.jpg", Description = "Tai nghe, ốp lưng, đồng hồ thông minh" },
            new Category { Id = 4, Name = "Giày dép thể thao", Status = 1, CreatedDate = new DateTime(2026, 1, 4), Image = "cat-4.jpg", Description = "Giày sneaker, giày chạy bộ cao cấp" }
        );

        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Áo sơ mi nam Oxford", Price = 350000, SalePrice = 299000, Status = 1, CategoryId = 1, CreatedDate = new DateTime(2026, 1, 10), Image = "prod-1.jpg", Description = "Chất liệu cotton cao cấp thoáng mát" },
            new Product { Id = 2, Name = "Áo polo nam Classic", Price = 280000, SalePrice = 250000, Status = 1, CategoryId = 1, CreatedDate = new DateTime(2026, 1, 11), Image = "prod-2.jpg", Description = "Thiết kế trẻ trung năng động" },
            new Product { Id = 3, Name = "Đầm hoa nhí Vintage", Price = 450000, SalePrice = 380000, Status = 1, CategoryId = 2, CreatedDate = new DateTime(2026, 1, 12), Image = "prod-3.jpg", Description = "Đầm dáng xòe nữ tính dịu dàng" },
            new Product { Id = 4, Name = "Tai nghe không dây TWS", Price = 690000, SalePrice = 590000, Status = 1, CategoryId = 3, CreatedDate = new DateTime(2026, 1, 13), Image = "prod-4.jpg", Description = "Âm thanh sống động chống ồn chủ động" },
            new Product { Id = 5, Name = "Giày thể thao Running Pro", Price = 850000, SalePrice = 720000, Status = 1, CategoryId = 4, CreatedDate = new DateTime(2026, 1, 14), Image = "prod-5.jpg", Description = "Đế đệm khí êm ái bảo vệ khớp gối" }
        );

        modelBuilder.Entity<Banner>().HasData(
            new Banner { Id = 1, Name = "Khuyến mãi mùa hè 2026", Status = 1, Prioty = 1, Image = "banner-1.jpg", Description = "Giảm giá lên đến 50% cho tất cả sản phẩm thời trang" },
            new Banner { Id = 2, Name = "Bộ sưu tập mới Thu Đông", Status = 1, Prioty = 2, Image = "banner-2.jpg", Description = "Khám phá các thiết kế mới nhất vừa cập bến" }
        );

        modelBuilder.Entity<Blog>().HasData(
            new Blog { Id = 1, Name = "Xu hướng thời trang tối giản 2026", Status = 1, CreatedDate = new DateTime(2026, 1, 15), Image = "blog-1.jpg", Description = "Phong cách tối giản minimalism đang thống trị xu hướng hiện đại." },
            new Blog { Id = 2, Name = "Bí quyết chọn giày chạy bộ phù hợp", Status = 1, CreatedDate = new DateTime(2026, 1, 16), Image = "blog-2.jpg", Description = "Cách chọn đôi giày vừa vặn cho buổi chạy đạt hiệu quả cao nhất." },
            new Blog { Id = 3, Name = "Top 5 phụ kiện công nghệ đáng mua nhất", Status = 1, CreatedDate = new DateTime(2026, 1, 17), Image = "blog-3.jpg", Description = "Tổng hợp những thiết bị công nghệ hữu ích cho cuộc sống hàng ngày." }
        );
    }
}
