using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IRepository<TEntity> where TEntity:class
    {
		Task AddAsync(TEntity entity);
		void Update(TEntity entity);
		Task DeleteAsync(int id);
		IQueryable<TEntity> GetAll();
		Task<IEnumerable<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate);
		Task<TEntity> GetAsync(int id);
		Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
		Task<int> Complete();

	}
}
