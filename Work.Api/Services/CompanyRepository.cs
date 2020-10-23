using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Work.Api.Data;
using Work.Api.DtoParameters;
using Work.Api.Entities;
using Work.Api.Helpers;
using Work.Api.Models;

namespace Work.Api.Services
{
    public class CompanyRepository: ICompanyRepository
    {
        private readonly WorkDbContext _context;
        private readonly IPropertyMappingService _propertyMappingService;
        public CompanyRepository(WorkDbContext context, IPropertyMappingService propertyMappingService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _propertyMappingService = propertyMappingService ?? throw new ArgumentNullException(nameof(propertyMappingService));
        }

        public async Task<PagedList<Company>> GetCompaniesAsync(CompanyDtoParameter parameters)
        {
            if(parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }
            var items = _context.Companies as IQueryable<Company>;
            if(!string.IsNullOrWhiteSpace(parameters.CompanyName))
            {
                parameters.CompanyName = parameters.CompanyName.Trim();
                items = items.Where(x => x.Name == parameters.CompanyName);
            }
            if(!string.IsNullOrWhiteSpace(parameters.SearchTerm))
            {
                parameters.SearchTerm = parameters.SearchTerm.Trim();
                items = items.Where(x => x.Name.Contains(parameters.SearchTerm) || x.Introduction.Contains(parameters.SearchTerm));
            }
            return await PagedList<Company>.CreateAsync(items, parameters.PageNumber, parameters.PageSize);
        }

        public async Task<Company> GetCompanyAsync(Guid companyId)
        {
            if(companyId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(companyId));
            }
            return await _context.Companies
            .FirstOrDefaultAsync(x => x.Id == companyId);
        }

        public async Task<IEnumerable<Company>> GetCompaniesAsync(IEnumerable<Guid> companyIds)
        {
            if(companyIds == null)
            {
                throw new ArgumentNullException(nameof(companyIds));
            }
            return await _context.Companies
                .Where(x => companyIds.Contains(x.Id))
                .OrderBy(x => x.Name)
                .ToListAsync();
        }
        
        public async Task<bool> CompanyExistsAsync(Guid companyId)
        {
            if(companyId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(companyId));
            }
            return await _context.Companies.AnyAsync(x => x.Id == companyId);
        }

        public void AddCompany(Company company)
        {
            if(company == null)
            {
                throw new ArgumentNullException(nameof(company));
            }
            company.Id = Guid.NewGuid();
            if(company.Employees != null)
            {
                foreach(var employee in company.Employees)
                {
                    employee.Id = Guid.NewGuid();
                }
            }
            
            _context.Companies.Add(company);
        }

        public void UpdateCompany(Company company)
        {
            _context.Entry(company).State = EntityState.Modified;
        }

        public void DeleteCompany(Company company)
        {
            if(company == null)
            {
                throw new ArgumentNullException(nameof(company));
            }
            _context.Companies.Remove(company);
        }



        public async Task<IEnumerable<Employee>> GetEmployeesAsync(Guid companyId, EmployeeDtoParameter parameters)
        {
            if(companyId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(companyId));
            }

            if(string.IsNullOrWhiteSpace(parameters.Gender) && string.IsNullOrWhiteSpace(parameters.Q))
            {
                return await _context.Employees
                    .Where(x => x.CompanyId == companyId)
                    .OrderBy(x => x.EmployeeNo)
                    .ToListAsync();
            }

            var items = _context.Employees.Where(x => x.CompanyId == companyId);
            if(!string.IsNullOrWhiteSpace(parameters.Gender))
            {
                parameters.Gender = parameters.Gender.Trim();
                var gender = Enum.Parse<Gender>(parameters.Gender);
                items = items.Where(x => x.Gender == gender);
            }
            if(!string.IsNullOrWhiteSpace(parameters.Q))
            {
                parameters.Q = parameters.Q.Trim();
                items = items.Where(x => x.EmployeeNo.Contains(parameters.Q) || x.FirstName.Contains(parameters.Q) || x.LastName.Contains(parameters.Q));
            }
            var mappingDictionary = _propertyMappingService.GetPropertyMapping<EmployeeDto, Employee>();

            items = items.ApplySort(parameters.OrderBy, mappingDictionary);

            return await items.ToListAsync();
        }

        public async Task<Employee> GetEmployeeAsync(Guid companyId, Guid employeeId)
        {
            if (companyId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(companyId));
            }
            if (employeeId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(companyId));
            }
            return await _context.Employees
                .Where(x => x.CompanyId == companyId)
                .FirstOrDefaultAsync(x => x.Id == employeeId);
        }

        public void AddEmployee(Guid companyId, Employee employee)
        {
            if(companyId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(companyId));
            }
            if(employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }
            employee.CompanyId = companyId;
            _context.Employees.Add(employee);
        }
        
        public void UpdateEmployee(Employee employee)
        {
            _context.Entry(employee).State = EntityState.Modified;
        }

        public void DeleteEmployee(Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }
            _context.Employees.Remove(employee);
        }



        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }
    }
}
