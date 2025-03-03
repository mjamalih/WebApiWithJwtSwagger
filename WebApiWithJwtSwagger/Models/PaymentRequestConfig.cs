using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WebApiWithJwtSwagger.Models.Common.UseFull;

 namespace WebApiWithJwtSwagger.Models
{
    public class PaymentRequestConfig : IEntityTypeConfiguration<PaymentRequest>
    {
        public void Configure(EntityTypeBuilder<PaymentRequest> builder)
        {
            builder
                .Property(k => k.PaymentStatus)
                .HasConversion(
                v => v.ToString(),
                v => (PaymentStatus)Enum.Parse(typeof(PaymentStatus), v));
              builder.HasQueryFilter(p => !p.IsRemove);


        }
    }
}
