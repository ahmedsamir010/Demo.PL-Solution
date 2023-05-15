using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Interfaces
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        public IEmployeeRepository EmployeeRepository { get; set; }     
        public IDepartmentRepositories  DepartmentRepositories { get; set; }



        Task<int> Complete();

            

    }
}
    