using System.Collections.ObjectModel;
using WebApiWithJwtSwagger.Models;
using static WebApiWithJwtSwagger.Models.Common.UseFull;

namespace WebApiWithJwtSwagger.Dto
{
    public class PaymentRequestDto
    {

        //public long Id { get; set; }
        public long Amount { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime RequestTime { get; set; }
        public long TrxNumber { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public ICollection<AmountDetailDto>? AmountDetails { get; set; }
        public ICollection<SubModelDetailDto>? SubModelDetails { get; set; }

    }
}
