using LinqToDB;

namespace sposko;

public class SportGroupService(ISposkoDb db, IServiceHelper<SportGroup, SportGroupDTO, CreateSportGroupDTO> serviceHelper) : ISportGroupService
{
    private static Func<SportGroup, SportGroupDTO> _mapper = group => (SportGroupDTO)group;
    private Func<int, IQueryable<SportGroup?>> _getGroupById = id => db.SportGroups.Where(s => s.Id == id);

    public async Task<SportGroupDTO?> CreateSportGroup(CreateSportGroupDTO group)
    {
        var insertTask = db.SportGroups
          .Value(g => g.TrainerId, group.TrainerId)
          .Value(g => g.SportId, group.SportId)
          .Value(g => g.MaxMembers, group.MaxMembers)
          .Value(g => g.MinAge, group.MinAge)
          .Value(g => g.MaxAge, group.MaxAge)
          .Value(g => g.Name, group.Name)
          .InsertAsync();
        var query = db.SportGroups.Where(g =>
            string.CompareOrdinal(g.Name, group.Name) == 0 &&
            g.TrainerId == group.TrainerId);
        return await serviceHelper.CreateObject(group, insertTask, query, _mapper);
    }

    public async Task<SportGroupDTO?> GetSportGroupById(int id)
    {
        return await serviceHelper.GetObjectById(_getGroupById.Invoke(id), _mapper);
    }

    public async IAsyncEnumerable<SportGroupDTO> GetSportGroups(Guid? trainerId = null)
    {
        var query = trainerId == null ?
          db.SportGroups :
          db.SportGroups.Where(g => g.TrainerId.Equals(trainerId));
        await foreach (var group in serviceHelper.GetObjects(query.AsAsyncEnumerable(), _mapper))
        {
            yield return group;
        }
    }

    public async Task<int> DeleteSportGroupById(int id)
    {
        return await serviceHelper.DeleteObjectById(_getGroupById.Invoke(id));
    }
}
