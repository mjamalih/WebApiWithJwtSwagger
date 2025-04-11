using  WebApiWithJwtSwagger.Models;
using  WebApiWithJwtSwagger.Server.Models.Config;
using Microsoft.EntityFrameworkCore;

namespace WebApiWithJwtSwagger.Models
{

    public class PaymentDbContext(DbContextOptions<PaymentDbContext> options) : DbContext(options)
    {
        public DbSet<PaymentRequest> PaymentRequest { get; set; }
        public DbSet<AmountDetail> AmountDetail { get; set; }
        public DbSet<SubModelDetail> SubModelDetail { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            EntitiesConfigs(modelBuilder);
        }
        private static void EntitiesConfigs(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PaymentRequestConfig());
        }
    }
}