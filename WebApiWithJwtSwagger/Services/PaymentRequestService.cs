using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using WebApiWithJwtSwagger.Dto;
using WebApiWithJwtSwagger.Interfaces;
using WebApiWithJwtSwagger.Models;

namespace WebApiWithJwtSwagger.Services
{
    public class PaymentRequestService : IPaymentRequestService
    {
        private readonly DatabaseContext _context;
        public PaymentRequestService(DatabaseContext databaseContext)
        {
            _context = databaseContext;
        }

        public Task<ResultDto> AddPaymentAsync(PaymentRequestDto paymentRequestDto)
        {
            throw new NotImplementedException();
        }

        public async Task<ResultDto> DeletePaymentAsync(long paymentId)
        {
            try
            {
                var payemnt = await _context.PaymentRequest.FindAsync(paymentId);
                payemnt.IsRemove=true;
                return new ResultDto()
                {
                    IsSuccess = true,
                    Status = 200
                };
            }
            catch (Exception)
            {

                return new ResultDto()
                {
                    IsSuccess = false,
                    Status = 205
                };
            }
           

        }

        public async Task<ResultDto<List<PaymentRequestDto>>> GetAll()
        {
            try
            {
                var result = await _context.PaymentRequest
                    .Include(p => p.AmountDetails)
                    .Include(P => P.SubModelDetails)
                    .ToListAsync();
                var dataList = new List<PaymentRequestDto>();
                foreach (var item in result)
                {
                    dataList.Add(
                        new PaymentRequestDto()
                        {
                            Amount = item.Amount,
                            PaymentStatus = item.PaymentStatus,
                            RequestDate = item.RequestDate,
                            RequestTime = item.RequestTime,
                            TrxNumber = item.TrxNumber,
                            //AmountDetails = item.AmountDetails.
                            AmountDetails = item.AmountDetails?.Select(rowSub =>
                                      new AmountDetailDto
                                      {
                                          Effect = rowSub.Effect,
                                          Key = rowSub.Key,
                                          Value = rowSub.Value,
                                      }).ToList(),
                            SubModelDetails = item.SubModelDetails?.Select(
                                rowSub => new SubModelDetailDto
                                {
                                    Key = rowSub.Key,
                                    Value = rowSub.Value,
                                }).ToList()
                        });
                }
                return new ResultDto<List<PaymentRequestDto>>()
                {
                    IsSuccess = true,
                    Data = dataList,
                    Message = "all data"
                };
            }
            catch (Exception)
            {

                throw;
            }



        }

        public async Task<ResultDto<PaymentRequestDto>> GetPaymentAsync(long id)
        {
            try
            {
                var payment = await _context.PaymentRequest
                    .Include(p => p.SubModelDetails)
                    .Include(p => p.AmountDetails).FirstOrDefaultAsync(p=>p.Id==id);
                    
                if (payment == null)
                    return new ResultDto<PaymentRequestDto>()
                    {
                        IsSuccess = false,
                        Message = "Not Found",
                        Status = 200,
                        Data = null
                    };
                return new ResultDto<PaymentRequestDto>()
                {
                    IsSuccess = true,
                    Status = 200,
                    Data = new PaymentRequestDto()
                    {
                        Amount = payment.Amount,
                        PaymentStatus = payment.PaymentStatus,
                        RequestDate = payment.RequestDate,
                        RequestTime = payment.RequestTime,
                        TrxNumber = payment.TrxNumber,
                        AmountDetails = payment.AmountDetails?
                          .Select(a => new AmountDetailDto()
                          {
                              Effect = a.Effect,
                              Key = a.Key,
                              Value = a.Value
                          }).ToList(),
                        SubModelDetails = payment.SubModelDetails?.Select(sub =>
                        new SubModelDetailDto()
                        {
                            Key = sub.Key,
                            Value = sub.Value
                        }).ToList(),
                    },

                };
            }
            catch (Exception)
            {

                return new ResultDto<PaymentRequestDto>()
                {
                    IsSuccess = false,
                    Data = null,
                    Message = "خطا در یافتن پرداختی",
                    Status = 209
                };
            }
        }

        public async Task<ResultDto> UpdatePaymentAsync(long id,PaymentRequestDto paymentRequestDto)
        {

            try
            {
                var payemnt =await _context.PaymentRequest.FindAsync(id);
                
                if (payemnt != null)
                {
                    payemnt.TrxNumber = paymentRequestDto.TrxNumber;
                    payemnt.UpdateTime = DateTime.Now;
                    payemnt.RequestDate = paymentRequestDto.RequestDate;
                    payemnt.SubModelDetails = (ICollection<SubModelDetail>?)(paymentRequestDto.SubModelDetails?.Select(sub =>
                        new SubModelDetailDto()
                        {
                            Key = sub.Key,
                            Value = sub.Value
                        }));
                    payemnt.Amount = paymentRequestDto.Amount;
                    payemnt.AmountDetails = (ICollection<AmountDetail>?)paymentRequestDto.AmountDetails.ToList();
                }
                _context.SaveChanges();
                return new ResultDto()
                {
                    IsSuccess = true,
                    Status = 200,

                };
            }
            catch (Exception)
            {

                throw;
            }
        }

        //public ResultDto<List<PaymentRequestDto>> GetAll()
        //{
        //    try
        //    {
        //        var result = _context.PaymentRequest
        //            .Include(p => p.AmountDetails)
        //            .Include(P=>P.SubModelDetails)
        //            .ToList();
        //        var dataList = new List<PaymentRequestDto>();
        //        foreach (var item in result)
        //        {
        //            dataList.Add(
        //                new PaymentRequestDto()
        //                {
        //                    Amount = item.Amount,
        //                    PaymentStatus = item.PaymentStatus,
        //                    RequestDate = item.RequestDate,
        //                    RequestTime = item.RequestTime,
        //                    TrxNumber = item.TrxNumber,
        //                    //AmountDetails = item.AmountDetails.
        //                    AmountDetails = item.AmountDetails?.Select(rowSub =>
        //                              new AmountDetailDto
        //                              {
        //                                  Effect = rowSub.Effect,
        //                                  Key = rowSub.Key,
        //                                  Value = rowSub.Value,
        //                              }).ToList(),
        //                    SubModelDetails = item.SubModelDetails?.Select(
        //                        rowSub => new SubModelDetailDto
        //                        {
        //                            Key = rowSub.Key,
        //                            Value = rowSub.Value,
        //                        }).ToList()
        //                });
        //        }
        //        return new ResultDto<List<PaymentRequestDto>>()
        //        {
        //            IsSuccess = true,
        //            Data = dataList,
        //            Message = "all data"
        //        };
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }



        //}
    }
}
