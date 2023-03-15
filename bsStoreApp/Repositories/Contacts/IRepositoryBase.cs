using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contacts
{
    public interface IRepositoryBase<T>
    {
        //CRUD
        void Create(T entity);
        IQueryable<T> FindAll(bool trackChanges);
        IQueryable<T> FindByCondition(Expression<Func<T,bool>> expression,bool trackChanges);
        void Update(T entity);
        void Delete(T entity);
    }
}
