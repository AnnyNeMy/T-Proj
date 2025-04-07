using invest_api.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace invest_api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class MoneyReportController : ControllerBase
  {
    // GET: api/<MoneyReportController>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
      var MoneyReport = await ApiService.MoneyReportAsync();

      return Ok(MoneyReport);
    }

    // GET api/<MoneyReportController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
      return "value";
    }

    // POST api/<MoneyReportController>
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/<MoneyReportController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<MoneyReportController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
  }
}
