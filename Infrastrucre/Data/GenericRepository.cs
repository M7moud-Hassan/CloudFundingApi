using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class GenericRepository<T>  : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _storeContext;
        public GenericRepository(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }
        public async Task<T> CreateAsync(T entity)
        {
             await _storeContext.Set<T>().AddAsync(entity);
            _storeContext.SaveChanges();
            return entity;
        }

        public async Task<RegisterEntity?> ActiveUserAsync(string email)
        {
           var register= await  _storeContext.Registers.FirstOrDefaultAsync(u => u.Email == email);
            if (register != null)
            {
                register.IsActive = true;
                _storeContext.SaveChanges();
                return register;
            }
            else
            {
                return null;
            }
        }

        public Task<T> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
