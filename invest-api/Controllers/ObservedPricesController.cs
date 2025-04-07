using invest_api.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace invest_api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ObservedPricesController : ControllerBase
  {
    // GET: api/<ObservedPricesController>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
      var res = await ApiService.ObservedPricesAsync();
      return Ok(res);
    }

  
  }
}
