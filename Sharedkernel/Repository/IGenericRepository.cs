using DapperExample.Sharedkernel;

namespace DapperExample.Infracstructure.Repository
{
    public interface IGenericRepository<T, Z, Y> where T: class
    {
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<Response> Create(Z obj);
        Task<Response> UpdateAsync(Y obj);
        Task<Response> DeleteAsync(Guid id);
    }
}
