﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Data.Abstraction
{
    public interface IRepository<T> where T : BaseEntityModel
    {
        Task<T> GetByIdAsync(long id);

        T Add(T entity);

        Task<bool> DeleteByIdAsync(long id);

        T Update(T entity);
        IEnumerable<T> Find(Expression<Func<T, bool>> searchCriteria);
        IEnumerable<T> Find(Expression<Func<T, bool>> searchCriteria, int pageNumber, int pageSize);
        IEnumerable<T> Find(Expression<Func<T, bool>> searchCriteria, string includePropertyByName);
    }
}
