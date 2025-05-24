using LinqToDB;
namespace sposko;

public class ServiceHelper<TEntity, TDTO, TCreateDTO> : IServiceHelper<TEntity, TDTO, TCreateDTO> where TDTO : class
{
    public async Task<TDTO?> CreateObject(TCreateDTO obj, Task<int> insertTask, IQueryable<TEntity?> getObjectQuery, Func<TEntity, TDTO> mapper)
    {
        await insertTask.WaitAsync(CancellationToken.None);
        var readObj = await getObjectQuery.FirstOrDefaultAsync(CancellationToken.None);
        return readObj == null ? null : mapper.Invoke(readObj);
    }

    public async Task<TDTO?> GetObjectById(IQueryable<TEntity?> getObject, Func<TEntity, TDTO> mapper)
    {
        var obj = await getObject.FirstOrDefaultAsync(CancellationToken.None);
        return obj == null ? null : mapper.Invoke(obj);
    }

    public async IAsyncEnumerable<TDTO> GetObjects(IAsyncEnumerable<TEntity> getObjects, Func<TEntity, TDTO> mapper)
    {
        await foreach (var obj in getObjects)
        {
            yield return mapper.Invoke(obj);
        }
    }

    public async Task<int> DeleteObjectById(IQueryable<TEntity?> getObjectQuery)
    {
        return await getObjectQuery.DeleteAsync();
    }
}
