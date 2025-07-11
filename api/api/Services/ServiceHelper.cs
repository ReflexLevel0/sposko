using LinqToDB;
namespace sposko;

public class ServiceHelper<TEntity, TDTO, TCreateDTO, TUpdateDTO> : IServiceHelper<TEntity, TDTO, TCreateDTO, TUpdateDTO> where TDTO : class
{
    public async Task<TDTO?> CreateObject(TCreateDTO obj, Task<int> insertTask, IQueryable<TEntity?> getObjectQuery, Func<TEntity, TDTO> mapper)
    {
        try
        {
            await insertTask.WaitAsync(CancellationToken.None);
            var readObj = await getObjectQuery.FirstOrDefaultAsync(CancellationToken.None);
            return readObj == null ? null : mapper.Invoke(readObj);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
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

    public async Task<TDTO?> UpdateObject(TUpdateDTO obj, IQueryable<TEntity?> getObjectQuery, Task<int> updateTask, Func<TEntity, TDTO> mapper)
    {
        int changedRows = await updateTask.WaitAsync(CancellationToken.None);
        if (changedRows == 0) return null;
        var updatedObj = await getObjectQuery.FirstOrDefaultAsync();
        return updatedObj == null ? null : mapper.Invoke(updatedObj);
    }

    public async Task<int> DeleteObjectById(IQueryable<TEntity?> getObjectQuery)
    {
        return await getObjectQuery.DeleteAsync();
    }
}
