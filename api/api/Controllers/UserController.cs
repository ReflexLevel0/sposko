using Microsoft.AspNetCore.Mvc;
namespace sposko;

[Route("api/[controller]")]
[ApiController]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(List<UserDTO>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<UserDTO>>> Get()
    {
        var users = new List<UserDTO>();
        await foreach (var user in userService.GetUsers())
        {
            users.Add(user);
        }
        return users;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserDTO?>> Get(Guid id)
    {
        var user = await userService.GetUserById(id);
        return user == null ? NotFound() : Ok(user);
    }

    [HttpPost]
    [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<UserDTO?>> Post([FromBody] CreateUserDTO user)
    {
        var insertedUser = await userService.CreateUser(user);
        return insertedUser == null ? Conflict() : Ok(insertedUser);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(Guid id)
    {
        int changedRows = await userService.DeleteUserById(id);
        return changedRows == 0 ? NotFound() : Ok();
    }
}

