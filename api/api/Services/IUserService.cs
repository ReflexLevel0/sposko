namespace sposko;

public interface IUserService
{
    Task<UserDTO?> CreateUser(CreateUserDTO sport);
    Task<UserDTO?> GetUserById(Guid id);
    IAsyncEnumerable<UserDTO> GetUsers();
    Task<int> DeleteUserById(Guid id);
}

