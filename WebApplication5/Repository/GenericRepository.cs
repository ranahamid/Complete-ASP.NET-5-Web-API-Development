
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebApplication5.Data;
using WebApplication5.IRepository;
using WebApplication5.Models;
using X.PagedList;

namespace WebApplication5.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DatabaseContext context;
        private readonly DbSet<T> db;

        public GenericRepository(DatabaseContext _context)
        {
            context = _context;
            db= context.Set<T>();
        }
      public async Task  Delete(int id)
        {
            var entity= await db.FindAsync(id);
            if (entity != null)
            {
                db.Remove(entity);
            }
        }

        public  void DeleteRange(IEnumerable<T> entity)
        {
         db.RemoveRange(entity);
        }

        public async Task<T> Get(Expression<Func<T, bool>> expression, List<string> includes) 
        {
            IQueryable<T> query = db;
            if (includes != null)
            {
                foreach(var item in includes)
                {
                    query = query.Include(item);
                }
            }            
            return await query.AsNoTracking().FirstOrDefaultAsync(expression);
        }

        public async Task<IList<T>> GetAll(Expression<Func<T, bool>> expression, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, List<string> includes)
        {
            IQueryable<T> query = db;
            if(expression != null)
            {
                query= query.Where(expression);
            }
          
            if (includes != null)
            {
                foreach (var item in includes)
                {
                    query = query.Include(item);
                }
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            return await query.AsNoTracking().ToListAsync ();
        }

        public async Task<IPagedList<T>> GetAll(RequestParams requestParams,List<string> includes = null)
        {
            IQueryable<T> query = db; 
            if (includes != null)
            {
                foreach (var item in includes)
                {
                    query = query.Include(item);
                }
            } 
            return await  query.AsNoTracking().ToPagedListAsync(requestParams.PageNumber,requestParams.PageSize);
        }

        public async Task Insert(T entity)
        {
            await db.AddAsync(entity);
        }

        public async Task InsertRange(IEnumerable<T> entity)
        {
            await db.AddRangeAsync(entity);
        }

        public  void Update(T entity)
        {
            db.Attach(entity);
            context.Entry(entity).State= EntityState.Modified;

        }
    }
}
