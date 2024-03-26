using IOTBack.Configuracao;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
namespace IOTBack.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SmsController : ControllerBase
    {
        private readonly SmsService _smsService;

        public SmsController(SmsService smsService)
        {
            _smsService = smsService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendSms([FromBody] SmsRequest request)
        {
            string result = await _smsService.SendSms(request.PhoneNumber??"", request.Message ?? "");
            return Ok(new { Result = result });
        }

    }


    public class SmsRequest
    {
        public string? PhoneNumber { get; set; }
        public string? Message { get; set; }
    }
}
