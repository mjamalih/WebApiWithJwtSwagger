using  WebApiWithJwtSwagger.Dto;
using  WebApiWithJwtSwagger.Interfaces;
using  WebApiWithJwtSwagger.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApiWithJwtSwagger.Models;

namespace  WebApiWithJwtSwagger.Server.Services
{

    internal class PaymentService(PaymentDbContext dbContext, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IFennecDispatcher fennecDispatcher) : IPaymentService
    {
        public async Task<PaymentDto> PaymentGet(Guid payId)
        {
            try
            {
                var row = dbContext.PaymentModel.FirstOrDefault(row => row.Id == payId);
                if (row is null)
                    throw new BusinessConflictException("not found");

                //check verify
                row.Status = Verify(row, row.StatusGetway);

                var userInfo = fennecDispatcher.Query(new AllProfileQuery(1,10,row.UserId.ToString()));

                var paymentDto = new PaymentDto
                {
                    Amount = row.Amount,
                    DateTimeRequest = row.DateTimeRequest,
                    UserFullName = userInfo == null ? string.Empty : userInfo.Result.ToString(),
                    //jamali
                    Status = row.Status,
                    NumberTransaction = row.NumberTransaction,
                    Description = row.Description,
                    AmountDetails = row.AmountDetails?.Select(rowSub =>
                    new AmountDetailsSubDto
                    {
                        Effect = rowSub.Effect,
                        Key = rowSub.Key,
                        Val = rowSub.Val
                    }).ToList(),
                    Details = row.Details?.Select(rowSub =>
                    new DetailsSubDto
                    {
                        Key = rowSub.Key,
                        Val = rowSub.Val
                    }).ToList()
                };

                return await Task.FromResult(paymentDto);
            }
            catch
            {
                throw;
            }
        }

        public Task<Guid> PaymentRequest(PaymentRequestDto paymentDto)
        {
            try
            {
                var userId = CurrentUserId();
                if (userId.Equals(Guid.Empty))
                    throw new BusinessConflictException("CurrentUser UnPaid");

                List<AmountDetailsSubModel> amountDetails = [];
                List<DetailsSubModel> details = [];

                paymentDto.AmountDetails?.ForEach(item =>
                {
                    amountDetails.Add(new AmountDetailsSubModel
                    { Effect = item.Effect, Key = item.Key, Val = item.Val }
                    );
                });

                paymentDto.Details?.ForEach(item =>
                {
                    details.Add(new DetailsSubModel { Key = item.Key, Val = item.Val });
                });


                var paymentModule = new PaymentModel
                {
                    UserId = userId,
                    CallbackURL = paymentDto.CallbackURL,
                    Amount = paymentDto.Amount,
                    Description = paymentDto.Description,
                    Status = (byte)PaymentStatuses.Request,
                    ReferenceId = paymentDto.ReferenceId,
                    DateTimeRequest = DateTime.Now,
                    AmountDetails = (amountDetails.Count > 0 ? amountDetails : null),
                    Details = (details.Count > 0 ? details : null),
                };

                dbContext.PaymentModel.Add(paymentModule);
                dbContext.SaveChanges();
                return Task.FromResult(paymentModule.Id);
            }
            catch
            {
                throw;
            }

        }

        public async Task<string> PaymentStart(Guid payId)
        {
            var row = dbContext.PaymentModel.FirstOrDefault(row => row.Id == payId);
            if (row is null)
                throw new BusinessConflictException("not fount");
            else if (row.Status != (byte)PaymentStatuses.Request)
                throw new BusinessConflictException("Status UnPaid");


            var merchantId = configuration["Fennec:Payment:MerchantID"] ?? throw new BusinessConflictException("MerchantID UnPaid");
            var paymentGateway = configuration["Fennec:Payment:GatewayUrl"] ?? throw new BusinessConflictException("PaymentGateway UnPaid");

            var token = $"{Guid.NewGuid()}";//from web

            //Task.FromResult(string.Format("{0}/{1}", paymentGateway, token))

            var affectRow = dbContext.PaymentModel.Where(row => row.Id == payId)
                .ExecuteUpdate(row => row.SetProperty(col => col.Token, token)
                .SetProperty(col => col.Status, (byte)PaymentStatuses.Paying));
            if (affectRow > 0)
                return await Task.FromResult(string.Format("{0}?{1}", "https://localhost:54483/payment", "Status=1&Token=04774f73-1f6f-4d5d-e8d8-08dc5dce96e0"));
            else
                throw new BusinessConflictException("None UnPaid");


        }

        public async Task<string> PaymentVerify(string token, byte status)
        {
            try
            {
                var merchantId = configuration["Fennec:Payment:MerchantID"] ?? throw new BusinessConflictException("MerchantID UnPaid");
                var afterVerifyUrl = configuration["Fennec:Payment:AfterVerifyUrl"] ?? throw new BusinessConflictException("AfterVerifyUrl UnPaid");

                var row = dbContext.PaymentModel.FirstOrDefault(row => row.Token == token);
                if (row is null)
                    throw new BusinessConflictException("not found");

                row.Status = Verify(row, status);

                var CallbackURL = string.Empty;
                if (row.CallbackURL != null)
                    CallbackURL = $"?CallbackURL={row.CallbackURL}&";
                else
                    CallbackURL = $"?";


                CallbackURL += $"Status={row.Status}";

                return await Task.FromResult($"{afterVerifyUrl}/{row.Id}{CallbackURL}");
            }
            catch
            {
                throw;
            }
        }

        public async Task<byte> PaymentStatus(Guid payId)
        {
            var row = dbContext.PaymentModel.FirstOrDefault(row => row.Id == payId);

            if (row != null)
            {
                //check verify
                row.Status = Verify(row, row.StatusGetway);
                return await Task.FromResult(row.Status);
            }
            else
                throw new BusinessConflictException("not fount");

        }

        private Guid CurrentUserId()
        {
            Guid result = Guid.Empty;
            try
            {
                if (httpContextAccessor.HttpContext != null)
                {
                    var userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                    //var userId = httpContextAccessor.HttpContext.User.GetUserId();

                    if (userId != null)
                        result = Guid.Parse(userId);
                }
            }
            catch
            {

            }
            return result;
        }

        private byte Verify(PaymentModel paymentModel, byte? StatusGetway)
        {
            if (StatusGetway != null)
            {
                try
                {
                    if (paymentModel.Status == (byte)PaymentStatuses.Paying
                        || paymentModel.Status == (byte)PaymentStatuses.Verifying)
                    {
                        if (paymentModel.StatusGetway == null)
                            paymentModel.StatusGetway = StatusGetway;


                        if (StatusGetway == 1)
                        {
                            if (paymentModel.Status == (byte)PaymentStatuses.Paying)
                            {
                                paymentModel.Status = (byte)PaymentStatuses.Verifying;
                                dbContext.SaveChanges();
                            }

                            var StatusResult = 100;//check Status from Getway

                            if (StatusResult == 100 || StatusResult == 101)
                            {
                                //paymentModel.NumberTransaction =;
                                paymentModel.Status = (byte)PaymentStatuses.Paid;
                            }
                            else
                            {
                                paymentModel.Status = (byte)PaymentStatuses.UnPaid;
                            }
                            dbContext.SaveChanges();
                        }
                        else
                        {
                            paymentModel.Status = (byte)PaymentStatuses.UnPaid;
                            dbContext.SaveChanges();
                        }

                    }
                }
                catch
                {
                    throw;
                }
            }
            return paymentModel.Status;
        }
    }
}
