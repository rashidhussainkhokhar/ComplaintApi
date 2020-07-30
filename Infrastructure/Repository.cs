using Application.Interfaces;
using Infrastructure.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity:class
    {
		private readonly ComplaintDBContext _db;
		private readonly DbSet<TEntity> _entities;
		public Repository(ComplaintDBContext db)
		{
			_db = db;
			_entities = _db.Set<TEntity>();
		}
		public async Task AddAsync(TEntity entity)
		{
			await _entities.AddAsync(entity);
		}
		public void Update(TEntity entity)
		{
			_db.Entry(entity).CurrentValues.SetValues(entity);
		}
		public async Task DeleteAsync(int id)
		{
			var entity = await  GetAsync(id);
			_entities.Remove(entity);
		}
		public async Task<TEntity> GetAsync(int id)
		{
			return await _entities.FindAsync(id);
		}
		public  IQueryable<TEntity> GetAll()
		{
			return _entities.AsQueryable();
		}
		public async Task<IEnumerable<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate)
		{
			return await _entities.Where(predicate).ToListAsync();
		}
        public async Task<int> Complete()
        {
			return await _db.SaveChangesAsync();
        }
        public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
			return await _entities.SingleOrDefaultAsync(predicate);
        }
    }
}
