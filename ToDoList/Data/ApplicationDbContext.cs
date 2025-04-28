// Data/ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using ToDoList.Models;

namespace ToDoList.Data
{
    public class ApplicationDbContext : DbContext
    {
  
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        public DbSet<TodoItem> TodoItems { get; set; }

 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
       
            modelBuilder.Entity<TodoItem>()
                .Property(t => t.IsCompleted)
                .HasDefaultValue(false);
        }
    }


    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
 
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            
   
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            
    
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            
            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
    
}
