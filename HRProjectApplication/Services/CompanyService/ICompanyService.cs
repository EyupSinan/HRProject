using HRProjectApplication.Models.DTOs;
using HRProjectApplication.Models.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRProjectApplication.Services.CompanyService
{
    public interface ICompanyService
    {
        Task UpdateCompany(UpdateCompanyDTO model);

        Task<UpdateCompanyDTO> GetById(int id);

        Task<CompanyDetailsVM> GetCompanyDetails(int id);

        Task<List<CompanyVM>> GetCompanies();

        Task Delete(int id);

        Task Create(CreateCompanyDTO model);
    }
}
