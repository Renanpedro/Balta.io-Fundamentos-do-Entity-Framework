using Blog.Data.Mappings;
using Blog.Models;
using Microsoft.EntityFrameworkCore;


namespace Blog.Data{
    public class BlogDataContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Server=localhost\\SQLEXPRESS01;Database=Blog_Migrations;Trusted_Connection=True;TrustServerCertificate=True;");
            // options.LogTo(Console.WriteLine); //Mostra as querys que serão executadas
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryMap());
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new PostMap());
        }
    }
}