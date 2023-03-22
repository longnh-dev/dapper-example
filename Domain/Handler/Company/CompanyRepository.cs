
using Dapper;
using DapperExample.Entity;
using DapperExample.Infracstructure.Repository;
using DapperExample.Sharedkernel;
using Infracstructure.Context;
using Serilog;
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

        public Task<Response> DeleteAsync(int id)
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

        public async Task<Response> GetById(int id)
        {
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var company = connection.Query<Company>("SELECT * FROM Company WHERE Id = @Id", new { Id = id }).FirstOrDefault();

                    if (company == null)
                        return new Response(HttpStatusCode.BadRequest, "Bản ghi không tồn tại");

                    return new Response<Company>(HttpStatusCode.OK, company, "Lấy bản ghi thành công!");
                }

            }
            catch(Exception ex)
            {
                Log.Error(ex, string.Empty);
                Log.Information("Params: Id: {@id}",  id);
                return new Response(HttpStatusCode.InternalServerError, "Đã xảy ra lỗi trong quá trình xử lý");
            }
        }


        public async Task<Response> UpdateAsync(CompanyUpdateModel model, int id)
        {
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var company = connection.Execute("UPDATE Company SET Name=@name, Address=@address, Country=@country WHERE Id=@id",
                        param: new {});

                    return new Response(HttpStatusCode.OK,"Update success!");
                }
            }
            catch(Exception ex)
            {
                Log.Error(ex, string.Empty);
                Log.Information("Params: Model: {@model}, Id: {@id}", model, id);
                return new Response(HttpStatusCode.InternalServerError, "Đã xảy ra lỗi trong quá trình xử lý");
            }
        }

        
    }
}
