using Microsoft.AspNetCore.Mvc;
namespace sposko;

[Route("api/[controller]")]
[ApiController]
public class SportGroupController(ISportGroupService groupService) : ControllerBase
{
    [HttpGet]
    public async IAsyncEnumerable<SportGroupDTO> Get()
    {
        var groups = groupService.GetSportGroups();
        await foreach (var group in groups)
        {
            yield return group;
        }
    }

    [HttpGet("{id}")]
    public async Task<SportGroupDTO?> Get(int id)
    {
        return await groupService.GetSportGroupById(id);
    }

    [HttpPost]
    public async Task<SportGroupDTO?> Post([FromBody] CreateSportGroupDTO group)
    {
        return await groupService.CreateSportGroup(group);
    }

    [HttpDelete("{id}")]
    public async Task<int> Delete(int id)
    {
        return await groupService.DeleteSportGroupById(id);
    }
}
