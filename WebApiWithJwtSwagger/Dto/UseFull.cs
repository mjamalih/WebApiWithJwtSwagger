namespace WebApiWithJwtSwagger
{
    public class UseFull
    {
        public enum PaymentStatus
        {
            SentToCustomer = 1,
            WaitForConfirm,
            Confirmed,
            Rejected
        }

        public enum PaymentType
        {
            DirectByBank,
            DepositReceipt,// فیش واریزی
            InterMediatePayment//پرداخت واسط
        }
        public abstract class URLs
        {
            //public const String gateWayUrl = "https://www.zarinpal.com/pg/StartPay/";
            //public const String requestUrl = "https://api.zarinpal.com/pg/v4/payment/request.json";
            //public const String verifyUrl = "https://api.zarinpal.com/pg/v4/payment/verify.json";

            public const String requestUrl = "https://sandbox.zarinpal.com/pg/v4/payment/request.json";

            public const String verifyUrl = "https://sandbox.zarinpal.com/pg/v4/payment/verify.json";
            public const String gateWayUrl = "https://sandbox.zarinpal.com/pg/StartPay/";


        }
    }
}
