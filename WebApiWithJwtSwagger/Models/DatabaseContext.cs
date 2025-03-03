using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace WebApiWithJwtSwagger.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions obtion) : base(obtion)
        {

        }
        public DbSet<Products> Products { get; set; }
        // public  DbSet<UserInfo> Users { get; set; }  
        public DbSet<PaymentRequest> PaymentRequest { get; set; }
        public DbSet<AmountDetail> AmountDetail { get; set; }
        public DbSet<SubModelDetail> SubModelDetail { get; set; }
        public DbSet<UserInfo> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            EntitiesConfigs(modelBuilder);
        }
        private static void EntitiesConfigs(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PaymentRequestConfig());
        }
    }
}




//-- عدم نمایش اطلاعات با برچسب حذف شده
//ApplyQueryFilter(modelBuilder);
//        }

