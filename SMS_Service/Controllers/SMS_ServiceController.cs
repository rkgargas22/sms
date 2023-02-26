using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMS_Service.Model;
using SMS_Service.Services.Contracts;

namespace SMS_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SMS_ServiceController : ControllerBase
    {
        private readonly ISMS_Interface _smsInterface;

        public SMS_ServiceController(ISMS_Interface smsInterface)
        {
            _smsInterface = smsInterface;
        }

        [HttpPost("SendSMS")]
        public async Task<IActionResult> SendSMSRequest(SMSRequestModel requestModel)
        {
            var result = await _smsInterface.SendSMSService(requestModel);
            if (!result.IsSuccess)
            {
                return StatusCode(500, result);
            }
            return Ok(result);
        }

    }
}
