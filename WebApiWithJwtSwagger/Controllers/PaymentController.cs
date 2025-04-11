using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Security.Policy;
using WebApiWithJwtSwagger.Dto;
using WebApiWithJwtSwagger.Interfaces;
using WebApiWithJwtSwagger.Models;
using RestSharp;
using static WebApiWithJwtSwagger.UseFull;
using WebApiWithJwtSwagger.Server.Models;
using Method = RestSharp.Method;

namespace WebApiWithJwtSwagger.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    [AllowAnonymous]
    [Tags("Payment.Api")]
    public class PaymentController : ControllerBase
    {
        private readonly PaymentDbContext _context;
        private readonly IPaymentRequestService _paymentService;


        string localMerchant = "86b93019-ea9c-4ced-8608-da6eda071538";//a5eeba9d-de7e-4e2d-be4a-bbd67eeacd8d
        string loaclAmount = "450000";
        string authority;
        string description = "test test";
        string callbackurl = "http://localhost:2812/Home/VerifyByHttpClient";


        public PaymentController(PaymentDbContext databaseContext, IPaymentRequestService paymentRequestService)
        {
            _context = databaseContext;
            _paymentService = paymentRequestService;
        }
        [HttpGet]
        public async Task<ActionResult> GetAllPayment() => Ok(await _paymentService.GetAll());

        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentRequest>> GetPayment(long id) => Ok(await _paymentService.GetPaymentAsync(id));

        [HttpPost]
        public async Task<ActionResult> AddPayment(PaymentRequestDto paymentRequest)
        {
            await _paymentService.AddPaymentAsync(paymentRequest);
            return Ok();
        }



        [HttpPost]
        public async Task<IActionResult> DoPayment(ZarrinPalPaymentParameter zarrinPalPaymentParameterDto)
        {

            try
            {
                newRequestParameters Parameters = new newRequestParameters(zarrinPalPaymentParameterDto.Merchant_Id, zarrinPalPaymentParameterDto.Amount.ToString(), zarrinPalPaymentParameterDto.Description, zarrinPalPaymentParameterDto.CallBackUrl, "", "");


                //be dalil in ke metadata be sorate araye ast va do meghdare mobile va email dar metadata gharar mmigirad
                //shoma mitavanid in maghadir ra az kharidar begirid va set konid dar gheir in sorat khali ersal konid

                var client = new RestClient(URLs.requestUrl);

                RestSharp.Method method = Method.Post;

                var request = new RestRequest("", method);

                request.AddHeader("accept", "application/json");

                request.AddHeader("content-type", "application/json");

                request.AddJsonBody(Parameters);

                var requestresponse = client.ExecuteAsync(request);

                JObject jo = JObject.Parse(requestresponse.Result.Content);

                string errorscode = jo["errors"].ToString();

                JObject jodata = JObject.Parse(requestresponse.Result.Content);

                string dataauth = jodata["data"].ToString();


                if (dataauth != "{}")
                {


                    authority = jodata["data"]["authority"].ToString();

                    string gatewayUrl = URLs.gateWayUrl + authority;

                    return Ok(gatewayUrl);
                    //  return Redirect(gatewayUrl);

                }
                else
                {

                    return BadRequest("error " + errorscode);
                }


            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);


            }
        }



        [HttpPost]
        public async Task<IActionResult> VerifyPayment(string authority, string amount, string merchant)
        {

            try
            {
                VerifyParameters parameters = new VerifyParameters();

                parameters.authority = authority;// authority;

                parameters.amount = amount;

                parameters.merchant_id = merchant;

                var client = new RestClient(URLs.verifyUrl);

                Method method = Method.Post;

                var request = new RestRequest("", method);

                request.AddHeader("accept", "application/json");

                request.AddHeader("content-type", "application/json");

                request.AddJsonBody(parameters);

                var response = client.ExecuteAsync(request);


                JObject jodata = JObject.Parse(response.Result.Content);

                string data = jodata["data"].ToString();

                JObject jo = JObject.Parse(response.Result.Content);

                string errors = jo["errors"].ToString();

                if (data != "{}")
                {
                    string refid = jodata["data"]["ref_id"].ToString();
                    Object obj = new
                    {
                        Code = jodata["data"]["code"].ToString(),
                        Message = jodata["data"]["message"].ToString(),
                        Card_Pan = jodata["data"]["card_pan"].ToString(),
                        Fee = jodata["data"]["fee"].ToString(),
                        Shaparak_Fee = jodata["data"]["shaparak_fee"].ToString(),
                        Refid = jodata["data"]["ref_id"].ToString()

                    };
                    return Ok(JsonConvert.SerializeObject(obj));
                }
                else if (errors != "[]")
                {

                    string errorscode = jo["errors"]["code"].ToString();

                    return BadRequest($"error code {errorscode}");

                }

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return NotFound();
        }


    }
}