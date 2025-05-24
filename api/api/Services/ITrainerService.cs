namespace sposko;

public interface ITrainerService
{
    Task<TrainerDTO?> CreateTrainer(CreateTrainerDTO trainer);
    Task<TrainerDTO?> GetTrainerById(Guid id);
    IAsyncEnumerable<TrainerDTO> GetTrainers();
    Task<int> DeleteTrainerById(Guid id);
}
