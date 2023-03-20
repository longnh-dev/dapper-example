using DapperExample.Sharedkernel;

namespace DapperExample.Infracstructure.Repository
{
    public interface IGenericRepository<T, Z> where T: class
    {
        Task<T> GetById(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<Response> Create(Z obj);
        Task<Response> UpdateAsync(T obj);
        Task<Response> DeleteAsync(Guid id);
    }
}
