using LinqToDB;

namespace sposko;

public class SportTrainingService(ISposkoDb db, IServiceHelper<SportTraining, SportTrainingDTO, CreateSportTrainingDTO> serviceHelper) : ISportTrainingService
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

    public async Task<int> DeleteSportTrainingById(int id)
    {
        return await serviceHelper.DeleteObjectById(_getTrainingById.Invoke(id));
    }
}

