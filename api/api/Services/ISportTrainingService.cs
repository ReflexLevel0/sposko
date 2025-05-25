namespace sposko;

public interface ISportTrainingService
{
    Task<SportTrainingDTO?> CreateSportTraining(CreateSportTrainingDTO training);
    Task<SportTrainingDTO?> GetSportTrainingById(int id);
    IAsyncEnumerable<SportTrainingDTO> GetSportTrainings(int? groupId);
    Task<SportTrainingDTO?> UpdateSportTraining(int id, UpdateSportTrainingDTO newTraining);
    Task<int> DeleteSportTrainingById(int id);
}
