using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebApplication5.Models;
using X.PagedList;

namespace WebApplication5.IRepository
{
    public interface IGenericRepository< T> where T : class
    {
        Task<IList<T>> GetAll(
            Expression<Func<T, bool>> expression = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy=null,
            List<string> includes= null
            );

        Task<IPagedList<T>> GetAll(RequestParams requestParams,
            List<string> includes = null);

        Task< T> Get(
             Expression<Func<T, bool>> expression = null,
             
            List<string> includes = null
            );

        Task Insert(T entity);
        Task InsertRange(IEnumerable<T> entity);
        Task Delete(int id);
        void DeleteRange(IEnumerable<T> entity);
        void Update(T entity);
    }
}
