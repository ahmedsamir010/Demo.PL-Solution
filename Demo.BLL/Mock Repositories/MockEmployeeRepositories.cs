using Demo.BLL.Interfaces;
using Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Mock_Repositories
{
    internal class MockEmployeeRepositories : IEmployeeRepository
    {
        public Task Add(Employee item)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Employee item)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Employee>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Employee> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<Employee>> GetEmployeeByAddress(string address)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<Employee>> SearchEmployeeByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<Employee> Update(Employee item)
        {
            throw new NotImplementedException();
        }
    }
}
