using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Razorpay.Api;
using Hotel.Domain.Models;

namespace Hotel.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RazorPayController : ControllerBase
    {
        public IConfiguration _configuration;
        public RazorPayController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("create-order")]
        public async Task<IActionResult> CreateOrder([FromBody] RazorPaymentRequest obj)
        {

            RazorpayClient client = new RazorpayClient(_configuration["RazorPay:key"], _configuration["RazorPay:secretKey"]);

            Dictionary<string, object> input = new Dictionary<string, object>();

            Random random = new Random();
            string TransactionId = random.Next(0, 1000).ToString();

            input.Add("amount", obj.Amount * 100); // multiplied because to convert to paisa 
            input.Add("currency", "INR");
            input.Add("receipt", TransactionId);

            Razorpay.Api.Order order = client.Order.Create(input);

            return Ok(new
            {
                orderId = order["id"].ToString(),
                currency = order["currency"].ToString(),
                amount = Convert.ToInt32(order["amount"])
            });

        }

        [HttpPost("verify-payment")]
        public async Task<IActionResult> VerifyPayment([FromBody] RazorPayVerifyRequest obj)
        {
            RazorpayClient client = new RazorpayClient(_configuration["RazorPay:key"], _configuration["RazorPay:secretKey"]);

            Dictionary<string, string> attributes = new Dictionary<string, string>();
            attributes.Add("razor_payment_id", obj.razorpay_payment_id);
            attributes.Add("razor_order_id", obj.razorpay_order_id);
            attributes.Add("signature", obj.razorpay_signature);

            Utils.verifyPaymentLinkSignature(attributes);

            return Ok(new { message = "Payment verified successfully" });

        }
    }
}
