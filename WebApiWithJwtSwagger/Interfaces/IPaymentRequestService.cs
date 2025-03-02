using WebApiWithJwtSwagger.Dto;
using WebApiWithJwtSwagger.Models;

namespace WebApiWithJwtSwagger.Interfaces
{
    public interface IPaymentRequestService
    {
       // public  ResultDto<List<PaymentRequestDto>> GetAll();
        public Task< ResultDto<List<PaymentRequestDto>>> GetAll();
        public Task<ResultDto> AddPaymentAsync(PaymentRequestDto paymentRequestDto);
        public Task<ResultDto> DeletePaymentAsync(long  paymentId);
        public  Task<ResultDto> UpdatePaymentAsync(long id, PaymentRequestDto paymentRequestDto);
        public Task<ResultDto<PaymentRequestDto>> GetPaymentAsync(long id);
    }
}
