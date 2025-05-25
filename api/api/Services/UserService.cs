using LinqToDB;

namespace sposko;

public class UserService(ISposkoDb db, IServiceHelper<User, UserDTO, CreateUserDTO, CreateUserDTO> serviceHelper) : IUserService
{
    private static Func<User, UserDTO> _mapper = user => (UserDTO)user;
    private Func<Guid, IQueryable<User?>> _getUserById = id => db.Users.Where(s => s.Id == id);

    public async Task<UserDTO?> CreateUser(CreateUserDTO user)
    {
        var insertTask = db.Users
          .Value(u => u.Username, user.Username)
          .Value(u => u.Password, user.Password)
          .Value(u => u.FirstName, user.FirstName)
          .Value(u => u.LastName, user.LastName)
          .Value(u => u.Email, user.Email)
          .Value(u => u.PhoneNumber, user.PhoneNumber)
          .InsertAsync();
        var query = db.Users.Where(u => string.CompareOrdinal(u.Email, user.Email) == 0);
        return await serviceHelper.CreateObject(user, insertTask, query, _mapper);
    }

    public async Task<UserDTO?> GetUserById(Guid id)
    {
        return await serviceHelper.GetObjectById(_getUserById.Invoke(id), _mapper);
    }

    public async IAsyncEnumerable<UserDTO> GetUsers()
    {
        await foreach (var user in serviceHelper.GetObjects(db.Users.AsAsyncEnumerable(), _mapper))
        {
            yield return (UserDTO)user;
        }
    }

    public async Task<int> DeleteUserById(Guid id)
    {
        return await serviceHelper.DeleteObjectById(_getUserById.Invoke(id));
    }
}


