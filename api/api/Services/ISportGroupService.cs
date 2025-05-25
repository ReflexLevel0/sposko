namespace sposko;

public interface ISportGroupService
{
    Task<SportGroupDTO?> CreateSportGroup(CreateSportGroupDTO group);
    Task<SportGroupDTO?> GetSportGroupById(int id);
    IAsyncEnumerable<SportGroupDTO> GetSportGroups(Guid? trainerId);
    Task<int> DeleteSportGroupById(int id);
}
