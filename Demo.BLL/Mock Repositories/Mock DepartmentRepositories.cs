using Demo.BLL.Interfaces;
using Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Mock_Repositories
{
    internal class Mock_DepartmentRepositories : IDepartmentRepositories
    {
        public Task Add(Department item)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Department item)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Department>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Department> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Department> Update(Department item)
        {
            throw new NotImplementedException();
        }
    }
}
