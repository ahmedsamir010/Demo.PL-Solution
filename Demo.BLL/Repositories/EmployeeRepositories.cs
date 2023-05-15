using Demo.BLL.Interfaces;
using Demo.DAL.Context;
using Demo.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class EmployeeRepositories : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly DataContext _dbContex;

        public EmployeeRepositories(DataContext dbContex):base(dbContex)
        {
            _dbContex = dbContex;
        }

        public Task<IQueryable<Employee>> GetEmployeeByAddress(string address)
        {
            throw new NotImplementedException();
        }

        public async Task<IQueryable<Employee>> SearchEmployeeByName(string name)
        {
            var result = await _dbContex.Employees.Where(E => E.Name.ToLower().Contains(name.ToLower())).ToListAsync();
            return result.AsQueryable();
        }

    }
}
