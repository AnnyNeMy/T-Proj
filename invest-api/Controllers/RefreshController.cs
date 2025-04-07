using invest_api.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace invest_api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class RefreshController : ControllerBase
  {
    [HttpPatch]
    public async Task<IActionResult> Patch()
    {
      try
      {
        var res = await DownloadDataService.RefreshAsync();
        return res != 0 && res != -1 ? Ok(new { message = "Обновление успешно" }) : StatusCode(500, new { message = "Ошибка обновления" });
      } catch (Exception ex)
      {
        return StatusCode(500, new { message = ex.Message });
      }
    }


  }
}
