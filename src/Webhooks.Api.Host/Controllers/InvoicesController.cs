//using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Webhooks.Models.Dtos;
using Webhooks.Models.Parameters;
using Webhooks.Services.Interfaces;

namespace Webhooks.Api.Host.Controllers
{
    [//Authorize,
     ApiVersion("1.0"),
     Route("api/v{version:apiVersion}/[controller]"),
     ApiController]
    public class InvoicesController : ControllerBase
    {
        private readonly IInvoiceService _service;

        public InvoicesController(IInvoiceService service)
        {
            _service = service;
        }

        /// <summary>
        /// Add invoice
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] InvoiceParameters parameters)
        {
            await _service.AddAsync(parameters);

            return NoContent();
        }

        /// <summary>
        /// Update invoice
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [HttpPut("{invoiceId}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid invoiceId, [FromBody] InvoiceParameters parameters)
        {
            await _service.UpdateAsync(invoiceId, parameters);

            return NoContent();
        }

        /// <summary>
        /// Delete invoice
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        [HttpDelete("{invoiceId}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid invoiceId)
        {
            await _service.DeleteAsync(invoiceId);

            return NoContent();
        }

        /// <summary>
        /// Get invoice
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        [HttpGet("{invoiceId}"),
         ProducesResponseType(typeof(InvoiceDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync([FromRoute] Guid invoiceId)
        {
            var result = await _service.GetAsync(invoiceId);

            return Ok(result);
        }

        /// <summary>
        /// Get invocies
        /// </summary>
        /// <returns></returns>
        [HttpGet,
         ProducesResponseType(typeof(IEnumerable<InvoiceDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _service.GetAllAsync();

            return Ok(result);
        }

        /// <summary>
        /// Approve invoice
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        [HttpPost("{invoiceId}/approve")]
        public async Task<IActionResult> ApproveAsync([FromRoute] Guid invoiceId)
        {
            await _service.ApproveAsync(invoiceId);

            return NoContent();
        }
    }
}