using invest_api.Services;
using invest_api.SQL;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace invest_api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class PortfolioReportController : ControllerBase
  {
    // GET: api/<PortfolioReportController>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
      var PortfolioReport = await ApiService.PortfolioReportAsync();

      return Ok(PortfolioReport);
    }

    // GET api/<PortfolioReportController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
      return "value";
    }

    // POST api/<PortfolioReportController>
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }


    // DELETE api/<PortfolioReportController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
  }
}
