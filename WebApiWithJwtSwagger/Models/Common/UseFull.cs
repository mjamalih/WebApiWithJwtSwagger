namespace WebApiWithJwtSwagger.Models.Common
{
    public class UseFull
    {
        public enum PaymentStatus
        {
            SentToCustomer=1,
            WaitForConfirm,
            Confirmed,
            Rejected
        }
    }
}
