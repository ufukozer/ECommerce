using ECommerce.Models;
using Microsoft.EntityFrameworkCore;


namespace ECommerce
{
    public class ECommerceContext : DbContext
    {
        public ECommerceContext()
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            //DB BAĞLANTISI VB DATABASE INSTANCE'INI İLGİLENDİREN İNCE AYARLAR
            dbContextOptionsBuilder.UseSqlServer(
                "Server=127.0.0.1;" +
                "Database=ECommerce;" +
                "User Id=sa;" +
                "Password = 123;"
                );
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //DATABASE ŞEMASI UYGULANIRKEN KULLANILACAK KURAL SETLERİ
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }
}
