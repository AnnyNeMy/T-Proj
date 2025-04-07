using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace invest_api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class HistoryPriceController : ControllerBase
  {
    // GET: api/<HistoryPriceController>
    [HttpGet]
    public IEnumerable<string> Get()
    {
      return new string[] { "value1", "value2" };
    }

    // GET api/<HistoryPriceController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
      return "value";
    }

    // POST api/<HistoryPriceController>
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/<HistoryPriceController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<HistoryPriceController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
  }
}
