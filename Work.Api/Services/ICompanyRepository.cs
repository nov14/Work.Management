using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Work.Api.DtoParameters;
using Work.Api.Entities;
using Work.Api.Helpers;

namespace Work.Api.Services
{
    public interface ICompanyRepository
    {
        Task<PagedList<Company>> GetCompaniesAsync(CompanyDtoParameter parameters);
        Task<Company> GetCompanyAsync(Guid companyId);
        Task<IEnumerable<Company>> GetCompaniesAsync(IEnumerable<Guid> companyIds);
        Task<bool> CompanyExistsAsync(Guid companyId);
        void AddCompany(Company company);
        void UpdateCompany(Company company);
        void DeleteCompany(Company company);


        Task<IEnumerable<Employee>> GetEmployeesAsync(Guid companyId,EmployeeDtoParameter parameters);
        Task<Employee> GetEmployeeAsync(Guid companyId, Guid employeeId);
        void AddEmployee(Guid companyId, Employee employee);
        void UpdateEmployee(Employee employee);
        void DeleteEmployee(Employee employee);

        Task<bool> SaveAsync();
    }
}
