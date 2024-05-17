using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApiWithJwtSwagger.Areas.Identity.Data;

namespace WebApiWithJwtSwagger.Data;

public class WebApiWithJwtSwaggerContext : IdentityDbContext<WebApiWithJwtSwaggerUser>
{
    public WebApiWithJwtSwaggerContext(DbContextOptions<WebApiWithJwtSwaggerContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
