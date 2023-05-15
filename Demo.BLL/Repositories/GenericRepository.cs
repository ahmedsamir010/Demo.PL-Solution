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
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DataContext _dbContext;
        public GenericRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Add(T item)
        {
            await _dbContext.Set<T>().AddAsync(item);
        }

        public async Task Delete(T item)
        {
             _dbContext.Set<T>().Remove(item);              
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            if (typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>)_dbContext.Employees.Include(E => E.Department).ToList();
            }
            else
            {
                return await _dbContext.Set<T>().ToListAsync();

            }
        }

        public async Task<T> GetById(int id)
            => await _dbContext.Set<T>().FindAsync(id);

        public async Task<T> Update(T item)
        {
            _dbContext.Update(item);
            await _dbContext.SaveChangesAsync();
            return item;
        }

    }
}
