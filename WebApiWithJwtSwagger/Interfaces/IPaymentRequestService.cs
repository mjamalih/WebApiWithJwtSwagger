using WebApiWithJwtSwagger.Dto;
using WebApiWithJwtSwagger.Models;

namespace WebApiWithJwtSwagger.Interfaces
{
    public interface IPaymentRequestService
    {
        // public  ResultDto<List<PaymentRequestDto>> GetAll();
        public Task<List<PaymentRequestDto>> GetAll();
        public Task AddPaymentAsync(PaymentRequestDto paymentRequestDto);
        public Task DeletePaymentAsync(long paymentId);
        public Task UpdatePaymentAsync(long id, PaymentRequestDto paymentRequestDto);
        public Task<PaymentRequestDto> GetPaymentAsync(long id);

        public Task DoPaymentAsync();
        public Task ZPayment();
        public Task VerifyByHttpClient();
        public Task VerifyPayment();
    }
}
