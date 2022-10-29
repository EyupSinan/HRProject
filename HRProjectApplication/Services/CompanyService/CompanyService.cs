using AutoMapper;
using HRProjectApplication.Models.DTOs;
using HRProjectApplication.Models.VMs;
using HRProjectDomain.Entities;
using HRProjectDomain.Enums;
using HRProjectDomain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRProjectApplication.Services.CompanyService
{
    public class CompanyService : ICompanyService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(ICompanyRepository companyRepository, IMapper mapper, UserManager<AppUser> userManager)
        {
            _mapper = mapper;
            _companyRepository = companyRepository;
            _userManager = userManager;
        }

        public async Task<UpdateCompanyDTO> GetById(int id)
        {
            var company = await _companyRepository.GetDefault(x => x.Id == id);
            var model = _mapper.Map<UpdateCompanyDTO>(company);
            return model;
        }

        public async Task<CompanyDetailsVM> GetCompanyDetails(int id)
        {
            var company = await _companyRepository.GetFilteredFirstOrDefault(
                select: x => new CompanyDetailsVM
                {
                    Id = x.Id,
                    CompanyName = x.CompanyName,
                    Address = x.Address,
                    TaxOffice = x.TaxOffice,
                    TaxNumber = x.TaxNumber,
                    PhoneNumber = x.PhoneNumber,
                    ContactPerson = x.ContactPerson,
                    NumberOfEmployees = x.NumberOfEmployees
                },
                where: x => x.Id == id);
            //include: x=> x.Include(x => x.Users));
            return company;
        }

        public async Task<List<CompanyVM>> GetCompanies()
        {           
            var companies = await _companyRepository.GetFilteredList(
                select: x => new CompanyVM
                {
                    Id = x.Id,
                    CompanyName = x.CompanyName,
                    Address = x.Address,
                    TaxOffice = x.TaxOffice,
                    TaxNumber = x.TaxNumber,
                    PhoneNumber = x.PhoneNumber,
                    ContactPerson = x.ContactPerson,
                    NumberOfEmployees =x.NumberOfEmployees,
                    WebSite = x.WebSite,
                    Status = x.Status,
                    CreateDate = x.CreateDate
                },
                where: x => x.Status != Status.Passive,
                orderBy: x => x.OrderBy(x => x.CompanyName));
            //include: x => x.Include(x => x.Users));
            return companies;
        }

        public async Task UpdateCompany(UpdateCompanyDTO model)
        {
            var updatedCompany = _mapper.Map<Company>(model);
            await _companyRepository.Update(updatedCompany);
        }

        public async Task Delete(int id)
        {
            var company = await _companyRepository.GetDefault(x => x.Id == id);
            company.Status = Status.Passive;
            company.DeleteDate = DateTime.Now;
            await _companyRepository.Delete(company);
        }

        public async Task Create(CreateCompanyDTO model)
        {
            var company = _mapper.Map<Company>(model);
            await _companyRepository.Create(company);
        }
    }
}
