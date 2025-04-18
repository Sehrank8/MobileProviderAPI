using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobileProviderAPI.Data.Svc;
using MobileProviderAPI.Model.Dto;

namespace MobileProviderAPI.Controllers
{
    [ApiController]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class BillingController : ControllerBase
    {
        private readonly IBillingService _billingService;

        public BillingController(IBillingService billingService)
        {
            _billingService = billingService;
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost("AddUsage")]
        public IActionResult AddUsage([FromBody] UsageDto dto)
        {
            return Ok(_billingService.AddUsage(dto));
        }

        [Authorize(Roles = "User,Administrator")]
        [HttpGet("CalculateBill")]
        public IActionResult CalculateBill(string subscriberNo, int month, int year)
        {
            var result = _billingService.CalculateBill(subscriberNo, month, year);
            if (result == null)
                return NotFound(new { message = "Usages not found for the given subscriber and date." });
            return Ok(_billingService.CalculateBill(subscriberNo, month, year));
        }

        [Authorize(Roles = "User,Administrator")]
        [HttpGet("QueryBillDetailed")]
        public IActionResult QueryBillDetailed(string subscriberNo, int month, int year, int page = 1, int pageSize = 10)
        {
            var result = _billingService.QueryBillDetailed(subscriberNo, month, year, page,pageSize);
            if (result == null)
                return NotFound(new { message = "Bill not found for the given subscriber and date." });
            return Ok(result);
        }

        [HttpGet("QueryBill")]
        public IActionResult QueryBill(string subscriberNo, int month, int year)
        {
            var result = _billingService.QueryBill(subscriberNo,month, year);
            if (result == null)
                return NotFound(new { message = "Bill not found for the given subscriber and date." });
            return Ok(result);
        }

        [HttpPost("PayBill")]
        public IActionResult PayBill(string subscriberNo, int month, int year)
        {
            return Ok(_billingService.PayBill(subscriberNo, month, year));
        }
    }
}