using DapperExample.Entity;
using DapperExample.Handler;
using DapperExample.Sharedkernel;
using Microsoft.AspNetCore.Mvc;
using Sharedkernel.Helper;
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
                var companies = await _companyRepo.GetAllAsync();
                return Ok(companies);
            
        }

        [HttpPost]
        public async Task<IActionResult> Create(CompanyCreateModel model)
        {

            var companies = await _companyRepo.Create(model);
            return Helper.TransformData(companies);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetail(int id)
        {

            var result = await _companyRepo.GetById(id);
            return Helper.TransformData(result);

        }
    }
}
