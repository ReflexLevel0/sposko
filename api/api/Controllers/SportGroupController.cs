using Microsoft.AspNetCore.Mvc;
namespace sposko;

[Route("api/[controller]")]
[ApiController]
public class SportGroupController(ISportGroupService groupService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(List<SportGroupDTO>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<SportGroupDTO>>> Get()
    {
        var groups = new List<SportGroupDTO>();
        await foreach (var group in groupService.GetSportGroups())
        {
            groups.Add(group);
        }
        return Ok(groups);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(SportGroupDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SportGroupDTO?>> Get(int id)
    {
        var group = await groupService.GetSportGroupById(id);
        return group == null ? NotFound() : Ok(group);
    }

    [HttpPost]
    [ProducesResponseType(typeof(SportGroupDTO), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<SportGroupDTO?>> Post([FromBody] CreateSportGroupDTO group)
    {
        var insertedGroup = await groupService.CreateSportGroup(group);
        return insertedGroup == null ? Conflict() : Ok(insertedGroup);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        if (await groupService.DeleteSportGroupById(id) == 0) return NotFound();
        return Ok();
    }
}
