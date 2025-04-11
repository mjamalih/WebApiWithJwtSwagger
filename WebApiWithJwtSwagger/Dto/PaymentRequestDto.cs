using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using WebApiWithJwtSwagger.Models;
using static WebApiWithJwtSwagger.UseFull;

namespace WebApiWithJwtSwagger.Dto
{
    public class PaymentRequestDto
    {

        public long Id { get; set; }
        public long Amount { get; set; }
        public string RequestDate { get; set; } = DateTime.Now.Date.ToShortDateString();
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
        public ICollection<AmountDetailDto>? AmountDetails { get; set; }
        public ICollection<SubModelDetailDto>? SubModelDetails { get; set; }

    }
}
