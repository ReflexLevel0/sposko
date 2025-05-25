using LinqToDB;

namespace sposko;

public class SportService(ISposkoDb db, IServiceHelper<Sport, SportDTO, CreateSportDTO, CreateSportDTO> serviceHelper) : ISportService
{
    private static Func<Sport, SportDTO> _mapper = sport => (SportDTO)sport;
    private Func<int, IQueryable<Sport?>> _getSportById = id => db.Sports.Where(s => s.Id == id);

    public async Task<SportDTO?> CreateSport(CreateSportDTO sport)
    {
        var insertTask = db.Sports.Value(s => s.Name, sport.Name).InsertAsync();
        var query = db.Sports.Where(s => string.CompareOrdinal(s.Name, sport.Name) == 0);
        return await serviceHelper.CreateObject(sport, insertTask, query, _mapper);
    }

    public async Task<SportDTO?> GetSportById(int id)
    {
        return await serviceHelper.GetObjectById(_getSportById.Invoke(id), _mapper);
    }

    public async IAsyncEnumerable<SportDTO> GetSports()
    {
        await foreach (var sport in serviceHelper.GetObjects(db.Sports.AsAsyncEnumerable(), _mapper))
        {
            yield return (SportDTO)sport;
        }
    }

    public async Task<int> DeleteSportById(int id)
    {
        return await serviceHelper.DeleteObjectById(_getSportById.Invoke(id));
    }
}
