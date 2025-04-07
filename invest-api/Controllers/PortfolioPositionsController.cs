using invest_api.SQL;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace invest_api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class PortfolioPositionsController : ControllerBase
  {
    // GET: api/<PortfolioController>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
      var positions = await PortfolioDBService.GetPositionsAsync();
      return Ok(positions);
    }

    // GET api/<PortfolioController>/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
     // var positions = PortfolioDBService.GetPosition(id);
      return Ok(null);
    }

    // POST api/<PortfolioController>
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/<PortfolioController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<PortfolioController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
  }
}
