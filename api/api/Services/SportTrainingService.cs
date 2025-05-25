using LinqToDB;

namespace sposko;

public class SportTrainingService(ISposkoDb db, IServiceHelper<SportTraining, SportTrainingDTO, CreateSportTrainingDTO, UpdateSportTrainingDTO> serviceHelper) : ISportTrainingService
{
    private static Func<SportTraining, SportTrainingDTO> _mapper = training => (SportTrainingDTO)training;
    private Func<int, IQueryable<SportTraining?>> _getTrainingById = id => db.SportTrainings.Where(t => t.Id == id);

    public async Task<SportTrainingDTO?> CreateSportTraining(CreateSportTrainingDTO training)
    {
        var insertTask = db.SportTrainings
          .Value(t => t.GroupId, training.GroupId)
          .Value(t => t.Cost, training.Cost)
          .Value(t => t.StartDate, training.StartDate)
          .Value(t => t.StartTime, training.StartTime)
          .Value(t => t.Duration, training.Duration)
          .Value(t => t.EndDate, training.EndDate)
          .Value(t => t.RepeatType, training.RepeatType)
          .Value(t => t.RepeatInterval, training.RepeatInterval)
          .InsertAsync();
        var query = db.SportTrainings.Where(g =>
            g.GroupId == training.GroupId &&
            g.StartDate == training.StartDate
        );
        return await serviceHelper.CreateObject(training, insertTask, query, _mapper);
    }

    public async Task<SportTrainingDTO?> GetSportTrainingById(int id)
    {
        return await serviceHelper.GetObjectById(_getTrainingById.Invoke(id), _mapper);
    }

    public async IAsyncEnumerable<SportTrainingDTO> GetSportTrainings(int? groupId)
    {
        var query = groupId == null ?
          db.SportTrainings :
          db.SportTrainings.Where(t => t.GroupId == groupId);
        await foreach (var training in serviceHelper.GetObjects(query.AsAsyncEnumerable(), _mapper))
        {
            yield return training;
        }
    }

    public async Task<SportTrainingDTO?> UpdateSportTraining(int id, UpdateSportTrainingDTO newTraining)
    {
        var updateTask = db.SportTrainings
          .Where(t => t.Id == id)
          .Set(t => t.GroupId, t => newTraining.GroupId ?? t.GroupId)
          .Set(t => t.StartDate, t => newTraining.StartDate ?? t.StartDate)
          .Set(t => t.StartTime, t => newTraining.StartTime ?? t.StartTime)
          .Set(t => t.Duration, t => newTraining.Duration ?? t.Duration)
          .Set(t => t.EndDate, t => newTraining.EndDate ?? t.EndDate)
          .Set(t => t.RepeatType, t => newTraining.RepeatType ?? t.RepeatType)
          .Set(t => t.RepeatInterval, t => newTraining.RepeatInterval ?? t.RepeatInterval)
          .Set(t => t.Cost, t => newTraining.Cost ?? t.Cost)
          .UpdateAsync();
        return await serviceHelper.UpdateObject(newTraining, _getTrainingById.Invoke(id), updateTask, _mapper);
    }

    public async Task<int> DeleteSportTrainingById(int id)
    {
        return await serviceHelper.DeleteObjectById(_getTrainingById.Invoke(id));
    }
}

