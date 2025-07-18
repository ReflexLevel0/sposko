namespace sposko;

public interface IServiceHelper<TEntity, TDTO, TCreateDTO, TUpdateDTO>
{
    Task<TDTO?> CreateObject(TCreateDTO obj, Task<int> insertTask, IQueryable<TEntity?> getObjectQuery, Func<TEntity, TDTO> mapper);
    Task<TDTO?> GetObjectById(IQueryable<TEntity?> getObject, Func<TEntity, TDTO> mapper);
    IAsyncEnumerable<TDTO> GetObjects(IAsyncEnumerable<TEntity> getObjects, Func<TEntity, TDTO> mapper);
    Task<TDTO?> UpdateObject(TUpdateDTO obj, IQueryable<TEntity?> getObjectQuery, Task<int> updateTask, Func<TEntity, TDTO> mapper);
    Task<int> DeleteObjectById(IQueryable<TEntity?> getObjectQuery);
}
