using LinqToDB;

namespace sposko;

public class TrainerService(ISposkoDb db, IServiceHelper<Trainer, TrainerDTO, CreateTrainerDTO, CreateTrainerDTO> serviceHelper) : ITrainerService
{
    private static Func<Trainer, TrainerDTO> _mapper = trainer => (TrainerDTO)trainer;
    private Func<Guid, IQueryable<Trainer?>> _getTrainerById = id => db.Trainers.Where(s => s.Id == id);

    public async Task<TrainerDTO?> CreateTrainer(CreateTrainerDTO trainer)
    {
        var insertTask = db.Trainers
          .Value(t => t.Id, trainer.Id)
          .Value(t => t.Info, trainer.Info)
          .Value(t => t.DateOfBirth, trainer.DateOfBirth)
          .InsertAsync();
        var query = db.Trainers.Where(t => string.CompareOrdinal(t.Info, trainer.Info) == 0);
        return await serviceHelper.CreateObject(trainer, insertTask, query, _mapper);
    }

    public async Task<TrainerDTO?> GetTrainerById(Guid id)
    {
        return await serviceHelper.GetObjectById(_getTrainerById.Invoke(id), _mapper);
    }

    public async IAsyncEnumerable<TrainerDTO> GetTrainers()
    {
        await foreach (var trainer in serviceHelper.GetObjects(db.Trainers.AsAsyncEnumerable(), _mapper))
        {
            yield return (TrainerDTO)trainer;
        }
    }

    public async Task<int> DeleteTrainerById(Guid id)
    {
        return await serviceHelper.DeleteObjectById(_getTrainerById.Invoke(id));
    }
}



