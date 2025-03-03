using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using WebApiWithJwtSwagger.Models.Common;
using static WebApiWithJwtSwagger.Models.Common.UseFull;

namespace WebApiWithJwtSwagger.Models
{
    public class PaymentRequest:BaseEntity
    {
        public  long Amount { get; set; }
        public string RequestDate { get; set; }= DateTime.Now.Date.ToShortDateString();
        public string RequestTime { get; set; } = DateTime.Now.ToShortTimeString();
        public long TrxNumber { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PaymentStatus PaymentStatus { get; set; }
        public virtual ICollection<AmountDetail>? AmountDetails { get; set; }
        public virtual ICollection<SubModelDetail>? SubModelDetails { get; set; }

    }
}
