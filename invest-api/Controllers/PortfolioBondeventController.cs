using invest_api.SQL;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace invest_api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class PortfolioBondeventController : ControllerBase
  {
    // GET: api/<PortfolioBondeventController>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
      var positions = await PortfolioDBService.GetBondeventAsync();
      return Ok(positions);
    }

    // GET api/<PortfolioBondeventController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
      return "value";
    }

    // POST api/<PortfolioBondeventController>
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/<PortfolioBondeventController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<PortfolioBondeventController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
  }
}
