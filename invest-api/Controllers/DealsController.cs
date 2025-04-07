using invest_api.SQL;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace invest_api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class DealsController : ControllerBase
  {
    // GET: api/<DealsController>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
      var bonds = await BondsDBService.GetBondsAsync();
      return Ok(bonds);
    }

    // GET api/<DealsController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
      return "value";
    }

    // POST api/<DealsController>
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/<DealsController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<DealsController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
  }
}
