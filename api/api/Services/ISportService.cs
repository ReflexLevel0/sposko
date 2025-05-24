namespace sposko;

public interface ISportService
{
    Task<SportDTO?> CreateSport(CreateSportDTO sport);
    Task<SportDTO?> GetSportById(int id);
    IAsyncEnumerable<SportDTO> GetSports();
    Task<int> DeleteSportById(int id);
}
