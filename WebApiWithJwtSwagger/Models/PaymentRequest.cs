using System.Collections.ObjectModel;
using WebApiWithJwtSwagger.Models.Common;
using static WebApiWithJwtSwagger.Models.Common.UseFull;

namespace WebApiWithJwtSwagger.Models
{
    public class PaymentRequest:BaseEntity
    {
        public  long Amount { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime RequestTime { get; set; }
        public long TrxNumber { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public virtual ICollection<AmountDetail>? AmountDetails { get; set; }
        public virtual ICollection<SubModelDetail>? SubModelDetails { get; set; }

    }
}
