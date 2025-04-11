using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  WebApiWithJwtSwagger.Models
{
   public class ZarrinPalPaymentParameter
    {
        public  string  Merchant_Id { get; set; }
        public int Amount { get; set; }
        public  string Description { get; set; }
        public  string CallBackUrl { get; set; }
        public  string[]? MetaData { get; set; }

    }
}
