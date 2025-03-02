using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiWithJwtSwagger.Models
{
    public class AmountDetail
    {
        public long Id { get; set; }
        public string Key { get; set; }
        public long Value { get; set; }
        public bool Effect { get; set; }
        public long? RequestPaymentId { get; set; }

        [ForeignKey("RequestPaymentId")]
        public virtual PaymentRequest? PaymentRequest { get; set; }

    }
}
