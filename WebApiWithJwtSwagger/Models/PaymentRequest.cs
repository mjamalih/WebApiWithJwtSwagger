using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using  WebApiWithJwtSwagger.Models.Common;
using static WebApiWithJwtSwagger.UseFull;
namespace  WebApiWithJwtSwagger.Models
{
    public class PaymentRequest:BaseEntity
    {
        [Column(TypeName = "numeric(9,0)")]
        public  long Amount { get; set; }
        public string RequestDate { get; set; }= DateTime.Now.Date.ToShortDateString();
        public string RequestTime { get; set; } = DateTime.Now.ToShortTimeString();
        public long TrxNumber { get; set; }

        public required long UserId { get; set; }
        public string? CustomerMobileNumber { get; set; }
       

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PaymentStatus PaymentStatus { get; set; }
        

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PaymentType paymentType { get; set; }

        public string? Description { get; set; }
        public string? TrackingNumber { get; set; }

        [AllowNull]
        public virtual ICollection<AmountDetail>? AmountDetails { get; set; }

        [AllowNull]
        public virtual ICollection<SubModelDetail>? Details { get; set; }

    }
}
