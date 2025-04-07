using invest_api.Common;
using invest_api.Contracts;
using invest_api.Services;
using Microsoft.AspNetCore.Mvc;
using static Google.Rpc.Context.AttributeContext.Types;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace invest_api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class PositionController : ControllerBase
  {
    // GET: api/<PositionController>
    [HttpGet("favorite")]
    public async Task<IActionResult> GetFavoritePositionReport()
    {
      var FaviritePositionReport = await ApiService.FaviritePositionReportAsync();

      return Ok(FaviritePositionReport);
    }

    [HttpGet("position")]
    public async Task<IActionResult> GetPositionReport()
    {
      var PositionReport = await ApiService.PositionReportAsync();

      return Ok(PositionReport);
    }

    // GET api/<PositionController>/5
    [HttpGet("{isin}")]
    public async Task<IActionResult> Get(string isin)
    {
      var res = await ApiService.PositionDealsReportAsync(isin);
      return Ok(res);
    }

  }
}
