using Microsoft.AspNetCore.Mvc;
using ReceiptProcessorAPI.Models;
using ReceiptProcessorAPI.Services;

namespace ReceiptProcessorAPI.Controllers
{
    [ApiController]
    [Route("receipts")]
    public class ReceiptsController : ControllerBase
    {
        private readonly ReceiptService _receiptService;

        public ReceiptsController(ReceiptService receiptService)
        {
            _receiptService = receiptService;
        }

        [HttpPost("process")]
        public IActionResult ProcessReceipt([FromBody] Receipt receipt)
        {
            if (receipt == null || !ModelState.IsValid)
            {
                return BadRequest("The receipt is invalid.");
            }

            string id = _receiptService.SaveReceipt(receipt);
            return Ok(new { id });
        }

        [HttpGet("{id}/points")]
        public IActionResult GetPoints(string id)
        {
            int points = _receiptService.GetPoints(id);
            if (points == -1)
                return NotFound("No receipt found for that ID.");

            return Ok(new { points });
        }
    }
}
