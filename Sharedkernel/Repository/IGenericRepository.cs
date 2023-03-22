using DapperExample.Sharedkernel;

namespace DapperExample.Infracstructure.Repository
{
    public interface IGenericRepository<T, Z, Y> where T: class
    {
        Task<Response> GetById(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<Response> Create(Z obj);
        Task<Response> UpdateAsync(Y obj, int id);
        Task<Response> DeleteAsync(int id);
    }
}
