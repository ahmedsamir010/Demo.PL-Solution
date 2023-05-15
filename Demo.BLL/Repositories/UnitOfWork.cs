using Demo.BLL.Interfaces;
using Demo.DAL.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _dbContext;

        public IEmployeeRepository EmployeeRepository { get; set; }
        public IDepartmentRepositories DepartmentRepositories { get; set; }

        public UnitOfWork(DataContext dbContext)
        {
            EmployeeRepository = new EmployeeRepositories(dbContext);
            DepartmentRepositories = new DepartmentRepositories(dbContext);
            _dbContext = dbContext;
        }

        public async Task<int> Complete()
        {
            return await _dbContext.SaveChangesAsync();
        }


        public async ValueTask DisposeAsync()
        {
            await _dbContext.DisposeAsync();
        }


    }
}
