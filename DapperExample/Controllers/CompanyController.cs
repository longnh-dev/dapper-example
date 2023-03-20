using DapperExample.Entity;
using DapperExample.Handler;
using DapperExample.Sharedkernel;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DapperExample.Controllers
{
    [Route("api/companies")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepo;
        public CompaniesController(ICompanyRepository companyRepo)
        {
            _companyRepo = companyRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            try
            {
                var companies = await _companyRepo.GetAllAsync();
                return Ok(companies);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }



        }

        [HttpPost]
        public async Task<Response> Create(CompanyCreateModel model)
        {

            var companies = await _companyRepo.Create(model);
            return new Response(HttpStatusCode.OK, "Created");

        }

        [HttpGet("{id}")]
        public async Task<Response> GetDetail(int id)
        {

            var companies = await _companyRepo.GetById(id);
            return new Response<Company>(HttpStatusCode.OK, companies);

        }
    }
}
