using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiWithJwtSwagger.Dto;
using WebApiWithJwtSwagger.Interfaces;
using WebApiWithJwtSwagger.Models;

namespace WebApiWithJwtSwagger.Controllers
{


    [Route("api/[controller]/[action]")]
    [ApiController]
    [AllowAnonymous]
    public class PaymentController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IPaymentRequestService _paymentService;

        public PaymentController(DatabaseContext databaseContext, IPaymentRequestService paymentRequestService)
        {
            _context = databaseContext;
            _paymentService = paymentRequestService;
        }
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<PaymentRequest>>> GetAllPaymentRequest()
        public async Task<ActionResult> GetAllPaymentRequest()
        {

            //    var l=await _context.PaymentRequest.Include(p=>p.AmountDetails)
            //    .ToListAsync();
            //return l;
            ResultDto<List<PaymentRequestDto>> x = await _paymentService.GetAll();
            var y = Ok(x);
            return y;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Products>> GetPayment(long id)
        {
            var payment = await _paymentService.GetPaymentAsync(id);
            if (payment.IsSuccess == true)
            { return Ok(payment); }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> AddPaymentRequest(PaymentRequestDto paymentRequest)
        {
            return Ok(await _paymentService.AddPaymentAsync(paymentRequest));
            // List<AmountDetail> list = new List<AmountDetail>();
            //foreach (var item in paymentRequest.AmountDetails)
            //{
            //    list.Add(
            //        new AmountDetail()
            //        {
            //            Key = item.Key,
            //            Value = item.Value,
            //            Effect = item.Effect
            //        }
            //        );

            //}

            //List<SubModelDetail> subModellist = new List<SubModelDetail>();
            //foreach (var item in paymentRequest.SubModelDetails)
            //{
            //    subModellist.Add(
            //        new SubModelDetail()
            //        {
            //            Key = item.Key,
            //            Value = item.Value,
            //        }
            //        );

            //}

            //PaymentRequest payr = new PaymentRequest()
            //{
            //    Amount = paymentRequest.Amount,
            //    //Id = paymentRequest.Id,
            //    AmountDetails = list,
            //    SubModelDetails = subModellist,
            //    TrxNumber = paymentRequest.TrxNumber,
            //    PaymentStatus = paymentRequest.PaymentStatus,
            //    RequestDate = paymentRequest.RequestDate,
            //    RequestTime = paymentRequest.RequestTime,

            //};
            //_context.PaymentRequest.Add(payr);
            //_context.SaveChanges();
            //return Ok();
        }
        //[HttpPut("{id}")]
        [HttpPost]
        public async Task<IActionResult> PutPayment(long id, PaymentRequestDto paymentRequest)
        {
            return Ok();
            ////var paym =await GetPayment(id);
            ////ActionResult? t =paym.Result;
            //var id = 3;
            //var p = await _context.PaymentRequest.FindAsync(id);
            //p.TrxNumber=paymentRequest.TrxNumber;
            //p.Amount = paymentRequest.Amount;
            //p.PaymentStatus = paymentRequest.PaymentStatus;
            //await _context.SaveChangesAsync();
            //return Ok(p);
            //if (id != paymentRequest.Id)
            //{
            //    return BadRequest();
            //}

            //_context.Entry(paymentRequest).State = EntityState.Modified;

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!PaymentExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            //return NoContent();

        }
        private bool PaymentExists(long id)
        {
            return _context.PaymentRequest.Any(e => e.Id == id);
        }
    
}
}