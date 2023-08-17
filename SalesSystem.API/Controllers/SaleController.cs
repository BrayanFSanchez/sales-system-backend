using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using SalesSystem.BLL.Services.Contract;
using SalesSystem.API.Utility;
using SalesSystem.DTO;

namespace SalesSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ISaleService _saleService;

        public SaleController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] SaleDTO sale)
        {
            var rsp = new Response<SaleDTO>();

            try
            {
                rsp.status = true;
                rsp.value = await _saleService.Register(sale);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
            }

            return Ok(rsp);
        }

        [HttpGet]
        [Route("Record")]
        public async Task<IActionResult> Record(string tolookFor, string? saleNumber, string? startDate, string? endingDate)
        {
            var rsp = new Response<List<SaleDTO>>();
            saleNumber = saleNumber is null ? "" : saleNumber;
            startDate = startDate is null ? "" : startDate;
            endingDate = endingDate is null ? "" : endingDate;

            try
            {
                rsp.status = true;
                rsp.value = await _saleService.Record(tolookFor, saleNumber, startDate, endingDate);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
            }

            return Ok(rsp);
        }

        [HttpGet]
        [Route("Report")]
        public async Task<IActionResult> Report(string? startDate, string? endingDate)
        {
            var rsp = new Response<List<ReportDTO>>();

            try
            {
                rsp.status = true;
                rsp.value = await _saleService.Report(startDate, endingDate);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
            }

            return Ok(rsp);
        }
    }
}
