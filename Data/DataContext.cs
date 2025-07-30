using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace dotnet_store.Data;

public class DataContext : IdentityDbContext<AppUser, AppRole, string>
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        
    }
    public DbSet<Product> Products{ get; set; }
    public DbSet<Category> Categories{ get; set; }
    public DbSet<Slider> Sliders{ get; set; }
    public DbSet<Cart> Carts{ get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Slider>().HasData(
            new List<Slider> {
                new Slider { Id=1, Title="Slider 1 Başlık", Description="Slider 1 Açıklama", Image="slider-1.jpeg", IsActive=true, Index=0},
                new Slider { Id=2, Title="Slider 2 Başlık", Description="Slider 2 Açıklama", Image="slider-2.jpeg", IsActive=true, Index=1},
                new Slider { Id=3, Title="Slider 3 Başlık", Description="Slider 3 Açıklama", Image="slider-3.jpeg", IsActive=true, Index=2},
            }
        );

        modelBuilder.Entity<Category>().HasData(
            new List<Category>() {
                new Category { Id=1, CategoryName="Telefon", Url="telefon"},
                new Category { Id=2, CategoryName="Elektronik", Url="elektronik"},
                new Category { Id=3, CategoryName="Beyaz Eşya", Url="beyaz-esya"},
                new Category { Id=4, CategoryName="Giyim", Url="giyim"},
                new Category { Id=5, CategoryName="Kozmetik", Url="kozmetik"},
                new Category { Id=6, CategoryName="Ayakkabı", Url="ayakkabi"},
                new Category { Id=7, CategoryName="Çanta", Url="canta"},
                new Category { Id=8, CategoryName="Kişisel Bakım", Url="kisisel-bakim"},
                new Category { Id=9, CategoryName="Saat ve Aksesuar", Url="saat-ve-aksesuar"},
                new Category { Id=10, CategoryName="Spor ve Outdoor", Url="spor-ve-outdoor"},
                new Category { Id=11, CategoryName="Hobi", Url="hobi"},
                new Category { Id=12, CategoryName="Kitap", Url="kitap"},
                new Category { Id=13, CategoryName="Takı", Url="taki"},
            }
        );

        modelBuilder.Entity<Product>().HasData(
            new List<Product>() {
                new Product() {
                    Id = 1,
                    ProductName = "Apple Watch 7",
                    Price = 10000,
                    IsActive = false,
                    Image ="1.jpeg",
                    IsHome=true,
                    Stock=0,
                    CategoryId=1,
                    Description="Lorem ipsum, dolor sit amet consectetur adipisicing elit. Enim consequatur fugiat laborum. Enim, quod. Itaque repellat vero corrupti autem, eius officiis quibusdam reprehenderit voluptatibus iusto maiores dolorem consectetur, eum optio?"
                    },
                new Product() {
                    Id = 2,
                    ProductName = "Apple Watch 8",
                    Price = 20000,
                    IsActive = true,
                    Image ="2.jpeg",
                    IsHome=true,
                    Stock=35,
                    CategoryId=1,
                    Description="Lorem ipsum, dolor sit amet consectetur adipisicing elit. Enim consequatur fugiat laborum. Enim, quod. Itaque repellat vero corrupti autem, eius officiis quibusdam reprehenderit voluptatibus iusto maiores dolorem consectetur, eum optio?"
                    },
                new Product() {
                    Id = 3,
                    ProductName = "Apple Watch 9",
                    Price = 30000,
                    IsActive = true,
                    Image ="3.jpeg",
                    IsHome=true,
                    Stock=12,
                    CategoryId=2,
                    Description="Lorem ipsum, dolor sit amet consectetur adipisicing elit. Enim consequatur fugiat laborum. Enim, quod. Itaque repellat vero corrupti autem, eius officiis quibusdam reprehenderit voluptatibus iusto maiores dolorem consectetur, eum optio?"
                    },
                new Product() {
                    Id = 4,
                    ProductName = "Apple Watch 10",
                    Price = 40000,
                    IsActive = false,
                    Image ="4.jpeg",
                    IsHome=true,
                    Stock=0,
                    CategoryId=2,
                    Description="Lorem ipsum, dolor sit amet consectetur adipisicing elit. Enim consequatur fugiat laborum. Enim, quod. Itaque repellat vero corrupti autem, eius officiis quibusdam reprehenderit voluptatibus iusto maiores dolorem consectetur, eum optio?"
                    },
                new Product() {
                    Id = 5,
                    ProductName = "Apple Watch 11",
                    Price = 50000,
                    IsActive = true,
                    Image ="5.jpeg",
                    IsHome=true,
                    Stock=20,
                    CategoryId=3,
                    Description="Lorem ipsum, dolor sit amet consectetur adipisicing elit. Enim consequatur fugiat laborum. Enim, quod. Itaque repellat vero corrupti autem, eius officiis quibusdam reprehenderit voluptatibus iusto maiores dolorem consectetur, eum optio?"
                    },
                new Product() {
                    Id = 6,
                    ProductName = "Apple Watch 12",
                    Price = 60000,
                    IsActive = true,
                    Image ="6.jpeg",
                    IsHome=false,
                    Stock=14,
                    CategoryId=3,
                    Description="Lorem ipsum, dolor sit amet consectetur adipisicing elit. Enim consequatur fugiat laborum. Enim, quod. Itaque repellat vero corrupti autem, eius officiis quibusdam reprehenderit voluptatibus iusto maiores dolorem consectetur, eum optio?"
                    },
                new Product() {
                    Id = 7,
                    ProductName = "Apple Watch 13",
                    Price = 70000,
                    IsActive = true,
                    Image ="7.jpeg",
                    IsHome=true,
                    Stock=7,
                    CategoryId=4,
                    Description="Lorem ipsum, dolor sit amet consectetur adipisicing elit. Enim consequatur fugiat laborum. Enim, quod. Itaque repellat vero corrupti autem, eius officiis quibusdam reprehenderit voluptatibus iusto maiores dolorem consectetur, eum optio?"
                    },
                new Product() {
                    Id = 8,
                    ProductName = "Apple Watch 14",
                    Price = 80000,
                    IsActive = true,
                    Image ="8.jpeg",
                    IsHome=true,
                    Stock=72,
                    CategoryId=4,
                    Description="Lorem ipsum, dolor sit amet consectetur adipisicing elit. Enim consequatur fugiat laborum. Enim, quod. Itaque repellat vero corrupti autem, eius officiis quibusdam reprehenderit voluptatibus iusto maiores dolorem consectetur, eum optio?"
                    },
            }
        );
    }
} 