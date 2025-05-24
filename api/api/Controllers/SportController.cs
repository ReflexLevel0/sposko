using Microsoft.AspNetCore.Mvc;
namespace sposko;

[Route("api/[controller]")]
[ApiController]
public class SportController(ISportService sportService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(List<SportDTO>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<SportDTO>>> Get()
    {
        var sports = new List<SportDTO>();
        await foreach (var sport in sportService.GetSports())
        {
            sports.Add(sport);
        }

        return Ok(sports);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(SportDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SportDTO?>> Get(int id)
    {
        var sport = await sportService.GetSportById(id);
        if (sport == null) return NotFound(null);
        return Ok(sport);
    }

    [HttpPost]
    [ProducesResponseType(typeof(SportDTO), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<SportDTO?>> Post([FromBody] CreateSportDTO sport)
    {
        var insertedSport = await sportService.CreateSport(sport);
        if (insertedSport == null) return Conflict();
        return Created("/api/sport", insertedSport);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        if (await sportService.DeleteSportById(id) == 0) return NotFound();
        return Ok();
    }
}
