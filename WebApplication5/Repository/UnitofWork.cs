using WebApplication5.IRepository;
using System;
using System.Threading.Tasks;
using WebApplication5.Data;

namespace WebApplication5.Repository
{
    public class UnitofWork : IUnitofWork
    {
        private readonly DatabaseContext context;
        private IGenericRepository<Country> _countries;
        private IGenericRepository<Hotel> _hotels;

        public UnitofWork(DatabaseContext _context)
        {
            context = _context;
        }
        public IGenericRepository<Country> Countries => _countries ??= new GenericRepository<Country>(context);
        public IGenericRepository<Hotel> Hotels=> _hotels ??= new GenericRepository<Hotel>(context);

        public void Dispose()
        {
            context.Dispose();
            GC.SuppressFinalize(this);

        } 
        public async Task Save()
        {
            await context.SaveChangesAsync();
        }
    }
}
