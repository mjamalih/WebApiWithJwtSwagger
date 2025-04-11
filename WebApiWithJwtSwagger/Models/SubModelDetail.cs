using System.ComponentModel.DataAnnotations.Schema;

namespace  WebApiWithJwtSwagger.Models
{
    public class SubModelDetail
    {
        public long Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public long? RequestPaymentId { get; set; }

        [ForeignKey("RequestPaymentId")]
        public virtual PaymentRequest? PaymentRequest { get; set; }

    }
}
