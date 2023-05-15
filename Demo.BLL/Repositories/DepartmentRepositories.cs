using Demo.BLL.Interfaces;
using Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Context;
namespace Demo.BLL.Repositories
{
    public class DepartmentRepositories : GenericRepository<Department>, IDepartmentRepositories
    {
        private readonly DataContext _dbContext;

        public DepartmentRepositories(DataContext dbContext):base(dbContext)
        {
            _dbContext = dbContext;
        }

    }
}
