using Microsoft.EntityFrameworkCore;

namespace WebApiWithJwtSwagger.Models
{
    public class DatabaseContext:DbContext
    {
        public DatabaseContext(DbContextOptions obtion):base(obtion)
        {
                
        }
      public  DbSet<Products> Products { get; set; }  
      public  DbSet<UserInfo> Users { get; set; }  
      public  DbSet<PaymentRequest> PaymentRequest { get; set; }  
      public  DbSet<AmountDetail> AmountDetail { get; set; }  
      public  DbSet<SubModelDetail> SubModelDetail { get; set; }  
    }
}
