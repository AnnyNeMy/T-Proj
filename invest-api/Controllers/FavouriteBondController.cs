using invest_api.Common;
using invest_api.SQL;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace invest_api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class FavouriteBondController : ControllerBase
  {
    // GET: api/<FavouriteBondController>
    [HttpGet]
    public async Task<List<FavouriteBond>> Get()
    {
      var fbounds = await FavouriteBondDBService.GetFavouriteBondsAsync();
      return fbounds;
    }

    // POST api/<FavouriteBondController>
    [HttpPost]
    public async Task<CommonResponse> Post([FromBody] CommonRequest isin)
    {
      return await FavouriteBondDBService.WriteFavouriteBondAsync(isin.Id);
    }


    // DELETE api/<FavouriteBondController>/5
    [HttpDelete("{id}")]
    public async Task<int> Delete(string id)
    {
      return await FavouriteBondDBService.CleanFavouriteBondByIsinAsync(id);
    }

    // DELETE api/<FavouriteBondController>
    [HttpDelete]
    public async Task<int> Delete()
    {
      return await FavouriteBondDBService.CleanFavouriteBondAsync();
    }
  }
}
