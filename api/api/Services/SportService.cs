using LinqToDB;

namespace sposko;

public class SportService(ISposkoDb db) : ISportService
{
    public async Task<SportDTO?> CreateSport(CreateSportDTO sport)
    {
        await db.Sports
          .Value(s => s.Name, sport.Name)
          .InsertAsync();
        var sportDb = await (
            from s in db.Sports
            where string.CompareOrdinal(s.Name, sport.Name) == 0
            select s).FirstOrDefaultAsync();
        return sportDb == null ? null : (SportDTO)sportDb;
    }

    public async Task<SportDTO?> GetSportById(int id)
    {
        var sport = await
          (from s in db.Sports
           where s.Id == id
           select s)
            .FirstOrDefaultAsync();
        return sport == null ? null : (SportDTO)sport;
    }

    public async IAsyncEnumerable<SportDTO> GetSports()
    {
        var sports = (from s in db.Sports
                      select s).AsAsyncEnumerable();
        await foreach (var sport in sports)
        {
            yield return (SportDTO)sport;
        }
    }

    public async Task<SportDTO?> DeleteSportById(int id)
    {
        var sportQuery = db.Sports.Where(s => s.Id == id);
        var sport = await sportQuery.FirstOrDefaultAsync();
        if (sport == null) return null;

        int changedRows = await sportQuery.DeleteAsync();
        if (changedRows == 0) return null;
        return (SportDTO)sport;
    }
}
