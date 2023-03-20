using DapperExample.Entity;
using DapperExample.Infracstructure.Repository;

namespace DapperExample.Handler
{
    public interface ICompanyRepository : IGenericRepository<Company, CompanyCreateModel>
    {
    }
}
