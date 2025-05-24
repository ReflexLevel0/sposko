using Microsoft.AspNetCore.Mvc;
namespace sposko;

[Route("api/[controller]")]
[ApiController]
public class UserController(IUserService sportService) : ControllerBase
{
    [HttpGet]
    public async IAsyncEnumerable<UserDTO> Get()
    {
        var sports = sportService.GetUsers();
        await foreach (var sport in sports)
        {
            yield return sport;
        }
    }

    [HttpGet("{id}")]
    public async Task<UserDTO?> Get(Guid id)
    {
        return await sportService.GetUserById(id);
    }

    [HttpPost]
    public async Task<UserDTO?> Post([FromBody] CreateUserDTO sport)
    {
        return await sportService.CreateUser(sport);
    }

    [HttpDelete("{id}")]
    public async Task<int> Delete(Guid id)
    {
        return await sportService.DeleteUserById(id);
    }
}

