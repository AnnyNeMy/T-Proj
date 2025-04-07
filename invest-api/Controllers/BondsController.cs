using invest_api.Contracts;
using invest_api.Services;
using invest_api.SQL;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace invest_api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class BondsController : ControllerBase
  {
// GET: api/<BondsController>
[HttpGet]
    public async Task<IActionResult> Get()
    {
      var bonds = await DealsDBService.GetDealsAsync();

      return Ok(bonds);
    }

    // GET api/<BondsController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
      return "value";
    }

    // POST api/<BondsController>
    [HttpPost]
    public async Task<List<BondsReport>> Post ([FromBody] BondsReportRequest request)
    {
      return await ApiService.GetBondAsync(request);
    }

    // PUT api/<BondsController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<BondsController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
  }
}
