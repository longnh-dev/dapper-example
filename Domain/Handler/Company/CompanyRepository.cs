
using Dapper;
using DapperExample.Entity;
using DapperExample.Sharedkernel;
using Infracstructure.Context;
using System.Linq;
using System.Net;

namespace DapperExample.Handler
{


    public class CompanyRepository : ICompanyRepository
    {
        private readonly DapperContext _context;
        public CompanyRepository(DapperContext context)
        {
            _context = context;
        }

     

        public async Task<Response> Create(CompanyCreateModel model)
        {
            try
            {
                //open connection database
                var connection = _context.CreateConnection();
               
                Company company = new Company()
                {
                    Id = model.Id,
                    Name = model.Name,
                    Address = model.Address,
                    Country  = model.Country
                };
                var sql = "INSERT INTO Company (id, name, address, country) VALUES (@id, @name, @address, @country)";

                var response = await connection.ExecuteAsync(sql, company);
                if (response > 0)
                    return new Response(HttpStatusCode.OK, "Created!");
                else
                    return new Response(HttpStatusCode.BadRequest, "Have an exception"); 
                
            }
            catch (Exception ex)
            {
                return new Response(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        public Task<Response> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Company>> GetAllAsync()
        {
            var query = "SELECT * FROM Company";
            using (var connection = _context.CreateConnection())
            {
                var companies = await connection.QueryAsync<Company>(query);
                return companies.ToList();
            }
        }

        public async Task<Company> GetById(Guid id)
        {
            try
            {
                var connection = _context.CreateConnection();
                return connection.Query<Company>("", new { Id = id }).FirstOrDefault();
            }
            catch(Exception ex)
            {
                throw new NotImplementedException();
            }
        }


        public Task<Response> UpdateAsync(Company obj)
        {
            throw new NotImplementedException();
        }
    }
}
