using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolOf.Data.Abstraction
{
    public interface IRepository<T> where T : BaseEntityModel
    {
        T GetById(long id);

        T Add(T entity);

        Task<bool> DeleteByIdAsync(long id);

        Task<T> UpdateAsync(T entity);

        IEnumerable<T> Find(Func<T, bool> searchCriteria); 
    }
}
