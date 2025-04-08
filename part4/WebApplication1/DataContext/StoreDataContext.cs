using Microsoft.EntityFrameworkCore;
using Repository.Entities;
using Repository.Interface;

namespace DataContext
{
    public class StoreDataContext : DbContext, IContext
    {
        public StoreDataContext(DbContextOptions<StoreDataContext> options) : base(options)
        {
        }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<SupplierProducts> SupplierProducts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }

        public async Task Save()
        {
            await SaveChangesAsync();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-0HVQFLL\\MSSQLSERVER01; Database=Store; Integrated Security=True;Trust Server Certificate=True");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Supplier)
                .WithMany(s => s.Products)
                .HasForeignKey(p => p.SupplierId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SupplierProducts>()
                .HasKey(sp => new
                {
                    sp.SupplierId,
                    sp.ProductId
                });

            modelBuilder.Entity<OrderDetails>()
        .HasOne(od => od.Product)
        .WithMany()
        .HasForeignKey(od => od.ProductId)
        .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<OrderDetails>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
