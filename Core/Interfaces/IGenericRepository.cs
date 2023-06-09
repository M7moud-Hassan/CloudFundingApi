﻿using Core.Entities;
using Core.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> CreateAsync(T entity);
        Task<T> GetByIdAsync(int id);
        //Task<AppUser?> ActiveUserAsync(string email);
    }
}
