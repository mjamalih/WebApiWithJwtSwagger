using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using  WebApiWithJwtSwagger.Dto;
using  WebApiWithJwtSwagger.Interfaces;
using  WebApiWithJwtSwagger.Server;
using Microsoft.AspNetCore.Http.HttpResults;
using  WebApiWithJwtSwagger.Models;
namespace  WebApiWithJwtSwagger.Services
{
    public class PaymentRequestService : IPaymentRequestService
    {
        private readonly PaymentDbContext _context;
        public PaymentRequestService(PaymentDbContext databaseContext)
        {
            _context = databaseContext;
        }

        public async Task AddPaymentAsync(PaymentRequestDto paymentRequestDto)
        {

            try
            {
                List<AmountDetail>? amountDetailList = new List<AmountDetail>();
                amountDetailList = paymentRequestDto.AmountDetails?.Select(p => new AmountDetail()
                {
                    Effect = p.Effect,
                    Key = p.Key,
                    Value = p.Value
                }).ToList();


                List<SubModelDetail>? subModellist = new List<SubModelDetail>();
                subModellist = paymentRequestDto.SubModelDetails?.Select(sub =>
                new SubModelDetail()
                {
                    Key = sub.Key,
                    Value = sub.Value
                }).ToList();



                PaymentRequest payRequest = new PaymentRequest()
                {
                    Amount = paymentRequestDto.Amount,
                    AmountDetails = amountDetailList,
                    Details = subModellist,
                    TrxNumber = paymentRequestDto.TrxNumber,
                    PaymentStatus = paymentRequestDto.PaymentStatus,
                    RequestDate = paymentRequestDto.RequestDate,
                    RequestTime = paymentRequestDto.RequestTime,
                    Description = paymentRequestDto.Description,
                    paymentType = paymentRequestDto.paymentType,
                    TrackingNumber = paymentRequestDto.TrackingNumber,
                    UserId = paymentRequestDto.UserId,
                    CustomerMobileNumber = paymentRequestDto.CustomerMobileNumber

                };
                await _context.PaymentRequest.AddAsync(payRequest);
                await _context.SaveChangesAsync();

            }
            catch (Exception)
            {

                throw new Exception("not found");


            }
        }

        public async Task DeletePaymentAsync(long paymentId)
        {
            try
            {
                var payemnt = await _context.PaymentRequest.FindAsync(paymentId);
                payemnt.IsRemove = true;
                
            }
            catch (Exception)
            {

                throw new Exception("not found");
            }


        }

        public Task DoPaymentAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<PaymentRequestDto>> GetAll()
        {
            try
            {
                var paymentList = await _context.PaymentRequest
                    .Include(p => p.AmountDetails)
                    .Include(P => P.Details)
                    .ToListAsync();
                var dataList = new List<PaymentRequestDto>();
                foreach (var payment in paymentList)
                {
                    dataList.Add(
                        new PaymentRequestDto()
                        {
                            Id = payment.Id,
                            Amount = payment.Amount,
                            PaymentStatus = payment.PaymentStatus,
                            RequestDate = payment.RequestDate,
                            RequestTime = payment.RequestTime,
                            TrxNumber = payment.TrxNumber,
                            UserId = payment.UserId,
                            CustomerMobileNumber = payment.CustomerMobileNumber,

                            AmountDetails = payment.AmountDetails?.Select(rowSub =>
                                      new AmountDetailDto
                                      {
                                          Effect = rowSub.Effect,
                                          Key = rowSub.Key,
                                          Value = rowSub.Value,
                                      }).ToList(),
                            SubModelDetails = payment.Details?.Select(
                                rowSub => new SubModelDetailDto
                                {
                                    Key = rowSub.Key,
                                    Value = rowSub.Value,
                                }).ToList()
                        });
                }
                return await Task.FromResult(dataList);
            }
            catch (Exception)
            {

                throw new Exception("not found");
            }



        }

        public async Task<PaymentRequestDto> GetPaymentAsync(long id)
        {
            try
            {
                var payment = await _context.PaymentRequest
                    .Include(p => p.Details)
                    .Include(p => p.AmountDetails).FirstOrDefaultAsync(p => p.Id == id);

                if (payment == null)
                    throw new Exception("not found");

                var result=  new PaymentRequestDto()
                {
                    Id = payment.Id,
                    Amount = payment.Amount,
                    PaymentStatus = payment.PaymentStatus,
                    RequestDate = payment.RequestDate,
                    RequestTime = payment.RequestTime,
                    TrxNumber = payment.TrxNumber,
                    Description = payment.Description,
                    paymentType = payment.paymentType,
                    TrackingNumber = payment.TrackingNumber,
                    UserId = payment.UserId,
                    CustomerMobileNumber = payment.CustomerMobileNumber,


                    AmountDetails = payment.AmountDetails?
                          .Select(a => new AmountDetailDto()
                          {
                              Effect = a.Effect,
                              Key = a.Key,
                              Value = a.Value
                          }).ToList(),
                    SubModelDetails = payment.Details?.Select(sub =>
                    new SubModelDetailDto()
                    {
                        Key = sub.Key,
                        Value = sub.Value
                    }).ToList(),
                };

                return await Task.FromResult(result);
            }
            catch (Exception ex)
            {

                throw new Exception("not found");
            }
        }

        public async Task UpdatePaymentAsync(long id, PaymentRequestDto paymentRequestDto)
        {

            try
            {
                var payemnt = await _context.PaymentRequest.FindAsync(id);

                if (payemnt != null)
                {
                    payemnt.TrxNumber = paymentRequestDto.TrxNumber;
                    payemnt.UpdateTime = DateTime.Now;
                    payemnt.RequestDate = paymentRequestDto.RequestDate;
                    payemnt.Description = paymentRequestDto.Description;
                    payemnt.paymentType = paymentRequestDto.paymentType;
                    payemnt.TrackingNumber = paymentRequestDto.TrackingNumber;
                    payemnt.UserId = paymentRequestDto.UserId;
                    payemnt.CustomerMobileNumber = paymentRequestDto.CustomerMobileNumber;
                    payemnt.Details = (ICollection<SubModelDetail>?)(paymentRequestDto.SubModelDetails?.Select(sub =>
                        new SubModelDetailDto()
                        {
                            Key = sub.Key,
                            Value = sub.Value
                        }));
                    payemnt.Amount = paymentRequestDto.Amount;
                    payemnt.AmountDetails = (ICollection<AmountDetail>?)paymentRequestDto.AmountDetails.ToList();
                }
                _context.SaveChanges();
                
            }
            catch (Exception)
            {

                throw new Exception("not found");
            }
        }

        public Task VerifyByHttpClient()
        {
            throw new NotImplementedException();
        }

        public Task VerifyPayment()
        {
            throw new NotImplementedException();
        }

        public Task ZPayment()
        {
            throw new NotImplementedException();
        }
    }
}
