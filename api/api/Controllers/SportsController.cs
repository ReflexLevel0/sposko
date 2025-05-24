using Microsoft.AspNetCore.Mvc;
namespace sposko;

[Route("api/[controller]")]
[ApiController]
public class SportsController(ISportService sportService) : ControllerBase
{
    [HttpGet]
    public async IAsyncEnumerable<SportDTO> Get()
    {
        var sports = sportService.GetSports();
        await foreach (var sport in sports)
        {
            yield return sport;
        }
    }

    [HttpGet("{id}")]
    public async Task<SportDTO?> Get(int id)
    {
        return await sportService.GetSportById(id);
    }

    [HttpPost]
    public async Task<SportDTO?> Post([FromBody] CreateSportDTO sport)
    {
        return await sportService.CreateSport(sport);
    }

    [HttpDelete("{id}")]
    public async Task<SportDTO?> Delete(int id)
    {
        return await sportService.DeleteSportById(id);
    }
}
